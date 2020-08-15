using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Report.Report_Forms
{
    public partial class frmBranchShortingReport : Form
    {
        public frmBranchShortingReport()
        {
            InitializeComponent();
        }
        public DataTable ddtBranchShorting;
        public string Branch, ToStore, FromDate;
        private void frmBranchShortingReport_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("ds_BranchShorting", ddtBranchShorting);

            // creating the parameter with the extact name as in the report.
            ReportParameter param1 = new ReportParameter("ParmBranch", Branch, true);
            ReportParameter param2 = new ReportParameter("ParmToStore", ToStore, true);
            ReportParameter param3 = new ReportParameter("ParmFromDate", FromDate, true);
        
            // adding the parameter in the report dynamically
            reportViewer1.LocalReport.SetParameters(param1);
            reportViewer1.LocalReport.SetParameters(param2);
            reportViewer1.LocalReport.SetParameters(param3);
      

            reportViewer1.LocalReport.DataSources.Add(rds);


            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();




            this.reportViewer1.RefreshReport();
        }
    }
}
