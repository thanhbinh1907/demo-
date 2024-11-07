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
    public partial class Form3 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private bool isEditing = false; // Biến để theo dõi trạng thái chỉnh sửa
        public Form3()
        {
            InitializeComponent();
            List<TextBox> list = new List<TextBox> { txtMaHDB, txtMaNV, txtMaKH, txtTongTien };
            function = new Function(dataProcess, dgvDanhSach, list, "HOADONBAN", null, dtpNgayBan, null);
            function.LoadData();
            SetTextBoxReadOnly(true);
            txtMaHDB.ReadOnly = true;
            dtpNgayBan.Enabled = false;
            txtTongTien.ReadOnly = true;

            // Dùng cho MenuStrip 
            MenuStripHelper.ApplyHoverEffect(danhMụcToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(hóaĐơnNhậpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(acnToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhânViênToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhàCungCấpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(tiệnÍchToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(trợGiúpToolStripMenuItem);
        }
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (!isEditing) // Chỉ thực hiện nếu không đang ở chế độ chỉnh sửa
            {
                SetTextBoxReadOnly(true); // Đặt lại TextBox về chế độ chỉ đọc
                dtpNgayBan.Enabled = false; // Vô hiệu hóa DateTimePicker
            }
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtMaNV, txtMaKH })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtMaHDB, txtMaNV, txtMaKH, txtTongTien })
            {
                textBox.Clear();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            form1.Show();

            this.Hide();
        }
        private void hóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện mới của Form2
            Form2 form2 = new Form2();

            // Hiển thị Form2
            form2.Show();

            // Nếu bạn muốn đóng Form1 sau khi mở Form2, hãy thêm dòng sau:
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
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDB, txtMaNV, txtMaKH, txtTongTien };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                txtMaHDB.ReadOnly = false;
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
                dtpNgayBan.Enabled = true;
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
                    txtMaHDB.ReadOnly = true;
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Gọi phương thức Delete từ Function để xóa sản phẩm
                string query = $"DELETE FROM CHITIETHDB WHERE {function.ColumnMappings["txtMaHDB"]} = '{function.TextBoxes[0].Text}'";
                function.Delete(query);
                query = $"DELETE FROM HOADONBAN WHERE {function.ColumnMappings["txtMaHDB"]} = '{function.TextBoxes[0].Text}'";
                function.Delete(query);

                // Cập nhật lại dữ liệu trong DataGridView
                function.LoadData();

                // Xóa các TextBox
                ClearTextBoxes();

                // Đặt lại trạng thái chỉnh sửa
                isEditing = false;
                SetTextBoxReadOnly(true);
                dtpNgayBan.Value = DateTime.Now;
                dtpNgayBan.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDB, txtMaNV, txtMaKH, txtTongTien };
            if (!isEditing)
            {
                // Nếu chưa ở chế độ chỉnh sửa, bỏ chế độ ReadOnly
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang chỉnh sửa
                dtpNgayBan.Enabled = true;

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
                        function.Update(); // Đảm bảo gọi với đường dẫn hình ảnh nếu cần
                        MessageBox.Show("Cập nhật thành công!");

                        // Xóa thông tin cũ để giải phóng bộ nhớ
                        function.ClearOldValues();

                        // Đặt lại TextBox về chế độ chỉ đọc
                        SetTextBoxReadOnly(true);
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                    else
                    {
                        // Nếu không, chỉ đặt lại TextBox về chế độ chỉ đọc
                        SetTextBoxReadOnly(true);
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                }
                else
                {
                    function.Update(); // Test
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    SetTextBoxReadOnly(true);
                    isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    dtpNgayBan.Enabled = false;
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                string maHDB = dgvDanhSach.SelectedRows[0].Cells["MaHDB"].Value.ToString(); // Lấy mã hóa đơn

                // Tạo một instance của Form6 với mã hóa đơn
                Form6 formChiTiet = new Form6(maHDB);

                // Gọi phương thức để nạp chi tiết hóa đơn
                formChiTiet.LoadChiTiet(maHDB);

                formChiTiet.Show(); // Hiển thị form chi tiết hóa đơn

                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
            }
        }
    }
}
