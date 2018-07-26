using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul2
{
    class Program
    {
        static void Main(string[] args)
        {
            MainScreen mainScreen = new MainScreen(15, 15);

            mainScreen.Run();
        }
    }


    public struct MainScreen
    {
        Random rnd;
        Point[] point;

        int FocusX;
        int FocusY;
        int Line;
        int Column;

        string[,] ScreenArray;

        public MainScreen(int line, int column)
        {
            rnd = new Random();
            point = new Point[column];
            Line = line;
            Column = column;

            FocusX = line - 1;
            FocusY = 0;

            ScreenArray = new string[Line, Column];

            Fill();
            AddPoint();
        }

        private void Fill()
        {
            for (int i = 0; i < ScreenArray.GetLength(0); i++)
            {
                for (int j = 0; j < ScreenArray.GetLength(1); j++)
                {
                    ScreenArray[i, j] = "  ";
                }
            }
        }

        private void ShowMainScreen()
        {
            ScreenArray[FocusX, FocusY] = $"██";

            for (int i = 0; i < Column; i++)
            {
                if (point[i].Flag == true)
                {
                    ScreenArray[point[i].RandomX, point[i].RandomY] = $"██";
                }
            }

            Console.Clear();

            for (int i = 0; i < ScreenArray.GetLength(1) + 2; i++)
            {
                Console.Write("██");
            }

            Console.WriteLine();

            for (int i = 0; i < ScreenArray.GetLength(0); i++)
            {
                for (int j = 0; j < ScreenArray.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write("██");
                    }

                    if (i == FocusX & j == FocusY)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{ScreenArray[i, j]}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{ScreenArray[i, j]}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (j == ScreenArray.GetLength(1) - 1)
                    {
                        Console.Write("██");
                    }
                }

                Console.WriteLine();
            }

            for (int i = 0; i < ScreenArray.GetLength(1) + 2; i++)
            {
                Console.Write("██");
            }
        }

        public void Run()
        {
            ConsoleKeyInfo pressKey;

            bool flag = true;


            ShowMainScreen();

            do
            {

                pressKey = Console.ReadKey();

                switch (pressKey.Key)
                {
                    case ConsoleKey.LeftArrow: PressKeyLeft(); break;
                    case ConsoleKey.RightArrow: PressKeyRight(); break;
                    case ConsoleKey.Escape: flag = false; break;
                    default: break;
                }
                flag = ContactPoint();

            }
            while (flag);
        }

        private void PressKeyLeft()
        {
            DownStep();

            ScreenArray[FocusX, FocusY] = "  ";

            if (FocusY != 0)
            {
                FocusY--;
            }
            else
            {
                FocusY = ScreenArray.GetLength(1) - 1;
            }


            ShowMainScreen();
        }

        private void PressKeyRight()
        {
            DownStep();

            ScreenArray[FocusX, FocusY] = "  ";

            if (FocusY != ScreenArray.GetLength(1) - 1)
            {
                FocusY++;
            }
            else
            {
                FocusY = 0;
            }

            ShowMainScreen();
        }

        private void RandomPoint(int a)
        {
            point[a].RandomX = 0;
            point[a].RandomY = rnd.Next(0, Column);
        }

        private void DownStep()
        {
            for (int i = 0; i < Column; i++)
            {
                if (point[i].Flag == true)
                {
                    ScreenArray[point[i].RandomX, point[i].RandomY] = "  ";

                    if (point[i].RandomX == ScreenArray.GetLength(1) - 1)
                    {

                        point[i].Flag = false;
                    }
                    else
                    {
                        point[i].RandomX++;
                    }
                }
            }
            AddPoint();
        }

        private bool ContactPoint()
        {
            for (int i = 0; i < Column; i++)
            {
                if (point[i].Flag == true & FocusX == point[i].RandomX & FocusY == point[i].RandomY)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Game Over!!!");

                    Console.ReadKey();

                    return false;
                }
            }
            return true;
        }

        private void AddPoint()
        {
            for (int i = 0; i < Column; i++)
            {
                if (point[i].Flag == false)
                {
                    point[i].Flag = true;
                    RandomPoint(i);
                    break;
                }

            }
        }
    }

    public struct Point
    {
        public int RandomX;
        public int RandomY;
        public bool Flag;

    }

}
