using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Report
{
    public partial class frmSalesInvoiceReport : Form
    {
        public frmSalesInvoiceReport()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);

        public int InvoiceID = 0;

        public bool IsDirectPrint = false;
        private void frmSalesInvoice_Load(object sender, EventArgs e)
        {
            //string query = "SELECT p1.ProductName,IMS.dbo.fun_ToArabicNum(s1.QTY) as QTY," +
            //    "IMS.dbo.fun_ToArabicNum(s1.Rate) as Rate,"+
            //    "IMS.dbo.fun_ToArabicNum((s1.Qty*s1.Rate)) AS Total,ps.barcodeNo as BarNumber FROM " + clsUtility.DBName + ".[dbo].[SalesDetails] s1 JOIN " + clsUtility.DBName + ".dbo.ProductMaster p1 " +
            //   " ON s1.ProductID = p1.ProductID "+
            //   "  JOIN "+ clsUtility.DBName + ".dbo.ProductStockMaster ps"+
            //   "  ON ps.colorID=s1.ColorID and ps.SizeID=s1.SizeID "+
            //   " WHERE s1.InvoiceID = " + InvoiceID;

            string query = "SELECT p1.ProductName," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.QTY) as QTY," +
                "" + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.Rate) as Rate," +
                "" + clsUtility.DBName + ".dbo.fun_ToArabicNum((s1.Qty*s1.Rate)) AS Total,ps.barcodeNo as BarNumber FROM " + clsUtility.DBName + ".[dbo].[SalesDetails] s1 JOIN " + clsUtility.DBName + ".dbo.ProductMaster p1 " +
               " ON s1.ProductID = p1.ProductID " +
               "  JOIN " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster ps" +
               "  ON s1.ProductID = ps.ProductID AND ps.colorID=s1.ColorID AND ps.SizeID=s1.SizeID " +
               " WHERE s1.InvoiceID = " + InvoiceID;

            DataTable dtSalesDetails = ObjCon.ExecuteSelectStatement(query);

            string strQueryHeader_Footer = "SELECT s1.InvoiceNumber,s1.InvoiceDate, c1.Name AS CustName,e1.Name AS empName,st1.StoreName AS StoreName,s1.SubTotal,s1.Discount,s1.Tax,s1.GrandTotal,s1.PaymentMode,s1.PaymentAutoID,c1.PhoneNo AS CustomerMobile FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails s1 left JOIN " +
                                            " " + clsUtility.DBName + ".dbo.EmployeeDetails e1 ON s1.SalesMan = e1.EmpID left JOIN" +
                                            " " + clsUtility.DBName + ".[dbo].[CustomerMaster] c1 ON s1.CustomerID = c1.CustomerID left join" +
                                            " " + clsUtility.DBName + ".dbo.StoreMaster st1 ON st1.StoreID = s1.ShopeID WHERE s1.Id=" + InvoiceID;

            DataTable dtSalesHeader_Footer = ObjCon.ExecuteSelectStatement(strQueryHeader_Footer);

            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("ds_SalesDetails", dtSalesDetails);
            ReportDataSource rds2 = new ReportDataSource("ds_InvoiceHeader", dtSalesHeader_Footer);

            string strcomName = "";
            string strAddress = "";
            DataTable dtCompinfo = ObjCon.ExecuteSelectStatement("SELECT CompanyName,[Address] FROM " + clsUtility.DBName + ".dbo.CompanyMaster WITH(NOLOCK)");

            if (dtCompinfo != null && dtCompinfo.Rows.Count > 0)
            {
                strcomName = dtCompinfo.Rows[0]["CompanyName"].ToString();
                strAddress = dtCompinfo.Rows[0]["Address"].ToString();
            }
            else
            {
                strcomName = "Default Store/Shop Name";
                strAddress = "Default Address Line. Road No, Block No, Pin Code and State.";
            }
            DataTable dtFooterNote = ObjCon.ExecuteSelectStatement("SELECT InvoiceFooterNote FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK)");
            string footerNoteDefault = "Once payment made will not be refunded under any circumstances. ";
            if (dtFooterNote != null && dtFooterNote.Rows.Count > 0)
            {
                if (dtFooterNote.Rows[0]["InvoiceFooterNote"]!=DBNull.Value)
                {
                    footerNoteDefault = dtFooterNote.Rows[0]["InvoiceFooterNote"].ToString();
                }
            }
            else
            {
                strcomName = "Default Store/Shop Name";
                strAddress = "Default Address Line. Road No, Block No, Pin Code and State.";
            }

            string NetAmt = new Sales.NumberToEnglish().changeCurrencyToWords(dtSalesHeader_Footer.Rows[0]["GrandTotal"].ToString());
            // creating the parameter with the extact name as in the report.
            ReportParameter param1 = new ReportParameter("CompanyName", strcomName, true);
            ReportParameter param2 = new ReportParameter("CompanyAddress", strAddress, true);
            ReportParameter param3 = new ReportParameter("Note", footerNoteDefault, true);
            ReportParameter param4 = new ReportParameter("NetAmount", NetAmt, true);

            // adding the parameter in the report dynamically
            reportViewer1.LocalReport.SetParameters(param1);
            reportViewer1.LocalReport.SetParameters(param2);
            reportViewer1.LocalReport.SetParameters(param3);
            reportViewer1.LocalReport.SetParameters(param4);

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);

            this.reportViewer1.RefreshReport();

            if (IsDirectPrint)
            {
                ReportPrinting reportPrinting = new ReportPrinting();
                reportPrinting.Export(this.reportViewer1.LocalReport);
                reportPrinting.Print();

                this.Close();
            }
        }
    }
}