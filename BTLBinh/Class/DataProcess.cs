using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization; // Đảm bảo bạn đã thêm namespace này
using BTLBinh.Class;

namespace BTLBinh
{
    internal class DataProcess
    {
        private string connectionString = @"Data Source=LAPTOP-4I3NB3DQ\MSSQLSERVER01;Initial Catalog=LTTQ;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public string ConnectionString => connectionString;

        public DataTable DataConnect(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        connection.Open();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery(); // Thực thi câu lệnh không trả về kết quả
                }
            }
        }
        public DataTable GetDanhMuc()
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                string query = "Select * from SANPHAM";
                SqlDataAdapter adt = new SqlDataAdapter(query, Connection);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                return dt;
            }
        }
        public DataTable GetNhanVien()
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                string query = "Select * from NHANVIEN";
                SqlDataAdapter adt = new SqlDataAdapter(query, Connection);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                return dt;
            }
        }
        public DataTable GetChiTietHD(string tablename, string TenCot, string maHD)
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                // Sử dụng phép nối chuỗi để tạo câu truy vấn SQL
                string query = $"SELECT * FROM {tablename} WHERE {TenCot} = @MaHD";

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    // Thêm tham số để tránh SQL Injection
                    command.Parameters.AddWithValue("@MaHD", maHD);

                    SqlDataAdapter adt = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adt.Fill(dt);
                    return dt;
                }
            }
        }
        public DataTable GetNhaCungCap()
        {
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                string query = "Select * from NHACUNGCAP";
                SqlDataAdapter adt = new SqlDataAdapter(query, Connection);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                return dt;
            }
        }
        public List<SalesData> GetSalesData(DateTime startDate, DateTime endDate)
        {
            List<SalesData> salesDataList = new List<SalesData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Sử dụng chuỗi để xây dựng câu truy vấn
                string query = $"SELECT NgayBan, SUM(TongTien) AS TotalRevenue FROM HOADONBAN WHERE NgayBan BETWEEN @startDate AND @endDate GROUP BY NgayBan ORDER BY NgayBan";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesDataList.Add(new SalesData
                            {
                                NgayBan = reader.GetDateTime(0),
                                TongTien = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }

            return salesDataList;
        }
        public List<BuyData> GetBuyData(DateTime startDate, DateTime endDate)
        {
            List<BuyData> salesDataList = new List<BuyData>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Câu truy vấn lấy dữ liệu cho hóa đơn nhập
                string query = "SELECT NgayNhap, SUM(TongTien) AS TotalRevenue FROM HOADONNHAP WHERE NgayNhap BETWEEN @startDate AND @endDate GROUP BY NgayNhap ORDER BY NgayNhap";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesDataList.Add(new BuyData
                            {
                                NgayNhap = reader.GetDateTime(0), // Giả sử bạn vẫn sử dụng thuộc tính NgayNhap
                                TongTien = reader.GetDecimal(1)
                            });
                        }
                    }
                }
            }

            return salesDataList;
        }
    }
}