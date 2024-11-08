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
                reportViewer1.LocalReport.ReportEmbeddedResource = "BTLBinh.Report.RpChiTietHoaDonBan.HoaDonBan.rdlc";
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = process.GetChiTietHD("CHITIETHDB", "MaHDB", maHDB);
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
