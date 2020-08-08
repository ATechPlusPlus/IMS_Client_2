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
using IMS_Client_2.Barcode;

namespace IMS_Client_2.Sales
{
    public partial class Sales_Invoice : Form
    {
        public Sales_Invoice()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        bool isFromTabChanged = false;
        public bool IsReplaceReturnMode = false;
        public string OldInvoiceID = "";

        DataTable dtItemDetails = new DataTable();
        DataTable dtReplcaeItemDetails = new DataTable();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void Sales_Invoice_Load(object sender, EventArgs e)
        {
            cboEntryMode.SelectedIndex = 0; // by default

            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;
            btnSaveData.BackgroundImage = B_Leave;

            BindStoreDetails();

            // GenerateInvoiceNumber();
            InitItemTable();
            dtpSalesDate.Value = DateTime.Now;
            txtBarCode.Focus();
            dtpSalesDate.MaxDate = DateTime.Now;
            if (!IsReplaceReturnMode)
            {
                tabControl1.TabPages.RemoveAt(1);
                radNewItem.Visible = false;
                radReplace.Visible = false;
                lblTitle.Visible = false;
                label16.Enabled = false;
                txtOldBillAmount.Enabled = false;
            }
            else
            {
                radReplace.Checked = true;
                label16.Enabled = true;
                txtOldBillAmount.Enabled = true;
            }
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
        private void InitItemTable()
        {
            dtItemDetails.Columns.Add("ProductID");
            dtItemDetails.Columns.Add("ProductName");
            dtItemDetails.Columns.Add("BarcodeNo");
            dtItemDetails.Columns.Add("ColorID");
            dtItemDetails.Columns.Add("Color");
            dtItemDetails.Columns.Add("SizeID");
            dtItemDetails.Columns.Add("Size");
            dtItemDetails.Columns.Add("QTY");
            dtItemDetails.Columns.Add("Rate");

            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");
            dtItemDetails.Columns.Add("Total");
            dtItemDetails.Columns.Add("Delete");
            dtItemDetails.Columns.Add("SubProductID");

            

            dtReplcaeItemDetails.Columns.Add("ProductID");
            dtReplcaeItemDetails.Columns.Add("ProductName");
            dtReplcaeItemDetails.Columns.Add("BarcodeNo");
            dtReplcaeItemDetails.Columns.Add("ColorID");
            dtReplcaeItemDetails.Columns.Add("Color");
            dtReplcaeItemDetails.Columns.Add("SizeID");
            dtReplcaeItemDetails.Columns.Add("Size");
            dtReplcaeItemDetails.Columns.Add("QTY");
            dtReplcaeItemDetails.Columns.Add("Rate");
            dtReplcaeItemDetails.Columns.Add("Total");
            dtReplcaeItemDetails.Columns.Add("Delete");
            dtReplcaeItemDetails.Columns.Add("SubProductID");
        }
        private void AddRowToItemDetails(string productID, string name, string qty, string rate, string total,
            string BarCode, string SizeID, string Size, string ColorID, string Color, string subProductID)
        {
            DataRow dRow = dtItemDetails.NewRow();
            dRow["ProductID"] = productID;
            dRow["ProductName"] = name;
            dRow["QTY"] = qty;
            dRow["Rate"] = rate;
            dRow["Total"] = total;
            dRow["Delete"] = "Delete";
            dRow["ColorID"] = ColorID;
            dRow["Color"] = Color;
            dRow["SizeID"] = SizeID;
            dRow["Size"] = Size;
            dRow["BarcodeNo"] = BarCode;
            dRow["SubProductID"] = subProductID;

            if (radNewItem.Checked && IsReplaceReturnMode)
            {
                SetOldRate(dRow, BarCode, Convert.ToDecimal(rate));
            }

            dtItemDetails.Rows.Add(dRow);
            dtItemDetails.AcceptChanges();

            dgvProductDetails.DataSource = dtItemDetails;
        }
        private void SetOldRate(DataRow drow, string _BarCoeNumber, decimal CurrentRate)
        {
            ObjDAL.SetStoreProcedureData("InvoiceID", SqlDbType.Int, OldInvoiceID);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.NVarChar, _BarCoeNumber);

            DataSet dataSet = ObjDAL.ExecuteStoreProcedure_Get("spr_GetReplaceReturnDetails");
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                drow["OIRate"] = dataSet.Tables[0].Rows[0]["Rate"].ToString();

                decimal odlRate = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["Rate"]);
                decimal adAmount = CurrentRate - odlRate;
                drow["Adj_Amount"] = adAmount.ToString();
            }
        }
        private void AddRowToReplaceItemDetails(string productID, string name, string qty, string rate, string total,
       string BarCode, string SizeID, string Size, string ColorID, string Color, string SubProductID)
        {
            DataRow dRow = dtReplcaeItemDetails.NewRow();
            dRow["ProductID"] = productID;
            dRow["ProductName"] = name;
            dRow["QTY"] = qty;
            dRow["Rate"] = rate;
            dRow["Total"] = total;
            dRow["Delete"] = "Delete";
            dRow["ColorID"] = ColorID;
            dRow["Color"] = Color;
            dRow["SizeID"] = SizeID;
            dRow["Size"] = Size;
            dRow["BarcodeNo"] = BarCode;
            dRow["SubProductID"] = SubProductID;

            
            dtReplcaeItemDetails.Rows.Add(dRow);
            dtReplcaeItemDetails.AcceptChanges();

            dgvReplaceReturn.DataSource = dtReplcaeItemDetails;
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
        private bool IsReplaceItemExist(string barCode)
        {
            DataRow[] dRow = dtReplcaeItemDetails.Select("BarcodeNo='" + barCode + "'");
            if (dRow.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool IsItemExist_NonBarCode(string PID, string ColorID, string SizeID)
        {
            DataRow[] dRow = dtItemDetails.Select("ProductID='" + PID + "' AND ColorID='" + ColorID + "' AND SizeID='" + SizeID + "'");
            if (dRow.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void UpdateQTYByOne(string barCode, decimal rate)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["QTY"]) + 1;
            if (CheckProductQTY(barCode, Convert.ToDecimal(NewQTY)))
            {
                // set to col
                dRow[0]["QTY"] = NewQTY.ToString();
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
        private void UpdateReplaceQTYByOne(string barCode, decimal rate)
        {
            DataRow[] dRow = dtReplcaeItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["QTY"]) + 1;
            if (CheckReplaceProductQTY(barCode, Convert.ToDecimal(NewQTY)))
            {
                // set to col
                dRow[0]["QTY"] = NewQTY.ToString();
                // cal total
                decimal total = rate * NewQTY;
                // set the total
                dRow[0]["Total"] = total.ToString();
                dtReplcaeItemDetails.AcceptChanges();
                dgvReplaceReturn.DataSource = dtReplcaeItemDetails;
            }
            else
            {
                clsUtility.ShowInfoMessage("All QTY for this Item has been added from old invoice. No more QTY Left", clsUtility.strProjectTitle);
            }
        }
        private void UpdateQTYByOne_NonBarCode(string pID, string ColorID, string SizeID, decimal rate)
        {
            DataRow[] dRow = dtItemDetails.Select("ProductID='" + pID + "' AND SizeID='" + SizeID + "' AND ColorID='" + ColorID + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["QTY"]) + 1;
            if (CheckProductQTY_Non_BarCode(pID, SizeID, ColorID, Convert.ToDecimal(NewQTY)))
            {
                // set to col
                dRow[0]["QTY"] = NewQTY.ToString();
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
        private string GenerateInvoiceNumber()
        {
            string InvoiceNumber = "";
            if (IsReplaceReturnMode)
            {
                //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
                int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].SequenceInvoice");
                InvoiceNumber = "RE-" + LastID;
            }
            else
            {
                //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
                int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].SequenceInvoice");
                InvoiceNumber = "INV-" + LastID;
            }
            return InvoiceNumber;
        }
        private string GenerateReplaceReturnInvoiceNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].SequenceInvoice");
            string InvoiceNumber = "INV-" + LastID;
            return InvoiceNumber;
        }
        private void BindStoreDetails()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", " StoreID");
            cmbShop.DataSource = dt;
            cmbShop.DisplayMember = "StoreName";
            cmbShop.ValueMember = "StoreID";
            cmbShop.SelectedIndex = -1;
            // set Default store
            //int deafultStoreID = ObjDAL.ExecuteScalarInt("SELECT Storeid FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK) WHERE MachineName = '" + Environment.MachineName + "'");
            int deafultStoreID = frmHome.Home_StoreID;
            cmbShop.SelectedValue = deafultStoreID;
            if (deafultStoreID == 0)
            {
                clsUtility.ShowInfoMessage("Please select the default shop for this client from Setting Window.", clsUtility.strProjectTitle);
                Settings.frmOtherSetting otherSetting = new Settings.frmOtherSetting();
                otherSetting.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Store_Master) || clsUtility.IsAdmin)
            {
                Masters.Store_Master Obj = new Masters.Store_Master();
                Obj.Show();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnDrug_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Employee_Details) || clsUtility.IsAdmin)
            {
                Masters.Employee_Details Obj = new Masters.Employee_Details();
                Obj.Show();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Customer_Master) || clsUtility.IsAdmin)
            {
                Masters.Customer_Master customer_Master = new Masters.Customer_Master();
                customer_Master.ShowDialog();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void txtSalesMan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Empid,EmployeeCode FROM " + clsUtility.DBName + ".dbo.employeeDetails WITH(NOLOCK) WHERE [Name] Like '" + txtSalesMan.Text + "%'");
                if (ObjUtil.ValidateTable(dt))
                {
                    ObjUtil.SetControlData(txtSalesMan, "EmployeeCode");
                    ObjUtil.SetControlData(txtEmpID, "Empid");
                    ObjUtil.ShowDataPopup(dt, txtSalesMan, this, this);
                    if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                    {
                        // if there is only one column                
                        ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (ObjUtil.GetDataPopup().ColumnCount > 0)
                        {
                            ObjUtil.GetDataPopup().Columns["Empid"].Visible = false;
                            ObjUtil.SetDataPopupSize(200, 0);
                        }
                    }
                    //ObjUtil.GetDataPopup().CellClick += frmSalecounter_CellClick;
                    //ObjUtil.GetDataPopup().KeyDown += frmSalecounter_KeyDown;
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }
        private Image GetProductPhoto(int SubProductID)
        {
            Image imgProduct = null;
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".dbo.tblProductWiseModelNo WHERE SubProductID=" + SubProductID);
            if (ObjUtil.ValidateTable(dt))
            {
                if (dt.Rows[0]["Photo"] != DBNull.Value)
                {
                    DataTable dtImagePath = ObjDAL.ExecuteSelectStatement("  SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
                    if (ObjUtil.ValidateTable(dtImagePath))
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

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (cboEntryMode.SelectedIndex == 1) // if manual entry
            {
                if (txtBarCode.Text.Trim().Length == 0)
                {
                    return;
                }
            }
            else
            {
                if (txtBarCode.Text.Trim().Length != 0 && !ObjUtil.IsNumeric(txtBarCode.Text))
                {
                    clsUtility.ShowInfoMessage("Invalid BarCode Entry. Please check the Product Code.", clsUtility.strProjectTitle);
                }
            }
        }


        private bool CheckProductQTY(string _BarCoeNumber, decimal CurQTY)
        {
            string strSQL = "SELECT QTY FROM  " + clsUtility.DBName + ".[dbo].[ProductStockColorSizeMaster] WITH(NOLOCK) WHERE BarcodeNo=" + _BarCoeNumber + " AND StoreID=" + cmbShop.SelectedValue.ToString();

            decimal TotalQTY = Convert.ToDecimal(ObjDAL.ExecuteScalar(strSQL));

            if (CurQTY > TotalQTY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckReplaceProductQTY(string _BarCoeNumber, decimal CurQTY)
        {
            int TotalQTY = 0;
            ObjDAL.SetStoreProcedureData("InvoiceID", SqlDbType.Int, OldInvoiceID);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.NVarChar, _BarCoeNumber);

            DataSet dataSet = ObjDAL.ExecuteStoreProcedure_Get("spr_GetReplaceReturnDetails");
            if (dataSet.Tables.Count > 0)
            {
                TotalQTY = Convert.ToInt32(dataSet.Tables[0].Rows[0]["QTY"]);
                if (CurQTY > TotalQTY)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckProductQTY_Non_BarCode(string PID, string SizeID, string ColorID, decimal CurQTY)
        {
            string strSQL = "SELECT QTY FROM  " + clsUtility.DBName + ".[dbo].[ProductStockColorSizeMaster] WITH(NOLOCK) WHERE ProductID=" + PID + " AND  SizeID=" + SizeID + " AND ColorID=" + ColorID + " AND StoreID=" + cmbShop.SelectedValue.ToString();

            decimal TotalQTY = Convert.ToDecimal(ObjDAL.ExecuteScalar(strSQL));
            if (CurQTY > TotalQTY)
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
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.GetProductDetailsByBarCode " + cmbShop.SelectedValue + ", " + _BarCodeValue);
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
                string subProductID= dt.Rows[0]["SubProductID"].ToString();

                if (CheckProductQTY(barCode, Convert.ToDecimal(qty)))
                {
                    // if Item already there in the grid, then just increase the QTY
                    if (IsItemExist(barCode))
                    {
                        UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate));
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(subProductID));
                    }
                    else
                    {
                        AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, subProductID);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(subProductID));

                    }
                    txtProductID.Clear();
                    txtBarCode.Clear();

                    CalculateGrandTotal();
                    dgvProductDetails.ClearSelection();
                    txtBarCode.Focus();
                }
                else
                {
                    clsUtility.ShowInfoMessage("No QTY avaiable for the Product : " + txtBarCode.Text, clsUtility.strProjectTitle);
                    txtBarCode.Clear();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue, clsUtility.strProjectTitle);
                txtBarCode.Clear();
            }
        }

        
        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            txtTotalItems.Text = "New Items Count :" + dgvProductDetails.Rows.Count.ToString();
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["ColoriD"].Visible = false;
            dgvProductDetails.Columns["SizeiD"].Visible = false;
            dgvProductDetails.Columns["SubProductID"].Visible = false;
            if (IsReplaceReturnMode)
            {
                dgvProductDetails.Columns["OIRate"].Visible = true;
                dgvProductDetails.Columns["Adj_Amount"].Visible = false;
                ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            }
            else
            {
                dgvProductDetails.Columns["OIRate"].Visible = false;
                dgvProductDetails.Columns["Adj_Amount"].Visible = false;
                ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            }
        }

        private void dgvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dgvProductDetails.Columns[e.ColumnIndex].Name == "Rate" ||
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "QTY")
            {
                string pID = dgvProductDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                string sizeid = dgvProductDetails.Rows[e.RowIndex].Cells["SizeID"].Value.ToString();
                string colorid = dgvProductDetails.Rows[e.RowIndex].Cells["ColorID"].Value.ToString();
                decimal QTY = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value);
                if (QTY < 0)
                {
                    clsUtility.ShowInfoMessage("The QTY can not be negative. Default 1 QTY will be set.", clsUtility.strProjectTitle);
                    dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value = "1";
                    QTY = 1;
                }
                decimal Rate = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                decimal Total = QTY * Rate;
                string _barNo = dgvProductDetails.Rows[e.RowIndex].Cells["Barcodeno"].Value.ToString();

                if (_barNo.Trim().Length == 0) // if barcode not found
                {
                    if (CheckProductQTY_Non_BarCode(pID, sizeid, colorid, QTY))
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["Total"].Value = Total.ToString();
                        CalculateGrandTotal();
                    }
                    else
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value = _StartValue;
                        CalculateGrandTotal();

                        clsUtility.ShowInfoMessage("QTY : " + QTY + " NOT avaiable for the Product : " + dgvProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value, clsUtility.strProjectTitle);
                    }
                }
                else
                {
                    if (CheckProductQTY(_barNo, Convert.ToDecimal(QTY)))
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["Total"].Value = Total.ToString();
                        CalculateGrandTotal();
                    }
                    else
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value = _StartValue;
                        CalculateGrandTotal();

                        clsUtility.ShowInfoMessage("QTY : " + QTY + " NOT avaiable for the Product : " + dgvProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value, clsUtility.strProjectTitle);
                    }
                }
            }
        }
        private void CalculateGrandTotal()
        {
            decimal NewBillAmount = 0.0M;
            decimal OldBillAmount = 0.0M;
            decimal Discount = 0.0M;
            decimal Deliverycharges = 0.0M;
            decimal GrandTotal = 0.0M;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                NewBillAmount += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["Total"].Value);
            }

            for (int i = 0; i < dgvReplaceReturn.Rows.Count; i++)
            {
                OldBillAmount += Convert.ToDecimal(dgvReplaceReturn.Rows[i].Cells["ColReplaceTotal"].Value);
            }
            txtOldBillAmount.Text = Math.Round(OldBillAmount, 2).ToString();
            txtNewBillAmount.Text = Math.Round(NewBillAmount, 2).ToString();

            decimal MainSubTotal = NewBillAmount - OldBillAmount;

            txtSubTotal.Text = Math.Round(MainSubTotal, 2).ToString();

            Discount = txtDiscount.Text.Length > 0 ? Convert.ToDecimal(txtDiscount.Text) : 0;
            Deliverycharges = txtDeliveryCharges.Text.Length > 0 ? Convert.ToDecimal(txtDeliveryCharges.Text) : 0;

            GrandTotal = MainSubTotal - (MainSubTotal * Discount * 0.01M) + Deliverycharges;
            txtGrandTotal.Text = Math.Round(GrandTotal, 2).ToString();
        }

        private bool ValidateGrandTotal()
        {
            decimal FinalTotal = Convert.ToDecimal(txtCash.Text) + Convert.ToDecimal(txtCredit.Text);

            decimal GrandTotal = Convert.ToDecimal(txtGrandTotal.Text);

            if (FinalTotal == GrandTotal)
            {
                return true;
            }
            else
            {
                return false;
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
                    picProduct.Image = null;
                }
            }
            else
            {
                int SubID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["SubProductID"].Value);
                picProduct.Image = GetProductPhoto(Convert.ToInt32(SubID));
            }
        }

        private void ClearAll()
        {

            txtOldBillAmount.ReadOnly = false;

            Other_Forms.frmDiscountLogin.IsValidAdmin = false;
            txtDiscount.ReadOnly = false;
            txtCash.Clear();
            txtCredit.Clear();

            txtOldBillAmount.Clear();
            txtNewBillAmount.Clear();
            Other_Forms.frmPayment.ResetData();

            lblPMode.Text = "";
            txtCustomerID.Clear();
            txtCustomerMobile.Clear();
            dtItemDetails.Clear();
            dtReplcaeItemDetails.Clear();
            txtProductID.Clear();
            txtEmpID.Clear();

            txtInvoiceNumber.Clear();
            txtBarCode.Clear();
            txtCustomerMobile.Clear();
            cmbShop.SelectedIndex = -1;
            txtTotalItems.Text = "Total Items : 0";

            txtSubTotal.Text = "0";
            txtDeliveryCharges.Text = "0";
            txtDiscount.Text = "0";
            txtGrandTotal.Text = "0";
            picProduct.Image = null;
            BindStoreDetails();

            txtSalesMan.Clear();
            txtColorID.Clear();
            txtSizeID.Clear();
        }
        private bool SalesValidation(bool isSave)
        {
            if (txtCustomerID.Text.Trim().Length == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.", clsUtility.strProjectTitle);
                txtCustomerMobile.Focus();
                return false;
            }
            if (txtEmpID.Text.Trim().Length == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Sales Man Name.", clsUtility.strProjectTitle);
                txtSalesMan.Focus();
                return false;
            }
            // if only saving the dataa
            // as per salman, -ve should be printed.
            //if (isSave==false)
            //{
            //    if (txtGrandTotal.Text.Trim().Length != 0)
            //    {
            //        if (Convert.ToDecimal(txtGrandTotal.Text) <= 0)
            //        {
            //            clsUtility.ShowInfoMessage("You can not print invoice for negative grand total.", clsUtility.strProjectTitle);
            //            return false;
            //        }
            //    }
            //}
            if (!IsReplaceReturnMode)  // check this only in new mode. dont check if replace
            {
              
                if (isSave == false)
                {

                    if (!ValidateGrandTotal())
                    {
                        clsUtility.ShowInfoMessage("Cash and Credit amount must be equal to Grand Total.", clsUtility.strProjectTitle);
                        return false;
                    }
                }
            }
            
           
            return true;
        }
        private void InsertPayment(int SalesInvoiceID)
        {
            try
            {
                for (int i = 0; i < IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType.Count; i++)
                {
                    string amount = "0";
                    string number = "0";

                    switch (IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType[i])
                    {
                        case "K Net":
                            amount = IMS_Client_2.Other_Forms.frmPayment.KNET_Amount.ToString();
                            number = IMS_Client_2.Other_Forms.frmPayment.KNET_Number;
                            break;

                        case "Visa":
                            amount = IMS_Client_2.Other_Forms.frmPayment.VisaAmount.ToString();
                            number = IMS_Client_2.Other_Forms.frmPayment.VisaNumber;
                            break;

                        case "Master Card":
                            amount = IMS_Client_2.Other_Forms.frmPayment.MasterCardAmount.ToString();
                            number = IMS_Client_2.Other_Forms.frmPayment.MasterCarNumber;

                            break;

                        case "Cash":
                            amount = IMS_Client_2.Other_Forms.frmPayment.CashAmount.ToString();
                            number = "000000";
                            break;
                    }

                    ObjDAL.SetColumnData("PaymentType", SqlDbType.NVarChar, IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType[i].ToString());
                    ObjDAL.SetColumnData("Amount", SqlDbType.Decimal, Convert.ToDecimal(amount));
                    ObjDAL.SetColumnData("SalesInvoiceID", SqlDbType.Int, SalesInvoiceID);
                    ObjDAL.SetColumnData("PaymentNumber", SqlDbType.NVarChar, number);

                    ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblSalesPayment", false);
                }

            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
      
        private int DoNewSales()
        {
            dgvProductDetails.EndEdit();

            // Before sales invocing make sure you have available qty for particular store

            string InvoiceDateTime = dtpSalesDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

            #region SalesInvoiceDetails
            ObjDAL.SetColumnData("InvoiceNumber", SqlDbType.NVarChar, GenerateInvoiceNumber());
            ObjDAL.SetColumnData("InvoiceDate", SqlDbType.DateTime, InvoiceDateTime);
            ObjDAL.SetColumnData("SubTotal", SqlDbType.Decimal, txtSubTotal.Text);
            ObjDAL.SetColumnData("Discount", SqlDbType.Decimal, txtDiscount.Text);
            ObjDAL.SetColumnData("Tax", SqlDbType.Decimal, txtDeliveryCharges.Text);
            ObjDAL.SetColumnData("GrandTotal", SqlDbType.Decimal, txtGrandTotal.Text);
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
            ObjDAL.SetColumnData("CustomerID", SqlDbType.Int, txtCustomerID.Text);
            ObjDAL.SetColumnData("SalesMan", SqlDbType.Int, txtEmpID.Text);
            ObjDAL.SetColumnData("ShopeID", SqlDbType.Int, cmbShop.SelectedValue.ToString());

            int InvoiceID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.SalesInvoiceDetails", true);
            if (InvoiceID == -1)
            {
                ObjDAL.ResetData();
                return InvoiceID;
            }
            #endregion
            InsertPayment(InvoiceID);

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                string Total = dgvProductDetails.Rows[i].Cells["Total"].Value.ToString();
                string ProductID = dgvProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                string QTY = dgvProductDetails.Rows[i].Cells["QTY"].Value.ToString();
                string Rate = dgvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                string ColorID = dgvProductDetails.Rows[i].Cells["ColorID"].Value.ToString();
                string SizeID = dgvProductDetails.Rows[i].Cells["SizeID"].Value.ToString();
                string subProductID= dgvProductDetails.Rows[i].Cells["SubProductID"].Value.ToString();
                string BarCode= dgvProductDetails.Rows[i].Cells["BarCodeNo"].Value.ToString();


                ObjDAL.SetColumnData("InvoiceID", SqlDbType.Int, InvoiceID);
                ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                ObjDAL.SetColumnData("QTY", SqlDbType.Decimal, QTY);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                ObjDAL.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                ObjDAL.SetColumnData("SizeID", SqlDbType.Int, SizeID);

                ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, subProductID);
                ObjDAL.SetColumnData("BarCode", SqlDbType.NVarChar, BarCode);

                ObjDAL.InsertData(clsUtility.DBName + ".dbo.SalesDetails", false);

                ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                                        "SET QTY=QTY-" + QTY + " WHERE ProductID=" + ProductID + " AND StoreID=" + cmbShop.SelectedValue.ToString() + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID+" AND SubProductID="+subProductID);
            }
            if (Convert.ToDecimal(txtGrandTotal.Text) >= 0)
            {
                // save the payment details
                if (lblPMode.Text == "K Net" || lblPMode.Text == "Visa" || lblPMode.Text == "Master Card" || lblPMode.Text == "Other")
                {
                    // check if any entry is there
                    //string strQ = " SELECT MasterCashClosingID FROM " + clsUtility.DBName + ".[dbo].[tblMasterCashClosing] WITH(NOLOCK) WHERE cashboxDate=CONVERT(DATE,GETDATE())";
                    //int MasterCashClosingID = ObjDAL.ExecuteScalarInt(strQ);
                    int MasterCashClosingID = frmHome.Home_MasterCashClosingID;

                    int count = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblCreditClosing] WITH(NOLOCK) WHERE MasterCashClosingID=" + MasterCashClosingID + " AND Type='" + lblPMode.Text + "'");
                    if (count == 0)  // if NOT found for today
                    {
                        ObjDAL.SetColumnData("MasterCashClosingID", SqlDbType.Int, MasterCashClosingID);
                        ObjDAL.SetColumnData("Type", SqlDbType.NVarChar, lblPMode.Text);
                        ObjDAL.SetColumnData("Count", SqlDbType.Int, 1);
                        ObjDAL.SetColumnData("Value", SqlDbType.Decimal, txtGrandTotal.Text);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                        ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblCreditClosing]", false);
                    }
                    else
                    {
                        // update the tblCreditClosing, Add count and amount to the exsting value
                        string strUpdate = "  UPDATE " + clsUtility.DBName + ".[dbo].[tblCreditClosing] SET Count=Count+1 , Value=value+" + txtGrandTotal.Text +
                                        ",Type='" + lblPMode.Text + "' WHERE MasterCashClosingID=" + MasterCashClosingID;
                        ObjDAL.ExecuteNonQuery(strUpdate);
                    }
                }
            }
            return InvoiceID;
        }
        private void DoReplaceReturn()
        {
            // Take the return and add to stock
            for (int i = 0; i < dgvReplaceReturn.Rows.Count; i++)
            {
                string Total = dgvReplaceReturn.Rows[i].Cells["ColReplaceTotal"].Value.ToString();
                string ProductID = dgvReplaceReturn.Rows[i].Cells["ReplaceProductID"].Value.ToString();
                string QTY = dgvReplaceReturn.Rows[i].Cells["ReplaceQTY"].Value.ToString();
                string Rate = dgvReplaceReturn.Rows[i].Cells["ReplaceRate"].Value.ToString();
                string ColorID = dgvReplaceReturn.Rows[i].Cells["RepalceColorID"].Value.ToString();
                string SizeID = dgvReplaceReturn.Rows[i].Cells["RepalceSizeID"].Value.ToString();
                string barcodeNumber = dgvReplaceReturn.Rows[i].Cells["ReplaceBarcode"].Value.ToString();
                string SubProductID= dgvReplaceReturn.Rows[i].Cells["SubProductID"].Value.ToString();


                ObjDAL.SetColumnData("OldInvoiceID", SqlDbType.Int, OldInvoiceID);
                ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                ObjDAL.SetColumnData("QTY", SqlDbType.Decimal, QTY);
                ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                ObjDAL.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                ObjDAL.SetColumnData("SizeID", SqlDbType.Int, SizeID);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.SetColumnData("BarCode", SqlDbType.NVarChar, barcodeNumber);
                ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);

                ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblReplaceReturn", false);

                // add the QTY as we are getting back the stock
                ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                                        "SET QTY=QTY+" + QTY + " WHERE ProductID=" + ProductID + " AND StoreID=" + cmbShop.SelectedValue.ToString() + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID+ " AND SubProductID="+SubProductID);
            }

            //Cash Return Entry
            //string strQ = " SELECT MasterCashClosingID FROM " + clsUtility.DBName + ".[dbo].[tblMasterCashClosing] WITH(NOLOCK) WHERE cashboxDate)=CONVERT(DATE,GETDATE())";
            //int MasterCashClosingID = ObjDAL.ExecuteScalarInt(strQ);
            int MasterCashClosingID = frmHome.Home_MasterCashClosingID;

            int count = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblCashReturn] WITH(NOLOCK) WHERE MasterCashClosingID=" + MasterCashClosingID);
            if (count == 0)  // if NOT found for today
            {
                ObjDAL.SetColumnData("MasterCashClosingID", SqlDbType.Int, MasterCashClosingID);
                ObjDAL.SetColumnData("Value", SqlDbType.Decimal, txtOldBillAmount.Text);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblCashReturn]", false);
            }
            else
            {
                // update the tblCashReturn, Add value the exsting value

                string strUpdate = "  UPDATE " + clsUtility.DBName + ".[dbo].[tblCashReturn] SET Value=value+" + txtOldBillAmount.Text +
                                      "WHERE MasterCashClosingID=" + MasterCashClosingID;

                ObjDAL.ExecuteNonQuery(strUpdate);
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Sales_Invoice, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (button.Name == "btnPrint")
                {
                    if (clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.InvoicePrinter).Trim().Length == 0)
                    {
                        clsUtility.ShowInfoMessage("Printer Not Configured for Invoice Print. Please configure the printer from Setting menu.", clsUtility.strProjectTitle);
                        return;
                    }
                }
                if (SalesValidation(button.Name == "btnSaveData"))
                {
                    int NewInvoiceID = 0;

                    // If its New sales invoice
                    if (!IsReplaceReturnMode)
                    {
                        NewInvoiceID = DoNewSales();
                    }
                    else if (IsReplaceReturnMode) // For reaplce and retrun mode
                    {
                        DoReplaceReturn();
                        // if new item purchased ;
                        if (dgvProductDetails.Rows.Count > 0)
                        {
                            NewInvoiceID = DoNewSales();

                            // update the crossponding old invoice ID
                            ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".[dbo].[tblReplaceReturn] set NewInvoiceID=" + NewInvoiceID + " where OldInvoiceID=" + OldInvoiceID);
                        }
                    }

                    clsUtility.ShowInfoMessage("Data has been saved successfully.", clsUtility.strProjectTitle);
                    ClearAll();

                    if (button.Name == "btnSaveData")
                    {
                        this.Close();
                    }
                    else if (button.Name == "btnPrint")
                    {
                        Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
                        frmSalesInvoice.InvoiceID = NewInvoiceID;
                        frmSalesInvoice.IsDirectPrint = true;
                        frmSalesInvoice.Show();
                    }
                    else
                    {
                        Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
                        frmSalesInvoice.InvoiceID = NewInvoiceID;
                        frmSalesInvoice.IsDirectPrint = false;
                        frmSalesInvoice.Show();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to cancel?", clsUtility.strProjectTitle);
            if (result)
            {
                ClearAll();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateGrandTotal();
        }

        decimal _StartValue = 0;

        decimal _OldRateStartValue = 0;

        private void dgvProductDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                _StartValue = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value);
            }
            catch (Exception)
            {
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboEntryMode.SelectedIndex == 0)
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (IsReplaceReturnMode && radReplace.Checked)
                    {
                        // get the data froms sales.
                        GetDataFromSales(txtBarCode.Text);
                    }
                    else
                    {   //txtProductName contains barcode.
                        GetItemDetailsByProductID(txtBarCode.Text);
                    }
                }
            }
            else
            {
                if (ObjUtil.GetDataPopup() != null)
                {
                    ObjUtil.GetDataPopup().Focus();
                }
            }
        }

        private void GetDataFromSales(string barcode)
        {

            string strQ = "select s1.InvoiceID,p1.ProductName,s1.ProductID,s1.subproductID,s1.QTY,s1.Rate,s1.ColorID,s1.SizeID,s1.BarCode,c1.ColorName as Color,sm.Size from SalesDetails s1 "+
                        " join ProductMaster  p1 on s1.ProductID = p1.ProductID "+
                        " join ColorMaster c1 on s1.ColorID = c1.ColorID"+
                        " join SizeMaster sm on s1.SizeID = sm.SizeID"+
                        " where s1.BarCode = '"+ barcode + "'";


            DataTable dtSalesDetails = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtSalesDetails))
            {
                try
                {
                    string pID = dtSalesDetails.Rows[0]["ProductID"].ToString();
                    string name = dtSalesDetails.Rows[0]["ProductName"].ToString();
                    string rate = dtSalesDetails.Rows[0]["Rate"].ToString();
                    string barCode = dtSalesDetails.Rows[0]["Barcode"].ToString();
                    string qty = "1";
                    string SizeID = dtSalesDetails.Rows[0]["SizeID"].ToString();
                    string Size = dtSalesDetails.Rows[0]["Size"].ToString();
                    string ColorID = dtSalesDetails.Rows[0]["ColorID"].ToString();
                    string ColorName = dtSalesDetails.Rows[0]["Color"].ToString();
                    string SubProductID = dtSalesDetails.Rows[0]["SubProductID"].ToString();

                    decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);
                   
                    // if Item already there in the grid, then just increase the QTY
                    if (IsReplaceItemExist(barCode))
                    {
                        UpdateReplaceQTYByOne(barCode.ToString(), Convert.ToDecimal(rate));
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));
                    }
                    else
                    {
                        AddRowToReplaceItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, SubProductID);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));

                    }
                    txtProductID.Clear();
                    txtBarCode.Clear();

                    CalculateGrandTotal();
                    dgvReplaceReturn.ClearSelection();
                    txtBarCode.Focus();
                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Data found.", clsUtility.strProjectTitle);
            }
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerMobile.Text.Trim().Length > 0)
                {
                    string query = "SELECT CustomerID,[Name],PhoneNo FROM " + clsUtility.DBName + ".dbo.CustomerMaster WITH(NOLOCK) WHERE PhoneNo like '%" + txtCustomerMobile.Text + "%'";
                    DataTable dt = ObjDAL.ExecuteSelectStatement(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        ObjUtil.SetControlData(txtCustomerMobile, "PhoneNo");
                        ObjUtil.SetControlData(txtCustomerID, "CustomerID");

                        ObjUtil.ShowDataPopup(dt, txtCustomerMobile, this, this);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            // if there is only one column                
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["CustomerID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        //ObjUtil.GetDataPopup().CellClick += Sales_Invoice_CellClick;
                        //ObjUtil.GetDataPopup().KeyDown += Sales_Invoice_KeyDown;
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void picCash_Click(object sender, EventArgs e)
        {
            lblPMode.Text = "Cash";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.txtPaymentAutoID.Enabled = false;

            frmPayment.lblPaymentMode.Text = "Cash";
            frmPayment.picPaymentMode.Image = picCash.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCash.Text = Other_Forms.frmPayment.GetTotalCash().ToString();
        }
        private void picKnet_Click(object sender, EventArgs e)
        {
            lblPMode.Text = "K Net";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "K Net";
            frmPayment.picPaymentMode.Image = picKnet.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCash.Text = Other_Forms.frmPayment.GetTotalCash().ToString();
        }

        private void picVisa_Click(object sender, EventArgs e)
        {
            lblPMode.Text = "Visa";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "Visa";
            frmPayment.picPaymentMode.Image = picVisa.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCash.Text = Other_Forms.frmPayment.GetTotalCash().ToString();
        }
        private void PicMaster_Click(object sender, EventArgs e)
        {
            lblPMode.Text = "Master Card";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "Master Card";
            frmPayment.picPaymentMode.Image = PicMaster.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCash.Text = Other_Forms.frmPayment.GetTotalCash().ToString();
        }

        private void lblActiveStatus_Click(object sender, EventArgs e)
        {
            txtInvoiceNumber.ReadOnly = false;
        }

        private void Sales_Invoice_Activated(object sender, EventArgs e)
        {
            txtBarCode.Focus();
        }

        private void cboEntryMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboEntryMode.SelectedIndex == 1)
            {
                label4.Text = "Product Name :";
            }
            else
            {
                label4.Text = "Barcode :";
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            e.Handled = ObjUtil.IsDecimal(txt, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...", clsUtility.strProjectTitle);
            }
        }

        private void radNewItem_CheckedChanged(object sender, EventArgs e)
        {
            if (isFromTabChanged)
            {
                isFromTabChanged = false;
                return;
            }
            tabControl1.SelectedIndex = 0;
            lblTitle.Text = "New Item Details";
            txtBarCode.Focus();
        }

        private void radReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (isFromTabChanged)
            {
                isFromTabChanged = false;
                return;
            }
            tabControl1.SelectedIndex = 1;
            lblTitle.Text = "Replace/Return";
            txtBarCode.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BringToFront();
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void dgvReplaceReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (dgvReplaceReturn.Columns[e.ColumnIndex].Name == "ColReplaceDelete")
            {
                DialogResult d = MessageBox.Show("Are you sure want to delete ? ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    dgvReplaceReturn.Rows.RemoveAt(e.RowIndex);
                    dgvReplaceReturn.EndEdit();
                     CalculateGrandTotal();
                    picProduct.Image = null;
                }
            }
            else
            {
                int _SubPID = Convert.ToInt32(dgvReplaceReturn.Rows[e.RowIndex].Cells["SubProductID"].Value);
                picProduct.Image = GetProductPhoto(Convert.ToInt32(_SubPID));
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                isFromTabChanged = true;
                radNewItem.Checked = true;

                txtTotalItems.Text = "New Items Count :" + dgvProductDetails.Rows.Count.ToString();

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                isFromTabChanged = true;
                radReplace.Checked = true;
                txtTotalItems.Text = "Replace/Return Items :" + dgvReplaceReturn.Rows.Count.ToString();
            }
        }

        private void txtSalesMan_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSalesMan_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, System.Drawing.Color.White);
        }

        private void txtDiscount_DoubleClick(object sender, EventArgs e)
        {
            Other_Forms.frmDiscountLogin frmDiscountLogin = new Other_Forms.frmDiscountLogin();
            frmDiscountLogin.ShowDialog();
            if (Other_Forms.frmDiscountLogin.IsValidAdmin)
            {
                txtDiscount.ReadOnly = false;
            }
            else
            {
                txtDiscount.ReadOnly = true;
            }
        }

        private void dgvReplaceReturn_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                _OldRateStartValue = Convert.ToDecimal(dgvReplaceReturn.Rows[e.RowIndex].Cells["ReplaceRate"].Value);
            }
            catch (Exception)
            {
            }
        }

        private void dgvReplaceReturn_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dgvProductDetails.Columns[e.ColumnIndex].Name == "Rate" ||
            if (dgvReplaceReturn.Columns[e.ColumnIndex].Name == "ReplaceRate")
            {
                decimal QTY = Convert.ToDecimal(dgvReplaceReturn.Rows[e.RowIndex].Cells["ReplaceQTY"].Value);

                decimal Rate = Convert.ToDecimal(dgvReplaceReturn.Rows[e.RowIndex].Cells["ReplaceRate"].Value);
                decimal Total = QTY * Rate;

                dgvReplaceReturn.Rows[e.RowIndex].Cells["ColReplaceTotal"].Value = Total;

                CalculateGrandTotal();
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            txtOldBillAmount.Enabled = true;
            txtOldBillAmount.ReadOnly = false;
        }

        private void dgvReplaceReturn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvReplaceReturn.Columns["SubProductID"].Visible = false;
        }
    }
}