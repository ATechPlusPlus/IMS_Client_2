using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmStoreRights : Form
    {
        public frmStoreRights()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmStoreRights_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            loadUser();
            LoadShop();
        }
        private void loadUser()
        {
            if (clsUtility.IsAdmin)
            {
                string str = " select distinct s1.UserID,u.UserName from "+clsUtility.DBName+".dbo.tblStoreUserRights s1 join " +
                            " UserManagement u on s1.UserID = u.UserID ";

                DataTable dtuser = ObjCon.ExecuteSelectStatement(str);
                dgvUser.DataSource = dtuser;
                dgvUser.Columns["UserID"].Visible = false;
            }
            else
            {
                string str = "SELECT DISTINCT u1.UserID , u1.UserName FROM " + clsUtility.DBName + ".[dbo].[UserManagement] u1 JOIN " + clsUtility.DBName + ".[dbo].[tblUserRights] ur " +
                       "ON u1.UserID = ur.UserID WHERE u1.UserID=" + clsUtility.LoginID;

                DataTable dtuser = ObjCon.ExecuteSelectStatement(str);
                dgvUser.DataSource = dtuser;
                dgvUser.Columns["UserID"].Visible = false;
            }
        }
        bool isEdit;
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (isEdit)
                {
                    return;
                }
                if (ObjUtil.IsControlTextEmpty(txtName))
                {
                    txtUserID.Clear();
                    ObjUtil.CloseAutoExtender();
                    return;
                }
                DataTable dt = ObjCon.ExecuteSelectStatement("SELECT USERNAME,USERID FROM " + clsUtility.DBName + ".dbo.UserManagement WITH(NOLOCK) WHERE IsAdmin=0 AND UserName LIKE '" + txtName.Text + "%'");
                if (ObjUtil.ValidateTable(dt))
                {
                    ObjUtil.SetControlData(txtName, "USERNAME");
                    ObjUtil.SetControlData(txtUserID, "USERID");
             
                    ObjUtil.ShowDataPopup(dt, txtName, this, this);
                    if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                    {
                      
                        // if there is only one column
                        ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        if (ObjUtil.GetDataPopup().ColumnCount > 0)
                        {
                            ObjUtil.GetDataPopup().Columns["UserID"].Visible = false;
                            ObjUtil.SetDataPopupSize(200, 0);
                        }
                    }
                    //ObjUtil.GetDataPopup().CellClick += Sales_Bill_Details_CellClick;
                    //ObjUtil.GetDataPopup().KeyDown += Sales_Bill_Details_KeyDown;
                }
                else
                {
                    txtUserID.Clear();
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvShopeDetails.Enabled = true;
            isEdit = false;
            txtName.Enabled = true;
            grpRights.Enabled = true;
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            txtName.Focus();
            txtUserID.Clear();
        }

        private void dgvUser_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvUser);
            ObjUtil.SetDataGridProperty(dgvUser, DataGridViewAutoSizeColumnsMode.Fill);

        }
        private void LoadShop()
        {
            DataTable dt = null;
            dt = ObjCon.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName,Place,case StoreCategory when 0 then 'Normal' when 1 then'WearHouse' end as Category ", "StoreName");

            if (ObjUtil.ValidateTable(dt))
            {
                dgvShopeDetails.DataSource = dt;
            }
            else
            {
                dgvShopeDetails.DataSource = null;
            }
        }

        private void dgvUserRIghts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvShopeDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvShopeDetails.Columns[0].ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length==0)
            {
                CoreApp.clsUtility.ShowInfoMessage("Please enter user name.", clsUtility.strProjectTitle);
                return;
            }
            else if (AnyRightCheck()==false)
            {
                CoreApp.clsUtility.ShowInfoMessage("Please select shop.", clsUtility.strProjectTitle);
                return;

            }
            SaveUserRights();
            
        }
        private bool AnyRightCheck()
        {
            dgvShopeDetails.EndEdit();
            foreach (DataGridViewRow row in dgvShopeDetails.Rows)
            {
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
            }
            return false;
        }
        private void SaveUserRights()
        {
            ObjCon.ExecuteNonQuery("Delete " + clsUtility.DBName + ".dbo.[tblStoreUserRights] where UserID = " + txtUserID.Text);
            dgvShopeDetails.EndEdit();
            for (int i = 0; i < dgvShopeDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvShopeDetails.Rows[i].Cells["colCheck"].Value))
                {
                    string storeID = dgvShopeDetails.Rows[i].Cells["StoreID"].Value.ToString();
                    ObjCon.SetColumnData("UserID", SqlDbType.Int, txtUserID.Text);
                    ObjCon.SetColumnData("StoreID", SqlDbType.Int, storeID);
                    ObjCon.SetColumnData("CreatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreUserRights", false);
                }
               
            }
            Clear();
            clsUtility.ShowInfoMessage("User Shop rights have been saved.", clsUtility.strProjectTitle);
           
        }
        private void UpdateRights()
        {
            ObjCon.ExecuteNonQuery("Delete " + clsUtility.DBName + ".dbo.[tblStoreUserRights] where UserID = " + txtUserID.Text);
            dgvShopeDetails.EndEdit();
            for (int i = 0; i < dgvShopeDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvShopeDetails.Rows[i].Cells["colCheck"].Value))
                {
                    string storeID = dgvShopeDetails.Rows[i].Cells["StoreID"].Value.ToString();
                    ObjCon.SetColumnData("UserID", SqlDbType.Int, txtUserID.Text);
                    ObjCon.SetColumnData("StoreID", SqlDbType.Int, storeID);
                    ObjCon.SetColumnData("CreatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreUserRights", false);
                }

            }
            Clear();
            clsUtility.ShowInfoMessage("User Shop rights have been updated.", clsUtility.strProjectTitle);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
             if (AnyRightCheck() == false)
            {
                CoreApp.clsUtility.ShowInfoMessage("Please select shop.", clsUtility.strProjectTitle);
                return;

            }
            UpdateRights();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                Clear();
            }
        }
        private void Clear()
        {
            chkAll.Checked = false;
                txtName.Clear();
                txtUserID.Clear();
                dgvShopeDetails.Enabled = false;
                LoadShop();
                txtName.Enabled = false;
                grpRights.Enabled = false;
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
            loadUser();

        }

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                LoadShop();
                int UserID = Convert.ToInt32(dgvUser.SelectedRows[0].Cells["UserID"].Value);
                txtUserID.Text = UserID.ToString();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);


               DataTable dtStoreRights= ObjCon.ExecuteSelectStatement("select StoreID from "+clsUtility.DBName+".dbo.tblStoreUserRights where UserID="+ UserID);
                for (int i = 0; i < dtStoreRights.Rows.Count; i++)
                {
                    for (int k = 0; k < dgvShopeDetails.Rows.Count; k++)
                    {
                        int gridValue=Convert.ToInt32(dgvShopeDetails.Rows[k].Cells["StoreID"].Value);
                        if (gridValue == Convert.ToInt32(dtStoreRights.Rows[i]["StoreID"]))
                        {
                            dgvShopeDetails.Rows[k].Cells[0].Value = true;
                        }
                    }
                }
                dgvShopeDetails.EndEdit();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEdit = true;
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpRights.Enabled = true;
            txtName.Enabled = false;
            dgvShopeDetails.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete user rights for User :  '" + txtName.Text + "'  ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                ObjCon.ExecuteNonQuery("DELETE " + clsUtility.DBName + ".[dbo].[tblStoreUserRights] WHERE UserID=" + txtUserID.Text);
                clsUtility.ShowInfoMessage("Rights has been deleted.", clsUtility.strProjectTitle);
                Clear();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                loadUser();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int k = 0; k < dgvShopeDetails.Rows.Count; k++)
                {
                    dgvShopeDetails.Rows[k].Cells["colCheck"].Value = true;
                    
                }
                dgvShopeDetails.EndEdit();
            }
            else
            {
                for (int k = 0; k < dgvShopeDetails.Rows.Count; k++)
                {
                    dgvShopeDetails.Rows[k].Cells["colCheck"].Value = false;

                }
                dgvShopeDetails.EndEdit();

            }
        }
    }
}
