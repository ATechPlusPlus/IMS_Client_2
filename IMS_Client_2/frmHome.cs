using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreApp;
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

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        static UserManagement.frmUserManagement ObjUserManag = new UserManagement.frmUserManagement();

        private void userCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void otherArtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmDatabaseMaintenance) || clsUtility.IsAdmin)
            {
                DB_backupRestore.cs.frmDatabaseMaintenance Obj = new DB_backupRestore.cs.frmDatabaseMaintenance();
                Obj.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            try
            {
                clsUtility.DBName = "IMS_Client_2";
                clsUtility.LoginID = 1;
                //clsUtility.IsAdmin = false;
                clsUtility.IsAdmin = true;
                clsUtility.strProjectTitle = "IMS";
                if (clsUtility.LoginID > 0)
                {

                    object ob = ObjDAL.ExecuteScalar("SELECT UserName from " + clsUtility.DBName + ".[dbo].[UserManagement] WHERE UserID =" + clsUtility.LoginID);
                    lblLoginName.Text = "Login By : " + ob.ToString();
                }
                else
                {
                    lblLoginName.Text = "Login By : Test Admin";
                }
                lblVersion.Text = "Version : " + Application.ProductVersion;
            }
            catch { }
        }

        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsLogOut)
            {
                bool b = clsUtility.ShowQuestionMessage("Are you sure, you want to Exit?", clsUtility.strProjectTitle);
                if (b)
                {
                    ObjDAL.UpdateColumnData("LogOutTime", SqlDbType.DateTime, DateTime.Now);
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
                ObjDAL.UpdateColumnData("LogOutTime", SqlDbType.DateTime, DateTime.Now);
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
                Masters.Category_Master Obj = new Masters.Category_Master();
                Obj.Show();
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
                Masters.Store_Master Obj = new Masters.Store_Master();
                Obj.Show();
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
                Masters.Supplier_Details Obj = new Masters.Supplier_Details();
                Obj.Show();
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
                Masters.Employee_Details Obj = new Masters.Employee_Details();
                Obj.Show();
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
                Purchase.Purchase_Invoice Obj = new Purchase.Purchase_Invoice();
                Obj.Show();
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
                Purchase.Purchase_Bill_Details Obj = new Purchase.Purchase_Bill_Details();
                Obj.Show();
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
                Sales.Sales_Invoice Obj = new Sales.Sales_Invoice();
                Obj.Show();
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
                Sales.Sales_Bill_Details Obj = new Sales.Sales_Bill_Details();
                Obj.Show();
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
                StockManagement.Material_Details Obj = new StockManagement.Material_Details();
                Obj.Show();
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
                Settings.Currency_Value_Settings Obj = new Settings.Currency_Value_Settings();
                Obj.Show();
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
                Masters.Country_Master Obj = new Masters.Country_Master();
                Obj.Show();
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
                Masters.Brand_Master Obj = new Masters.Brand_Master();
                Obj.Show();
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
                Masters.Product_Master Obj = new Masters.Product_Master();
                Obj.Show();
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
                Masters.Customer_Master Obj = new Masters.Customer_Master();
                Obj.Show();
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
                Masters.Color_Master Obj = new Masters.Color_Master();
                Obj.Show();
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
                Masters.Size_Type_Master Obj = new Masters.Size_Type_Master();
                Obj.Show();
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
                Purchase.Delivering_Purchase_Bill Obj = new Purchase.Delivering_Purchase_Bill();
                Obj.Show();
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
                Masters.Size_Master Obj = new Masters.Size_Master();
                Obj.Show();
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
                Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
                frmSalesInvoice.Show();
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
                Barcode.frmBarCodeDesigner frmBarCode = new IMS_Client_2.Barcode.frmBarCodeDesigner();
                frmBarCode.Show();
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
                Barcode.frmBarCode frmBarCode = new Barcode.frmBarCode();
                frmBarCode.Show();
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
                Purchase.Posting_Delivery obj = new Purchase.Posting_Delivery();
                obj.Show();
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
                Purchase.frmDiffPurchaseReceived Obj = new Purchase.frmDiffPurchaseReceived();
                Obj.Show();
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
                Report.frmSalesReport frmSalesReport = new Report.frmSalesReport();
                frmSalesReport.Show();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void userCreationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!ObjUserManag.IsHandleCreated)
            {
                ObjUserManag = null;
                ObjUserManag = new UserManagement.frmUserManagement();
                ObjUserManag.LoginStatus(clsUtility.LoginID, clsUtility.IsAdmin);
                ObjUserManag.Show();
                ObjUserManag.BringToFront();
            }
            ObjUserManag.LoginStatus(clsUtility.LoginID, clsUtility.IsAdmin);
            ObjUserManag.Show();
            ObjUserManag.BringToFront();
        }

        private void userRightsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserRights frmUserRights = new frmUserRights();
            frmUserRights.ShowDialog();
        }
    }
}