using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public static class MenuStripHelper
    {
        // Phương thức để thêm hiệu ứng phóng to khi di chuột vào các mục MenuStrip
        public static void ApplyHoverEffect(ToolStripMenuItem menuItem)
        {
            menuItem.MouseEnter += MenuItem_MouseEnter;
            menuItem.MouseLeave += MenuItem_MouseLeave;
        }

        // Sự kiện khi chuột di vào mục
        private static void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Font = new Font("Algerian", 16, FontStyle.Bold); // Tăng kích thước font
        }

        // Sự kiện khi chuột rời khỏi mục
        private static void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Font = new Font("Algerian", 14, FontStyle.Regular); // Kích thước font ban đầu
        }
    }
}
