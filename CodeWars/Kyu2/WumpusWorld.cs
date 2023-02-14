using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace CodeWars.Kyu2
{
    public static class WumpusWorld
    {
        public static int[,] battlefield = { { 0,-1,-1,-1},
            {-1,-1,-1,-1},
            {-1,-1,-1,-1},
            {-1,-1,-1,-1}};

        public static int[,] cMap = battlefield;// делаем копию карты локации, для дальнейшей ее разметки

        public static bool Solve(char[][] cave)
        {
            //Располагаем все объекты на поле
            SetObjectsPosition(FindObjects(cave, 'W'), -1);//Размещаем вумпуса
            Point gold = FindObjects(cave, 'G');
            SetObjectsPosition(gold, -1);//Размещаем золото
            for (int i = 0; i < cave.Where(c => c.Contains('P')).Count(); i++)
                SetObjectsPosition(FindObjects(cave, 'P'), -2);// Размещаем ямы

            return FindWay(gold);
        }
 
        // ищмем объекты
        static Point FindObjects(char[][] cave, char parameter)
        {
            for (int i = 0; i < cave.Length; i++)
                for (int j = 0; j < cave[i].Length; j++)
                    if (cave[i][j] == parameter)
                        return new Point(i, j);

            return new Point(0, 0);
        }

        static void SetObjectsPosition(Point objectPosition, int value)
        {
            battlefield[objectPosition.X,objectPosition.Y] = value; // Запишите в карту локации, что клетка занята
        }

        //Ищмем путь к золоту
        //TargetX, TargetY - координаты золота
        public static bool FindWay(Point gold)
        {
            bool add = true; // условие выхода из цикла
            int step = 0; // значение шага равно 0

            while (add == true)
            {
                add = false;
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (cMap[x,y] == step)
                        {
                            // если соседняя клетка не стена, и если она еще не помечена
                            // то помечаем ее значением шага + 1
                            if (y - 1 >= 0 && y - 1 < 4 && cMap[x,y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (x - 1 >= 0 && x - 1 < 4 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (y + 1 >= 0 && y + 1 < 4 && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                            if (x + 1 >= 0 && x + 1 < 4 && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                        }
                    }
                }
                step++;
                add = true;

                if (cMap[gold.X,gold.Y] >= 0)//решение найдено
                {
                    add = false;
                    return true;
                } 
                    
                if (step > battlefield.Length)//решение не найдено, если шагов больше чем клеток
                {
                    add = false;
                    return false;
                }
            }

            return false;
        }

        //Если нужно востановить путь
        public static bool Move(Point gold)
        {
            Point currentPosition = gold; // Сохраняем текущую позицию

            Point moveTo = new(0,0);
            bool add = true; // условие выхода из цикла
            int value = cMap[gold.X, gold.Y];

            while (add == true)
            {
                add = false;
                for (int x = currentPosition.X; x >= 0;)
                {
                    for (int y = currentPosition.Y; y >= 0;)
                    {
                        if (y - 1 >= 0 && y - 1 < 4 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == value - 1)
                        {
                            currentPosition = new(x, y - 1);
                            break;
                        }

                        if (x - 1 >= 0 && x - 1 < 4 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == value - 1)
                        {
                            currentPosition = new(x - 1, y);
                            break;
                        }

                        if (y + 1 >= 0 && y + 1 < 4 && cMap[x, y + 1] != -2 && cMap[x, y + 1] == value - 1)
                        {
                            currentPosition = new(x, y + 1);
                            break;
                        }

                        if (x + 1 >= 0 && x + 1 < 4 && cMap[x + 1, y] != -2 && cMap[x + 1, y] == value - 1)
                        {
                            currentPosition = new(x + 1, y);
                            break;
                        }
                        break;
                    }
                    break;
                }
                value--;
                add = true;

                if (currentPosition == moveTo) //решение найдено
                    add = false;
                if (value > battlefield.Length) //решение не найдено, если шагов больше чем клеток
                    add = false;
            }

            return true;
        }
    }
}
