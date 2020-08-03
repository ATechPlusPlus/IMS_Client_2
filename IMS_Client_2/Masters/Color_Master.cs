using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Masters
{
    public partial class Color_Master : Form
    {
        public Color_Master()
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
            txtColorName.Clear();
            cmbActiveStatus.SelectedIndex = -1;

            txtColorName.Focus();
        }

        private bool ValidateForm()
        {
            if (ObjUtil.IsControlTextEmpty(txtColorName))
            {
                clsUtility.ShowInfoMessage("Enter Color Name       ", clsUtility.strProjectTitle);
                txtColorName.Focus();
                return false;
            }

            else if (ObjUtil.IsControlTextEmpty(cmbActiveStatus))
            {
                clsUtility.ShowErrorMessage("Select Active Status.", clsUtility.strProjectTitle);
                cmbActiveStatus.Focus();
                return false;
            }
            return true;
        }

        private bool DuplicateColor(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.ColorMaster", "ColorName='" + txtColorName.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.ColorMaster", "ColorName='" + txtColorName.Text + "' AND ColorID !=" + i);
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
            ObjDAL.SetStoreProcedureData("ColorName", SqlDbType.NVarChar, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Color_Master");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvColorMaster.DataSource = dt;
                }
                else
                {
                    dgvColorMaster.DataSource = null;
                }
            }
            else
            {
                dgvColorMaster.DataSource = null;
            }
            ObjDAL.ResetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpColorDetails.Enabled = true;
            txtColorName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Color_Master, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (ValidateForm())
                {
                    if (DuplicateColor(0))
                    {
                        ObjDAL.SetColumnData("ColorName", SqlDbType.NVarChar, txtColorName.Text.Trim());
                        ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.ColorMaster", true) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage("Color Name : '" + txtColorName.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpColorDetails.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Color Name : '" + txtColorName.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtColorName.Text + "' Color is already exist..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                        txtColorName.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Color_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpColorDetails.Enabled = true;
                txtColorName.Focus();
                txtColorName.SelectionStart = txtColorName.MaxLength;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Color_Master, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (ValidateForm())
                {
                    if (DuplicateColor(ID))
                    {
                        ObjDAL.UpdateColumnData("ColorName", SqlDbType.NVarChar, txtColorName.Text.Trim());
                        ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.ColorMaster", "ColorID = " + ID + "") > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                            clsUtility.ShowInfoMessage("'" + txtColorName.Text + "' Color is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpColorDetails.Enabled = false;
                            ObjDAL.ResetData();
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("'" + txtColorName.Text + "' Color is not Updated", clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtColorName.Text + "' Color is already exist..", clsUtility.strProjectTitle);
                        txtColorName.Focus();
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
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Color_Master, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtColorName.Text + "' Color ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.ColorMaster", "ColorID = " + ID + "") > 0)
                    {
                        clsUtility.ShowInfoMessage("'" + txtColorName.Text + "' Color is deleted  ", clsUtility.strProjectTitle);
                        ClearAll();
                        LoadData();
                        grpColorDetails.Enabled = false;
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("'" + txtColorName.Text + "' Color is not deleted  ", clsUtility.strProjectTitle);
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
                grpColorDetails.Enabled = false;
            }
        }

        private void txtColorName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtColorName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dgvColorMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dgvColorMaster.SelectedRows[0].Cells["ColorID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);

                    txtColorName.Text = dgvColorMaster.SelectedRows[0].Cells["ColorName"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dgvColorMaster.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();

                    grpColorDetails.Enabled = false;
                    txtColorName.Focus();
                }
                catch { }
            }
        }
        clsThreadTask thr = new clsThreadTask();
        private void Color_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            dgvColorMaster.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvColorMaster.RowHeadersVisible = false; // set it to false if not needed

            LoadData();

            grpColorDetails.Enabled = false;
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

        private void rdSearchByColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByColor.Checked)
            {
                txtSearchByColor.Enabled = true;
                txtSearchByColor.Focus();
            }
            else
            {
                txtSearchByColor.Enabled = false;
                txtSearchByColor.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByColor.Enabled = false;
                txtSearchByColor.Clear();
                LoadData();
            }
        }

        private void txtSearchByColor_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByColor.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }

            ObjDAL.SetStoreProcedureData("ColorName", SqlDbType.NVarChar, txtSearchByColor.Text.Trim(), clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Color_Master");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvColorMaster.DataSource = dt;
                }
                else
                {
                    dgvColorMaster.DataSource = null;
                }
            }
            else
            {
                dgvColorMaster.DataSource = null;
            }
            ObjDAL.ResetData();
        }

        private void dgvColorMaster_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvColorMaster);
            ObjUtil.SetDataGridProperty(dgvColorMaster, DataGridViewAutoSizeColumnsMode.Fill);
            dgvColorMaster.Columns["ColorID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dgvColorMaster.Rows.Count;
        }
    }
}