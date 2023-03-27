using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAD_Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            while (input != "exit")
            {
                Console.WriteLine("Введите слово exit для выхода : ");
                input = Console.ReadLine();
            }
            Console.WriteLine("Работа программы завершена.");
            Console.ReadKey();
        }
    }
}
