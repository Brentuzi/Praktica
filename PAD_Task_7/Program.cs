using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAD_Task_7
{
    class Program
    {
        static void Main(string[] args)
        {

            int n = 10;
            Console.Write ("Введите размер массива- ");
            n = Convert.ToInt32( Console.ReadLine());
            int[] arr = new int[n];

            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                arr[i] = rnd.Next(1, 101); 
            }

            Console.WriteLine("Первоначальный массив:");
            Console.WriteLine(string.Join(", ", arr));

            Shuffle(arr);

            Console.WriteLine("Перемешанный массив:");
            Console.WriteLine(string.Join(", ", arr));

            Console.ReadKey();
        }

        static void Shuffle(int[] arr)
        {
            Random rng = new Random();

            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rng.Next(i + 1);
                int temp = arr[j];
                arr[j] = arr[i];
                arr[i] = temp;
            }
        }
    }
}
