using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            InterfaceDataBase DB;
            ControllerDataBase ctrlDB = new ControllerDataBase();

            //bool flag = true;

            while (true)
            {
                Console.WriteLine("Введите название СУБД:");
                string name = Console.ReadLine();

                if (CheckData(name, ControllerDataBase.collDBMS))
                {
                    if (name == ControllerDataBase.collDBMS[0])
                    {
                        DB = new Oracle();
                        ctrlDB = new ControllerDataBase(DB);
                    }
                    else if (name == ControllerDataBase.collDBMS[1])
                    {
                        DB = new MySQL();
                        ctrlDB = new ControllerDataBase(DB);
                    }
                    else if (name == ControllerDataBase.collDBMS[2])
                    {
                        DB = new PostgreSQL();
                        ctrlDB = new ControllerDataBase(DB);
                    }

                    Console.Clear();
                    Console.WriteLine("0. Выйти");
                    ShowCommands();
                    byte command = 1;

                    while (command != 0)
                    {
                        Console.WriteLine("\nВыберите команду:");
                        try
                        {
                            command = Convert.ToByte(Console.ReadLine());
                        }
                        catch
                        {
                            command = 0;
                        }

                        if (command >= 0 && command <= ControllerDataBase.collCommands.Count)
                        {
                            switch (command)
                            {
                                case 0:
                                    Console.Clear();
                                    break;
                                case 1:
                                    ctrlDB.createDB();
                                    break;
                                case 2:
                                    ctrlDB.createSession();
                                    break;
                                case 3:
                                    ctrlDB.createTable();
                                    break;
                                case 4:
                                    ctrlDB.modifyTable();
                                    break;
                                case 5:
                                    ctrlDB.dropTable();
                                    break;
                                case 6:
                                    ctrlDB.truncateTable();
                                    break;
                                case 7:
                                    ctrlDB.addColumn();
                                    break;
                                case 8:
                                    ctrlDB.deleteColumn();
                                    break;
                                case 9:
                                    ctrlDB.grantOption();
                                    break;
                                case 10:
                                    ctrlDB.removeOption();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Команда выбрана неверно!");
                        }
                    }
                }
                else
                {
                    Console.Write("Введите СУБД из списка:");
                    foreach (string val in ControllerDataBase.collDBMS)
                    {
                        Console.Write(" " + val);
                    }
                    Console.WriteLine("\n");
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        ///     Проверяет, есть ли данная СУБД в коллекции
        /// </summary>
        /// <param name="name"> Название СУБД </param>
        /// <param name="array"> Коллекция СУБД </param>
        /// <returns> Возвращает true, если такая СУБД в коллекции есть </returns>
        /// 
        static public bool CheckData(string name, ReadOnlyCollection<string> array)
        {
            bool flag = false;
            foreach(string val in array)
            {
                if (val == name) flag = true;
            }
            return flag;
        }

        /// <summary>
        ///     Выводит в консоль список общих команд для СУБД
        /// </summary>
        /// 
        static public void ShowCommands()
        {
            int i = 1;
            foreach (string val in ControllerDataBase.collCommands)
            {
                Console.WriteLine(i + ". " + val);
                i++;
            }
        }
    }

    /// <summary>
    ///     Абстрактный класс СУБД
    /// </summary>
    /// 
    public abstract class InterfaceDataBase
    {
        public string NameDBMS;

        protected InterfaceDataBase(string name) {
            NameDBMS = name;
        }

        public abstract void connect();

        // Other own methods...
    }

    /// <summary>
    ///     конкретный класс Oracle
    /// </summary>
    /// 
    public class Oracle : InterfaceDataBase
    {
        private const string name = "Oracle";

        public Oracle() : base(name) {}

        public override void connect()
        {
            // do connection...
        }

        // Other own methods...
    }

    /// <summary>
    ///     конкретный класс MySQL
    /// </summary>
    /// 
    public class MySQL : InterfaceDataBase
    {
        private const string name = "MySQL";

        public MySQL() : base(name) {}

        public override void connect()
        {
            // do connection...
        }

        // Other own methods...
    }

    /// <summary>
    ///     конкретный класс PostgreSQL
    /// </summary>
    /// 
    public class PostgreSQL : InterfaceDataBase
    {
        private const string name = "PostgreSQL";

        public PostgreSQL() : base(name) {}

        public override void connect()
        {
            // do connection...
        }

        // Other own methods...
    }

    /// <summary>
    ///     класс-контроллер, позволяющий работать с экземплярами конкретных СУБД
    /// </summary>
    /// 
    public class ControllerDataBase
    {
        private static string[] arrDBMS = new string[] {
            "Oracle",
            "MySQL",
            "PostgreSQL"
        };
        public static ReadOnlyCollection<string> collDBMS = new ReadOnlyCollection<string>(arrDBMS);

        private static string[] arrCommands = new string[] {
            "Создание БД",
            "Создание сессии",
            "Создание таблицы",
            "Изменение таблицы",
            "Удаление таблицы",
            "Очищение таблицы",
            "Добавление атрибута",
            "Удаление атрибута",
            "Дача привилегии",
            "Удаление привилегии"
        };
        public static ReadOnlyCollection<string> collCommands = new ReadOnlyCollection<string>(arrCommands);

        private InterfaceDataBase db;

        public ControllerDataBase() {}

        public ControllerDataBase(InterfaceDataBase db)
        {
            this.db = db;
        }

        public void createDB()
        {
            db.connect();
            Console.WriteLine("База данных " + db.NameDBMS + " создана.");
        }

        public void createSession()
        {
            db.connect();
            Console.WriteLine("Сессия " + db.NameDBMS + " создана.");
        }

        public void createTable()
        {
            db.connect();
            Console.WriteLine("Таблица " + db.NameDBMS + " создана.");
        }

        public void modifyTable()
        {
            db.connect();
            Console.WriteLine("Таблица " + db.NameDBMS + " изменена.");
        }

        public void dropTable()
        {
            db.connect();
            Console.WriteLine("Таблица " + db.NameDBMS + " удалена.");
        }

        public void truncateTable()
        {
            db.connect();
            Console.WriteLine("Таблица " + db.NameDBMS + " очищена.");
        }

        public void addColumn()
        {
            db.connect();
            Console.WriteLine("Атрибут " + db.NameDBMS + " добавлен.");
        }

        public void deleteColumn()
        {
            db.connect();
            Console.WriteLine("Атрибут " + db.NameDBMS + " удалён.");
        }

        public void grantOption()
        {
            db.connect();
            Console.WriteLine("Привилегия " + db.NameDBMS + " дана.");
        }

        public void removeOption()
        {
            db.connect();
            Console.WriteLine("Привилегия " + db.NameDBMS + " удалена.");
        }
    }
}
