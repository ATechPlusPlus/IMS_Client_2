using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace IMS_Client_2.Report.Report_Forms
{
    public partial class frmStockMovesReport : Form
    {
        public frmStockMovesReport()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmStockMovesReport_Load(object sender, EventArgs e)
        {
            btnPrinterSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void GenerateReport()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"));
                ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"));
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StockMove_Report");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        reportViewer1.LocalReport.DataSources.Clear();
                        if (dt.Columns.Contains("Flag"))
                        {
                            clsUtility.ShowErrorMessage(dt.Rows[0]["Msg"].ToString());
                            return;
                        }
                        ReportParameter param1 = new ReportParameter("parmDateFilter", "Date Filter : " + dtpFromDate.Value.ToString("dd-MMM-yyyy") + " to " + dtpToDate.Value.ToString("dd-MMM-yyyy"), true);

                        // adding the parameter in the report dynamically
                        reportViewer1.LocalReport.SetParameters(param1);

                        ReportDataSource rds = new ReportDataSource("dsStockMovesReport", dt);
                        reportViewer1.LocalReport.DataSources.Add(rds);

                        reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                        reportViewer1.ZoomMode = ZoomMode.Percent;
                        reportViewer1.ZoomPercent = 100;
                        this.reportViewer1.RefreshReport();
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("No Data found for the given filter.", clsUtility.strProjectTitle);
                        reportViewer1.LocalReport.DataSources.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void btnPrinterSave_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

    }
}