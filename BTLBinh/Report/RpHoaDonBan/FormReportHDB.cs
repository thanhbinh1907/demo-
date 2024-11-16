using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLBinh.Report.RpHoaDonBan
{
    public partial class FormReportHDB : Form
    {
        public FormReportHDB()
        {
            InitializeComponent();
        }

        private void FormReportHDB_Load(object sender, EventArgs e)
        {
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStart.Value;
            DateTime endDate = dtpEnd.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc.");
                return;
            }

            DataProcess dataProcess = new DataProcess();
            var salesData = dataProcess.GetSalesData(startDate, endDate);

            if (salesData.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu cho khoảng thời gian đã chọn.");
                return;
            }

            // Cấu hình ReportViewer
            reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpHoaDonBan.HoaDonBan.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "DataSet1", // Đảm bảo tên này trùng khớp với DataSet trong RDLC
                Value = salesData
            };

            reportViewer1.LocalReport.DataSources.Clear(); // Xóa các nguồn dữ liệu cũ nếu có
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            // Thêm tham số để hiển thị tên người dùng
            var parameters = new ReportParameter[]
            {
        new ReportParameter("UserName", User.CurrentEmployeeName ?? "Người dùng chưa đăng nhập")
            };

            reportViewer1.LocalReport.SetParameters(parameters); // Thiết lập tham số

            // Hiển thị ReportViewer
            reportViewer1.RefreshReport();
            reportViewer1.Visible = true; // Hiện ReportViewer
            btnTao.Visible = false;
            dtpStart.Visible = false;
            dtpEnd.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
