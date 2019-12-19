using System;
using System.Threading;

namespace GameOfLife
{
    public class World
    {
        public bool[,] WorldBoard { get; private set; }
        public int Size { get; private set; }

        public World(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Size > 0");
            }

            Size = size;
            WorldBoard = new bool[size, size];
        }


        public bool this[int x, int y]
        {
            get => WorldBoard[x, y];
            set => WorldBoard[x, y] = value;
        }

        public bool Toggle(int x, int y)
        {
            bool tmpVal = WorldBoard[x, y];
            return WorldBoard[x, y] = !tmpVal;
        }

        public void UpdateWorld(bool[,] bufferedWorld)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this[i, j] = bufferedWorld[i, j];
                }
            }
        }


        public void PrintWorld(int sleepInMS = 100)
        {
            
            Thread.Sleep(sleepInMS);
            Console.Clear();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Console.Write(this[x, y] ? "X " : "- ");
                }

                Console.WriteLine();
            }
        }


        public World SetPattern(Pattern pattern)
        {
            var center = Size / 2;
            var last = Size - 1;
            switch (pattern)
            {
                case Pattern.Blinker://s,r
                    Toggle(center, center);
                    Toggle(center, center + 1);
                    Toggle(center, center + 2);
                    break;
                case Pattern.Glider:
                    Toggle(last, last - 2);
                    Toggle(last - 1, last - 2);
                    Toggle(last - 2, last - 2);
                    Toggle(last - 2, last - 1);
                    Toggle(last - 1, last);

                    break;

                case Pattern.Toad:
                    Toggle(center - 1, center);
                    Toggle(center - 2, center);
                    Toggle(center - 3, center);
                    Toggle(center, center - 1);
                    Toggle(center - 1, center - 1);
                    Toggle(center - 2, center - 1);
                    break;
                case Pattern.Pulsar:
                    Toggle(center - 6, center + 2);
                    Toggle(center - 6, center + 3);
                    Toggle(center - 6, center + 4);
                    Toggle(center - 6, center - 2);
                    Toggle(center - 6, center - 3);
                    Toggle(center - 6, center - 4);

                    Toggle(center + 6, center + 2);
                    Toggle(center + 6, center + 3);
                    Toggle(center + 6, center + 4);
                    Toggle(center + 6, center - 2);
                    Toggle(center + 6, center - 3);
                    Toggle(center + 6, center - 4);

                    Toggle(center + 2, center + 6);
                    Toggle(center + 3, center + 6);
                    Toggle(center + 4, center + 6);
                    Toggle(center + 2, center - 6);
                    Toggle(center + 3, center - 6);
                    Toggle(center + 4, center - 6);

                    Toggle(center + 2, center + 1);
                    Toggle(center + 3, center + 1);
                    Toggle(center + 4, center + 1);
                    Toggle(center - 2, center - 1);
                    Toggle(center - 3, center - 1);
                    Toggle(center - 4, center - 1);

                    Toggle(center - 2, center + 6);
                    Toggle(center - 3, center + 6);
                    Toggle(center - 4, center + 6);
                    Toggle(center - 2, center - 6);
                    Toggle(center - 3, center - 6);
                    Toggle(center - 4, center - 6);

                    Toggle(center + 2, center - 1);
                    Toggle(center + 3, center - 1);
                    Toggle(center + 4, center - 1);
                    Toggle(center - 2, center + 1);
                    Toggle(center - 3, center + 1);
                    Toggle(center - 4, center + 1);

                    Toggle(center - 1, center + 2);
                    Toggle(center - 1, center + 3);
                    Toggle(center - 1, center + 4);
                    Toggle(center - 1, center - 2);
                    Toggle(center - 1, center - 3);
                    Toggle(center - 1, center - 4);

                    Toggle(center + 1, center + 2);
                    Toggle(center + 1, center + 3);
                    Toggle(center + 1, center + 4);
                    Toggle(center + 1, center - 2);
                    Toggle(center + 1, center - 3);
                    Toggle(center + 1, center - 4);


                    break;
            }
            return this;
        }
    }
}
