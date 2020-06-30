using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2
{
    public partial class frmUserRights : Form
    {
        public frmUserRights()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        DataTable dtUserRights = new DataTable();
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void frmUserRights_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            InitRightTable();
            LoadRightGrid();
            loadUser();
        }
        private void loadUser()
        {
            if (clsUtility.IsAdmin)
            {
                string str = "SELECT DISTINCT u1.UserID , u1.UserName FROM " + clsUtility.DBName + ".[dbo].[UserManagement] u1 JOIN " + clsUtility.DBName + ".[dbo].[tblUserRights] ur " +
                          "ON u1.UserID = ur.UserID";

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

        private void InitRightTable()
        {
            if (dtUserRights.Columns.Count == 0)
            {
                dtUserRights.Columns.Add("FormID");
                dtUserRights.Columns.Add("FormName");
                dtUserRights.Columns.Add("View");
                dtUserRights.Columns.Add("Save");
                dtUserRights.Columns.Add("Update");
                dtUserRights.Columns.Add("Delete");
                dtUserRights.Columns.Add("Other");
                dtUserRights.Columns.Add("ParentID");
            }
        }
        private void LoadRightGrid()
        {
            try
            {
                DataTable dtAllFormsDetails = ObjCon.ExecuteSelectStatement("SELECT *  FROM " + clsUtility.DBName + ".[dbo].[tblFormRightDetails] WITH(NOLOCK)");
                // get only parent 
                DataRow[] ParentRows = dtAllFormsDetails.Select("ParentID=0");
                if (ParentRows.Length > 0)
                {
                    for (int i = 0; i < ParentRows.Length; i++)
                    {
                        AddRowRightsTable(ParentRows[i]["FormID"].ToString(), ParentRows[i]["FormName"].ToString(), false, false, false, false, false, 0);
                        int ParentID = Convert.ToInt32(ParentRows[i]["FormID"]);
                        // get the child
                        DataRow[] dataRowChild = dtAllFormsDetails.Select("ParentID=" + ParentID);
                        for (int j = 0; j < dataRowChild.Length; j++)
                        {
                            AddRowRightsTable(dataRowChild[j]["FormID"].ToString(), dataRowChild[j]["FormName"].ToString(), false, false, false, false, false, ParentID);
                        }
                    }
                }
                dgvUserRIghts.DataSource = dtUserRights;
                dgvUserRIghts.ClearSelection();
            }
            catch (Exception ex)
            {
                clsUtility.ShowInfoMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void AddRowRightsTable(string fID, string fname, bool IsView, bool IsSave, bool IsUpdate, bool IsDelete, bool IsOther, int ParentID)
        {
            DataRow dRow = dtUserRights.NewRow();
            dRow["FormID"] = fID;
            dRow["FormName"] = fname;
            dRow["View"] = IsView;
            dRow["Save"] = IsSave;
            dRow["Update"] = IsUpdate;
            dRow["Delete"] = IsDelete;
            dRow["Other"] = IsOther;
            dRow["ParentID"] = ParentID.ToString();
            dtUserRights.Rows.Add(dRow);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //ObjUtil.SetDataGridProperty(dgvUserRIghts, DataGridViewAutoSizeColumnsMode.Fill);
            for (int i = 0; i < dgvUserRIghts.Rows.Count; i++)
            {
                if (dgvUserRIghts.Rows[i].Cells["ParentID"].Value.ToString() == "0")
                {
                    dgvUserRIghts.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(0, 64, 128);
                    dgvUserRIghts.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    dgvUserRIghts.Rows[i].DefaultCellStyle.Font = new Font("Times New Roman", 11.2f, FontStyle.Bold);
                }
                else
                {
                    dgvUserRIghts.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(204, 230, 255);
                    dgvUserRIghts.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dgvUserRIghts.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
        }
        private void txtCategoryName_TextChanged(object sender, EventArgs e)
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
                    
                    ObjUtil.ShowDataPopup(dt, txtName, this, grpUserName);
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                ClearAll();
                grpUserName.Enabled = false;
                grpRights.Enabled = false;
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
            }
        }
        private void ClearAll()
        {
            txtUserID.Clear();
            txtName.Clear();
            dtUserRights.Clear();
            LoadRightGrid();
            isEdit = false;
            loadUser();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEdit = false;
            grpUserName.Enabled = true;
            grpRights.Enabled = true;
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            txtName.Focus();
            txtUserID.Clear();
        }
        private bool ValidateRights()
        {
            if (ObjUtil.IsControlTextEmpty(txtUserID))
            {
                clsUtility.ShowInfoMessage("Enter Valid User Name.", clsUtility.strProjectTitle);
                txtName.Focus();
                return false;
            }

            if (!AnyRightCheck())
            {
                clsUtility.ShowInfoMessage("Please select any rights from rights grid.", clsUtility.strProjectTitle);
                grpRights.Focus();
                return false;
            }

            int count = ObjCon.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblUserRights] WITH(NOLOCK) WHERE UserID=" + txtUserID.Text);
            if (count > 0)
            {
                clsUtility.ShowInfoMessage("Rights for the user : " + txtName.Text + " already exists. Please select new user or select existing user and update.", clsUtility.strProjectTitle);
                txtName.Focus();
                return false;
            }
            return true;
        }
        private bool AnyRightCheck()
        {
            foreach (DataGridViewRow row in dgvUserRIghts.Rows)
            {
                if (row.Cells["View"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["View"].Value))
                {
                    return true;
                }
                if (row.Cells["Save"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Save"].Value))
                {
                    return true;
                }
                if (row.Cells["Update"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Update"].Value))
                {
                    return true;
                }
                if (row.Cells["Delete"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Delete"].Value))
                {
                    return true;
                }
                if (row.Cells["Other"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Other"].Value))
                {
                    return true;
                }
            }
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvUserRIghts.EndEdit();
            if (ValidateRights())
            {
                SaveRightsData(false);
            }
        }
        private void SaveRightsData(bool isEditMode)
        {
            if (isEditMode)
            {
                ObjCon.ExecuteNonQuery("DELETE " + clsUtility.DBName + ".[dbo].[tblUserRights] WHERE UserID=" + txtUserID.Text);
            }

            foreach (DataGridViewRow row in dgvUserRIghts.Rows)
            {
                bool RightFoud = false;
                if (row.Cells["View"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["View"].Value))
                {
                    RightFoud = true;
                }
                else if (row.Cells["Save"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Save"].Value))
                {
                    RightFoud = true;
                }
                else if (row.Cells["Update"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Update"].Value))
                {
                    RightFoud = true;
                }
                else if (row.Cells["Delete"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Delete"].Value))
                {
                    RightFoud = true;
                }
                else if (row.Cells["Other"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Other"].Value))
                {
                    RightFoud = true;
                }
                // Only insert if right found. 
                if (RightFoud)
                {
                    RightFoud = false;
                    bool IsView = false;
                    bool IsSave = false;
                    bool IsUpdate = false;
                    bool IsDelete = false;
                    bool IsOther = false;

                    if (row.Cells["View"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["View"].Value))
                    {
                        IsView = true;
                    }
                    if (row.Cells["Save"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Save"].Value))
                    {
                        IsSave = true;
                    }
                    if (row.Cells["Update"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Update"].Value))
                    {
                        IsUpdate = true;
                    }
                    if (row.Cells["Delete"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Delete"].Value))
                    {
                        IsDelete = true;
                    }
                    if (row.Cells["Other"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["Other"].Value))
                    {
                        IsOther = true;
                    }
                    ObjCon.SetColumnData("UserID", SqlDbType.Int, txtUserID.Text);
                    ObjCon.SetColumnData("FormID", SqlDbType.Int, row.Cells["FormID"].Value.ToString());
                    ObjCon.SetColumnData("IsView", SqlDbType.Bit, IsView);
                    ObjCon.SetColumnData("IsSave", SqlDbType.Bit, IsSave);
                    ObjCon.SetColumnData("IsUpdate", SqlDbType.Bit, IsUpdate);
                    ObjCon.SetColumnData("IsDelete", SqlDbType.Bit, IsDelete);
                    ObjCon.SetColumnData("IsOther", SqlDbType.Bit, IsOther);
                    ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjCon.InsertData(clsUtility.DBName + ".dbo.tblUserRights", false);
                }
            }
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
            if (isEditMode)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);
                clsUtility.ShowInfoMessage("User rights has been Updaetd Successfully.", clsUtility.strProjectTitle);
                isEdit = false;
            }
            else
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                clsUtility.ShowInfoMessage("User rights has been Saved Successfully.", clsUtility.strProjectTitle);
            }
            ClearAll();
            grpUserName.Enabled = false;
            grpRights.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isEdit = true;
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpRights.Enabled = true;
            grpUserName.Enabled = false;
        }
        private void dgvUserRIghts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                dgvUserRIghts.EndEdit();
                bool IsRightChecked = false;
                if (dgvUserRIghts.Columns[e.ColumnIndex].Name == "FormName")
                {
                    return;
                }
                if (dgvUserRIghts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value &&
                   Convert.ToBoolean(dgvUserRIghts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    IsRightChecked = true;
                }
                if (IsRightChecked)
                {
                    // if its parent
                    if (dgvUserRIghts.Rows[e.RowIndex].Cells["ParentID"].Value.ToString() == "0")
                    {
                        string formID = dgvUserRIghts.Rows[e.RowIndex].Cells["FormID"].Value.ToString();
                        // get the child
                        IEnumerable<DataGridViewRow> rows = dgvUserRIghts.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["ParentID"].Value.ToString().Equals(formID));
                        foreach (var item in rows)
                        {
                            item.Cells[e.ColumnIndex].Value = true;
                        }
                        dgvUserRIghts.EndEdit();
                    }
                    else
                    {
                        // get the parent ID
                        string formID = dgvUserRIghts.Rows[e.RowIndex].Cells["ParentID"].Value.ToString();
                        // get all child
                        IEnumerable<DataGridViewRow> rows = dgvUserRIghts.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["ParentID"].Value.ToString().Equals(formID));

                        bool IsAllChecked = false;
                        foreach (var item in rows)
                        {
                            if (item.Cells[e.ColumnIndex].Value != DBNull.Value && Convert.ToBoolean(item.Cells[e.ColumnIndex].Value))
                            {
                                IsAllChecked = true;
                            }
                            else
                            {
                                // if found a single unchecked.
                                IsAllChecked = false;
                                break;
                            }
                        }
                        // check the parent
                        if (IsAllChecked)
                        {
                            IEnumerable<DataGridViewRow> rows2 = dgvUserRIghts.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["FormID"].Value.ToString().Equals(formID));
                            foreach (var item in rows2)
                            {
                                item.Cells[e.ColumnIndex].Value = true;
                            }
                            dgvUserRIghts.EndEdit();
                        }
                    }
                }
                else
                {
                    // if its parent
                    if (dgvUserRIghts.Rows[e.RowIndex].Cells["ParentID"].Value.ToString() == "0")
                    {
                        string formID = dgvUserRIghts.Rows[e.RowIndex].Cells["FormID"].Value.ToString();
                        // get the child
                        IEnumerable<DataGridViewRow> rows = dgvUserRIghts.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["ParentID"].Value.ToString().Equals(formID));
                        foreach (var item in rows)
                        {
                            item.Cells[e.ColumnIndex].Value = false;
                        }
                        dgvUserRIghts.EndEdit();
                    }
                    else
                    {
                        // get the parent ID
                        string formID = dgvUserRIghts.Rows[e.RowIndex].Cells["ParentID"].Value.ToString();
                        // get all child
                        IEnumerable<DataGridViewRow> rows = dgvUserRIghts.Rows
                          .Cast<DataGridViewRow>()
                          .Where(r => r.Cells["ParentID"].Value.ToString().Equals(formID));

                        bool IsAllChecked = false;
                        foreach (var item in rows)
                        {
                            if (item.Cells[e.ColumnIndex].Value != DBNull.Value && Convert.ToBoolean(item.Cells[e.ColumnIndex].Value))
                            {
                                IsAllChecked = true;
                            }
                            else
                            {
                                // if found a single unchecked.
                                IsAllChecked = false;
                                break;
                            }
                        }
                        // check the parent
                        if (IsAllChecked == false)
                        {
                            IEnumerable<DataGridViewRow> rows2 = dgvUserRIghts.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells["FormID"].Value.ToString().Equals(formID));
                            foreach (var item in rows2)
                            {
                                item.Cells[e.ColumnIndex].Value = false;
                            }
                            dgvUserRIghts.EndEdit();
                        }
                    }
                }
            }
        }
        private void dgvUserRIghts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUserRIghts.IsCurrentCellDirty)
            {
                dgvUserRIghts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvUser_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvUser);
            ObjUtil.SetDataGridProperty(dgvUser, DataGridViewAutoSizeColumnsMode.Fill);
        }
        bool isEdit = false;
        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    int UserID = Convert.ToInt32(dgvUser.SelectedRows[0].Cells["UserID"].Value);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    dtUserRights.Clear();
                    LoadRightGrid();

                    string strQ = "SELECT * FROM " + clsUtility.DBName + ".[dbo].[UserManagement] u1 JOIN  " + clsUtility.DBName + ".[dbo].[tblUserRights] ur " +
                                " ON u1.UserID = ur.UserID WHERE u1.UserID = " + UserID;
                    DataTable dt = ObjCon.ExecuteSelectStatement(strQ);
                    if (ObjUtil.ValidateTable(dt))
                    {
                        isEdit = true;
                        txtUserID.Text = UserID.ToString();
                        txtName.Text = dt.Rows[0]["UserName"].ToString();
                        isEdit = false;
                    }

                    // Bind Grid
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string formID = dt.Rows[i]["FormID"].ToString();
                        DataGridViewRow row = dgvUserRIghts.Rows
                       .Cast<DataGridViewRow>()
                       .Where(r => r.Cells["FormID"].Value.ToString().Equals(formID)).First();

                        if (row != null)
                        {
                            if (Convert.ToBoolean(dt.Rows[i]["IsView"]))
                            {
                                row.Cells["View"].Value = true;
                            }
                            else
                            {
                                row.Cells["View"].Value = false;
                            }

                            if (Convert.ToBoolean(dt.Rows[i]["IsSave"]))
                            {
                                row.Cells["Save"].Value = true;
                            }
                            else
                            {
                                row.Cells["Save"].Value = false;
                            }

                            if (Convert.ToBoolean(dt.Rows[i]["IsUpdate"]))
                            {
                                row.Cells["Update"].Value = true;
                            }
                            else
                            {
                                row.Cells["Update"].Value = false;
                            }

                            if (Convert.ToBoolean(dt.Rows[i]["IsDelete"]))
                            {
                                row.Cells["Delete"].Value = true;
                            }
                            else
                            {
                                row.Cells["Delete"].Value = false;
                            }

                            if (Convert.ToBoolean(dt.Rows[i]["IsOther"]))
                            {
                                row.Cells["Other"].Value = true;
                            }
                            else
                            {
                                row.Cells["Other"].Value = false;
                            }
                        }
                        dgvUserRIghts.EndEdit();
                    }
                }
                catch { }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!AnyRightCheck())
            {
                clsUtility.ShowInfoMessage("Please select any rights from rights grid.", clsUtility.strProjectTitle);
                grpRights.Focus();
                return;
            }
            SaveRightsData(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete user rights for User :  '" + txtName.Text + "'  ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                ObjCon.ExecuteNonQuery("DELETE " + clsUtility.DBName + ".[dbo].[tblUserRights] WHERE UserID=" + txtUserID.Text);
                clsUtility.ShowInfoMessage("Rights has been deleted.", clsUtility.strProjectTitle);
                ClearAll();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
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
    }
}