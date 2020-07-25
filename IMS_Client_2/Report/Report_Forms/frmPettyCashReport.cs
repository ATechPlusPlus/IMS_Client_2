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
    public partial class frmPettyCashReport : Form
    {
        public frmPettyCashReport()
        {
            InitializeComponent();
        }

       
        public DataTable dtPettyCashDetails;

        public string strFilterBy;
        public string strCondtion;

        public string strTotalPettyCashAmt;
        public string strToTotalPettyCashExpAmt;
        public string strPettyCashBalance;

        private void frmPettyCashReport_Load(object sender, EventArgs e)
        {
            // creating the parameter with the extact name as in the report.
            ReportParameter param1 = new ReportParameter("parmFilterBy", strFilterBy, true);
            ReportParameter param2 = new ReportParameter("parmFilterCondition", strCondtion, true);
            ReportParameter param3 = new ReportParameter("parmPettyCashAmount", strTotalPettyCashAmt, true);
            ReportParameter param4 = new ReportParameter("ParmPettyCashExpAmt", strToTotalPettyCashExpAmt, true);
            ReportParameter param5 = new ReportParameter("parmPettyCashBalance", strPettyCashBalance, true);
            // adding the parameter in the report dynamically
            reportViewer1.LocalReport.SetParameters(param1);
            reportViewer1.LocalReport.SetParameters(param2);
            reportViewer1.LocalReport.SetParameters(param3);
            reportViewer1.LocalReport.SetParameters(param4);
            reportViewer1.LocalReport.SetParameters(param5);


            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("ds_PettyCashDetails", dtPettyCashDetails);


            reportViewer1.LocalReport.DataSources.Add(rds);
         

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();




            this.reportViewer1.RefreshReport();
        }
    }
}
