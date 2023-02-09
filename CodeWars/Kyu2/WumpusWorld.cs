using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu2
{
    public static class WumpusWorld
    {
        public static int[,] battlefield;

        public static int x, y;

        public static bool ready = false;

        private static Vector3 currentPosition;

        private static Vector3 lastPosition;


        public static bool Solve(char[][] cave)
        {



            return false;
        }

        static void Start()
        {
            currentPosition = new Vector3(1, 1, 10); // сохраняем текущую позицию
            lastPosition = currentPosition; // сохраняем последную позицию юнита.

            battlefield = new int[,]{ // матрица нашей локации. 1 - стена. 0 - свободная клетка
			{1,1,1,1,1,1},
            {1,0,0,0,0,1},
            {1,0,0,0,0,1},
            {1,0,0,0,0,1},
            {1,0,0,0,0,1},
            {1,1,1,1,1,1}};

            SetWumpusPosition(enemyNumber); // размещаем вумпуса на поле
            SetPitPosition(playerNumber); // размещаем ямы
            ready = true; // можем начинать искать путь


        }

        static void SetWumpusPosition(int numberEnemies)
        {

            battlefield[14, 2] = 1; // обязательно запишите в карту локации, что клетка, на которой находится воин, занята.
        }

        static void SetPitPosition(int numberPlayerWarior)
        {
            battlefield[2, 6] = 1;
        }

        //public static void printMatrix()
        //{
        //    string arr = "";
        //    for (int i = 0; i < x; i++)
        //    {
        //        for (int j = 0; j < y; j++)
        //        {
        //            arr += battlefield[i, j] + ",";
        //        }
        //        arr += "\n";
        //    }
        //    print(arr);
        //}

        /// <summary>РЕАЛИЗАЦИЯ ВОЛНОВОГО АЛГОРИТМА
        ///	</summary>
        /// <param name="cMap">Копия карты локации</param>
        /// <param name="targetX">координата цели x</param>
        /// <param name="targetY">координата цели y</param>
        private static IEnumerator move(int[,] cMap, int targetX, int targetY)
        {
            ready = false;
            int[] neighbors = new int[8]; //значение весов соседних клеток
                                          // будем хранить в векторе координаты клетки в которую нужно переместиться
            Vector3 moveTO = new Vector3(-1, 0, 10);

            // да да да, можно было сделать через цикл for
            neighbors[0] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y + 1];
            neighbors[1] = cMap[(int)currentPosition.x, (int)currentPosition.y + 1];
            neighbors[2] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y + 1];
            neighbors[3] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y];
            neighbors[4] = cMap[(int)currentPosition.x - 1, (int)currentPosition.y - 1];
            neighbors[5] = cMap[(int)currentPosition.x, (int)currentPosition.y - 1];
            neighbors[6] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y - 1];
            neighbors[7] = cMap[(int)currentPosition.x + 1, (int)currentPosition.y];
            for (int i = 0; i < 8; i++)
            {
                if (neighbors[i] == -2)
                    // если клетка является непроходимой, делаем так, чтобы на нее юнит точно не попал
                    // делаем этот костыль для того, чтобы потом было удобно брать первый элемент в
                    // отсортированом по возрастанию массиве
                    neighbors[i] = 99999;
            }
            Array.Sort(neighbors); //первый элемент массива будет вес клетки куда нужно двигаться

            //ищем координаты клетки с минимальным весом. 
            for (int x = (int)currentPosition.x - 1; x <= (int)currentPosition.x + 1; x++)
            {
                for (int y = (int)currentPosition.y + 1; y >= (int)currentPosition.y - 1; y--)
                {
                    if (cMap[x, y] == neighbors[0])
                    {
                        // и указываем вектору координаты клетки, в которую переместим нашего юнита
                        moveTO = new Vector3(x, y, 10);
                    }
                }
            }
            //если мы не нашли куда перемещать юнита, то оставляем его на старой позиции.
            // это случается, если вокруг юнита, во всех 8 клетках, уже размещены другие юниты
            if (moveTO == new Vector3(-1, 0, 10))
                moveTO = new Vector3(currentPosition.x, currentPosition.y, 10);

            //и ура, наконец-то мы перемещаем нашего юнита
            // теперь он на 1 клетку ближе к врагу
            transform.localPosition = moveTO;

            //устанавливаем задержку.
            yield return new WaitForSeconds(waitMove);
            ready = true;
        }

        //Ищмем путь к врагу
        //TargetX, TargetY - координаты ближайшего врага
        public static int[,] findWave(int targetX, int targetY)
        {
            bool add = true; // условие выхода из цикла
                             // делаем копию карты локации, для дальнейшей ее разметки
            int[,] cMap = new int[battlefield.x, battlefield.y];
            int x, y, step = 0; // значение шага равно 0
            for (x = 0; x < Battlefield.x; x++)
            {
                for (y = 0; y < Battlefield.y; y++)
                {
                    if (Battlefield.battlefield[x, y] == 1)
                        cMap[x, y] = -2; //если ячейка равна 1, то это стена (пишим -2)
                    else cMap[x, y] = -1; //иначе еще не ступали сюда
                }
            }

            //начинаем отсчет с финиша, так будет удобней востанавливать путь
            cMap[targetX, targetY] = 0;
            while (add == true)
            {
                add = false;
                for (x = 0; x < Battlefield.x; x++)
                {
                    for (y = 0; y < Battlefield.y; y++)
                    {
                        if (cMap[x, y] == step)
                        {
                            // если соседняя клетка не стена, и если она еще не помечена
                            // то помечаем ее значением шага + 1
                            if (y - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (x - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;
                            if (y + 1 >= 0 && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                            if (x + 1 >= 0 && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                        }
                    }
                }
                step++;
                add = true;
                if (cMap[(int)transform.localPosition.x, (int)transform.localPosition.y] > 0) //решение найдено
                    add = false;
                if (step > Battlefield.X * Battlefield.Y) //решение не найдено, если шагов больше чем клеток
                    add = false;
            }
            return cMap; // возвращаем помеченную матрицу, для востановления пути в методе move()
        }

        // если в сосденей клетки есть враг, то останавливаемся
        public bool stopMove(int targetX, int targetY)
        {
            bool move = false;
            for (int x = (int)currentPosition.x - 1; x <= (int)currentPosition.x + 1; x++)
            {
                for (int y = (int)currentPosition.y + 1; y >= (int)currentPosition.y - 1; y--)
                {
                    if (x == targetX && y == targetY)
                    {
                        move = true;
                    }
                }
            }
            return move;
        }

        // ищмем ближайшего врага
        static Point findClosestEnemy()
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                float curDistance = Vector3.Distance(go.transform.position, position);
                if (curDistance < distance)
                {
                    closestEnemy = go;
                    distance = curDistance;
                }
            }
            return closestEnemy;
        }


    }
}
