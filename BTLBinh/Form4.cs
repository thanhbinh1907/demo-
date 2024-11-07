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
    public partial class Form4 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private bool isEditing = false;
        public Form4()
        {
            InitializeComponent();
            List<TextBox> list = new List<TextBox> { txtMaNCC, txtTenNCC, txtDiaChi, txtSDT };
            function = new Function(dataProcess, dgvDanhSach, list, "NHACUNGCAP", null, null, null);
            function.LoadData();
            txtMaNCC.ReadOnly = true;
            SetTextBoxReadOnly(true);
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtTenNCC, txtDiaChi, txtSDT })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtMaNCC, txtTenNCC, txtDiaChi, txtSDT })
            {
                textBox.Clear();
            }
        }
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetTextBoxReadOnly(true); // Đặt lại TextBox về chế độ chỉ đọc
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNCC, txtTenNCC, txtDiaChi, txtSDT };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                txtMaNCC.ReadOnly = false;
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
            }
            else
            {
                // Lần thứ hai, kiểm tra xem các TextBox đã được điền thông tin chưa
                if (textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
                {
                    string maNCC = txtMaNCC.Text.Trim();

                    // Kiểm tra mã nhà cung cấp đã tồn tại chưa
                    if (function.CheckExists("NHACUNGCAP", "MaNCC", maNCC))
                    {
                        MessageBox.Show("Mã nhà cung cấp đã tồn tại. Vui lòng nhập mã khác.");
                        return; // Dừng lại nếu mã đã tồn tại
                    }

                    // Nếu tất cả các TextBox đã được điền và mã chưa tồn tại, gọi phương thức Add
                    function.Add();
                    MessageBox.Show("Thêm nhà cung cấp thành công!");

                    // Thiết lập lại trạng thái
                    SetTextBoxReadOnly(true);
                    txtMaNCC.ReadOnly = true;
                    isEditing = false; // Đánh dấu không còn ở chế độ nhập thông tin
                    ClearTextBoxes(); // Xóa các TextBox sau khi thêm
                }
                else
                {
                    MessageBox.Show("Vui lòng điền tất cả các trường trước khi thêm.");
                }
            }
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Gọi phương thức Delete từ Function để xóa sản phẩm
                string query = $"DELETE FROM NHACUNGCAP WHERE {function.ColumnMappings["txtMaNCC"]} = '{function.TextBoxes[0].Text}'"; // Điều kiện dựa trên ID
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaNCC, txtTenNCC, txtDiaChi, txtSDT };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng sửa thông tin
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang chỉnh sửa

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();

                // Cập nhật các TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
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

                if (hasChanged)
                {
                    // Hiện thông báo xác nhận
                    DialogResult result = MessageBox.Show("Thông tin đã thay đổi. Bạn có muốn lưu thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Gọi phương thức Update để cập nhật vào cơ sở dữ liệu
                        function.Update();
                        MessageBox.Show("Cập nhật thành công!");

                        // Cập nhật lại dữ liệu trong DataGridView
                        function.LoadData();

                        // Đặt lại trạng thái chỉnh sửa
                        SetTextBoxReadOnly(true);
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                    else
                    {
                        // Đặt lại trạng thái nếu không lưu
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
    }
}
