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

namespace BTLBinh.Report.RpChiTietHoaDonBan
{
    public partial class FormReportChiTietHDB : Form
    {
        private string maHDB;
        public FormReportChiTietHDB(string maHDB)
        {
            InitializeComponent();
            this.maHDB = maHDB;
        }

        private void FormReportChiTietHDB_Load(object sender, EventArgs e)
        {
            try
            {
                DataProcess process = new DataProcess();
                reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpChiTietHoaDonBan.ChiTietHoaDonBan.rdlc";

                // Thiết lập nguồn dữ liệu
                ReportDataSource reportDataSource = new ReportDataSource
                {
                    Name = "DataSet1",
                    Value = process.GetChiTietHD("CHITIETHDB", "MaHDB", maHDB)
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
