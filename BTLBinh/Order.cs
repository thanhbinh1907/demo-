using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            // Dùng cho MenuStrip 
            MenuStripHelper.ApplyHoverEffect(danhMucToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhapHangToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(banHangToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhanVienToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhaCungCapToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(tienIchToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(troGiupToolStripMenuItem);
        }

        private void danhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void nhapHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện mới của Form2`1
            Form2 form2 = new Form2();

            // Hiển thị Form2
            form2.Show();

            // Nếu bạn muốn đóng Form1 sau khi mở Form2, hãy thêm dòng sau:
            this.Hide();
        }

        private void banHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Show();

            this.Hide();
        }

        private void nhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();

            form5.Show();
        }

        private void nhaCungCapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            form4.Show();
        }
    }
}
