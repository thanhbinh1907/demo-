using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTLBinh.Function;
using BTLBinh.Report.RpNhanVien;

namespace BTLBinh
{
    public partial class Form5 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private DateTime oldNgaySinh;
        bool isEditing = false;
        private string oldMaQue;
        public Form5()
        {
            InitializeComponent();
            List<TextBox> list = new List<TextBox> { txtMaNV ,txtTenNV, txtDiaChi, txtSDT };
            function = new Function(dataProcess, dgvDanhSach, list, "NHANVIEN", null, dtpNgaySinh, cbbMaQue,null,clbGioiTinh);
            function.LoadData();
            SetNotEdit();
            function.LoadMaQue(cbbMaQue);
        }
        private void TxtKeyPress(object sender, KeyPressEventArgs e) // Chỉ dc nhập số 
        {
            // Kiểm tra nếu ký tự nhập vào không phải là số, dấu xóa (backspace), dấu phẩy hoặc dấu chấm
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true; // Ngăn chặn nhập ký tự không hợp lệ
            }

            // Nếu đã có dấu phẩy hoặc dấu chấm, không cho phép thêm dấu phẩy hoặc dấu chấm khác
            if ((e.KeyChar == ',' || e.KeyChar == '.') && (sender as TextBox).Text.Contains(",") || (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true; // Ngăn chặn nhập thêm dấu phẩy hoặc dấu chấm
            }
        }
        private void SetEdit()
        {
            SetTextBoxReadOnly(false);
            dtpNgaySinh.Enabled = true;
            clbGioiTinh.Enabled = true;
            cbbMaQue.Enabled = true;
        }
        private void SetNotEdit()
        {
            SetTextBoxReadOnly(true);
            dtpNgaySinh.Enabled=false;
            clbGioiTinh.Enabled=false;
            cbbMaQue.Enabled=false;
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên nhân viên hoặc mã nhân viên")
            {
                txtTimKiem.Text = ""; // Xóa gợi ý khi nhập
                txtTimKiem.ForeColor = Color.Black; // Đặt màu chữ về màu đen
            }
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập tên sản phẩm hoặc mã sản phẩm"; // Đặt lại gợi ý nếu không có gì nhập
                txtTimKiem.ForeColor = Color.Gray; // Đặt màu chữ nhạt
            }
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtTenNV, txtDiaChi, txtSDT })
            {
                textBox.Clear();
            }
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtTenNV, txtDiaChi, txtSDT })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------//
        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNV ,txtTenNV, txtDiaChi, txtSDT };

            if (!isEditing)
            {
                SetEdit();
                isEditing = true; // Đánh dấu là đang chỉnh sửa

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();
                oldNgaySinh = dtpNgaySinh.Value; // Lưu lại ngày cũ

                if (oldValues == null)
                {
                    oldValues = function.GetOldValues();
                }

                // Cập nhật các TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
                }
                if (DateTime.TryParse(oldValues[3], out DateTime oldDate)) // Giả sử ngày nhập là ở vị trí 3
                {
                    dtpNgaySinh.Value = oldDate; // Cập nhật giá trị DateTimePicker
                }
            }
            else
            {
                // Kiểm tra xem thông tin có khác không
                var oldValues = function.GetOldValues();
                bool hasChanged = false;

                if (oldValues == null)
                {
                    oldValues = function.GetOldValues();
                }

                // Kiểm tra các TextBox
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    if (textBoxes[i].Text != oldValues[i])
                    {
                        hasChanged = true;
                        break;
                    }
                }

                // Kiểm tra ngày
                if (dtpNgaySinh.Value != oldNgaySinh)
                {
                    hasChanged = true; // Đánh dấu là có sự thay đổi
                }

                string newMaQue = (cbbMaQue.SelectedItem as ComboBoxItem)?.Value; // Lấy giá trị mới từ ComboBox
                if (!string.IsNullOrEmpty(newMaQue) && newMaQue != oldMaQue)
                {
                    hasChanged = true; // Đánh dấu có sự thay đổi
                }

                if (hasChanged)
                {
                    // Hiện thông báo xác nhận
                    DialogResult result = MessageBox.Show("Thông tin đã thay đổi. Bạn có muốn lưu thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Gọi phương thức Update để cập nhật vào cơ sở dữ liệu
                        function.Update();
                        MessageBox.Show("Cập nhật thành công!");

                        // Xóa thông tin cũ để giải phóng bộ nhớ
                        function.ClearOldValues();

                        // Đặt lại TextBox về chế độ chỉ đọc
                        SetNotEdit();
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                    else
                    {
                        SetNotEdit();
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    SetNotEdit();
                    isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtSDT };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                SetEdit();
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
                if (string.IsNullOrWhiteSpace(txtMaNV.Text))
                {
                    txtMaNV.Text = function.GetNextMa("NHANVIEN", "MaNV", "NV");
                }
            }
            else
            {
                // Lần thứ hai, kiểm tra xem các TextBox đã được điền thông tin chưa
                if (textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
                {
                    string maNV = txtMaNV.Text.Trim();
                    if (function.CheckExists("NHANVIEN", "MaNV", maNV))
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                        return; // Dừng lại nếu mã đã tồn tại
                    }

                    // Thêm nhân viên vào bảng NHANVIEN
                    function.Add();

                    // Thêm mã nhân viên và số điện thoại vào bảng TaiKhoan
                    string sdt = txtSDT.Text.Trim();
                    string addAccountQuery = $"INSERT INTO TaiKhoan (MaNV, MatKhau) VALUES ('{maNV}', '{sdt}')";
                    dataProcess.ExecuteQuery(addAccountQuery); // Sử dụng ExecuteQuery để thực thi câu lệnh

                    // Hiển thị thông báo cho phép nhập email
                    DialogResult result = MessageBox.Show("Bạn có muốn nhập email không? Nếu bỏ qua, mật khẩu sẽ không được thay đổi.", "Nhập Email", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Tạo một InputBox tạm thời để nhập email
                        string email = Microsoft.VisualBasic.Interaction.InputBox("Nhập email:", "Nhập Email", "", -1, -1);

                        // Kiểm tra xem người dùng đã nhập email hay không
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            // Cập nhật email vào cơ sở dữ liệu
                            string updateEmailQuery = $"UPDATE TaiKhoan SET email = '{email}' WHERE MaNV = '{maNV}'";
                            dataProcess.ExecuteQuery(updateEmailQuery);
                        }
                    }

                    MessageBox.Show("Thêm nhân viên thành công!");

                    // Thiết lập lại trạng thái
                    SetNotEdit();
                    isEditing = false; // Đánh dấu không còn ở chế độ nhập thông tin
                    ClearTextBoxes(); // Xóa các TextBox sau khi thêm
                }
                else
                {
                    MessageBox.Show("Vui lòng điền tất cả các trường trước khi thêm.");
                }
            }
        }
  
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không? Điều này sẽ xóa tài khoản liên quan.", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string maNV = function.TextBoxes[0].Text; // Giả sử MaNV ở vị trí đầu tiên

                // Xóa tài khoản liên quan trước
                string deleteAccountQuery = $"DELETE FROM TAIKHOAN WHERE MaNV = '{maNV}'";
                dataProcess.ExecuteQuery(deleteAccountQuery); // Xóa tài khoản

                // Xóa nhân viên từ bảng NHANVIEN
                string deleteEmployeeQuery = $"DELETE FROM NHANVIEN WHERE {function.ColumnMappings["txtMaNV"]} = '{maNV}'"; // Điều kiện dựa trên ID
                function.Delete(deleteEmployeeQuery);

                // Cập nhật lại dữ liệu trong DataGridView
                function.LoadData();

                // Xóa các TextBox
                ClearTextBoxes();

                // Đặt lại trạng thái chỉnh sửa
                isEditing = false;
                SetTextBoxReadOnly(true);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem.Text.Trim(); // Lấy từ khóa tìm kiếm
            function.Search(searchTerm); // Gọi phương thức tìm kiếm
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            function.LoadData();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Nhập tên nhân viên hoặc mã nhân viên";
            txtTimKiem.ForeColor = Color.Gray;
        }

        private void btnRp_Click(object sender, EventArgs e)
        {
            FormReportNhanVien formReport = new FormReportNhanVien();
            formReport.ShowDialog();
        }

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetTextBoxReadOnly(true);
            txtMaNV.ReadOnly = true;
        }
        private void clbGioiTinh_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Kiểm tra nếu mục được chọn là "Nam" hoặc "Nữ"
            if (e.Index == 0) // Giả sử "Nam" là mục đầu tiên
            {
                // Nếu chọn "Nam", bỏ chọn "Nữ"
                if (e.NewValue == CheckState.Checked)
                {
                    clbGioiTinh.SetItemChecked(1, false); // Giả sử "Nữ" là mục thứ hai
                }
            }
            else if (e.Index == 1) // Nếu chọn "Nữ"
            {
                // Nếu chọn "Nữ", bỏ chọn "Nam"
                if (e.NewValue == CheckState.Checked)
                {
                    clbGioiTinh.SetItemChecked(0, false); // Giả sử "Nam" là mục đầu tiên
                }
            }
        }

        private void btnThemGmail_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có nhân viên nào được chọn không
            if (dgvDanhSach.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để thêm email.");
                return;
            }

            // Lấy mã nhân viên từ dòng được chọn
            string maNV = dgvDanhSach.SelectedRows[0].Cells["MaNV"].Value.ToString(); // Sử dụng tên cột thực tế

            // Hiển thị InputBox để nhập email
            string email = Microsoft.VisualBasic.Interaction.InputBox("Nhập email cho nhân viên:", "Thêm Email", "", -1, -1);

            // Kiểm tra xem email có hợp lệ không (có thể thêm kiểm tra định dạng email nếu cần)
            if (!string.IsNullOrWhiteSpace(email))
            {
                // Cập nhật email vào cơ sở dữ liệu
                string updateEmailQuery = $"UPDATE TAIKHOAN SET email = '{email}' WHERE MaNV = '{maNV}'";

                try
                {
                    dataProcess.ExecuteQuery(updateEmailQuery); // Thực hiện câu lệnh cập nhật
                    MessageBox.Show("Email đã được thêm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng thử lại.");
            }
        }
    }
}
