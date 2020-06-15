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
    public partial class Size_Type_Master : Form
    {
        public Size_Type_Master()
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
            txtSizeTypeName.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbActiveStatus.SelectedIndex = -1;

            txtSizeTypeName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSizeTypeName))
            {
                clsUtility.ShowInfoMessage("Enter Size Type Name           ", clsUtility.strProjectTitle);
                txtSizeTypeName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbDepartment))
            {
                clsUtility.ShowInfoMessage("Select Department for " + txtSizeTypeName.Text, clsUtility.strProjectTitle);
                cmbDepartment.Focus();
                return false;
            }
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeName='" + txtSizeTypeName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeName='" + txtSizeTypeName.Text + "' AND SizeTypeID !=" + i);
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
            ObjUtil.SetDataGridProperty(dgvSizeTypeMaster, DataGridViewAutoSizeColumnsMode.Fill);
            DataTable dt = null;
            dt = ObjDAL.ExecuteSelectStatement("SELECT sm.SizeTypeID,sm.SizeTypeName,cm.CategoryID,cm.CategoryName" +
                    ", (CASE WHEN sm.ActiveStatus = 1 THEN 'Active' WHEN sm.ActiveStatus = 0 THEN 'InActive' END) " + "ActiveStatus FROM SizeTypeMaster sm " +
                    " LEFT JOIN CategoryMaster cm ON sm.CategoryID = cm.CategoryID");

            if (ObjUtil.ValidateTable(dt))
            {
                dgvSizeTypeMaster.DataSource = dt;
            }
            else
            {
                dgvSizeTypeMaster.DataSource = null;
            }
        }

        private void FillDepartmentData()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName", "ISNULL(ActiveStatus,1)=1", "CategoryName ASC");
            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "CategoryName";
            cmbDepartment.ValueMember = "CategoryID";

            cmbDepartment.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpSizeTypeDetails.Enabled = true;
            txtSizeTypeName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(0))
                {
                    ObjDAL.SetColumnData("SizeTypeName", SqlDbType.NVarChar, txtSizeTypeName.Text.Trim());
                    //if (cmbDepartment.SelectedIndex >= 0)
                    //{
                    ObjDAL.SetColumnData("CategoryID", SqlDbType.Int, cmbDepartment.SelectedValue);
                    //}
                    ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.SizeTypeMaster", true) > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                        clsUtility.ShowInfoMessage("Size Type Name : '" + txtSizeTypeName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpSizeTypeDetails.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Size Type Name : '" + txtSizeTypeName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtSizeTypeName.Text + "' Size Type is already exist..", clsUtility.strProjectTitle);
                    txtSizeTypeName.Focus();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpSizeTypeDetails.Enabled = true;
            txtSizeTypeName.Focus();
            txtSizeTypeName.SelectionStart = txtSizeTypeName.MaxLength;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(ID))
                {
                    ObjDAL.UpdateColumnData("SizeTypeName", SqlDbType.NVarChar, txtSizeTypeName.Text.Trim());
                    //if (cmbDepartment.SelectedIndex >= 0)
                    //{
                    ObjDAL.UpdateColumnData("CategoryID", SqlDbType.Int, cmbDepartment.SelectedValue);
                    //}
                    ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                    if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID = " + ID) > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                        clsUtility.ShowInfoMessage("'" + txtSizeTypeName.Text + "' Size Type is Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        grpSizeTypeDetails.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtSizeTypeName.Text + "' Size Type is not Updated", clsUtility.strProjectTitle);
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtSizeTypeName.Text + "' Size Type is already exist..", clsUtility.strProjectTitle);
                    txtSizeTypeName.Focus();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtSizeTypeName.Text + "' Size Type ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID=" + ID) > 0)
                {
                    clsUtility.ShowInfoMessage("'" + txtSizeTypeName.Text + "' Size Type is deleted  ", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpSizeTypeDetails.Enabled = false;
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtSizeTypeName.Text + "' Size Type is not deleted  ", clsUtility.strProjectTitle);
                    ObjDAL.ResetData();
                }
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
                grpSizeTypeDetails.Enabled = false;
            }
        }

        private void txtSizeTypeName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSizeTypeName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dgvSizeTypeMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dgvSizeTypeMaster.SelectedRows[0].Cells["SizeTypeID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtSizeTypeName.Text = dgvSizeTypeMaster.SelectedRows[0].Cells["SizeTypeName"].Value.ToString();
                    cmbDepartment.SelectedValue = dgvSizeTypeMaster.SelectedRows[0].Cells["CategoryID"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dgvSizeTypeMaster.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();

                    grpSizeTypeDetails.Enabled = false;
                    txtSizeTypeName.Focus();
                }
                catch { }
            }
        }

        private void txtSizeTypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbDepartment.Focus();
                return;
            }
        }

        private void Size_Type_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            //clsUtility.IsAdmin = true;//removed

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            LoadData();
            FillDepartmentData();
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

        private void rdSearchBySizeType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchBySizeType.Checked)
            {
                txtSearchBySizeType.Enabled = true;
                txtSearchBySizeType.Focus();
            }
            else
            {
                txtSearchBySizeType.Enabled = false;
                txtSearchBySizeType.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchBySizeType.Enabled = false;
                txtSearchBySizeType.Clear();
                LoadData();
            }
        }

        private void txtSearchBySizeType_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchBySizeType.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID,SizeTypeName,CategoryID,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "SizeTypeName LIKE '%" + txtSearchBySizeType.Text + "%'", "SizeTypeName");
            if (ObjUtil.ValidateTable(dt))
            {
                dgvSizeTypeMaster.DataSource = dt;
            }
            else
            {
                dgvSizeTypeMaster.DataSource = null;
            }
        }

        private void dgvSizeTypeMaster_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvSizeTypeMaster);
            ObjUtil.SetDataGridProperty(dgvSizeTypeMaster, DataGridViewAutoSizeColumnsMode.Fill);
            dgvSizeTypeMaster.Columns["SizeTypeID"].Visible = false;
            dgvSizeTypeMaster.Columns["CategoryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dgvSizeTypeMaster.Rows.Count;
        }

        private void btnCategoryPopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbDepartment.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbDepartment.SelectedValue);
            }
            Masters.Category_Master Obj = new Category_Master();
            Obj.ShowDialog();
            FillDepartmentData();
            if (a > 0)
            {
                cmbDepartment.SelectedValue = a;
            }
        }

        private void txtSizeTypeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtSizeTypeName.Focus();
            }
        }
    }
}