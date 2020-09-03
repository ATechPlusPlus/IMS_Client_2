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
    public partial class tblMiniSalesReport : Form
    {
        public tblMiniSalesReport()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new CoreApp.clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void tblMiniSalesReport_Load(object sender, EventArgs e)
        {
            btnGenerateReport.BackgroundImage = B_Leave;
            btnClear.BackgroundImage = B_Leave;

            FillStoreData();
            //BindColorData();

            // set Default store
            //int deafultStoreID = ObjDAL.ExecuteScalarInt("SELECT Storeid FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName = '" + Environment.MachineName + "'");
            int deafultStoreID = frmHome.Home_StoreID;
            cmbShop.SelectedValue = deafultStoreID;

            dtpFromDate.MaxDate = DateTime.Now;
            dtpToDate.MaxDate = DateTime.Now;
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
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbShop.DataSource = dt;
                cmbShop.DisplayMember = "StoreName";
                cmbShop.ValueMember = "StoreID";
                cmbShop.SelectedIndex = -1;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (ObjUtil.IsControlTextEmpty(cmbShop))
            {
                clsUtility.ShowInfoMessage("Please select Store.");
                cmbShop.Focus();
                return;
            }
            if (ObjUtil.IsControlTextEmpty(cmbGenerate))
            {
                clsUtility.ShowInfoMessage("Please select Specification.");
                cmbGenerate.Focus();
                return;
            }
            
            //string strQuery = "select * from "+ clsUtility.DBName + ".dbo.View_SalesBillDetails";

            //string reportQuery = "";
            string strTotalQTY="0", strTotalRate="0", strAddress = "", strShopName = "";
            string strGenericColumn = "";

            #region Commented Inline SQL Query
            //if (cmbGenerate.SelectedIndex == 0) //Employee ( Sales Man)
            //{
            //    reportQuery = " SELECT v1.Name as GenericColumn  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                    " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                     " ON v1.id = v2.InvoiceID " +
            //                     " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0 " +
            //                     " Group by Name";

            //    strGenericColumn = "Sales Man";
            //}
            //else if (cmbGenerate.SelectedIndex == 1) //Color
            //{
            //    reportQuery = " SELECT  v2.ColorName as GenericColumn  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                   " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                    " ON v1.id = v2.InvoiceID " +
            //                    " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0" +
            //                    " Group by ColorName";

            //    strGenericColumn = "Color";
            //}
            //else if (cmbGenerate.SelectedIndex == 2) //Product 
            //{
            //    reportQuery = " SELECT  v2.ProductName as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                   " ON v1.id = v2.InvoiceID " +
            //                   " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0" +
            //                   " Group by  v2.ProductName";

            //    strGenericColumn = "Item Name";
            //}
            //else if (cmbGenerate.SelectedIndex == 3) //Invoice/Bill No
            //{
            //    reportQuery = " SELECT  v1.InvoiceNumber as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                   " ON v1.id = v2.InvoiceID " +
            //                   " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0" +
            //                   " Group by  v1.InvoiceNumber";

            //    strGenericColumn = "Invoice No";
            //}
            //else if (cmbGenerate.SelectedIndex == 4) // Category Department
            //{
            //    reportQuery = " SELECT   (select CategoryName from CategoryMaster where CategoryID=v2.CategoryID )  as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                   " ON v1.id = v2.InvoiceID " +
            //                   " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0" +
            //                   " Group by  v2.CategoryID";

            //    strGenericColumn = "Invoice No";
            //}
            //else if (cmbGenerate.SelectedIndex == 5) // Model No
            //{
            //    reportQuery = " SELECT  v2.ModelNo  as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
            //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
            //                   " ON v1.id = v2.InvoiceID " +
            //                   " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' AND v2.Rate>0" +
            //                   " Group by  v2.ModelNo";

            //    strGenericColumn = "ModelNo";
            //}
            #endregion

            string strQ = "SELECT StoreName, Place FROM StoreMaster WITH(NOLOCK) WHERE StoreID=" + cmbShop.SelectedValue;
            DataTable dtShope = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtShope))
            {
                strShopName = dtShope.Rows[0]["StoreName"].ToString();
                strAddress = dtShope.Rows[0]["Place"].ToString();
            }

            ObjDAL.SetStoreProcedureData("index", SqlDbType.Int, cmbGenerate.SelectedIndex, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, cmbShop.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ReportType", SqlDbType.VarChar, "Mini", clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("GenericColumn", SqlDbType.VarChar, "", clsConnection_DAL.ParamType.Output);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_GetDashBoard_MiniSales_Report");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtSalesDetails = ds.Tables[0];
                //DataTable dtSalesDetails = ObjDAL.ExecuteSelectStatement(reportQuery);
                if (ObjUtil.ValidateTable(dtSalesDetails))
                {
                    dtSalesDetails.Columns[0].ColumnName = "GenericColumn";
                    dtSalesDetails.AcceptChanges();

                    DataTable dtOutput = ObjDAL.GetOutputParmData();
                    if (ObjUtil.ValidateTable(dtOutput))
                    {
                        strGenericColumn = dtOutput.Rows[0][1].ToString();
                    }
                    //strTotalRate = dtSalesDetails.Compute("Sum(Rate)", "Rate>0").ToString();
                    //strTotalQTY = dtSalesDetails.Compute("Sum(QTY)", "Rate>0").ToString();

                    DataTable dtTotal = ds.Tables[1];
                    if (ObjUtil.ValidateTable(dtTotal))
                    {
                        strTotalRate = dtTotal.Rows[0]["TotalRate"].ToString();
                        strTotalQTY = dtTotal.Rows[0]["TotalQTY"].ToString();
                    }

                    string strDateFilter = "From : " + dtpFromDate.Value.ToShortDateString() + " To " + dtpToDate.Value.ToShortDateString();

                    ReportParameter param1 = new ReportParameter("DateFilter", strDateFilter, true);
                    ReportParameter param2 = new ReportParameter("ShopeName", strShopName, true);
                    ReportParameter param3 = new ReportParameter("Address", strAddress, true);
                    ReportParameter param4 = new ReportParameter("TotalQTY", strTotalQTY, true);
                    ReportParameter param5 = new ReportParameter("TotalAmount", strTotalRate, true);
                    ReportParameter param6 = new ReportParameter("parmGenericColumn", strGenericColumn, true);

                    // adding the parameter in the report dynamically
                    reportViewer1.LocalReport.SetParameters(param1);
                    reportViewer1.LocalReport.SetParameters(param2);
                    reportViewer1.LocalReport.SetParameters(param3);
                    reportViewer1.LocalReport.SetParameters(param4);
                    reportViewer1.LocalReport.SetParameters(param5);
                    reportViewer1.LocalReport.SetParameters(param6);

                    reportViewer1.LocalReport.DataSources.Clear();

                    ReportDataSource rds = new ReportDataSource("ds_MiniSales", dtSalesDetails);

                    reportViewer1.LocalReport.DataSources.Add(rds);

                    reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.Percent;
                    reportViewer1.ZoomPercent = 100;
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    clsUtility.ShowInfoMessage("No Data found for the given filter.");
                    reportViewer1.LocalReport.DataSources.Clear();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Data found for the given filter.");
                reportViewer1.LocalReport.DataSources.Clear();
            }
            ObjDAL.ResetData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpID.Clear();
            cmbGenerate.SelectedIndex = -1;
            cmbShop.SelectedIndex = -1;
            reportViewer1.LocalReport.DataSources.Clear();
        }
    }
}