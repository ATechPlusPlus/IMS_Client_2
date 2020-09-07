using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Report
{
    public partial class frmPettyCashExpReport : Form
    {
        public frmPettyCashExpReport()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        DateTime dtDate = DateTime.Now;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PettyCashExpAmt");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ds.Tables.Count > 1)
                {
                    DataTable dtPettyCashBal = ds.Tables[1];
                    if (ObjUtil.ValidateTable(dtPettyCashBal))
                    {
                        txtTotalPettyCash.Text = dtPettyCashBal.Rows[0]["TotalPettyCashAmt"].ToString();
                        txtTotalExpenses.Text = dtPettyCashBal.Rows[0]["TotalPettyCashExpAmt"].ToString();
                        txtPettyCashBAL.Text = dtPettyCashBal.Rows[0]["PettyCashBalance"].ToString();
                    }
                }
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPettyCashExp.DataSource = dt;
                }
                else
                {
                    dgvPettyCashExp.DataSource = null;
                }
            }
        }

        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            cmbStore.DataSource = dt;
            cmbStore.DisplayMember = "StoreName";
            cmbStore.ValueMember = "StoreID";
            cmbStore.SelectedIndex = -1;
        }

        private void frmPettyCashExpReport_Load(object sender, EventArgs e)
        {
            btnPrint.BackgroundImage = B_Leave;
            btnClose.BackgroundImage = B_Leave;

            dtpFromDate.MaxDate = dtDate;
            dtpToDate.MaxDate = dtDate;

            FillStoreData();

            dgvPettyCashExp.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvPettyCashExp.RowHeadersVisible = false; // set it to false if not needed

            LoadData();

            cmbStore.SelectedValue = frmHome.Home_StoreID;
            cmbStore.Enabled = true;
        }

        private void dgvPettyCashExp_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvPettyCashExp);
            ObjUtil.SetDataGridProperty(dgvPettyCashExp, DataGridViewAutoSizeColumnsMode.Fill);
            dgvPettyCashExp.Columns["PettyCashExpID"].Visible = false;
            dgvPettyCashExp.Columns["MasterCashClosingID"].Visible = false;
            dgvPettyCashExp.Columns["StoreID"].Visible = false;
            dgvPettyCashExp.Columns["CreatedBy"].Visible = false;
        }
        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }
        private void rdByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByDate.Checked)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                cmbStore.SelectedIndex = -1;
                cmbStore.Enabled = false;

                dgvPettyCashExp.DataSource = null;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }

        private void rdByStore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdByStore.Checked)
            {
                cmbStore.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = dtDate;
                dtpToDate.Value = dtDate;

                dgvPettyCashExp.DataSource = null;
            }
            else
            {
                cmbStore.SelectedIndex = -1;
                cmbStore.Enabled = false;
            }
        }

        private void rdSearchByAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByAll.Checked)
            {
                cmbStore.Enabled = false;
                cmbStore.SelectedIndex = -1;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                dtpFromDate.Value = dtDate;
                dtpToDate.Value = dtDate;

                LoadData();
            }
        }

        private void cmbStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByStore();
        }

        private void SearchByStore()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, DBNull.Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, cmbStore.SelectedValue, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PettyCashExpAmt");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ds.Tables.Count > 1)
                {
                    DataTable dtPettyCashBal = ds.Tables[1];
                    if (ObjUtil.ValidateTable(dtPettyCashBal))
                    {
                        txtTotalPettyCash.Text = dtPettyCashBal.Rows[0]["TotalPettyCashAmt"].ToString();
                        txtTotalExpenses.Text = dtPettyCashBal.Rows[0]["TotalPettyCashExpAmt"].ToString();
                        txtPettyCashBAL.Text = dtPettyCashBal.Rows[0]["PettyCashBalance"].ToString();
                    }
                }
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPettyCashExp.DataSource = dt;
                }
                else
                {
                    dgvPettyCashExp.DataSource = null;
                }
            }
        }

        private void SearchByDate()
        {
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PettyCashExpAmt");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ds.Tables.Count > 1)
                {
                    DataTable dtPettyCashBal = ds.Tables[1];
                    if (ObjUtil.ValidateTable(dtPettyCashBal))
                    {
                        txtTotalPettyCash.Text = dtPettyCashBal.Rows[0]["TotalPettyCashAmt"].ToString();
                        txtTotalExpenses.Text = dtPettyCashBal.Rows[0]["TotalPettyCashExpAmt"].ToString();
                        txtPettyCashBAL.Text = dtPettyCashBal.Rows[0]["PettyCashBalance"].ToString();
                    }
                }
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPettyCashExp.DataSource = dt;
                }
                else
                {
                    dgvPettyCashExp.DataSource = null;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strFilterBY = "";
            string strFilterCondition = "";

            if (rdByDate.Checked)
            {
                strFilterBY = "Date";
                strFilterCondition = "From Date : " + dtpFromDate.Value.ToShortDateString() + " To Date : " + dtpToDate.Value.ToShortDateString();
            }
            else if (rdByStore.Checked)
            {
                strFilterBY = "Store/Shop";
                strFilterCondition = cmbStore.Text;

            }
            else if (rdSearchByAll.Checked)
            {

                strFilterBY = "All";
                strFilterCondition = "All";
            }

            Report.Report_Forms.frmPettyCashReport frmPettyCashReport = new Report_Forms.frmPettyCashReport();

            frmPettyCashReport.strCondtion = strFilterCondition;
            frmPettyCashReport.strFilterBy = strFilterBY;
            frmPettyCashReport.dtPettyCashDetails =(DataTable) dgvPettyCashExp.DataSource;

            frmPettyCashReport.strPettyCashBalance = txtPettyCashBAL.Text;
            frmPettyCashReport.strTotalPettyCashAmt = txtTotalPettyCash.Text;
            frmPettyCashReport.strToTotalPettyCashExpAmt = txtTotalExpenses.Text;

            frmPettyCashReport.Show();
        }
    }
}