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
    public partial class Store_Master : Form
    {
        public Store_Master()
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
            txtStoreName.Clear();
            txtTel.Clear();
            txtFax.Clear();
            txtPlace.Clear();
            cmbActiveStatus.SelectedIndex = -1;
            cmbStoreCat.SelectedIndex = -1;
            txtStoreName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtStoreName))
            {
                clsUtility.ShowInfoMessage("Enter Store Name           ", clsUtility.strProjectTitle);
                txtStoreName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtPlace))
            {
                clsUtility.ShowInfoMessage("Enter Place for " + txtStoreName.Text + " Store", clsUtility.strProjectTitle);
                txtPlace.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbStoreCat))
            {
                clsUtility.ShowInfoMessage("Select store category.", clsUtility.strProjectTitle);
                cmbStoreCat.Focus();
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.StoreMaster", "StoreName='" + txtStoreName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.StoreMaster", "StoreName='" + txtStoreName.Text + "' AND StoreID !=" + i);
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
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName,Tel,Place,Fax,(CASE ActiveStatus WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END) ActiveStatus,(CASE StoreCategory WHEN 0 THEN 'Normal Store' WHEN 1 THEN 'Warehouse' END) StoreCategory,StoreCategory [StoreCategoryID] ", "StoreName");
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
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpStore.Enabled = true;
            txtStoreName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("StoreName", SqlDbType.NVarChar, txtStoreName.Text.Trim());
                        ObjDAL.SetColumnData("Tel", SqlDbType.VarChar, txtTel.Text.Trim());
                        ObjDAL.SetColumnData("Fax", SqlDbType.VarChar, txtFax.Text.Trim());
                        ObjDAL.SetColumnData("Place", SqlDbType.NVarChar, txtPlace.Text.Trim());
                        ObjDAL.SetColumnData("StoreCategory", SqlDbType.Int, cmbStoreCat.SelectedIndex);
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.StoreMaster", true) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);

                            clsUtility.ShowInfoMessage("Store : '" + txtStoreName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpStore.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Store : '" + txtStoreName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Store : '" + txtStoreName.Text + "' is already exist..", clsUtility.strProjectTitle);
                        txtStoreName.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpStore.Enabled = true;
                txtStoreName.Focus();
                txtStoreName.SelectionStart = txtStoreName.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("StoreName", SqlDbType.NVarChar, txtStoreName.Text.Trim());
                        ObjDAL.UpdateColumnData("Tel", SqlDbType.VarChar, txtTel.Text.Trim());
                        ObjDAL.UpdateColumnData("Fax", SqlDbType.VarChar, txtFax.Text.Trim());
                        ObjDAL.UpdateColumnData("Place", SqlDbType.NVarChar, txtPlace.Text.Trim());
                        ObjDAL.UpdateColumnData("StoreCategory", SqlDbType.Int, cmbStoreCat.SelectedIndex);
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.StoreMaster", "StoreID = " + ID + "") > 0)
                        {
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);
                            clsUtility.ShowInfoMessage("'" + txtStoreName.Text + "' Store is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpStore.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtStoreName.Text + "' Store is not Updated", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtStoreName.Text + "' Store is already exist..", clsUtility.strProjectTitle);
                        txtStoreName.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtStoreName.Text + "' Store ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.StoreMaster", "StoreID=" + ID) > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtStoreName.Text + "' Store is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpStore.Enabled = false;
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtStoreName.Text + "' Store is not deleted  ", clsUtility.strProjectTitle);
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
                grpStore.Enabled = false;
            }
        }

        private void txtStoreName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtStoreName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ClearAll();
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StoreID"].Value);
                    txtStoreName.Text = dataGridView1.SelectedRows[0].Cells["StoreName"].Value.ToString();
                    txtTel.Text = dataGridView1.SelectedRows[0].Cells["Tel"].Value.ToString();
                    txtFax.Text = dataGridView1.SelectedRows[0].Cells["Fax"].Value.ToString();
                    txtPlace.Text = dataGridView1.SelectedRows[0].Cells["Place"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();
                    cmbStoreCat.SelectedIndex = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["StoreCategoryID"].Value);

                    grpStore.Enabled = false;
                    txtStoreName.Focus();
                }
                catch { }
            }
        }

        private void txtStoreName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtPlace.Focus();
                return;
            }
        }

        private void txtPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtTel.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtStoreName.Focus();
                txtStoreName.SelectionStart = txtStoreName.MaxLength;
                return;
            }
        }

        private void txtTel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtFax.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtPlace.Focus();
                txtPlace.SelectionStart = txtPlace.MaxLength;
                return;
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbActiveStatus.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtTel.Focus();
                txtTel.SelectionStart = txtTel.Text.Length;
                return;
            }
        }

        private void Store_Master_Load(object sender, EventArgs e)
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

        private void rdSearchByStoreName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByStoreName.Checked)
            {
                txtSearchByStoreName.Enabled = true;
                txtSearchByStoreName.Focus();
            }
            else
            {
                rdSearchByStoreName.Enabled = false;
                txtSearchByStoreName.Clear();
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByStoreName.Enabled = false;
                txtSearchByStoreName.Clear();
                LoadData();
            }
        }

        private void txtSearchByStoreName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByStoreName.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID, StoreName, Tel, Fax, Place, (CASE ActiveStatus WHEN 1 THEN 'Active' WHEN 0 THEN 'InActive' END) ActiveStatus,(CASE StoreCategory WHEN 0 THEN 'Normal Store' WHEN 1 THEN 'Warehouse' END) StoreCategory,StoreCategory [StoreCategoryID] ", "StoreName LIKE '%" + txtSearchByStoreName.Text + "%'", "StoreName");

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
            dataGridView1.Columns["StoreID"].Visible = false;
            dataGridView1.Columns["StoreCategoryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void txtStoreName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtStoreName.Focus();
            }
        }
    }
}