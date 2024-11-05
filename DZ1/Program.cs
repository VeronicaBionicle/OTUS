using System.Reflection;

namespace DZ1 {
    /// <summary>Состояния команд</summary>
    public enum Status : int {
        Exit = -2,
        Error = -1,
        Ok = 0,
        TryAgain = 1
    }; 

    internal class Menu {
        static Dictionary<string, string> commands = new Dictionary<string, string> () {
         { "start", "Начало работы и/или настройка имени пользователя для обращения. Имя можно ввести как аргумент через пробел"},
         { "help", "Краткая справка"},
         { "echo", "Возврат введенного после команды текста (*недоступно без ввода имени)"},
         { "info", "Информация о версии программы и дате создания"},
         { "exit", "Выход из программы" }
        };

        static string ? _line, _command, _text; // вводимая линия, команда и текст после команды
        static string ? _userName = ""; // имя пользователя

        /// <summary>Форматирует строку под имя пользователя, если заполнено</summary>
        /// <param name="message">Входная строка</param>
        /// <returns>Отформатированная строка</returns>
        static public string FormatWithName(string message) {
            if (string.IsNullOrEmpty(_userName)){
                return message;
            }
            else { // Мои сообщения точно длинее двух символов, проверку добавлять не стала
                return _userName + ", " + message[..1].ToLower() + message[1..];
            }
        }

        /// <summary>Метод обработки введенной строки с выделением команды и аргумента</summary>
        static public Status GetCommandTextFromLine(){
            Console.WriteLine(FormatWithName("Введите команду с аргументом и нажмите Enter. Для выхода введите /exit или нажмите сочетание Ctrl+Z"));
            _line = Console.ReadLine();

            if (_line == null){
                return Status.Exit;
            }

            char[] delimiterChars = [' ', '\t'];
            var parts = _line.Split(delimiterChars, 2); // Делим на две части - команду и текст после нее

            _command = parts[0];
            _text = parts.Length > 1 ? parts[1] : ""; // Подставлем строку с ввода или пустую

            if (_command.Length < 2 || _command[0] != '/') {
                Console.WriteLine(FormatWithName("Некорректный формат команды, попробуйте еще раз"));
                return Status.TryAgain;
            }

            _command = parts[0].TrimStart('/');
            if (!commands.ContainsKey(_command)) {
                Console.WriteLine(FormatWithName("Неизвестная команда, попробуйте еще раз"));
                return Status.TryAgain;
            }
           
            return Status.Ok;
        }

        /// <summary>Метод получения краткой справки</summary>
        static public void GetHelp(){
            Console.WriteLine(FormatWithName("Доступные команды:"));
            foreach (var rec in commands){
                Console.WriteLine($"/{rec.Key}\t{rec.Value}");
            }
        }

        /// <summary>Метод ввода и сохранения имени</summary>
        static public void SetName(){
            if (string.IsNullOrEmpty(_text)){ // если сразу не ввели, спрашиваем
                Console.WriteLine("Введите имя для обращения к вам:");
                _userName = Console.ReadLine();
            }
            else { 
                _userName = _text;
            }
        }

        /// <summary>Метод для возвращения введенного текста</summary>
        static public void Echo(){
            Console.WriteLine(_text);
        }

        /// <summary>Метод получения информации по программе</summary>
        static public void GetInfo(){
            var app = Assembly.GetExecutingAssembly();
            var name = app.GetName();
            var appFile = new FileInfo(app.Location);
            Console.WriteLine(FormatWithName("Информация о боте для вас:"));
            Console.WriteLine("Имя: {0}, версия: {1}, дата создания: {2}, дата последнего обновления: {3}",
                              name.Name, name.Version, appFile.CreationTime, appFile.LastWriteTime);
        }

        /// <summary>Метод обработки команд</summary>
        /// <returns>Статус выполнения</returns>
        static public Status ProcessCommand() {
            // Если имя не вводили, завставляем ввести
            if (string.IsNullOrEmpty(_userName) && _command == "echo") {
                Console.WriteLine("Чтобы воспользоваться функцией echo, сначала начните работу командой /start");
                return Status.TryAgain;
            }

            switch (_command){ // Можно было сделать словарь с функциями, но не хочу забегать вперед
                case "start": SetName(); break;
                case  "help": GetHelp(); break;
                case  "info": GetInfo(); break;
                case  "echo": Echo(); break;
                case  "exit": return Status.Exit;
                default :     Console.WriteLine(FormatWithName("Неизвестная команда, попробуйте еще раз"));
                              return Status.TryAgain;
            }
            return Status.Ok;
        }
    }
    internal class Program {
        static void Main(string[] args){
            Console.Clear();
            Console.WriteLine("Добро пожаловать в прототип меню бота");
            Menu.GetHelp();
            Console.WriteLine("Для начала работы введите /start");
            var status = Status.Ok;

            do { // Цикл ввода команд
                status = Menu.GetCommandTextFromLine();

                if (status == Status.TryAgain) continue; // на входе что-то не так, просим ввести команду раз
                if (status == Status.Exit) break;        // решили выйти по Ctrl+Z

                status = Menu.ProcessCommand(); // Обрабатываем команду и текст
            } while (status != Status.Exit);
            // Прощаемся перед выходом
            Console.WriteLine(Menu.FormatWithName("До свидания, выходим из программы..."));
        }
    }
}