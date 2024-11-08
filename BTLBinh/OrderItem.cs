using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLBinh
{
    public class OrderItem
    {
        public string Name { get; set; }          // Tên món
        public int SoLuong { get; set; }          // Số lượng
        public decimal ThanhTien { get; set; }      // Giá tiền
        public decimal KhuyenMai { get; set; } = 0; // Khuyến mãi (mặc định là 0)
    }

}
