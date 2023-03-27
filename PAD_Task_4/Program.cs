using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAD_Task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int playerHealth = random.Next(100, 601);
            int bossHealth = random.Next(800, 1201);
            int bossDamage = random.Next(50, 300);
            bool shadowSpiritSummoned = false;
            bool isPlayerTurn = random.NextDouble() > 0.5;

            Console.WriteLine("Теневой маг против Босса");
            Console.WriteLine($"У вас {playerHealth} хп, у Босса {bossHealth} хп");

            while (playerHealth > 0 && bossHealth > 0)
            {
                Console.WriteLine();
                Console.WriteLine(isPlayerTurn ? "Ваш ход" : "Ход босса");

                if (isPlayerTurn)
                {
                    Console.WriteLine("Выберите заклинание:");
                    Console.WriteLine("1. Рашамон призывает теневого духа для нанесения атаки (Отнимает 100 хп игроку) ");
                    Console.WriteLine("2. Хуганзакура наносит 100 ед. урона");
                    Console.WriteLine("3. Межпространственный разлом позволяет скрыться в разломе и восстановить 250 хп. Урон босса по вам не проходит ");

                    int spellChoice = int.Parse(Console.ReadLine());

                    switch (spellChoice)
                    {
                        case 1:
                            playerHealth -= 100;
                            shadowSpiritSummoned = true;
                            Console.WriteLine("Вы призвали теневого духа. Ваше здоровье уменьшилось на 100.");
                            break;
                        case 2:
                            if (shadowSpiritSummoned)
                            {
                                bossHealth -= 100;
                                Console.WriteLine("Теневой дух нанес Боссу 100 урона.");
                            }
                            else
                            {
                                Console.WriteLine("Теневой дух не призван. Заклинание не сработало.");
                            }
                            break;
                        case 3:
                            playerHealth += 250;
                            Console.WriteLine("Вы скрылись в межпространственном разломе и восстановили 250 хп.");
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор заклинания. Вы пропускаете ход.");
                            break;
                    }
                }
                else
                {
                    playerHealth -= bossDamage;
                    Console.WriteLine($"Босс наносит вам {bossDamage} урона.");
                    
                }
                Console.WriteLine();
                isPlayerTurn = !isPlayerTurn;
                Console.WriteLine($"У вас {playerHealth} хп, у Босса {bossHealth} хп");
            }

            if (playerHealth <= 0)
            {
                Console.WriteLine("Вы проиграли. Босс победил.");
            }
            else
            {
                Console.WriteLine("Поздравляем! Вы победили Босса.");
            }
            Console.ReadKey();
        }
    }
}
