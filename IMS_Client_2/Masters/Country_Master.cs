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
    public partial class Country_Master : Form
    {
        public Country_Master()
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
            txtCountryCode.Clear();
            txtCountryName.Clear();
            txtSearchByCountry.Clear();
            cmbActiveStatus.SelectedIndex = -1;

            txtCountryCode.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtCountryCode))
            {
                clsUtility.ShowInfoMessage("Enter Country Code           ", clsUtility.strProjectTitle);
                txtCountryCode.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtCountryName))
            {
                clsUtility.ShowInfoMessage("Enter Country Name            ", clsUtility.strProjectTitle);
                txtCountryName.Focus();
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CountryMaster", "CountryName='" + txtCountryName.Text.Trim() + "'");
                if (a == 0)
                {
                    a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CountryMaster", "CountryCode='" + txtCountryCode.Text + "'");
                }
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CountryMaster", "CountryID !=" + i + " AND (CountryName ='" + txtCountryName.Text + "' OR CountryCode = '" + txtCountryCode.Text + "')");
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
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CountryMaster", "CountryID,CountryCode,CountryName,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "CountryName");

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
            grpCountry.Enabled = true;
            txtCountryCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(0))
                {
                    ObjDAL.SetColumnData("CountryCode", SqlDbType.NVarChar, txtCountryCode.Text.Trim());
                    ObjDAL.SetColumnData("CountryName", SqlDbType.NVarChar, txtCountryName.Text);
                    ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.CountryMaster", true) > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                        clsUtility.ShowInfoMessage("Country Name : '" + txtCountryName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpCountry.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Country Name : '" + txtCountryName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtCountryName.Text + "' Country Name OR Country Code is already exist..", clsUtility.strProjectTitle);
                    txtCountryName.Focus();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpCountry.Enabled = true;
            txtCountryCode.Focus();
            txtCountryCode.SelectionStart = txtCountryCode.MaxLength;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(ID))
                {
                    ObjDAL.UpdateColumnData("CountryCode", SqlDbType.NVarChar, txtCountryCode.Text.Trim());
                    ObjDAL.UpdateColumnData("CountryName", SqlDbType.NVarChar, txtCountryName.Text);
                    ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                    if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.CountryMaster", "CountryID = " + ID + "") > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                        clsUtility.ShowInfoMessage("'" + txtCountryName.Text + "' Country Name is Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        grpCountry.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCountryName.Text + "' Country Name is not Updated", clsUtility.strProjectTitle);
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtCountryName.Text + "' Country Name is already exist..", clsUtility.strProjectTitle);
                    txtCountryName.Focus();
                    ObjDAL.ResetData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtCountryName.Text + "' Country ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.CountryMaster", "CountryName='" + txtCountryName.Text.Trim() + "'") > 0)
                {
                    clsUtility.ShowInfoMessage("'" + txtCountryName.Text + "' Country Name is deleted  ", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpCountry.Enabled = false;
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtCountryName.Text + "' Country Name is not deleted  ", clsUtility.strProjectTitle);
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
                grpCountry.Enabled = false;
            }
        }

        private void txtCountryCode_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtCountryCode_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CountryID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtCountryCode.Text = dataGridView1.SelectedRows[0].Cells["CountryCode"].Value.ToString();
                    txtCountryName.Text = dataGridView1.SelectedRows[0].Cells["CountryName"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();

                    grpCountry.Enabled = false;
                    txtCountryCode.Focus();
                }
                catch { }
            }
        }

        private void txtCountryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtCountryName.Focus();
                txtCountryName.SelectionStart = txtCountryName.MaxLength;
                return;
            }
        }

        private void txtCountryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbActiveStatus.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtCountryCode.Focus();
                txtCountryCode.SelectionStart = txtCountryCode.MaxLength;
                return;
            }
        }

        private void Country_Master_Load(object sender, EventArgs e)
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

        private void txtSearchByCountry_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByCountry.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CountryMaster", "CountryID,CountryCode,CountryName,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "CountryName LIKE '%" + txtSearchByCountry.Text + "%'", "CountryName");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByCountry.Enabled = false;
                txtSearchByCountry.Clear();
                LoadData();
            }
        }

        private void rdSearchByCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByCountry.Checked)
            {
                txtSearchByCountry.Enabled = true;
                txtSearchByCountry.Focus();
            }
            else
            {
                txtSearchByCountry.Enabled = false;
                txtSearchByCountry.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["CountryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void txtCountryCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCountryCode.Focus();
            }
        }
    }
}
