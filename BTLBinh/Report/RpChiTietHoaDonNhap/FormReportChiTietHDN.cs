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
                reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpChiTietHoaDonNhap.HoaDonNhap.rdlc";
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = process.GetChiTietHD("CHITIETHDN","MaHDN",maHDN);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
