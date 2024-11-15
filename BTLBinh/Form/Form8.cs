using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace BTLBinh
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();

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
            string username = txtUser.Text.Trim(); // Lấy mã nhân viên
            string password = txtPassword.Text.Trim(); // Lấy mật khẩu

            // Kiểm tra nếu một trong hai ô trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã nhân viên và số điện thoại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Kết thúc phương thức nếu có ô trống
            }

            // Kiểm tra tài khoản admin
            if (username == "admin" && password == Password.CurrentPassword)
            {
                User.SetSession(username, "Admin", "Admin"); // Lưu thông tin admin
                User.UpdateUserLabel(); // Cập nhật label

                Form1 newForm = new Form1();
                newForm.Show();
                this.Hide();
                return; // Kết thúc phương thức nếu đăng nhập admin thành công
            }

            // Kiểm tra tài khoản nhân viên
            DataProcess dataProcess = new DataProcess();
            string query = "SELECT MaNV, TenNV FROM NHANVIEN WHERE MaNV = @MaNV AND SDT = @SDT";

            using (SqlConnection connection = new SqlConnection(dataProcess.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNV", username);
                    command.Parameters.AddWithValue("@SDT", password);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string employeeId = reader["MaNV"].ToString();
                        string employeeName = reader["TenNV"].ToString();

                        // Đăng nhập thành công cho nhân viên
                        User.SetSession(employeeId, employeeName, "Employee"); // Lưu thông tin nhân viên
                        User.UpdateUserLabel(); // Cập nhật label

                        Form1 newForm = new Form1();
                        newForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Hiển thị thông báo lỗi
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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