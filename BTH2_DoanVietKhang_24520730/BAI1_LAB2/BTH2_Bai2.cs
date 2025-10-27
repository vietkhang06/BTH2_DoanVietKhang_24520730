using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2
{
    public class Bai2
    {
        public static void Run()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Bai 2: Liet ke thu muc (tuong tu lenh DIR) ===");

            string path = GetValidPath();
            PrintDirectoryContent(path);
        }
        static string GetValidPath()
        {
            while (true)
            {
                Console.Write("Nhap duong dan thu muc: ");
                string? path = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(path))
                {
                    Console.WriteLine("Loi: Duong dan khong duoc de trong.");
                    continue;
                }

                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Loi: Thu muc khong ton tai. Hay nhap lai.");
                    continue;
                }

                return path;
            }
        }

        static void PrintDirectoryContent(string path)
        {
            Console.WriteLine("\n Directory of " + path + "\n");

            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            long totalFileSize = 0;

            foreach (string dir in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                Console.WriteLine($"{di.LastWriteTime:MM/dd/yyyy  hh:mm tt}    <DIR>          {di.Name}");
            }

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                totalFileSize += fi.Length;
                Console.WriteLine($"{fi.LastWriteTime:MM/dd/yyyy  hh:mm tt}    {fi.Length,15:N0}  {fi.Name}");
            }

            Console.WriteLine();
            Console.WriteLine($"{files.Length,4} File(s) {totalFileSize,15:N0} bytes");
            Console.WriteLine($"{dirs.Length,4} Dir(s) {GetFreeSpace(path),15:N0} bytes free\n");

            static long GetFreeSpace(string path)
            {
                DriveInfo drive = new DriveInfo(Path.GetPathRoot(path));
                return drive.AvailableFreeSpace;
            }
        }
    }
}
