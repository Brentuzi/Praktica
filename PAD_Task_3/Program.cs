using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAD_Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string password = "pas"; 
            int attempts = 0; 
            string input = "";
            while (input != password)
            {
                Console.Write("Введите пароль: ");
                input = Console.ReadLine();
                if (input != password)
                {
                    attempts++;
                    Console.WriteLine($"Неверный пароль. Осталось {3 - attempts} попыток.");
                    if (attempts >= 3)
                    {
                        Console.WriteLine("Превышено количество попыток. Программа завершается.");
                        return;
                    }
                }
            }
            Console.WriteLine("Пароль верный. Секретное сообщение: код 10");
            Console.ReadKey();
        }
    }
}
