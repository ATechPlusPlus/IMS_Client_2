using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Sales
{
    public partial class frmCloseShifWindow : Form
    {
        public frmCloseShifWindow()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        public int pMasterCashClosingID = 0;
        int pStoreID = 0;

        DataTable dtCredit = new DataTable();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            txtTotalValue.Clear();
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("MasterCashClosingID", SqlDbType.Int, pMasterCashClosingID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashClosing_Details");
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dtCredit = ds.Tables[1];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvCloseCash.DataSource = dt;
                    if (Convert.ToInt32(dt.Rows[0]["CashStatus"]) == 0)
                    {
                        dgvCloseCash.ReadOnly = false;
                        btnOpenCash.Enabled = false;
                        btnCloseCash.Enabled = true;
                        btnPreview.Enabled = false;
                        btnPrint.Enabled = false;

                        //btnPreview.Enabled = true;
                        //btnPrint.Enabled = true;
                    }
                    else
                    {
                        if (DateTime.Now.ToString("yyyy-MM-dd") == Convert.ToDateTime(dt.Rows[0]["CashBoxDate"]).ToString("yyyy-MM-dd"))
                        {
                            dgvCloseCash.ReadOnly = true;
                            btnOpenCash.Enabled = false;
                            btnCloseCash.Enabled = false;
                            btnPreview.Enabled = true;
                            btnPrint.Enabled = true;
                        }
                        else
                        {
                            dgvCloseCash.ReadOnly = true;
                            btnOpenCash.Enabled = true;
                            btnCloseCash.Enabled = false;
                            btnPreview.Enabled = true;
                            btnPrint.Enabled = true;

                            //btnPreview.Enabled = false;
                            //btnPrint.Enabled = false;
                        }
                    }
                    txtCashNo.Text = dt.Rows[0]["CashNo"].ToString();
                    txtCashierName.Text = dt.Rows[0]["Name"].ToString();
                    cmbShop.SelectedValue = dt.Rows[0]["StoreID"];
                    lblReturnedAmount.Text = dt.Rows[0]["CashReturn"].ToString();
                    txtTotalValue.Text = dt.Rows[0]["TotalCashValue"].ToString();
                }
                else
                {
                    dgvCloseCash.DataSource = null;
                    btnOpenCash.Enabled = true;
                    btnCloseCash.Enabled = false;
                    btnPreview.Enabled = false;
                    btnPreview.Enabled = false;
                }
            }
            else
            {
                dgvCloseCash.DataSource = null;
                btnOpenCash.Enabled = false;
                btnCloseCash.Enabled = false;
                btnPreview.Enabled = false;
                btnPreview.Enabled = false;
            }
        }

        private int GetDefaultStoreID()
        {
            return ObjDAL.ExecuteScalarInt("SELECT StoreID FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
        }

        private void frmCloseShifWindow_Load(object sender, EventArgs e)
        {
            btnOpenCash.BackgroundImage = B_Leave;
            btnCloseCash.BackgroundImage = B_Leave;
            btnPreview.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;

            pStoreID = GetDefaultStoreID();
            FillStoreData();
            listView1.Items[0].Selected = true;

            //LoadData();
        }

        private void btnOpenCash_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnOpenCash_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void dgvCloseCash_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvCloseCash);

            if (listView1.Items[0].Selected)
            {
                dgvCloseCash.Columns["CashBandID"].Visible = false;
                dgvCloseCash.Columns["CashBand"].Visible = false;

                dgvCloseCash.Columns["CashBoxDate"].Visible = false;
                dgvCloseCash.Columns["CashStatus"].Visible = false;
                dgvCloseCash.Columns["StoreID"].Visible = false;
                dgvCloseCash.Columns["MasterCashClosingID"].Visible = false;
                dgvCloseCash.Columns["CashNo"].Visible = false;
                dgvCloseCash.Columns["EmployeeID"].Visible = false;
                dgvCloseCash.Columns["TotalCashValue"].Visible = false;
                dgvCloseCash.Columns["Name"].Visible = false;
                dgvCloseCash.Columns["CashReturn"].Visible = false;
            }
            else if (listView1.Items[1].Selected)
            {
                dgvCloseCash.Columns["CreditClosingID"].Visible = false;
                dgvCloseCash.Columns["MasterCashClosingID"].Visible = false;
            }
        }

        private string GetCashNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            long LastID = (long)ObjDAL.ExecuteScalar("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Seq_CashNumber");
            return LastID.ToString();
        }

        private void OpenCashBox()
        {
            string NexCashNumber = GetCashNumber();
            int pStoreID = GetDefaultStoreID();
            ObjDAL.SetColumnData("CashNo", SqlDbType.NVarChar, NexCashNumber);
            ObjDAL.SetColumnData("StoreID", SqlDbType.Int, pStoreID);
            ObjDAL.SetColumnData("CashBoxDate", SqlDbType.Date, DateTime.Now.ToString("yyyy-MM-dd"));
            ObjDAL.SetColumnData("EmployeeID", SqlDbType.Int, clsUtility.LoginID);
            ObjDAL.SetColumnData("CashStatus", SqlDbType.Bit, false);
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

            pMasterCashClosingID = ObjDAL.InsertData(clsUtility.DBName + ".[dbo].tblMasterCashClosing", true);
            if (pMasterCashClosingID > 0)
            {
                clsUtility.ShowInfoMessage("Cash Box has been Opened !", clsUtility.strProjectTitle);
                ClearAll();
                LoadData();
                btnOpenCash.Enabled = false;
            }
        }

        private void btnOpenCash_Click(object sender, EventArgs e)
        {
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to open cash box ?", clsUtility.strProjectTitle);
            if (result)
            {
                this.Cursor = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(2000);
                OpenCashBox();
                this.Cursor = Cursors.Default;
            }
        }
        private void InsertCloseCashBand(DataTable dt)
        {
            int ID = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ObjDAL.SetColumnData("MasterCashClosingID", SqlDbType.Int, dt.Rows[i]["MasterCashClosingID"]);
                ObjDAL.SetColumnData("CashBandID", SqlDbType.Int, dt.Rows[i]["CashBandID"]);
                ObjDAL.SetColumnData("Count", SqlDbType.Int, dt.Rows[i]["Count"] == DBNull.Value ? 0 : dt.Rows[i]["Count"]);
                ObjDAL.SetColumnData("Value", SqlDbType.Int, dt.Rows[i]["Value"] == DBNull.Value ? 0 : dt.Rows[i]["Value"]);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                ID = ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblCashClosing]", true);
            }
            if (ID > 0)
            {
                ObjDAL.UpdateColumnData("TotalCashValue", SqlDbType.Decimal, txtTotalValue.Text);
                ObjDAL.UpdateColumnData("CashStatus", SqlDbType.Int, 1);
                ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                int a = ObjDAL.UpdateData(clsUtility.DBName + ".[dbo].[tblMasterCashClosing]", "MasterCashClosingID=" + pMasterCashClosingID);
                if (a > 0)
                {
                    clsUtility.ShowInfoMessage("Cash Box has been Closed !", clsUtility.strProjectTitle);
                }
            }
        }
        private void btnCloseCash_Click(object sender, EventArgs e)
        {
            if (listView1.Items[0].Selected)
            {
                DataTable dt = (DataTable)dgvCloseCash.DataSource;
                double total = Convert.ToDouble(txtTotalValue.Text);
                if (ObjUtil.ValidateTable(dt) && total > 0)
                {
                    InsertCloseCashBand(dt);
                    ClearAll();
                    LoadData();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Please Enter CashBand for Closing Today's Cash.", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Please Select Cash Option from List.", clsUtility.strProjectTitle);
            }
        }

        private void dgvCloseCash_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.FormattedValue.ToString() != "")
            {
                e.Cancel = false;
                dgvCloseCash.Rows[e.RowIndex].ErrorText = "";
                int newInteger = 0;
                if (dgvCloseCash.Rows[e.RowIndex].IsNewRow) { return; }
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    clsUtility.ShowInfoMessage("Enter Only Numbers..", clsUtility.strProjectTitle);
                    //dgvQtycolor.Rows[e.RowIndex].ErrorText = "Size must be a Positive integer";
                }
            }
        }
        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            cmbShop.DataSource = dt;
            cmbShop.DisplayMember = "StoreName";
            cmbShop.ValueMember = "StoreID";
            cmbShop.SelectedIndex = -1;
        }

        private void CalcTotalCashBand()
        {
            DataTable dt = (DataTable)dgvCloseCash.DataSource;
            if (ObjUtil.ValidateTable(dt))
            {
                //DataRow[] drow = dt.Select("SUM(Value)");
                //txtTotalValue.Text = drow[0].ToString();
                object total = dt.Compute("SUM(Value)", string.Empty);
                txtTotalValue.Text = total.ToString();
            }

        }
        private void dgvCloseCash_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (listView1.Items[0].Selected)
            {
                double CashBand = dgvCloseCash.Rows[e.RowIndex].Cells["CashBand"].Value == DBNull.Value ? 0 : Convert.ToDouble(dgvCloseCash.Rows[e.RowIndex].Cells["CashBand"].Value);
                int Count = dgvCloseCash.Rows[e.RowIndex].Cells["Count"].Value == DBNull.Value ? 0 : Convert.ToInt32(dgvCloseCash.Rows[e.RowIndex].Cells["Count"].Value);
                dgvCloseCash.Rows[e.RowIndex].Cells["Value"].Value = Math.Round(CashBand * Count, 3);

                CalcTotalCashBand();
            }
        }

        private void dgvCloseCash_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvCloseCash.Columns[e.Column.Index].ReadOnly = true;
            if (listView1.Items[0].Selected)
            {
                if (dgvCloseCash.Columns.Contains("Count"))
                {
                    dgvCloseCash.Columns["Count"].ReadOnly = false;
                }
            }
            dgvCloseCash.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items[0].Selected)
            {
                LoadData();
            }
            else if (listView1.Items[1].Selected)
            {
                txtTotalValue.Clear();
                dgvCloseCash.DataSource = dtCredit;
            }
        }

        private void PrintReport(bool Direct)
        {
            ObjDAL.SetStoreProcedureData("MasterCashClosingID", SqlDbType.Int, pMasterCashClosingID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashClosing_Details");

            DataTable dtCashDetails = ds.Tables[0];
            DataTable dtCredit = ds.Tables[1];


            Report.Report_Forms.frmClosingCashReport frmClosingCashReport = new Report.Report_Forms.frmClosingCashReport();
            frmClosingCashReport.CashierNo = GetCashierNumber(dtCashDetails.Rows[0]["EmployeeID"].ToString()); ;
            frmClosingCashReport.ShopAddress = GetShopAddress();
            frmClosingCashReport.ShopName = cmbShop.Text.ToString();
            frmClosingCashReport.CashNumber = dtCashDetails.Rows[0]["CashNo"].ToString();
            frmClosingCashReport.TotalCash = dtCashDetails.Rows[0]["TotalCashValue"].ToString();
            frmClosingCashReport.TotalCredit = GetTotalCreditValue();
            frmClosingCashReport.GrandToatl = Convert.ToString(Convert.ToDecimal(frmClosingCashReport.TotalCash) + Convert.ToDecimal(frmClosingCashReport.TotalCredit));
            frmClosingCashReport.dtCashDetails = dtCashDetails;
            frmClosingCashReport.IsDirectPrint = Direct;
            frmClosingCashReport.dtCreditDetails = dtCredit;
            frmClosingCashReport.Show();
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            PrintReport(false);
        }
        private string GetCashierNumber(string empID)
        {
            object obj = ObjDAL.ExecuteScalar("SELECT EmployeeCode FROM " + clsUtility.DBName + ".dbo.EmployeeDetails WITH(NOLOCK) WHERE EmpID=" + empID);
            if (obj == null)
            {
                return "NA";
            }
            else
            {
                return obj.ToString();
            }
        }
        private string GetShopAddress()
        {
            return ObjDAL.ExecuteScalar("SELECT Place+' ,Tel :'+Tel FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK) WHERE StoreID=" + cmbShop.SelectedValue.ToString()).ToString();
        }
        private string GetTotalCreditValue()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT ISNULL(SUM(Value),0) FROM " + clsUtility.DBName + ".[dbo].[tblCreditClosing] WITH(NOLOCK) WHERE MasterCashClosingID=" + pMasterCashClosingID);
            if (ObjUtil.ValidateTable(dt))
            {
                return dt.Rows[0][0].ToString();
            }
            return "0";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport(true);
        }
    }
}