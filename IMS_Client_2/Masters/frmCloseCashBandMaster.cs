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
    public partial class frmCloseCashBandMaster : Form
    {
        public frmCloseCashBandMaster()
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
            txtCashBand.Clear();
            cmbActiveStatus.SelectedIndex = -1;

            txtCashBand.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtCashBand))
            {
                clsUtility.ShowInfoMessage("Enter Cash Band           ", clsUtility.strProjectTitle);
                txtCashBand.Focus();
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
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.tblCloseBALMaster", "CashBand='" + txtCashBand.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.tblCloseBALMaster", "CashBand='" + txtCashBand.Text + "' AND CashBandID !=" + i);
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
            ObjDAL.SetStoreProcedureData("Status", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashBand_Master");
            DataTable dt = ds.Tables[0];
            if (ObjUtil.ValidateTable(dt))
            {
                dgvCashBand.DataSource = dt;
            }
            else
            {
                dgvCashBand.DataSource = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpClosingBALMaster.Enabled = true;
            txtCashBand.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Brand_Master, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("CashBand", SqlDbType.Decimal, txtCashBand.Text.Trim());
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblCloseCashBandMaster", true) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage("CashBand : '" + txtCashBand.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpClosingBALMaster.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("CashBand Name : '" + txtCashBand.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCashBand.Text + "' CashBand is already exist..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                        txtCashBand.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Brand_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpClosingBALMaster.Enabled = true;
                txtCashBand.Focus();
                txtCashBand.SelectionStart = txtCashBand.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Brand_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("CashBand", SqlDbType.Decimal, txtCashBand.Text.Trim());
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.tblCloseCashBandMaster", "CashBandID = " + ID) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                            clsUtility.ShowInfoMessage("'" + txtCashBand.Text + "' CashBand is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpClosingBALMaster.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtCashBand.Text + "' CashBand is not Updated", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCashBand.Text + "' CashBand is already exist..", clsUtility.strProjectTitle);
                        txtCashBand.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Brand_Master, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtCashBand.Text + "' CashBand ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.tblCloseCashBandMaster", "CashBandID = " + ID) > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtCashBand.Text + "' CashBand is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpClosingBALMaster.Enabled = false;
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtCashBand.Text + "' CashBand is not deleted  ", clsUtility.strProjectTitle);
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
                grpClosingBALMaster.Enabled = false;
            }
        }

        private void txtCashBand_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtCashBand_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void frmCloseBALMaster_Load(object sender, EventArgs e)
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

        private void dgvCashBand_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvCashBand);
            ObjUtil.SetDataGridProperty(dgvCashBand, DataGridViewAutoSizeColumnsMode.Fill);
            dgvCashBand.Columns["CashBandID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dgvCashBand.Rows.Count;
        }

        private void txtCashBand_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtCashBand, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
                txtCashBand.Focus();
            }
        }

        private void dgvCashBand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dgvCashBand.SelectedRows[0].Cells["CashBandID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtCashBand.Text = dgvCashBand.SelectedRows[0].Cells["CashBand"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dgvCashBand.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();
                    grpClosingBALMaster.Enabled = false;
                    txtCashBand.Focus();
                }
                catch { }
            }
        }
    }
}