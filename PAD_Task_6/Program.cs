using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAD_Task_6
{
    class Program
    {
        static string[] names = new string[0];
        static string[] positions = new string[0];

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить досье");
                Console.WriteLine("2. Вывести все досье");
                Console.WriteLine("3. Удалить досье");
                Console.WriteLine("4. Поиск по фамилии");
                Console.WriteLine("5. Выход");

                Console.Write("Введите номер пункта меню: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddDossier();
                        break;
                    case "2":
                        PrintDossiers();
                        break;
                    case "3":
                        DeleteDossier();
                        break;
                    case "4":
                        SearchByLastName();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неправильный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine();
              
            }
        }

        static void AddDossier()
        {
            Console.Clear();
            Console.Write("Введите ФИО: ");
            string name = Console.ReadLine();

            Console.Write("Введите должность: ");
            string position = Console.ReadLine();

            Array.Resize(ref names, names.Length + 1);
            Array.Resize(ref positions, positions.Length + 1);
            names[names.Length - 1] = name;
            positions[positions.Length - 1] = position;

            Console.WriteLine("Досье добавлено.");
        }

        static void PrintDossiers()
        {
            if (names.Length == 0)
            {
                Console.WriteLine("Досье не найдено.");
                return;
            }

            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} - {positions[i]}");
            }
        }

        static void DeleteDossier()
        {
            Console.Clear();
            Console.Write("Введите номер досье для удаления: ");
            string input = Console.ReadLine();
            int index;

            if (!int.TryParse(input, out index) || index < 1 || index > names.Length)
            {
                Console.WriteLine("Неправильный номер досье.");
                return;
            }

            index--;

            for (int i = index + 1; i < names.Length; i++)
            {
                names[i - 1] = names[i];
                positions[i - 1] = positions[i];
            }

            Array.Resize(ref names, names.Length - 1);
            Array.Resize(ref positions, positions.Length - 1);

            Console.WriteLine("Досье удалено.");
        }

        static void SearchByLastName()
        {
            Console.Clear();
            Console.Write("Введите фамилию для поиска: ");
            string lastName = Console.ReadLine();
            bool found = false;

            for (int i = 0; i < names.Length; i++)
            {
                string[] nameParts = names[i].Split(' ');

                if (nameParts[0].EndsWith(lastName))
                {
                    Console.WriteLine($"{i + 1}. {nameParts[0]} {nameParts[1]} {nameParts[2]} - {positions[i]}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Досье не найдено.");
            }
        }

    }
}
