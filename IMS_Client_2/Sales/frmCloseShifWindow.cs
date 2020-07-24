using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CoreApp;
using Button = System.Windows.Forms.Button;
using Image = System.Drawing.Image;

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
        decimal ExpensesAmt = 0, PettyCashBAL = 0;
        object Cashtotal = 0;

        DataTable dtCash = new DataTable();
        DataTable dtCredit = new DataTable();
        DataTable dtExpenses = new DataTable();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            Cashtotal = 0;
            txtTotalValue.Clear();
            dtCash = null;
            dtExpenses = null;
        }

        private void LoadData()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("MasterCashClosingID", SqlDbType.Int, pMasterCashClosingID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("PettyCashBAL", SqlDbType.Decimal, lblPettyCashBAL.Text, clsConnection_DAL.ParamType.Output);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashClosing_Details");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dtCredit = ds.Tables[1];
                    dtExpenses = ds.Tables[2];

                    DataTable dtoutput = ObjDAL.GetOutputParmData();
                    if (ObjUtil.ValidateTable(dtoutput))
                    {
                        PettyCashBAL = Convert.ToDecimal(dtoutput.Rows[0][1]);
                        lblPettyCashBAL.Text = PettyCashBAL.ToString();
                    }

                    if (ObjUtil.ValidateTable(dt))
                    {
                        dgvCloseCash.DataSource = dt;
                        dtCash = dt;
                        if (Convert.ToInt32(dt.Rows[0]["CashStatus"]) == 0)
                        {
                            dgvCloseCash.ReadOnly = false;
                            btnOpenCash.Enabled = false;
                            btnCloseCash.Enabled = true;
                            btnPreview.Enabled = false;
                            btnPrint.Enabled = false;

                            //btnPreview.Enabled = true;
                            //btnPrint.Enabled = true;
                            dgvCloseCash.DataSource = dtCash;
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
                        dtCash = null;
                        dtExpenses = null;
                        btnOpenCash.Enabled = true;
                        btnCloseCash.Enabled = false;
                        btnPreview.Enabled = false;
                        btnPrint.Enabled = false;
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    dtCash = null;
                    dtExpenses = null;
                    dgvCloseCash.DataSource = null;
                    btnOpenCash.Enabled = false;
                    btnCloseCash.Enabled = false;
                    btnPreview.Enabled = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void frmCloseShifWindow_Load(object sender, EventArgs e)
        {
            btnOpenCash.BackgroundImage = B_Leave;
            btnCloseCash.BackgroundImage = B_Leave;
            btnPreview.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;

            pStoreID = frmHome.Home_StoreID;
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
            ObjUtil.SetDataGridProperty(dgvCloseCash, DataGridViewAutoSizeColumnsMode.Fill, Color.White);
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

                CalcTotalCashBand();

                dgvCloseCash.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                dgvCloseCash.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            }
            else if (listView1.Items[1].Selected)
            {
                dgvCloseCash.Columns["CreditClosingID"].Visible = false;
                dgvCloseCash.Columns["MasterCashClosingID"].Visible = false;
            }
            else if (listView1.Items[2].Selected)
            {
                if (dgvCloseCash.Columns.Contains("PettyCashExpID"))
                {
                    dgvCloseCash.Columns["PettyCashExpID"].Visible = false;
                }
                if (dgvCloseCash.Columns.Contains("MasterCashClosingID"))
                {
                    dgvCloseCash.Columns["MasterCashClosingID"].Visible = false;
                }
                CalcTotalPettyCashExp();

                dgvCloseCash.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
                dgvCloseCash.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            }
            dgvCloseCash.ClearSelection();
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
            int pStoreID = frmHome.Home_StoreID;
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
                ObjDAL.UpdateColumnData("TotalCashValue", SqlDbType.Decimal, Cashtotal);
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

        private void InsertPettyCashExpenses()
        {
            bool b = false;
            if (ObjUtil.ValidateTable(dtExpenses))
            {
                for (int i = 0; i < dtExpenses.Rows.Count; i++)
                {
                    ObjDAL.SetStoreProcedureData("PettyCashExpAmt", SqlDbType.Decimal, dtExpenses.Rows[i]["ExpensesAmt"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("Particulars", SqlDbType.NVarChar, dtExpenses.Rows[i]["Particulars"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("MasterCashClosingID", SqlDbType.Int, dtExpenses.Rows[i]["MasterCashClosingID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);

                    b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".[dbo].SPR_Insert_PettyCashExpAmt");
                }
                if (b)
                {
                    clsUtility.ShowInfoMessage("Petty Cash Book has been Saved !", clsUtility.strProjectTitle);
                }
            }
        }

        private bool ValidatePettyCashExpAmt()
        {
            if (PettyCashBAL >= ExpensesAmt)
            {
                for (int i = 0; i < dtExpenses.Rows.Count; i++)
                {
                    if (dtExpenses.Rows[i]["MasterCashClosingID"] == DBNull.Value || dtExpenses.Rows[i]["MasterCashClosingID"].ToString() == "" || Convert.ToDecimal(dtExpenses.Rows[i]["ExpensesAmt"]) == 0)
                    {
                        dtExpenses.Rows[i].Delete();
                        dtExpenses.AcceptChanges();
                        return true;
                    }
                    else
                    {
                        if (dtExpenses.Rows[i]["Particulars"] == DBNull.Value || dtExpenses.Rows[i]["Particulars"].ToString() == "" || dtExpenses.Rows[i]["ExpensesAmt"] == DBNull.Value)
                        {
                            clsUtility.ShowInfoMessage("Enter valid entry for Expenses", clsUtility.strProjectTitle);
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have added Extra " + (PettyCashBAL - ExpensesAmt) + " Expenses..", clsUtility.strProjectTitle);
                return false;
            }
        }

        private void btnCloseCash_Click(object sender, EventArgs e)
        {
            if (listView1.Items[0].Selected || listView1.Items[2].Selected)
            {
                //DataTable dt = (DataTable)dgvCloseCash.DataSource;
                DataTable dt = dtCash;
                double total = Convert.ToDouble(Cashtotal);
                if (ObjUtil.ValidateTable(dt) && total > 0)
                {
                    if (ValidatePettyCashExpAmt())
                    {
                        InsertCloseCashBand(dt);
                        InsertPettyCashExpenses();
                        ClearAll();
                        listView1.Items[0].Selected = true;
                        //LoadData();
                    }
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
            if (listView1.Items[0].Selected)
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
            else if (listView1.Items[2].Selected)
            {
                //if (e.ColumnIndex == 2 && e.FormattedValue.ToString() == "")
                //{
                //    e.Cancel = false;
                //    if (dgvCloseCash.Rows[e.RowIndex].IsNewRow) { return; }
                //    if (e.FormattedValue.ToString() == "")
                //    {
                //        e.Cancel = true;
                //        clsUtility.ShowInfoMessage("Enter Particulars for Expenses..", clsUtility.strProjectTitle);
                //    }
                //}
                if (e.ColumnIndex == 3 && e.FormattedValue.ToString() != "")
                {
                    if (dgvCloseCash.Rows[e.RowIndex].IsNewRow) { return; }

                    decimal newDecimal = 0;
                    if (!decimal.TryParse(e.FormattedValue.ToString(),
                        out newDecimal) || newDecimal < 0)
                    {
                        e.Cancel = true;
                        clsUtility.ShowInfoMessage("Enter Only Numbers..", clsUtility.strProjectTitle);
                        //dgvQtycolor.Rows[e.RowIndex].ErrorText = "Size must be a Positive integer";
                    }
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
            //DataTable dt = (DataTable)dgvCloseCash.DataSource;
            DataTable dt = dtCash;
            if (ObjUtil.ValidateTable(dt))
            {
                //DataRow[] drow = dt.Select("SUM(Value)");
                //txtTotalValue.Text = drow[0].ToString();
                Cashtotal = dt.Compute("SUM(Value)", string.Empty);
                txtTotalValue.Text = Cashtotal.ToString();
            }
        }

        private void CalcTotalPettyCashExp()
        {
            try
            {
                DataTable dt = dtExpenses;
                if (ObjUtil.ValidateTable(dt))
                {
                    //DataRow[] drow = dt.Select("SUM(ExpensesAmt)");
                    //txtTotalValue.Text = drow[0].ToString();

                    //object total = dt.Compute("SUM(ExpensesAmt)", "ExpensesAmt IS NOT NULL");
                    if (dt.Rows[0]["ExpensesAmt"] != DBNull.Value)
                    {
                        ExpensesAmt = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x["ExpensesAmt"]));
                        txtTotalValue.Text = ExpensesAmt.ToString();
                    }
                }
            }
            catch { }
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
            else if (listView1.Items[2].Selected)
            {
                if (ObjUtil.ValidateTable(dtExpenses))
                {
                    dtExpenses.Rows[e.RowIndex]["MasterCashClosingID"] = pMasterCashClosingID;
                    dtExpenses.Rows[e.RowIndex]["PettyCashExpID"] = 0;
                    if (e.RowIndex + 1 == dgvCloseCash.Rows.Count)
                    {
                        DataRow Row = dtExpenses.NewRow();
                        Row["ExpensesAmt"] = 0;
                        dtExpenses.Rows.Add(Row);
                    }
                    dtExpenses.AcceptChanges();
                    dgvCloseCash.DataSource = dtExpenses;
                }
                CalcTotalPettyCashExp();
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
            else if (listView1.Items[2].Selected)
            {
                if (dgvCloseCash.Columns.Contains("Particulars"))
                {
                    dgvCloseCash.Columns["Particulars"].ReadOnly = false;
                }
                if (dgvCloseCash.Columns.Contains("ExpensesAmt"))
                {
                    dgvCloseCash.Columns["ExpensesAmt"].ReadOnly = false;
                }
            }
            dgvCloseCash.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items[0].Selected)
                {
                    if (!ObjUtil.ValidateTable(dtCash))
                    {
                        LoadData();
                    }
                    else
                    {
                        dgvCloseCash.DataSource = dtCash;
                    }
                }
                else if (listView1.Items[1].Selected)
                {
                    //dtCash = (DataTable)dgvCloseCash.DataSource;
                    txtTotalValue.Clear();
                    dgvCloseCash.DataSource = dtCredit;
                }
                else if (listView1.Items[2].Selected)
                {
                    //dtCash = (DataTable)dgvCloseCash.DataSource;
                    txtTotalValue.Clear();
                    dgvCloseCash.DataSource = dtExpenses;
                }
            }
            catch (Exception ex)
            {
                LoadData();
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void PrintReport(bool Direct)
        {
            ObjDAL.SetStoreProcedureData("MasterCashClosingID", SqlDbType.Int, pMasterCashClosingID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("PettyCashBAL", SqlDbType.Decimal, lblPettyCashBAL.Text, clsConnection_DAL.ParamType.Output);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashClosing_Details");

            DataTable dtCashDetails = ds.Tables[0];
            DataTable dtCredit = ds.Tables[1];
            string TotalExpenses = "0.00";
            try
            {
                if (ObjUtil.ValidateTable(dtExpenses))
                {
                    TotalExpenses = Convert.ToString(dtExpenses.Compute("Sum(ExpensesAmt)", string.Empty));
                }
            }
            catch (Exception ex)
            {
            }

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
            frmClosingCashReport.PettyCash = lblPettyCashBAL.Text;

            frmClosingCashReport.PettyCashBeforeExp = Convert.ToDecimal(lblPettyCashBAL.Text + TotalExpenses).ToString();

            frmClosingCashReport.dtExpanses = this.dtExpenses;
            frmClosingCashReport.TotalExpenses = TotalExpenses;

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