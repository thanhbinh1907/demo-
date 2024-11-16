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

namespace BTLBinh.Report.RpChiTietHoaDonNhap
{
    public partial class FormReportChiTietHDN : Form
    {
        private string maHDN;
        public FormReportChiTietHDN(string maHDN)
        {
            InitializeComponent();
            this.maHDN = maHDN;
        }

        private void FormReportHDN_Load(object sender, EventArgs e)
        {
            try
            {
                DataProcess process = new DataProcess();
                reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpChiTietHoaDonNhap.ChiTietHoaDonNhap.rdlc";

                // Thiết lập nguồn dữ liệu
                ReportDataSource reportDataSource = new ReportDataSource
                {
                    Name = "DataSet1",
                    Value = process.GetChiTietHD("CHITIETHDN", "MaHDN", maHDN)
                };

                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Thêm tham số để hiển thị tên người dùng
                var parameters = new ReportParameter[]
                {
            new ReportParameter("UserName", User.CurrentEmployeeName ?? "Người dùng chưa đăng nhập")
                };

                reportViewer1.LocalReport.SetParameters(parameters); // Thiết lập tham số
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
