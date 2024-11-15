/*
1. В СУБД PostgreSQL создать БД Shop
*/
CREATE DATABASE shop OWNER postgres; 

/*
2. Создать таблицы
Customers (ID, FirstName, LastName, Age),
Products (ID, Name, Description, StockQuantity, Price)
Orders (ID, CustomerID, ProductID, Quantity)
3. Установить между ними соответствующие связи по внешним ключам (в каждой таблице поле ID является первичным ключом)
*/
CREATE TABLE customers (
	id int GENERATED ALWAYS AS IDENTITY NOT NULL,
	FirstName text NOT NULL,
	LastName text,
	Age int,
	CONSTRAINT PK_customers_id PRIMARY KEY(id)
);

CREATE TABLE products (
	id int GENERATED ALWAYS AS IDENTITY NOT NULL,
	Name text NOT NULL,
	Description text,
	StockQuantity int,
	Price decimal,
	CONSTRAINT PK_products_id PRIMARY KEY(id)
);

CREATE TABLE orders (
	id int GENERATED ALWAYS AS IDENTITY NOT NULL,
	CustomerID int, 
	ProductID int,
	Quantity int,
	CONSTRAINT PK_orders_id PRIMARY KEY(id),
	CONSTRAINT FK_orders_customer_id FOREIGN KEY(ProductID) REFERENCES customers(id),
	CONSTRAINT FK_orders_product_id FOREIGN KEY(ProductID) REFERENCES products(id)
);

/*
4. Заполнить таблицы произвольными значениями (с корректными значениями для внешних ключей). В каждой таблице не менее 10 записей.
*/
-- Заполнение пользователей
INSERT INTO customers (FirstName, LastName, Age)
 SELECT 'name-' || round(random()*1000)::text || chr(ascii('B') + (random() * 25)::integer) FirstName,
 		'surname-' || round(random()*1000)::text LastName,
		round(1 + random()*100)::integer Age
 FROM generate_series(1, 9000);
 
-- Заполнение товаров 
INSERT INTO products (Name, Description, StockQuantity, Price)
 SELECT 'name-' || round(random()*1000)::text Name,
		'Description: ' || round(random()*1000000)::text Description,
		round(random()*100)::int StockQuantity,
		round(random()::numeric * 20000, 2) Price
FROM generate_series(1, 9000);
 
-- Заполнение заказов
INSERT INTO orders (CustomerID, ProductID, Quantity)
 SELECT round(1 + random()*99)::integer CustomerID,
		round(1 + random()*99)::integer ProductID,
		round(1 + random()*49)::integer	Quantity  
 FROM generate_series(1, 27000);

SELECT * FROM products;
SELECT * FROM customers;  
SELECT * FROM orders;

/*
5. Написать запрос, который возвращает список всех пользователей старше 30 лет, у которых есть заказ на продукт с ID=1.
Используйте alias, чтобы дать столбцам в результирующей выборке понятные названия.
В результате должны получить таблицу:
CustomerID, FirstName, LastName, ProductID, ProductQuantity, ProductPrice
*/
SELECT c.ID CustomerID,  FirstName, LastName, o.ProductID, sum(o.Quantity) ProductQuantity, p.Price ProductPrice
FROM orders o
JOIN customers c ON o.CustomerID = c.Id
JOIN products p ON o.ProductID = p.Id
WHERE c.Age > 30 -- Старше 30
AND EXISTS (SELECT 1 FROM orders oo WHERE oo.CustomerID = c.Id AND oo.ProductID=1 LIMIT 1) -- есть заказ на продукт с ID=1
GROUP BY c.ID,  FirstName, LastName, o.ProductID, p.Price
ORDER BY 1, 4;

/*
6. Убедитесь, что вы повесили необходимый некластерный индекс
*/
DROP INDEX IF EXISTS customers_age_idx;
DROP INDEX IF EXISTS orders_ProductId_idx;

CREATE INDEX customers_age_idx ON customers (Age);
CREATE INDEX orders_ProductId_idx ON orders (ProductId); 

EXPLAIN
SELECT c.ID CustomerID,  FirstName, LastName, o.ProductID, sum(o.Quantity) ProductQuantity, p.Price ProductPrice
FROM orders o
JOIN customers c ON o.CustomerID = c.Id
JOIN products p ON o.ProductID = p.Id
WHERE c.Age > 30 -- Старше 30
AND EXISTS (SELECT 1 FROM orders oo WHERE oo.CustomerID = c.Id AND oo.ProductID=1 LIMIT 1) -- есть заказ на продукт с ID=1
GROUP BY c.ID,  FirstName, LastName, o.ProductID, p.Price
ORDER BY 1, 4;

