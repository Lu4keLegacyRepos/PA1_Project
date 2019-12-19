using System;
using System.Diagnostics;
using System.Threading;

namespace PA1_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Game of life");
            Thread.Sleep(1000);
            var game = new GameOfLife.GameOfLife(20, GameOfLife.Pattern.Pulsar, 30, 50)
            //var game = new GameOfLife.GameOfLife(5000, GameOfLife.Pattern.Pulsar, 1000, 0)
                .Start();


            Console.Clear();
            Console.WriteLine("LU decomposition");
            Thread.Sleep(1000);
            var lu = new LUDecomposition.MaxtrixLUD();
            double[,] mat2print = {
                { 2, -1, -2 },
                { -4, 6, 3 },
                { -4, -2, 8 } };
            lu.ComputeDecomposition(mat2print, 3);
            lu.PrintLU();


            Random r = new Random();
            int n = 2000;
            Console.WriteLine($"\n\n Compare Seq & Parallel for random matrix {n}x{n}");
            double[,] mat = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mat[i, j] = r.Next(0, 100);
                }
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            lu.ComputeDecomposition(mat, n);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms  - Parallel");

            sw.Reset();
            sw.Start();
            lu.ComputeDecomposition(mat, n);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms  - Seq");

            Console.ReadKey();

        }
    }
}
