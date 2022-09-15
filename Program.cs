using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOKLOK
{
    class Matrix {
        public int[,] Array { get; private set; }

        public Matrix(int height, int width) {
            Array = new int[height, width];
        }


        public (int, int) FindMaxUp() {
            (int maxh, int maxw) = (0, 0);

            for (int i = 0; i < Array.GetLength(0) - 1; i++) {
                for (int j = i + 1; j < Array.GetLength(1); j++) {
                    // System.Console.Write(Array[i, j] + " ");
                    if (Array[i, j].CompareTo(Array[maxh, maxw]) == 1) {
                        maxh = i;
                        maxw = j;
                    }
                }
               // System.Console.WriteLine();
            }

            return (maxh, maxw);
        }

        public (int, int) FindMaxDown() {
            (int maxh, int maxw) = (0, 0);

            for (int i = 1; i < Array.GetLength(0); i++) {
                for (int j = 0; j < i; j++) {
                 //   System.Console.Write(Array[i, j] + " ");
                    if (Array[i, j].CompareTo(Array[maxh, maxw]) == 1) {
                        maxh = i;
                        maxw = j;
                    }
                }
                 //System.Console.WriteLine();
            }

            return (maxh, maxw);
        }


        public void GenerateArray() {
            var rnd = new Random();

            for (int i = 0; i < Array.GetLength(0); i++) {
                for (int j = 0; j < Array.GetLength(1); j++) {
                    Array[i, j] = rnd.Next() % 100;
                }
            }
        }

        public override string ToString() {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < Array.GetLength(0); i++) {
                for (int j = 0; j < Array.GetLength(1); j++) {
                    sb.Append(Array[i, j] + " ");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }

    class MainClass {
        static void Main(string[] args) {
            Console.WriteLine("Матрица 5 на 5 к вашим услугам :)");

            var array = new Matrix(5, 5);

            array.GenerateArray();

            System.Console.WriteLine($"Матрица до изменения:\n{array.ToString()}");

            (int heightLeft, int widthLeft) = array.FindMaxUp();
            System.Console.WriteLine($"Максимальный элемент выше главной диагонали = {array.Array[heightLeft, widthLeft]}");

            (int heightRight, int widthRight) = array.FindMaxDown();
            System.Console.WriteLine($"Максимальный элемент ниже главной диагонали = {array.Array[heightRight, widthRight]}");
            System.Console.WriteLine();

            array.Array[heightLeft, widthLeft] += array.Array[heightRight, widthRight];
            array.Array[heightRight, widthRight] = array.Array[heightLeft, widthLeft] - array.Array[heightRight, widthRight];
            array.Array[heightLeft, widthLeft] -= array.Array[heightRight, widthRight];

            System.Console.WriteLine($"Матрица после изменения:\n{array.ToString()}");
        }
    }
}
