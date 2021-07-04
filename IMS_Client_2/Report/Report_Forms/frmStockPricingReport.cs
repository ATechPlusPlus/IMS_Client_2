using CoreApp;
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
    public partial class frmStockPricingReport : Form
    {
        public frmStockPricingReport()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new CoreApp.clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmStockPricingReport_Load(object sender, EventArgs e)
        {
            btnGenerateReport.BackgroundImage = B_Leave;
            btnClear.BackgroundImage = B_Leave;
            FillStoreData();
        }

        private void btnViewDetails_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnViewDetails_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void FillStoreData()
        {
            try
            {
                DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
                if (ObjUtil.ValidateTable(dt))
                {
                    cmbShop.DataSource = dt;
                    cmbShop.DisplayMember = "StoreName";
                    cmbShop.ValueMember = "StoreID";
                    cmbShop.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbShop.SelectedIndex < 0)
                {
                    clsUtility.ShowInfoMessage("Enter Shop Name           ", clsUtility.strProjectTitle);
                    cmbShop.Focus();
                }
                else
                {
                    GenerateReport();
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void GenerateReport()
        {
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, Convert.ToInt32(cmbShop.SelectedValue));
            DataSet dsStockPricing = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Stock_LocalCost_Report");

            if (ObjUtil.ValidateDataSet(dsStockPricing))
            {
                DataTable dt = dsStockPricing.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportParameter param1 = new ReportParameter("parmShop", cmbShop.Text, true);

                    // adding the parameter in the report dynamically
                    reportViewer1.LocalReport.SetParameters(param1);
                    ReportDataSource rds = new ReportDataSource("dsStockPricingReport", dt);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbShop.SelectedIndex = -1;
        }
    }
}