using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLBinh
{
    internal class DataProcess
    {
        private string connectionString = @"Data Source=LAPTOP-4I3NB3DQ\MSSQLSERVER01;Initial Catalog=BTL-LTTQ;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

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
    }
}