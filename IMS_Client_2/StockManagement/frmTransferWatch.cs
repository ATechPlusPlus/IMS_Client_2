using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.StockManagement
{
    public partial class frmTransferWatch : Form
    {
        public frmTransferWatch()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void dgvTransferWatch_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            ObjUtil.SetDataGridProperty(dgvTransferWatch, DataGridViewAutoSizeColumnsMode.DisplayedCells);
            //ObjUtil.SetDataGridProperty(dgvTransferWatch, DataGridViewAutoSizeColumnsMode.Fill);

            dgvTransferWatch.Columns["StoreTransferID"].Visible = false;
            dgvTransferWatch.Columns["CreatedBy"].Visible = false;
            if (dgvTransferWatch.DataSource != null)
            {
                ObjUtil.SetRowNumber(dgvTransferWatch);
                if (dgvTransferWatch.Columns.Contains("ColView"))
                {
                    dgvTransferWatch.Columns.Remove("ColView");
                }
                DataGridViewButtonColumn ColView = new DataGridViewButtonColumn();
                ColView.DataPropertyName = "View";
                ColView.HeaderText = "View";
                ColView.Name = "ColView";
                ColView.Text = "View";
                ColView.UseColumnTextForButtonValue = true;
                //dataGridView1.Columns.Add(ColView);
                dgvTransferWatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColView });
            }
        }

        private void LoadData()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("BillStatus", SqlDbType.Int, DBNull.Value, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_List");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        dgvTransferWatch.DataSource = dt;
                    }
                    else
                    {
                        dgvTransferWatch.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void frmTransferWatch_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void rdByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByDate.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                cmbBillStatus.SelectedIndex = -1;
                cmbBillStatus.Enabled = false;

                dgvTransferWatch.DataSource = null;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }

        private void rdByBillStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByBillStatus.Checked)
            {
                cmbBillStatus.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;

                dgvTransferWatch.DataSource = null;
            }
            else
            {
                cmbBillStatus.SelectedIndex = -1;
                cmbBillStatus.Enabled = false;
            }
        }

        private void SearchByDate()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BillStatus", SqlDbType.Int, DBNull.Value, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_List");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvTransferWatch.DataSource = dt;
                }
                else
                {
                    dgvTransferWatch.DataSource = null;
                }
            }
        }

        private void SearchByBillStatus()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BillStatus", SqlDbType.Int, cmbBillStatus.SelectedIndex, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_List");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvTransferWatch.DataSource = dt;
                }
                else
                {
                    dgvTransferWatch.DataSource = null;
                }
            }
        }

        private void cmbBillStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByBillStatus();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            SearchByDate();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            SearchByDate();
        }

        private void dgvTransferWatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (dgvTransferWatch.Columns[e.ColumnIndex].Name == "ColView")
            {
                StockManagement.frmTransferWatch_Items Obj = new frmTransferWatch_Items();
                Obj.pStoreBillDetailsID = dgvTransferWatch.SelectedRows[0].Cells["StoreTransferID"].Value == DBNull.Value ? 0 : Convert.ToInt32(dgvTransferWatch.SelectedRows[0].Cells["StoreTransferID"].Value);
                Obj.ShowDialog();
                //if (clsFormRights.HasFormRight(clsFormRights.Forms.Sales_Invoice) || clsUtility.IsAdmin)
                //{
                //    StockManagement.frmTransferWatch_Items Obj = new frmTransferWatch_Items();
                //    Obj.Show();
                //}
                //else
                //{
                //    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                //}
            }
        }

        private void SearchByALL()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("BillStatus", SqlDbType.Int, -1, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_List");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        dgvTransferWatch.DataSource = dt;
                    }
                    else
                    {
                        dgvTransferWatch.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void rdSearchByAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByAll.Checked)
            {
                cmbBillStatus.Enabled = false;
                cmbBillStatus.SelectedIndex = -1;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;

                SearchByALL();
            }
        }
    }
}