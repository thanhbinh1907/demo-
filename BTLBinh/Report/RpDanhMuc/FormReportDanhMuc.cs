using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace BTLBinh
{
    public partial class FormReportDanhMuc : Form
    {

        public FormReportDanhMuc()
        {
            InitializeComponent();
        }

        private void FormReportDanhMuc_Load(object sender, EventArgs e)
        {
            try
            {
                DataProcess process = new DataProcess();
                reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpDanhMuc.DanhMuc.rdlc";
                ReportDataSource reportDataSource = new ReportDataSource
                {
                    Name = "DataSet1",
                    Value = process.GetDanhMuc()
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
