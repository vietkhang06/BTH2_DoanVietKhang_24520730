using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("===== MENU CHINH =====");
                Console.WriteLine("1. Chay Bai 1");
                Console.WriteLine("2. Chay Bai 2");
                Console.WriteLine("3. Chay Bai 3");
                Console.WriteLine("4. Chay Bai 4");
                Console.WriteLine("5. Chay Bai 5");
                Console.WriteLine("0. Thoat");
                Console.WriteLine("======================");
                Console.Write("Nhap lua chon: ");

                choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Bai1.Run();
                        break;
                    case 2:
                        Bai2.Run();
                        break;
                    case 3:
                        Bai3.Run();
                        break;
                    case 4:
                        Bai4.Run();
                        break;
                    case 5:
                        BaiKhuDat.Run();
                        break;
                    case 0:
                        Console.WriteLine("Tam biet!");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nNhan phim bat ky de quay lai menu...");
                    Console.ReadKey();
                }

            } while (choice != 0);
        }
    }
}