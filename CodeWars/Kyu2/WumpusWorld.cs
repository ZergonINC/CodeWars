using System;
using System.Collections;
using System.Drawing;
using System.Numerics;

namespace CodeWars.Kyu2
{
    public static class WumpusWorld
    {
        public static int[][] battlefield = { new int[]{ 0,-1,-1,-1},
            new int[]{-1,-1,-1,-1},
            new int[]{-1,-1,-1,-1},
            new int[]{-1,-1,-1,-1}};

        private static Point currentPosition;

        private static Point lastPosition;


        public static bool Solve(char[][] cave)
        {
            currentPosition = new Point(1, 1); // Сохраняем текущую позицию
            lastPosition = currentPosition; // Сохраняем последную позицию

            //Располагаем все объекты на поле
            SetObjectsPosition(FindObjects(cave, 'W'), -1);//Размещаем вупуса
            Point gold = FindObjects(cave, 'G');
            SetObjectsPosition(gold, 0);//Размещаем золото
            for (int i = 0; i < cave.Where(c => c.Contains('P')).Count(); i++)
                SetObjectsPosition(FindObjects(cave, 'P'), -2);// Размещаем ямы

            int[][] cMap = FindWay(gold);


            return false;
        }
 
        // ищмем объекты
        static Point FindObjects(char[][] cave, char parameter)
        {
            for (int i = 0; i < cave.Length; i++)
            {
                for (int j = 0; j < cave[i].Length; j++)
                {
                    if (cave[i][j] == parameter)
                        return new Point(i, j);
                }
            }

            return new Point(0, 0);
        }

        static void SetObjectsPosition(Point objectPosition, int value)
        {
            battlefield[objectPosition.X][objectPosition.Y] = value; // Запишите в карту локации, что клетка занята
        }

        //Ищмем путь к золоту
        //TargetX, TargetY - координаты золота
        public static int[][] FindWay(Point gold)
        {
            bool add = true; // условие выхода из цикла
            // делаем копию карты локации, для дальнейшей ее разметки
            int[][] cMap = battlefield;
            int step = 0; // значение шага равно 0

            int xLength = battlefield.Length;
            int yLength = battlefield[0].Length;

            while (add == true)
            {
                add = false;
                for (int x = 0; x < xLength; x++)
                {
                    for (int y = 0; y < yLength; y++)
                    {
                        if (cMap[x][y] == step)
                        {
                            // если соседняя клетка не стена, и если она еще не помечена
                            // то помечаем ее значением шага + 1
                            if (y - 1 >= 0 && y - 1 < yLength && cMap[x][y - 1] != -2 && cMap[x][y - 1] == -1)
                                cMap[x][y - 1] = step + 1;
                            if (x - 1 >= 0 && x - 1 < xLength && cMap[x - 1][y] != -2 && cMap[x - 1][y] == -1)
                                cMap[x - 1][y] = step + 1;
                            if (y + 1 >= 0 && y + 1 < yLength && cMap[x][y + 1] != -2 && cMap[x][y + 1] == -1)
                                cMap[x][y + 1] = step + 1;
                            if (x + 1 >= 0 && x + 1 < xLength && cMap[x + 1][y] != -2 && cMap[x + 1][y] == -1)
                                cMap[x + 1][y] = step + 1;
                        }
                    }
                }
                step++;
                add = true;
                if (cMap[gold.X][gold.Y] > 0) //решение найдено
                    add = false;
                //if (step > battlefield.Length * battlefield[0].Length) //решение не найдено, если шагов больше чем клеток
                //    add = false;
            }
            return cMap; // возвращаем помеченную матрицу, для востановления пути в методе move()
        }
    }
}
