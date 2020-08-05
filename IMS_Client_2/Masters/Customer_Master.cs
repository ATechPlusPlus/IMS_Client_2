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
    public partial class Customer_Master : Form
    {
        public Customer_Master()
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
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            txtCustomerPhoneNo.Clear();
            cmbCustomerActiveStatus.SelectedIndex = -1;

            txtCustomerName.Focus();
        }

        private bool ValidateForm()
        {
            if (ObjUtil.IsControlTextEmpty(txtCustomerName))
            {
                clsUtility.ShowInfoMessage("Enter Customer Name       ", clsUtility.strProjectTitle);
                txtCustomerName.Focus();
                return false;
            }

            else if (ObjUtil.IsControlTextEmpty(txtCustomerPhoneNo))
            {
                clsUtility.ShowInfoMessage("Enter Customer Phone No.      ", clsUtility.strProjectTitle);
                txtCustomerPhoneNo.Focus();
                return false;
            }

            else if (ObjUtil.IsControlTextEmpty(cmbCustomerActiveStatus))
            {
                clsUtility.ShowInfoMessage("Select Active Status.", clsUtility.strProjectTitle);
                cmbCustomerActiveStatus.Focus();
                return false;
            }
            return true;
        }

        private bool DuplicateUser(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CustomerMaster", "Name='" + txtCustomerName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CustomerMaster", "Name='" + txtCustomerName.Text + "' AND CustomerID !=" + i);
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
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CustomerMaster", "CustomerID,Name,Address,PhoneNo,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "Name");

            if (ObjUtil.ValidateTable(dt))
            {
                dgvCustomerMaster.DataSource = dt;
            }
            else
            {
                dgvCustomerMaster.DataSource = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpCustomer.Enabled = true;
            txtCustomerName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (ValidateForm())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("Name", SqlDbType.NVarChar, txtCustomerName.Text.Trim());
                        ObjDAL.SetColumnData("Address", SqlDbType.NVarChar, txtCustomerAddress.Text.Trim());
                        ObjDAL.SetColumnData("PhoneNo", SqlDbType.NVarChar, txtCustomerPhoneNo.Text.Trim());
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbCustomerActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.CustomerMaster", true) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage("Customer Name : '" + txtCustomerName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpCustomer.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Customer Name : '" + txtCustomerName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCustomerName.Text + "' Customer is already exist..", clsUtility.strProjectTitle);
                        txtCustomerName.Focus();
                    }
                    ObjDAL.ResetData();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpCustomer.Enabled = true;
                txtCustomerName.Focus();
                txtCustomerName.SelectionStart = txtCustomerName.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (ValidateForm())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("Name", SqlDbType.NVarChar, txtCustomerName.Text.Trim());
                        ObjDAL.UpdateColumnData("Address", SqlDbType.NVarChar, txtCustomerAddress.Text.Trim());
                        ObjDAL.UpdateColumnData("PhoneNo", SqlDbType.NVarChar, txtCustomerPhoneNo.Text.Trim());
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbCustomerActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.CustomerMaster", "CustomerID = " + ID + "") > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                            clsUtility.ShowInfoMessage("'" + txtCustomerName.Text + "' Customer is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpCustomer.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtCustomerName.Text + "' Customer is not Updated", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCustomerName.Text + "' Customer is already exist..", clsUtility.strProjectTitle);
                        txtCustomerName.Focus();
                    }
                    ObjDAL.ResetData();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtCustomerName.Text + "' Customer ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.CustomerMaster", "CustomerID = " + ID + "") > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtCustomerName.Text + "' Customer is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpCustomer.Enabled = false;
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCustomerName.Text + "' Customer is not deleted  ", clsUtility.strProjectTitle);
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
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                grpCustomer.Enabled = false;
            }
        }

        private void txtCustomerName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dgvCustomerMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    ID = Convert.ToInt32(dgvCustomerMaster.SelectedRows[0].Cells["CustomerID"].Value);
                    txtCustomerName.Text = dgvCustomerMaster.SelectedRows[0].Cells["Name"].Value.ToString();
                    txtCustomerAddress.Text = dgvCustomerMaster.SelectedRows[0].Cells["Address"].Value.ToString();
                    txtCustomerPhoneNo.Text = dgvCustomerMaster.SelectedRows[0].Cells["PhoneNo"].Value.ToString();
                    cmbCustomerActiveStatus.SelectedItem = dgvCustomerMaster.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();
                    grpCustomer.Enabled = false;
                    txtCustomerName.Focus();
                }
                catch { }
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtCustomerPhoneNo.Focus();
                return;
            }
        }

        private void Customer_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            dgvCustomerMaster.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvCustomerMaster.RowHeadersVisible = false; // set it to false if not needed

            LoadData();

            grpCustomer.Enabled = false;
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

        private void rdSearchByCustomerName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByCustomerName.Checked)
            {
                txtSearchByCustomer.Enabled = true;
                txtSearchByCustomer.Focus();
            }
            else
            {
                txtSearchByCustomer.Enabled = false;
                txtSearchByCustomer.Clear();
                rdShowAllOfCustomer.Checked = true;
            }

        }

        private void rdShowAllOfCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAllOfCustomer.Checked)
            {
                txtSearchByCustomer.Enabled = false;
                txtSearchByCustomer.Clear();
                LoadData();
            }
        }

        private void txtSearchByCustomer_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByCustomer.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }

            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CustomerMaster", "CustomerID,Name,Address,PhoneNo,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "Name LIKE '%" + txtSearchByCustomer.Text + "%'", "Name");
            if (ObjUtil.ValidateTable(dt))
            {
                dgvCustomerMaster.DataSource = dt;
            }
            else
            {
                dgvCustomerMaster.DataSource = null;
            }
        }

        private void dgvCustomerMaster_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvCustomerMaster);
            ObjUtil.SetDataGridProperty(dgvCustomerMaster, DataGridViewAutoSizeColumnsMode.Fill);
            dgvCustomerMaster.Columns["CustomerID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dgvCustomerMaster.Rows.Count;
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCustomerName.Focus();
            }
        }

        private void rdSearchByCustomerMobileNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByCustomerMobileNo.Checked)
            {
                txtSearchByMobileNo.Enabled = true;
                txtSearchByMobileNo.Focus();
            }
            else
            {
                txtSearchByMobileNo.Enabled = false;
                txtSearchByMobileNo.Clear();
                rdShowAllOfCustomer.Checked = true;
            }
        }

        private void txtSearchByMobileNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSearchByMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCustomerName.Focus();
            }
        }

        private void txtSearchByMobileNo_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByMobileNo.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }

            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CustomerMaster", "CustomerID,Name,Address,PhoneNo,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "PhoneNo LIKE '%" + txtSearchByMobileNo.Text + "%'", "Name");
            if (ObjUtil.ValidateTable(dt))
            {
                dgvCustomerMaster.DataSource = dt;
            }
            else
            {
                dgvCustomerMaster.DataSource = null;
            }
        }
    }
}
