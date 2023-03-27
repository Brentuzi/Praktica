using System;

using System.IO;
namespace PAD_Task_5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Загрузка карты из файла
            string[] lines = File.ReadAllLines("map.txt");
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] maze = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    maze[i, j] = lines[i][j];
                }
            }

            // Нахождение начальной и конечной точек
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (maze[i, j] == '@')
                    {
                        startX = i;
                        startY = j;
                    }
                    else if (maze[i, j] == 'X')
                    {
                        endX = i;
                        endY = j;
                    }
                }
            }

            // Начальное положение игрока
            int playerX = startX;
            int playerY = startY;

            // Основной игровой цикл
            while (true)
            {
                Console.Clear();
                DrawMaze(maze);
                Console.WriteLine("Use arrow keys to move, P to show path, Q to quit");
                Console.WriteLine();

                // Отображение состояния игрока
                int health = 100;
                Console.Write("Health: ");
                DrawBar(health);

                // Обработка ввода пользователя
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow && playerX > 0 && maze[playerX - 1, playerY] != '#')
                {
                    playerX--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && playerX < rows - 1 && maze[playerX + 1, playerY] != '#')
                {
                    playerX++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && playerY > 0 && maze[playerX, playerY - 1] != '#')
                {
                    playerY--;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && playerY < cols - 1 && maze[playerX, playerY + 1] != '#')
                {
                    playerY++;
                }
                else if (keyInfo.Key == ConsoleKey.P)
                {
                    ShowPath(maze, playerX, playerY, endX, endY);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Выход...");
                    break;
                }

                // Обновление карты
                maze[startX, startY] = ' ';
                maze[playerX, playerY] = '@';

                // Проверка на победу
                if (playerX == endX && playerY == endY)
                {
                    Console.WriteLine("Вы достигли финиша! Поздравляем!");
                    break;
                }
            }
        }

        static void DrawMaze(char[,] maze)
        {
            // Рисование карты
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void DrawBar(int percent)
        {
            Console.Write("[");
            int bars = percent / 10;
            for (int i = 0; i < 10; i++)
            {
                if (i < bars)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write("_");
                }
            }
            Console.Write("]");
        }

        static char[,] FindPath(char[,] maze, int startX, int startY, int endX, int endY)
        {
            // Копирование карты, чтобы не изменять исходную карту
            char[,] copy = (char[,])maze.Clone();

            // Настройка начальной и конечной точек
            copy[startX, startY] = ' ';
            copy[endX, endY] = 'X';

            // Нахождение пути с помощью алгоритма "левая рука"
            int x = startX;
            int y = startY + 1;
            int dx = 0;
            int dy = 1;
            while (x != endX || y != endY)
            {
                if (copy[x + dx, y + dy] == ' ')
                {
                    x += dx;
                    y += dy;
                }
                else if (copy[x - dy, y + dx] == ' ')
                {
                    int temp = dx;
                    dx = -dy;
                    dy = temp;
                    x -= dy;
                    y += dx;
                }
                else
                {
                    int temp = dx;
                    dx = dy;
                    dy = -temp;
                    x += dy;
                    y -= dx;
                }
                copy[x, y] = '+';
            }

            return copy;
        }

        static void ShowPath(char[,] maze, int startX, int startY, int endX, int endY)
        {
            char[,] path = FindPath(maze, startX, startY, endX, endY);
            DrawMaze(path);
        }
    }


}

