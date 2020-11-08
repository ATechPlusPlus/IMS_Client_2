using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Inventory
{
    public partial class frmScanInventoryList : Form
    {
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        public frmScanInventoryList()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CompareStatus", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryList");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    DataTable dt1 = null;
                    dgvProductDetails.DataSource = dt1;
                }
            }
            else
            {
                DataTable dt1 = null;
                dgvProductDetails.DataSource = dt1;
            }
        }

        private void frmScanInventoryList_Load(object sender, EventArgs e)
        {
            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            LoadData();
            LoadFromStore();
        }

        private void LoadFromStore()
        {
            string strQ = "";
            if (clsUtility.IsAdmin)
            {
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK) WHERE ISNULL(ActiveStatus,1)=1 ORDER BY StoreName ASC";
            }
            else
            {
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WHERE ISNULL(ActiveStatus,1)=1 AND StoreID IN  " +
                           " (SELECT StoreID FROM  " + clsUtility.DBName + ".dbo.tblStoreUserRights WHERE UserID = " + clsUtility.LoginID + ") ORDER BY StoreName ASC";
            }
            DataTable dtFromStore = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtFromStore))
            {
                cmbSearchByStore.DataSource = dtFromStore;
                cmbSearchByStore.DisplayMember = "StoreName";
                cmbSearchByStore.ValueMember = "StoreID";
            }
            cmbSearchByStore.SelectedIndex = -1;
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvProductDetails.Columns["MasterScanID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["CompareStatus"].Visible = false;

            if (dgvProductDetails.DataSource != null)
            {
                if (dgvProductDetails.Columns.Contains("ColViewCompare"))
                {
                    dgvProductDetails.Columns.Remove("ColViewCompare");
                }
                DataGridViewButtonColumn ColView = new DataGridViewButtonColumn();
                ColView.DataPropertyName = "View Compare";
                ColView.HeaderText = "View Compare";
                ColView.Name = "ColViewCompare";
                ColView.Text = "View Compare";
                ColView.UseColumnTextForButtonValue = true;
                //dataGridView1.Columns.Add(ColView);
                dgvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColView });
            }
        }

        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "ColViewCompare")
            {

                if (clsFormRights.HasFormRight(clsFormRights.Forms.frmScanInventoryCompare) || clsUtility.IsAdmin)
                {
                    Inventory.frmScanInventoryCompare Obj = new frmScanInventoryCompare();
                    Obj.pMasterScanID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["MasterScanID"].Value);
                    Obj.comparestatus= Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["CompareStatus"].Value);

                    Obj.ScanBy = dgvProductDetails.SelectedRows[0].Cells["Scanned By"].Value.ToString();
                    Obj.ComparedDate = Convert.ToDateTime(dgvProductDetails.SelectedRows[0].Cells["Compared Date"].Value);
                    Obj.ShowDialog();

                    rdSearchByAll.Checked = true;
                    SearchByALL();
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                }
            }
        }

        private void rdByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByDate.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                cmbCompareStatus.SelectedIndex = -1;
                cmbCompareStatus.Enabled = false;
                cmbSearchByStore.SelectedIndex = -1;
                cmbSearchByStore.Enabled = false;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }

        private void rdSearchByStore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByStore.Checked)
            {
                cmbSearchByStore.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
            }
            else
            {
                cmbSearchByStore.SelectedIndex = -1;
                cmbSearchByStore.Enabled = false;
            }
        }

        private void rdByCompareStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByCompareStatus.Checked)
            {
                cmbCompareStatus.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
            }
            else
            {
                cmbCompareStatus.SelectedIndex = -1;
                cmbCompareStatus.Enabled = false;
            }
        }

        private void rdSearchByAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByAll.Checked)
            {
                cmbCompareStatus.Enabled = false;
                cmbCompareStatus.SelectedIndex = -1;
                cmbSearchByStore.Enabled = false;
                cmbSearchByStore.SelectedIndex = -1;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;

                SearchByALL();
            }
        }

        private void SearchByALL()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CompareStatus", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);

                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryList");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        dgvProductDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvProductDetails.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void SearchByDate()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CompareStatus", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryList");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
        }

        private void SearchByStore()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, cmbSearchByStore.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CompareStatus", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryList");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
        }

        private void SearchByCompareStatus()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CompareStatus", SqlDbType.Int, cmbCompareStatus.SelectedIndex, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryList");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            SearchByDate();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            SearchByDate();
        }

        private void cmbSearchByStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByStore();
        }

        private void cmbCompareStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByCompareStatus();
        }
    }
}