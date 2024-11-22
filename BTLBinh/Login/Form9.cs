using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail; // Thêm thư viện để gửi email
using System.Timers; // Thêm thư viện để quản lý thời gian OTP

namespace BTLBinh
{
    public partial class Form9 : Form
    {
        private string otp;
        private DateTime otpExpiration;
        private DataProcess dataProcess = new DataProcess();
        private TextBox txtOtp;
        private System.Windows.Forms.Timer otpTimer;
        private int remainingTime = 60; // Thời gian còn lại (60 giây)
        private string storedEmail; // Biến để lưu email đã nhập

        public Form9()
        {
            this.FormClosed += (sender, e) => storedEmail = null;

            InitializeComponent();
            txtOldPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;

            // Khởi tạo txtOtp
            txtOtp = new TextBox()
            {
                Left = txtOldPassword.Left,
                Top = txtOldPassword.Top,
                Width = txtOldPassword.Width,
                Visible = false // Ẩn ô này khi khởi động
            };
            this.Controls.Add(txtOtp); // Thêm vào form

            // Khởi tạo Timer
            otpTimer = new System.Windows.Forms.Timer();
            otpTimer.Interval = 1000; // 1 giây
            otpTimer.Tick += OtpTimer_Tick;

            btnResendOtp.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có đang nhập OTP không
            if (txtOtp.Visible)
            {
                // Nếu OTP đã được gửi và còn hiệu lực, kiểm tra OTP
                if (!string.IsNullOrEmpty(otp) && DateTime.Now < otpExpiration)
                {
                    if (txtOtp.Text.Trim() == otp)
                    {
                        // Nếu mã OTP đúng, cập nhật mật khẩu mà không cần mật khẩu cũ
                        UpdatePasswordWithoutOld();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Mã OTP không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Mã OTP đã hết hạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Nếu không nhập OTP, kiểm tra mật khẩu cũ từ cơ sở dữ liệu
            string username = txtUser.Text.Trim();
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            DataProcess dataProcess = new DataProcess();
            string queryCheckOldPassword = "SELECT MatKhau FROM TaiKhoan WHERE MaNV = @MaNV";

            string currentPasswordFromDB = string.Empty;

            using (SqlConnection connection = new SqlConnection(dataProcess.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryCheckOldPassword, connection))
                {
                    command.Parameters.AddWithValue("@MaNV", username);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        currentPasswordFromDB = reader["MatKhau"].ToString();
                    }
                }
            }

            // Kiểm tra mật khẩu cũ có đúng không
            if (oldPassword != currentPasswordFromDB)
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

            // Cập nhật mật khẩu mới trong bảng TAIKHOAN
            string queryUpdatePassword = "UPDATE TaiKhoan SET MatKhau = @NewPassword WHERE MaNV = @MaNV";

            using (SqlConnection connection = new SqlConnection(dataProcess.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdatePassword, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@MaNV", username);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Password.CurrentPassword = newPassword; // Cập nhật mật khẩu mới trong bộ nhớ
                        File.WriteAllText("password.txt", newPassword); // Lưu mật khẩu mới vào tệp
                        MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            this.Close(); // Đóng form thay đổi mật khẩu
        }

        private void UpdatePasswordWithoutOld()
        {
            string username = txtUser.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            // Cập nhật mật khẩu mới trong bảng TAIKHOAN
            string queryUpdatePassword = "UPDATE TaiKhoan SET MatKhau = @NewPassword WHERE MaNV = @MaNV";

            using (SqlConnection connection = new SqlConnection(dataProcess.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryUpdatePassword, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@MaNV", username);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Password.CurrentPassword = newPassword; // Cập nhật mật khẩu mới trong bộ nhớ
                        File.WriteAllText("password.txt", newPassword); // Lưu mật khẩu mới vào tệp
                        MessageBox.Show("Thay đổi mật khẩu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mã nhân viên không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lblQuenMK_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();

            // Kiểm tra xem ô tài khoản có trống không
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tài khoản!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng thực hiện nếu ô tài khoản trống
            }

            string emailAddress = PromptForEmailAddress();

            if (string.IsNullOrEmpty(emailAddress)) return;

            // Kiểm tra email trong cơ sở dữ liệu
            string emailFromDB = GetEmailFromDatabase(username);

            if (emailAddress != emailFromDB)
            {
                MessageBox.Show("Địa chỉ email không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lưu email vào biến để sử dụng sau này
            storedEmail = emailAddress;

            // Tạo mã OTP và thời gian hết hạn
            otp = GenerateOtp();
            otpExpiration = DateTime.Now.AddMinutes(1);

            // Gửi mã OTP qua email
            SendOtpToEmail(storedEmail, otp);

            // Hiển thị thông báo
            MessageBox.Show("Mã OTP đã được gửi đến email của bạn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ẩn ô nhập mật khẩu cũ và hiện ô nhập OTP
            txtOldPassword.Visible = false;
            lblOldPassword.Text = "Nhập OTP";
            txtOtp.Visible = true;
            btnResendOtp.Visible = true;
            txtOtp.Focus(); // Đặt con trỏ vào ô nhập OTP

            // Bắt đầu đếm ngược
            remainingTime = 60; // 60 giây
            btnResendOtp.Enabled = false; // Vô hiệu hóa nút gửi lại ngay khi gửi OTP
            otpTimer.Start(); // Bắt đầu đếm ngược
            btnResendOtp.Text = $"Gửi lại ({remainingTime}s)"; // Cập nhật văn bản cho nút
        }

        private string PromptForEmailAddress()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "Nhập địa chỉ email";
                Label textLabel = new Label() { Left = 20, Top = 20, Text = "Địa chỉ email:" };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
                Button confirmation = new Button() { Text = "Xác nhận", Left = 180, Width = 80, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.ShowDialog();
                return textBox.Text;
            }
        }

        private void SendOtpToEmail(string emailAddress, string otp)
        {
            string subject = "Mã OTP của bạn";
            string body = $"Mã OTP của bạn là: {otp}. Hạn sử dụng trong 1 phút.";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("thanhvsbinh4a@gmail.com"); // Địa chỉ email của bạn
                mail.To.Add(emailAddress); // Địa chỉ email của người nhận
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("thanhvsbinh4a@gmail.com", "gfkh wmsp yklo qyss"); // Sử dụng mật khẩu ứng dụng
                    smtp.EnableSsl = true; // Bật SSL để bảo mật

                    try
                    {
                        smtp.Send(mail); // Gửi email
                    }
                    catch (SmtpException ex)
                    {
                        MessageBox.Show($"Lỗi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Tạo OTP có 6 chữ số
        }

        private string GetEmailFromDatabase(string username)
        {
            string email = string.Empty;
            string queryCheckEmail = "SELECT Email FROM TaiKhoan WHERE MaNV = @MaNV"; // Cập nhật tên cột cho đúng

            using (SqlConnection connection = new SqlConnection(dataProcess.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryCheckEmail, connection))
                {
                    command.Parameters.AddWithValue("@MaNV", username);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        email = reader["Email"].ToString(); // Đảm bảo cột Email tồn tại trong bảng
                    }
                }
            }

            return email;
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtOldPassword.UseSystemPasswordChar = false;
                txtNewPassword.UseSystemPasswordChar = false;
                txtConfirm.UseSystemPasswordChar = false;
            }
            else
            {
                txtOldPassword.UseSystemPasswordChar = true;
                txtNewPassword.UseSystemPasswordChar = true;
                txtConfirm.UseSystemPasswordChar = true;
            }
        }
        private void OtpTimer_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            if (remainingTime <= 0)
            {
                otpTimer.Stop(); // Dừng timer
                btnResendOtp.Enabled = true; // Kích hoạt lại nút gửi lại
                btnResendOtp.Text = "Gửi lại mã OTP"; // Đặt lại văn bản cho nút
            }
            else
            {
                btnResendOtp.Enabled = false; // Vô hiệu hóa nút gửi lại
                btnResendOtp.Text = $"Gửi lại ({remainingTime}s)"; // Cập nhật văn bản cho nút
            }
        }

        private void btnResendOtp_Click(object sender, EventArgs e)
        {
            // Sử dụng email đã lưu trong biến storedEmail
            if (!string.IsNullOrEmpty(storedEmail))
            {
                // Tạo mã OTP và thời gian hết hạn
                otp = GenerateOtp();
                otpExpiration = DateTime.Now.AddMinutes(1);

                // Gửi mã OTP qua email
                SendOtpToEmail(storedEmail, otp);

                // Đặt lại thời gian đếm ngược
                remainingTime = 60; // 60 giây
                otpTimer.Start(); // Bắt đầu đếm ngược

                MessageBox.Show("Mã OTP đã được gửi lại đến email của bạn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập email trước khi gửi lại mã OTP!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}