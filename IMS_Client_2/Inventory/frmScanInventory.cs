using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IMS_Client_2.Inventory
{
    public partial class frmScanInventory : Form
    {
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        DataTable dtItemDetails = new DataTable();
        DataTable dtItemDetails_default = new DataTable();

        int pMasterScanID = 0;
        int OldTotalQTY = 0;
        int SelectedStoreID = 0;

        Thread thread;

        public frmScanInventory()
        {
            InitializeComponent();
        }
        private void LoadFromStore()
        {
            string strQ = "";
            if (clsUtility.IsAdmin)
            {
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK) WHERE ISNULL(ActiveStatus,1)=1 ORDER BY StoreName ASC";
                cmdFrom.Enabled = true;
            }
            else
            {
                cmdFrom.Enabled = false;
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK) WHERE ISNULL(ActiveStatus,1)=1 AND StoreID IN  " +
                           " (SELECT StoreID FROM  " + clsUtility.DBName + ".dbo.tblStoreUserRights WHERE UserID = " + clsUtility.LoginID + ") ORDER BY StoreName ASC";
            }
            DataTable dtFromStore = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtFromStore))
            {
                cmdFrom.DataSource = dtFromStore;
                cmdFrom.DisplayMember = "StoreName";
                cmdFrom.ValueMember = "StoreID";
            }
        }
        private void txtBarCode_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtBarCode_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, System.Drawing.Color.White);
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
        public delegate void InvocationDelegate();

        public void DoGuiStuff()
        {
            if (dgvProductDetails.InvokeRequired)
            {
                dgvProductDetails.Invoke(new InvocationDelegate(DoGuiStuff));
                return;
            }

            //GUI manipulation here
            dgvProductDetails.DataSource = dtItemDetails;
            dgvProductDetails.Refresh();
            CalculateGrandTotal();
            pnlLoading.Visible = false;
            progressBar1.Value = 0;
            this.Focus();
        }
        public delegate void InvocationDelegate2(int a, int b);

        public void ReportProgress(int value, int MaxValue)
        {
            try
            {
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.Invoke(new InvocationDelegate2(ReportProgress), value, MaxValue);
                    return;
                }
                progressBar1.Maximum = MaxValue;
                //GUI manipulation hereg
                progressBar1.Value = value;

                lblInfo.Text = "Loading Data Please wait.... [ " + value.ToString() + "/" + MaxValue.ToString() + "]";
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception)
            {
            }
        }


        private void BindScannedItems()
        {
            try
            {
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, SelectedStoreID, clsConnection_DAL.ParamType.Input);

                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryItemDetails");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dtOldBill = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dtOldBill))
                    {
                        pMasterScanID = Convert.ToInt32(dtOldBill.Rows[0]["MasterScanID"]);
                        OldTotalQTY = Convert.ToInt32(dtOldBill.Compute("SUM(BillQTY)", string.Empty));

                        dtItemDetails.Clear();
                        for (int i = 0; i < dtOldBill.Rows.Count; i++)
                        {
                            string pID = dtOldBill.Rows[i]["ProductID"].ToString();
                            string name = dtOldBill.Rows[i]["ProductName"].ToString();
                            string rate = dtOldBill.Rows[i]["Rate"].ToString();
                            string barCode = dtOldBill.Rows[i]["Barcode"].ToString();
                            string qty = dtOldBill.Rows[i]["BillQTY"].ToString();
                            string SizeID = dtOldBill.Rows[i]["SizeID"].ToString();
                            string Size = dtOldBill.Rows[i]["Size"].ToString();
                            string ColorID = dtOldBill.Rows[i]["ColorID"].ToString();
                            string ColorName = dtOldBill.Rows[i]["ColorName"].ToString();
                            string total = dtOldBill.Rows[i]["Total"].ToString();
                            string SubProductID = dtOldBill.Rows[i]["SubProductID"].ToString();

                            ReportProgress(i, dtOldBill.Rows.Count);

                            System.Diagnostics.Debug.WriteLine(" i : " + i.ToString());
                            AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, SubProductID);
                        }
                    }
                    else
                    {
                        pMasterScanID = 0;
                        OldTotalQTY = 0;
                        dtItemDetails.Clear();
                        //AddRowToItemDetails("0", "NA", "0", "0", "0", "NA", "0", "0", "", "NA", "0");

                        dgvProductDetails.DataSource = dtItemDetails_default;
                    }
                }
                DoGuiStuff();
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void frmScanInventory_Load(object sender, EventArgs e)
        {
            btncancel.BackgroundImage = B_Leave;
            btnSaveData.BackgroundImage = B_Leave;

            LoadFromStore();
            InitItemTable();

            txtBarCode.Focus();
            pnlLoading.Visible = true;

            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            SelectedStoreID = Convert.ToInt32(cmdFrom.SelectedValue);
            thread = new Thread(new ThreadStart(BindScannedItems));
            thread.Start();
        }
        private Image GetProductPhoto(int SubProductID)
        {
            Image imgProduct = null;

            ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, SubProductID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ProductPhoto");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (Convert.ToInt32(dt.Rows[0]["Flag"]) == 1)
                    {
                        string img = dt.Rows[0]["ImgName"].ToString();
                        if (System.IO.File.Exists(img))
                        {
                            imgProduct = Image.FromFile(img);
                        }
                        else
                        {
                            imgProduct = IMS_Client_2.Properties.Resources.NoImage;
                        }
                    }
                    else
                    {
                        imgProduct = IMS_Client_2.Properties.Resources.NoImage;
                        //clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                    }
                }
            }
            return imgProduct;
        }
        private bool IsItemExist(string barCode)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            if (dRow.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void GetItemDetailsByProductID(string _BarCodeValue)
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.GetProductDetailsByBarCode " + cmdFrom.SelectedValue + ", " + _BarCodeValue + ",0");
            if (ObjUtil.ValidateTable(dt))
            {
                string pID = dt.Rows[0]["ProductID"].ToString();
                string name = dt.Rows[0]["ProductName"].ToString();
                string rate = dt.Rows[0]["Rate"].ToString();
                string barCode = dt.Rows[0]["BarcodeNo"].ToString();
                string stockQTY = dt.Rows[0]["QTY"].ToString();

                string qty = "1";
                string SizeID = dt.Rows[0]["SizeID"].ToString();
                string Size = dt.Rows[0]["Size"].ToString();
                string ColorID = dt.Rows[0]["ColorID"].ToString();
                string ColorName = dt.Rows[0]["ColorName"].ToString();
                decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);
                string SubProductID = dt.Rows[0]["SubProductID"].ToString();

                // if Item already there in the grid, then just increase the QTY
                if (IsItemExist(barCode))
                {
                    UpdateQTYByOne(barCode, Convert.ToDecimal(rate));
                    picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));
                }
                else
                {
                    AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, SubProductID);
                    picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));
                }

                CalculateGrandTotal();
                //dgvProductDetails.ClearSelection();
                txtBarCode.Clear();
                txtBarCode.Focus();
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue, clsUtility.strProjectTitle);
            }
        }
        private void CalculateGrandTotal()
        {
            decimal NewBillAmount = 0.0M;
            int TotalQTY = 0;
            try
            {
                //for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                //{
                //    NewBillAmount += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["Total"].Value);
                //    TotalQTY += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["BillQTY"].Value);
                //}

                DataTable dt = (DataTable)dgvProductDetails.DataSource;
                if (ObjUtil.ValidateTable(dt))
                {
                    NewBillAmount = Convert.ToDecimal(dt.Compute("SUM(Total)", string.Empty));
                    TotalQTY = Convert.ToInt32(dt.Compute("SUM(BillQTY)", string.Empty));
                }
                txtTotalQTY.Text = TotalQTY.ToString();
                txtValue.Text = NewBillAmount.ToString();
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }
        private void UpdateQTYByOne(string barCode, decimal rate, int qty = 1)
        {
            try
            {
                //if (dtItemDetails.Rows.Count > 0)
                if (ObjUtil.ValidateTable(dtItemDetails))
                {
                    DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
                    if (dRow != null && dRow.Length > 0)
                    {
                        // add one qty
                        int NewQTY = 0;
                        //int NewQTY = Convert.ToInt32(dRow[0]["BillQTY"]) + qty;
                        if (qty == 1)
                        {
                            NewQTY = Convert.ToInt32(dRow[0]["BillQTY"]) + qty;
                        }
                        else
                        {
                            qty = qty == 0 ? 1 : qty;
                            NewQTY = qty;
                        }
                        // set to col
                        dRow[0]["BillQTY"] = NewQTY.ToString();

                        // cal total
                        decimal total = rate * NewQTY;
                        // set the total
                        dRow[0]["Total"] = total.ToString();
                    }
                    dtItemDetails.AcceptChanges();
                    dgvProductDetails.DataSource = dtItemDetails;
                }
            }
            catch (Exception)
            {
            }
        }
        private void InitItemTable()
        {
            dtItemDetails.Columns.Add("ProductID");
            dtItemDetails.Columns.Add("ProductName");
            dtItemDetails.Columns.Add("BarcodeNo");
            dtItemDetails.Columns.Add("Rate");
            dtItemDetails.Columns.Add("BillQTY", typeof(int));
            dtItemDetails.Columns.Add("ColorID");
            dtItemDetails.Columns.Add("Color");
            dtItemDetails.Columns.Add("SizeID");
            dtItemDetails.Columns.Add("Size");
            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");
            dtItemDetails.Columns.Add("Total", typeof(decimal));
            dtItemDetails.Columns.Add("Delete");
            dtItemDetails.Columns.Add("SubProductID");

            dtItemDetails_default = dtItemDetails.Copy();// Added for showing default structure
        }

        private void AddRowToItemDetails(string productID, string name, string qty, string rate, string total,
       string BarCode, string SizeID, string Size, string ColorID, string Color, string SubProductID)
        {
            DataRow dRow = dtItemDetails.NewRow();
            dRow["ProductID"] = productID;
            dRow["ProductName"] = name;
            dRow["BillQTY"] = qty;
            dRow["Rate"] = rate;
            dRow["Total"] = total;
            dRow["Delete"] = "Delete";
            dRow["ColorID"] = ColorID;
            dRow["Color"] = Color;
            dRow["SizeID"] = SizeID;
            dRow["Size"] = Size;
            dRow["BarcodeNo"] = BarCode;
            dRow["SubProductID"] = SubProductID;

            dtItemDetails.Rows.Add(dRow);
            dtItemDetails.AcceptChanges();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmdFrom.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select from Store.", clsUtility.strProjectTitle);
                return;
            }
            if (txtBarCode.Text.Trim().Length != 0)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GetItemDetailsByProductID(txtBarCode.Text);
                }
            }
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                dgvProductDetails.ClearSelection();
                ObjUtil.SetRowNumber(dgvProductDetails);

                dgvProductDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dgvProductDetails.Columns.Contains("MasterScanID"))
                {
                    dgvProductDetails.Columns["MasterScanID"].Visible = false;
                }
                dgvProductDetails.Columns["ProductID"].Visible = false;
                dgvProductDetails.Columns["ColoriD"].Visible = false;
                dgvProductDetails.Columns["SizeiD"].Visible = false;
                if (dgvProductDetails.Columns["OIRate"] != null)
                {
                    dgvProductDetails.Columns["OIRate"].Visible = false;
                }
                if (dgvProductDetails.Columns["Adj_Amount"] != null)
                {
                    dgvProductDetails.Columns["Adj_Amount"].Visible = false;
                }
                if (dgvProductDetails.Columns["SubProductID"] != null)
                {
                    dgvProductDetails.Columns["SubProductID"].Visible = false;
                }

                if (!ObjUtil.IsControlTextEmpty(txtBarCode))
                {
                    foreach (DataGridViewRow row in dgvProductDetails.Rows)
                    {
                        if (row.Cells["BarcodeNo"].Value.ToString() == "" + txtBarCode.Text.Trim() + "")
                        {
                            dgvProductDetails.Rows[row.Index].Selected = true;
                            dgvProductDetails.FirstDisplayedScrollingRowIndex = row.Index;

                            break;
                        }
                    }
                }
                ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, System.Drawing.Color.White);
            }
            catch (NullReferenceException ex)
            {
            }
            catch (Exception)
            {
            }
        }

        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "ColDelete")
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete ? ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    dgvProductDetails.Rows.RemoveAt(e.RowIndex);
                    dgvProductDetails.EndEdit();
                    CalculateGrandTotal();
                    return;
                }
            }
            if (ObjUtil.ValidateTable((DataTable)dgvProductDetails.DataSource))
            {
                int _SUBPID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["SubProductID"].Value);
                picProduct.Image = GetProductPhoto(Convert.ToInt32(_SUBPID));
            }
        }

        private void ClearAll()
        {
            dtItemDetails.Clear();
            txtBarCode.Clear();

            picProduct.Image = null;
            txtValue.Clear();
            txtTotalQTY.Clear();
            cmdFrom.SelectedIndex = -1;
            pMasterScanID = 0;
            OldTotalQTY = 0;
        }
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmScanInventory, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (dtItemDetails.Rows.Count > 0)
                {
                    int NewQTY = Convert.ToInt32(txtTotalQTY.Text) - OldTotalQTY;
                    if (NewQTY > 0)
                    {
                        Sales.frmQTYValidation.QTYConfirmation = 0;
                        Sales.frmQTYValidation frmQTYValidation = new Sales.frmQTYValidation();
                        frmQTYValidation.TotalQTY = Math.Abs(NewQTY);
                        frmQTYValidation.ShowDialog();

                        if (Sales.frmQTYValidation.QTYConfirmation == -1)
                        {
                            // Exit the bill -Dead bill
                            this.Close();
                        }
                        else if (Sales.frmQTYValidation.QTYConfirmation == 1)
                        {
                            if (pMasterScanID == 0)
                            {
                                SaveScanInventory();
                            }
                            else
                            {
                                UpdateScanInventory();
                            }
                        }
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("There is no Scan item detected.");
                        txtBarCode.Focus();
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("Please Scan items before you save them.");
                    txtBarCode.Focus();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void SaveScanInventory()
        {
            ObjDAL.SetColumnData("StoreID", SqlDbType.Int, cmdFrom.SelectedValue);
            ObjDAL.SetColumnData("CompareStatus", SqlDbType.Bit, false);
            ObjDAL.SetColumnData("ScanDate", SqlDbType.Date, dtpSalesDate.Value.ToString("yyyy-MM-dd"));
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

            int ScanInventoryID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblScanInventoryDetails", true);

            if (ScanInventoryID > 0)
            {
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    string Total = dgvProductDetails.Rows[i].Cells["Total"].Value.ToString();
                    string ProductID = dgvProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                    string QTY = dgvProductDetails.Rows[i].Cells["BillQTY"].Value.ToString();
                    string Rate = dgvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                    string ColorID = dgvProductDetails.Rows[i].Cells["ColorID"].Value.ToString();
                    string SizeID = dgvProductDetails.Rows[i].Cells["SizeID"].Value.ToString();
                    string BarcodeNo = dgvProductDetails.Rows[i].Cells["BarcodeNo"].Value.ToString();
                    string SubProductID = dgvProductDetails.Rows[i].Cells["SubProductID"].Value.ToString();

                    ObjDAL.SetColumnData("MasterScanID", SqlDbType.Int, ScanInventoryID);
                    ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                    ObjDAL.SetColumnData("Barcode", SqlDbType.BigInt, BarcodeNo);
                    ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);
                    ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                    ObjDAL.SetColumnData("BillQTY", SqlDbType.Int, QTY);
                    ObjDAL.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                    ObjDAL.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                    ObjDAL.SetColumnData("Total", SqlDbType.Decimal, Total);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblScanInventoryItemDetails", false);
                }
                ObjDAL.ResetData();
                clsUtility.ShowInfoMessage("All Scanned Items have been Saved.", clsUtility.strProjectTitle);
                ClearAll();
            }
            else
            {
                ObjDAL.DeleteData(clsUtility.DBName + ".dbo.tblScanInventoryDetails", "MasterScanID=" + ScanInventoryID);
            }
        }

        private void UpdateScanInventory()
        {
            if (pMasterScanID > 0)
            {
                ObjDAL.DeleteData(clsUtility.DBName + ".dbo.tblScanInventoryItemDetails", "MasterScanID=" + pMasterScanID);
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    string Total = dgvProductDetails.Rows[i].Cells["Total"].Value.ToString();
                    string ProductID = dgvProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                    string QTY = dgvProductDetails.Rows[i].Cells["BillQTY"].Value.ToString();
                    string Rate = dgvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                    string ColorID = dgvProductDetails.Rows[i].Cells["ColorID"].Value.ToString();
                    string SizeID = dgvProductDetails.Rows[i].Cells["SizeID"].Value.ToString();
                    string BarcodeNo = dgvProductDetails.Rows[i].Cells["BarcodeNo"].Value.ToString();
                    string SubProductID = dgvProductDetails.Rows[i].Cells["SubProductID"].Value.ToString();

                    ObjDAL.SetColumnData("MasterScanID", SqlDbType.Int, pMasterScanID);
                    ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                    ObjDAL.SetColumnData("Barcode", SqlDbType.BigInt, BarcodeNo);
                    ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);
                    ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                    ObjDAL.SetColumnData("BillQTY", SqlDbType.Int, QTY);
                    ObjDAL.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                    ObjDAL.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                    ObjDAL.SetColumnData("Total", SqlDbType.Decimal, Total);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblScanInventoryItemDetails", false);
                }
                ObjDAL.ResetData();
                clsUtility.ShowInfoMessage("All Scanned Items have been Updated.", clsUtility.strProjectTitle);
                ClearAll();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to clear?", clsUtility.strProjectTitle);
            if (result)
            {
                ClearAll();
            }
        }

        private void cmdFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pnlLoading.Visible = true;
            SelectedStoreID = Convert.ToInt32(cmdFrom.SelectedValue);

            dgvProductDetails.DataSource = dtItemDetails_default;

            thread = new Thread(new ThreadStart(BindScannedItems));
            thread.Start();
        }

        private void frmScanInventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                try
                {
                    thread.Abort();
                }
                catch
                {
                }
            }
            GC.Collect();
        }

        private void dgvProductDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled && e.KeyChar != 13)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...");
                txtBarCode.SelectionStart = txtBarCode.MaxLength;
            }
        }

        private void dgvProductDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            e.Cancel = false;
            int column = dgvProductDetails.CurrentCell.ColumnIndex;
            string headerText = dgvProductDetails.Columns[column].HeaderText;

            if (headerText == "QTY")
            {
                if (e.FormattedValue == DBNull.Value || e.FormattedValue.ToString() == "")
                {
                    clsUtility.ShowInfoMessage("Enter QTY..");
                    e.Cancel = true;
                }
                else if (Convert.ToInt32(e.FormattedValue) == 0)
                {
                    clsUtility.ShowInfoMessage("Enter Valid QTY..");
                    e.Cancel = true;
                }
                return;
            }
        }

        private void dgvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int column = dgvProductDetails.CurrentCell.ColumnIndex;
            string headerText = dgvProductDetails.Columns[column].HeaderText;

            if (headerText == "QTY")
            {
                e.Control.KeyPress += Int_Control_KeyPress;
            }
        }

        private void Int_Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string k = e.KeyChar.ToString();
            //TextBox txt = (TextBox)sender;
            e.Handled = ObjUtil.IsNumeric(e);
        }

        private void dgvProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string strBarCodeValue = dgvProductDetails.Rows[e.RowIndex].Cells["BarcodeNo"].Value.ToString() == "" ? "0" : dgvProductDetails.Rows[e.RowIndex].Cells["BarcodeNo"].Value.ToString();

                decimal rate = dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value.ToString() == "" ? 0 : Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);

                int newQTY = dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value.ToString() == "" ? 0 : Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value);

                newQTY = newQTY == 1 ? 0 : newQTY;
                UpdateQTYByOne(strBarCodeValue, rate, newQTY);
                CalculateGrandTotal();
            }
        }

        private void dgvProductDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvProductDetails.Columns[e.Column.Index].ReadOnly = true;
            if (clsUtility.IsAdmin)
            {
                if (dgvProductDetails.Columns.Contains("BillQTY"))
                {
                    dgvProductDetails.Columns["BillQTY"].ReadOnly = false;
                }
            }
        }
    }
}