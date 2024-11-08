using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLBinh
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string MaNhanVien { get; set; }
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }

        // Constructor để khởi tạo đối tượng HoaDon
        public HoaDon(string maHoaDon, string maNhanVien, string maKhachHang, string tenKhachHang)
        {
            MaHoaDon = maHoaDon;
            MaNhanVien = maNhanVien;
            MaKhachHang = maKhachHang;
            TenKhachHang = tenKhachHang;
        }

        public Boolean checkNull()
        {
            if (MaHoaDon != null /*|| MaNhanVien != null || MaKhachHang != null || TenKhachHang != null */)
            {
                return false;
            } else { return true; }
        }

        // Phương thức hiển thị thông tin hóa đơn (nếu cần)
        public void Display()
        {
            Console.WriteLine($"Mã Hóa Đơn: {MaHoaDon}");
            Console.WriteLine($"Mã Nhân Viên: {MaNhanVien}");
            Console.WriteLine($"Mã Khách Hàng: {MaKhachHang}");
            Console.WriteLine($"Tên Khách Hàng: {TenKhachHang}");
        }
    }

}
