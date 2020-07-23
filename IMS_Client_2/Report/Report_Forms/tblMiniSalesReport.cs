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
            int deafultStoreID = ObjDAL.ExecuteScalarInt("SELECT Storeid FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName = '" + Environment.MachineName + "'");
            cmbShop.SelectedValue = deafultStoreID;


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
            if (cmbGenerate.SelectedIndex==-1)
            {
                clsUtility.ShowInfoMessage("Please select Specification.", clsUtility.strProjectTitle);
                return;
            }
            //string strQuery = "select * from "+ clsUtility.DBName + ".dbo.View_SalesBillDetails";

            string reportQuery = "";
            string strTotalQTY, strTotalRate, strAddress="", strShopName="";
            string strGenericColumn = "";

            if (cmbGenerate.SelectedIndex==0) //Employee ( Sales Man)
            {
                reportQuery = " SELECT v1.Name as GenericColumn  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                                " IMS_Client_2.dbo.View_SalesBillDetails v1 JOIN IMS_Client_2.dbo.View_SalesDetails v2 " +
                                 " ON v1.id = v2.InvoiceID " +
                                 " where v1.ShopeID = "+cmbShop.SelectedValue+" AND v1.InvoiceDate between '"+dtpFromDate.Value.ToString("yyyy-MM-dd") +"' AND '"+dtpToDate.Value.ToString("yyyy-MM-dd")+"' " +
                                 " Group by Name";

                strGenericColumn = "Sales Man";
            }
            else if (cmbGenerate.SelectedIndex == 1) //Color
            {
                reportQuery = " SELECT  v2.ColorName as GenericColumn  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                               " IMS_Client_2.dbo.View_SalesBillDetails v1 JOIN IMS_Client_2.dbo.View_SalesDetails v2 " +
                                " ON v1.id = v2.InvoiceID " +
                                " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' " +
                                " Group by ColorName";

                strGenericColumn = "Color";
            }
            else if (cmbGenerate.SelectedIndex == 2) //Product 
            {
                reportQuery = " SELECT  v2.ProductName as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                              " IMS_Client_2.dbo.View_SalesBillDetails v1 JOIN IMS_Client_2.dbo.View_SalesDetails v2 " +
                               " ON v1.id = v2.InvoiceID " +
                               " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' " +
                               " Group by  v2.ProductName";

                strGenericColumn = "Item Name";
            }
            else if (cmbGenerate.SelectedIndex == 3) //Invoice/Bill No
            {
                reportQuery = " SELECT  v1.InvoiceNumber as GenericColumn, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                              " IMS_Client_2.dbo.View_SalesBillDetails v1 JOIN IMS_Client_2.dbo.View_SalesDetails v2 " +
                               " ON v1.id = v2.InvoiceID " +
                               " where v1.ShopeID = " + cmbShop.SelectedValue + " AND v1.InvoiceDate between '" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpToDate.Value.ToString("yyyy-MM-dd") + "' " +
                               " Group by  v1.InvoiceNumber";

                strGenericColumn = "Invoice No";
            }

          

            string strQ = "select StoreName, Place from StoreMaster where StoreID="+cmbShop.SelectedValue;
            DataTable dtShope= ObjDAL.ExecuteSelectStatement(strQ);
            if (dtShope.Rows.Count>0)
            {
                strShopName = dtShope.Rows[0]["StoreName"].ToString();
                strAddress = dtShope.Rows[0]["Place"].ToString();
            }

          

            DataTable dtSalesDetails = ObjDAL.ExecuteSelectStatement(reportQuery);

            if (ObjUtil.ValidateTable(dtSalesDetails))
            {

            
                strTotalRate = dtSalesDetails.Compute("Sum(Rate)", string.Empty).ToString();
                strTotalQTY = dtSalesDetails.Compute("Sum(QTY)", string.Empty).ToString();

                string strDateFilter ="From : "+ dtpFromDate.Value.ToShortDateString()+" To "+dtpToDate.Value.ToShortDateString();

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
                clsUtility.ShowInfoMessage("No Datafound for the given filter.", clsUtility.strProjectTitle);
                reportViewer1.LocalReport.DataSources.Clear();
            }
        }

        private void txtSalesMan_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpID.Clear();
            cmbGenerate.SelectedIndex = -1;
            cmbShop.SelectedIndex = -1;
           
        }

        private void txtInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void cmbColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
          
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
        
        }
    }
}
