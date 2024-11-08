using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public partial class Payment : Form
    {
        string tongTien;

        public event EventHandler ButtonClicked;
        public Payment(string tongTien)
        {
            InitializeComponent();
            this.tongTien = tongTien;


            // Chuyển đổi tongTien sang decimal
            if (decimal.TryParse(tongTien, out decimal amount))
            {
                // Tạo URL mới với amount là giá trị decimal
                string url = $"https://img.vietqr.io/image/970422-0975777504-compact2.jpg?amount={amount}&addInfo=thanh%20toan&accountName=Ngo%20Quang%20Huy";

                // Tải ảnh từ URL
                LoadImageFromUrl(url);
            }
            else
            {
                MessageBox.Show("Giá trị tổng tiền không hợp lệ.");
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void LoadImageFromUrl(string url)
        {
            try
            {
                // Tạo WebClient để tải ảnh từ URL
                WebClient webClient = new WebClient();

                // Tải dữ liệu ảnh dưới dạng byte từ URL
                byte[] imageBytes = webClient.DownloadData(url);

                // Chuyển đổi dữ liệu byte thành hình ảnh
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu sự kiện đã được đăng ký
            ButtonClicked?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

    }
}
