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
            string str = "select StoreID from DefaultStoreSetting where MachineName='" + Environment.MachineName + "'";

            int deafultStoreID = ObjCon.ExecuteScalarInt("SELECT Storeid FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName = '" + Environment.MachineName + "'");
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
                 strQ = "select StoreID,StoreName from " + clsUtility.DBName + ".dbo.StoreMaster ";
                cmdFrom.Enabled = true;

            }
            else
            {
                cmdFrom.Enabled = false;
                strQ = "select StoreID,StoreName from " + clsUtility.DBName + ".dbo.StoreMaster where StoreID in  " +
                           " (select StoreID from  " + clsUtility.DBName + ".dbo.tblStoreUserRights where UserID = " + clsUtility.LoginID + ")";

            }


            DataTable dtFromStore = ObjCon.ExecuteSelectStatement(strQ);
            if (dtFromStore.Rows.Count > 0)
            {
                cmdFrom.DataSource = dtFromStore;
                cmdFrom.DisplayMember = "StoreName";
                cmdFrom.ValueMember = "StoreID";
            }
        }
        private void LoadToStore()
        {
            string strQ = "";
            if (cmdFrom.SelectedValue!=null)
            {

                if (clsUtility.IsAdmin)
                {
                    strQ = "select StoreID,StoreName from StoreMaster where StoreID not in (" + cmdFrom.SelectedValue + ")";

                 

                }
                else
                {
                     strQ = "select StoreID,StoreName from StoreMaster where StoreID in  " +
                               " (select StoreID from tblStoreUserRights where UserID = " + clsUtility.LoginID + " AND StoreID not in (" + cmdFrom.SelectedValue + "))";

                }

                DataTable ftToStore = ObjCon.ExecuteSelectStatement(strQ);
                if (ftToStore.Rows.Count > 0)
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
        private void TransferStock()
        {
            ObjCon.SetColumnData("BillNo", SqlDbType.NVarChar, GenerateBillNumber());
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

                    ObjCon.SetColumnData("StoreBillDetailsID", SqlDbType.Int, TransferID);

                    ObjCon.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                    ObjCon.SetColumnData("Barcode", SqlDbType.NVarChar, BarcodeNo);
                    ObjCon.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                    ObjCon.SetColumnData("BillQTY", SqlDbType.Int, QTY);
                    ObjCon.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                    ObjCon.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                    ObjCon.SetColumnData("Total", SqlDbType.Decimal, Total);
                    ObjCon.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjCon.InsertData(clsUtility.DBName + ".dbo.tblStoreTransferItemDetails", false);
                }
                clsUtility.ShowInfoMessage("Item has been transferd to selected store.", clsUtility.strProjectTitle);
                ClearAll();
            }
        }
        private bool CheckProductQTY(string _BarCoeNumber, decimal CurQTY)
        {
            string strSQL = "SELECT QTY FROM  " + clsUtility.DBName + ".[dbo].[ProductStockColorSizeMaster] WITH(NOLOCK) WHERE BarcodeNo=" + _BarCoeNumber + " AND StoreID=" + cmdFrom.SelectedValue.ToString();

            decimal TotalQTY = Convert.ToDecimal(ObjCon.ExecuteScalar(strSQL));

            if (CurQTY > TotalQTY)
            {
                return false;
            }
            else
            {
                return true;
            }
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
            dtItemDetails.Columns.Add("Rate");

            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");

            dtItemDetails.Columns.Add("Total");
            dtItemDetails.Columns.Add("Delete");
        }
        private void UpdateQTYByOne(string barCode, decimal rate)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["BillQTY"]) + 1;
            if (CheckProductQTY(barCode, Convert.ToDecimal(NewQTY)))
            {
                // set to col
                dRow[0]["BillQTY"] = NewQTY.ToString();
                // cal total
                decimal total = rate * NewQTY;
                // set the total
                dRow[0]["Total"] = total.ToString();
                dtItemDetails.AcceptChanges();
                dgvProductDetails.DataSource = dtItemDetails;
            }
            else
            {
                clsUtility.ShowInfoMessage("No QTY avaiable for the given Product.", clsUtility.strProjectTitle);
            }
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
           string BarCode, string SizeID, string Size, string ColorID, string Color)
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
                string qty = "1";
                string SizeID = dt.Rows[0]["SizeID"].ToString();
                string Size = dt.Rows[0]["Size"].ToString();
                string ColorID = dt.Rows[0]["ColorID"].ToString();
                string ColorName = dt.Rows[0]["ColorName"].ToString();
                decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);

                if (CheckProductQTY(barCode, Convert.ToDecimal(qty)))
                {
                    // if Item already there in the grid, then just increase the QTY
                    if (IsItemExist(barCode))
                    {
                        UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate));
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));
                    }
                    else
                    {
                        AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));
                    }

                    txtBarCode.Clear();

                    CalculateGrandTotal();
                    dgvProductDetails.ClearSelection();
                    txtBarCode.Focus();
                }
                else
                {
                    clsUtility.ShowInfoMessage("No QTY avaiable for the Product : " + txtBarCode.Text, clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue, clsUtility.strProjectTitle);
            }
        }
        private void ClearAll()
        {
            dtItemDetails.Clear();

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
            }

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                TotalQTY += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["BillQTY"].Value);
            }

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
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
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
            //dgvProductDetails.Columns[e.ColumnIndex].Name == "Rate" ||
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "BillQTY")
            {
                string pID = dgvProductDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                string sizeid = dgvProductDetails.Rows[e.RowIndex].Cells["SizeID"].Value.ToString();
                string colorid = dgvProductDetails.Rows[e.RowIndex].Cells["ColorID"].Value.ToString();
                decimal QTY = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value);
                if (QTY < 0)
                {
                    clsUtility.ShowInfoMessage("The QTY can not be negative. Default 1 QTY will be set.", clsUtility.strProjectTitle);
                    dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value = "1";
                    QTY = 1;
                }
                decimal Rate = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                decimal Total = QTY * Rate;
                string _barNo = dgvProductDetails.Rows[e.RowIndex].Cells["Barcodeno"].Value.ToString();

                if (CheckProductQTY(_barNo, Convert.ToDecimal(QTY)))
                {
                    dgvProductDetails.Rows[e.RowIndex].Cells["Total"].Value = Total.ToString();
                    CalculateGrandTotal();
                }
                else
                {
                    dgvProductDetails.Rows[e.RowIndex].Cells["BillQTY"].Value = _StartValue;
                    CalculateGrandTotal();

                    clsUtility.ShowInfoMessage("QTY : " + QTY + " NOT avaiable for the Product : " + dgvProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value, clsUtility.strProjectTitle);
                }
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

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
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
            }
        }
    }
}