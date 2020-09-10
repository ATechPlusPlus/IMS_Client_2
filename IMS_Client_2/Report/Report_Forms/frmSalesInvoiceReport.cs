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
using System.Drawing.Printing;
using IMS_Client_2.Barcode;
using System.IO;

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

        private bool IsArabicEnabled()
        {
            int count = ObjCon.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) where ISNULL(UserArabicNumbers,0)=1 and MachineName='" + Environment.MachineName + "'");
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void frmSalesInvoice_Load(object sender, EventArgs e)
        {
            string query = "";
            string strQueryHeader_Footer = "";
            string PaymentQuery = "";
            string VoucherNumber = "NA";
            if (IsArabicEnabled())
            {
                query = "SELECT p1.ProductName," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.QTY) as QTY," +
             "" + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.Rate) as Rate," +
             "" + clsUtility.DBName + ".dbo.fun_ToArabicNum((s1.Qty*s1.Rate)) AS Total,ps.barcodeNo as BarNumber FROM " + clsUtility.DBName + ".[dbo].[SalesDetails] s1 JOIN " + clsUtility.DBName + ".dbo.ProductMaster p1 " +
            " ON s1.ProductID = p1.ProductID " +
            "  JOIN " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster ps" +
            "  ON s1.SubProductID=ps.SubProductID AND s1.ProductID = ps.ProductID AND ps.colorID=s1.ColorID AND ps.SizeID=s1.SizeID " +
            "  AND ps.StoreID=(select ShopeID from SalesInvoiceDetails where Id=s1.InvoiceID) WHERE s1.InvoiceID = " + InvoiceID;

                PaymentQuery = " select PaymentType," + clsUtility.DBName + ".dbo.fun_ToArabicNum(Amount) as Amount from " + clsUtility.DBName + ".dbo.[tblSalesPayment]  where SalesInvoiceID=" + InvoiceID;
            }
            else
            {
                query = "SELECT p1.ProductName,s1.QTY," +
                "s1.Rate," +
                "(s1.Qty*s1.Rate) AS Total,ps.barcodeNo as BarNumber FROM " + clsUtility.DBName + ".[dbo].[SalesDetails] s1 JOIN " + clsUtility.DBName + ".dbo.ProductMaster p1 " +
               " ON s1.ProductID = p1.ProductID " +
               "  JOIN " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster ps" +
               "  ON s1.SubProductID=ps.SubProductID AND s1.ProductID = ps.ProductID AND s1.ColorID=ps.ColorID AND s1.SizeID=ps.SizeID " +
               " AND  ps.StoreID=(select ShopeID from SalesInvoiceDetails where Id=s1.InvoiceID)  WHERE s1.InvoiceID = " + InvoiceID;

                PaymentQuery = "select PaymentTYpe,Amount from " + clsUtility.DBName + ".dbo.[tblSalesPayment] WITH(NOLOCK)  where SalesInvoiceID=" + InvoiceID;
            }

            DataTable dtSalesDetails = ObjCon.ExecuteSelectStatement(query);
            DataTable dtPaymentDetails = ObjCon.ExecuteSelectStatement(PaymentQuery);

            if (IsArabicEnabled())
            {
                strQueryHeader_Footer = "SELECT s1.InvoiceNumber,s1.InvoiceDate, c1.Name AS CustName,e1.Name AS empName,st1.StoreName AS StoreName," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.SubTotal) as SubTotal," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.Discount) as Discount," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.Tax) as Tax," + clsUtility.DBName + ".dbo.fun_ToArabicNum(s1.GrandTotal) as GrandTotal,s1.PaymentMode,s1.PaymentAutoID,c1.PhoneNo AS CustomerMobile,s1.CashTendered, s1.Change,s1.VoucherNo FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails s1 left JOIN " +
                                         " " + clsUtility.DBName + ".dbo.EmployeeDetails e1 ON s1.SalesMan = e1.EmpID left JOIN" +
                                         " " + clsUtility.DBName + ".[dbo].[CustomerMaster] c1 ON s1.CustomerID = c1.CustomerID left join" +
                                         " " + clsUtility.DBName + ".dbo.StoreMaster st1 ON st1.StoreID = s1.ShopeID WHERE s1.Id=" + InvoiceID;
            }
            else
            {
                strQueryHeader_Footer = "SELECT s1.InvoiceNumber,s1.InvoiceDate, c1.Name AS CustName,e1.Name AS empName,st1.StoreName AS StoreName,st1.Place [Address],s1.SubTotal,s1.Discount,s1.Tax,s1.GrandTotal,s1.PaymentMode,s1.PaymentAutoID,c1.PhoneNo AS CustomerMobile,s1.CashTendered,s1.Change, s1.VoucherNo FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails s1 left JOIN " +
                                                         " " + clsUtility.DBName + ".dbo.EmployeeDetails e1 ON s1.SalesMan = e1.EmpID left JOIN" +
                                                         " " + clsUtility.DBName + ".[dbo].[CustomerMaster] c1 ON s1.CustomerID = c1.CustomerID left join" +
                                                         " " + clsUtility.DBName + ".dbo.StoreMaster st1 ON st1.StoreID = s1.ShopeID WHERE s1.Id=" + InvoiceID;
            }

            DataTable dtSalesHeader_Footer = ObjCon.ExecuteSelectStatement(strQueryHeader_Footer);
            if (dtSalesHeader_Footer.Rows.Count>0)
            {
                VoucherNumber = dtSalesHeader_Footer.Rows[0]["VoucherNo"].ToString();
                if (VoucherNumber.Trim().Length==0)
                {
                    VoucherNumber = "NA";
                }
            }
            Bitmap bmpBarCode = Barcode.clsBarCodeUtility.GenerateBarCode(dtSalesHeader_Footer.Rows[0]["InvoiceNumber"].ToString());
            string imgPath = "";
            if (bmpBarCode != null)
            {
                imgPath = Application.StartupPath + "//BarCodeImg//" + dtSalesHeader_Footer.Rows[0]["InvoiceNumber"].ToString() + ".jpeg";

                if (!File.Exists(imgPath))
                {
                    bmpBarCode.Save(imgPath);
                }
            }
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("ds_SalesDetails", dtSalesDetails);
            ReportDataSource rds2 = new ReportDataSource("ds_InvoiceHeader", dtSalesHeader_Footer);
            ReportDataSource rds3 = new ReportDataSource("ds_Payment", dtPaymentDetails);

            string strcomName = "";
            string strAddress = "";
            DataTable dtCompinfo = ObjCon.ExecuteSelectStatement("SELECT CompanyName,[Address] FROM " + clsUtility.DBName + ".dbo.CompanyMaster WITH(NOLOCK)");

            if (dtCompinfo != null && dtCompinfo.Rows.Count > 0)
            {
                strcomName = dtCompinfo.Rows[0]["CompanyName"].ToString();
                //strAddress = dtCompinfo.Rows[0]["Address"].ToString();

                if (dtSalesHeader_Footer != null && dtSalesHeader_Footer.Rows.Count > 0)
                    strAddress = dtSalesHeader_Footer.Rows[0]["Address"].ToString();
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
                if (dtFooterNote.Rows[0]["InvoiceFooterNote"] != DBNull.Value)
                {
                    footerNoteDefault = dtFooterNote.Rows[0]["InvoiceFooterNote"].ToString();
                }
            }
            else
            {
                strcomName = "Default Store/Shop Name";
                strAddress = "Default Address Line. Road No, Block No, Pin Code and State.";
            }

            string NetAmt = "NA";
            try
            {
                DataTable dtNetAmount = ObjCon.ExecuteSelectStatement("select SUM(Amount) from " + clsUtility.DBName + ".dbo.[tblSalesPayment] WITH(NOLOCK) where SalesInvoiceID=" + InvoiceID);
                if (dtNetAmount != null && dtNetAmount.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(dtNetAmount.Rows[0][0].ToString()) == 0)
                    {
                        NetAmt = "Zero";
                    }
                    else
                    {
                        NetAmt = new Sales.NumberToEnglish().changeCurrencyToWords(dtNetAmount.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception)
            {
            }
            reportViewer1.LocalReport.EnableExternalImages = true;
            // creating the parameter with the extact name as in the report.
            ReportParameter param1 = new ReportParameter("CompanyName", strcomName, true);
            ReportParameter param2 = new ReportParameter("CompanyAddress", strAddress, true);
            ReportParameter param3 = new ReportParameter("Note", footerNoteDefault, true);
            ReportParameter param4 = new ReportParameter("NetAmount", NetAmt, true);
            ReportParameter param5 = new ReportParameter("BarCodeImage", imgPath, true);
            ReportParameter param6 = new ReportParameter("ParmVoucherNumber", VoucherNumber, true);

            // adding the parameter in the report dynamically
            reportViewer1.LocalReport.SetParameters(param1);
            reportViewer1.LocalReport.SetParameters(param2);
            reportViewer1.LocalReport.SetParameters(param3);
            reportViewer1.LocalReport.SetParameters(param4);
            reportViewer1.LocalReport.SetParameters(param5);
            reportViewer1.LocalReport.SetParameters(param6);

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.DataSources.Add(rds2);
            reportViewer1.LocalReport.DataSources.Add(rds3);

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();

            if (IsDirectPrint)
            {
                ReportPrinting reportPrinting = new ReportPrinting();
                reportPrinting.Export(this.reportViewer1.LocalReport);

                PrinterSettings printerSetting = new PrinterSettings();
                //  printerSetting.DefaultPageSettings.PaperSize = new PaperSize("IMS_Paper", 80, 297);

                if (clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.InvoicePrinter).Trim().Length == 0)
                {
                    bool b = clsUtility.ShowQuestionMessage("Printer Not Configured for Invoice. Do you want to print on default printer?", clsUtility.strProjectTitle);
                    if (b)
                    {
                        reportPrinting.Print();
                    }
                }
                else
                {
                    printerSetting.PrinterName = clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.InvoicePrinter);
                    reportPrinting.Print(printerSetting);
                }
                this.Close();
            }
        }
    }
}