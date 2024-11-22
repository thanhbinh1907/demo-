using System;
using System.Linq;
using System.Windows.Forms;

namespace BTLBinh
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi chạy Form1
            Form8 mainForm = new Form8();
            mainForm.FormClosed += OnFormClosed; // Đăng ký sự kiện khi đóng form
            Application.Run(mainForm); // Chạy vòng lặp sự kiện với Form1
        }

        // Phương thức xử lý khi form đóng
        public static void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            // Kiểm tra nếu không còn form nào mở thì thoát ứng dụng
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit(); // Thoát ứng dụng nếu không còn form nào
            }
        }
    }
}