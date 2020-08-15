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
using IMS_Client_2.Sales;

namespace IMS_Client_2.Settings
{
    public partial class frmOtherSetting : Form
    {
        public frmOtherSetting()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
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
            ClearAll();

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
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT StoreID, StoreName FROM " + clsUtility.DBName + ".[dbo].[StoreMaster] WITH(NOLOCK) WHERE ISNULL(ActiveStatus,1)=1");
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
            if (ObjUtil.IsControlTextEmpty(cmbSelectPC))   //(cmbSelectPC.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select PC.", clsUtility.strProjectTitle);
                cmbSelectPC.Focus();
                return false;

            }
            else if (ObjUtil.IsControlTextEmpty(cmbStoreCategory))   //(cmbStoreCategory.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select Store Category.", clsUtility.strProjectTitle);
                cmbStoreCategory.Focus();
                return false;

            }
            else if (ObjUtil.IsControlTextEmpty(cmbStoreName))    //(cmbStoreName.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please Select Store Name.", clsUtility.strProjectTitle);
                cmbStoreName.Focus();
                return false;
            }
            else if (IsDataExist())
            {
                clsUtility.ShowInfoMessage("Machine name is already mapped to this Store.", clsUtility.strProjectTitle);
                cmbSelectPC.Focus();
                return false;
            }
            return true;
        }

        private bool IsDataExist()
        {
            //bool result = false;
            //int count = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping] WHERE  MachineName='" + cmbSelectPC.Text.ToString() + "' AND StoreID=" + cmbStoreName.SelectedValue.ToString() + " AND StoreID <>" + ID);

            int count = ObjDAL.CountRecords(clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping]", "MachineName='" + cmbSelectPC.Text.ToString() + "' " +
                "AND PC_Store_ID <>" + ID);

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Load_PC_Store_Mapping()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT RegistrationID, PcName FROM " + clsUtility.DBName + ".[dbo].[RegistrationDetails] WITH(NOLOCK)");
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
            //DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");

            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT TOP 1 * FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dt))
            {
                lblmsg.Visible = false;
                //cmbStoreName.SelectedValue = dt.Rows[0]["StoreID"].ToString();

                //we are binding store category by index value.
                //cmbStoreCategory.SelectedIndex = Convert.ToInt32(dt.Rows[0]["StoreCategory"]);

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

                txtImagePath.Text = dt.Rows[0]["ImagePath"] == DBNull.Value ? "" : dt.Rows[0]["ImagePath"].ToString();
                txtFileExtension.Text = dt.Rows[0]["Extension"] == DBNull.Value ? "" : dt.Rows[0]["Extension"].ToString();

                chkSalesManName.Checked = dt.Rows[0]["SalesManIDMandatory"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["SalesManIDMandatory"]);
                chkCustomerMobile.Checked = dt.Rows[0]["CustMobileMandatory"] == DBNull.Value ? false : Convert.ToBoolean(dt.Rows[0]["CustMobileMandatory"]);


                //if (dt.Rows[0]["ImagePath"] != DBNull.Value)
                //{
                //    txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
                //}
                //else
                //{
                //    //txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
                //    txtImagePath.Text = "";
                //}

                //if (dt.Rows[0]["Extension"] != DBNull.Value)
                //{
                //    txtFileExtension.Text = dt.Rows[0]["Extension"].ToString();
                //}
                //else
                //{
                //    txtFileExtension.Text = "";
                //}
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
            ID = 0;
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

        private void LoadData()
        {
            //DataTable dt = ObjCon.ExecuteSelectStatement("SELECT PSM.PC_Store_ID,PSM.StoreID, PSM.MachineName, SM.StoreName,SM.StoreID" + ",(CASE WHEN PSM.StoreCategory = 1 THEN 'Warehouse' WHEN PSM.StoreCategory = 0 THEN 'Normal Store' END) [StoreCategory] FROM " + clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping] PSM " + "INNER JOIN " + clsUtility.DBName + ".[dbo].[StoreMaster] SM ON SM.StoreID = PSM.StoreID ORDER BY PSM.PC_Store_ID");

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".[dbo].SPR_Get_PCStoreMapping");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
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
                //DataTable dtFooterNote = ObjDAL.ExecuteSelectStatement("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK)");

                int a = ObjDAL.CountRecords(clsUtility.DBName + ".[dbo].[DefaultStoreSetting]");
                if (a > 0) // if data found for the PC thenupdate
                {
                    //ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.DefaultStoreSetting SET InvoiceFooterNote =N'" + txtFooterNote.Text + "', UserArabicNumbers='" + chkArabicPrice.Checked.ToString() + "', ImagePath='" + txtImagePath.Text + "', Extension='" + GetExtension() + "'");

                    ObjDAL.UpdateColumnData("InvoiceFooterNote", SqlDbType.NVarChar, "N" + txtFooterNote.Text);
                    ObjDAL.UpdateColumnData("UserArabicNumbers", SqlDbType.Bit, chkArabicPrice.Checked);
                    ObjDAL.UpdateColumnData("ImagePath", SqlDbType.NVarChar, txtImagePath.Text);
                    ObjDAL.UpdateColumnData("Extension", SqlDbType.NVarChar, GetExtension());
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ObjDAL.UpdateColumnData("SalesManIDMandatory ", SqlDbType.Bit, chkSalesManName.Checked);
                    ObjDAL.UpdateColumnData("CustMobileMandatory ", SqlDbType.Bit, chkCustomerMobile.Checked);
                    ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DefaultStoreSetting", "1=1");

                    clsUtility.ShowInfoMessage("Settings has been updated.", clsUtility.strProjectTitle);
                }
                else
                {
                    // else insert.
                    ObjDAL.SetColumnData("InvoiceFooterNote", SqlDbType.NVarChar, "N" + txtFooterNote.Text);
                    ObjDAL.SetColumnData("UserArabicNumbers", SqlDbType.Bit, chkArabicPrice.Checked);
                    ObjDAL.SetColumnData("ImagePath", SqlDbType.NVarChar, txtImagePath.Text);
                    ObjDAL.SetColumnData("Extension", SqlDbType.NVarChar, GetExtension());
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.SetColumnData("SalesManIDMandatory ", SqlDbType.Bit, chkSalesManName.Checked);
                    ObjDAL.SetColumnData("CustMobileMandatory ", SqlDbType.Bit,chkCustomerMobile.Checked);

                    int r = ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[DefaultStoreSetting]", false);
                    if (r > 0)
                    {
                        clsUtility.ShowInfoMessage("Settings has been saved.", clsUtility.strProjectTitle);
                    }
                    lblmsg.Visible = false;
                }


                Sales_Invoice._Is_SalesManIDMandat = chkSalesManName.Checked;
                Sales_Invoice._Is_CustomerMobileMandat = chkCustomerMobile.Checked;
            
            }
        }

        private void btnFooterCancel_Click(object sender, EventArgs e)
        {
            LoadStore();
            BindStoreSettingData();
        }

        private void btnStorePopup_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master) || clsUtility.IsAdmin)
            {
                Masters.Store_Master Obj = new Masters.Store_Master();
                Obj.ShowDialog();
                LoadStore();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (cmbBarcodPrinter.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select your BarCode Printer.", clsUtility.strProjectTitle);
                cmbBarcodPrinter.Focus();
                return;
            }
            else if (cmbInvoicePrinter.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select your Invoice Printer.", clsUtility.strProjectTitle);
                cmbInvoicePrinter.Focus();
                return;
            }
            SavePrinterSetting();
        }
        private void BindPrinterDetails()
        {
            DataTable dtPrinter = ObjDAL.ExecuteSelectStatement("SELECT BarCodePrinter, InvoicePrinter FROM " + clsUtility.DBName + ".dbo.[tblPrinterSetting] WITH(NOLOCK) WHERE MachineName = '" + txtMachineName.Text + "'");
            if (ObjUtil.ValidateTable(dtPrinter))
            {
                cmbBarcodPrinter.SelectedItem = dtPrinter.Rows[0]["BarCodePrinter"].ToString();
                cmbInvoicePrinter.SelectedItem = dtPrinter.Rows[0]["InvoicePrinter"].ToString();
            }
        }
        private void SavePrinterSetting()
        {
            int count = ObjDAL.CountRecords(clsUtility.DBName + ".[dbo].[tblPrinterSetting]", "MachineName='" + Environment.MachineName + "'");
            if (count == 0)
            {
                ObjDAL.SetColumnData("MachineName", SqlDbType.NVarChar, Environment.MachineName);
                ObjDAL.SetColumnData("BarCodePrinter", SqlDbType.NVarChar, cmbBarcodPrinter.SelectedItem.ToString());
                ObjDAL.SetColumnData("InvoicePrinter", SqlDbType.NVarChar, cmbInvoicePrinter.SelectedItem.ToString());
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblPrinterSetting", false);
            }
            else
            {
                //ObjCon.UpdateColumnData("MachineName", SqlDbType.NVarChar, Environment.MachineName);
                ObjDAL.UpdateColumnData("BarCodePrinter", SqlDbType.NVarChar, cmbBarcodPrinter.SelectedItem.ToString());
                ObjDAL.UpdateColumnData("InvoicePrinter", SqlDbType.NVarChar, cmbInvoicePrinter.SelectedItem.ToString());
                ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.UpdateData(clsUtility.DBName + ".dbo.tblPrinterSetting", "MachineName='" + Environment.MachineName + "'");
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
                    ObjDAL.SetColumnData("StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjDAL.SetColumnData("MachineName", SqlDbType.NVarChar, cmbSelectPC.Text);
                    ObjDAL.SetColumnData("StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    int r = ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblPC_Store_Mapping]", false);
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
                    ObjDAL.UpdateColumnData("MachineName", SqlDbType.NVarChar, cmbSelectPC.Text);
                    ObjDAL.UpdateColumnData("StoreID", SqlDbType.Int, cmbStoreName.SelectedValue);
                    ObjDAL.UpdateColumnData("StoreCategory", SqlDbType.Int, cmbStoreCategory.SelectedIndex);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.tblPC_Store_Mapping", "PC_Store_ID = " + ID) > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                        clsUtility.ShowInfoMessage("'" + cmbSelectPC.Text + "' Data has been Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        groupBox1.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + cmbSelectPC.Text + "' Data has not been Updated", clsUtility.strProjectTitle);
                    }
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["PC_Store_ID"].Visible = false;
            dataGridView1.Columns["StoreID"].Visible = false;
            dataGridView1.Columns["StoreCategoryID"].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmOtherSetting, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + cmbSelectPC.Text + "' Machine ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.tblPC_Store_Mapping", "PC_Store_ID = " + ID) > 0)
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
                        ObjDAL.ResetData();
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
                    //cmbStoreCategory.SelectedIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StoreCategoryID"].Value);
                    cmbStoreName.SelectedValue = dataGridView1.SelectedRows[0].Cells["StoreID"].Value.ToString();
                    cmbStoreCategory.SelectedIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StoreCategoryID"].Value);

                    groupBox1.Enabled = false;
                    cmbSelectPC.Focus();
                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.Message, clsUtility.strProjectTitle);
                }
            }
        }

        private void cmbStoreName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStoreName.SelectedIndex != -1)
            {
                int stroreID = cmbStoreName.SelectedValue == DBNull.Value ? 0 : Convert.ToInt32(cmbStoreName.SelectedValue);
                string str = "SELECT StoreCategory FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK) WHERE StoreID=" + stroreID;

                int _stID = ObjDAL.ExecuteScalarInt(str);
                cmbStoreCategory.SelectedIndex = _stID;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {

        }
        private void SavemendatorySetting(bool SalesMan, bool CustMobile)
        {

           

        }
    }
}