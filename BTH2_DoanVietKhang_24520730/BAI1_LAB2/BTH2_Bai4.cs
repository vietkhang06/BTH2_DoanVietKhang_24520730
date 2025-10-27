using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2
{
    public class PhanSo
    {
        private int tu;
        private int mau;

        public PhanSo()
        {
            tu = 0;
            mau = 1;
        }

        public PhanSo(double tu, double mau)
        {
            if (mau == 0) throw new DivideByZeroException("Mau so khong duoc bang 0.");

            var (t1, m1) = ConvertFromDouble(tu);
            var (t2, m2) = ConvertFromDouble(mau);

            this.tu = t1 * m2;
            this.mau = m1 * t2;

            RutGon();
        }

        public static PhanSo Nhap()
        {
            while (true)
            {
                try
                {
                    string s = Console.ReadLine().Trim();
                    string[] parts = s.Split('/');
                    if (parts.Length != 2)
                    {
                        Console.Write("Nhap sai dinh dang, hay nhap lai (dang tu/mau): ");
                        continue;
                    }

                    double tu = double.Parse(parts[0]);
                    double mau = double.Parse(parts[1]);
                    return new PhanSo(tu, mau);
                }
                catch
                {
                    Console.Write("Nhap sai, hay nhap lai (dang tu/mau): ");
                }
            }
        }

        public static (int tu, int mau) ConvertFromDouble(double x)
        {
            string s = x.ToString().Replace(',', '.');
            if (!s.Contains(".")) return ((int)x, 1);
            int soSauDauPhay = s.Length - s.IndexOf('.') - 1;
            int mau = (int)Math.Pow(10, soSauDauPhay);
            int tu = (int)Math.Round(x * mau);
            return (tu, mau);
        }

        public void RutGon()
        {
            int ucln = UCLN(Math.Abs(tu), Math.Abs(mau));
            tu /= ucln;
            mau /= ucln;
            if (mau < 0)
            {
                tu = -tu;
                mau = -mau;
            }
        }

        private static int UCLN(int a, int b)
        {
            while (b != 0)
            {
                int r = a % b;
                a = b;
                b = r;
            }
            return a;
        }

        public PhanSo Cong(PhanSo p)
        {
            return new PhanSo((double)(tu * p.mau + p.tu * mau), (double)(mau * p.mau));
        }

        public PhanSo Tru(PhanSo p)
        {
            return new PhanSo((double)(tu * p.mau - p.tu * mau), (double)(mau * p.mau));
        }

        public PhanSo Nhan(PhanSo p)
        {
            return new PhanSo((double)(tu * p.tu), (double)(mau * p.mau));
        }

        public PhanSo Chia(PhanSo p)
        {
            if (p.tu == 0)
                throw new DivideByZeroException("Khong the chia cho phan so co gia tri bang 0.");
            return new PhanSo((double)(tu * p.mau), (double)(mau * p.tu));
        }

        public double GiaTri()
        {
            return (double)tu / mau;
        }

        public override string ToString()
        {
            if (mau == 1)
                return tu.ToString();
            return tu.ToString() + "/" + mau.ToString();
        }
    }

    public class Bai4
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Bai 4: Phan So ===");

            Console.Write("Nhap phan so 1 (tu/mau): ");
            PhanSo p1 = PhanSo.Nhap();
            Console.Write("Nhap phan so 2 (tu/mau): ");
            PhanSo p2 = PhanSo.Nhap();

            Console.WriteLine("Phan so 1: " + p1);
            Console.WriteLine("Phan so 2: " + p2);
            Console.WriteLine("Cong: " + p1.Cong(p2));
            Console.WriteLine("Tru: " + p1.Tru(p2));
            Console.WriteLine("Nhan: " + p1.Nhan(p2));

            try
            {
                Console.WriteLine("Chia: " + p1.Chia(p2));
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }

            int n;
            while (true)
            {
                Console.Write("Nhap so luong phan so trong day: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out n) && n > 0)
                    break;
                else
                    Console.Write("Nhap sai, vui long nhap so nguyen duong.");
            }

            List<PhanSo> ds = new List<PhanSo>();

            for (int i = 0; i < n; i++)
            {
                Console.Write("Nhap phan so thu " + (i + 1) + " (tu/mau): ");
                ds.Add(PhanSo.Nhap());
            }

            PhanSo max = ds[0];
            foreach (PhanSo p in ds)
            {
                if (p.GiaTri() > max.GiaTri()) max = p;
            }

            Console.WriteLine("Phan so lon nhat: " + max);

            ds.Sort((a, b) => a.GiaTri().CompareTo(b.GiaTri()));
            Console.WriteLine("Day phan so tang dan:");
            foreach (PhanSo p in ds) Console.WriteLine(p);
        }
    }
}

