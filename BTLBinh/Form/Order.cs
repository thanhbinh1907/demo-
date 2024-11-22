using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    public partial class Order : Form
    {
        private DataProcess dataProcess;
        // public DataGridView dgvDanhSach;
        MenuItems menu;
        string diaChiKH = "";
        string tenNhanVien;
        string maNhanVien;

        Boolean checkBan01 = false;
        Boolean checkBan02 = false;
        Boolean checkBan03 = false;
        Boolean checkBan04 = false;
        Boolean checkBan05 = false;
        Boolean checkBan06 = false;
        Boolean checkBan07 = false;
        Boolean checkBan08 = false;
        Boolean checkBan09 = false;

        HoaDon hoaDon01;
        HoaDon hoaDon02;
        HoaDon hoaDon03;
        HoaDon hoaDon04;
        HoaDon hoaDon05;
        HoaDon hoaDon06;
        HoaDon hoaDon07;
        HoaDon hoaDon08;
        HoaDon hoaDon09;

        private List<OrderItem> hoaDonList1 = new List<OrderItem>();
        private List<OrderItem> hoaDonList2 = new List<OrderItem>();
        private List<OrderItem> hoaDonList3 = new List<OrderItem>();
        private List<OrderItem> hoaDonList4 = new List<OrderItem>();
        private List<OrderItem> hoaDonList5 = new List<OrderItem>();
        private List<OrderItem> hoaDonList6 = new List<OrderItem>();
        private List<OrderItem> hoaDonList7 = new List<OrderItem>();
        private List<OrderItem> hoaDonList8 = new List<OrderItem>();
        private List<OrderItem> hoaDonList9 = new List<OrderItem>();

        private List<string> ProductNames;  // Danh sách tên sản phẩm từ FormMenu
        private List<int> ProductQuantities; // Danh sách số lượng sản phẩm từ FormMenu


        public Order()
        {
            InitializeComponent();


            this.FormClosed += (sender, e) => Application.Exit();

            // Dùng cho MenuStrip 
            MenuStripHelper.ApplyHoverEffect(danhMucToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhapHangToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(banHangToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhanVienToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(nhaCungCapToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(tienIchToolStripMenuItem);
            MenuStripHelper.ApplyHoverEffect(troGiupToolStripMenuItem);

            hoaDon01 = new HoaDon(null, null, null, null);
            hoaDon02 = new HoaDon(null, null, null, null);
            hoaDon03 = new HoaDon(null, null, null, null);
            hoaDon04 = new HoaDon(null, null, null, null);
            hoaDon05 = new HoaDon(null, null, null, null);
            hoaDon06 = new HoaDon(null, null, null, null);
            hoaDon07 = new HoaDon(null, null, null, null);
            hoaDon07 = new HoaDon(null, null, null, null);
            hoaDon08 = new HoaDon(null, null, null, null);
            hoaDon09 = new HoaDon(null, null, null, null);

            dataProcess = new DataProcess();

            // Khởi tạo datagridview
            SetupDataGridView();

            // Thiết lập ComboBox chỉ có thể chọn giá trị từ danh sách
            cbPayment.DropDownStyle = ComboBoxStyle.DropDownList;

            cbPayment.Items.Add("Tiền mặt");
            cbPayment.Items.Add("Chuyển khoản");

            txtMaNhanVien.Text = User.CurrentEmployeeId;

            tenNhanVien = User.CurrentEmployeeName;
            maNhanVien = User.CurrentEmployeeId;
            lbUser.Text = "Mã NV: " + maNhanVien + " - " + "Tên NV: " + tenNhanVien;

        }

        private void Order_Load(object sender, EventArgs e)
        {

            dgv_hoaDon.CellEndEdit += dgv_hoaDon_CellEndEdit_1;
        }

        private void danhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void nhapHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tạo một thể hiện mới của Form2`1
            Form2 form2 = new Form2();

            // Hiển thị Form2
            form2.Show();

            // Nếu bạn muốn đóng Form1 sau khi mở Form2, hãy thêm dòng sau:
            this.Hide();
        }

        private void banHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Show();

            this.Hide();
        }

        private void nhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();

            form5.Show();
        }

        private void nhaCungCapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            form4.Show();
        }

        //-------------------------------------------------------------------------------------------
        // Phần dưới này là các phương thức

        private Dictionary<string, string> columnMappings = new Dictionary<string, string>
        {
            { "SP001", "Cà phê đen" },
            { "SP002", "Cà phê sữa" },
            { "SP003", "Trà sữa trân châu" },
            { "SP004", "Sinh tố dâu" },
            { "SP005", "Nước cam ép" },
            { "SP006", "Soda chanh" },
            { "SP007", "Bánh ngọt dâu" },
            { "SP008", "Bánh mì thịt" },
            { "SP009", "Pizza phô mai" },
            { "SP010", "Salad rau củ" },
            { "SP011", "Hamburger bò" },
            { "SP012", "Kem dừa" },
            { "SP013", "Nước ép táo" },
            { "SP014", "Cà phê đá xay" },
            { "SP015", "Trà sữa matcha" }
        };

        private void SetupDataGridView()
        {
            // Xóa tất cả các cột và hàng hiện tại nếu có
            dgv_hoaDon.Columns.Clear();
            dgv_hoaDon.Rows.Clear();

            // Thêm các cột vào DataGridView
            dgv_hoaDon.Columns.Add("SanPham", "Sản phẩm");
            dgv_hoaDon.Columns.Add("SoLuong", "Số lượng");
            dgv_hoaDon.Columns.Add("ThanhTien", "Thành tiền");
            dgv_hoaDon.Columns.Add("KhuyenMai", "Khuyến mãi");

        }

        private void ResetControlsInGroupBox(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Name == "txtMaNhanVien")
                    {
                        textBox.Text = User.CurrentEmployeeId; // Đặt giá trị của txtmaNhanVien là User.id
                    }
                    else
                    {
                        textBox.Text = string.Empty; // Đặt giá trị của các TextBox khác là rỗng
                    }
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now; // Đặt giá trị của DateTimePicker là ngày hiện tại
                }
                else if (control is DataGridView dataGridView)
                {
                    dataGridView.Rows.Clear(); // Xóa tất cả các hàng trong DataGridView
                }
            }
        }


        private void buttonColor(Button btn)
        {
            btn.BackColor = Color.FromArgb(255, 128, 128);
            btn.ForeColor = Color.Snow;
        }

        private void ResetButtonColor(Button btn)
        {
            btn.BackColor = Color.FromArgb(224, 224, 224);
            btn.ForeColor = Color.Black;
        }

        // Phương thức dùng để hiển thị dữ liệu hóa đơn lên các control
        private void LoadHoaDon(HoaDon hoaDon, List<OrderItem> hoaDonList)
        {
            txtMaHoaDon.Text = hoaDon.MaHoaDon;
            txtMaNhanVien.Text = User.CurrentEmployeeId;
            txtSoDienThoai.Text = hoaDon.SoDienThoai;

            dgv_hoaDon.Rows.Clear();  // Xóa tất cả các hàng hiện có trong DataGridView

            foreach (var item in hoaDonList)
            {
                // Thêm dòng vào DataGridView
                dgv_hoaDon.Rows.Add(item.Name, item.SoLuong, item.ThanhTien, item.KhuyenMai);
            }
        }

        private void UpdateHoaDon(HoaDon hoaDon)
        {
            hoaDon.MaHoaDon = txtMaHoaDon.Text;
            hoaDon.MaNhanVien = User.CurrentEmployeeId;
            hoaDon.SoDienThoai = txtSoDienThoai.Text;
        }

        private void DeleteHoaDon(HoaDon hoaDon)
        {
            hoaDon.MaHoaDon = null;
            hoaDon.MaNhanVien = null;
            hoaDon.SoDienThoai = null;

            txtMaHoaDon.Text = "";
            txtMaNhanVien.Text = User.CurrentEmployeeId;
            txtSoDienThoai.Text = "";
            SetupDataGridView();
        }

        // Hàm để lấy giá tiền từ cơ sở dữ liệu dựa trên tên sản phẩm
        private decimal GetGiaTien(string tenSanPham)
        {
            // Tìm mã sản phẩm từ tên sản phẩm trong Dictionary
            string maSanPham = columnMappings.FirstOrDefault(x => x.Value == tenSanPham).Key;

            if (string.IsNullOrEmpty(maSanPham))
            {
                // Nếu không tìm thấy mã sản phẩm tương ứng với tên sản phẩm, trả về giá trị 0
                return 0;
            }

            // Câu truy vấn SQL để lấy giá tiền từ cơ sở dữ liệu, sử dụng mã sản phẩm
            string query = $"SELECT GiaBan FROM SANPHAM WHERE MaSP = '{maSanPham}'";

            // Sử dụng DataConnect để thực hiện câu truy vấn
            DataTable dt = dataProcess.DataConnect(query);

            if (dt.Rows.Count > 0)
            {
                // Nếu tìm thấy kết quả, chuyển đổi giá trị thành decimal
                return Convert.ToDecimal(dt.Rows[0]["GiaBan"]);
            }
            else
            {
                // Nếu không tìm thấy, trả về 0
                return 0;
            }
        }

        private void CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            // Đảm bảo vòng lặp này có biến 'row' là đúng
            foreach (DataGridViewRow row in dgv_hoaDon.Rows)
            {
                // Kiểm tra xem dòng có bị bỏ trống hay không
                if (row.IsNewRow) continue;

                decimal thanhTien = 0;
                string thanhTienValue = row.Cells[2].Value?.ToString();

                if (decimal.TryParse(thanhTienValue, out thanhTien))
                {

                    decimal khuyenMai = 0;
                    string khuyenMaiValue = row.Cells[3].Value?.ToString();

                    if (decimal.TryParse(khuyenMaiValue, out khuyenMai))
                    {

                        decimal thanhTienSauKhuyenMai = thanhTien * (1 - khuyenMai / 100);
                        totalAmount += thanhTienSauKhuyenMai;
                    }
                    else
                    {
                        totalAmount += thanhTien;
                    }
                }
                else
                {
                }
            }

            // Cập nhật tổng tiền vào TextBox
            txtTongTien.Text = totalAmount.ToString("N0");

        }

        private void SaveDataToList(List<OrderItem> hoaDonList)
        {
            hoaDonList.Clear(); // Xóa danh sách hiện tại để tránh trùng dữ liệu

            foreach (DataGridViewRow row in dgv_hoaDon.Rows)
            {
                // Kiểm tra nếu dòng không phải là dòng mới (dòng trống)
                if (row.IsNewRow) continue;

                // Lấy thông tin từ các cột
                string sanPham = row.Cells[0].Value?.ToString();
                int soLuong = 0;
                decimal thanhTien = 0;
                decimal khuyenMai = 0;

                // Kiểm tra nếu số lượng hợp lệ
                if (int.TryParse(row.Cells[1].Value?.ToString(), out soLuong))
                {
                    thanhTien = Convert.ToDecimal(row.Cells[2].Value);
                }

                // Kiểm tra nếu khuyến mãi hợp lệ
                if (decimal.TryParse(row.Cells[3].Value?.ToString(), out khuyenMai))
                {
                    // Tạo đối tượng OrderItem từ dữ liệu của dòng
                    var orderItem = new OrderItem
                    {
                        Name = sanPham,
                        SoLuong = soLuong,
                        ThanhTien = thanhTien,
                        KhuyenMai = khuyenMai
                    };

                    // Thêm đối tượng vào danh sách
                    hoaDonList.Add(orderItem);
                }
            }

            // Debug: In danh sách hoaDonList ra Console để kiểm tra
            foreach (var item in hoaDonList)
            {
                Console.WriteLine($"Sản phẩm: {item.Name}, Số lượng: {item.SoLuong}, Thành tiền: {item.ThanhTien}, Khuyến mãi: {item.KhuyenMai}");
            }
        }


        private void Payment_ButtonClicked(object sender, EventArgs e)
        {
            // Gọi phương thức SaveToCsdl và truyền địa chỉ khách hàng
            SaveToCsdl();
        }

        private void Cash_buttonClicked(object sender, EventArgs e)
        {
            // Gọi phương thức SaveToCsdl và truyền địa chỉ khách hàng
            SaveToCsdl();
        }

        private void SaveToCsdl()
        {
            var dataProcess = new DataProcess();
            string maHDB = txtMaHoaDon.Text;
            string maNV = txtMaNhanVien.Text;
            string soDienThoai = txtSoDienThoai.Text;
            decimal tongTien;

            // Kiểm tra và chuyển đổi giá trị từ TextBox tổng tiền
            if (!decimal.TryParse(txtTongTien.Text, out tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ!");
                return;
            }
            DateTime ngayBan = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6C4ON0I\SQLEXPRESS;Initial Catalog=LTTQ;Integrated Security=True;"))
            {
                connection.Open();

                string maKH = "";

                // Kiểm tra mã khách hàng và thêm địa chỉ nếu cần
                string checkMaKHQuery = "SELECT MaKH FROM KHACHHANG WHERE SDT = @SDT";
                using (SqlCommand checkCmd = new SqlCommand(checkMaKHQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@SDT", soDienThoai);
                    var result = checkCmd.ExecuteScalar();

                    if (result != null) // Nếu tìm thấy khách hàng
                    {
                        maKH = result.ToString(); // Lấy mã khách hàng
                    }
                    else
                    {
                        // Thêm khách hàng mới nếu không tồn tại
                        string getMaxMaKHQuery = "SELECT TOP 1 MaKH FROM KHACHHANG ORDER BY MaKH DESC";
                        maKH = "KH001"; // Mã mặc định nếu chưa có khách hàng nào

                        using (SqlCommand getMaxCmd = new SqlCommand(getMaxMaKHQuery, connection))
                        {
                            var maxResult = getMaxCmd.ExecuteScalar();
                            if (maxResult != null)
                            {
                                string lastMaKH = maxResult.ToString();
                                int numberPart = int.Parse(lastMaKH.Substring(2));
                                maKH = "KH" + (numberPart + 1).ToString("D3");
                            }
                        }

                        // Kiểm tra xem địa chỉ có được nhập không
                        if (!string.IsNullOrEmpty(diaChiKH))
                        {
                            string insertKHQuery = "INSERT INTO KHACHHANG (MaKH, SDT, DiaChi) VALUES (@MaKH, @SDT, @DiaChi)";
                            using (SqlCommand insertCmd = new SqlCommand(insertKHQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@MaKH", maKH);
                                insertCmd.Parameters.AddWithValue("@SDT", soDienThoai);
                                insertCmd.Parameters.AddWithValue("@DiaChi", diaChiKH);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập địa chỉ cho khách hàng mới!");
                            return;
                        }
                    }
                }


                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Thêm vào bảng HOADONBAN
                    string insertHoaDonQuery = "INSERT INTO HOADONBAN (MaHDB, MaNV, MaKH, TongTien, NgayBan) " +
                                               "VALUES (@MaHDB, @MaNV, @MaKH, @TongTien, @NgayBan)";
                    using (SqlCommand cmd = new SqlCommand(insertHoaDonQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaHDB", maHDB);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@NgayBan", ngayBan);
                        cmd.ExecuteNonQuery();
                    }

                    // Thêm chi tiết hóa đơn vào CHITIETHDB
                    List<(string maSP, int soLuong, decimal thanhTien, float khuyenMai)> chiTietHDB = new List<(string, int, decimal, float)>();
                    foreach (DataGridViewRow row in dgv_hoaDon.Rows)
                    {
                        if (row.Cells["SoLuong"].Value != null && row.Cells["ThanhTien"].Value != null && row.Cells["KhuyenMai"].Value != null)
                        {
                            string tenSP = row.Cells["SanPham"].Value.ToString();
                            int soLuong;
                            decimal thanhTien;
                            float khuyenMai;

                            // Kiểm tra dữ liệu
                            if (!int.TryParse(row.Cells["SoLuong"].Value.ToString(), out soLuong) ||
                                !decimal.TryParse(row.Cells["ThanhTien"].Value.ToString(), out thanhTien) ||
                                !float.TryParse(row.Cells["KhuyenMai"].Value.ToString(), out khuyenMai))
                            {
                                MessageBox.Show("Dữ liệu trong DataGridView không hợp lệ!");
                                return;
                            }

                            // Kiểm tra sản phẩm trong Dictionary
                            if (columnMappings.ContainsValue(tenSP))
                            {
                                string maSP = columnMappings.FirstOrDefault(x => x.Value == tenSP).Key;
                                chiTietHDB.Add((maSP, soLuong, thanhTien, khuyenMai));
                            }
                            else
                            {
                                MessageBox.Show($"Sản phẩm '{tenSP}' không tồn tại trong hệ thống.");
                                return;
                            }
                        }
                    }

                    foreach (var item in chiTietHDB)
                    {
                        // Thêm chi tiết hóa đơn vào bảng CHITIETHDB
                        string insertChiTietQuery = "INSERT INTO CHITIETHDB (MaHDB, MaSP, SoLuong, ThanhTien, KhuyenMai) " +
                                                    "VALUES (@MaHDB, @MaSP, @SoLuong, @ThanhTien, @KhuyenMai)";
                        using (SqlCommand cmd = new SqlCommand(insertChiTietQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaHDB", maHDB);
                            cmd.Parameters.AddWithValue("@MaSP", item.maSP);
                            cmd.Parameters.AddWithValue("@SoLuong", item.soLuong);
                            cmd.Parameters.AddWithValue("@ThanhTien", item.thanhTien);
                            cmd.Parameters.AddWithValue("@KhuyenMai", item.khuyenMai);
                            cmd.ExecuteNonQuery();
                        }

                        // Trừ số lượng sản phẩm trong bảng SANPHAM
                        string updateSanPhamQuery = "UPDATE SANPHAM SET SoLuong = SoLuong - @SoLuong WHERE MaSP = @MaSP";
                        using (SqlCommand updateCmd = new SqlCommand(updateSanPhamQuery, connection, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@SoLuong", item.soLuong);
                            updateCmd.Parameters.AddWithValue("@MaSP", item.maSP);
                            updateCmd.ExecuteNonQuery();
                        }
                    }


                    transaction.Commit();
                    MessageBox.Show("Thanh toán thành công!");
                    SetupDataGridView();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra trong quá trình thanh toán: " + ex.Message);
                }
            }

            // Xóa hóa đơn nếu thanh toán thành công
            if (lbSoBan.Text == "Bàn số 01")
            {
                DeleteHoaDon(hoaDon01);
                checkBan01 = false;
                ResetButtonColor(btBan01);
            }
            else if (lbSoBan.Text == "Bàn số 02")
            {
                DeleteHoaDon(hoaDon02);
                checkBan02 = false;
                ResetButtonColor(btBan02);
            }
            else if (lbSoBan.Text == "Bàn số 03")
            {
                DeleteHoaDon(hoaDon03);
                checkBan03 = false;
                ResetButtonColor(btBan03);
            }
            else if (lbSoBan.Text == "Bàn số 04")
            {
                DeleteHoaDon(hoaDon04);
                checkBan04 = false;
                ResetButtonColor(btBan04);
            }
            else if (lbSoBan.Text == "Bàn số 05")
            {
                DeleteHoaDon(hoaDon05);
                checkBan05 = false;
                ResetButtonColor(btBan05);
            }
            else if (lbSoBan.Text == "Bàn số 06")
            {
                DeleteHoaDon(hoaDon06);
                checkBan06 = false;
                ResetButtonColor(btBan06);
            }
            else if (lbSoBan.Text == "Bàn số 07")
            {
                DeleteHoaDon(hoaDon07);
                checkBan07 = false;
                ResetButtonColor(btBan07);
            }
            else if (lbSoBan.Text == "Bàn số 08")
            {
                DeleteHoaDon(hoaDon08);
                checkBan08 = false;
                ResetButtonColor(btBan08);
            }
            else if (lbSoBan.Text == "Bàn số 09")
            {
                DeleteHoaDon(hoaDon09);
                checkBan09 = false;
                ResetButtonColor(btBan09);
            }
            diaChiKH = "";
        }

        private void menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (menu != null)
            {
                this.ProductNames = menu.ProductNames;  // Lấy danh sách tên sản phẩm
                this.ProductQuantities = menu.ProductQuantities; // Lấy danh sách số lượng sản phẩm
            }
            else
            {
                MessageBox.Show("menu rong");
            }

            // Gọi phương thức xử lý danh sách sau khi form đóng
            XuLyDanhSachTextBox();

        }

        private int GetAvailableProductQuantity(string productName)
        {
            // Truy vấn số lượng sản phẩm hiện có trong bảng SANPHAM
            int availableQuantity = 0;
            string query = "SELECT SoLuong FROM SANPHAM WHERE TenSP = @TenSP"; // Giả sử tên sản phẩm là TenSP trong bảng SANPHAM

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6C4ON0I\SQLEXPRESS;Initial Catalog=LTTQ;Integrated Security=True;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenSP", productName);
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        availableQuantity = Convert.ToInt32(result);
                    }
                }
            }

            return availableQuantity;
        }


        private void XuLyDanhSachTextBox()
        {
            if (ProductNames != null && ProductQuantities != null)
            {
                for (int i = 0; i < ProductNames.Count; i++)
                {
                    int quantity = ProductQuantities[i];
                    if (quantity > 0)
                    {
                        // Kiểm tra số lượng sản phẩm trong bảng SANPHAM
                        int availableQuantity = GetAvailableProductQuantity(ProductNames[i]);
                        if (availableQuantity < quantity)
                        {
                            MessageBox.Show($"Sản phẩm '{ProductNames[i]}' không đủ số lượng. Số lượng còn lại: {availableQuantity}");
                            return; // Nếu không đủ số lượng, thoát phương thức
                        }

                        // Kiểm tra xem sản phẩm đã có trong DataGridView chưa
                        bool productExists = false;
                        foreach (DataGridViewRow row in dgv_hoaDon.Rows)
                        {
                            if (row.Cells["SanPham"].Value != null && row.Cells["SanPham"].Value.ToString() == ProductNames[i])
                            {
                                // Nếu đã có, cộng thêm số lượng vào dòng hiện tại
                                int existingQuantity = Convert.ToInt32(row.Cells["SoLuong"].Value);
                                row.Cells["SoLuong"].Value = existingQuantity + quantity;

                                decimal price = GetGiaTien(ProductNames[i]) * (existingQuantity + quantity);
                                row.Cells["ThanhTien"].Value = price;

                                productExists = true;
                                break;
                            }
                        }

                        // Nếu sản phẩm chưa có, thêm dòng mới vào DataGridView
                        if (!productExists)
                        {
                            int index = dgv_hoaDon.Rows.Add(); // Thêm hàng mới và lấy chỉ số

                            // Thiết lập giá trị cho các ô trong hàng mới
                            dgv_hoaDon.Rows[index].Cells["SanPham"].Value = ProductNames[i]; // Cột Sản phẩm từ ProductNames
                            dgv_hoaDon.Rows[index].Cells["SoLuong"].Value = quantity; // Cột Số lượng từ ProductQuantities
                            decimal price = GetGiaTien(ProductNames[i]) * quantity;
                            dgv_hoaDon.Rows[index].Cells["ThanhTien"].Value = price; // Cột Thành tiền
                            dgv_hoaDon.Rows[index].Cells["KhuyenMai"].Value = "0"; // Cột Khuyến mãi, giả định là 0
                        }

                        // Gọi hàm tính tổng tiền sau khi cập nhật "Thành tiền"
                        CalculateTotalAmount();
                    }
                }
            }
            else
            {
                Console.WriteLine("Danh sách giá trị trống hoặc chưa được khởi tạo.");
            }
        }


        // Hàm mở dialog để nhập địa chỉ khách hàng
        private void PromptForAddress()
        {
            string diaChi = Microsoft.VisualBasic.Interaction.InputBox("Nhập địa chỉ khách hàng:", "Thông Tin Khách Hàng");
            diaChiKH = diaChi;
        }


        private string kiemTraKhachHang(string soDienThoai)
        {

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6C4ON0I\SQLEXPRESS;Initial Catalog=LTTQ;Integrated Security=True;"))
            {
                connection.Open();
                string checkMaKHQuery = "SELECT COUNT(*) FROM KHACHHANG WHERE SDT = @soDienThoai";
                using (SqlCommand checkCmd = new SqlCommand(checkMaKHQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@soDienThoai", soDienThoai);
                    int count = (int)checkCmd.ExecuteScalar();

                    // Nếu mã khách hàng không tồn tại, yêu cầu nhập địa chỉ
                    if (count == 0)
                    {
                        PromptForAddress(); // Hàm nhập địa chỉ
                        if (string.IsNullOrEmpty(diaChiKH))
                        {
                            MessageBox.Show("Bạn chưa nhập địa chỉ khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                    }
                }
            }

            return diaChiKH;
        }

        public void logout()
        {
            maNhanVien = "";
            tenNhanVien = "Chưa đăng nhập";
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra độ dài là 10 và chỉ chứa số
            if (phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit))
            {
                return true;
            }

            return false;
        }


        //------------------------------------------------------------------------------------------

        private void txtLuuThongTin_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có muốn lưu thông tin không?", // Nội dung câu hỏi
                "Xác nhận",                        // Tiêu đề của hộp thoại
                MessageBoxButtons.YesNo,            // Các nút Yes/No
                MessageBoxIcon.Question);           // Biểu tượng câu hỏi

            // Kiểm tra xem người dùng chọn Yes hay No
            if (dialogResult == DialogResult.Yes)
            {
                // Nếu chọn Yes, thực hiện lưu thông tin
                if (lbSoBan.Text == "Bàn số 01" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon01);
                    SaveDataToList(hoaDonList1);
                    checkBan01 = true;
                    buttonColor(btBan01);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 02" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon02);
                    SaveDataToList(hoaDonList2);
                    checkBan02 = true;
                    buttonColor(btBan02);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 03" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon03);
                    SaveDataToList(hoaDonList3);
                    checkBan03 = true;
                    buttonColor(btBan03);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 04" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon04);
                    SaveDataToList(hoaDonList4);
                    checkBan04 = true;
                    buttonColor(btBan04);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 05" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon05);
                    SaveDataToList(hoaDonList5);
                    checkBan05 = true;
                    buttonColor(btBan05);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 06" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon06);
                    SaveDataToList(hoaDonList6);
                    checkBan06 = true;
                    buttonColor(btBan06);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 07" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon07);
                    SaveDataToList(hoaDonList7);
                    checkBan07 = true;
                    buttonColor(btBan07);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 08" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon08);
                    SaveDataToList(hoaDonList8);
                    checkBan08 = true;
                    buttonColor(btBan08);
                    MessageBox.Show("Lưu thành công");
                }
                else if (lbSoBan.Text == "Bàn số 09" && txtMaHoaDon.Text != "")
                {
                    UpdateHoaDon(hoaDon09);
                    SaveDataToList(hoaDonList9);
                    checkBan09 = true;
                    buttonColor(btBan09);
                    MessageBox.Show("Lưu thành công");
                }
            }
            // Nếu chọn No, không thực hiện gì thêm
            else
            {
                // Bạn có thể để trống hoặc thông báo gì đó nếu muốn
                MessageBox.Show("Lưu thông tin bị hủy.");
            }
        }


        private void btBan01_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 01";

            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan01 == true)
            {
                LoadHoaDon(hoaDon01, hoaDonList1);
                CalculateTotalAmount();
            }
        }

        private void btBan02_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 02";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan02 == true)
            {
                LoadHoaDon(hoaDon02, hoaDonList2);
                CalculateTotalAmount();
            }
        }

        private void btBan03_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 03";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan03 == true)
            {
                LoadHoaDon(hoaDon03, hoaDonList3);
                CalculateTotalAmount();
            }
        }

        private void btBan04_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 04";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan04 == true)
            {
                LoadHoaDon(hoaDon04, hoaDonList4);
                CalculateTotalAmount();
            }
        }

        private void btBan05_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 05";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan05 == true)
            {
                LoadHoaDon(hoaDon05, hoaDonList5);
                CalculateTotalAmount();
            }
        }

        private void btBan06_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 06";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan06 == true)
            {
                LoadHoaDon(hoaDon06, hoaDonList6);
                CalculateTotalAmount();
            }
        }

        private void btBan07_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 07";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan07 == true)
            {
                LoadHoaDon(hoaDon07, hoaDonList7);
                CalculateTotalAmount();
            }
        }

        private void btBan08_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 08";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan08 == true)
            {
                LoadHoaDon(hoaDon08, hoaDonList8);
                CalculateTotalAmount();
            }
        }

        private void btBan09_Click(object sender, EventArgs e)
        {
            lbSoBan.Text = "Bàn số 09";
            // Xóa dữ liệu cũ trong groupBox và tạo hóa đơn mới
            ResetControlsInGroupBox(groupBox1);

            if (checkBan09 == true)
            {
                LoadHoaDon(hoaDon09, hoaDonList9);
                CalculateTotalAmount();
            }
        }

        private void btHuyHoaDon_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy hóa đơn này không?", // Nội dung câu hỏi
                "Xác nhận hủy hóa đơn",                        // Tiêu đề của hộp thoại
                MessageBoxButtons.YesNo,                        // Các nút Yes/No
                MessageBoxIcon.Warning);                         // Biểu tượng cảnh báo

            // Kiểm tra xem người dùng chọn Yes hay No
            if (dialogResult == DialogResult.Yes)
            {
                // Tiến hành hủy hóa đơn nếu người dùng chọn Yes
                if (lbSoBan.Text == "Bàn số 01")
                {
                    DeleteHoaDon(hoaDon01);
                    checkBan01 = false;
                    ResetButtonColor(btBan01);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 02")
                {
                    DeleteHoaDon(hoaDon02);
                    checkBan02 = false;
                    ResetButtonColor(btBan02);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 03")
                {
                    DeleteHoaDon(hoaDon03);
                    checkBan03 = false;
                    ResetButtonColor(btBan03);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 04")
                {
                    DeleteHoaDon(hoaDon04);
                    checkBan04 = false;
                    ResetButtonColor(btBan04);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 05")
                {
                    DeleteHoaDon(hoaDon05);
                    checkBan05 = false;
                    ResetButtonColor(btBan05);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 06")
                {
                    DeleteHoaDon(hoaDon06);
                    checkBan06 = false;
                    ResetButtonColor(btBan06);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 07")
                {
                    DeleteHoaDon(hoaDon07);
                    checkBan07 = false;
                    ResetButtonColor(btBan07);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 08")
                {
                    DeleteHoaDon(hoaDon08);
                    checkBan08 = false;
                    ResetButtonColor(btBan08);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
                else if (lbSoBan.Text == "Bàn số 09")
                {
                    DeleteHoaDon(hoaDon09);
                    checkBan09 = false;
                    ResetButtonColor(btBan09);
                    MessageBox.Show("Hủy hóa đơn thành công");
                }
            }
            else
            {
                // Nếu chọn No, không thực hiện hành động gì
                MessageBox.Show("Hủy hóa đơn bị hủy.");
            }
        }


        private void btThem_Click(object sender, EventArgs e)
        {
            // Thêm một hàng mới vào DataGridView
            int index = dgv_hoaDon.Rows.Add();

            // Thiết lập giá trị cho các ô trong hàng mới
            dgv_hoaDon.Rows[index].Cells["SanPham"].Value = "Giá trị 1"; // Giá trị cột 1
            dgv_hoaDon.Rows[index].Cells["SoLuong"].Value = "Giá trị 2"; // Giá trị cột 2
            dgv_hoaDon.Rows[index].Cells["ThanhTien"].Value = "Giá trị 3"; // Giá trị cột 3
            dgv_hoaDon.Rows[index].Cells["KhuyenMai"].Value = "0"; // Giá trị cột 3
            // Hoặc bạn có thể dùng giá trị từ các điều khiển khác như TextBox nếu muốn
            // Ví dụ: dataGridView1.Rows[index].Cells["Column1"].Value = textBox1.Text;
        }

        private void dgv_hoaDon_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng vừa hoàn thành chỉnh sửa cột "Tên sản phẩm", "Số lượng", hoặc "Khuyến mãi"
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 3)
            {
                string tenSP = dgv_hoaDon.Rows[e.RowIndex].Cells[0].Value?.ToString();
                int soLuong = 0;
                decimal gia = 0;

                if (!string.IsNullOrEmpty(tenSP))
                {
                    // Truy vấn giá từ cơ sở dữ liệu dựa trên tên sản phẩm
                    gia = GetGiaTien(tenSP);
                }

                // Lấy số lượng từ cột "Số lượng" (cột thứ 1)
                if (int.TryParse(dgv_hoaDon.Rows[e.RowIndex].Cells[1].Value?.ToString(), out soLuong))
                {
                    // Kiểm tra xem số lượng có vượt quá số lượng trong kho không
                    int availableQuantity = GetAvailableProductQuantity(tenSP); // Hàm lấy số lượng sản phẩm còn lại
                    if (soLuong > availableQuantity)
                    {
                        // Nếu số lượng nhập vào lớn hơn số lượng trong kho, thông báo và không cập nhật giá trị
                        MessageBox.Show($"Sản phẩm '{tenSP}' không đủ số lượng. Số lượng còn lại: {availableQuantity}");
                        dgv_hoaDon.Rows[e.RowIndex].Cells[1].Value = availableQuantity; // Đặt số lượng về số còn lại
                        return; // Dừng lại không thực hiện tính toán
                    }

                    // Tính thành tiền = Số lượng * Giá
                    decimal thanhTien = gia * soLuong;

                    // Cập nhật ô "Thành tiền" (cột thứ 2)
                    dgv_hoaDon.Rows[e.RowIndex].Cells[2].Value = thanhTien;

                    // Gọi hàm tính tổng tiền sau khi cập nhật "Thành tiền"
                    CalculateTotalAmount();
                }
                else
                {
                    // Nếu số lượng không hợp lệ, hiển thị thông báo
                    MessageBox.Show("Số lượng sản phẩm không hợp lệ!");
                }
            }
        }


        private void btXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgv_hoaDon.SelectedRows.Count > 0)
            {
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn "Yes"
                if (result == DialogResult.Yes)
                {
                    // Lấy chỉ số của dòng được chọn
                    int rowIndex = dgv_hoaDon.SelectedRows[0].Index;

                    // Xóa dòng khỏi DataGridView
                    dgv_hoaDon.Rows.RemoveAt(rowIndex);

                    // Tính lại tổng tiền sau khi xóa dòng
                    CalculateTotalAmount();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void btYeuCauThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã điền đầy đủ các ô chưa
            if (txtMaHoaDon.Text == "")
            {
                MessageBox.Show("Mã hóa đơn chưa được nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Mã nhân viên chưa được nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Số điện thoại chưa được nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPhoneNumber(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại nhập không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu mã nhân viên đã tồn tại trong cơ sở dữ liệu
            string maNV = txtMaNhanVien.Text;
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6C4ON0I\SQLEXPRESS;Initial Catalog=LTTQ;Integrated Security=True;"))
            {
                connection.Open();
                string checkMaNVQuery = "SELECT COUNT(*) FROM NHANVIEN WHERE MaNV = @MaNV";
                using (SqlCommand checkCmd = new SqlCommand(checkMaNVQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@MaNV", maNV);
                    int count = (int)checkCmd.ExecuteScalar();

                    // Nếu mã nhân viên không tồn tại, thông báo và dừng phương thức
                    if (count == 0)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Kiểm tra nếu ô tổng tiền có giá trị lớn hơn 0
            if (decimal.TryParse(txtTongTien.Text, out decimal tongTien) && tongTien > 0)
            {
                // Kiểm tra nếu người dùng chưa chọn giá trị
                if (cbPayment.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn phương thức thanh toán!");
                }
                else
                {
                    string selectedPaymentMethod = cbPayment.SelectedItem.ToString();

                    // Kiểm tra nếu giá trị là "Tiền mặt" hoặc "Chuyển khoản"
                    if (selectedPaymentMethod == "Tiền mặt")
                    {
                        // Xử lý thanh toán bằng tiền mặt ở đây
                        // Kiểm tra mã khách hàng và lấy địa chỉ nếu cần
                        string soDienThoai = txtSoDienThoai.Text;
                        string diaChiKH = kiemTraKhachHang(soDienThoai);
                        if (diaChiKH == null) return;  // Nếu địa chỉ không hợp lệ thì dừng phương thức

                        // Tiến hành xử lý thanh toán tiền mặt 
                        Cash cash = new Cash(txtTongTien.Text);
                        cash.ButtonClicked += Cash_buttonClicked;
                        cash.Show();
                    }
                    else if (selectedPaymentMethod == "Chuyển khoản")
                    {
                        // Kiểm tra mã khách hàng và lấy địa chỉ nếu cần
                        string soDienThoai = txtSoDienThoai.Text;
                        string diaChiKH = kiemTraKhachHang(soDienThoai);
                        if (diaChiKH == null) return;  // Nếu địa chỉ không hợp lệ thì dừng phương thức

                        // Tiến hành xử lý thanh toán chuyển khoản
                        BankTransfer payment = new BankTransfer(txtTongTien.Text);
                        payment.ButtonClicked += Payment_ButtonClicked;
                        payment.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa thêm đơn hàng để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btChonMon_Click(object sender, EventArgs e)
        {
            menu = new MenuItems();

            // Đăng ký sự kiện FormClosed để xử lý sau khi form đóng lại
            menu.FormClosed += menu_FormClosed;

            menu.Show();


        }

        private void lbLogout_Click(object sender, EventArgs e)
        {
            // Xóa thông tin phiên làm việc
            User.ClearSession();

            logout();
            lbUser.Text = "Mã NV: " + maNhanVien + " - " + "Tên NV: " + tenNhanVien;

            // Hiển thị thông báo hoặc thực hiện hành động khác (tuỳ chọn)
            MessageBox.Show("Bạn đã đăng xuất thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Quay trở lại form đăng nhập (hoặc form chính)
            Form8 loginForm = new Form8(); // Giả sử bạn có FormLogin
            loginForm.Show();
            this.Hide(); // Ẩn form hiện tại
        }
    }
}
