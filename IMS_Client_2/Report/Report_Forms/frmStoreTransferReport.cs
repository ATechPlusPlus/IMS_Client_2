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
    public partial class frmStoreTransferReport : Form
    {
        public frmStoreTransferReport()
        {
            InitializeComponent();
        }
        public DataTable dtStoreTransferDetails;
        public string strFromStore, strToStore, strTotalQTY, strTotalRate, strBillDate, strBillNo;

        private void frmStoreTransferReport_Load(object sender, EventArgs e)
        {
            GenerateReport();
        }
        private void GenerateReport()
        {
            try
            {
              

                ReportDataSource rds = new ReportDataSource("ds_StoreTransfer", dtStoreTransferDetails);

                // creating the parameter with the extact name as in the report.
                ReportParameter param1 = new ReportParameter("parmFromStore", strFromStore, true);
                ReportParameter param2 = new ReportParameter("ParmToStore", strToStore, true);
                ReportParameter param3 = new ReportParameter("ParmBillNo", strBillNo, true);
                ReportParameter param4 = new ReportParameter("parmBillDate", strBillDate, true);
                ReportParameter param5 = new ReportParameter("ParmTotalQTY", strTotalQTY, true);
                ReportParameter param6 = new ReportParameter("ParmTotalAmount", strTotalRate, true);



                // adding the parameter in the report dynamically
                reportViewer1.LocalReport.SetParameters(param1);
                reportViewer1.LocalReport.SetParameters(param2);
                reportViewer1.LocalReport.SetParameters(param3);
                reportViewer1.LocalReport.SetParameters(param4);
                reportViewer1.LocalReport.SetParameters(param5);
                reportViewer1.LocalReport.SetParameters(param6);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);


                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
           


            
          

           
        }
    }
}
