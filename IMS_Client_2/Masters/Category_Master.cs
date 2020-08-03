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
    public partial class Category_Master : Form
    {
        public Category_Master()
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
            txtCategoryName.Clear();
            txtCategoryDescription.Clear();
            txtSearchByCategory.Clear();
            cmbActiveStatus.SelectedIndex = -1;

            txtCategoryName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtCategoryName))
            {
                clsUtility.ShowInfoMessage("Enter Category Name           ", clsUtility.strProjectTitle);
                txtCategoryName.Focus();
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryName='" + txtCategoryName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryName='" + txtCategoryName.Text + "' AND CategoryID !=" + i);
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
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName,CategoryDescription,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "CategoryName");

            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            grpCategory.Enabled = true;
            txtCategoryName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Category_Master, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("CategoryName", SqlDbType.NVarChar, txtCategoryName.Text.Trim());
                        ObjDAL.SetColumnData("CategoryDescription", SqlDbType.NVarChar, txtCategoryDescription.Text);
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.CategoryMaster", true) > 0)
                        {
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            clsUtility.ShowInfoMessage("Category Name : '" + txtCategoryName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpCategory.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Category Name : '" + txtCategoryName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCategoryName.Text + "' Category is already exist..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                        txtCategoryName.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Category_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpCategory.Enabled = true;
                txtCategoryName.Focus();
                txtCategoryName.SelectionStart = txtCategoryName.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Category_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("CategoryName", SqlDbType.NVarChar, txtCategoryName.Text.Trim());
                        ObjDAL.UpdateColumnData("CategoryDescription", SqlDbType.NVarChar, txtCategoryDescription.Text);
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID = " + ID + "") > 0)
                        {
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);

                            clsUtility.ShowInfoMessage("'" + txtCategoryName.Text + "' Category is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpCategory.Enabled = false;
                            ObjDAL.ResetData();
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtCategoryName.Text + "' Category is not Updated", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCategoryName.Text + "' Category is already exist..", clsUtility.strProjectTitle);
                        txtCategoryName.Focus();
                        ObjDAL.ResetData();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Category_Master, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtCategoryName.Text + "' Category ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID=" + ID + "") > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtCategoryName.Text + "' Category is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpCategory.Enabled = false;
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCategoryName.Text + "' Category is not deleted  ", clsUtility.strProjectTitle);
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
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
                grpCategory.Enabled = false;
            }
        }

        private void txtCategoryName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtCategoryName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CategoryID"].Value);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtCategoryName.Text = dataGridView1.SelectedRows[0].Cells["CategoryName"].Value.ToString();
                    txtCategoryDescription.Text = dataGridView1.SelectedRows[0].Cells["CategoryDescription"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();

                    grpCategory.Enabled = false;
                    txtCategoryName.Focus();
                }
                catch { }
            }
        }

        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtCategoryDescription.Focus();
                txtCategoryDescription.SelectionStart = txtCategoryDescription.MaxLength;
                return;
            }
        }

        private void txtCategoryDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbActiveStatus.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtCategoryName.Focus();
                txtCategoryName.SelectionStart = txtCategoryName.MaxLength;
                return;
            }
        }

        private void Category_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

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

        private void rdSearchByCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByCategory.Checked)
            {
                txtSearchByCategory.Enabled = true;
                txtSearchByCategory.Focus();
            }
            else
            {
                txtSearchByCategory.Enabled = false;
                txtSearchByCategory.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByCategory.Enabled = false;
                txtSearchByCategory.Clear();
                LoadData();
            }
        }

        private void txtSearchByCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByCategory.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName,CategoryDescription,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "CategoryName LIKE '%" + txtSearchByCategory.Text + "%'", "CategoryName");
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
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["CategoryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void txtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCategoryName.Focus();
            }
        }
    }
}