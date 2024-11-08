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
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username == "admin" && password == Password.CurrentPassword)
            {
                try
                {
                    Form1 newForm = new Form1();
                    newForm.Show(); // Hiển thị form mới
                    this.Hide(); // Đóng form hiện tại
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
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
    }
}