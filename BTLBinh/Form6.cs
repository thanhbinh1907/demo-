using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using BTLBinh.Report.RpChiTietHoaDonBan;

namespace BTLBinh
{
    public partial class Form6 : Form
    {
        private DataProcess dataProcess = new DataProcess();
        private bool isEditing = false;
        private string maHDB;
        private int SoLuongCu;
        private decimal KhuyenMaiCu;
        private string maHDTro;
        public Form6(string maHDB)
        {
            InitializeComponent();
            this.maHDB = maHDB; // Lưu mã hóa đơn
            LoadChiTiet(maHDB); // Gọi phương thức để tải chi tiết hóa đơn
            txtMaHDB.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            LoadComboBoxMaSP();
            txtMaHDB.Text = maHDB;
            cbbMaSP.Enabled = false;
            SetTextBoxReadOnly(true);
            txtThanhTien.ReadOnly = true;

            CheckAndAddChiTietHoaDon(maHDB);

        }
        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một dòng hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgvDanhSach.Rows[e.RowIndex];

                // Lấy thông tin từ các ô trong dòng đã chọn
                string maSP = selectedRow.Cells["MaSP"].Value.ToString();
                int soLuong = int.Parse(selectedRow.Cells["SoLuong"].Value.ToString());
                decimal khuyenMai = decimal.Parse(selectedRow.Cells["KhuyenMai"].Value.ToString());
                decimal thanhTien = decimal.Parse(selectedRow.Cells["ThanhTien"].Value.ToString());

