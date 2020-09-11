using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Web.UI;
using System.Windows.Forms;
using System.Windows.Media;
using CoreApp;
using IMS_Client_2.Barcode;
using IMS_Client_2.Other_Forms;

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
        bool isSalesManbind = false;
        decimal _StartValue = 0;

        public static bool _Is_SalesManIDMandat = false;
        public static bool _Is_CustomerMobileMandat = false;

        DataTable dtItemDetails = new DataTable();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void GetLastSalesMan()
        {
            string strQ = "SELECT TOP(1) SalesMan FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails WITH(NOLOCK) WHERE ShopeID=" + cmbShop.SelectedValue + " ORDER BY ID DESC";

            int SalesManID = ObjDAL.ExecuteScalarInt(strQ);
            if (SalesManID == 0)
            {
                txtEmpID.Text = "0";
            }
            else
            {
                txtEmpID.Text = SalesManID.ToString();
                string strQ2 = "SELECT Name FROM " + clsUtility.DBName + ".dbo.EmployeeDetails WITH(NOLOCK) WHERE EmpID=" + SalesManID;
                object name = ObjDAL.ExecuteScalar(strQ2);
                if (name != null)
                {
                    isSalesManbind = true;
                    txtSalesMan.Text = name.ToString();
                }
            }
        }
        private void Sales_Invoice_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;
            btnSaveData.BackgroundImage = B_Leave;

            BindStoreDetails();
            //  BindSalesManDetails();
            // GenerateInvoiceNumber();
            InitItemTable();
            dtpSalesDate.Value = DateTime.Now;
            txtBarCode.Focus();
            dtpSalesDate.MaxDate = DateTime.Now;
            BindMandetorySettings();
            GetLastSalesMan();
            if (!IsReplaceReturnMode)
            {
                //radNewItem.Visible = false;
                //radReplace.Visible = false;

                label16.Enabled = false;
                //txtOldBillAmount.Enabled = false;
            }
            else
            {
                radReplace.Checked = true;
                label16.Enabled = true;
                // txtOldBillAmount.Enabled = true;
            }
        }
        private void BindMandetorySettings()
        {
            string strQ = " SELECT SalesManIDMandatory, CustMobileMandatory FROM " + clsUtility.DBName + ".[dbo].[DefaultStoreSetting] WITH(NOLOCK)";

            DataTable dtmandat = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtmandat))
            {
                _Is_SalesManIDMandat = dtmandat.Rows[0]["SalesManIDMandatory"] == DBNull.Value ? false : Convert.ToBoolean(dtmandat.Rows[0]["SalesManIDMandatory"]);
                _Is_CustomerMobileMandat = dtmandat.Rows[0]["CustMobileMandatory"] == DBNull.Value ? false : Convert.ToBoolean(dtmandat.Rows[0]["CustMobileMandatory"]);
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
            dtItemDetails.Columns.Add("QTY", typeof(int));
            dtItemDetails.Columns.Add("Rate");

            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");
            dtItemDetails.Columns.Add("Total", typeof(decimal));
            dtItemDetails.Columns.Add("Delete");
            dtItemDetails.Columns.Add("SubProductID");
            dtItemDetails.Columns.Add("IsReplaceReturn", typeof(Boolean));
        }
        private void AddRowToItemDetails(string productID, string name, string qty, string rate, string total,
            string BarCode, string SizeID, string Size, string ColorID, string Color, string subProductID, bool isReplcereturn)
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
            dRow["IsReplaceReturn"] = isReplcereturn;

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
            //ObjDAL.SetStoreProcedureData("InvoiceID", SqlDbType.Int, OldInvoiceID);
            //ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.NVarChar, _BarCoeNumber);

            //DataSet dataSet = ObjDAL.ExecuteStoreProcedure_Get("spr_GetReplaceReturnDetails");
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    drow["OIRate"] = dataSet.Tables[0].Rows[0]["Rate"].ToString();

            //    decimal odlRate = Convert.ToDecimal(dataSet.Tables[0].Rows[0]["Rate"]);
            //    decimal adAmount = CurrentRate - odlRate;
            //    drow["Adj_Amount"] = adAmount.ToString();
            //}
        }

        private bool IsItemExist(string barCode, bool isReturn)
        {
            if (isReturn)
            {
                DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "' AND IsReplaceReturn=1");
                if (dRow.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "' AND IsReplaceReturn=0");
                if (dRow.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void UpdateQTYByOne(string barCode, decimal rate, bool isReplacereturn)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["QTY"]) + 1;

            if (isReplacereturn)
            {
                if (CheckReplaceProductQTY(barCode, Convert.ToDecimal(NewQTY)))
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
                    clsUtility.ShowInfoMessage("No more QTY found in the given invoice No : " + txtInvoiceNumber.Text);
                }
            }
            else
            {
                if (CheckProductQTY(barCode, Convert.ToDecimal(NewQTY), isReplacereturn))
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
                    clsUtility.ShowInfoMessage("No QTY avaiable for the given Product.");
                }
            }
        }

        private string GenerateInvoiceNumber()
        {
            string InvoiceNumber = "";
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].SequenceInvoice");
            InvoiceNumber = "INV-" + LastID;
            return InvoiceNumber;
        }
        bool isfromdefaul = false;
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
                clsUtility.ShowInfoMessage("Please select the default shop for this client from Setting Window.");
                Settings.frmOtherSetting otherSetting = new Settings.frmOtherSetting();
                otherSetting.ShowDialog();
                isfromdefaul = true;
                this.Close();
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
                if (isSalesManbind)
                {
                    isSalesManbind = false;
                    return;
                }
                DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Empid,EmployeeCode FROM " + clsUtility.DBName + ".dbo.EmployeeDetails WITH(NOLOCK) WHERE [EmployeeCode] Like '" + txtSalesMan.Text + "%'");
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
                    if (ObjUtil.GetDataPopup() != null)
                    {
                        ObjUtil.GetDataPopup().CellClick += Sales_Invoice_CellClick1;
                        ObjUtil.GetDataPopup().KeyDown += Sales_Invoice_KeyDown1;
                    }
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception ex)
            {
            }
            txtSalesMan.Focus();
        }

        private void Sales_Invoice_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (_Is_CustomerMobileMandat)
                {
                    if (txtCustomerMobile.Text.Trim().Length == 0)
                    {
                        txtCustomerMobile.Focus();
                    }
                }
                else
                {
                    txtGrandTotal.Focus();
                }
            }
        }

        private void Sales_Invoice_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerMobile.Text.Trim().Length == 0)
                {
                    txtCustomerMobile.Focus();
                }
            }
            else
            {
                txtGrandTotal.Focus();
            }

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

            #region OLD Code
            //DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".dbo.tblProductWiseModelNo WHERE SubProductID=" + SubProductID);
            //if (ObjUtil.ValidateTable(dt))
            //{
            //    if (dt.Rows[0]["Photo"] != DBNull.Value)
            //    {
            //        DataTable dtImagePath = ObjDAL.ExecuteSelectStatement("  SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) ");
            //        if (ObjUtil.ValidateTable(dtImagePath))
            //        {
            //            if (dtImagePath.Rows[0]["ImagePath"] != DBNull.Value)
            //            {
            //                string ImgPath = dtImagePath.Rows[0]["ImagePath"].ToString();
            //                string extension = dtImagePath.Rows[0]["Extension"].ToString();

            //                string imgFile = ImgPath + "//" + dt.Rows[0]["Photo"].ToString() + extension;
            //                if (File.Exists(imgFile))
            //                {
            //                    imgProduct = Image.FromFile(imgFile);
            //                }
            //                else
            //                {
            //                    imgProduct = null;
            //                }
            //            }
            //            else
            //            {
            //                clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
            //            }
            //        }
            //    }
            //}
            #endregion

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
                    clsUtility.ShowInfoMessage("Invalid BarCode Entry. Please check the bar Code no.", clsUtility.strProjectTitle);
                    txtBarCode.Clear();
                }
            }
        }

        private bool CheckProductQTY(string _BarCoeNumber, decimal CurQTY, bool isReplaceReturn)
        {
            // dont check the shop if item is returning, item can be return from any shope
            if (!isReplaceReturn)
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

            return true;
        }
        private bool CheckReplaceProductQTY(string _BarCoeNumber, decimal CurQTY)
        {
            if (frmReplaceReturnPopup.strInvoiceID == "0")
            {
                // we dont know the invoice so keep adding whatever customer is giving in return.
                return true;
            }
            else
            {
                int TotalQTY = 0;
                ObjDAL.SetStoreProcedureData("InvoiceID", SqlDbType.Int, frmReplaceReturnPopup.strInvoiceID);
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
        private void GetItemDetailsByProductID(string _BarCodeValue, bool isReplaceReturn)
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.GetProductDetailsByBarCode " + cmbShop.SelectedValue + ", " + _BarCodeValue + "," + isReplaceReturn);
            if (ObjUtil.ValidateTable(dt))
            {
                string pID = dt.Rows[0]["ProductID"].ToString();
                string name = dt.Rows[0]["ProductName"].ToString();
                string rate = "";
                if (isReplaceReturn)
                {
                    rate = "-" + dt.Rows[0]["Rate"].ToString();
                }
                else
                {
                    rate = dt.Rows[0]["Rate"].ToString();
                }

                string barCode = dt.Rows[0]["BarcodeNo"].ToString();
                string qty = "1";
                string SizeID = dt.Rows[0]["SizeID"].ToString();
                string Size = dt.Rows[0]["Size"].ToString();
                string ColorID = dt.Rows[0]["ColorID"].ToString();
                string ColorName = dt.Rows[0]["ColorName"].ToString();
                decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);
                string subProductID = dt.Rows[0]["SubProductID"].ToString();

                if (CheckProductQTY(barCode, Convert.ToDecimal(qty), isReplaceReturn))
                {
                    // if Item already there in the grid, then just increase the QTY
                    //may be invoice ID is not given and returning, but we will get to know by F7
                    if (IsItemExist(barCode, isReplaceReturn))
                    {
                        UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate), isReplaceReturn);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(subProductID));
                    }
                    else
                    {
                        AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, subProductID, isReplaceReturn);
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
                    clsUtility.ShowInfoMessage("No QTY avaiable for the Product : " + txtBarCode.Text);
                    txtBarCode.Clear();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue);
                txtBarCode.Clear();
            }
        }

        private void CountQTY()
        {
            object ob = dtItemDetails.Compute("SUM(QTY)", "Total > 0");
            txtTotalItems.Text = "Total QTY :" + ob.ToString();
        }
        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);

            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["ColoriD"].Visible = false;
            dgvProductDetails.Columns["SizeiD"].Visible = false;
            dgvProductDetails.Columns["SubProductID"].Visible = false;

            ObjUtil.SetDataGridProperty(dgvProductDetails);
            SetRowColor();
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
                if (QTY <= 0)
                {
                    clsUtility.ShowInfoMessage("The QTY can not be 0 OR negative. Default 1 QTY will be set.", clsUtility.strProjectTitle);
                    dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value = "1";
                    QTY = 1;
                }
                decimal Rate = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                decimal Total = QTY * Rate;
                string _barNo = dgvProductDetails.Rows[e.RowIndex].Cells["Barcodeno"].Value.ToString();
                bool _isRepLaceR = Convert.ToBoolean(dgvProductDetails.Rows[e.RowIndex].Cells["IsReplaceReturn"].Value);
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

                        clsUtility.ShowInfoMessage("QTY : " + QTY + " NOT avaiable for the Product : " + dgvProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value);
                    }
                }
                else
                {
                    if (CheckProductQTY(_barNo, Convert.ToDecimal(QTY), _isRepLaceR))
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["Total"].Value = Total.ToString();
                        CalculateGrandTotal();
                    }
                    else
                    {
                        dgvProductDetails.Rows[e.RowIndex].Cells["QTY"].Value = _StartValue;
                        CalculateGrandTotal();

                        clsUtility.ShowInfoMessage("QTY : " + QTY + " NOT avaiable for the Product : " + dgvProductDetails.Rows[e.RowIndex].Cells["ProductName"].Value);
                    }
                }
            }
        }
        private void CalculateGrandTotal()
        {
            CountQTY();

            decimal NewBillAmount = 0.0M;
            decimal OldBillAmount = 0.0M;
            decimal Discount = 0.0M;
            decimal Deliverycharges = 0.0M;
            decimal GrandTotal = 0.0M;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                NewBillAmount += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["Total"].Value);
            }

            //txtOldBillAmount.Text = Math.Round(OldBillAmount, 2).ToString();
            txtNewBillAmount.Text = Math.Round(NewBillAmount, 2).ToString();

            decimal MainSubTotal = NewBillAmount - OldBillAmount;

            txtSubTotal.Text = Math.Round(MainSubTotal, 2).ToString();

            Discount = txtDiscount.Text.Length > 0 ? Convert.ToDecimal(txtDiscount.Text) : 0;
            Deliverycharges = txtDeliveryCharges.Text.Length > 0 ? Convert.ToDecimal(txtDeliveryCharges.Text) : 0;

            GrandTotal = (MainSubTotal - Discount) + Deliverycharges;
            //GrandTotal = MainSubTotal - (MainSubTotal * Discount * 0.01M) + Deliverycharges;
            txtGrandTotal.Text = Math.Round(GrandTotal, 2).ToString();
        }

        private bool ValidateGrandTotal()
        {
            decimal FinalTotal = Convert.ToDecimal(txtCashTendered.Text) + Convert.ToDecimal(txtCredit.Text);

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
            frmPayment.strAmountEntered = "0";
            IsReplaceReturnMode = false;
            Sales.frmReplaceReturnPopup.strReplaceInvoiceNumber = "0";
            Sales.frmReplaceReturnPopup.IsReplaceInvoice = false;
            //txtOldBillAmount.ReadOnly = false;
            txtNewBillAmount.Text = "0";
            Other_Forms.frmDiscountLogin.IsValidAdmin = false;
            Other_Forms.frmDiscountLogin.VoucherNo = string.Empty;
            txtDiscount.ReadOnly = true;
            txtCashTendered.Clear();
            txtCredit.Clear();

            // txtOldBillAmount.Clear();
            txtNewBillAmount.Clear();
            Other_Forms.frmPayment.ResetData();

            //lblPMode.Text = "";
            txtCustomerID.Clear();
            txtCustomerMobile.Clear();
            dtItemDetails.Clear();

            txtProductID.Clear();
            //txtEmpID.Clear();

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

            //  txtSalesMan.Clear();
            txtColorID.Clear();
            txtSizeID.Clear();

            NewSaleMode();
            txtChange.Clear();

            txtCashTendered.Text = "0";
            txtCredit.Text = "0";
            txtChange.Text = "0";

            Sales.frmReplaceReturnPopup.strReplaceInvoiceNumber = "";
            Sales.frmReplaceReturnPopup.IsReplaceInvoice = false;
            Sales.frmReplaceReturnPopup.strInvoiceID = "0";
        }
        private bool SalesValidation(bool isSave)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtEmpID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Sales Man Employee Code/ID.");
                    txtSalesMan.Focus();
                    return false;
                }
            }

            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.");
                    txtCustomerMobile.Focus();
                    return false;
                }
            }

            if (Convert.ToDecimal(txtGrandTotal.Text) > 0)
            {
                if (IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType.Count == 0)
                {
                    clsUtility.ShowInfoMessage("Please select payment mode.");
                    return false;
                }
            }

            if (dgvProductDetails.Rows.Count==0)
            {
                clsUtility.ShowInfoMessage("Please add items to sale.", clsUtility.strProjectTitle);
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
                //if (isSave == false)
                //{
                //    if (!ValidateGrandTotal())
                //    {
                //        clsUtility.ShowInfoMessage("Cash and Credit amount must be equal to Grand Total.", clsUtility.strProjectTitle);
                //        return false;
                //    }
                //}
            }
            return true;
        }
        private void InsertIntoCreditCosing()
        {
            // check if any entry is there
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

                int MasterCashClosingID = frmHome.Home_MasterCashClosingID;

                int count = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName +
                    ".[dbo].[tblCreditClosing] WITH(NOLOCK) WHERE MasterCashClosingID=" + MasterCashClosingID +
                    " AND Type='" + IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType[i].ToString() + "'");

                if (count == 0)  // if NOT found for today
                {
                    ObjDAL.SetColumnData("MasterCashClosingID", SqlDbType.Int, MasterCashClosingID);
                    ObjDAL.SetColumnData("Type", SqlDbType.NVarChar, IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType[i].ToString());
                    ObjDAL.SetColumnData("Count", SqlDbType.Int, 1);
                    ObjDAL.SetColumnData("Value", SqlDbType.Decimal, amount);
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);

                    ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblCreditClosing]", false);
                }
                else
                {
                    // update the tblCreditClosing, Add count and amount to the exsting value
                    string strUpdate = "  UPDATE " + clsUtility.DBName + ".[dbo].[tblCreditClosing] SET Count=Count+1 , Value=value+" + amount +
                                    ", UpdatedOn=getdate(), UpdatedBy=" + clsUtility.LoginID + " WHERE MasterCashClosingID=" + MasterCashClosingID + " AND Type='" + IMS_Client_2.Other_Forms.frmPayment.lstPaymnetType[i].ToString() + "'";
                    ObjDAL.ExecuteNonQuery(strUpdate);
                }
            }
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
                    ObjDAL.SetColumnData("Amount", SqlDbType.Decimal, Convert.ToDecimal(txtGrandTotal.Text));
                    ObjDAL.SetColumnData("SalesInvoiceID", SqlDbType.Int, SalesInvoiceID);
                    ObjDAL.SetColumnData("PaymentNumber", SqlDbType.NVarChar, number);

                    ObjDAL.InsertData(clsUtility.DBName + ".dbo.tblSalesPayment", false);
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private int DoNewSales()
        {
            dgvProductDetails.EndEdit();

            string InvoiceDateTime = dtpSalesDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

            #region SalesInvoiceDetails
            ObjDAL.SetColumnData("InvoiceNumber", SqlDbType.NVarChar, GenerateInvoiceNumber());
            ObjDAL.SetColumnData("InvoiceDate", SqlDbType.DateTime, InvoiceDateTime);
            ObjDAL.SetColumnData("SubTotal", SqlDbType.Decimal, txtSubTotal.Text);
            ObjDAL.SetColumnData("Discount", SqlDbType.Decimal, txtDiscount.Text);
            ObjDAL.SetColumnData("Tax", SqlDbType.Decimal, txtDeliveryCharges.Text);
            ObjDAL.SetColumnData("GrandTotal", SqlDbType.Decimal, txtGrandTotal.Text);

            if (Other_Forms.frmDiscountLogin.IsValidAdmin)
                ObjDAL.SetColumnData("VoucherNo", SqlDbType.VarChar, frmDiscountLogin.VoucherNo);

            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
            ObjDAL.SetColumnData("CustomerID", SqlDbType.Int, txtCustomerID.Text.Trim().Length > 0 ? Convert.ToInt32(txtCustomerID.Text) : 0);
            ObjDAL.SetColumnData("SalesMan", SqlDbType.Int, txtEmpID.Text.Trim().Length > 0 ? Convert.ToInt32(txtEmpID.Text) : 0);
            
            //if (txtCustomerID.Text.Trim().Length == 0)
            //{
            //    ObjDAL.SetColumnData("CustomerID", SqlDbType.Int, 0);
            //}
            //else
            //{
            //    ObjDAL.SetColumnData("CustomerID", SqlDbType.Int, txtCustomerID.Text);
            //}
            //if (txtEmpID.Text.Trim().Length == 0)
            //{
            //    ObjDAL.SetColumnData("SalesMan", SqlDbType.Int, 0);
            //}
            //else
            //{
            //    ObjDAL.SetColumnData("SalesMan", SqlDbType.Int, txtEmpID.Text);
            //}

            ObjDAL.SetColumnData("ShopeID", SqlDbType.Int, cmbShop.SelectedValue.ToString());
            ObjDAL.SetColumnData("CashTendered", SqlDbType.Decimal, txtCashTendered.Text);
            ObjDAL.SetColumnData("Change", SqlDbType.Decimal, txtChange.Text);

            int InvoiceID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.SalesInvoiceDetails", true);
            if (InvoiceID == -1)
            {
                ObjDAL.ResetData();
                return InvoiceID;
            }
            #endregion
            InsertPayment(InvoiceID);
            decimal OldBillAmount = 0;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                string Total = dgvProductDetails.Rows[i].Cells["Total"].Value.ToString();
                string ProductID = dgvProductDetails.Rows[i].Cells["ProductID"].Value.ToString();
                string QTY = dgvProductDetails.Rows[i].Cells["QTY"].Value.ToString();
                string Rate = dgvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                string ColorID = dgvProductDetails.Rows[i].Cells["ColorID"].Value.ToString();
                string SizeID = dgvProductDetails.Rows[i].Cells["SizeID"].Value.ToString();
                string subProductID = dgvProductDetails.Rows[i].Cells["SubProductID"].Value.ToString();
                string BarCode = dgvProductDetails.Rows[i].Cells["BarCodeNo"].Value.ToString();
                bool IsReplaceReturn = Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["IsReplaceReturn"].Value);

                ObjDAL.SetColumnData("InvoiceID", SqlDbType.Int, InvoiceID);
                ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                ObjDAL.SetColumnData("QTY", SqlDbType.Decimal, QTY);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, Rate);
                ObjDAL.SetColumnData("ColorID", SqlDbType.Int, ColorID);
                ObjDAL.SetColumnData("SizeID", SqlDbType.Int, SizeID);

                ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, subProductID);
                ObjDAL.SetColumnData("BarCode", SqlDbType.NVarChar, BarCode);
                ObjDAL.SetColumnData("IsReplaceReturn", SqlDbType.Bit, Convert.ToBoolean(IsReplaceReturn));

                int result = ObjDAL.InsertData(clsUtility.DBName + ".dbo.SalesDetails", false);
                if (result == -1)
                {
                    ObjDAL.ResetData();
                    return -1;
                }

                if (IsReplaceReturn)// if replace return, Add the QTY
                {
                    OldBillAmount = OldBillAmount + Convert.ToDecimal(Rate);

                    ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                                       "SET QTY=QTY+" + QTY + ", UpdatedOn=GETDATE(), UpdatedBy=" + clsUtility.LoginID + " WHERE ProductID=" + ProductID + " AND StoreID=" + cmbShop.SelectedValue.ToString() + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID + " AND SubProductID=" + subProductID);
                }
                else
                {
                    ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                                       "SET QTY=QTY-" + QTY + ", UpdatedOn=GETDATE(), UpdatedBy=" + clsUtility.LoginID + " WHERE ProductID=" + ProductID + " AND StoreID=" + cmbShop.SelectedValue.ToString() + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID + " AND SubProductID=" + subProductID);
                }
            }
            // data will be saved to tblCreditClosing, only if he is paying something.
            if (Convert.ToDecimal(txtGrandTotal.Text) >= 0)
            {
                InsertIntoCreditCosing();
            }
            // if Old Bill amount is greater than zero means customer has returned something.
            if (Math.Abs(OldBillAmount) > 0)
            {
                int MasterCashClosingID = frmHome.Home_MasterCashClosingID;

                int count = ObjDAL.ExecuteScalarInt("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblCashReturn] WITH(NOLOCK) WHERE MasterCashClosingID=" + MasterCashClosingID);
                if (count == 0)  // if NOT found for today
                {
                    ObjDAL.SetColumnData("MasterCashClosingID", SqlDbType.Int, MasterCashClosingID);
                    ObjDAL.SetColumnData("Value", SqlDbType.Decimal, Math.Abs(OldBillAmount));
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.InsertData(clsUtility.DBName + ".[dbo].[tblCashReturn]", false);
                }
                else
                {
                    // update the tblCashReturn, Add value the exsting value

                    string strUpdate = "  UPDATE " + clsUtility.DBName + ".[dbo].[tblCashReturn] SET Value=value+" + Math.Abs(OldBillAmount) +
                                          " , UpdatedBy=" + clsUtility.LoginID + ", UpdatedOn=GETDATE() WHERE MasterCashClosingID=" + MasterCashClosingID;

                    ObjDAL.ExecuteNonQuery(strUpdate);
                }
            }
            // update the crossponding old invoice ID
            ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".[dbo].[SalesInvoiceDetails] set  OldInvoiceID=" + frmReplaceReturnPopup.strInvoiceID + " where id=" + InvoiceID);
            return InvoiceID;
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
                        clsUtility.ShowInfoMessage("Printer Not Configured for Invoice Print. Please configure the printer from Setting menu.");
                        return;
                    }

                    bool printstatus = clsUtility.ShowQuestionMessage("Do you want to print this sales invoice ? ");
                    if (printstatus == false)
                    {
                        return;
                    }
                }
                if (SalesValidation(button.Name == "btnSaveData"))
                {
                    int NewInvoiceID = DoNewSales();

                    ClearAll();

                    if (button.Name == "btnSaveData")
                    {
                        if (NewInvoiceID > 0)
                        {
                            clsUtility.ShowInfoMessage("Sale invoice has been generated successfully.");
                        }
                        //this.Close();
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
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to cancel this sales order?");
            if (result)
            {
                ClearAll();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateGrandTotal();
        }

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

        private void BindSalesManDetails()
        {
            string str = " SELECT e1.Name,e1.EmpID,e1.EmployeeCode FROM " + clsUtility.DBName + ".dbo.EmployeeDetails e1 JOIN  " +
                         " " + clsUtility.DBName + ".dbo.UserManagement u1 on e1.EmpID = u1.EmployeeID" +
                         " WHERE u1.UserID = " + clsUtility.LoginID;

            DataTable dtEmployeeDetails = ObjDAL.ExecuteSelectStatement(str);
            if (ObjUtil.ValidateTable(dtEmployeeDetails))
            {
                isSalesManbind = true;
                txtEmpID.Text = dtEmployeeDetails.Rows[0]["EmpID"].ToString();
                txtSalesMan.Text = dtEmployeeDetails.Rows[0]["EmployeeCode"].ToString();
                txtSalesMan.Enabled = true;
            }
        }
        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtBarCode.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Barcode number.");
                    return;
                }
                if (IsReplaceReturnMode && radReplace.Checked)
                {
                    // No Invoice ID is given when return/replace
                    if (frmReplaceReturnPopup.strInvoiceID == "0")
                    {
                        // get the data from stock 
                        GetItemDetailsByProductID(txtBarCode.Text, true);
                    }
                    else
                    {
                        // get the data froms sales.
                        GetDataFromSales(txtBarCode.Text);
                    }
                }
                else
                {   //txtProductName contains barcode.
                    GetItemDetailsByProductID(txtBarCode.Text, false);
                }
            }
        }

        private void GetDataFromSales(string barcode)
        {
            // get the sales data from old sales invoice ID, get from Previous bill which is enwly purchase.
            string strQ = "select s1.InvoiceID,p1.ProductName,s1.ProductID,s1.subproductID,s1.QTY,s1.Rate,s1.ColorID,s1.SizeID,s1.BarCode,c1.ColorName as Color,sm.Size from SalesDetails s1 " +
                        " join ProductMaster  p1 on s1.ProductID = p1.ProductID " +
                        " join ColorMaster c1 on s1.ColorID = c1.ColorID" +
                        " join SizeMaster sm on s1.SizeID = sm.SizeID" +
                        " where s1.BarCode = '" + barcode + "' AND s1.IsReplaceReturn=0 AND s1.InvoiceID=" + frmReplaceReturnPopup.strInvoiceID;

            DataTable dtSalesDetails = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dtSalesDetails))
            {
                try
                {
                    string pID = dtSalesDetails.Rows[0]["ProductID"].ToString();
                    string name = dtSalesDetails.Rows[0]["ProductName"].ToString();

                    string rate = "-" + dtSalesDetails.Rows[0]["Rate"].ToString();
                    string barCode = dtSalesDetails.Rows[0]["Barcode"].ToString();
                    string qty = "1";
                    string SizeID = dtSalesDetails.Rows[0]["SizeID"].ToString();
                    string Size = dtSalesDetails.Rows[0]["Size"].ToString();
                    string ColorID = dtSalesDetails.Rows[0]["ColorID"].ToString();
                    string ColorName = dtSalesDetails.Rows[0]["Color"].ToString();
                    string SubProductID = dtSalesDetails.Rows[0]["SubProductID"].ToString();

                    decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);

                    // if Item already there in the grid, then just increase the QTY
                    if (IsItemExist(barCode, true))
                    {
                        UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate), true);

                        picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));
                    }
                    else
                    {
                        //  AddRowToReplaceItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName, SubProductID);

                        AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barcode, SizeID, Size, ColorID, ColorName, SubProductID, true);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(SubProductID));
                    }
                    txtProductID.Clear();
                    txtBarCode.Clear();

                    CalculateGrandTotal();

                    txtBarCode.Focus();
                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.ToString());
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Couldn't find barcode number : " + barcode + " in Invoice No : " + txtInvoiceNumber.Text);
                txtBarCode.Clear();
            }
        }
        bool isOldInvoiceFlag = false;
        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOldInvoiceFlag)
                {
                    isOldInvoiceFlag = false;
                    return;
                }
                if (txtCustomerMobile.Text.Trim().Length > 0)
                {
                    string query = "SELECT CustomerID,[Name],PhoneNo FROM " + clsUtility.DBName + ".dbo.CustomerMaster WITH(NOLOCK) WHERE PhoneNo LIKE '%" + txtCustomerMobile.Text + "%'";
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
                        if (ObjUtil.GetDataPopup() != null)
                        {
                            ObjUtil.GetDataPopup().CellClick += Sales_Invoice_CellClick;
                            ObjUtil.GetDataPopup().KeyDown += Sales_Invoice_KeyDown;
                        }
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

        private void Sales_Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (_Is_SalesManIDMandat)
                {
                    if (txtSalesMan.Text.Trim().Length == 0)
                    {
                        txtSalesMan.Focus();
                    }
                }
                else
                {
                    txtGrandTotal.Focus();
                }
            }
        }

        private void Sales_Invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtSalesMan.Text.Trim().Length == 0)
                {
                    txtSalesMan.Focus();
                }
            }
            else
            {
                txtGrandTotal.Focus();
            }
        }

        private void picCash_Click(object sender, EventArgs e)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtEmpID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Sales Man Employee Code/ID.");
                    txtSalesMan.Focus();
                    return;
                }
            }
            else
            {
                txtGrandTotal.Focus();
            }

            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.");
                    txtCustomerMobile.Focus();
                    return;
                }
            }
            else
            {
            }

            //lblPMode.Text = "Cash";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.txtPaymentAutoID.Enabled = false;

            frmPayment.lblPaymentMode.Text = "Cash";
            frmPayment.picPaymentMode.Image = picCash.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCashTendered.Text = Other_Forms.frmPayment.GetTotalCash().ToString();
            txtGrandTotal.Focus();
            if (frmPayment.strAmountEntered != "0")
            {
                CalculateChange();
            }
        }
        private void picKnet_Click(object sender, EventArgs e)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtEmpID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Sales Man Employee Code/ID.", clsUtility.strProjectTitle);
                    txtSalesMan.Focus();
                    return;
                }
            }
            else
            {
            }

            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.");
                    txtCustomerMobile.Focus();
                    return;
                }
            }
            else
            {
            }
            //lblPMode.Text = "K Net";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "K Net";
            frmPayment.picPaymentMode.Image = picKnet.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCashTendered.Text = Other_Forms.frmPayment.GetTotalCash().ToString();

            txtGrandTotal.Focus();

            if (frmPayment.strAmountEntered != "0")
            {
                CalculateChange();
            }
        }
        private void CalculateChange()
        {
            decimal GrandTotal = Convert.ToDecimal(txtGrandTotal.Text);
            decimal TotalAmountGiven = Convert.ToDecimal(txtCashTendered.Text) + Convert.ToDecimal(txtCredit.Text);

            decimal Change = TotalAmountGiven - GrandTotal;
            txtChange.Text = Change.ToString();
        }

        private void picVisa_Click(object sender, EventArgs e)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtEmpID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Sales Man Employee Code/ID.");
                    txtSalesMan.Focus();
                    return;
                }
            }
            else
            {
            }

            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.");
                    txtCustomerMobile.Focus();
                    return;
                }
            }
            else
            {
            }
            //lblPMode.Text = "Visa";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "Visa";
            frmPayment.picPaymentMode.Image = picVisa.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCashTendered.Text = Other_Forms.frmPayment.GetTotalCash().ToString();

            txtGrandTotal.Focus();

            if (frmPayment.strAmountEntered != "0")
            {
                CalculateChange();
            }
        }
        private void PicMaster_Click(object sender, EventArgs e)
        {
            if (_Is_SalesManIDMandat)
            {
                if (txtEmpID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Sales Man Employee Code/ID.", clsUtility.strProjectTitle);
                    txtSalesMan.Focus();
                    return;
                }
            }
            else
            {
            }

            if (_Is_CustomerMobileMandat)
            {
                if (txtCustomerID.Text.Trim().Length == 0)
                {
                    clsUtility.ShowInfoMessage("Please Enter Customer Mobile No.");
                    txtCustomerMobile.Focus();
                    return;
                }
            }
            else
            {
            }

            //lblPMode.Text = "Master Card";
            Other_Forms.frmPayment frmPayment = new Other_Forms.frmPayment();

            frmPayment.lblPaymentMode.Text = "Master Card";
            frmPayment.picPaymentMode.Image = PicMaster.Image;
            frmPayment.ShowDialog();

            txtCredit.Text = Other_Forms.frmPayment.GetTotalCreditAmount().ToString();
            txtCashTendered.Text = Other_Forms.frmPayment.GetTotalCash().ToString();

            txtGrandTotal.Focus();

            if (frmPayment.strAmountEntered != "0")
            {
                CalculateChange();
            }
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
                clsUtility.ShowInfoMessage("Enter Only Numbers...");
            }
        }

        private void radNewItem_CheckedChanged(object sender, EventArgs e)
        {
            if (isFromTabChanged)
            {
                isFromTabChanged = false;
                return;
            }
            txtBarCode.Focus();
        }

        private void radReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (isFromTabChanged)
            {
                isFromTabChanged = false;
                return;
            }
            txtBarCode.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BringToFront();
            timer1.Stop();
            timer1.Enabled = false;
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

        private void label16_Click(object sender, EventArgs e)
        {
            //txtOldBillAmount.Enabled = true;
            //txtOldBillAmount.ReadOnly = false;
        }

        private void f1CashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picCash_Click(null, null);
        }

        private void f2KnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picKnet_Click(null, null);
        }

        private void f3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picVisa_Click(null, null);
        }

        private void f4MasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMaster_Click(null, null);
        }

        private void ReplaceMode()
        {
            IsReplaceReturnMode = true;
            picSaleMode.Image = Properties.Resources.Sales_Return;

            label19.Text = "Return/Replace";
            radReplace.Checked = true;
            label19.ForeColor = System.Drawing.Color.DarkRed;

        }
        private void NewSaleMode()
        {
            IsReplaceReturnMode = false;
            picSaleMode.Image = Properties.Resources.news;
            label19.Text = "New Sale";
            label19.ForeColor = System.Drawing.Color.FromArgb(0, 64, 0);
            radNewItem.Checked = true;
        }
        private void f7ReplaceReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoF7();
        }
        private void DoF7()
        {
            if (IsReplaceReturnMode == false)
            {
                ReplaceMode();
                // opening for the first time.
                if (Sales.frmReplaceReturnPopup.strReplaceInvoiceNumber.Trim().Length == 0)
                {
                    Sales.frmReplaceReturnPopup obj = new Sales.frmReplaceReturnPopup();
                    obj.ShowDialog();

                    if (Sales.frmReplaceReturnPopup.strInvoiceID != "0")
                    {
                        BindOldInvoiceCustoemrDetails(Sales.frmReplaceReturnPopup.strInvoiceID);
                    }
                }

                if (Sales.frmReplaceReturnPopup.IsReplaceInvoice == false)
                {
                    NewSaleMode();
                }
                else
                {
                    txtInvoiceNumber.Text = Sales.frmReplaceReturnPopup.strReplaceInvoiceNumber;
                }
            }
            else
            {
                NewSaleMode();
            }
        }

        private void BindOldInvoiceCustoemrDetails(string strInvoiceID)
        {
            string strQ = "select c1.CustomerID, c1.Name,c1.PhoneNo from " + clsUtility.DBName + ".dbo.SalesInvoiceDetails  s1 inner join " +
                         " " + clsUtility.DBName + ".dbo.CustomerMaster c1 on s1.CustomerID = c1.CustomerID " +
                         " where Id = " + strInvoiceID;

            DataTable dt = ObjDAL.ExecuteSelectStatement(strQ);
            if (ObjUtil.ValidateTable(dt))
            {
                isOldInvoiceFlag = true;
                txtCustomerID.Text = dt.Rows[0]["CustomerID"].ToString();
                txtCustomerName.Text = dt.Rows[0]["Name"].ToString();
                txtCustomerMobile.Text = dt.Rows[0]["PhoneNo"].ToString();
            }
        }

        private void txtGrandTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAdd_Click_1(btnPrint, null);
            }
        }

        private void SetRowColor()
        {
            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["IsReplaceReturn"].Value))
                {
                    dgvProductDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                    dgvProductDetails.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                    dgvProductDetails.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
                    dgvProductDetails.Rows[i].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    dgvProductDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    dgvProductDetails.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                    dgvProductDetails.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
                    dgvProductDetails.Rows[i].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                }
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        private void picSaleMode_Click(object sender, EventArgs e)
        {
            DoF7();
        }

        private void dgvProductDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // if nothing is in edit mode.
                if (dgvProductDetails.IsCurrentCellInEditMode == false)
                {
                    if (txtCustomerMobile.Text.Trim().Length != 0 && txtSalesMan.Text.Trim().Length != 0)
                    {
                        btnAdd_Click_1(btnPrint, null);
                    }
                }
            }
        }

        private void txtDeliveryCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAdd_Click_1(btnPrint, null);
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAdd_Click_1(btnPrint, null);
            }
        }

        private void dgvProductDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvProductDetails.Columns[e.Column.Index].ReadOnly = true;
            if (dgvProductDetails.Columns.Contains("QTY"))
            {
                dgvProductDetails.Columns["QTY"].ReadOnly = false;
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
                return;
            }
        }

        private void txtSalesMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // if customer name is given
                if (txtCustomerID.Text.Trim().Length != 0)
                {
                    // if grand total amount is entered.
                    if (Convert.ToDecimal(txtGrandTotal.Text) > 0)
                    {
                        if (frmPayment.lstPaymnetType.Count == 0)
                        {
                            clsUtility.ShowInfoMessage("Please select payment option.");
                            return;
                        }
                    }
                    btnAdd_Click_1(btnPrint, null);
                }
            }
        }

        private void Sales_Invoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isfromdefaul)
            {
                return;
            }
            bool resul = clsUtility.ShowQuestionMessage("Do you want to close this window ?");
            if (resul)
            {
                ClearAll();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (txtEmpID.Visible)
            {
                txtEmpID.Visible = false;
            }
            else
            {
                txtEmpID.Visible = true;
            }
        }

        private void lblCategoryName_Click(object sender, EventArgs e)
        {
            if (txtCustomerID.Visible)
            {
                txtCustomerID.Visible = false;
            }
            else
            {
                txtCustomerID.Visible = true;
            }
        }
    }
}