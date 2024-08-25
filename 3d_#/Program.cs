using System;
using System.Runtime.Serialization.Formatters;

namespace sharp_game
{

    class Program
    {
        private const int ScreeWidth = 100;
        private const int ScreeHeight = 50;

        private const int MapHeight = 32;
        private const int MapWidth = 32;

        private const int Depth = 16;
        private const double Fov = Math.PI / 3;

        private static double _playerX = 5;
        private static double _playerY = 5;
        private static double _playerA = 0;


        private static readonly char[] Screen = new char[ScreeWidth * ScreeHeight];

        private static string _map = "";
        static void Main(string[] args)
        {
            Console.SetWindowSize(ScreeWidth, ScreeHeight);
            Console.SetBufferSize(ScreeWidth, ScreeHeight);
            Console.CursorVisible = false;

            char c = ' ';


            _map += "################################";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#.........######...............#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "################################";
            DateTime dateTimeFrom = DateTime.Now;
            while (true)
            {
                DateTime dateTime = DateTime.Now;
                double elaplsedTime = (dateTime - dateTimeFrom).TotalSeconds;
                dateTimeFrom = DateTime.Now;

                if (Console.KeyAvailable)
                {

                    ConsoleKey consoleKey = Console.ReadKey(true).Key;
                    switch (consoleKey)
                    {
                        case ConsoleKey.W:
                            _playerX += Math.Sin(_playerA) * 100 * elaplsedTime;
                            _playerY += Math.Cos(_playerA) * 100 * elaplsedTime;
                            break;
                        case ConsoleKey.S:
                            _playerX -= Math.Sin(_playerA) * 100 * elaplsedTime;
                            _playerY -= Math.Cos(_playerA) * 100 * elaplsedTime;
                            break;
                        case ConsoleKey.A:
                            _playerA += 5 * elaplsedTime;
                            break;
                        case ConsoleKey.D:
                            _playerA -= 5 * elaplsedTime;
                            break;


                    }
                }
                for (int x = 0; x < ScreeWidth; x++)
                {
                    double rayAngle = _playerA + Fov / 2 - x * Fov / ScreeWidth;

                    double rayX = Math.Sin(rayAngle);
                    double rayY = Math.Cos(rayAngle);

                    double distanceToWall = 0;
                    bool hitWall = false;

                    while (!hitWall && distanceToWall < Depth)
                    {
                        distanceToWall += 0.1;
                        int testX = (int)(_playerX + rayX * distanceToWall);
                        int testY = (int)(_playerY + rayY * distanceToWall);

                        if (testX < 0 || testX > Depth + _playerX || testY < 0 || testY > Depth + _playerY)
                        {
                            hitWall = true;
                            distanceToWall = Depth;
                        }
                        else
                        {
                            char testCell = _map[testY * MapWidth + testX];
                            if (testCell == '#')
                            {
                                hitWall = true;
                            }
                        }
                    }
                    int celling = (int)(ScreeHeight / 2d - ScreeHeight * Fov / distanceToWall);
                    int floor = ScreeHeight - celling;

                    char wallshade;
                    if (distanceToWall <= Depth / 4d)
                    {
                        wallshade = '\u2588';
                    }
                    else if (distanceToWall < Depth / 3d)
                    {
                        wallshade = '\u2593';
                    }
                    else if (distanceToWall < Depth / 2d)
                    {
                        wallshade = '\u2592';
                    }
                    else if (distanceToWall < Depth)
                    {
                        wallshade = '\u2591';
                    }
                    else
                    {
                        wallshade = ' ';
                    }

                    for (int y = 0; y < ScreeHeight; y++)
                    {
                        if (y <= celling)
                        {
                            Screen[y * ScreeWidth + x] = ' ';
                        }
                        else if (y > celling && y <= floor)
                        {
                            Screen[y * ScreeWidth + x] = wallshade;
                        }
                        else
                        {

                            Screen[y * ScreeWidth + x] = '.';
                        }
                    }

                }


                Console.SetCursorPosition(0, 0);
                Console.Write(Screen);
            }
        }
    }

}