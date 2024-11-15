using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh
{
    internal class User
    {
        public static string CurrentEmployeeId { get; set; } // Mã nhân viên
        public static string CurrentEmployeeName { get; set; } // Tên nhân viên
        public static string UserType { get; set; } // Có thể là "Admin" hoặc "Employee"
        private static Label userLabel; // Tham chiếu đến label hiển thị tên người dùng

        public static void SetSession(string employeeId, string employeeName, string userType)
        {
            CurrentEmployeeId = employeeId;
            CurrentEmployeeName = employeeName;
            UserType = userType;
        }

        public static void ClearSession()
        {
            CurrentEmployeeId = null;
            CurrentEmployeeName = null;
            UserType = null;
            UpdateUserLabel(); // Cập nhật label khi xóa phiên
        }

        public static void SetUserLabel(Label label)
        {
            userLabel = label; // Lưu label để cập nhật
            UpdateUserLabel(); // Cập nhật label ngay khi thiết lập
        }

        public static void UpdateUserLabel()
        {
            if (userLabel != null)
            {
                userLabel.Text = $"Mã NV: {CurrentEmployeeId} - Tên NV: {CurrentEmployeeName ?? "Chưa đăng nhập"}";
            }
        }
    }
}
