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
    public partial class Form6 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private bool isEditing = false;
        private string maHDB;

        public Form6(string maHDB)
        {
            InitializeComponent();
            this.maHDB = maHDB; // Lưu mã hóa đơn
            LoadChiTiet(); // Gọi phương thức để tải chi tiết hóa đơn
            txtMaHDB.ReadOnly = true;
            LoadComboBoxMaSP();
            txtMaHDB.Text = maHDB;
            cbbMaSP.Enabled = false;
            SetTextBoxReadOnly(true);
        }
        private void LoadChiTiet()
        {
            // Kiểm tra mã hóa đơn không rỗng
            if (string.IsNullOrWhiteSpace(maHDB))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return;
            }

            // Câu truy vấn để lấy chi tiết hóa đơn
            string query = $"SELECT * FROM CHITIETHDB WHERE MaHDB = '{maHDB}'";

            try
            {
                DataTable dt = dataProcess.DataConnect(query); // Thực hiện truy vấn

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Thiết lập DataSource cho DataGridView
                    dgvDanhSach.DataSource = dt;

                    // Tuỳ chọn: ẩn các cột không cần thiết
                    dgvDanhSach.Columns["MaHDB"].Visible = false; 
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết hóa đơn.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtTenSP, txtKhuyenMai, txtThanhTien, txtSoLuong })
            {
                textBox.Clear();
            }
        }
        private void LoadComboBoxMaSP()
        {
            string query = "SELECT MaSP FROM SANPHAM"; // Truy vấn lấy mã sản phẩm
            try
            {
                DataTable dt = dataProcess.DataConnect(query);
                cbbMaSP.DataSource = dt;
                cbbMaSP.DisplayMember = "MaSP"; // Hiển thị mã sản phẩm
                cbbMaSP.ValueMember = "MaSP"; // Giá trị của ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nạp dữ liệu vào ComboBox: {ex.Message}");
            }
        }
        private void CbbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy mã sản phẩm đã chọn từ ComboBox
            string maSP = cbbMaSP.SelectedValue?.ToString();

            if (!string.IsNullOrWhiteSpace(maSP))
            {
                // Gọi phương thức để lấy tên sản phẩm từ cơ sở dữ liệu
                string tenSP = GetTenSP(maSP);
                txtTenSP.Text = tenSP; // Cập nhật TextBox với tên sản phẩm
            }
        }
        private string GetTenSP(string maSP)
        {
            string tenSP = string.Empty;

            // Câu truy vấn để lấy tên sản phẩm
            string query = $"SELECT TenSP FROM SANPHAM WHERE MaSP = '{maSP}'";

            try
            {
                DataTable dt = dataProcess.DataConnect(query); // Thực hiện truy vấn

                if (dt != null && dt.Rows.Count > 0)
                {
                    tenSP = dt.Rows[0]["TenSP"].ToString(); // Lấy tên sản phẩm từ kết quả truy vấn
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tên sản phẩm cho mã sản phẩm đã chọn.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy tên sản phẩm: {ex.Message}");
            }

            return tenSP;
        }
        public void LoadChiTiet(string maHDB)
        {
            if(string.IsNullOrWhiteSpace(maHDB))
    {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return;
            }

            // Truy vấn lấy thông tin sản phẩm và chi tiết hóa đơn
            string query = $@"
        SELECT 
            c.MaSP, 
            s.TenSP, 
            c.SoLuong, 
            s.GiaBan AS DonGia,  -- Đơn giá từ bảng sản phẩm
            c.KhuyenMai, 
            (c.SoLuong * (s.GiaBan - (s.GiaBan * c.KhuyenMai / 100))) AS ThanhTien
        FROM 
            CHITIETHDB c 
        JOIN 
            SANPHAM s ON c.MaSP = s.MaSP 
        WHERE 
            c.MaHDB = '{maHDB}'";

            try
            {
                DataTable dt = dataProcess.DataConnect(query); // Thực hiện truy vấn

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Hiển thị dữ liệu lên DataGridView
                    dgvDanhSach.DataSource = dt;

                    // Đặt thứ tự hiển thị cho các cột
                    dgvDanhSach.Columns["MaSP"].DisplayIndex = 0;
                    dgvDanhSach.Columns["TenSP"].DisplayIndex = 1;
                    dgvDanhSach.Columns["SoLuong"].DisplayIndex = 2;
                    dgvDanhSach.Columns["DonGia"].DisplayIndex = 3; // Đặt "Đơn giá" trước "Thành tiền"
                    dgvDanhSach.Columns["KhuyenMai"].DisplayIndex = 4;
                    dgvDanhSach.Columns["ThanhTien"].DisplayIndex = 5;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết hóa đơn.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtTenSP, txtKhuyenMai, txtThanhTien, txtSoLuong })
            {
                textBox.ReadOnly = isReadOnly;
            }
        }
        private decimal GetGiaSP(string maSP)
        {
            decimal gia = 0;

            // Câu truy vấn để lấy giá sản phẩm
            string query = $"SELECT GiaBan FROM SANPHAM WHERE MaSP = '{maSP}'";

            try
            {
                DataTable dt = dataProcess.DataConnect(query); // Thực hiện truy vấn

                if (dt != null && dt.Rows.Count > 0)
                {
                    gia = Convert.ToDecimal(dt.Rows[0]["GiaBan"]); // Lấy giá sản phẩm từ kết quả truy vấn
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy giá sản phẩm: {ex.Message}");
            }

            return gia;
        }
        private void AddChiTietHoaDon(string maHDB, string maSP, int soLuong, decimal gia, decimal khuyenMai)
        {
            string query = $"INSERT INTO CHITIETHDN (MaHDB, MaSP, SoLuong, DonGia, KhuyenMai) VALUES ('{maHDB}', '{maSP}', {soLuong}, {gia}, {khuyenMai})";

            try
            {
                dataProcess.ExecuteQuery(query); // Thực hiện truy vấn thêm dữ liệu
                MessageBox.Show("Thêm sản phẩm thành công.");
                LoadChiTiet(); // Tải lại chi tiết hóa đơn để hiển thị sản phẩm mới
                ClearTextBoxes(); // Xóa các trường nhập liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sản phẩm vào hóa đơn: {ex.Message}");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                SetTextBoxReadOnly(false);
                cbbMaSP.Enabled = true;
                isEditing = true;
                ClearTextBoxes();
            }
            else
            {
                // Lấy giá trị từ các điều khiển
                string maSP = cbbMaSP.SelectedValue?.ToString();
                if (string.IsNullOrWhiteSpace(maSP))
                {
                    MessageBox.Show("Vui lòng chọn mã sản phẩm.");
                    return;
                }

                int soLuong;
                if (!int.TryParse(txtSoLuong.Text, out soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ.");
                    return;
                }

                decimal khuyenMai;
                if (!decimal.TryParse(txtKhuyenMai.Text, out khuyenMai) || khuyenMai < 0 || khuyenMai > 100)
                {
                    MessageBox.Show("Khuyến mãi không hợp lệ. Vui lòng nhập số từ 0 đến 100.");
                    return;
                }

                // Lấy giá sản phẩm từ cơ sở dữ liệu
                decimal gia = GetGiaSP(maSP);
                if (gia == 0)
                {
                    MessageBox.Show("Không tìm thấy giá sản phẩm.");
                    return;
                }

                // Tính toán thành tiền
                decimal thanhTien = (gia - (gia * khuyenMai / 100)) * soLuong;
                txtThanhTien.Text = thanhTien.ToString("N0"); // Hiển thị thành tiền

                // Thêm sản phẩm vào bảng chi tiết hóa đơn
                AddChiTietHoaDon(maHDB, maSP, soLuong, gia, khuyenMai);

                // Đặt lại trạng thái chỉnh sửa
                SetTextBoxReadOnly(true);
                cbbMaSP.Enabled = false;
                isEditing = false;
            }
        }
    }
}


