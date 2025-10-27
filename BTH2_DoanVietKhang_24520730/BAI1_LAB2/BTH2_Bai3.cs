using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BTH2
{
    public class Bai3
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Bai 3: Xu ly ma tran hai chieu ===\n");

            int[,] matrix = InputMatrix(out int rows, out int cols);
            if (rows == 0 && cols == 0)
            {
                Console.WriteLine("Ma tran rong. Ket thuc chuong trinh.");
                return;
            }

            Console.WriteLine("\nMa tran vua nhap:");
            OutputMatrix(matrix, rows, cols);

            while (true)
            {
                Console.WriteLine("\n=== MENU ===");
                Console.WriteLine("1. Tim kiem mot phan tu");
                Console.WriteLine("2. Xuat cac so nguyen to");
                Console.WriteLine("3. Cho biet dong co nhieu so nguyen to nhat");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                string? choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        SearchMenu(matrix, rows, cols);
                        break;
                    case "2":
                        PrintPrime(matrix, rows, cols);
                        break;
                    case "3":
                        RowWithMostPrimes(matrix, rows, cols);
                        break;
                    case "0":
                        Console.WriteLine("Ket thuc chuong trinh.");
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le. Nhap lai.");
                        break;
                }
            }
        }
        static bool TryParseIntAllowDotZero(string? s, out int value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Trim();

            // try plain int first
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
                return true;
            if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                return true;
            if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out double d) ||
                double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out d))
            {
                if (double.IsNaN(d) || double.IsInfinity(d)) return false;

                double rounded = Math.Round(d);
                if (Math.Abs(d - rounded) < 1e-9)
                {
                    if (rounded >= int.MinValue && rounded <= int.MaxValue)
                    {
                        value = (int)rounded;
                        return true;
                    }
                }
            }

            return false;
        }

        static int[,] InputMatrix(out int rows, out int cols)
        {
            while (true)
            {
                Console.Write("Nhap so hang va so cot (vd: 3 4,...): ");
                string[] parts = Console.ReadLine()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

                if (parts.Length != 2)
                {
                    Console.WriteLine("Loi: Ban phai nhap dung 2 so!");
                    continue;
                }

                if (!TryParseIntAllowDotZero(parts[0], out rows) || !TryParseIntAllowDotZero(parts[1], out cols) || rows < 0 || cols < 0)
                {
                    Console.WriteLine("Loi: Kich thuoc khong hop le. Nhap lai.");
                    continue;
                }

                if (rows == 0 && cols == 0)
                    return new int[0, 0];

                int[,] matrix = new int[rows, cols];
                Console.WriteLine("Nhap " + rows * cols + " phan tu cua ma tran:");

                for (int i = 0; i < rows; i++)
                {
                    while (true)
                    {
                        Console.Write($"Dong {i + 1}: ");
                        string[] rowValues = Console.ReadLine()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                        if (rowValues.Length != cols)
                        {
                            Console.WriteLine("Loi: Can nhap dung " + cols + " gia tri.");
                            continue;
                        }
                        bool ok = true;
                        for (int j = 0; j < cols; j++)
                        {
                            if (!TryParseIntAllowDotZero(rowValues[j], out matrix[i, j]))
                            {
                                Console.WriteLine("Loi: Vui long nhap so nguyen (hoac so thuc dang x.0).");
                                ok = false;
                                break;
                            }
                        }
                        if (ok) break;
                    }
                }
                return matrix;
            }
        }

        static void OutputMatrix(int[,] matrix, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write($"{matrix[i, j],5}");
                Console.WriteLine();
            }
        }

        static void SearchMenu(int[,] matrix, int rows, int cols)
        {
            Console.Write("\nNhap gia tri can tim: ");
            string? input = Console.ReadLine();
            if (!TryParseIntAllowDotZero(input, out int x))
            {
                Console.WriteLine("Gia tri khong hop le. Vui long nhap so nguyen (hoac so thuc dang x.0).");
                return;
            }
            Search(matrix, rows, cols, x);
        }

        static void Search(int[,] matrix, int rows, int cols, int x)
        {
            bool found = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == x)
                    {
                        Console.WriteLine("Tim thay " + x + " tai hang " + (i + 1) + " , cot " + (j + 1));
                        found = true;
                    }
                }
            }
            if (!found)
                Console.WriteLine("Khong tim thay " + x + " trong ma tran.");
        }

        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        static void PrintPrime(int[,] matrix, int rows, int cols)
        {
            bool found = false;
            Console.WriteLine("\nCac so nguyen to trong ma tran:");
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (IsPrime(matrix[i, j]))
                    {
                        Console.Write(matrix[i, j] + " ");
                        found = true;
                    }
            if (!found)
                Console.WriteLine("Khong co so nguyen to nao.");
            Console.WriteLine();
        }

        static void RowWithMostPrimes(int[,] matrix, int rows, int cols)
        {
            int maxRow = -1;
            int maxCount = 0;

            for (int i = 0; i < rows; i++)
            {
                int count = 0;
                for (int j = 0; j < cols; j++)
                    if (IsPrime(matrix[i, j]))
                        count++;

                if (count > maxCount)
                {
                    maxCount = count;
                    maxRow = i;
                }
            }

            if (maxCount == 0)
                Console.WriteLine("Khong co dong nao chua so nguyen to.");
            else
                Console.WriteLine("Dong " + (maxRow + 1) + " co nhieu so nguyen to nhat ( " + maxCount + " so).");
        }
    }
}
