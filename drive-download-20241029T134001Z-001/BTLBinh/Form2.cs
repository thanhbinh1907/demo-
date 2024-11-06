using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public partial class Form2 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private bool isEditing = false; // Biến để theo dõi trạng thái chỉnh sửa
        private DateTime oldNgayNhap; // Lưu ngày nhập cũ

        public Form2()
        {
            InitializeComponent();
            List<TextBox> list = new List<TextBox> { txtMaHDN, txtMaNV, txtMaNCC, txtTongTien };
            function = new Function(dataProcess, dgvDanhSach, list, "HOADONNHAP", null, dtpNgayNhap, null);
            function.LoadData();
            SetTextBoxReadOnly(true);
            txtMaHDN.ReadOnly = true;
            dtpNgayNhap.Enabled = false;
            txtTongTien.ReadOnly = true;
        }
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetTextBoxReadOnly(true); // Đặt lại TextBox về chế độ chỉ đọc
            dtpNgayNhap.Enabled = false; // Vô hiệu hóa DateTimePicker
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtMaNV, txtMaNCC })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }

        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtMaHDN, txtMaNV, txtMaNCC, txtTongTien })
            {
                textBox.Clear();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void hóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDN, txtMaNV, txtMaNCC, txtTongTien };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                txtMaHDN.ReadOnly = false;
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
                dtpNgayNhap.Enabled = true;
            }
            else
            {
                // Lần thứ hai, kiểm tra xem các TextBox đã được điền thông tin chưa
                if (textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
                {
                    // Nếu tất cả các TextBox đã được điền, gọi phương thức Add
                    function.Add();
                    MessageBox.Show("Thêm sản phẩm thành công!");

                    // Thiết lập lại trạng thái
                    SetTextBoxReadOnly(true);
                    txtMaHDN.ReadOnly = true;
                    isEditing = false; // Đánh dấu không còn ở chế độ nhập thông tin
                    ClearTextBoxes(); // Xóa các TextBox sau khi thêm
                    dtpNgayNhap.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Vui lòng điền tất cả các trường trước khi thêm.");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDN, txtMaNV, txtMaNCC, txtTongTien };

            if (!isEditing)
            {
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang chỉnh sửa
                dtpNgayNhap.Enabled = true;

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();
                oldNgayNhap = dtpNgayNhap.Value; // Lưu lại ngày cũ

                // Cập nhật các TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
                }
                if (DateTime.TryParse(oldValues[3], out DateTime oldDate)) // Giả sử ngày nhập là ở vị trí 3
                {
                    dtpNgayNhap.Value = oldDate; // Cập nhật giá trị DateTimePicker
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
                if (dtpNgayNhap.Value != oldNgayNhap)
                {
                    hasChanged = true; // Đánh dấu là có sự thay đổi
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
                    dtpNgayNhap.Enabled = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Gọi phương thức Delete từ Function để xóa sản phẩm
                string query = $"DELETE FROM CHITIETHDN WHERE {function.ColumnMappings["txtMaHDN"]} = '{function.TextBoxes[0].Text}'";
                function.Delete(query);
                query = $"DELETE FROM HOADONNHAP WHERE {function.ColumnMappings["txtMaHDN"]} = '{function.TextBoxes[0].Text}'";
                function.Delete(query);

                // Cập nhật lại dữ liệu trong DataGridView
                function.LoadData();

                // Xóa các TextBox
                ClearTextBoxes();

                // Đặt lại trạng thái chỉnh sửa
                isEditing = false;
                SetTextBoxReadOnly(true);
                dtpNgayNhap.Value = DateTime.Now;
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {

        }
    }
}