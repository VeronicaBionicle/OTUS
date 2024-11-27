using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json;
using Dapper;
using Npgsql;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace DZ_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Читаем файл с настройками
            var fileName = "D:\\CSharp\\Курс OTUS Basic\\DZ_17\\settings.json";
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл \"{fileName}\" не найден");
                return;
            }
            string data = File.ReadAllText(fileName);

            // Получаем настройки из Json
            var databaseSettings = JsonSerializer.Deserialize<DatabaseSettings>(data);
            if (databaseSettings is null)
            {
                Console.WriteLine("Настройки в неправильном формате!");
                return;
            }
            var dbConnection = databaseSettings.CreateDatabaseConnectionString();

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            // Создаем подключение и делаем запросы
            using (IDbConnection db = new NpgsqlConnection(dbConnection))
            {
                // "Общий запрос" для таблицы customers
                var customerQuery = $@"select * from customers
                          where @{nameof(CustomerFilter.MinAge)} <= age and
                          age <= @{nameof(CustomerFilter.MaxAge)} and
                          firstname ilike @{nameof(CustomerFilter.FirstName)} and
                          lastname ilike @{nameof(CustomerFilter.LastName)}
                          order by firstname limit @{nameof(CustomerFilter.Limit)}";

                // Параметры фильтра для customers
                var customerFilter = new CustomerFilter
                {
                    MinAge = 18,
                    MaxAge = 70,
                    FirstName = "%123%",
                    LastName = "%",
                    Limit = 10
                };

                // Запрос пользователей с возрастом от MinAge до MaxAge
                var customersByAge = db.Query<Customer>(customerQuery,
                    new { customerFilter.MinAge, customerFilter.MaxAge, customerFilter.FirstName, customerFilter.LastName, customerFilter.Limit }
                ).ToList();

                Console.WriteLine($"{customerFilter.Limit} покупателей возраста от {customerFilter.MinAge} до {customerFilter.MaxAge} и именем с символами {customerFilter.FirstName.Replace("%", string.Empty)}");

                foreach (var customer in customersByAge)
                {
                    Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Age}");
                }

                customerFilter.Limit = 5;
                // Запрос ФИ первых 5 пользователей, у которых нет заказов
                var customersWithoutOrders = db.Query<string>("select firstname || ' ' || lastname from customers c"
                    + $" where not exists (select 1 from orders o where o.customerid = c.id) limit @{nameof(CustomerFilter.Limit)}",
                    new { customerFilter.Limit }).ToList();

                Console.WriteLine($"\n{customerFilter.Limit} покупателей без заказов");

                foreach (var customer in customersWithoutOrders)
                {
                    Console.WriteLine($"{customer}");
                }

                // Вставка новых продуктов и просмотр их в базе
                var newProducts = new List<object> {
                    new { Id = 0, Name = "Крупа полба", Description = "Крупа из особого сорта пшеницы", StockQuantity = 15, Price = 150.0 },
                    new { Id = 0, Name = "Каша Nordic Rice", Description = "Хлопья рисовые", StockQuantity = 18, Price = 200.0 },
                    new { Id = 0, Name = "Макароны Барилла", Description = "Перья", StockQuantity = 5, Price = 126 }
                };

                var insertQuery = @"insert into products (name, description, stockquantity, price)
                                    values (@name, @description, @stockquantity, @price)
                                    returning id";
                Console.WriteLine("\nВставка новых продуктов:");
                foreach (var product in newProducts) 
                {
                    var newId = db.ExecuteScalar<int>(insertQuery, product);
                    Console.WriteLine($"Вставлен продукт с id = {newId}");
                    var newProduct = db.QueryFirstOrDefault<Product>("select * from products where id = @Id", new { Id = newId });
                    // Проверим, что записалось в таблицу
                    Console.WriteLine($"Наименование: {newProduct.Name}, описание: {newProduct.Description}, количество: {newProduct.StockQuantity}, цена: {newProduct.Price}");
                }

                // Найдем пользователей с общей стоимостью заказов менее 50000000 у.е.
                var minTotal = 50000000;
                var smallOrdersQUery = @"with small_customers as (
                                            select o.customerid
                                            from orders o
                                            join products p on o.productid = p.id
                                            group by o.customerid
                                            having sum(quantity * p.price) < @TotalBuys
                                          ) 
                                        select * from orders
                                        where customerid in (select customerid from small_customers)";

                var smallOrders = db.Query<Order>(smallOrdersQUery,  new { TotalBuys = minTotal }).ToList();
                Console.WriteLine($"\nНашли {smallOrders.Count} заказов у пользователей с суммой заказов менее {minTotal}.");

                // Удвоим в этих заказа количество товаров
                var updateQuery = "update orders set quantity = quantity * 2 where id = @Id";
                var rowsChanged = 0;
                foreach (var order in smallOrders) 
                {
                    rowsChanged += db.Execute(updateQuery, new { order.Id });
                    
                }
                Console.WriteLine($"Изменили {rowsChanged} строк.");
            }
        }
    }
}
