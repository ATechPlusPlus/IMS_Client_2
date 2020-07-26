using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmDashBoardSettings : Form
    {
        public frmDashBoardSettings()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void frmDashBoardSettings_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;

            LoadShop();
        }
        private void LoadShop()
        {
            DataTable dt = null;
            dt = ObjCon.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName,Place,case StoreCategory when 0 then 'Normal' when 1 then'WearHouse' end as Category ", "StoreName");

            if (ObjUtil.ValidateTable(dt))
            {
                dgvShopeDetails.DataSource = dt;
            }
            else
            {
                dgvShopeDetails.DataSource = null;
            }
        }
        private void btnViewDetails_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnViewDetails_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void dgvShopeDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvShopeDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvShopeDetails.Columns[0].ReadOnly = false;
        }
        private bool AnyRightCheck()
        {
            dgvShopeDetails.EndEdit();
            foreach (DataGridViewRow row in dgvShopeDetails.Rows)
            {
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
                if (row.Cells["colCheck"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["colCheck"].Value))
                {
                    return true;
                }
            }
            return false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbGenerate.SelectedIndex==-1)
            {
                clsUtility.ShowInfoMessage("Please select the specification.", clsUtility.strProjectTitle);
                return;
            }

            else if (AnyRightCheck() == false)
            {
                CoreApp.clsUtility.ShowInfoMessage("Please select shop.", clsUtility.strProjectTitle);
                return;

            }

            StringBuilder bs = new StringBuilder();
            dgvShopeDetails.EndEdit();
            for (int i = 0; i < dgvShopeDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvShopeDetails.Rows[i].Cells["colCheck"].Value))
                {
                    string storeID = dgvShopeDetails.Rows[i].Cells["StoreID"].Value.ToString();
                    bs.Append(storeID);
                    bs.Append(",");


                }

            }

         string StoreID=  bs.ToString().Remove(bs.ToString().Length - 1, 1);

            ObjCon.ExecuteNonQuery("Delete IMS_Client_2.dbo.tblDashBoard ");

       
            ObjCon.SetColumnData("Shopes", SqlDbType.NVarChar, StoreID);
            ObjCon.SetColumnData("FromDate", SqlDbType.Date,dtpFromDate.Value.ToString("yyyy-MM-dd"));
            ObjCon.SetColumnData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"));
            ObjCon.SetColumnData("Specification", SqlDbType.Int,cmbGenerate.SelectedIndex);
            ObjCon.SetColumnData("RefreshRate", SqlDbType.Int,Convert.ToInt32(numericUpDown1.Value));
            ObjCon.InsertData("IMS_Client_2.dbo.tblDashBoard", false);

            clsUtility.ShowInfoMessage("Dashboard settings has been saved.", clsUtility.strProjectTitle);
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int k = 0; k < dgvShopeDetails.Rows.Count; k++)
                {
                    dgvShopeDetails.Rows[k].Cells["colCheck"].Value = true;

                }
                dgvShopeDetails.EndEdit();
            }
            else
            {
                for (int k = 0; k < dgvShopeDetails.Rows.Count; k++)
                {
                    dgvShopeDetails.Rows[k].Cells["colCheck"].Value = false;

                }
                dgvShopeDetails.EndEdit();

            }
        }
    }
}
