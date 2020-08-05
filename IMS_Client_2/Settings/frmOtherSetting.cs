using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        int ID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmOtherSetting_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
            btnFooterCancel.BackgroundImage = B_Leave;
            btnFooterSave.BackgroundImage = B_Leave;
            btnPrinterSave.BackgroundImage = B_Leave;
            btnPrinterCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            lblPCName.Text = Environment.MachineName;
            txtMachineName.Text = Environment.MachineName;
            LoadStore();
            Load_PC_Store_Mapping();
            BindStoreSettingData();
            LoadPrinter();
            BindPrinterDetails();
            LoadData();
            groupBox1.Enabled = false;
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

        private bool Validateform()
        {
            if (cmbSelectPC.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select PC.", clsUtility.strProjectTitle);
                cmbSelectPC.Focus();
                return false;

            }
            else if (cmbStoreCategory.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select Store Category.", clsUtility.strProjectTitle);
                cmbStoreCategory.Focus();
                return false;

            }
            else if (cmbStoreName.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select Store Name.", clsUtility.strProjectTitle);
                cmbStoreName.Focus();
                return false;
            }
            return true;
        }
        private void Load_PC_Store_Mapping()
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT RegistrationID, PcName FROM " + clsUtility.DBName + ".[dbo].[RegistrationDetails] WITH(NOLOCK) ");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbSelectPC.DataSource = dt;
                cmbSelectPC.DisplayMember = "PcName";
                cmbSelectPC.ValueMember = "RegistrationID";
                cmbSelectPC.SelectedIndex = -1;
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

        private void ClearAll()
        {
            cmbSelectPC.SelectedIndex = -1;
            cmbStoreCategory.SelectedIndex = -1;
            cmbStoreName.SelectedIndex = -1;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            groupBox1.Enabled = true;
            cmbSelectPC.Focus();
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

        private void LoadData()
        {
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            DataTable dt = null;

            dt = ObjCon.ExecuteSelectStatement("SELECT PSM.PC_Store_ID,PSM.SM_StoreID, PSM.MachineName, SM.StoreName,SM.StoreID" + ",(CASE WHEN PSM.SM_StoreCategory = 1 THEN 'Warehouse' WHEN PSM.SM_StoreCategory = 0 THEN 'Normal Store' END) [StoreCategory] FROM " + clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping] PSM " +
                  "INNER JOIN " + clsUtility.DBName + ".[dbo].[StoreMaster] SM ON SM.StoreID = PSM.SM_StoreID ORDER BY PSM.PC_Store_ID");


            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
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
            //if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master) || clsUtility.IsAdmin)
            //{
            //}
            //else
            //{
            //    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            //}
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
            DataTable dtPrinter = ObjCon.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".dbo.[tblPrinterSetting] WITH(NOLOCK) WHERE MachineName = '" + txtMachineName.Text + "'");
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
                ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {

                if (Validateform())
                {
                    ObjCon.SetColumnData("SM_StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjCon.SetColumnData("MachineName", SqlDbType.NVarChar, cmbSelectPC.Text);
                    ObjCon.SetColumnData("SM_StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);

                    int r = ObjCon.InsertData(clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping]", false);
                    if (r > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                        clsUtility.ShowInfoMessage("Default store settings has been saved.", clsUtility.strProjectTitle);
                        ClearAll();
                        groupBox1.Enabled = false;
                        LoadData();
                    }
                    lblmsg.Visible = false;
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                groupBox1.Enabled = true;
                cmbSelectPC.Focus();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    ObjCon.UpdateColumnData("MachineName", SqlDbType.NVarChar, cmbSelectPC.Text);
                    ObjCon.UpdateColumnData("SM_StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjCon.UpdateColumnData("SM_StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);

                    if (ObjCon.UpdateData(clsUtility.DBName + ".dbo.tblPC_Store_Mapping", "PC_Store_ID = " + ID) > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                        clsUtility.ShowInfoMessage("'" + lblPCName.Text + "' Data has been Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        groupBox1.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + lblPCName.Text + "' Data has not been Updated", clsUtility.strProjectTitle);
                    }
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["PC_Store_ID"].Visible = false;
            dataGridView1.Columns["SM_StoreID"].Visible = false;
            dataGridView1.Columns["StoreID"].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + cmbSelectPC.Text + "' Machine ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjCon.DeleteData(clsUtility.DBName + ".dbo.tblPC_Store_Mapping", "MachineName='" + cmbSelectPC.Text + "'") > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + cmbSelectPC.Text + "' Machine has been deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        groupBox1.Enabled = false;
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + cmbSelectPC.Text + "' Machine is not deleted  ", clsUtility.strProjectTitle);
                        ObjCon.ResetData();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                ClearAll();
                LoadData();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                groupBox1.Enabled = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PC_Store_ID"].Value);
                    cmbSelectPC.Text = dataGridView1.SelectedRows[0].Cells["MachineName"].Value.ToString();
                    cmbStoreCategory.SelectedItem = dataGridView1.SelectedRows[0].Cells["StoreCategory"].Value.ToString();
                    cmbStoreName.SelectedValue = dataGridView1.SelectedRows[0].Cells["SM_StoreID"].Value.ToString();
                    
                    groupBox1.Enabled = false;
                    cmbSelectPC.Focus();
                }
                catch { }
            }
        }
    }
}
