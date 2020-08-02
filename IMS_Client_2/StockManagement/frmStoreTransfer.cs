using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.StockManagement
{
    public partial class frmStoreTransfer : Form
    {
        public frmStoreTransfer()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjCon = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmStoreTransfer_Load(object sender, EventArgs e)
        {
            btncancel.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnSaveData.BackgroundImage = B_Leave;

            btnPrint.BackgroundImage= B_Leave;

            LoadFromStore();
            LoadToStore();
            BindDefaultFromStore();
            InitItemTable();
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

        private void BindDefaultFromStore()
        {
            //int deafultStoreID = ObjCon.ExecuteScalarInt("SELECT Storeid FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName = '" + Environment.MachineName + "'");
            int deafultStoreID = frmHome.Home_StoreID;
            cmdFrom.SelectedValue = deafultStoreID;
            if (deafultStoreID == 0)
            {
                clsUtility.ShowInfoMessage("Please select the default shop for this client from Setting Window.", clsUtility.strProjectTitle);
                Settings.frmOtherSetting otherSetting = new Settings.frmOtherSetting();
                otherSetting.ShowDialog();
                this.Close();
            }
        }
        private void LoadFromStore()
        {
            string strQ = "";
            if (clsUtility.IsAdmin)
            {
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WITH(NOLOCK)";
                cmdFrom.Enabled = true;
            }
            else
            {
                cmdFrom.Enabled = false;
                strQ = "SELECT StoreID,StoreName FROM " + clsUtility.DBName + ".dbo.StoreMaster WHERE StoreID in  " +
                           " (SELECT StoreID FROM  " + clsUtility.DBName + ".dbo.tblStoreUserRights WHERE UserID = " + clsUtility.LoginID + ")";
            }
            DataTable dtFromStore = ObjCon.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtFromStore))
            {
                cmdFrom.DataSource = dtFromStore;
                cmdFrom.DisplayMember = "StoreName";
                cmdFrom.ValueMember = "StoreID";
            }
        }
        private void LoadToStore()
        {
            string strQ = "";
            if (cmdFrom.SelectedValue != null)
            {

                if (clsUtility.IsAdmin)
                {
                    strQ = "SELECT StoreID,StoreName FROM StoreMaster WITH(NOLOCK) WHERE StoreID NOT IN (" + cmdFrom.SelectedValue + ")";
                }
                else
                {
                    strQ = "SELECT StoreID,StoreName FROM StoreMaster WHERE StoreID IN  " +
                              " (SELECT StoreID FROM tblStoreUserRights WHERE UserID = " + clsUtility.LoginID + " AND StoreID NOT IN (" + cmdFrom.SelectedValue + "))";
                }

                DataTable ftToStore = ObjCon.ExecuteSelectStatement(strQ);
                if (ObjUtil.ValidateTable(ftToStore))
                {
                    cmdTo.DataSource = ftToStore;
                    cmdTo.DisplayMember = "StoreName";
                    cmdTo.ValueMember = "StoreID";
                }
                cmdTo.SelectedIndex = -1;
            }

        }
        private string GenerateBillNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjCon.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Seq_StoreTransferBill");
            string InvoiceNumber = "TRANS-" + LastID;

            return InvoiceNumber;
        }
        private string TransferStock()
        {
            string _BillNumber = "";

            if (Bill_ID.Trim().Length == 0)
            {
                _BillNumber = GenerateBillNumber();

                ObjCon.SetColumnData("BillNo", SqlDbType.NVarChar, _BillNumber);
                ObjCon.SetColumnData("FromStore", SqlDbType.Int, cmdFrom.SelectedValue);
                ObjCon.SetColumnData("ToStore", SqlDbType.Int, cmdTo.SelectedValue);
                ObjCon.SetColumnData("BillStatus", SqlDbType.NVarChar, lblBillStatus.Text);
                ObjCon.SetColumnData("TotalQTY", SqlDbType.Int, txtTotalQTY.Text);
                ObjCon.SetColumnData("BillDate", SqlDbType.Date, dtpSalesDate.Value.ToString("yyyy-MM-dd"));
                ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                int TransferID = ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreTransferBillDetails", true);

                if (TransferID > 0)
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

                        ObjCon.SetColumnData("StoreBillDetailsID", SqlDbType.Int, TransferID);
                        
                        ObjCon.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                        ObjCon.SetColumnData("Barcode", SqlDbType.NVarChar, BarcodeNo);
                        ObjCon.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);
                        ObjCon.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                        ObjCon.SetColumnData("BillQTY", SqlDbType.Int, QTY);
                        ObjCon.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                        ObjCon.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                        ObjCon.SetColumnData("Total", SqlDbType.Decimal, Total);
                        ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                        
                        ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreTransferItemDetails", false);
                    }
                    clsUtility.ShowInfoMessage("Item has been transferd to selected store.", clsUtility.strProjectTitle);
                  
                }
            }
            else
            {
                _BillNumber = txtInvoiceNumber.Text;
                ObjCon.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                ObjCon.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjCon.UpdateData(clsUtility.DBName + ".dbo.tblStoreTransferBillDetails", "StoreTransferID='" + Bill_ID + "'");

                ObjCon.ExecuteNonQuery("Delete " + clsUtility.DBName + ".dbo.tblStoreTransferItemDetails WHERE StoreBillDetailsID=" + Bill_ID);
                int pBillQTY = 0;
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    string Total = dgvProductDetails.Rows[i].Cells["Total"].Value.ToString();
                    string ProductID = dgvProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                    
                    string QTY = dgvProductDetails.Rows[i].Cells["BillQTY"].Value.ToString();
                    pBillQTY += Convert.ToInt32(QTY);

                    string Rate = dgvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                    string ColorID = dgvProductDetails.Rows[i].Cells["ColorID"].Value.ToString();
                    string SizeID = dgvProductDetails.Rows[i].Cells["SizeID"].Value.ToString();
                    string BarcodeNo = dgvProductDetails.Rows[i].Cells["BarcodeNo"].Value.ToString();
                    string SubProductID = dgvProductDetails.Rows[i].Cells["SubProductID"].Value.ToString();
                    ObjCon.SetColumnData("StoreBillDetailsID", SqlDbType.Int, Bill_ID);

                    ObjCon.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                    ObjCon.SetColumnData("Barcode", SqlDbType.NVarChar, BarcodeNo);
                    ObjCon.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);
                    ObjCon.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                    ObjCon.SetColumnData("BillQTY", SqlDbType.Int, QTY);
                    ObjCon.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                    ObjCon.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                    ObjCon.SetColumnData("Total", SqlDbType.Decimal, Total);
                    ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreTransferItemDetails", false);
                }
                ObjCon.UpdateColumnData("TotalQTY", SqlDbType.Int, pBillQTY);
                ObjCon.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                ObjCon.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjCon.UpdateData(clsUtility.DBName + ".dbo.tblStoreTransferBillDetails", "StoreTransferID='" + Bill_ID + "'");

                clsUtility.ShowInfoMessage("Item has been transferd to selected store.", clsUtility.strProjectTitle);
               
            }

            return _BillNumber;
        }

        DataTable dtItemDetails = new DataTable();
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
        private void InitItemTable()
        {
            dtItemDetails.Columns.Add("ProductID");
            dtItemDetails.Columns.Add("ProductName");
            dtItemDetails.Columns.Add("BarcodeNo");
            dtItemDetails.Columns.Add("ColorID");
            dtItemDetails.Columns.Add("Color");
            dtItemDetails.Columns.Add("SizeID");
            dtItemDetails.Columns.Add("Size");
            dtItemDetails.Columns.Add("BillQTY");
            dtItemDetails.Columns.Add("StockQTY");
            dtItemDetails.Columns.Add("Rate");
            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");
            dtItemDetails.Columns.Add("Total");
            dtItemDetails.Columns.Add("Delete");
            dtItemDetails.Columns.Add("SubProductID");
            
        }
        private void UpdateQTYByOne(string barCode, decimal rate)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["BillQTY"]) + 1;
            // set to col
            dRow[0]["BillQTY"] = NewQTY.ToString();
            // cal total
            decimal total = rate * NewQTY;
            // set the total
            dRow[0]["Total"] = total.ToString();
            dtItemDetails.AcceptChanges();
            dgvProductDetails.DataSource = dtItemDetails;
        }
        private Image GetProductPhoto(int ProductID)
        {
            Image imgProduct = null;
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".dbo.ProductMaster WITH(NOLOCK) WHERE ProductID=" + ProductID);
            if (ObjUtil.ValidateTable(dt))
            {
                if (dt.Rows[0]["Photo"] != DBNull.Value)
                {
                    DataTable dtImagePath = ObjCon.ExecuteSelectStatement(" SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
                    if (dtImagePath.Rows.Count > 0)
                    {
                        if (dtImagePath.Rows[0]["ImagePath"] != DBNull.Value)
                        {
                            string ImgPath = dtImagePath.Rows[0]["ImagePath"].ToString();
                            string extension = dtImagePath.Rows[0]["Extension"].ToString();

                            string imgFile = ImgPath + "//" + dt.Rows[0]["Photo"].ToString() + extension;
                            if (File.Exists(imgFile))
                            {
                                imgProduct = Image.FromFile(imgFile);
                            }
                            else
                            {
                                imgProduct = null;
                            }
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                        }
                    }
                }
            }
            return imgProduct;
        }

        private void AddRowToItemDetails(string productID, string name, string qty, string rate, string total,
           string BarCode, string SizeID, string Size, string ColorID, string Color, string stockQTY, string SubProductID)
        {
            DataRow dRow = dtItemDetails.NewRow();
            dRow["ProductID"] = productID;
            dRow["ProductName"] = name;
            dRow["BillQTY"] = qty;
            dRow["StockQTY"] = stockQTY;
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

            dgvProductDetails.DataSource = dtItemDetails;
        }
        private void GetItemDetailsByProductID(string _BarCodeValue)
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.GetProductDetailsByBarCode " + cmdFrom.SelectedValue + ", " + _BarCodeValue);
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
                string SubProductID= dt.Rows[0]["SubProductID"].ToString();

                // if Item already there in the grid, then just increase the QTY
                if (IsItemExist(barCode))
                {
                    UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate));
                    picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));
                }
                else
                {
                    AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, stockQTY, SubProductID);
                    picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));
                }

                txtBarCode.Clear();

                CalculateGrandTotal();
                dgvProductDetails.ClearSelection();
                txtBarCode.Focus();
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue, clsUtility.strProjectTitle);
            }
        }
        private void ClearAll()
        {
            dtItemDetails.Clear();
            Bill_ID = "";

            lblBillStatus.Text = "Not Posted";

            txtInvoiceNumber.Clear();
            txtBarCode.Clear();
            cmdTo.SelectedIndex = -1;

            picProduct.Image = null;
            txtValue.Clear();
            txtTotalQTY.Clear();
        }
        private void CalculateGrandTotal()
        {
            decimal NewBillAmount = 0.0M;
            decimal TotalQTY = 0;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                NewBillAmount += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["Total"].Value);
                TotalQTY += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["BillQTY"].Value);
            }

            //for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            //{
            //    TotalQTY += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["BillQTY"].Value);
            //}

            txtTotalQTY.Text = TotalQTY.ToString();
            txtValue.Text = NewBillAmount.ToString();
        }
        decimal _StartValue = 0;
        private void cmdTo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtBarCode.Focus();
        }
        CoreApp.clsUtility ObjUtil = new clsUtility();
        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);

            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["ColoriD"].Visible = false;
            dgvProductDetails.Columns["SizeiD"].Visible = false;
            dgvProductDetails.Columns["OIRate"].Visible = false;
            dgvProductDetails.Columns["Adj_Amount"].Visible = false;
            dgvProductDetails.Columns["SubProductID"].Visible = false;
            
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, System.Drawing.Color.White);
            dgvProductDetails.ClearSelection();
            
           
                dgvProductDetails.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
                dgvProductDetails.RowsDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            
        }

        private void dgvProductDetails_KeyDown(object sender, KeyEventArgs e)
        {
            GetItemDetailsByProductID(txtBarCode.Text);
        }

        private void dgvProductDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                _StartValue = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value);
            }
            catch (Exception)
            {
            }
        }

        private void dgvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal QTY = 0;
            //dgvProductDetails.Columns[e.ColumnIndex].Name == "Rate" ||
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "BillQTY")
            {
                string pID = dgvProductDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                string sizeid = dgvProductDetails.Rows[e.RowIndex].Cells["SizeID"].Value.ToString();
                string colorid = dgvProductDetails.Rows[e.RowIndex].Cells["ColorID"].Value.ToString();
                 QTY = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value);

                decimal Rate = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                decimal Total = QTY * Rate;
                string _barNo = dgvProductDetails.Rows[e.RowIndex].Cells["Barcodeno"].Value.ToString();

                dgvProductDetails.Rows[e.RowIndex].Cells["Total"].Value = Total.ToString();
                CalculateGrandTotal();
            }

            int StockQTY = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["StockQTY"].Value);
       
            if (QTY <= StockQTY)
            {
                dgvProductDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dgvProductDetails.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                
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
                }
            }
            else
            {
                int _PID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["ProductID"].Value);
                picProduct.Image = GetProductPhoto(Convert.ToInt32(_PID));
            }
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmdFrom.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select from Shop.", clsUtility.strProjectTitle);
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to cancel?", clsUtility.strProjectTitle);
            if (result)
            {
                ClearAll();
            }
        }

        private bool IsValidateQTY()
        {
            bool Result = true;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                int QTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["BillQTY"].Value);
                int StockQTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["StockQTY"].Value);

                if (QTY > StockQTY)
                {
                    dgvProductDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    dgvProductDetails.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    Result = false;
                }
            }
            dgvProductDetails.ClearSelection();
            return Result;
        }
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (cmdTo.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select To Shop.", clsUtility.strProjectTitle);
                return;
            }

            if (dgvProductDetails.Rows.Count == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Items.", clsUtility.strProjectTitle);
                return;
            }
            if (lblBillStatus.Text == "Posted")
            {
                clsUtility.ShowInfoMessage("The Bill Status is Posted. you can not make any changes.", clsUtility.strProjectTitle);
                return;
            }
            if (IsValidateQTY())
            {
                Sales.frmQTYValidation frmQTYValidation = new Sales.frmQTYValidation();
                frmQTYValidation.TotalQTY = Convert.ToInt32(txtTotalQTY.Text);
                frmQTYValidation.ShowDialog();
                if (Sales.frmQTYValidation.QTYConfirmation == -1)
                {
                    // Exit the bill -Dead bill
                    this.Close();
                }
                else if (Sales.frmQTYValidation.QTYConfirmation == 1)
                {
                    TransferStock();
                    ClearAll();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text.Trim().Length == 0)
            {
                clsUtility.ShowInfoMessage("Please enter Bill Number.", clsUtility.strProjectTitle);
                txtInvoiceNumber.Focus();
                return;
            }

            //if (cmdFrom.SelectedIndex==-1)
            //{
            //    clsUtility.ShowInfoMessage("Please select the from store.", clsUtility.strProjectTitle);
            //    return;
            //}
            SearchBill();
            cmdFrom.Enabled = false;
            cmdTo.Enabled = false;
        }
        string Bill_ID = "";
        private void SearchBill()
        {
            try
            {
                //string strQ = "SELECT * FROM tblStoreTransferBillDetails WITH(NOLOCK) WHERE BillNo='" + txtInvoiceNumber.Text + "' AND FromStore=" + cmdFrom.SelectedValue;

                string strQ = "SELECT * FROM tblStoreTransferBillDetails WITH(NOLOCK) WHERE BillNo='" + txtInvoiceNumber.Text + "'";

                DataTable dtDetails = ObjCon.ExecuteSelectStatement(strQ);
                if (ObjUtil.ValidateTable(dtDetails))
                {
                    Bill_ID = dtDetails.Rows[0]["StoreTransferID"].ToString();
                    cmdFrom.SelectedValue = dtDetails.Rows[0]["FromStore"];
                    cmdTo.SelectedValue = dtDetails.Rows[0]["ToStore"];
                    lblBillStatus.Text = dtDetails.Rows[0]["BillStatus"].ToString();
                    dtpSalesDate.Value = Convert.ToDateTime(dtDetails.Rows[0]["BillDate"]);

                    //string strItemQuery = "select ProductID,(select ProductName from ProductMaster where ProductID=ts.ProductID) as ProductName, BillQTY,Rate,Total,Barcode,SizeID," +
                    //                      " (select Size from SizeMaster where SizeID = ts.SizeID) as Size,ColorID," +
                    //                      " (select ColorName from ColorMaster where ColorID = ts.ColorID) as ColorName," +
                    //                       "(select QTY from ProductStockColorSizeMaster where StoreID = " + cmdFrom.SelectedValue + " and BarcodeNo = ts.Barcode) as StockQTY" +
                    //                      " from tblStoreTransferItemDetails ts where StoreBillDetailsID = " + Bill_ID;


                    string strItemQuery = "select sti.ProductID, ps.ProductName, sti.BillQTY,sti.Rate,sti.Total,sti.Barcode,sti.SizeID,sm.Size, sti.ColorID, col.ColorName,psm.QTY as StockQTY, psm.SubProductID from tblStoreTransferItemDetails sti join " +
                                        " ProductMaster ps on sti.ProductID = ps.ProductID " +
                                        " join ColorMaster as col on sti.ColorID = col.ColorID " +
                                        " join SizeMaster as sm on sti.SizeID = sm.SizeID " +
                                        " join ProductStockColorSizeMaster psm on sti.Barcode = psm.BarcodeNo " +
                                        " where psm.StoreID = "+ cmdFrom.SelectedValue + " and psm.BarcodeNo = sti.Barcode " +
                                        " AND sti.StoreBillDetailsID =  "+ Bill_ID;



                    DataTable dtOldBill = ObjCon.ExecuteSelectStatement(strItemQuery);
                    if (ObjUtil.ValidateTable(dtOldBill))
                    {
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
                            string StockQTY = dtOldBill.Rows[i]["StockQTY"].ToString();
                            string SubProductID = dtOldBill.Rows[i]["SubProductID"].ToString();

                            AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, StockQTY, SubProductID);
                        }
                        CalculateGrandTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void lblCategoryName_Click(object sender, EventArgs e)
        {
            if (dgvProductDetails.Columns["StockQTY"].Visible)
            {
                dgvProductDetails.Columns["StockQTY"].Visible = false;
            }
            else
            {
                dgvProductDetails.Columns["StockQTY"].Visible = true;
            }
        }
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmdTo.SelectedIndex == -1)
            {
                clsUtility.ShowInfoMessage("Please select To Shop.", clsUtility.strProjectTitle);
                return;
            }

            if (dgvProductDetails.Rows.Count == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Items.", clsUtility.strProjectTitle);
                return;
            }
            if (lblBillStatus.Text == "Posted")
            {
                clsUtility.ShowInfoMessage("The Bill Status is Posted. you can not make any changes.", clsUtility.strProjectTitle);
                return;
            }
            if (IsValidateQTY())
            {
                Sales.frmQTYValidation frmQTYValidation = new Sales.frmQTYValidation();
                frmQTYValidation.TotalQTY = Convert.ToInt32(txtTotalQTY.Text);
                frmQTYValidation.ShowDialog();
                if (Sales.frmQTYValidation.QTYConfirmation == -1)
                {
                    // Exit the bill -Dead bill
                    this.Close();
                }
                else if (Sales.frmQTYValidation.QTYConfirmation == 1)
                {
                   string BillNo= TransferStock();

                    Report.Report_Forms.frmStoreTransferReport frmStoreTransferReport = new Report.Report_Forms.frmStoreTransferReport();
                    frmStoreTransferReport.strFromStore = cmdFrom.Text.ToString();
                    frmStoreTransferReport.strToStore = cmdTo.Text.ToString();

                    frmStoreTransferReport.dtStoreTransferDetails = this.dtItemDetails;
                    frmStoreTransferReport.strTotalQTY = txtTotalQTY.Text;
                    frmStoreTransferReport.strTotalRate = txtValue.Text;
                    frmStoreTransferReport.strBillDate = dtpSalesDate.Value.ToShortDateString();
                    frmStoreTransferReport.strBillNo = BillNo;

                    frmStoreTransferReport.ShowDialog();

                    ClearAll();
                }


               

            }
        }

        private void cmdFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadToStore();
        }
    }
}