using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreApp;
using IMS_Client_2.Barcode;
using IMS_Client_2.Settings;

namespace IMS_Client_2
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        bool IsLogOut = false;
        public int Login_History_ID = 0;
        public static int Home_MasterCashClosingID = 0;
        public static int Home_StoreID = 0;
        bool IsForceCloseCash = false;

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        static UserManagement.frmUserManagement ObjUserManag = new UserManagement.frmUserManagement();

        private void LoadCashStatus()
        {
            int pStoreID = Home_StoreID;
            DataTable dtCashMaster = ObjDAL.ExecuteSelectStatement("SELECT TOP 1 * FROM " + clsUtility.DBName + ".[dbo].[tblMasterCashClosing] WITH(NOLOCK) WHERE StoreID=" + pStoreID + " ORDER BY CashBoxDate DESC");

            if (ObjUtil.ValidateTable(dtCashMaster))
            {
                Home_MasterCashClosingID = Convert.ToInt32(dtCashMaster.Rows[0]["MasterCashClosingID"]);
                if (Convert.ToBoolean(dtCashMaster.Rows[0]["CashStatus"]))
                {
                    if (DateTime.Now.ToString("yyyy-MM-dd") == Convert.ToDateTime(dtCashMaster.Rows[0]["CashBoxDate"]).ToString("yyyy-MM-dd"))
                    {
                        btnOpenCash.Text = "View Details";
                    }
                    IsForceCloseCash = false;
                    label7.Text = "CLOSED";
                }
                else
                {
                    label7.Text = "OPEN";
                    btnOpenCash.Text = "View Details";
                    if (DateTime.Now.ToString("yyyy-MM-dd") != Convert.ToDateTime(dtCashMaster.Rows[0]["CashBoxDate"]).ToString("yyyy-MM-dd"))
                    {
                        IsForceCloseCash = true;
                        clsUtility.ShowInfoMessage("Please close your previous day cash box and Open a new cash box for today.", clsUtility.strProjectTitle);
                        return;
                    }
                    IsForceCloseCash = false;
                }
            }
            else
            {
                label7.Text = "NA";
            }
        }

        private void otherArtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtility.IsAdmin)
            {
                DB_backupRestore.cs.frmDatabaseMaintenance Obj = new DB_backupRestore.cs.frmDatabaseMaintenance();
                Obj.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void DisplayRegistrationInfo()
        {
            object ob = ObjDAL.ExecuteScalar("SELECT CONVERT(DATE,RegDate) RegDate FROM " + clsUtility.DBName + ".[dbo].[RegistrationDetails] WITH(NOLOCK) where PcName='" + Environment.MachineName + "'");
            if (ob != null)
            {
                lblRegistrationDate.Text = Convert.ToDateTime(ob).ToShortDateString();
            }
            else
            {
                lblRegistrationDate.Text = "NA";
            }

            object company = ObjDAL.ExecuteScalar("SELECT CompanyName FROM " + clsUtility.DBName + ".[dbo].[CompanyMaster] WITH(NOLOCK)");
            if (company != null)
            {
                lblLicensedTo.Text = company.ToString();
            }
            else
            {
                lblLicensedTo.Text = "NA";
            }
        }

        private void GetDefaultShop()
        {
            ObjDAL.SetStoreProcedureData("MachineName", SqlDbType.NChar, Environment.MachineName, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".[dbo].SPR_GetDefaultShopName");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    Home_StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                    lblShopName.Text = "Shop Name : " + dt.Rows[0]["StoreName"];
                }
                else
                {
                    lblShopName.Text = "Shop Name : " + "NA";
                }
            }
        }
        private void CreateBardCodeFolder()
        {
            try
            {
                string folderPath = Application.StartupPath + "//BarCodeImg";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowInfoMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void DeleteBarCodeFiles()
        {
            try
            {
                string folderPath = Application.StartupPath + "//BarCodeImg";
                if (Directory.Exists(folderPath))
                {
                    string[] strFiles = Directory.GetFiles(folderPath);

                    for (int i = 0; i < strFiles.Length; i++)
                    {
                        File.Delete(strFiles[i]);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            try
            {
                btnOpenCash.BackgroundImage = B_Leave;

                clsUtility.DBName = "IMS_Client_2";
                clsUtility.LoginID = 2;
                //clsUtility.IsAdmin = false;
                clsUtility.IsAdmin = true;
                clsUtility.strProjectTitle = "IMS";
                if (clsUtility.LoginID > 0)
                {
                    object ob = ObjDAL.ExecuteScalar("SELECT UserName from " + clsUtility.DBName + ".[dbo].[UserManagement] WITH(NOLOCK) WHERE UserID =" + clsUtility.LoginID);
                    lblLoginName.Text = "Login By : " + ob.ToString();
                }
                else
                {
                    lblLoginName.Text = "Login By : Test Admin";
                }
                lblVersion.Text = "Version : " + Application.ProductVersion;

                DisplayRegistrationInfo();
                GetDefaultShop();
                LoadCashStatus();
                CreateBardCodeFolder();
                DeleteBarCodeFiles();
            }
            catch { }
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

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsLogOut)
            {
                bool b = clsUtility.ShowQuestionMessage("Are you sure, you want to Exit?", clsUtility.strProjectTitle);
                if (b)
                {
                    ObjDAL.UpdateColumnData("LogOutTime", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ObjDAL.UpdateData(clsUtility.DBName + ".dbo.Login_History", "Login_History_ID = " + Login_History_ID);
                    IsLogOut = true;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void picLogOut_Click(object sender, EventArgs e)
        {
            if (clsUtility.ShowQuestionMessage("Are you sure to Logout?", clsUtility.strProjectTitle))
            {
                ObjDAL.UpdateColumnData("LogOutTime", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ObjDAL.UpdateData(clsUtility.DBName + ".dbo.Login_History", "Login_History_ID = " + Login_History_ID);
                frmLogin Obj = new frmLogin();
                Obj.BringToFront();
                Obj.Show();
                IsLogOut = true;
                this.Close();
            }
        }

        private void CategoryMaster_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Category_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Category_Master));
                if (!b)
                {
                    Masters.Category_Master Obj = new Masters.Category_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void StoreDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Store_Master));
                if (!b)
                {
                    Masters.Store_Master Obj = new Masters.Store_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SupplierDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Supplier_Details) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Supplier_Details));
                if (!b)
                {
                    Masters.Supplier_Details Obj = new Masters.Supplier_Details();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void EmployeeDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Employee_Details));
                if (!b)
                {
                    Masters.Employee_Details Obj = new Masters.Employee_Details();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void PurchaseInvoice_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Invoice) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.Purchase_Invoice));
                if (!b)
                {
                    Purchase.Purchase_Invoice Obj = new Purchase.Purchase_Invoice();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void PurchaseBillDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Bill_Details) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.Purchase_Bill_Details));
                if (!b)
                {
                    Purchase.Purchase_Bill_Details Obj = new Purchase.Purchase_Bill_Details();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SalesInvoice_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Sales_Invoice) || clsUtility.IsAdmin)
            {
                if (label7.Text == "OPEN")
                {
                    if (!IsForceCloseCash)
                    {
                        bool b = ObjUtil.IsAlreadyOpen(typeof(Sales.Sales_Invoice));
                        if (!b)
                        {
                            Sales.Sales_Invoice Obj = new Sales.Sales_Invoice();
                            Obj.Show();
                        }
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Please close your previous day cash box and Open a new cash box for today.", clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("There is no Opened Cash for today.", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SalesBillDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Sales_Bill_Details) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Sales.Sales_Bill_Details));
                if (!b)
                {
                    Sales.Sales_Bill_Details Obj = new Sales.Sales_Bill_Details();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void MaterialDetails_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Material_Details) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.Material_Details));
                if (!b)
                {
                    StockManagement.Material_Details Obj = new StockManagement.Material_Details();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void CurrencyValueSetting_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Currency_Value_Settings) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Settings.Currency_Value_Settings));
                if (!b)
                {
                    Settings.Currency_Value_Settings Obj = new Settings.Currency_Value_Settings();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void countryMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Country_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Country_Master));
                if (!b)
                {
                    Masters.Country_Master Obj = new Masters.Country_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void brandMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Brand_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Brand_Master));
                if (!b)
                {
                    Masters.Brand_Master Obj = new Masters.Brand_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void productMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Product_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Product_Master));
                if (!b)
                {
                    Masters.Product_Master Obj = new Masters.Product_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void defaultStoreSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting) || clsUtility.IsAdmin)
            {
                frmOtherSetting frmOtherSetting = new frmOtherSetting();
                frmOtherSetting.ShowDialog();
                GetDefaultShop();
                LoadCashStatus();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void CustomerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Customer_Master));
                if (!b)
                {
                    Masters.Customer_Master Obj = new Masters.Customer_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void ColorMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Color_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Color_Master));
                if (!b)
                {
                    Masters.Color_Master Obj = new Masters.Color_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SizeTypeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Size_Type_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Size_Type_Master));
                if (!b)
                {
                    Masters.Size_Type_Master Obj = new Masters.Size_Type_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void deliveringPurchaseBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.Delivering_Purchase_Bill));
                if (!b)
                {
                    Purchase.Delivering_Purchase_Bill Obj = new Purchase.Delivering_Purchase_Bill();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void sizeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Size_Master) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.Size_Master));
                if (!b)
                {
                    Masters.Size_Master Obj = new Masters.Size_Master();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Sales_Invoice) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Report.frmSalesInvoiceReport));
                if (!b)
                {
                    Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
                    frmSalesInvoice.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void BarcodeStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCodeDesigner) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Barcode.frmBarCodeDesigner));
                if (!b)
                {
                    Barcode.frmBarCodeDesigner frmBarCode = new IMS_Client_2.Barcode.frmBarCodeDesigner();
                    frmBarCode.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void barCodePrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCode) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Barcode.frmBarCode));
                if (!b)
                {
                    Barcode.frmBarCode frmBarCode = new Barcode.frmBarCode();
                    frmBarCode.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void postingDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Posting_Delivery) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.Posting_Delivery));
                if (!b)
                {
                    Purchase.Posting_Delivery obj = new Purchase.Posting_Delivery();
                    obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void diffBwPurchaseReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmDiffPurchaseReceived) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.frmDiffPurchaseReceived));
                if (!b)
                {
                    Purchase.frmDiffPurchaseReceived Obj = new Purchase.frmDiffPurchaseReceived();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SalesReport_ToolStrip_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmSalesReport) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Report.frmSalesReport));
                if (!b)
                {
                    Report.frmSalesReport frmSalesReport = new Report.frmSalesReport();
                    frmSalesReport.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void userCreationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool b = ObjUtil.IsAlreadyOpen(typeof(UserManagement.frmUserManagement));
            if (!b)
            {
                UserManagement.frmUserManagement ObjUserManag = new UserManagement.frmUserManagement();
                ObjUserManag.LoginStatus(clsUtility.LoginID, clsUtility.IsAdmin);
                ObjUserManag.Show();
            }
        }

        private void userRightsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtility.IsAdmin)
            {
                frmUserRights frmUserRights = new frmUserRights();
                frmUserRights.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void purchaseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmPurchaseDetails) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Purchase.frmPurchaseDetails));
                if (!b)
                {
                    Purchase.frmPurchaseDetails obj = new Purchase.frmPurchaseDetails();
                    obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DisplayRegistrationInfo();
        }

        private void replaceReturnItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label7.Text == "OPEN")
            {
                if (!IsForceCloseCash)
                {
                    Sales.frmReplaceReturnPopup obj = new Sales.frmReplaceReturnPopup();
                    obj.ShowDialog();
                    obj.Size = new Size(921, 743);
                }
                else
                {
                    clsUtility.ShowInfoMessage("Please close your previous day cash box and Open a new cash box for today.", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("There is no Opened Cash for today.", clsUtility.strProjectTitle);
            }
        }

        private void btnOpenCash_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmCloseShifWindow) || clsUtility.IsAdmin)
            {
                if (btnOpenCash.Text == "View Details")
                {
                    // Enter code for opening Cash clsoing window
                    Sales.frmCloseShifWindow Obj = new Sales.frmCloseShifWindow();
                    Obj.pMasterCashClosingID = Home_MasterCashClosingID;
                    Obj.ShowDialog();
                    LoadCashStatus();
                }
                else
                {
                    bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to open cash box ?", clsUtility.strProjectTitle);
                    if (result)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        System.Threading.Thread.Sleep(2000);
                        OpenCashBox();
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void OpenCashBox()
        {
            string NexCashNumber = GetCashNumber();
            int pStoreID = Home_StoreID;
            ObjDAL.SetColumnData("CashNo", SqlDbType.NVarChar, NexCashNumber);
            ObjDAL.SetColumnData("StoreID", SqlDbType.Int, pStoreID);
            ObjDAL.SetColumnData("CashBoxDate", SqlDbType.Date, DateTime.Now.ToString("yyyy-MM-dd"));
            ObjDAL.SetColumnData("EmployeeID", SqlDbType.Int, clsUtility.LoginID);
            ObjDAL.SetColumnData("CashStatus", SqlDbType.Bit, false);
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

            int result = ObjDAL.InsertData(clsUtility.DBName + ".[dbo].tblMasterCashClosing", true);
            if (result > 0)
            {
                Home_MasterCashClosingID = result;
                clsUtility.ShowInfoMessage("Cash Box has been Opened !", clsUtility.strProjectTitle);
                btnOpenCash.Text = "View Details";
                LoadCashStatus();
            }
        }
        private string GetCashNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            long LastID = (long)ObjDAL.ExecuteScalar("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Seq_CashNumber");
            return LastID.ToString();
        }

        private void storeRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtility.IsAdmin)
            {
                Other_Forms.frmStoreRights frmStoreRights = new Other_Forms.frmStoreRights();
                frmStoreRights.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void stockTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmStoreTransfer) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.frmStoreTransfer));
                if (!b)
                {
                    StockManagement.frmStoreTransfer frmStoreTransfer = new StockManagement.frmStoreTransfer();
                    frmStoreTransfer.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void brachReceiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmReceivedBranchTransfer) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.frmReceivedBranchTransfer));
                if (!b)
                {
                    StockManagement.frmReceivedBranchTransfer frmReceived = new StockManagement.frmReceivedBranchTransfer();
                    frmReceived.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void closeCashBandMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmCloseCashBandMaster) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.frmCloseCashBandMaster));
                if (!b)
                {
                    Masters.frmCloseCashBandMaster Obj = new Masters.frmCloseCashBandMaster();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void closeCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmCloseShifWindow) || clsUtility.IsAdmin)
            {
                Sales.frmCloseShifWindow Obj = new Sales.frmCloseShifWindow();
                Obj.pMasterCashClosingID = Home_MasterCashClosingID;
                Obj.ShowDialog();
                LoadCashStatus();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void transferWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmTransferWatch) || clsUtility.IsAdmin)
            {
                StockManagement.frmTransferWatch Obj = new StockManagement.frmTransferWatch();
                Obj.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void loadPettyCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Other_Forms.frmPettyCash));
                if (!b)
                {
                    Other_Forms.frmPettyCash Obj = new Other_Forms.frmPettyCash();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void pettyCashExpReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmPettyCashExpReport) || clsUtility.IsAdmin)
            {
                Report.frmPettyCashExpReport Obj = new Report.frmPettyCashExpReport();
                Obj.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void miniSalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.tblMiniSalesReport) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Report.Report_Forms.tblMiniSalesReport));
                if (!b)
                {
                    Report.Report_Forms.tblMiniSalesReport frmminiSalesReport = new Report.Report_Forms.tblMiniSalesReport();
                    frmminiSalesReport.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void stockDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmStockDetails) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.frmStockDetails));
                if (!b)
                {
                    StockManagement.frmStockDetails Obj = new StockManagement.frmStockDetails();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmDashBoard) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Other_Forms.frmDashBoard));
                if (!b)
                {
                    Other_Forms.frmDashBoard frmDashBoard = new Other_Forms.frmDashBoard();
                    frmDashBoard.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmItemWiseModelNo) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.frmItemWiseModelNo));
                if (!b)
                {
                    StockManagement.frmItemWiseModelNo frmItemWiseModelNo = new StockManagement.frmItemWiseModelNo();
                    frmItemWiseModelNo.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void branchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBranchSaleShorting) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(StockManagement.frmBranchSaleShorting));
                if (!b)
                {
                    StockManagement.frmBranchSaleShorting Obj = new StockManagement.frmBranchSaleShorting();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void barCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Barcode.frmManuallyBarCode));
                if (!b)
                {
                    Barcode.frmManuallyBarCode Obj = new Barcode.frmManuallyBarCode();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void f1CashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesInvoice_ToolStrip_Click(null, null);
        }

        private void quickBarcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmQuickBarCodePrint) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Barcode.frmQuickBarCodePrint));
                if (!b)
                {
                    frmQuickBarCodePrint frmQuickBarCodePrint = new frmQuickBarCodePrint();
                    frmQuickBarCodePrint.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void scanItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmScanInventory) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Inventory.frmScanInventory));
                if (!b)
                {
                    Inventory.frmScanInventory objCheckinventory = new Inventory.frmScanInventory();
                    objCheckinventory.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }

        }

        private void inventoryCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmScanInventoryList) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Inventory.frmScanInventoryList));
                if (!b)
                {
                    Inventory.frmScanInventoryList objCheckinventory = new Inventory.frmScanInventoryList();
                    objCheckinventory.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void companyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmCompanyMaster) || clsUtility.IsAdmin)
            {
                bool b = ObjUtil.IsAlreadyOpen(typeof(Masters.frmCompanyMaster));
                if (!b)
                {
                    Masters.frmCompanyMaster Obj = new Masters.frmCompanyMaster();
                    Obj.Show();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
    }
}