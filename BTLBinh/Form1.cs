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
    public partial class Form1 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private Function function;
        private string imagePath; // Biến lưu đường dẫn hình ảnh
        private bool isEditing = false; // Biến để theo dõi trạng thái chỉnh sửa
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            List<TextBox> list = new List<TextBox> { txtMaSp, txtTenSp, txtMaLoai, txtGiaNhap, txtGiaBan, txtSoLuong, txtMaCD };
            function = new Function(dataProcess, dgvDanhSach, list, "SANPHAM", pbAnh, null, null);
            function.LoadData();
            pbAnh.SizeMode = PictureBoxSizeMode.StretchImage;
            SetTextBoxReadOnly(true);
            txtMaSp.ReadOnly = true;
            function.ImageSelected += (imagePath) =>
            {
                pbAnh.Image?.Dispose(); // Giải phóng hình ảnh cũ
                pbAnh.Image = Image.FromFile(imagePath); // Hiển thị hình ảnh trong PictureBox
            };

            // Dùng cho MenuStrip 
            MenuStripHelper.ApplyHoverEffect(danhMụcToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(hóaĐơnNhậpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(acnToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhânViênToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhàCungCấpToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(tiệnÍchToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(trợGiúpToolStripMenuItem);
        }
        //----------THEM MOI----------------------------------------------------------------------------------------------------------------//
        private void Form1_Load(object sender, EventArgs e)
        {
            txtTimKiem.Text = "Nhập tên sản phẩm hoặc mã sản phẩm";
            txtTimKiem.ForeColor = Color.Gray;
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập tên sản phẩm hoặc mã sản phẩm")
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
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetTextBoxReadOnly(true); // Đặt lại TextBox về chế độ chỉ đọc
        }
        //----------------------------------------------------------------------------------------------------------------------------//
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtTenSp, txtMaLoai, txtGiaNhap, txtGiaBan, txtSoLuong, txtMaCD })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtMaSp, txtTenSp, txtMaLoai, txtGiaNhap, txtGiaBan, txtSoLuong, txtMaCD })
            {
                textBox.Clear();
            }
        }
        // MENU STRIP --------------------------------------------------------------------------------------------------------//
        private void hóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện mới của Form2`1
            Form2 form2 = new Form2();

            // Hiển thị Form2
            form2.Show();

            // Nếu bạn muốn đóng Form1 sau khi mở Form2, hãy thêm dòng sau:
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

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order form7 = new Order();
            form7.Show();
            this.Hide();
        }
        //--------------------------------------------------------------------------------------------------------------------//


        //-----BUTTON----------------------------------------------------------------------------------------------------------//
        private void btnThem_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaSp, txtTenSp, txtMaLoai, txtGiaNhap, txtGiaBan, txtSoLuong, txtMaCD };

            if (!isEditing)
            {
                // Lần đầu tiên, cho phép người dùng nhập thông tin
                txtMaSp.ReadOnly = false;
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang ở chế độ nhập thông tin
                ClearTextBoxes(); // Xóa các TextBox để người dùng có thể nhập thông tin mới
            }
            else
            {
                // Lần thứ hai, kiểm tra xem các TextBox đã được điền thông tin chưa
                if (textBoxes.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
                {
                    string maSp = txtMaSp.Text.Trim();

                    // Kiểm tra mã sản phẩm đã tồn tại chưa
                    if (function.CheckExists("SANPHAM", "MaSP", maSp))
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại. Vui lòng nhập mã khác.");
                        return; // Dừng lại nếu mã đã tồn tại
                    }

                    // Nếu tất cả các TextBox đã được điền và mã chưa tồn tại, gọi phương thức Add
                    function.Add(imagePath);
                    MessageBox.Show("Thêm sản phẩm thành công!");

                    // Thiết lập lại trạng thái
                    SetTextBoxReadOnly(true);
                    txtMaSp.ReadOnly = true;
                    isEditing = false; // Đánh dấu không còn ở chế độ nhập thông tin
                    ClearTextBoxes(); // Xóa các TextBox sau khi thêm
                }
                else
                {
                    MessageBox.Show("Vui lòng điền tất cả các trường trước khi thêm.");
                }
            }
        }
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lưu đường dẫn hình ảnh vào biến
                    imagePath = openFileDialog.FileName;

                    // Hiển thị hình ảnh trong PictureBox
                    pbAnh.Image = Image.FromFile(imagePath);
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox> { txtMaSp, txtTenSp, txtMaLoai, txtGiaNhap, txtGiaBan, txtSoLuong, txtMaCD };

            if (!isEditing)
            {
                // Nếu chưa ở chế độ chỉnh sửa, bỏ chế độ ReadOnly
                SetTextBoxReadOnly(false);
                isEditing = true; // Đánh dấu là đang chỉnh sửa

                // Lấy thông tin cũ từ Function
                var oldValues = function.GetOldValues();

                // Cập nhật các TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
                }

                // Đặt đường dẫn hình ảnh cũ vào biến tạm
                imagePath = function.GetOldImagePath(); // Thêm phương thức này trong Function để lấy đường dẫn hình ảnh cũ
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

                // Kiểm tra đường dẫn hình ảnh
                if (imagePath != function.GetOldImagePath()) // So sánh đường dẫn hình ảnh mới và cũ
                {
                    hasChanged = true;
                }

                if (hasChanged)
                {
                    // Hiện thông báo xác nhận
                    DialogResult result = MessageBox.Show("Thông tin đã thay đổi. Bạn có muốn lưu thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Gọi phương thức Update để cập nhật vào cơ sở dữ liệu
                        function.Update(imagePath); // Đảm bảo gọi với đường dẫn hình ảnh nếu cần
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
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    SetTextBoxReadOnly(true);
                    isEditing = false; // Đánh dấu không còn ở chế độ chỉnh sửa
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
                string query = $"DELETE FROM SANPHAM WHERE {function.ColumnMappings["txtMaSp"]} = '{function.TextBoxes[0].Text}'"; // Điều kiện dựa trên ID
                function.Delete(query);

                // Cập nhật lại dữ liệu trong DataGridView
                function.LoadData();

                // Xóa các TextBox
                ClearTextBoxes();

                // Đặt lại trạng thái chỉnh sửa
                isEditing = false;
                SetTextBoxReadOnly(true);
                imagePath = null; // Đặt lại đường dẫn hình ảnh
                pbAnh.Image = null; // Xóa hình ảnh trong PictureBox
            }
        }
        //----------------------------------------------THEM MOI ----------------------------------------//
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string searchTerm = txtTimKiem.Text.Trim(); // Lấy từ khóa tìm kiếm
            function.Search(searchTerm); // Gọi phương thức tìm kiếm
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            function.LoadData();
        }
        //--------------------------------------------------------------------------------------------------//

    }
}
