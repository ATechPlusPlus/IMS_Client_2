using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Settings
{
    public partial class frmOtherSetting : Form
    {
        public frmOtherSetting()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmOtherSetting_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnFooterCancel.BackgroundImage = B_Leave;
            btnFooterSave.BackgroundImage = B_Leave;
            btnPrinterSave.BackgroundImage = B_Leave;
            btnPrinterCancel.BackgroundImage = B_Leave;

            txtPCName.Text = Environment.MachineName;
            txtMachineName.Text = Environment.MachineName;
            LoadStore();
            BindStoreSettingData();
            LoadPrinter();
            BindPrinterDetails();
        }
        private void LoadPrinter()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbBarcodPrinter.Items.Add(printer);
                cmbInvoicePrinter.Items.Add(printer);
            }
        }

        private void LoadStore()
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT StoreID, StoreName FROM " + clsUtility.DBName + ".[dbo].[StoreMaster] WITH(NOLOCK) ");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbStoreName.DataSource = dt;
                cmbStoreName.DisplayMember = "StoreName";
                cmbStoreName.ValueMember = "StoreID";
                cmbStoreName.SelectedIndex = -1;
            }
        }
        private void BindStoreSettingData()
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
            if (ObjUtil.ValidateTable(dt))
            {
                lblmsg.Visible = false;
                cmbStoreName.SelectedValue = dt.Rows[0]["StoreID"].ToString();

                //we are binding store category by index value.
                cmbStoreCategory.SelectedIndex = Convert.ToInt32(dt.Rows[0]["StoreCategory"]);

                if (dt.Rows[0]["InvoiceFooterNote"] != DBNull.Value)
                {
                    txtFooterNote.Text = dt.Rows[0]["InvoiceFooterNote"].ToString();
                }
                else
                {
                    txtFooterNote.Text = "";
                }

                if (dt.Rows[0]["UserArabicNumbers"] != DBNull.Value)
                {
                    if ((Convert.ToBoolean(dt.Rows[0]["UserArabicNumbers"])))
                    {
                        chkArabicPrice.Checked = true;
                    }
                    else
                    {
                        chkArabicPrice.Checked = false;
                    }
                }
                else
                {
                    chkArabicPrice.Checked = false;
                }

                if (dt.Rows[0]["ImagePath"] != DBNull.Value)
                {
                    txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
                }
                else
                {
                    txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
                }

                if (dt.Rows[0]["Extension"] != DBNull.Value)
                {
                    txtFileExtension.Text = dt.Rows[0]["Extension"].ToString();
                }
                else
                {
                    txtFileExtension.Text = "";
                }
            }
            else
            {
                lblmsg.Visible = true;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (txtPCName.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please enter PC name.", clsUtility.strProjectTitle);
                    txtPCName.Focus();
                    return;

                }
                else if (cmbStoreCategory.SelectedIndex == -1)
                {
                    clsUtility.ShowInfoMessage("Please Select Store Category.", clsUtility.strProjectTitle);
                    cmbStoreCategory.Focus();
                    return;

                }
                else if (cmbStoreName.SelectedIndex == -1)
                {
                    clsUtility.ShowInfoMessage("Please Select Store Name.", clsUtility.strProjectTitle);
                    cmbStoreName.Focus();
                    return;
                }

                int result = ObjCon.ExecuteScalarInt("SELECT count(1) FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName='" + txtPCName.Text + "'");
                if (result > 0) // if data found for the PC thenupdate
                {
                    ObjCon.UpdateColumnData("StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjCon.UpdateColumnData("MachineName", SqlDbType.NVarChar, txtPCName.Text);
                    ObjCon.UpdateColumnData("StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);

                    int r = ObjCon.UpdateData(clsUtility.DBName + ".[dbo].[DefaultStoreSetting]", "MachineName='" + txtPCName.Text + "'");
                    if (r > 0)
                    {
                        clsUtility.ShowInfoMessage("Default store settings has been updated.", clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    // else insert.
                    ObjCon.SetColumnData("StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjCon.SetColumnData("MachineName", SqlDbType.NVarChar, txtPCName.Text);
                    ObjCon.SetColumnData("StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);

                    int r = ObjCon.InsertData(clsUtility.DBName + ".[dbo].[DefaultStoreSetting]", false);
                    if (r > 0)
                    {
                        clsUtility.ShowInfoMessage("Default store settings has been saved.", clsUtility.strProjectTitle);
                    }
                    lblmsg.Visible = false;
                }
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

        private void cmbStoreCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStoreCategory.SelectedIndex == 0) // Normal store
            {
                DataTable dt = ObjCon.ExecuteSelectStatement("SELECT StoreID, StoreName FROM " + clsUtility.DBName + ".[dbo].[StoreMaster] WITH(NOLOCK) WHERE ISNULL(StoreCategory,0) = 0");
                if (ObjUtil.ValidateTable(dt))
                {
                    cmbStoreName.DataSource = dt;
                    cmbStoreName.DisplayMember = "StoreName";
                    cmbStoreName.ValueMember = "StoreID";
                    cmbStoreName.SelectedIndex = -1;
                }
            }
            else if (cmbStoreCategory.SelectedIndex == 1) // Wearhouse
            {
                DataTable dt = ObjCon.ExecuteSelectStatement("SELECT StoreID, StoreName FROM " + clsUtility.DBName + ".[dbo].[StoreMaster] WITH(NOLOCK) WHERE StoreCategory = 1");
                if (ObjUtil.ValidateTable(dt))
                {
                    cmbStoreName.DataSource = dt;
                    cmbStoreName.DisplayMember = "StoreName";
                    cmbStoreName.ValueMember = "StoreID";
                    cmbStoreName.SelectedIndex = -1;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LoadStore();
            BindStoreSettingData();
        }

        private void txtPCName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtPCName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private string GetExtension()
        {
            if (txtFileExtension.Text.Trim().StartsWith("."))
            {
                return txtFileExtension.Text;
            }
            else
            {
                return "." + txtFileExtension.Text;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                DataTable dtFooterNote = ObjCon.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK)");
                if (ObjUtil.ValidateTable(dtFooterNote)) // if data found for the PC thenupdate
                {
                    ObjCon.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.DefaultStoreSetting SET InvoiceFooterNote =N'" + txtFooterNote.Text + "', UserArabicNumbers='" + chkArabicPrice.Checked.ToString() + "', ImagePath='" + txtImagePath.Text + "', Extension='" + GetExtension() + "'");
                    clsUtility.ShowInfoMessage("Settings has been updated.", clsUtility.strProjectTitle);
                }
                else
                {
                    // else insert.
                    ObjCon.SetColumnData("InvoiceFooterNote", SqlDbType.NVarChar, "N" + txtFooterNote.Text);
                    ObjCon.SetColumnData("UserArabicNumbers", SqlDbType.Bit, chkArabicPrice.Checked);
                    ObjCon.SetColumnData("ImagePath", SqlDbType.NVarChar, txtImagePath.Text);
                    ObjCon.SetColumnData("Extension", SqlDbType.NVarChar, GetExtension());

                    int r = ObjCon.InsertData(clsUtility.DBName + ".[dbo].[DefaultStoreSetting]", false);
                    if (r > 0)
                    {
                        clsUtility.ShowInfoMessage("Settings has been saved.", clsUtility.strProjectTitle);
                    }
                    lblmsg.Visible = false;
                }
            }
        }

        private void btnFooterCancel_Click(object sender, EventArgs e)
        {
            LoadStore();
            BindStoreSettingData();
        }

        private void btnStorePopup_Click(object sender, EventArgs e)
        {
            Masters.Store_Master Obj = new Masters.Store_Master();
            Obj.ShowDialog();
            LoadStore();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cmbBarcodPrinter.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select your printer.", clsUtility.strProjectTitle);
                return;
            }
            else if (cmbInvoicePrinter.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select your function.", clsUtility.strProjectTitle);
                return;
            }
            SavePrinterSetting();

        }
        private void BindPrinterDetails()
        {
            DataTable dtPrinter = ObjCon.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".dbo.[tblPrinterSetting] WHERE MachineName = '" + txtMachineName.Text + "'");
            if (ObjUtil.ValidateTable(dtPrinter))
            {
                cmbBarcodPrinter.SelectedItem = dtPrinter.Rows[0]["BarCodePrinter"].ToString();
                cmbInvoicePrinter.SelectedItem = dtPrinter.Rows[0]["InvoicePrinter"].ToString();
            }
        }
        private void SavePrinterSetting()
        {
            int count = ObjCon.CountRecords(clsUtility.DBName + ".[dbo].[tblPrinterSetting]", "MachineName='" + Environment.MachineName + "'");
            if (count == 0)
            {
                ObjCon.SetColumnData("MachineName", SqlDbType.NVarChar, Environment.MachineName);
                ObjCon.SetColumnData("BarCodePrinter", SqlDbType.NVarChar, cmbBarcodPrinter.SelectedItem.ToString());
                ObjCon.SetColumnData("InvoicePrinter", SqlDbType.NVarChar, cmbInvoicePrinter.SelectedItem.ToString());
                ObjCon.SetColumnData("CreateBy", SqlDbType.Int, clsUtility.LoginID);
                ObjCon.InsertData(clsUtility.DBName + ".dbo.tblPrinterSetting", false);
            }
            else
            {
                ObjCon.UpdateColumnData("MachineName", SqlDbType.NVarChar, Environment.MachineName);
                ObjCon.UpdateColumnData("BarCodePrinter", SqlDbType.NVarChar, cmbBarcodPrinter.SelectedItem.ToString());
                ObjCon.UpdateColumnData("InvoicePrinter", SqlDbType.NVarChar, cmbInvoicePrinter.SelectedItem.ToString());
                ObjCon.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                ObjCon.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjCon.UpdateData(clsUtility.DBName + ".dbo.tblPrinterSetting", "MachineName='" + Environment.MachineName + "'");
            }
            clsUtility.ShowInfoMessage("Printer settings has been saved.", clsUtility.strProjectTitle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmbInvoicePrinter.SelectedIndex = -1;
            cmbBarcodPrinter.SelectedIndex = -1;

        }
    }
}