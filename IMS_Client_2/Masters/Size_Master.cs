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
    public partial class Size_Master : Form
    {
        public Size_Master()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        DataTable dtSize = new DataTable();
        int ID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            cmbSizeType.SelectedIndex = -1;
            cmbActiveStatus.SelectedIndex = -1;
            txtSize.Clear();
            listBox1.Items.Clear();
            dtSize.Clear();
        }

        private bool ValidateForm()
        {
            if (listBox1.Items.Count == 0)
            {
                clsUtility.ShowInfoMessage("Please Add Size    ", clsUtility.strProjectTitle);
                return false;
            }
            return true;
        }

        private bool DuplicateColor(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SizeMaster", "SizeTypeID = " + cmbSizeType.SelectedValue + " AND Size='" + txtSize.Text.Trim() + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.SizeMaster", "SizeTypeID = " + cmbSizeType.SelectedValue + " AND Size='" + txtSize.Text.Trim() + "' AND SizeID!=" + i + "");
            }
            if (a > 0)
            {
                clsUtility.ShowInfoMessage("Size is already exist for Size Type " + cmbSizeType.Text, clsUtility.strProjectTitle);
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
            dt = ObjDAL.ExecuteSelectStatement("SELECT sm.SizeID, sm.Size, sm.SizeTypeID, stm.SizeTypeName" +
                ", (CASE WHEN sm.ActiveStatus = 1 THEN 'Active' WHEN sm.ActiveStatus = 0 THEN 'InActive' END) ActiveStatus FROM " + clsUtility.DBName + ".[dbo].[SizeMaster] sm " +
                  "INNER JOIN " + clsUtility.DBName + ".[dbo].[SizeTypeMaster] stm ON sm.SizeTypeID = stm.SizeTypeID ORDER BY sm.SizeTypeID,sm.Size");

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
            grpSizeType.Enabled = true;
            cmbSizeType.Focus();
            btnAddMore.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                int a = 0;
                for (int i = 0; i < dtSize.Rows.Count; i++)
                {
                    ObjDAL.SetColumnData("Size", SqlDbType.VarChar, dtSize.Rows[i]["Size"].ToString());
                    ObjDAL.SetColumnData("ActiveStatus", SqlDbType.Bit, Convert.ToInt32(dtSize.Rows[i]["ActiveStatus"]));
                    ObjDAL.SetColumnData("SizeTypeID", SqlDbType.Int, Convert.ToInt32(dtSize.Rows[i]["SizeTypeID"]));
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    a = ObjDAL.InsertData(clsUtility.DBName + ".dbo.SizeMaster", true);
                }
                if (a > 0)
                {
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                    clsUtility.ShowInfoMessage(clsUtility.MsgDataSaved, clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpSizeType.Enabled = false;
                    btnAddMore.Enabled = false;
                }
                else
                {
                    clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved, clsUtility.strProjectTitle);
                    ObjDAL.ResetData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
            grpSizeType.Enabled = true;
            txtSize.Focus();
            txtSize.SelectionStart = txtSize.MaxLength;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (ValidateForm())
            //{
            if (DuplicateColor(ID))
            {
                ObjDAL.UpdateColumnData("Size", SqlDbType.VarChar, txtSize.Text);
                ObjDAL.UpdateColumnData("SizeTypeID", SqlDbType.Int, cmbSizeType.SelectedValue);
                ObjDAL.UpdateColumnData("ActiveStatus", SqlDbType.Bit, cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0);
                ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.SizeMaster", "SizeID = " + ID + "") > 0)
                {
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                    clsUtility.ShowInfoMessage(clsUtility.MsgDataUpdated, clsUtility.strProjectTitle);
                    LoadData();
                    ClearAll();
                    grpSizeType.Enabled = false;
                }
                else
                {
                    clsUtility.ShowErrorMessage(clsUtility.MsgDatanotUpdated, clsUtility.strProjectTitle);
                }
            }
            else
            {
                //clsUtility.ShowErrorMessage("'" + txtSize.Text + "' Size is already exist..", clsUtility.strProjectTitle);
                txtSize.Focus();
            }
            ObjDAL.ResetData();
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure want to delete '" + txtSize.Text + "' Size ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (d == DialogResult.Yes)
            {
                if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.SizeMaster", "SizeID=" + ID + "") > 0)
                {
                    clsUtility.ShowInfoMessage("'" + txtSize.Text + "' Size is deleted  ", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                    grpSizeType.Enabled = false;
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                }
                else
                {
                    clsUtility.ShowErrorMessage("'" + txtSize.Text + "' Color is not deleted  ", clsUtility.strProjectTitle);
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
                grpSizeType.Enabled = false;
            }
        }

        private void Size_Master_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
            btnAddMore.BackgroundImage = B_Leave;

            //clsUtility.IsAdmin = true;//removed

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            FillSizeTypeData();
            FillSearchBySizeTypeData();
            InitItemTable();
            LoadData();

            grpSizeType.Enabled = false;
        }
        private void InitItemTable()
        {
            dtSize.Columns.Add("SizeTypeID");
            dtSize.Columns.Add("ActiveStatus");
            dtSize.Columns.Add("Size");
            dtSize.AcceptChanges();
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

        private void AddMoreSize()
        {
            if (ObjUtil.IsControlTextEmpty(cmbSizeType))
            {
                clsUtility.ShowInfoMessage("Please Select Size Type   ", clsUtility.strProjectTitle);
                cmbSizeType.Focus();
                return;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbActiveStatus))
            {
                clsUtility.ShowInfoMessage("Please Select Active Status for " + cmbSizeType.Text, clsUtility.strProjectTitle);
                cmbActiveStatus.Focus();
                return;
            }
            else if (ObjUtil.IsControlTextEmpty(txtSize))
            {
                clsUtility.ShowInfoMessage("Please enter Size for " + cmbSizeType.Text, clsUtility.strProjectTitle);
                txtSize.Focus();
                return;
            }
            else if (ObjUtil.IsNumeric(txtSize.Text))
            {
                if (Convert.ToInt32(txtSize.Text) == 0)
                {
                    clsUtility.ShowInfoMessage("Please enter Valid Size for " + cmbSizeType.Text, clsUtility.strProjectTitle);
                    txtSize.Focus();
                    return;
                }
                else if (listBox1.Items.Count > 0)
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (txtSize.Text == listBox1.Items[i].ToString())
                        {
                            clsUtility.ShowInfoMessage("Size is already entered for " + cmbSizeType.Text, clsUtility.strProjectTitle);
                            return;
                        }
                    }
                }
            }

            if (DuplicateColor(0))
            {
                listBox1.Items.Add(txtSize.Text);
                DataRow dRow = dtSize.NewRow();
                dRow["SizeTypeID"] = cmbSizeType.SelectedValue;
                dRow["ActiveStatus"] = cmbActiveStatus.SelectedItem.ToString() == "Active" ? 1 : 0;
                dRow["Size"] = txtSize.Text;
                dtSize.Rows.Add(dRow);
            }
        }
        private void btnAddMore_Click(object sender, EventArgs e)
        {
            AddMoreSize();
            ClearAddMoreSize();
        }

        private void ClearAddMoreSize()
        {
            txtSize.Clear();
            txtSize.Focus();
        }

        private void FillSizeTypeData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID,SizeTypeName", "ISNULL(ActiveStatus,1)=1", "SizeTypeName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbSizeType.DataSource = dt;
                cmbSizeType.DisplayMember = "SizeTypeName";
                cmbSizeType.ValueMember = "SizeTypeID";
            }
            cmbSizeType.SelectedIndex = -1;
        }

        private void FillSearchBySizeTypeData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID,SizeTypeName", "ISNULL(ActiveStatus,1)=1", "SizeTypeName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbSearchBySizeType.DataSource = dt;
                cmbSearchBySizeType.DisplayMember = "SizeTypeName";
                cmbSearchBySizeType.ValueMember = "SizeTypeID";
            }
            cmbSearchBySizeType.SelectedIndex = -1;
        }

        private void txtSize_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSize_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && listBox1.Items.Count > 0)
            {
                if (listBox1.SelectedIndex >= 0)
                {
                    DataRow[] dRow = dtSize.Select("Size= '" + listBox1.SelectedItem + "'");
                    for (int i = 0; i < dRow.Length; i++)
                        dtSize.Rows.Remove(dRow[i]);
                    dtSize.AcceptChanges();

                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["SizeID"].Visible = false;
            dataGridView1.Columns["SizeTypeID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void cmbSearchBySizeType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT sm.SizeID, sm.Size, sm.SizeTypeID, stm.SizeTypeName" + ", (CASE WHEN sm.ActiveStatus = 1 THEN 'Active' WHEN sm.ActiveStatus = 0 THEN 'InActive' END) ActiveStatus FROM "+clsUtility.DBName+".[dbo].[SizeMaster] sm " +
                  "INNER JOIN " + clsUtility.DBName + ".[dbo].[SizeTypeMaster] stm ON sm.SizeTypeID = stm.SizeTypeID WHERE sm.SizeTypeID = " + cmbSearchBySizeType.SelectedValue);

            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SizeID"].Value);
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    txtSize.Text = dataGridView1.SelectedRows[0].Cells["Size"].Value.ToString();
                    cmbSizeType.SelectedValue = dataGridView1.SelectedRows[0].Cells["SizeTypeID"].Value.ToString();
                    cmbActiveStatus.SelectedItem = dataGridView1.SelectedRows[0].Cells["ActiveStatus"].Value.ToString();
                    grpSizeType.Enabled = false;
                    txtSize.Focus();
                }
                catch { }
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                cmbSearchBySizeType.Enabled = false;
                cmbSearchBySizeType.SelectedIndex = -1;
                LoadData();
            }
        }

        private void rdSearchBySizeType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchBySizeType.Checked)
            {
                cmbSearchBySizeType.Enabled = true;
                cmbSearchBySizeType.Focus();
            }
            else
            {
                cmbSearchBySizeType.Enabled = false;
                cmbSearchBySizeType.SelectedIndex = -1;
                rdShowAll.Checked = true;
            }
        }

        private void btnSizeTypePopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbSizeType.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbSizeType.SelectedValue);
            }
            Masters.Size_Type_Master Obj = new Size_Type_Master();
            Obj.ShowDialog();
            FillSizeTypeData();
            if (a > 0)
            {
                cmbSizeType.SelectedValue = a;
            }
        }
    }
}