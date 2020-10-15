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
    public partial class Employee_Details : Form
    {
        public Employee_Details()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        int EmployeeID = 0;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PicEmployee.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void Employee_Details_Load(object sender, EventArgs e)
        {
            dtpDOB.ShowCheckBox = true;
            dtpDOB.Checked = false;

            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            FillStoreDetails();
            LoadData();
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpEmployee.Enabled = true;
            txtEmployeeCode.Focus();
        }
        private void FillStoreDetails()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", " StoreID");
            cmbShop.DataSource = dt;
            cmbShop.DisplayMember = "StoreName";
            cmbShop.ValueMember = "StoreID";

            cmbShop.SelectedIndex = -1;
        }
        private void ClearAll()
        {
            txtName.Clear();
            txtPass.Clear();
            txtUsername.Clear();
            txtEmployeeCode.Clear();
            radMale.Checked = false;
            radFemale.Checked = false;
            txtAdd.Clear();
            PicEmployee.Image = null;
            cmbShop.SelectedIndex = -1;
            cmbActiveStatus.SelectedIndex = -1;
            EmployeeID = 0;
            txtEmail.Clear();
            dtpDOB.Value = DateTime.Now;
            dtpDOB.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (ValidateEmployee())
                {
                    SaveEmployee();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void CreateNewUser(int EmployeeID)
        {
            ObjDAL.SetColumnData("UserName", SqlDbType.NVarChar, txtUsername.Text.Trim());
            ObjDAL.SetColumnData("Password", SqlDbType.NVarChar, ObjUtil.Encrypt(txtPass.Text.Trim(), true));
            ObjDAL.SetColumnData("EmailID", SqlDbType.NVarChar, txtEmail.Text.Trim());
            ObjDAL.SetColumnData("IsAdmin", SqlDbType.Bit, false);
            ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
            ObjDAL.SetColumnData("EmployeeID", SqlDbType.Int, EmployeeID);

            if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.UserManagement", true) > 0)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                clsUtility.ShowInfoMessage("User has been added Successfully.", clsUtility.strProjectTitle);
                ClearAll();
                LoadData();
                grpEmployee.Enabled = false;
            }
            else
            {
                clsUtility.ShowInfoMessage("Failed to add the User.", clsUtility.strProjectTitle);
                ObjDAL.ResetData();
            }
        }

        private bool ValidateEmployee()
        {
            if (ObjUtil.IsControlTextEmpty(txtEmployeeCode))
            {
                clsUtility.ShowInfoMessage("Please Enter Employee Code.", clsUtility.strProjectTitle);
                txtEmployeeCode.Focus();
                return false;
            }

            else if (ObjUtil.IsControlTextEmpty(txtName))
            {
                clsUtility.ShowInfoMessage("Please Enter Employee Name.", clsUtility.strProjectTitle);
                txtName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbActiveStatus))
            {
                clsUtility.ShowInfoMessage("Please Select ActiveStatus.", clsUtility.strProjectTitle);
                cmbActiveStatus.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbShop))
            {
                clsUtility.ShowInfoMessage("Please Select Shop.", clsUtility.strProjectTitle);
                cmbShop.Focus();
                return false;
            }

            else if (radFemale.Checked == false && radMale.Checked == false)
            {
                clsUtility.ShowInfoMessage("Please Select Gender.", clsUtility.strProjectTitle);
                radMale.Focus();
                radMale.Checked = false;
                radFemale.Checked = false;
                return false;
            }

            int result = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".dbo.EmployeeDetails WHERE EmployeeCode='" + txtEmployeeCode.Text + "' AND EMPID<>" + EmployeeID);
            if (result > 0)
            {
                clsUtility.ShowInfoMessage("The Employee already exist with the given employee code. Please Enter different employee code.", clsUtility.strProjectTitle);
                txtEmployeeCode.Focus();
                return false;
            }

            if (txtUsername.Text.Trim().Length > 0) // if user name is entered
            {
                if (ObjUtil.IsControlTextEmpty(txtPass))
                {
                    clsUtility.ShowInfoMessage("Please Enter Password.", clsUtility.strProjectTitle);
                    txtPass.Focus();
                    return false;
                }

                int countuser = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".dbo.UserManagement WHERE UserName='" + txtUsername.Text + "' AND EmployeeID<>" + EmployeeID);
                if (countuser > 0)
                {
                    clsUtility.ShowInfoMessage("The user name already exist.Please enter different user name.", clsUtility.strProjectTitle);
                    txtUsername.Focus();
                    return false;
                }
            }
            return true;
        }

        private void SaveEmployee()
        {
            ObjDAL.SetColumnData("EmployeeCode", SqlDbType.NVarChar, txtEmployeeCode.Text.Trim());
            ObjDAL.SetColumnData("Name", SqlDbType.NVarChar, txtName.Text.Trim());
            ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
            ObjDAL.SetColumnData("ShopID", SqlDbType.Int, cmbShop.SelectedValue);
            if (radMale.Checked)
            {
                ObjDAL.SetColumnData("Gender", SqlDbType.Bit, true);
            }
            else if (radFemale.Checked)
            {
                ObjDAL.SetColumnData("Gender", SqlDbType.Bit, false);
            }
            if (dtpDOB.Checked)
            {
                ObjDAL.SetColumnData("DOB", SqlDbType.DateTime, dtpDOB.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                ObjDAL.SetColumnData("DOB", SqlDbType.DateTime, DBNull.Value);
            }

            ObjDAL.SetColumnData("Address", SqlDbType.NVarChar, txtAdd.Text.Trim());

            if (PicEmployee.Image != null)
            {
                ObjDAL.SetColumnData("Photo", SqlDbType.VarBinary, ObjUtil.GetImageBytes(PicEmployee.Image));
            }
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            EmployeeID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.EmployeeDetails", true);

            if (EmployeeID > 0)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                clsUtility.ShowInfoMessage("Employee has been added Successfully.", clsUtility.strProjectTitle);

                if (txtUsername.Text.Trim().Length != 0 && txtPass.Text.Trim().Length != 0)
                {
                    CreateNewUser(EmployeeID);
                }
                ClearAll();
                LoadData();
                grpEmployee.Enabled = false;
            }
            else
            {
                clsUtility.ShowInfoMessage("Failed to add the employee.", clsUtility.strProjectTitle);
                ObjDAL.ResetData();
            }
        }

        private void LoadData()
        {
            string q = "SELECT e1.EmpID,EmployeeCode,Name,ShopID,(CASE e1.Gender WHEN 1 THEN 'Male' WHEN 0 THEN 'Female' END) Gender,DOB,[Address],Photo, s1.StoreName,(CASE e1.ActiveStatus WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END) ActiveStatus FROM " + clsUtility.DBName + ".dbo.EmployeeDetails e1 JOIN " + clsUtility.DBName + ".dbo.StoreMaster s1" +
              " ON e1.ShopID=s1.StoreID ORDER BY EmpID DESC";
            DataTable dataTable = ObjDAL.ExecuteSelectStatement(q);
            if (ObjUtil.ValidateTable(dataTable))
            {
                dgvEmployee.DataSource = dataTable;
                lblTotalRecords.Text = "Total Employee Count : " + dgvEmployee.Rows.Count.ToString();
            }
            else
            {
                dgvEmployee.DataSource = null;
                lblTotalRecords.Text = "Total Employee Count : 0";
            }
        }

        private void dgvEmployee_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvEmployee);
            ObjUtil.SetDataGridProperty(dgvEmployee, DataGridViewAutoSizeColumnsMode.Fill);
            dgvEmployee.Columns["ShopID"].Visible = false;
            dgvEmployee.Columns["Photo"].Visible = false;
            dgvEmployee.Columns["EmpID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dgvEmployee.Rows.Count;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PicEmployee.Image = null;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                ClearAll();
                LoadData();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                grpEmployee.Enabled = false;
            }
        }

        private void BindUserDetails()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".[dbo].[UserManagement] WHERE EmployeeID=" + EmployeeID);
            if (ObjUtil.ValidateTable(dt))
            {
                txtUsername.Text = dt.Rows[0]["UserName"].ToString();
                txtPass.Text = ObjUtil.Decrypt(dt.Rows[0]["Password"].ToString(), true);
                txtEmail.Text = dt.Rows[0]["EmailID"].ToString();
            }
        }
        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ClearAll();
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    EmployeeID = Convert.ToInt32(dgvEmployee.SelectedRows[0].Cells["EmpID"].Value);
                    txtEmployeeCode.Text = dgvEmployee.SelectedRows[0].Cells["EmployeeCode"].Value.ToString();
                    txtName.Text = dgvEmployee.SelectedRows[0].Cells["Name"].Value.ToString();
                    cmbShop.SelectedValue = Convert.ToInt32(dgvEmployee.SelectedRows[0].Cells["ShopID"].Value);

                    if (dgvEmployee.SelectedRows[0].Cells["Gender"].Value != DBNull.Value && dgvEmployee.SelectedRows[0].Cells["Gender"].Value.ToString() == "Male")
                    {
                        radMale.Checked = true;
                    }
                    else if (dgvEmployee.SelectedRows[0].Cells["Gender"].Value != DBNull.Value && dgvEmployee.SelectedRows[0].Cells["Gender"].Value.ToString() == "Female")
                    {
                        radFemale.Checked = true;
                    }

                    if (dgvEmployee.SelectedRows[0].Cells["DOB"].Value != DBNull.Value)
                    {
                        dtpDOB.Value = Convert.ToDateTime(dgvEmployee.SelectedRows[0].Cells["DOB"].Value);
                    }

                    if (dgvEmployee.SelectedRows[0].Cells["Address"].Value != DBNull.Value)
                    {
                        txtAdd.Text = dgvEmployee.SelectedRows[0].Cells["Address"].Value.ToString();
                    }

                    if (dgvEmployee.SelectedRows[0].Cells["Photo"].Value != DBNull.Value)
                    {
                        PicEmployee.Image = ObjUtil.GetImage((byte[])dgvEmployee.SelectedRows[0].Cells["Photo"].Value);
                    }
                    cmbActiveStatus.SelectedItem = dgvEmployee.SelectedRows[0].Cells["ActiveStatus"].Value;

                    grpEmployee.Enabled = false;
                    BindUserDetails();
                }
                catch { }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpEmployee.Enabled = true;
                txtEmployeeCode.Focus();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (ValidateEmployee())
                {
                    UpdateEmployee();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void UpdateEmployee()
        {
            ObjDAL.UpdateColumnData("EmployeeCode", SqlDbType.NVarChar, txtEmployeeCode.Text.Trim());
            ObjDAL.UpdateColumnData("Name", SqlDbType.NVarChar, txtName.Text.Trim());
            ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
            ObjDAL.UpdateColumnData("ShopID", SqlDbType.Int, cmbShop.SelectedValue);
            if (radMale.Checked)
            {
                ObjDAL.UpdateColumnData("Gender", SqlDbType.Bit, true);
            }
            else if (radFemale.Checked)
            {
                ObjDAL.UpdateColumnData("Gender", SqlDbType.Bit, false);
            }
            if (dtpDOB.Checked)
            {
                ObjDAL.UpdateColumnData("DOB", SqlDbType.DateTime, dtpDOB.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                ObjDAL.UpdateColumnData("DOB", SqlDbType.DateTime, DBNull.Value);
            }

            ObjDAL.UpdateColumnData("Address", SqlDbType.NVarChar, txtAdd.Text.Trim());

            if (PicEmployee.Image != null)
            {
                ObjDAL.UpdateColumnData("Photo", SqlDbType.VarBinary, ObjUtil.GetImageBytes(PicEmployee.Image));
            }

            ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.EmployeeDetails", "EMPID = " + EmployeeID + "") > 0)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);
                clsUtility.ShowInfoMessage("Employee Record has been updated.", clsUtility.strProjectTitle);

                if (txtUsername.Text.Trim().Length != 0 && txtPass.Text.Trim().Length != 0)
                {
                    int countuser = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".dbo.UserManagement WITH(NOLOCK) WHERE UserName='" + txtUsername.Text + "'");

                    if (countuser > 0)
                    {
                        UpdateUserDetails();
                    }
                    else
                    {
                        CreateNewUser(EmployeeID);
                    }
                }
                LoadData();
                ClearAll();
                grpEmployee.Enabled = false;
                ObjDAL.ResetData();
            }
            else
            {
                clsUtility.ShowErrorMessage("Failed to update the employee data.", clsUtility.strProjectTitle);
                ObjDAL.ResetData();
            }
        }

        private void UpdateUserDetails()
        {
            ObjDAL.UpdateColumnData("UserName", SqlDbType.NVarChar, txtUsername.Text.Trim());
            ObjDAL.UpdateColumnData("Password", SqlDbType.NVarChar, ObjUtil.Encrypt(txtPass.Text.Trim(), true));
            ObjDAL.UpdateColumnData("EmailID", SqlDbType.NVarChar, txtEmail.Text.Trim());
            ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
            ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //ObjDAL.UpdateColumnData("IsAdmin", SqlDbType.Bit, false); 
            //if user is blocked then if his details will update from this form then could be auto unblock

            //ObjDAL.UpdateColumnData("IsBlock", SqlDbType.Bit, false);
            if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.UserManagement", "EmployeeID = " + EmployeeID + "") > 0)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);
                clsUtility.ShowInfoMessage("User Login Record has been updated.", clsUtility.strProjectTitle);
                LoadData();
                ClearAll();
                grpEmployee.Enabled = false;
                ObjDAL.ResetData();
            }
            else
            {
                clsUtility.ShowErrorMessage("Failed to update the User Login Data.", clsUtility.strProjectTitle);
                ObjDAL.ResetData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtName.Text + "' ? ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.EmployeeDetails", "EmpID='" + EmployeeID + "'") > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtName.Text + "' Employee has been deleted.  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpEmployee.Enabled = false;
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Failed to delete the employee. ", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void txtEmployeeCode_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void btnStorePopup_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master) || clsUtility.IsAdmin)
            {
                int a = 0;
                if (cmbShop.SelectedIndex >= 0)
                {
                    a = Convert.ToInt32(cmbShop.SelectedValue);
                }
                Masters.Store_Master Obj = new Store_Master();
                Obj.ShowDialog();
                FillStoreDetails();
                if (a > 0)
                {
                    cmbShop.SelectedValue = a;
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtName.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}