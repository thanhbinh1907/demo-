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
            List<TextBox> list = new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtGioiTinh, txtSDT };
            function = new Function(dataProcess, dgvDanhSach, list, "NHANVIEN", null, dtpNgaySinh, cbbMaQue);
            function.LoadData();
            SetTextBoxReadOnly(true);
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
            foreach (var textBox in new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtGioiTinh, txtSDT })
            {
                textBox.Clear();
            }
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtGioiTinh, txtSDT })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------//
        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtGioiTinh, txtSDT };

            if (!isEditing)
            {
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang chỉnh sửa

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();
                oldNgaySinh = dtpNgaySinh.Value; // Lưu lại ngày cũ

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
                        SetTextBoxReadOnly(true);
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                    else
                    {
                        SetTextBoxReadOnly(true);
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    SetTextBoxReadOnly(true);
                    isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNV, txtTenNV, txtDiaChi, txtGioiTinh, txtSDT };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                txtMaNV.ReadOnly = false;
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
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
                    // Nếu tất cả các TextBox đã được điền, gọi phương thức Add
                    function.Add();
                    MessageBox.Show("Thêm nhân viên thành công!");

                    // Thiết lập lại trạng thái
                    SetTextBoxReadOnly(true);
                    txtMaNV.ReadOnly = true;
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Gọi phương thức Delete từ Function để xóa sản phẩm
                string query = $"DELETE FROM NHANVIEN WHERE {function.ColumnMappings["txtMaNV"]} = '{function.TextBoxes[0].Text}'"; // Điều kiện dựa trên ID
                function.Delete(query);

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
    }
}
