using BTLBinh.Report.RpHoaDonBan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTLBinh.Function;
using BTLBinh.Report.RpHoaDonNhap;

namespace BTLBinh
{
    public partial class Form2 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private bool isEditing = false; // Biến để theo dõi trạng thái chỉnh sửa
        private DateTime oldNgayNhap; // Lưu ngày nhập cũ
        private string oldMaNV;
        
        public Form2()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            List<TextBox> list = new List<TextBox> { txtMaHDN,txtTongTien };
            function = new Function(dataProcess, dgvDanhSach, list, "HOADONNHAP", null, dtpNgayNhap, cbbNhanVien, cbbNhaCC );
            function.LoadData();
            function.LoadMaNV(cbbNhanVien);
            function.LoadMaNCC(cbbNhaCC);
            SetNotEdit();
            txtMaHDN.ReadOnly = true;

            // Dùng cho MenuStrip 
            MenuStripHelper.ApplyHoverEffect(danhMụcToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(hóaĐơnNhậpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(acnToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhânViênToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhàCungCấpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(tiệnÍchToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(trợGiúpToolStripMenuItem);

            User.SetUserLabel(lblUser); // Thiết lập label trong UserSession
            User.UpdateUserLabel(); // Cập nhật label ngay khi form khởi tạo
        }
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetNotEdit();
        }
        private void SetEdit()
        {
            cbbNhaCC.Enabled = true;
            dtpNgayNhap.Enabled = true;
            txtTongTien.ReadOnly = true;
            cbbNhanVien.Enabled = true;

        }
        private void SetNotEdit()
        {
            cbbNhaCC.Enabled = false;
            dtpNgayNhap.Enabled = false;
            txtTongTien.ReadOnly = true;
            cbbNhanVien.Enabled=false;
        }

        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtMaHDN, txtTongTien })
            {
                textBox.Clear();
            }
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
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDN, txtTongTien };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                SetEdit();
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới

                // Tự động sinh mã sản phẩm nếu chưa có
                if (string.IsNullOrWhiteSpace(txtMaHDN.Text))
                {
                    txtMaHDN.Text = function.GetNextMa("HOADONNHAP", "MaHDN", "HDN");
                }

                // Đặt giá trị tổng tiền mặc định là 0
                txtTongTien.Text = "0"; // Thiết lập giá trị tổng tiền về 0
            }
            else
            {

                // Lần thứ hai, kiểm tra xem các TextBox đã được điền thông tin chưa
                if (textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
                {
                    // Kiểm tra mã hóa đơn đã tồn tại hay chưa
                    string maHDN = txtMaHDN.Text.Trim(); // Lấy mã hóa đơn từ TextBox
                    if (function.CheckExists("HOADONNHAP", "MaHDN", maHDN))
                    {
                        // Nếu mã hóa đơn đã tồn tại, thông báo cho người dùng
                        MessageBox.Show("Mã hóa đơn đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Ngừng thực hiện nếu trùng
                    }

                    // Nếu tất cả các TextBox đã được điền và mã hóa đơn không trùng, gọi phương thức Add
                    function.Add();
                    MessageBox.Show("Thêm hóa đơn thành công!");

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

        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaHDN, txtTongTien };

            if (!isEditing)
            {
                isEditing = true; // Đánh dấu là đang chỉnh sửa
                SetEdit();

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();
                oldNgayNhap = dtpNgayNhap.Value; // Lưu lại ngày cũ

                if (oldValues == null)
                {
                    oldValues = function.GetOldValues();
                }

                // Cập nhật các TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
                }
                if (DateTime.TryParse(oldValues[1], out DateTime oldDate)) // Giả sử ngày nhập là ở vị trí 3
                {
                    dtpNgayNhap.Value = oldDate; // Cập nhật giá trị DateTimePicker
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
                if (dtpNgayNhap.Value != oldNgayNhap)
                {
                    hasChanged = true; // Đánh dấu là có sự thay đổi
                }

                string newMaNV = (cbbNhanVien.SelectedItem as ComboBoxItem)?.Value; // Lấy giá trị mới từ ComboBox
                if (!string.IsNullOrEmpty(newMaNV) && newMaNV != oldMaNV)
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
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                    else
                    {
                        isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    }
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
                    SetNotEdit();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo);
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
                dtpNgayNhap.Value = DateTime.Now;
                SetNotEdit();
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                string maHDN = dgvDanhSach.SelectedRows[0].Cells["MaHDN"].Value.ToString(); // Lấy mã hóa đơn

                // Tạo một instance của Form6 với mã hóa đơn
                Form7 formChiTiet = new Form7(maHDN);

                // Gọi phương thức để nạp chi tiết hóa đơn
                formChiTiet.LoadChiTiet(maHDN);

                formChiTiet.Show(); // Hiển thị form chi tiết hóa đơn

                formChiTiet.FormClosed += (formSender, formEventArgs) => function.LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            function.LoadData();
            
        }

        private void oRDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order form = new Order();
            form.Show();
            this.Hide();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            // Xóa thông tin phiên làm việc
            User.ClearSession();

            // Hiển thị thông báo hoặc thực hiện hành động khác (tuỳ chọn)
            MessageBox.Show("Bạn đã đăng xuất thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Quay trở lại form đăng nhập (hoặc form chính)
            Form8 loginForm = new Form8(); // Giả sử bạn có FormLogin
            loginForm.Show();
            this.Hide(); // Ẩn form hiện tại
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReportHDN formReport = new FormReportHDN();
            formReport.ShowDialog();
        }
    }
}