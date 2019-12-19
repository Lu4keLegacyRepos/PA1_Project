using System;
using System.Threading.Tasks;

namespace LUDecomposition
{
    public class MaxtrixLUD
    {
        private int matDim;
        private double[,] lower;
        private double[,] upper;
        double[,] input;

        public void ComputeDecomposition(double[,] mat, int squareDimensionOfMat)
        {
            input = mat;
            matDim = squareDimensionOfMat;
            lower = new double[matDim, matDim];
            upper = new double[matDim, matDim];

            Parallel.For(0, matDim, j =>
            {
                for (int i = 0; i < matDim; i++)
                {
                    if (j > i)
                    {
                        upper[i, j] = input[i, j];
                        for (int k = 0; k < i; k++)
                        {
                            upper[i, j] -= lower[i, k] * upper[k, j];
                        }
                        if (i == j)
                            lower[i, j] = 1;
                        else
                            lower[i, j] = 0;
                    }

                    else
                    {
                        lower[i, j] = input[i, j];
                        for (int k = 0; k < j; k++)
                        {
                            lower[i, j] -= lower[i, k] * upper[k, j];
                        }
                        lower[i, j] /= upper[j, j];
                        upper[i, j] = 0;
                    }
                }
            });
        }


        public void ComputeDecompositionSeq(double[,] mat, int squareDimensionOfMat)
        {
            input = mat;
            matDim = squareDimensionOfMat;
            lower = new double[matDim, matDim];
            upper = new double[matDim, matDim];

            for (int j = 0; j < matDim; j++)
            {
                for (int i = 0; i < matDim; i++)
                {
                    if (i <= j)
                    {
                        upper[i, j] = input[i, j];
                        for (int k = 0; k < i; k++)
                        {
                            upper[i, j] -= lower[i, k] * upper[k, j];
                        }
                        if (i == j)
                            lower[i, j] = 1;
                        else
                            lower[i, j] = 0;
                    }

                    else
                    {
                        lower[i, j] = input[i, j];
                        for (int k = 0; k < j; k++)
                        {
                            lower[i, j] -= lower[i, k] * upper[k, j];
                        }
                        lower[i, j] /= upper[j, j];
                        upper[i, j] = 0;
                    }
                }
            }
        }
        private void PrintMat(double[,] matrix,string name)
        {
            string spaces = "    ";
            Console.WriteLine(name);
            for (int i = 0; i < matDim; i++)
            {
                for (int j = 0; j < matDim; j++)
                {
                    Console.Write(spaces + (matrix[i, j] < 0 ? matrix[i, j].ToString() : $" {matrix[i, j]}") + "\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n\n");
        }
        public void PrintLU()
        {
            PrintMat(input,"Input");
            PrintMat(lower,"Lower");
            PrintMat(upper,"Upper");
        }

    }

}

