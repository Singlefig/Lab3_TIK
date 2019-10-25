using System;
using System.Linq;

namespace Lab2_TIK
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] sumOfA = new double[8];
            double[] sumOfB = new double[8];
            double[] mass = new double[]
            {
            0.018, 0.023, 0.021, 0.009, 0.016, 0.024, 0.026, 0.003,
            0.004, 0.021, 0.017, 0.013, 0.013, 0.020, 0.021, 0.023,
            0.006, 0.013, 0.006, 0.011, 0.017, 0.018, 0.023, 0.015,
            0.013, 0.018, 0.018, 0.026, 0.015, 0.006, 0.001, 0.008,
            0.014, 0.008, 0.012, 0.024, 0.014, 0.002, 0.017, 0.012,
            0.024, 0.016, 0.017, 0.015, 0.013, 0.014, 0.011, 0.007,
            0.013, 0.025, 0.024, 0.004, 0.017, 0.021, 0.017, 0.025,
            0.026, 0.005, 0.022, 0.025, 0.005, 0.026, 0.015, 0.024
            };
            double[,] matrix = new double[8, 8];
            double entropyOfUnion, entropyH_A, entropyH_B, entropyH_A_B, entropyH_B_A;
            int choise;
            while (true)
            {

                Console.Write(
                    "Меню:\n" +
                    "1 - Сгенерировать матрицу вероятностей\n" +
                    "2 - Энтропия обьединения\n" +
                    "3 - Энтропия Н(А)\n" +
                    "4 - Энтропия Н(В)\n" +
                    "5 - Условная энтропия Н(А/B)\n" +
                    "6 - Условная энтропия H(B/A)\n" +
                    "7 - Выход\n" +
                    "Выберите команду:"
                    );
                choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        {
                            matrix = GenerateMatrix(matrix, mass);
                            Console.WriteLine("-----------------------------------------------");
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    Console.Write($"{matrix[i, j]}|");
                                    if (j == 7)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("-----------------------------------------------");
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            entropyOfUnion = EntropyOfUnion(matrix);
                            Console.WriteLine($"Энтропия объединения = {entropyOfUnion} бит");

                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Матрица верятностей появления первичного алфавита:");
                            entropyH_A = EntropyH_A(matrix);
                            Console.WriteLine($"\nЭнтропия H(A) = { entropyH_A} бит");
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("Матрица верятностей появления вторичного алфавита:");
                            entropyH_B = EntropyH_B(matrix);
                            Console.WriteLine($"\nЭнтропия H(B) = { entropyH_B} бит");
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("Матрица верятностей появления первичного алфавита:");
                            entropyH_A_B = EntropyH_A_B(matrix);
                            Console.WriteLine($"\nЭнтропия H(A/B) = {entropyH_A_B} бит");
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("Матрица верятностей появления вторичного алфавита:");
                            entropyH_B_A = EntropyH_B_A(matrix);
                            Console.WriteLine($"\nЭнтропия H(B/A) = {entropyH_B_A} бит");
                        }
                        break;
                    case 7:
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        break;
                }
            }

            double[,] GenerateMatrix(double[,] newMatrix, double[] newMass)
            {
                newMatrix = new double[8, 8];
                int s = 0;
                Random random = new Random();
                newMass = newMass.OrderBy(x => random.Next()).ToArray();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        newMatrix[i, j] = newMass[s];
                        s++;
                    }
                }
                return newMatrix;
            }

            double EntropyOfUnion(double[,] newMatrix)
            {
                double sumOfEntropy = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        sumOfEntropy += (newMatrix[i, j] * Math.Log(newMatrix[i, j], 2));
                    }
                }
                double newEntropyOfUnion = Math.Round(sumOfEntropy * (-1), 4);
                return newEntropyOfUnion;
            }

            double EntropyH_A(double[,] newMatrix)
            {
                double sum = 0;
                double[] arrayForSumElements = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        arrayForSumElements[i] += newMatrix[i, j];
                    }
                    sum += arrayForSumElements[i];
                }
                Console.WriteLine("-----------------------------------------------");
                for (int i = 0; i < 8; i++)
                {
                    Console.Write($"{Math.Round(arrayForSumElements[i], 3)}|");
                }
                Console.WriteLine("\n-----------------------------------------------");
                sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    sum += (arrayForSumElements[i] * Math.Log(arrayForSumElements[i], 2));
                }
                double H_A = Math.Round(sum * (-1), 4);
                Array.Copy(arrayForSumElements, sumOfA, 8);
                return H_A;
            }

            double EntropyH_B(double[,] newMatrix)
            {
                double sum = 0;
                double[] arrayForSumElements = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        arrayForSumElements[j] += newMatrix[i, j];
                    }
                    sum += arrayForSumElements[i];
                }
                Console.WriteLine("-----------------------------------------------");
                for (int i = 0; i < 8; i++)
                {
                    Console.Write($"{Math.Round(arrayForSumElements[i], 3)}|");
                }
                Console.WriteLine("\n-----------------------------------------------");
                sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    sum += (arrayForSumElements[i] * Math.Log(arrayForSumElements[i], 2));
                }
                double H_B = Math.Round(sum * (-1), 4);
                Array.Copy(arrayForSumElements, sumOfB, 8);
                return H_B;
            }

            double EntropyH_A_B(double[,] newMatrix)
            {
                double[,] additionalMatrix = new double[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        additionalMatrix[i, j] = Math.Round(newMatrix[i, j] / sumOfB[j], 2);
                    }
                }
                double sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        sum += newMatrix[i, j] * Math.Log(additionalMatrix[i, j], 2);
                    }
                }
                double H_A_B = Math.Round(sum * (-1), 4);
                Console.WriteLine("-------------------------------------------------------------");
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Console.Write($"{additionalMatrix[i, j]}|\t");
                        if (j == 7)
                        {
                            Console.WriteLine();
                            Console.WriteLine("-------------------------------------------------------------");
                        }
                    }
                }
                return H_A_B;
            }

            double EntropyH_B_A(double[,] newMatrix)
            {
                double[,] additionalMatrix = new double[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        additionalMatrix[i, j] = Math.Round(newMatrix[i, j] / sumOfA[j], 2);
                    }
                }
                double sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)

                    {
                        sum += newMatrix[i, j] * Math.Log(additionalMatrix[i, j], 2);
                    }
                }
                double H_B_A = Math.Round(sum * (-1), 4);
                Console.WriteLine("-------------------------------------------------------------");
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Console.Write($"{additionalMatrix[i, j]}|\t");
                        if (j == 7)
                        {
                            Console.WriteLine();
                            Console.WriteLine("-------------------------------------------------------------");
                        }
                    }
                }
                return H_B_A;
            }
        }
    }
}
