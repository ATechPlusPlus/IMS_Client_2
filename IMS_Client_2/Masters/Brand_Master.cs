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
    public partial class Brand_Master : Form
    {
        public Brand_Master()
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
            txtBrandName.Clear();
            cmbCountry.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            cmbActiveStatus.SelectedIndex = -1;

            txtBrandName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtBrandName))
            {
                clsUtility.ShowInfoMessage("Enter Brand Name           ", clsUtility.strProjectTitle);
                txtBrandName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbSupplier))
            {
                clsUtility.ShowInfoMessage("Select Supplier for " + txtBrandName.Text, clsUtility.strProjectTitle);
                cmbSupplier.Focus();
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.BrandMaster", "BrandName='" + txtBrandName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.BrandMaster", "BrandName='" + txtBrandName.Text + "' AND BrandID !=" + i);
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
            dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Brand_Master");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void FillSupplierData()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID,SupplierName", "ISNULL(ActiveStatus,1)=1", "SupplierName ASC");
            cmbSupplier.DataSource = dt;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";

            cmbSupplier.SelectedIndex = -1;
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
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpBrand.Enabled = true;
            txtBrandName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(0))
                {
                    ObjDAL.SetColumnData("BrandName", SqlDbType.NVarChar, txtBrandName.Text.Trim());
                    ObjDAL.SetColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                    ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.BrandMaster", true) > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                        clsUtility.ShowInfoMessage("Brand Name : '" + txtBrandName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpBrand.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Brand Name : '" + txtBrandName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                    }
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtBrandName.Text + "' Brand is already exist..", clsUtility.strProjectTitle);
                    ObjDAL.ResetData();
                    txtBrandName.Focus();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpBrand.Enabled = true;
            txtBrandName.Focus();
            txtBrandName.SelectionStart = txtBrandName.MaxLength;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(0))
                {
                    ObjDAL.UpdateColumnData("BrandName", SqlDbType.NVarChar, txtBrandName.Text.Trim());
                    ObjDAL.UpdateColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                    ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                    if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.BrandMaster", "BrandID = " + ID) > 0)
                    {
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                        clsUtility.ShowInfoMessage("'" + txtBrandName.Text + "' Brand is Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        grpBrand.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtBrandName.Text + "' Brand is not Updated", clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtBrandName.Text + "' Brand is already exist..", clsUtility.strProjectTitle);
                    txtBrandName.Focus();
                }
                ObjDAL.ResetData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtBrandName.Text + "' Brand ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.BrandMaster", "BrandID = " + ID) > 0)
                {
                    clsUtility.ShowInfoMessage("'" + txtBrandName.Text + "' Brand is deleted  ", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpBrand.Enabled = false;
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtBrandName.Text + "' Brand is not deleted  ", clsUtility.strProjectTitle);
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
                grpBrand.Enabled = false;
            }
        }

        private void txtBrandName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtBrandName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BrandID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtBrandName.Text = dataGridView1.SelectedRows[0].Cells["BrandName"].Value.ToString();
                    cmbSupplier.SelectedValue = dataGridView1.SelectedRows[0].Cells["SupplierID"].Value.ToString();
                    cmbCountry.SelectedValue = dataGridView1.SelectedRows[0].Cells["CountryID"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();
                    grpBrand.Enabled = false;
                    txtBrandName.Focus();
                }
                catch { }
            }
        }

        private void txtBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbSupplier.Focus();
                return;
            }
        }

        private void Brand_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            //clsUtility.IsAdmin = true;//removed

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning, clsUtility.IsAdmin);

            LoadData();
            FillSupplierData();
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

        private void rdSearchByBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByBrand.Checked)
            {
                txtSearchByBrand.Enabled = true;
                txtSearchByBrand.Focus();
            }
            else
            {
                txtSearchByBrand.Enabled = false;
                txtSearchByBrand.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByBrand.Enabled = false;
                txtSearchByBrand.Clear();
                LoadData();
            }
        }

        private void txtSearchByBrand_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByBrand.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            //DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.BrandMaster", "BrandID,BrandName,SupplierID,CountryID,(CASE WHEN ActiveStatus =1 THEN 'Active' WHEN ActiveStatus =0 THEN 'InActive' END) ActiveStatus", "BrandName LIKE '%" + txtSearchByBrand.Text + "%'", "BrandName");
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Brand_Master '" + txtSearchByBrand.Text.Trim() + "'");
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
            dataGridView1.Columns["BrandID"].Visible = false;
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void btnSupplierPopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbSupplier.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbSupplier.SelectedValue);
            }
            Masters.Supplier_Details Obj = new Supplier_Details();
            Obj.ShowDialog();
            FillSupplierData();
            if (a > 0)
            {
                cmbSupplier.SelectedValue = a;
            }
        }

        private void cmbSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int a = ObjDAL.ExecuteScalarInt("SELECT CountryID FROM " + clsUtility.DBName + ".[dbo].[SupplierMaster] WITH(NOLOCK) WHERE SupplierID=" + cmbSupplier.SelectedValue);
                if (a > 0)
                {
                    cmbCountry.SelectedValue = a;
                }
                else
                {
                    cmbCountry.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void txtBrandName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtBrandName.Focus();
            }
        }
    }
}