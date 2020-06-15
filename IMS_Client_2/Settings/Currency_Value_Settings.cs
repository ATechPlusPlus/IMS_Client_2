using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Settings
{
    public partial class Currency_Value_Settings : Form
    {
        public Currency_Value_Settings()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        int ID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void LoadData()
        {
            DataTable dt = null;
            dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_CurrencyRate");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void ClearAll()
        {
            cmbCountry.SelectedIndex = -1;
            txtCurrencyRate.Clear();
            txtCurrencyCode.Clear();
            txtCurrencyName.Clear();
            cmbCountry.Focus();
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

        private void Currency_Value_Settings_Load(object sender, EventArgs e)
        {
            //clsUtility.IsAdmin = true;//removed

            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning, clsUtility.IsAdmin);
            LoadData();
            FillCountryData();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(cmbCountry))
            {
                clsUtility.ShowInfoMessage("Select Country", clsUtility.strProjectTitle);
                cmbCountry.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtCurrencyRate))
            {
                clsUtility.ShowInfoMessage("Enter Currency Rate for " + cmbCountry.Text, clsUtility.strProjectTitle);
                txtCurrencyRate.Focus();
                return false;
            }
            else if (Convert.ToDecimal(txtCurrencyRate.Text) <= 0)
            {
                clsUtility.ShowInfoMessage("Enter Valid Currency Rate for " + cmbCountry.Text, clsUtility.strProjectTitle);
                txtCurrencyRate.Focus();
                return false;
            }
            return true;
        }

        private bool DuplicateUser(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CurrencyRateSetting", "CountryID=" + cmbCountry.SelectedValue);
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.CurrencyRateSetting", "CurrencyRateID !=" + i + " AND CountryID=" + cmbCountry.SelectedValue + " AND (CurrencyCode ='" + txtCurrencyCode.Text + "' OR CurrencyName = '" + txtCurrencyName.Text + "')");
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
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            grpCurrencyValue.Enabled = true;
            cmbCountry.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(0))
                {
                    ObjDAL.SetColumnData("CountryID", SqlDbType.Int, cmbCountry.SelectedValue);
                    ObjDAL.SetColumnData("CurrencyCode", SqlDbType.NVarChar, txtCurrencyCode.Text);
                    ObjDAL.SetColumnData("CurrencyName", SqlDbType.NVarChar, txtCurrencyName.Text);
                    ObjDAL.SetColumnData("CurrencyRate", SqlDbType.Decimal, txtCurrencyRate.Text);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.CurrencyRateSetting", true) > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                        clsUtility.ShowInfoMessage("Currency Rate for '" + cmbCountry.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpCurrencyValue.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Currency Rate for '" + cmbCountry.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    clsUtility.ShowErrorMessage("Currency Rate for '" + cmbCountry.Text + "' is already exist..", clsUtility.strProjectTitle);
                    cmbCountry.Focus();
                }
                ObjDAL.ResetData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
            grpCurrencyValue.Enabled = true;
            cmbCountry.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                if (DuplicateUser(ID))
                {
                    ObjDAL.UpdateColumnData("CountryID", SqlDbType.Int, cmbCountry.SelectedValue);
                    ObjDAL.UpdateColumnData("CurrencyCode", SqlDbType.NVarChar, txtCurrencyCode.Text);
                    ObjDAL.UpdateColumnData("CurrencyName", SqlDbType.NVarChar, txtCurrencyName.Text);
                    ObjDAL.UpdateColumnData("CurrencyRate", SqlDbType.Decimal, txtCurrencyRate.Text);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                    if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.CurrencyRateSetting", "CurrencyRateID = " + ID + "") > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);

                        clsUtility.ShowInfoMessage("Currency Rate for '" + cmbCountry.Text + "' is not Updated", clsUtility.strProjectTitle);
                        LoadData();
                        ClearAll();
                        grpCurrencyValue.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Currency Rate for '" + cmbCountry.Text + "' is not Updated", clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    clsUtility.ShowErrorMessage("Currency Rate for '" + cmbCountry.Text + "' is already exist..", clsUtility.strProjectTitle);
                    cmbCountry.Focus();
                }
                ObjDAL.ResetData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete?", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.CurrencyRateSetting", "CurrencyRateID=" + ID) > 0)
                {
                    clsUtility.ShowInfoMessage("Currency Rate for '" + cmbCountry.Text + "' is Deleted  ", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpCurrencyValue.Enabled = false;
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                }
                else
                {
                    clsUtility.ShowErrorMessage("Currency Rate for '" + cmbCountry.Text + "' is not Deleted  ", clsUtility.strProjectTitle);
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
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
                grpCurrencyValue.Enabled = false;
            }
        }

        private void txtCurrencyRate_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtCurrencyRate_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            //ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["CurrencyRateID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CurrencyRateID"].Value);
                    txtCurrencyRate.Text = dataGridView1.SelectedRows[0].Cells["CurrencyRate"].Value.ToString();
                    cmbCountry.SelectedValue = dataGridView1.SelectedRows[0].Cells["CountryID"].Value.ToString();
                    txtCurrencyCode.Text = dataGridView1.SelectedRows[0].Cells["CurrencyCode"].Value.ToString();
                    txtCurrencyName.Text = dataGridView1.SelectedRows[0].Cells["CurrencyName"].Value.ToString();
                    grpCurrencyValue.Enabled = false;
                    cmbCountry.Focus();
                }

                catch (Exception ex)
                {
                    clsUtility.ShowInfoMessage(ex.ToString(), clsUtility.strProjectTitle);
                }
            }
        }

        private void btnCountryPopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbCountry.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbCountry.SelectedValue);
            }
            Masters.Country_Master Obj = new Masters.Country_Master();
            Obj.ShowDialog();
            FillCountryData();
            if (a > 0)
            {
                cmbCountry.SelectedValue = a;
            }
        }

        private void txtCurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtCurrencyRate, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...", clsUtility.strProjectTitle);
                txtCurrencyRate.Focus();
            }
        }

        private void txtCurrencyCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCurrencyCode.Focus();
            }
        }

        private void txtCurrencyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsString(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Charactors...", clsUtility.strProjectTitle);
                txtCurrencyName.Focus();
            }
        }
    }
}