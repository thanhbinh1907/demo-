using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BTLBinh
{
    public partial class Form9 : Form
    {
        private string currentPassword = "123"; // Mật khẩu mặc định
        public Form9()
        {
            InitializeComponent();
            txtOldPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            // Kiểm tra mật khẩu cũ
            if (oldPassword != Password.CurrentPassword)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mật khẩu mới và xác nhận
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu mới và xác nhận!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật mật khẩu
            Password.CurrentPassword = newPassword; // Cập nhật mật khẩu mới
            File.WriteAllText("password.txt", newPassword); // Lưu mật khẩu mới vào tệp
            MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form thay đổi mật khẩu
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái của checkbox
            if (cbShowPassword.Checked)
            {
                // Nếu checkbox được tích, hiển thị mật khẩu
                txtOldPassword.UseSystemPasswordChar = false;
                txtNewPassword.UseSystemPasswordChar = false;
                txtConfirm.UseSystemPasswordChar = false;
            }
            else
            {
                // Nếu checkbox không được tích, ẩn mật khẩu
                txtOldPassword.UseSystemPasswordChar = true;
                txtNewPassword.UseSystemPasswordChar = true;
                txtConfirm.UseSystemPasswordChar = true;
            }
        }
    }
}