                // Thiết lập giá trị cho ComboBox và các textbox khác
                cbbMaSP.SelectedValue = maSP; // Chọn mã sản phẩm trong ComboBox
                txtSoLuong.Text = soLuong.ToString();
                txtKhuyenMai.Text = khuyenMai.ToString();
                txtThanhTien.Text = thanhTien.ToString();
            }
        }
        private void DgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            SetTextBoxReadOnly(true); // Đặt lại TextBox về chế độ chỉ đọc
        }
        private void ClearTextBoxes()
        {
            foreach (var textBox in new List<TextBox> { txtTenSP, txtKhuyenMai, txtThanhTien, txtSoLuong, txtDonGia })
            {
                textBox.Clear();
            }
            cbbMaSP.SelectedIndex = -1;
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
                // Gọi phương thức để lấy tên sản phẩm, đơn giá và khuyến mãi từ cơ sở dữ liệu
                var (tenSP, giaSP, khuyenMai) = GetSPDetails(maSP);

                txtTenSP.Text = tenSP; // Cập nhật TextBox với tên sản phẩm
                txtDonGia.Text = giaSP.ToString("N0"); // Cập nhật TextBox với đơn giá

                // Kiểm tra và cập nhật trạng thái ReadOnly cho txtKhuyenMai
                if (khuyenMai > 0)
                {
                    txtKhuyenMai.Text = khuyenMai.ToString("N2"); // Cập nhật TextBox với khuyến mãi
                    txtKhuyenMai.ReadOnly = true; // Đặt ReadOnly nếu có khuyến mãi
                }
                else
                {
                    // Nếu không có khuyến mãi, cho phép người dùng nhập
                    txtKhuyenMai.Text = ""; // Trống ô khuyến mãi
                    txtKhuyenMai.ReadOnly = false; // Không ReadOnly
                }
            }
        }
        private (string, decimal, decimal) GetSPDetails(string maSP)
        {
            string tenSP = string.Empty;
            decimal giaSP = 0;
            decimal khuyenMai = 0;

            // Câu truy vấn để lấy tên sản phẩm và đơn giá từ bảng SANPHAM
            // Sử dụng LEFT JOIN để lấy thông tin sản phẩm ngay cả khi không có khuyến mãi
            string query = $@"
    SELECT 
        s.TenSP, 
        s.GiaBan, 
        ISNULL(c.KhuyenMai, 0) AS KhuyenMai  -- Sử dụng ISNULL để mặc định khuyến mãi là 0 nếu không có
    FROM 
        SANPHAM s 
    LEFT JOIN 
        CHITIETHDB c ON s.MaSP = c.MaSP AND c.MaHDB = '{maHDB}' 
    WHERE 
        s.MaSP = '{maSP}'";

            try
            {
                DataTable dt = dataProcess.DataConnect(query); // Thực hiện truy vấn

                if (dt != null && dt.Rows.Count > 0)
                {
                    tenSP = dt.Rows[0]["TenSP"].ToString(); // Lấy tên sản phẩm
                    giaSP = Convert.ToDecimal(dt.Rows[0]["GiaBan"]); // Lấy đơn giá
                    khuyenMai = Convert.ToDecimal(dt.Rows[0]["KhuyenMai"]); // Lấy khuyến mãi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin sản phẩm: {ex.Message}");
            }

            return (tenSP, giaSP, khuyenMai);
        }
        public void LoadChiTiet(string maHDB)
        {
            if (string.IsNullOrWhiteSpace(maHDB))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return;
            }

            // Truy vấn kết hợp dữ liệu từ cả hai bảng để lấy Tên sản phẩm và Đơn giá
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
                    dgvDanhSach.DataSource = dt;

                    // Sắp xếp thứ tự cột nếu cần
                    dgvDanhSach.Columns["MaSP"].DisplayIndex = 0;
                    dgvDanhSach.Columns["TenSP"].DisplayIndex = 1;
                    dgvDanhSach.Columns["SoLuong"].DisplayIndex = 2;
                    dgvDanhSach.Columns["DonGia"].DisplayIndex = 3;
                    dgvDanhSach.Columns["KhuyenMai"].DisplayIndex = 4;
                    dgvDanhSach.Columns["ThanhTien"].DisplayIndex = 5;

                    // Tính tổng thành tiền
                    decimal tongTien = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        tongTien += Convert.ToDecimal(row["ThanhTien"]);
                    }

                    // Cập nhật tổng tiền vào txtTongTien
                    txtTongTien.Text = tongTien.ToString("N0");

                    // Cập nhật tổng tiền vào bảng HOADONBAN
                    UpdateTongTien(maHDB, tongTien);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
            }
        }
        private void SetTextBoxReadOnly(bool isReadOnly)
        {
            foreach (var textBox in new List<TextBox> { txtKhuyenMai, txtThanhTien, txtSoLuong })
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
            string maHDBToUse = !string.IsNullOrWhiteSpace(maHDTro) ? maHDTro : maHDB;

            if (string.IsNullOrWhiteSpace(maHDBToUse))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return;
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong bảng CHITIETHDN chưa
            string checkQuery = $"SELECT SoLuong FROM CHITIETHDB WHERE MaHDB = '{maHDBToUse}' AND MaSP = '{maSP}'";

            try
            {
                DataTable dtCheck = dataProcess.DataConnect(checkQuery);

                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    // Sản phẩm đã tồn tại, cập nhật số lượng
                    int existingQuantity = Convert.ToInt32(dtCheck.Rows[0]["SoLuong"]);
                    int newQuantity = existingQuantity + soLuong; // Cộng thêm số lượng mới

                    decimal thanhTien = (gia - (gia * khuyenMai / 100)) * newQuantity;

                    // Cập nhật số lượng và thành tiền
                    string updateQuery = $"UPDATE CHITIETHDB SET SoLuong = {newQuantity}, ThanhTien = {thanhTien}, KhuyenMai = {khuyenMai} WHERE MaHDB = '{maHDBToUse}' AND MaSP = '{maSP}'";
                    dataProcess.ExecuteQuery(updateQuery);
                    MessageBox.Show("Cập nhật số lượng sản phẩm thành công.");
                }
                else
                {
                    // Sản phẩm chưa tồn tại, thêm mới vào bảng
                    decimal thanhTien = (gia - (gia * khuyenMai / 100)) * soLuong;
                    string insertQuery = $"INSERT INTO CHITIETHDB (MaHDB, MaSP, SoLuong, ThanhTien, KhuyenMai) VALUES ('{maHDBToUse}', '{maSP}', {soLuong}, {thanhTien}, {khuyenMai})";
                    dataProcess.ExecuteQuery(insertQuery);
                    MessageBox.Show("Thêm sản phẩm thành công.");
                }

                // Tải lại chi tiết hóa đơn để cập nhật dữ liệu mới
                LoadChiTiet(maHDBToUse);
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hoặc cập nhật sản phẩm vào hóa đơn: {ex.Message}");
            }
        }
        private void UpdateTongTien(string maHDB, decimal tongTien)
        {
            // Câu truy vấn để cập nhật tổng tiền vào bảng HOADONBAN
            string updateQuery = $"UPDATE HOADONBAN SET TongTien = {tongTien} WHERE MaHDB = '{maHDB}'";

            try
            {
                dataProcess.ExecuteQuery(updateQuery); // Thực hiện câu lệnh cập nhật
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tổng tiền vào hóa đơn: {ex.Message}");
            }
        }
        public void CheckAndAddChiTietHoaDon(string maHDB)
        {
            // Kiểm tra xem mã hóa đơn đã tồn tại trong bảng HOADONNHAP chưa
            string checkHoaDonQuery = $"SELECT COUNT(*) FROM HOADONBAN WHERE MaHDB = '{maHDB}'";
            int hoaDonCount = 0;

            try
            {
                hoaDonCount = Convert.ToInt32(dataProcess.DataConnect(checkHoaDonQuery).Rows[0][0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra hóa đơn: {ex.Message}");
                return;
            }

            // Nếu mã hóa đơn tồn tại trong bảng HOADONNHAP
            if (hoaDonCount > 0)
            {
                // Kiểm tra xem mã hóa đơn đã tồn tại trong bảng CHITIETHDN chưa
                string checkChiTietQuery = $"SELECT COUNT(*) FROM CHITIETHDB WHERE MaHDB = '{maHDB}'";
                int chiTietCount = 0;

                try
                {
                    chiTietCount = Convert.ToInt32(dataProcess.DataConnect(checkChiTietQuery).Rows[0][0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi kiểm tra chi tiết hóa đơn: {ex.Message}");
                    return;
                }

                // Nếu mã hóa đơn chưa tồn tại trong bảng CHITIETHDN, lưu vào biến
                if (chiTietCount == 0)
                {
                    maHDTro = maHDB; // Lưu mã hóa đơn vào biến
                    MessageBox.Show("Mã hóa đơn đã được lưu và có sẵn trong bảng hóa đơn.");
                }
            }
            else
            {
                MessageBox.Show("Mã hóa đơn không tồn tại trong bảng hóa đơn.");
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
                txtThanhTien.ReadOnly = true;
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

                // Kiểm tra và thêm mã hóa đơn nếu cần
                CheckAndAddChiTietHoaDon(maHDB); // Gọi phương thức kiểm tra

                // Thêm sản phẩm vào bảng chi tiết hóa đơn
                AddChiTietHoaDon(maHDB, maSP, soLuong, gia, khuyenMai); // Sử dụng trực tiếp maHDN

                // Đặt lại trạng thái chỉnh sửa
                SetTextBoxReadOnly(true);
                cbbMaSP.Enabled = false;
                isEditing = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                // Kiểm tra xem có sản phẩm nào được chọn không
                if (dgvDanhSach.CurrentRow != null)
                {
                    // Cho phép chỉnh sửa số lượng và khuyến mãi
                    SetTextBoxReadOnly(false);
                    cbbMaSP.Enabled = false; // Không cho phép chỉnh sửa mã sản phẩm
                    txtThanhTien.ReadOnly = true;

                    // Lấy thông tin từ dòng hiện tại trong DataGridView
                    txtSoLuong.Text = dgvDanhSach.CurrentRow.Cells["SoLuong"].Value.ToString();
                    txtKhuyenMai.Text = dgvDanhSach.CurrentRow.Cells["KhuyenMai"].Value.ToString();

                    // Lưu giá trị gốc để so sánh
                    SoLuongCu = int.Parse(txtSoLuong.Text);
                    KhuyenMaiCu = decimal.Parse(txtKhuyenMai.Text);

                    isEditing = true; // Đánh dấu là đang ở chế độ chỉnh sửa
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm để sửa.");
                }
            }
            else
            {
                // So sánh giá trị gốc và giá trị mới
                int newSoLuong;
                decimal newKhuyenMai;

                if (!int.TryParse(txtSoLuong.Text, out newSoLuong) || newSoLuong <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ.");
                    return;
                }

                if (!decimal.TryParse(txtKhuyenMai.Text, out newKhuyenMai) || newKhuyenMai < 0 || newKhuyenMai > 100)
                {
                    MessageBox.Show("Khuyến mãi không hợp lệ. Vui lòng nhập số từ 0 đến 100.");
                    return;
                }

                // Kiểm tra sự thay đổi
                if (SoLuongCu == newSoLuong && KhuyenMaiCu == newKhuyenMai)
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.");
                    return;
                }

                // Hỏi người dùng có muốn lưu thay đổi không
                DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string maSP = dgvDanhSach.CurrentRow.Cells["MaSP"].Value.ToString();
                    decimal gia = GetGiaSP(maSP);
                    if (gia == 0)
                    {
                        MessageBox.Show("Không tìm thấy giá sản phẩm.");
                        return;
                    }

                    // Tính toán thành tiền mới
                    decimal thanhTien = (gia - (gia * newKhuyenMai / 100)) * newSoLuong;

                    // Cập nhật thông tin sản phẩm trong bảng chi tiết hóa đơn
                    string updateQuery = $"UPDATE CHITIETHDB SET SoLuong = {newSoLuong}, ThanhTien = {thanhTien}, KhuyenMai = {newKhuyenMai} WHERE MaHDB = '{maHDB}' AND MaSP = '{maSP}'";
                    dataProcess.ExecuteQuery(updateQuery);

                    MessageBox.Show("Thay đổi đã được lưu thành công.");
                    LoadChiTiet(maHDB); // Tải lại chi tiết sau khi cập nhật
                }

                // Đặt lại trạng thái chỉnh sửa
                SetTextBoxReadOnly(true);
                cbbMaSP.Enabled = false;
                isEditing = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có sản phẩm nào được chọn không
            if (dgvDanhSach.CurrentRow != null)
            {
                // Lấy mã sản phẩm từ dòng hiện tại
                string maSP = dgvDanhSach.CurrentRow.Cells["MaSP"].Value.ToString();

                // Xác nhận xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa sản phẩm khỏi bảng CHITIETHDB
                    string deleteQuery = $"DELETE FROM CHITIETHDB WHERE MaHDB = '{maHDB}' AND MaSP = '{maSP}'";
                    try
                    {
                        dataProcess.ExecuteQuery(deleteQuery); // Thực hiện câu lệnh xóa
                        MessageBox.Show("Sản phẩm đã được xóa thành công.");

                        // Tải lại chi tiết hóa đơn để cập nhật dữ liệu mới
                        LoadChiTiet(maHDB);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.");
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            // Khởi tạo Excel ứng dụng
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false; // Không hiển thị Excel

            // Tạo một Workbook mới
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Thêm tiêu đề cho hóa đơn
            worksheet.Cells[1, 1] = "Mã Hóa Đơn";
            worksheet.Cells[1, 2] = maHDB; // Mã hóa đơn
            worksheet.Cells[1, 3] = "Tổng Tiền";
            worksheet.Cells[1, 4] = txtTongTien.Text; // Tổng tiền

            // Thêm tiêu đề cho các cột
            worksheet.Cells[3, 1] = "Mã SP";
            worksheet.Cells[3, 2] = "Tên SP";
            worksheet.Cells[3, 3] = "Số Lượng";
            worksheet.Cells[3, 4] = "Đơn Giá";
            worksheet.Cells[3, 5] = "Khuyến Mãi";
            worksheet.Cells[3, 6] = "Thành Tiền";

            // Điền dữ liệu từ DataGridView
            for (int i = 0; i < dgvDanhSach.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDanhSach.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = dgvDanhSach.Rows[i].Cells[j].Value; // Dữ liệu bắt đầu từ hàng thứ 4
                }
            }

            // Định dạng cho các cột
            Excel.Range usedRange = worksheet.UsedRange;
            usedRange.Columns.AutoFit();

            // Lưu file Excel
            var saveFileDialog = new SaveFileDialog
            {
                FileName = "ChiTietHoaDonBan.xlsx",
                Filter = "Excel Files|*.xlsx",
                Title = "Save an Excel File"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFileDialog.FileName);
                MessageBox.Show("Xuất file Excel thành công!");
            }

            // Dọn dẹp
            workbook.Close(false);
            excelApp.Quit();

            // Giải phóng tài nguyên
            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReportChiTietHDB report = new FormReportChiTietHDB(maHDB);
            report.ShowDialog();
        }
    }
}



