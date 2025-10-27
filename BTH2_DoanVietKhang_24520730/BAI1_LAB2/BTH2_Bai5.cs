using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BTH2
{
    public class BaiKhuDat
    {
        public static void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("=== Bai 5: Quan ly KhuDat - NhaPho - ChungCu ===");
            QuanLyBatDongSan ql = new QuanLyBatDongSan();
            Console.WriteLine();
            Console.WriteLine("1. Nhap danh sach");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tong gia ban theo loai");
            Console.WriteLine("4. Xuat khu dat >100m2 va nha pho (dt>60 va nam>=2019)");
            Console.WriteLine("5. Tim kiem NhaPho va ChungCu");
            Console.WriteLine("0. Thoat");
            while (true)
            {
                
                Console.Write("Chon chuc nang: ");
                string ch = Console.ReadLine().Trim();
                if (ch == "1")
                {
                    ql.NhapDanhSach();
                }
                else if (ch == "2")
                {
                    ql.XuatDanhSach();
                }
                else if (ch == "3")
                {
                    ql.TongGiaBan();
                }
                else if (ch == "4")
                {
                    ql.XuatKhuDatVaNhaPhoDacBiet();
                }
                else if (ch == "5")
                {
                    ql.TimKiem();
                }
                else if (ch == "0")
                {
                    Console.WriteLine("Ket thuc chuong trinh.");
                    break;
                }
                else
                {
                    Console.WriteLine("Lua chon khong hop le.");
                }
            }
        }
        // ham kiem tra so nguyen hop le
        public static int GetValidInt(string str, int min, int max)
        {
            int v;
            while (true)
            {
                Console.Write(str);
                string s = Console.ReadLine().Trim();
                if (!int.TryParse(s, out v))
                {
                    Console.WriteLine("Loi: vui long nhap so nguyen.");
                    continue;
                }
                if (v < min || v > max)
                {
                    Console.WriteLine("Loi: gia tri phai nam trong khoang " + min + " - " + max + ".");
                    continue;
                }
                return v;
            }
        }
        public static double GetValidDouble(string str, double min)
        {
            double v;
            while (true)
            {
                Console.Write(str);
                string s = Console.ReadLine().Trim();
                s = s.Replace(',', '.');
                if (!double.TryParse(s, out v))
                {
                    Console.WriteLine("Lỗi: vui lòng nhập số hợp lệ.");
                    continue;
                }

                if (v <= min)
                {
                    Console.WriteLine("Lỗi: giá trị phải lớn hơn " + min + ".");
                    continue;
                }

                return v;
            }
        }
    }

    public class KhuDat
    {
        public string DiaDiem { get; set; }
        public double GiaBan { get; set; }
        public double DienTich { get; set; }
        public KhuDat() { }
        public KhuDat(string dd, double gia, double dt)
        {
            DiaDiem = dd;
            GiaBan = gia;
            DienTich = dt;
        }
        public virtual void Nhap()
        {
            Console.Write("Nhap dia diem: ");
            DiaDiem = Console.ReadLine().Trim();

            GiaBan = BaiKhuDat.GetValidDouble("Nhap gia ban (VND): ", 0);
            DienTich = BaiKhuDat.GetValidDouble("Nhap dien tich (m2): ", 0);
        }
        public virtual void Xuat()
        {
            Console.WriteLine("Loai: Khu Dat");
            Console.WriteLine("Dia diem: " + DiaDiem);
            Console.WriteLine("Gia ban: " + GiaBan.ToString("N0") + " VND");
            Console.WriteLine("Dien tich: " + DienTich.ToString("N2") + " m2");
        }
    }

    public class NhaPho : KhuDat
    {
        public int NamXayDung { get; set; }
        public int SoTang { get; set; }
        public NhaPho() : base() { }
        public NhaPho(string dd, double gia, double dt, int nam, int tang) : base(dd, gia, dt)
        {
            NamXayDung = nam;
            SoTang = tang;
        }
        public override void Nhap()
        {
            base.Nhap();
            NamXayDung = BaiKhuDat.GetValidInt("Nhap nam xay dung: ", 1800, DateTime.Now.Year);
            SoTang = BaiKhuDat.GetValidInt("Nhap so tang: ", 1, 100);
        }
        public override void Xuat()
        {
            Console.WriteLine("Loai: Nha Pho");
            Console.WriteLine("Dia diem: " + DiaDiem);
            Console.WriteLine("Gia ban: " + GiaBan.ToString("N0") + " VND");
            Console.WriteLine("Dien tich: " + DienTich.ToString("N2") + " m2");
            Console.WriteLine("Nam xay dung: " + NamXayDung);
            Console.WriteLine("So tang: " + SoTang);
        }
    }

    public class ChungCu : KhuDat
    {
        public int Tang { get; set; }
        public ChungCu() : base() { }
        public ChungCu(string dd, double gia, double dt, int tang) : base(dd, gia, dt)
        {
            Tang = tang;
        }
        public override void Nhap()
        {
            base.Nhap();
            Tang = BaiKhuDat.GetValidInt("Nhap tang: ", 0, 500);
        }
        public override void Xuat()
        {
            Console.WriteLine("Loai: Chung Cu");
            Console.WriteLine("Dia diem: " + DiaDiem);
            Console.WriteLine("Gia ban: " + GiaBan.ToString("N0") + " VND");
            Console.WriteLine("Dien tich: " + DienTich.ToString("N2") + " m2");
            Console.WriteLine("Tang: " + Tang);
        }
    }

    public class QuanLyBatDongSan
    {
        private List<KhuDat> ds = new List<KhuDat>();
        public void NhapDanhSach()
        {
            int n = BaiKhuDat.GetValidInt("Nhap so luong bat dong san: ", 1, 10000);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("--- Nhap thong tin thu " + (i + 1) + " ---");
                int loai = BaiKhuDat.GetValidInt("Chon loai (1 - KhuDat, 2 - NhaPho, 3 - ChungCu): ", 1, 3);
                KhuDat item;
                if (loai == 1) item = new KhuDat();
                else if (loai == 2) item = new NhaPho();
                else item = new ChungCu();
                item.Nhap();
                ds.Add(item);
            }
            Console.WriteLine("Nhap danh sach xong.");
        }
        public void XuatDanhSach()
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sach rong.");
                return;
            }
            Console.WriteLine("=== DANH SACH BAT DONG SAN ===");
            int idx = 1;
            foreach (KhuDat k in ds)
            {
                Console.WriteLine("STT: " + idx);
                k.Xuat();
                idx++;
            }
        }
        public void TongGiaBan()
        {
            double tongKhuDat = 0;
            double tongNhaPho = 0;
            double tongChungCu = 0;

            foreach (KhuDat k in ds)
            {
                if (k is NhaPho) tongNhaPho += k.GiaBan;
                else if (k is ChungCu) tongChungCu += k.GiaBan;
                else tongKhuDat += k.GiaBan;
            }
            Console.WriteLine("Tong gia ban - KhuDat: " + tongKhuDat.ToString("N0") + " VND");
            Console.WriteLine("Tong gia ban - NhaPho: " + tongNhaPho.ToString("N0") + " VND");
            Console.WriteLine("Tong gia ban - ChungCu: " + tongChungCu.ToString("N0") + " VND");
        }
        public void XuatKhuDatVaNhaPhoDacBiet()
        {
            Console.WriteLine("=== Khu dat >100m2 hoac Nha pho (dt>60 & nam>=2019) ===");
            bool found = false;
            foreach (KhuDat k in ds)
            {
                if (k is NhaPho np)
                {
                    if (np.DienTich > 60 && np.NamXayDung >= 2019)
                    {
                        np.Xuat();
                        Console.WriteLine();
                        found = true;
                    }
                }
                else if (!(k is ChungCu) && k.DienTich > 100)
                {
                    k.Xuat();
                    Console.WriteLine();
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Khong co ban ghi phu hop.");
        }

        public void TimKiem()
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sach rong.");
                return;
            }
            Console.WriteLine("=== Tim kiem NhaPho va ChungCu ===");
            Console.Write("Nhap chuoi dia diem can tim: ");
            string key = Console.ReadLine().Trim().ToLower();
            Console.Write("Nhap gia toi da (VND): ");
            double gia;
            while (!double.TryParse(Console.ReadLine().Trim(), out gia) || gia < 0)
            {
                Console.WriteLine("Gia phai la so lon hon hoac bang 0. Nhap lai.");
                Console.Write("Nhap gia toi da (VND): ");
            }

            Console.Write("Nhap dien tich toi thieu (m2): ");
            double dt;
            while (!double.TryParse(Console.ReadLine().Trim(), out dt) || dt < 0)
            {
                Console.WriteLine("Dien tich phai la so >= 0. Nhap lai.");
                Console.Write("Nhap dien tich toi thieu (m2): ");
            }

            bool found = false;
            int n = 1;
            foreach (KhuDat k in ds)
            {
                if ((k is NhaPho || k is ChungCu) && k.DiaDiem.ToLower().Contains(key) && k.GiaBan <= gia && k.DienTich >= dt)
                {
                    Console.WriteLine("Ket qua " + n + ":");
                    k.Xuat();
                    n++;
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Khong tim thay phan tu nao phu hop.");
        }
    }
}