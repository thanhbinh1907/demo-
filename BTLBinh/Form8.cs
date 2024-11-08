using System;
using System.Windows.Forms;
using System.IO;

namespace BTLBinh
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            // Đảm bảo mật khẩu được ẩn ngay từ đầu
            txtPassword.UseSystemPasswordChar = true;
            if (File.Exists("password.txt"))
            {
                Password.CurrentPassword = File.ReadAllText("password.txt");
            }
            else
            {
                Password.CurrentPassword = "123"; // Mật khẩu mặc định nếu tệp không tồn tại
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Mở form thay đổi mật khẩu
            Form9 changePasswordForm = new Form9();
            changePasswordForm.ShowDialog(); // Mở dưới dạng hộp thoại
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim(); // Lấy tên đăng nhập và loại bỏ khoảng trắng
            string password = txtPassword.Text.Trim(); // Lấy mật khẩu và loại bỏ khoảng trắng

            // Kiểm tra nếu một trong hai ô trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Hiển thị thông báo yêu cầu nhập đầy đủ
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Kết thúc phương thức nếu có ô trống
            }

            // Kiểm tra tài khoản và mật khẩu
            if (username == "admin" && password == Password.CurrentPassword) // Sử dụng mật khẩu từ lớp UserCredentials
            {
                // Chuyển sang form khác
                Form1 newForm = new Form1();
                newForm.Show();
                this.Hide();
            }
            else
            {
                // Hiển thị thông báo lỗi bằng MessageBox
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbHienMK_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái của checkbox
            if (cbHienMK.Checked)
            {
                // Nếu checkbox được tích, hiển thị mật khẩu
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                // Nếu checkbox không được tích, ẩn mật khẩu
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}