using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace BTLBinh
{
    internal class Function
    {
        private DataProcess dataProcess;
        private DataGridView dgvDanhSach;
        private List<TextBox> textBoxes;
        private string tableName; // Tên bảng
        private string[] oldValues; // Biến lưu thông tin cũ
        private string oldImagePath; // Biến lưu đường dẫn hình ảnh cũ
        private PictureBox pbAnh; // Thêm biến PictureBox
        private DateTimePicker ngay; // Thêm biến DateTimePicker cho ô chọn ngày
        private DateTime oldNgayNhap; // Biến lưu ngày nhập cũ
        private DateTime oldNgayBan;  // Biến lưu ngày bán cũ
        private DateTime oldNgaySinh;
        private ComboBox cbbMaQue;
        private string oldMaQue;
        private ComboBox cbbMaNV;
        private string oldMaNV;
        private ComboBox cbbMaNCC;
        private string oldMaNCC;
        private CheckedListBox clbGioiTinh;

        public Function(DataProcess dataProcess, DataGridView dgv, List<TextBox> textBoxes, string tableName, PictureBox pictureBox, DateTimePicker dateTimePicker, ComboBox comboBox, ComboBox comboBox1 = null, CheckedListBox checkedListBox = null)
        {
            this.dataProcess = dataProcess;
            this.dgvDanhSach = dgv;
            this.textBoxes = textBoxes;
            this.tableName = tableName;
            oldValues = new string[textBoxes.Count];
            this.pbAnh = pictureBox; // Lưu PictureBox
            this.ngay = dateTimePicker; // Lưu DateTimePicker cho ngày
            this.cbbMaQue = comboBox;
            this.cbbMaNV = comboBox;
            this.cbbMaNCC = comboBox1;
            this.clbGioiTinh = checkedListBox;

            // Đăng ký sự kiện
            this.dgvDanhSach.CellClick += DgvDanhSach_CellClick;
        }

        public List<TextBox> TextBoxes { get => textBoxes; set => textBoxes = value; }

        private Dictionary<string, string> columnMappings = new Dictionary<string, string>
        {
            { "txtMaSp", "MaSP" },
            { "txtTenSp", "TenSP" },
            { "txtMaLoai", "MaLoai" },
            { "txtGiaNhap", "GiaNhap" },
            { "txtGiaBan", "GiaBan" },
            { "txtSoLuong", "SoLuong" },
            { "txtMaCD", "MaCongDung" },
            { "txtMaHDN", "MaHDN" },
            { "txtTongTien", "TongTien" },
            { "txtMaNCC", "MaNCC" },
            { "txtMaHDB", "MaHDB" },
            { "txtMaKH", "MaKH" },
            { "txtSDT", "SDT" },
            { "txtDiaChi", "DiaChi" },
            { "txtMaQue", "MaQue" },
            { "txtTenNCC","TenNCC" },
            { "txtTenNV", "TenNV" },
            { "txtGioiTinh", "GioiTinh" },
            { "txtMaNV", "MaNV" }
        };

        public Dictionary<string, string> ColumnMappings
        {
            get { return columnMappings; }
            set { columnMappings = value; }
        }

        public void DgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDanhSach.Rows[e.RowIndex];

                // Khởi tạo oldValues nếu chưa được khởi tạo
                if (oldValues == null)
                {
                    oldValues = new string[textBoxes.Count];
                }

                // Cập nhật TextBox với thông tin từ dòng đã chọn
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    // Lấy tên cột từ dictionary
                    string columnName = columnMappings.FirstOrDefault(x => x.Key == textBoxes[i].Name).Value;

                    if (columnName != null && dgvDanhSach.Columns.Contains(columnName))
                    {
                        // Lưu thông tin cũ và cập nhật TextBox
                        oldValues[i] = row.Cells[columnName].Value?.ToString(); // Lưu thông tin cũ
                        textBoxes[i].Text = oldValues[i]; // Cập nhật TextBox
                    }
                }

                // Cập nhật DateTimePicker với giá trị từ cột 'NgayNhap' hoặc 'NgayBan'
                if (dgvDanhSach.Columns.Contains("NgayNhap"))
                {
                    var ngayNhapValue = row.Cells["NgayNhap"].Value;
                    if (ngayNhapValue != DBNull.Value)
                    {
                        ngay.Value = Convert.ToDateTime(ngayNhapValue);
                        oldNgayNhap = ngay.Value; // Lưu ngày cũ
                    }
                }

                if (dgvDanhSach.Columns.Contains("NgayBan"))
                {
                    var ngayBanValue = row.Cells["NgayBan"].Value;
                    if (ngayBanValue != DBNull.Value)
                    {
                        ngay.Value = Convert.ToDateTime(ngayBanValue);
                        oldNgayBan = ngay.Value; // Lưu ngày cũ
                    }
                }

                if (dgvDanhSach.Columns.Contains("NgaySinh"))
                {
                    var ngaySinhValue = row.Cells["NgaySinh"].Value;
                    if (ngaySinhValue != DBNull.Value)
                    {
                        ngay.Value = Convert.ToDateTime(ngaySinhValue);
                        oldNgaySinh = ngay.Value; // Lưu ngày cũ
                    }
                }
                if (tableName == "HOADONNHAP" || tableName == "HOADONBAN")
                {
                    if (dgvDanhSach.Columns.Contains("MaNV"))
                    {
                        var maNVValue = row.Cells["MaNV"].Value;
                        if (maNVValue != null && maNVValue != DBNull.Value)
                        {
                            string maNV = maNVValue.ToString();
                            bool found = false;

                            foreach (ComboBoxItem item in cbbMaNV.Items)
                            {
                                if (item.Value == maNV)
                                {
                                    cbbMaNV.SelectedItem = item;
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                MessageBox.Show($"Không tìm thấy mã nhân viên: {maNV}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Giá trị mã nhân viên không hợp lệ.");
                        }
                    }
                    if (dgvDanhSach.Columns.Contains("MaNCC"))
                    {
                        var maNCCValue = row.Cells["MaNCC"].Value;
                        if (maNCCValue != null && maNCCValue != DBNull.Value)
                        {
                            string maNCC = maNCCValue.ToString();
                            bool found = false;

                            foreach (ComboBoxItem item in cbbMaNCC.Items)
                            {
                                if (item.Value == maNCC)
                                {
                                    cbbMaNCC.SelectedItem = item;
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                MessageBox.Show($"Không tìm thấy mã nhà cung cấp: {maNCC}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Giá trị mã nhà cung cấp không hợp lệ.");
                        }
                    }
                }


                // Cập nhật ComboBox với mã quê
                if (dgvDanhSach.Columns.Contains("MaQue"))
                {
                    var maQueValue = row.Cells["MaQue"].Value; // Lấy giá trị từ cột MaQue
                    if (maQueValue != null && maQueValue != DBNull.Value)
                    {
                        string maQue = maQueValue.ToString(); // Chuyển giá trị thành chuỗi
                        bool found = false; // Biến để kiểm tra nếu tìm thấy mã quê

                        // Lặp qua từng mục trong ComboBox để tìm mã quê tương ứng
                        foreach (ComboBoxItem item in cbbMaQue.Items)
                        {
                            if (item.Value == maQue) // Kiểm tra giá trị của item
                            {
                                cbbMaQue.SelectedItem = item; // Chọn mục trong ComboBox
                                found = true; // Đánh dấu đã tìm thấy
                                break; // Thoát khỏi vòng lặp
                            }
                        }

                        // Nếu không tìm thấy mã quê, hiển thị thông báo
                        if (!found)
                        {
                            MessageBox.Show($"Không tìm thấy mã quê: {maQue}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá trị mã quê không hợp lệ.");
                    }
                }

                if (dgvDanhSach.Columns.Contains("GioiTinh"))
                {
                    var gioiTinhValue = row.Cells["GioiTinh"].Value;
                    if (gioiTinhValue != null)
                    {
                        string gioiTinh = gioiTinhValue.ToString();

                        // Đặt tất cả các mục trong CheckedListBox về trạng thái không được chọn
                        for (int i = 0; i < clbGioiTinh.Items.Count; i++)
                        {
                            clbGioiTinh.SetItemChecked(i, false);
                        }

                        // Tích vào checkbox tương ứng với giá trị giới tính
                        if (gioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                        {
                            clbGioiTinh.SetItemChecked(0, true); // Giả sử "Nam" là mục đầu tiên
                        }
                        else if (gioiTinh.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
                        {
                            clbGioiTinh.SetItemChecked(1, true); // Giả sử "Nữ" là mục thứ hai
                        }
                    }
                }


                // Cập nhật hình ảnh nếu cột hình ảnh tồn tại
                if (dgvDanhSach.Columns.Contains("HinhAnh"))
                {
                    oldImagePath = row.Cells["HinhAnh"].Value?.ToString(); // Lấy đường dẫn hình ảnh
                    if (!string.IsNullOrWhiteSpace(oldImagePath))
                    {
                        Image img = null;
                        try
                        {
                            img = Image.FromFile(oldImagePath); // Đọc hình ảnh từ đường dẫn
                            pbAnh.Image?.Dispose(); // Giải phóng hình ảnh cũ
                            pbAnh.Image = img; // Hiển thị hình ảnh trong PictureBox
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Không thể hiển thị hình ảnh: {ex.Message}");
                        }
                    }
                }
            }
        }
        public void LoadDataSanPham()
        {
            // Lấy dữ liệu sản phẩm từ cơ sở dữ liệu
            DataTable products = dataProcess.GetDanhMuc(); // Giả sử phương thức này trả về một DataTable chứa tất cả sản phẩm
            dgvDanhSach.DataSource = products;

            // Tạo một cột mới để lưu số lượng nếu chưa có
            if (!products.Columns.Contains("SoLuong"))
            {
                products.Columns.Add("SoLuong", typeof(int));
            }

            // Tính toán số lượng sản phẩm cho từng sản phẩm
            foreach (DataRow row in products.Rows)
            {
                string productCode = row["MaSP"].ToString(); // Giả sử "MaSP" là mã sản phẩm
                int quantity = CalculateAndUpdateProductQuantity(productCode); // Gọi phương thức tính số lượng
                row["SoLuong"] = quantity; // Cập nhật cột số lượng
            }

            dgvDanhSach.Refresh(); // Làm mới DataGridView để hiển thị dữ liệu mớ
        }

        public void Add(string imagePath = null)
        {
            // Kiểm tra xem tất cả các TextBox có giá trị hợp lệ không
            if (textBoxes.Any(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                MessageBox.Show("Vui lòng điền tất cả các trường.");
                return;
            }

            // Tạo câu lệnh INSERT với tên cột rõ ràng
            string query = $"INSERT INTO {tableName} (";
            string values = "VALUES (";

            for (int i = 0; i < textBoxes.Count; i++) // Bắt đầu từ 0 để đảm bảo tất cả các cột được đưa vào
            {
                string columnName;
                if (columnMappings.TryGetValue(textBoxes[i].Name, out columnName))
                {
                    query += $"{columnName}, "; // Thêm tên cột
                    values += $"N'{textBoxes[i].Text}', "; // Thêm giá trị
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy tên cột cho {textBoxes[i].Name}");
                    return; // Kết thúc nếu không tìm thấy tên cột
                }
            }

            if (tableName == "NHANVIEN")
            {
                if (clbGioiTinh.Items.Count > 0)
                {
                    string gioiTinh = "";
                    if (clbGioiTinh.GetItemChecked(0)) // Giả sử "Nam" là mục đầu tiên
                    {
                        gioiTinh = "Nam";
                    }
                    else if (clbGioiTinh.GetItemChecked(1)) // Giả sử "Nữ" là mục thứ hai
                    {
                        gioiTinh = "Nữ";
                    }

                    if (!string.IsNullOrEmpty(gioiTinh))
                    {
                        query += "GioiTinh, "; // Thêm cột giới tính
                        values += $"N'{gioiTinh}', "; // Thêm giá trị giới tính
                    }
                }
                if (cbbMaQue.SelectedItem != null)
                {
                    string maQueValue = (cbbMaQue.SelectedItem as ComboBoxItem)?.Value; // Lấy giá trị từ ComboBox
                    if (!string.IsNullOrEmpty(maQueValue))
                    {
                        query += "MaQue, "; // Giả sử tên cột cho mã quê là MaQue
                        values += $"N'{maQueValue}', "; // Thêm giá trị vào câu lệnh
                    }
                }
            }
            if (tableName == "HOADONNHAP")
            {
                if (cbbMaNV.SelectedItem != null)
                {
                    string maNVValue = (cbbMaNV.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNVValue))
                    {
                        query += "MaNV, ";
                        values += $"'{maNVValue}', ";
                    }
                }
            }
            if (tableName == "HOADONNHAP")
            {
                if (cbbMaNCC.SelectedItem != null)
                {
                    string maNCCValue = (cbbMaNCC.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNCCValue))
                    {
                        query += "MaNCC, ";
                        values += $"'{maNCCValue}', ";
                    }
                }
            }
            if (tableName == "HOADONBAN")
            {
                if (cbbMaNV.SelectedItem != null)
                {
                    string maNVValue = (cbbMaNV.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNVValue))
                    {
                        query += "MaNV, ";
                        values += $"'{maNVValue}', ";
                    }
                }
            }

            // Kiểm tra nếu DateTimePicker tồn tại và thêm cột ngày
            if (ngay != null)
            {
                if (tableName == "HOADONNHAP")
                {
                    query += "NgayNhap, ";
                    values += $"'{ngay.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}', ";
                }
                else if (tableName == "HOADONBAN")
                {
                    query += "NgayBan, ";
                    values += $"'{ngay.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}', ";
                }
                else if (tableName == "NHANVIEN")
                {
                    query += "NgaySinh, ";
                    values += $"'{ngay.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}', ";
                }
            }

            // Thêm đường dẫn hình ảnh nếu có
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                query += "HinhAnh, "; // Giả sử tên cột cho hình ảnh là HinhAnh
                values += $"'{imagePath}', ";
            }

            query = query.TrimEnd(',', ' ') + ") "; // Xóa dấu phẩy cuối
            values = values.TrimEnd(',', ' ') + ")"; // Xóa dấu phẩy cuối

            query += values; // Kết hợp câu lệnh

            dataProcess.ExecuteQuery(query);
            LoadData();
        }

        public void Update(string imagePath = null)
        {
            if (textBoxes.Any(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                MessageBox.Show("Vui lòng điền tất cả các thông tin");
                return;
            }

            string query = $"UPDATE {tableName} SET ";
            bool hasChanged = false; // Biến kiểm tra sự thay đổi

            // Cập nhật các trường từ TextBox
            for (int i = 1; i < textBoxes.Count; i++)
            {
                string columnName;
                if (columnMappings.TryGetValue(textBoxes[i].Name, out columnName))
                {
                    query += $"{columnName} = N'{textBoxes[i].Text}', ";
                    hasChanged = true; // Đánh dấu có sự thay đổi
                }
            }

            // Kiểm tra và cập nhật ngày từ DateTimePicker
            if (tableName == "HOADONNHAP" && ngay.Value != oldNgayNhap)
            {
                query += $"NgayNhap = '{ngay.Value.ToString("yyyy-MM-dd")}', ";
                hasChanged = true; // Đánh dấu có sự thay đổi
            }
            else if (tableName == "HOADONBAN" && ngay.Value != oldNgayBan)
            {
                query += $"NgayBan = '{ngay.Value.ToString("yyyy-MM-dd")}', ";
                hasChanged = true; // Đánh dấu có sự thay đổi
            }
            else if (tableName == "NHANVIEN" && ngay.Value != oldNgaySinh)
            {
                query += $"NgaySinh = '{ngay.Value.ToString("yyyy-MM-dd")}', ";
                hasChanged = true; // Đánh dấu có sự thay đổi
            }

            // Cập nhật giá trị từ ComboBox
            if (tableName == "NHANVIEN")
            {
                if (clbGioiTinh.Items.Count > 0)
                {
                    string gioiTinh = "";
                    if (clbGioiTinh.GetItemChecked(0)) // Giả sử "Nam" là mục đầu tiên
                    {
                        gioiTinh = "Nam";
                    }
                    else if (clbGioiTinh.GetItemChecked(1)) // Giả sử "Nữ" là mục thứ hai
                    {
                        gioiTinh = "Nữ";
                    }

                    if (!string.IsNullOrEmpty(gioiTinh))
                    {
                        query += $"GioiTinh = N'{gioiTinh}',"; // Thêm cột giới tính
                        hasChanged =true;
                    }
                }
                if (cbbMaQue.SelectedItem != null)
                {
                    string maQueValue = (cbbMaQue.SelectedItem as ComboBoxItem)?.Value; // Lấy giá trị từ ComboBox
                    if (!string.IsNullOrEmpty(maQueValue) && maQueValue != oldMaQue) // So sánh với giá trị cũ
                    {
                        query += $"MaQue = '{maQueValue}', "; // Giả sử tên cột cho mã quê là MaQue
                        hasChanged = true; // Đánh dấu có sự thay đổi
                    }
                }
            }
            // ComboBox ma nhan vien HDN
            if (tableName == "HOADONNHAP")
            {
                if (cbbMaNV.SelectedItem != null)
                {
                    string maNVValue = (cbbMaNV.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNVValue) && maNVValue != oldMaNV)
                    {
                        query += $"MaNV = '{maNVValue}', ";
                        hasChanged = true;
                    }
                }
            }

            // ComboBox ma nha cung cap HDN
            if (tableName == "HOADONNHAP")
            {
                if (cbbMaNCC.SelectedItem != null)
                {
                    string maNCCValue = (cbbMaNCC.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNCCValue) && maNCCValue != oldMaNCC)
                    {
                        query += $"MaNCC = '{maNCCValue}', ";
                        hasChanged = true;
                    }
                }
            }
            // ComboBox ma NV HDB
            if (tableName == "HOADONBAN")
            {
                if (cbbMaNV.SelectedItem != null)
                {
                    string maNVValue = (cbbMaNV.SelectedItem as ComboBoxItem)?.Value;
                    if (!string.IsNullOrEmpty(maNVValue) && maNVValue != oldMaNV)
                    {
                        query += $"MaNV = '{maNVValue}', ";
                        hasChanged = true;
                    }
                }
            }
        

            // Kiểm tra và cập nhật đường dẫn hình ảnh nếu có
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                query += $"HinhAnh = '{imagePath}', "; // Giả sử tên cột cho hình ảnh là HinhAnh
                hasChanged = true; // Đánh dấu có sự thay đổi
            }

            // Loại bỏ dấu phẩy thừa nếu có
            if (query.EndsWith(", "))
            {
                query = query.Substring(0, query.Length - 2); // Cắt bỏ dấu phẩy thừa
            }

            // Thêm điều kiện WHERE dựa trên tên bảng
            if (tableName == "SANPHAM")
            {
                query += $" WHERE {columnMappings["txtMaSp"]} = '{textBoxes[0].Text}'"; // Điều kiện dựa trên ID
            }
            else if (tableName == "HOADONNHAP")
            {
                query += $" WHERE {columnMappings["txtMaHDN"]} = '{textBoxes[0].Text}'"; // Điều kiện dựa trên ID
            }
            else if (tableName == "HOADONBAN")
            {
                query += $" WHERE {columnMappings["txtMaHDB"]} = '{textBoxes[0].Text}'"; // Điều kiện dựa trên ID
            }
            else if (tableName == "NHANVIEN")
            {
                query += $" WHERE {columnMappings["txtMaNV"]} = '{textBoxes[0].Text}'"; // Điều kiện dựa trên ID
            }

            // Chạy câu lệnh chỉ nếu có sự thay đổi
            if (hasChanged)
            {
                dataProcess.ExecuteQuery(query);
                LoadData();
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào để lưu.");
            }
        }

        public void Delete(string query)
        {
            dataProcess.ExecuteQuery(query);
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                string query = $"SELECT * FROM {tableName}";
                DataTable dt = dataProcess.DataConnect(query);
                dgvDanhSach.DataSource = dt != null ? dt : throw new Exception("Không có dữ liệu!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }


        public void Search(string searchTerm)
        {
            string query = ""; // Khai báo biến query ở ngoài để có thể sử dụng sau này

            if (tableName == "SANPHAM")
            {
                // Tạo câu lệnh SELECT với điều kiện LIKE để tìm kiếm
                query = $"SELECT * FROM {tableName} WHERE TenSP LIKE '%{searchTerm}%' OR MaSP LIKE '%{searchTerm}%'";
            }
            if (tableName == "NHANVIEN")
            {
                query = $"SELECT * FROM {tableName} WHERE TenNV LIKE '%{searchTerm}%' OR MaNV LIKE '%{searchTerm}%'";
            }

            // Gọi LoadData với truy vấn tìm kiếm
            if (!string.IsNullOrWhiteSpace(query)) // Kiểm tra nếu query không rỗng
            {
                DataTable dt = dataProcess.DataConnect(query);

                // Cập nhật DataGridView với dữ liệu tìm được
                if (dt != null && dt.Rows.Count > 0) // Kiểm tra nếu có dữ liệu
                {
                    dgvDanhSach.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào.");
                }
            }
            else
            {
                MessageBox.Show("Truy vấn không hợp lệ.");
            }
        }

        public string[] GetOldValues()
        {
            return oldValues; // Phương thức để truy cập thông tin cũ
        }

        public void SetOldValues(List<string> newValues, string newImagePath)
        {
            // Cập nhật các giá trị cũ với các giá trị mới
            oldValues = newValues.ToArray();
            oldImagePath = newImagePath;
        }

        public void ClearOldValues()
        {
            oldValues = null; // Giải phóng bộ nhớ
        }

        public string GetOldImagePath()
        {
            return oldImagePath; // Phương thức để truy cập đường dẫn hình ảnh cũ
        }

        public event Action<string> ImageSelected;

        private void OnImageSelected(string imagePath)
        {
            ImageSelected?.Invoke(imagePath);
        }
        public DateTime GetOldNgayNhap()
        {
            return oldNgayNhap;
        }
        public DateTime GetOldNgayBan()
        {
            return oldNgayBan;
        }
        public string GetOldMaQue()
        {
            return oldMaQue;
        }
        public string GetOldMaNV()
        {
            return oldMaNV;
        }
        public string GetOldMaNCC()
        {
            return oldMaNCC;
        }
        public bool CheckExists(string tableName, string columnName, string value)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = '{value}'";
            DataTable dt = dataProcess.DataConnect(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0; // Trả về true nếu tồn tại
            }
            return false; // Trả về false nếu không tồn tại
        }
        public class ComboBoxItem
        {
            public string Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text; // Để hiển thị văn bản trong ComboBox
            }
        }
        public void LoadMaQue(ComboBox comboBox)
        {
            try
            {
                string query = "SELECT MaQue, TenQue FROM QUE"; // Thay thế bằng tên bảng và cột phù hợp
                DataTable dt = dataProcess.DataConnect(query);

                comboBox.Items.Clear(); // Xóa các mục cũ trong ComboBox

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Kết hợp mã quê và tên quê thành một chuỗi
                        string displayText = $"{row["MaQue"]} - {row["TenQue"]}";

                        // Thêm vào ComboBox
                        comboBox.Items.Add(new ComboBoxItem
                        {
                            Value = row["MaQue"].ToString(), // Lưu mã quê
                            Text = displayText // Lưu chuỗi hiển thị
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải mã quê: {ex.Message}");
            }
        }
        public void LoadMaNV(ComboBox comboBox)
        {
            try
            {
                string query = "SELECT MaNV, TenNV FROM NHANVIEN";
                DataTable dt = dataProcess.DataConnect(query);

                comboBox.Items.Clear();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string displayText = $"{row["MaNV"]} - {row["TenNV"]}";

                        comboBox.Items.Add(new ComboBoxItem
                        {
                            Value = row["MaNV"].ToString(),
                            Text=displayText
                        });
                    }
                }
            }
            catch (Exception ex)  
            {
                MessageBox.Show($"Lỗi khi tải mã nhân viên: {ex.Message}");
            }
        }
        public void LoadMaNCC(ComboBox comboBox)
        {
            try
            {
                string query = "SELECT MaNCC, TenNCC FROM NHACUNGCAP";
                DataTable dt = dataProcess.DataConnect(query);

                comboBox.Items.Clear();

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string displayText = $"{row["MaNCC"]} - {row["TenNCC"]}";

                        comboBox.Items.Add(new ComboBoxItem
                        {
                            Value = row["MaNCC"].ToString(),
                            Text = displayText
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải mã nhà cung cấp: {ex.Message}");
            }
        }
        public DataTable GetChiTietHD(string maHDN)
        {
            // Kiểm tra mã hóa đơn không rỗng
            if (string.IsNullOrWhiteSpace(maHDN))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ.");
                return null;
            }

            // Câu truy vấn để lấy chi tiết hóa đơn
            string query = $"SELECT * FROM CHITIETHDN WHERE MaHDN = '{maHDN}'";

            try
            {
                // Thực hiện truy vấn và trả về DataTable
                DataTable dt = dataProcess.DataConnect(query);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}");
                return null;
            }
        }
        //---------------------------------------------------------------------------------------------------------------//
        public string GetNextMa(string tableName, string columnName, string prefix)
        {
            string lastMa = ""; // Biến chứa mã cuối cùng
            string query = $"SELECT TOP 1 {columnName} FROM {tableName} ORDER BY {columnName} DESC"; // Truy vấn mã cuối cùng

            var result = dataProcess.DataConnect(query);

            if (result != null && result.Rows.Count > 0)
            {
                lastMa = result.Rows[0][columnName].ToString(); // Lấy mã cuối cùng
            }

            // Tăng mã lên 1
            if (!string.IsNullOrEmpty(lastMa))
            {
                // Lấy phần số từ mã
                if (int.TryParse(lastMa.Substring(prefix.Length), out int number))
                {
                    number++; // Tăng số lên 1
                    return $"{prefix}{number:D3}"; // Trả về mã mới, định dạng 3 chữ số
                }
                else
                {
                    throw new FormatException("Mã không đúng định dạng.");
                }
            }

            // Nếu không có mã nào, trả về mã đầu tiên
            return $"{prefix}001"; // Trả về mã đầu tiên nếu không có mã nào
        }
        public int CalculateAndUpdateProductQuantity(string productCode)
        {
            // Truy vấn tất cả chi tiết hóa đơn nhập cho sản phẩm
            DataTable purchaseDetails = dataProcess.GetChiTietHDTinhTong("CHITIETHDN", "MaSP", productCode);

            // Truy vấn tất cả chi tiết hóa đơn bán cho sản phẩm
            DataTable salesDetails = dataProcess.GetChiTietHDTinhTong("CHITIETHDB", "MaSP", productCode);

            // Tính tổng số lượng đã mua
            int totalPurchased = purchaseDetails.AsEnumerable()
                .Sum(row => row.Field<int>("SoLuong"));

            // Tính tổng số lượng đã bán
            int totalSold = salesDetails.AsEnumerable()
                .Sum(row => row.Field<int>("SoLuong"));

            // Tính số lượng còn lại
            int remainingQuantity = totalPurchased - totalSold;

            // Cập nhật số lượng còn lại vào cơ sở dữ liệu
            UpdateProductQuantityInDatabase(productCode, remainingQuantity);

            // Trả về số lượng còn lại
            return remainingQuantity; // Trả về số lượng đã tính toán
        }

        // Phương thức cập nhật số lượng sản phẩm vào cơ sở dữ liệu
        private void UpdateProductQuantityInDatabase(string productCode, int quantity)
        {
            string query = $"UPDATE SANPHAM SET SoLuong = {quantity} WHERE MaSP = '{productCode}'";

            try
            {
                dataProcess.ExecuteQuery(query); // Thực hiện truy vấn cập nhật
                Console.WriteLine($"Cập nhật số lượng cho sản phẩm {productCode} thành công: {quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật số lượng sản phẩm: {ex.Message}");
            }
        }
    }
}