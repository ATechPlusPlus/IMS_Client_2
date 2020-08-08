using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Masters
{
    public partial class Supplier_Details : Form
    {
        public Supplier_Details()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        int ID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void ClearAll()
        {
            txtSupplierName.Clear();
            cmbCountry.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtFax.Clear();
            txtBankAdd.Clear();
            txtBankName.Clear();
            txtBankAccNo.Clear();
            cmbActiveStatus.SelectedIndex = -1;

            txtSupplierName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierName))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Name           ", clsUtility.strProjectTitle);
                txtSupplierName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbCountry))
            {
                clsUtility.ShowInfoMessage("Select Country for " + txtSupplierName.Text, clsUtility.strProjectTitle);
                cmbCountry.Focus();
                return false;
            }
            //else if (ObjUtil.IsControlTextEmpty(txtPhone))
            //{
            //    clsUtility.ShowInfoMessage("Enter Phone Number          ", clsUtility.strProjectTitle);
            //    txtPhone.Focus();
            //    return false;
            //}
            //else if (ObjUtil.IsControlTextEmpty(txtEmail))
            //{
            //    clsUtility.ShowInfoMessage("Enter E-Mail ID          ", clsUtility.strProjectTitle);
            //    txtEmail.Focus();
            //    return false;
            //}
            else if (ObjUtil.IsControlTextEmpty(cmbActiveStatus))
            {
                clsUtility.ShowInfoMessage("Select Active Status.", clsUtility.strProjectTitle);
                cmbActiveStatus.Focus();
                return false;
            }
            return true;
        }

        private bool DuplicateUser(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierName='" + txtSupplierName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierName='" + txtSupplierName.Text + "' AND SupplierID !=" + i);
            }
            if (a > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LoadData()
        {
            DataTable dt = null;
            dt = ObjDAL.ExecuteSelectStatement("SELECT S.SupplierID,S.SupplierName,S.CountryID,C.CountryName,(CASE S.ActiveStatus WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END) ActiveStatus,S.Phone,S.EmailID,S.BankName,S.BankAccountNo,S.BankAddress " +
              "FROM " + clsUtility.DBName + ".[dbo].[SupplierMaster] S " +
              "LEFT OUTER JOIN " + clsUtility.DBName + ".[dbo].[CountryMaster] C ON S.CountryID = C.CountryID");

            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void FillCountryData()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CountryMaster", "CountryID,CountryName", "ISNULL(ActiveStatus,1)=1", "CountryName ASC");
            cmbCountry.DataSource = dt;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";

            cmbCountry.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpSupplier.Enabled = true;
            txtSupplierName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Supplier_Details, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("SupplierName", SqlDbType.NVarChar, txtSupplierName.Text.Trim());
                        ObjDAL.SetColumnData("CountryID", SqlDbType.Int, cmbCountry.SelectedValue);
                        ObjDAL.SetColumnData("Phone", SqlDbType.VarChar, txtPhone.Text.Trim());
                        ObjDAL.SetColumnData("EmailID", SqlDbType.VarChar, txtEmail.Text.Trim());
                        ObjDAL.SetColumnData("BankName", SqlDbType.NVarChar, txtBankName.Text.Trim());
                        ObjDAL.SetColumnData("BankAccountNo", SqlDbType.VarChar, txtBankAccNo.Text.Trim());
                        ObjDAL.SetColumnData("BankAddress", SqlDbType.NVarChar, txtBankAdd.Text.Trim());
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.SupplierMaster", true) > 0)
                        {
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage("Supplier Name : '" + txtSupplierName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpSupplier.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Supplier : '" + txtSupplierName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                        }
                        ObjDAL.ResetData();
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtSupplierName.Text + "' Supplier is already exist..", clsUtility.strProjectTitle);
                        txtSupplierName.Focus();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Supplier_Details, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpSupplier.Enabled = true;
                txtSupplierName.Focus();
                txtSupplierName.SelectionStart = txtSupplierName.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Supplier_Details, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("SupplierName", SqlDbType.NVarChar, txtSupplierName.Text.Trim());
                        ObjDAL.UpdateColumnData("CountryID", SqlDbType.Int, cmbCountry.SelectedValue);
                        ObjDAL.UpdateColumnData("Phone", SqlDbType.VarChar, txtPhone.Text.Trim());
                        ObjDAL.UpdateColumnData("EmailID", SqlDbType.VarChar, txtEmail.Text.Trim());
                        ObjDAL.UpdateColumnData("BankName", SqlDbType.NVarChar, txtBankName.Text.Trim());
                        ObjDAL.UpdateColumnData("BankAccountNo", SqlDbType.VarChar, txtBankAccNo.Text.Trim());
                        ObjDAL.UpdateColumnData("BankAddress", SqlDbType.NVarChar, txtBankAdd.Text.Trim());
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID = " + ID) > 0)
                        {
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                            clsUtility.ShowInfoMessage("'" + txtSupplierName.Text + "' Supplier is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpSupplier.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtSupplierName.Text + "' Supplier is not Updated", clsUtility.strProjectTitle);
                        }
                        ObjDAL.ResetData();
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtSupplierName.Text + "' Supplier is already exist..", clsUtility.strProjectTitle);
                        txtSupplierName.Focus();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Supplier_Details, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtSupplierName.Text + "' Supplier ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID = " + ID) > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtSupplierName.Text + "' Supplier is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpSupplier.Enabled = false;
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtSupplierName.Text + "' Supplier is not deleted  ", clsUtility.strProjectTitle);
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
                grpSupplier.Enabled = false;
            }
        }

        private void txtSupplierName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSupplierName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SupplierID"].Value);
                    txtSupplierName.Text = dataGridView1.SelectedRows[0].Cells["SupplierName"].Value.ToString();
                    cmbCountry.SelectedValue = dataGridView1.SelectedRows[0].Cells["CountryID"].Value.ToString();
                    txtPhone.Text = dataGridView1.SelectedRows[0].Cells["Phone"].Value.ToString();
                    txtEmail.Text = dataGridView1.SelectedRows[0].Cells["EmailID"].Value.ToString();
                    txtBankName.Text = dataGridView1.SelectedRows[0].Cells["BankName"].Value.ToString();
                    txtBankAccNo.Text = dataGridView1.SelectedRows[0].Cells["BankAccountNo"].Value.ToString();
                    txtBankAdd.Text = dataGridView1.SelectedRows[0].Cells["BankAddress"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();

                    grpSupplier.Enabled = false;
                    txtSupplierName.Focus();
                }
                catch (Exception ex) { clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle); }
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbCountry.Focus();
                return;
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtFax.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                cmbCountry.Focus();
                return;
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtEmail.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtPhone.Focus();
                txtPhone.SelectionStart = txtPhone.Text.Length;
                return;
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBankName.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtFax.Focus();
                txtFax.SelectionStart = txtFax.Text.Length;
                return;
            }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBankAccNo.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtEmail.Focus();
                txtEmail.SelectionStart = txtEmail.Text.Length;
                return;
            }
        }

        private void txtBankAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtBankAdd.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtBankName.Focus();
                txtBankName.SelectionStart = txtBankName.Text.Length;
                return;
            }
        }

        private void txtBankAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbActiveStatus.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtBankAccNo.Focus();
                txtBankAccNo.SelectionStart = txtBankAccNo.Text.Length;
                return;
            }
        }

        private void Supplier_Details_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dataGridView1.RowHeadersVisible = false; // set it to false if not needed

            LoadData();

            FillCountryData();
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

        private void rdSearchBySupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchBySupplier.Checked)
            {
                txtSearchBySupplier.Enabled = true;
                txtSearchBySupplier.Focus();
            }
            else
            {
                txtSearchBySupplier.Enabled = false;
                txtSearchBySupplier.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchBySupplier.Enabled = false;
                txtSearchBySupplier.Clear();
                LoadData();
            }
        }

        private void txtSearchBySupplier_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchBySupplier.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = null;
            dt = ObjDAL.ExecuteSelectStatement("SELECT S.SupplierID,S.SupplierName,S.CountryID,C.CountryName,(CASE S.ActiveStatus WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END) ActiveStatus,S.Phone,S.EmailID,S.BankName,S.BankAccountNo,S.BankAddress " +
              "FROM " + clsUtility.DBName + ".[dbo].[SupplierMaster] S " +
              "LEFT OUTER JOIN " + clsUtility.DBName + ".[dbo].[CountryMaster] C ON S.CountryID = C.CountryID " +
              "WHERE S.SupplierName LIKE '%" + txtSearchBySupplier.Text + "%'");

            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void btnCountryPopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbCountry.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbCountry.SelectedValue);
            }
            Masters.Country_Master Obj = new Country_Master();
            Obj.ShowDialog();
            FillCountryData();
            if (a > 0)
            {
                cmbCountry.SelectedValue = a;
            }
        }

        private void txtBankAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
            }
        }

        private void txtSupplierName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtSupplierName.Focus();
            }
        }
    }
}