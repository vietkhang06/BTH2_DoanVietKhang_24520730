using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2
{
    public class Bai1
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Bai 1: In lich thang ===");

            int month = GetValidMonth();
            int year = GetValidYear();

            PrintCalendar(month, year);
        }

        static int GetValidMonth()
        {
            int month;
            while (true)
            {
                Console.Write("Nhap thang (1-12): ");
                string input = Console.ReadLine()?.Trim();

                if (double.TryParse(input, out double temp))
                {
                    if (temp % 1 == 0)
                        month = (int)temp;
                    else
                    {
                        Console.WriteLine("Loi: Thang phai la so nguyen.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Loi: Thang phai la so nguyen.");
                    continue;
                }

                if (month < 1 || month > 12)
                {
                    Console.WriteLine("Loi: Thang phai nam trong khoang tu 1 den 12.");
                    continue;
                }
                break;
            }
            return month;
        }

        static int GetValidYear()
        {
            int year;
            while (true)
            {
                Console.Write("Nhap nam (> 0): ");
                string input = Console.ReadLine()?.Trim();

                if (double.TryParse(input, out double temp))
                {
                    if (temp % 1 == 0)
                        year = (int)temp;
                    else
                    {
                        Console.WriteLine("Loi: Nam phai la so nguyen.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Loi: Nam phai la so nguyen.");
                    continue;
                }

                if (year < 1)
                {
                    Console.WriteLine("Loi: Nam phai lon hon 0");
                    continue;
                }
                break;
            }
            return year;
        }

        static void PrintCalendar(int month, int year)
        {
            DateTime firstDay = new DateTime(year, month, 1);
            int startDay = (int)firstDay.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            Console.WriteLine("\nMonth: " + month.ToString("D2") + "/" + year + "\n");
            Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat");

            for (int i = 0; i < startDay; i++)
                Console.Write("    ");

            for (int d = 1; d <= daysInMonth; d++)
            {
                Console.Write($"{d,3} ");
                startDay++;
                if (startDay % 7 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
}
