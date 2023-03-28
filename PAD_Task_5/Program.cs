using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace PAD_Task_5
{
    class Program
    {
        private static char[,] map;
        private static int playerX, playerY;
        private static int[] enemyPositions;
        private static Random random = new Random();
        private static int playerHealth = 100;

        private static void LoadMap(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            int height = lines.Length;
            int width = lines.Max(l => l.Length);

            map = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = lines[i].Length > j ? lines[i][j] : ' ';
                }
            }
        }

        private static void DrawMap()
        {
            Console.Clear();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static bool CanMove(int x, int y)
        {
            return map[x, y] != '#';
        }

        private static bool CheckCollisionWithEnemy(int x, int y)
        {
            for (int i = 0; i < enemyPositions.Length; i += 2)
            {
                if (enemyPositions[i] == x && enemyPositions[i + 1] == y)
                {
                    return true;
                }
            }
            return false;
        }

        private static void MovePlayer(int dx, int dy)
        {
            int newX = playerX + dx;
            int newY = playerY + dy;
            if (CanMove(newX, newY))
            {
                if (CheckCollisionWithEnemy(newX, newY))
                {
                    playerHealth -= 10;
                }
                else
                {
                    map[playerX, playerY] = ' ';
                    playerX = newX;
                    playerY = newY;
                }
                map[playerX, playerY] = '@';
            }
        }

        private static void MoveEnemy()
        {
            for (int i = 0; i < enemyPositions.Length; i += 2)
            {
                int dx = random.Next(-1, 2);
                int dy = random.Next(-1, 2);
                int newX = enemyPositions[i] + dx;
                int newY = enemyPositions[i + 1] + dy;

                if (CanMove(newX, newY))
                {
                    map[enemyPositions[i], enemyPositions[i + 1]] = ' ';
                    enemyPositions[i] = newX;
                    enemyPositions[i + 1] = newY;
                    map[newX, newY] = 'E';
                }
            }
        }

        private static void InitializeGame(string fileName)
        {
            LoadMap(fileName);
            playerX = Array.FindIndex(map.Cast<char>().ToArray(), c => c == '@') / map.GetLength(1);
            playerY = Array.FindIndex(map.Cast<char>().ToArray(), c => c == '@') % map.GetLength(1);

            enemyPositions = new int[2 * map.Cast<char>().Count(c => c == 'E')];
            int enemyIndex = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'E')
                    {
                        enemyPositions[enemyIndex] = i;
                        enemyPositions[enemyIndex + 1] = j;
                        enemyIndex += 2;
                    }
                }
            }
        }
        private static void DrawPlayerStatus()
        {
            Console.SetCursorPosition(0, map.GetLength(0));
            Console.WriteLine($"Health: {playerHealth}%");
            Console.WriteLine("управление W S D A P-показать путь");
        }

        private static void ShowPath()
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };
            int width = map.GetLength(1);
            int height = map.GetLength(0);
            int[,] dist = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    dist[i, j] = -1;
                }
            }

            dist[playerX, playerY] = 0;
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(playerX, playerY));

            while (queue.Count > 0)
            {
                Tuple<int, int> current = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && ny >= 0 && nx < height && ny < width && dist[nx, ny] == -1 && map[nx, ny] != '#')
                    {
                        dist[nx, ny] = dist[x, y] + 1;
                        queue.Enqueue(new Tuple<int, int>(nx, ny));
                    }
                }
            }

            int targetX = Array.FindIndex(map.Cast<char>().ToArray(), c => c == 'X') / map.GetLength(1);
            int targetY = Array.FindIndex(map.Cast<char>().ToArray(), c => c == 'X') % map.GetLength(1);


            while (dist[targetX, targetY] > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    int nx = targetX + dx[i];
                    int ny = targetY + dy[i];

                    if (nx >= 0 && ny >= 0 && nx < height && ny < width && dist[nx, ny] == dist[targetX, targetY] - 1)
                    {
                        map[targetX, targetY] = '.';
                        targetX = nx;
                        targetY = ny;
                        break;
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            InitializeGame("map.txt");
           
            while (true)
            {
                DrawMap();
                DrawPlayerStatus();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                int dx = 0, dy = 0;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                        dy = -1;
                        break;
                    case ConsoleKey.W:
                        dx = -1;
                        break;
                    case ConsoleKey.D:
                        dy = 1;
                        break;
                    case ConsoleKey.S:
                        dx = 1;
                        break;
                    case ConsoleKey.P:
                        ShowPath();
                        break;
                }

                MovePlayer(dx, dy);
                MoveEnemy();

                if (playerHealth <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over");
                    break;
                }

                Thread.Sleep(50); // Задержка
            }
        }
    }



}

