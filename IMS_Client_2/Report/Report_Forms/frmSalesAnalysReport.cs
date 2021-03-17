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
    public partial class frmSalesAnalysReport : Form
    {
        public frmSalesAnalysReport()
        {
            InitializeComponent();
        }
        CoreApp.clsConnection_DAL ObjDAL = new CoreApp.clsConnection_DAL(true);
        CoreApp.clsUtility ObjUtil = new CoreApp.clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        
        int ShopeID = 0;

        private void frmEmployeeSales_Load(object sender, EventArgs e)
        {
            btnPrinterSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
            dtFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            FillStoreData();
        }
        
        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(CoreApp.clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbShop.DataSource = dt;
                cmbShop.DisplayMember = "StoreName";
                cmbShop.ValueMember = "StoreID";
                cmbShop.SelectedIndex = -1;
            }
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

        private void btnPrinterSave_Click(object sender, EventArgs e)
        {
            if (cmbShop.SelectedIndex == -1)
            {
                ShopeID = 0;
            }
            else
            {
                ShopeID = (int)(cmbShop.SelectedValue);
            }
            reportViewer1.LocalReport.DataSources.Clear();

            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, ShopeID);
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtFromDate.Value.ToString("yyyy-MM-dd"));
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtToDate.Value.ToString("yyyy-MM-dd"));
         
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(CoreApp.clsUtility.DBName + ".dbo.SPR_Get_SaleAnalysis_Report");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtsalesAnaylisis = ds.Tables[0];
                ReportDataSource rds = new ReportDataSource("dsSalesAnalysis", dtsalesAnaylisis);

                string datefilter ="From : "+dtFromDate.Value.ToShortDateString() + " To " + dtToDate.Value.ToShortDateString();
                ReportParameter param1 = new ReportParameter("parmDateFilter", datefilter, true);
                string shopName = "";

                if (cmbShop.SelectedIndex==-1)
                {
                     shopName = "Shop Filter : ALL";
                }
                else
                {
                    shopName ="Shop Name : "+ cmbShop.Text;
                }

                ReportParameter param3 = new ReportParameter("parmShope", shopName, true);

                // adding the parameter in the report dynamically
                reportViewer1.LocalReport.SetParameters(param1);

                reportViewer1.LocalReport.SetParameters(param3);

                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                this.reportViewer1.RefreshReport();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbShop.SelectedIndex = -1;
         
            dtFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            reportViewer1.LocalReport.DataSources.Clear();
        }
    }
}