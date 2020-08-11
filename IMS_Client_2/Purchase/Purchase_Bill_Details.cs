//New
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Purchase
{
    public partial class Purchase_Bill_Details : Form
    {
        public Purchase_Bill_Details()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        DataTable dtPurchaseInvoice = new DataTable();
        DataTable dtDefaultBillDetails = new DataTable();

        int pTotalQTY = 0;
        double pTotalAmt = 0, LocalBillValue = 0, pAddRatio = 0, pCurrencyRate = 0;
        int PurchaseInvoiceID = 0;
        int _ProductID = 0, SubProductID = 0;
        int _QTY = 0;
        DateTime dtDate = DateTime.Now;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void ClearAll()
        {
            txtPurchaseInvoiceID.Clear();
            txtSupplierBillNo.Clear();
            txtStyleNo.Clear();
            txtProductName.Clear();
            txtQTY.Clear();
            txtRate.Clear();
            txtCurrencyRate.Clear();
            txtNewRate.Clear();
            txtProductID.Clear();
            SubProductID = 0;
            cmbSupplier.SelectedIndex = -1;
            cmbBrand.SelectedIndex = -1;
            cmbAddRatio.SelectedIndex = -1;

            dtpBillDate.Format = DateTimePickerFormat.Short;
            dtpBillDate.CustomFormat = " ";
            dtpBillDate.Value = dtDate;

            dtPurchaseInvoice.Clear();
            dataGridView1.DataSource = dtDefaultBillDetails;

            txtTotalQTYBill.Text = "0";
            txtTotalQTYEntered.Text = "0";
            txtTotalValueBill.Text = "0";
            txtTotalValueEntered.Text = "0";
            txtDiffValue.Text = "0";
            txtDiffQty.Text = "0";
            txtSupplierBillNo.Focus();
        }

        private void ClearPurchaseInvoiceBillDetails()
        {
            txtStyleNo.Clear();
            txtProductName.Clear();
            txtQTY.Clear();
            txtRate.Clear();
            txtProductID.Clear();
            SubProductID = 0;

            txtNewRate.Clear();
            txtSalesPrice.Clear();
            txtCurrencyRate.Clear();
            txtProductName.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(cmbAddRatio))
            {
                clsUtility.ShowInfoMessage("Please select Add Ratio to Suggested price.", clsUtility.strProjectTitle);
                cmbAddRatio.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtProductName))
            {
                clsUtility.ShowInfoMessage("Please Enter Product Name ", clsUtility.strProjectTitle);
                txtProductName.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtStyleNo))
            {
                clsUtility.ShowInfoMessage("Please Style Number ", clsUtility.strProjectTitle);
                txtStyleNo.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtQTY))
            {
                clsUtility.ShowInfoMessage("Please Enter Quantity ", clsUtility.strProjectTitle);
                txtQTY.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtRate))
            {
                clsUtility.ShowInfoMessage("Please Enter Cost Price ", clsUtility.strProjectTitle);
                txtRate.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbBrand))
            {
                clsUtility.ShowInfoMessage("Please Select Brand Name ", clsUtility.strProjectTitle);
                cmbBrand.Focus();
                return false;
            }
            //else if (ObjUtil.IsControlTextEmpty(txtSalesPrice))
            //{
            //    clsUtility.ShowInfoMessage("Please Enter Sales Price ", clsUtility.strProjectTitle);
            //    txtSalesPrice.Focus();
            //    return false;
            //}
            else if (Convert.ToInt32(txtQTY.Text) <= 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Valid Quantity ", clsUtility.strProjectTitle);
                txtQTY.Focus();
                return false;
            }
            else if (Convert.ToDouble(txtRate.Text) <= 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Valid Cost Price ", clsUtility.strProjectTitle);
                txtRate.Focus();
                return false;
            }
            else if (CheckDuplicateStyleNo())
            {
                txtStyleNo.Focus();
                return false;
            }
            return true;
        }

        private void FillSupplierData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID,SupplierName", "ISNULL(ActiveStatus,1)=1", "SupplierName ASC");
            cmbSupplier.DataSource = dt;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";

            cmbSupplier.SelectedIndex = -1;
        }

        private void FillBrandData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.BrandMaster", "BrandID,BrandName", "ISNULL(ActiveStatus,1)=1", "BrandName ASC");
            cmbBrand.DataSource = dt;
            cmbBrand.DisplayMember = "BrandName";
            cmbBrand.ValueMember = "BrandID";

            cmbBrand.SelectedIndex = -1;
        }

        private bool ValidatePurchaseBill(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["EndUser"].ToString() == "" || dt.Rows[i]["EndUser"].ToString() == "0.00")
                {
                    clsUtility.ShowInfoMessage("Enter Valid EndUser price for " + dt.Rows[i]["ProductName"], clsUtility.strProjectTitle);
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtDiffQty.Text) >= 0 && Convert.ToDouble(txtDiffValue.Text) >= 0)
            {
                DataTable dtPurchaseInvoiceBill = (DataTable)dataGridView1.DataSource;
                if (ObjUtil.ValidateTable(dtPurchaseInvoiceBill))
                {
                    int _ID = 0, _SubProductID;
                    bool b = false;
                    for (int i = 0; i < dtPurchaseInvoiceBill.Rows.Count; i++)
                    {
                        _ID = 0; _SubProductID = 0;
                        //if (ValidatePurchaseBill(dtPurchaseInvoiceBill))
                        //{
                            int PurchaseInvoiceDetailsID = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["PurchaseInvoiceDetailsID"]);
                            if (PurchaseInvoiceDetailsID == 0)
                            {
                                if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Bill_Details, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
                                {
                                    _ProductID = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["ProductID"]);
                                    _SubProductID = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["SubProductID"]);
                                    if (_SubProductID == 0)
                                    {
                                        //Insert SubProductID
                                        ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, _ProductID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, dtPurchaseInvoiceBill.Rows[i]["ModelNo"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("BrandID", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["BrandID"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("EndUser", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["EndUser"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, _SubProductID, clsConnection_DAL.ParamType.Output);

                                        b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Insert_ProductWiseModelNo");
                                        if (b)
                                        {
                                            DataTable dt = ObjDAL.GetOutputParmData();
                                            if (ObjUtil.ValidateTable(dt))
                                            {
                                                SubProductID = Convert.ToInt32(dt.Rows[0][1]);
                                                _SubProductID = SubProductID;
                                            }
                                        }
                                    }
                                    else if (_SubProductID > 0 && b == false)
                                    {
                                        //Update SubProductID
                                        ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, _ProductID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, dtPurchaseInvoiceBill.Rows[i]["ModelNo"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("BrandID", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["BrandID"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, _SubProductID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("EndUser", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["EndUser"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                                        b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Update_ProductWiseModelNo");
                                    }
                                    //Insert Purchase bill
                                    ObjDAL.SetColumnData("PurchaseInvoiceID", SqlDbType.Int, PurchaseInvoiceID);
                                    ObjDAL.SetColumnData("ProductID", SqlDbType.Int, _ProductID);
                                    ObjDAL.SetColumnData("ModelNo", SqlDbType.NVarChar, dtPurchaseInvoiceBill.Rows[i]["ModelNo"].ToString());
                                    ObjDAL.SetColumnData("BrandID", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["BrandID"].ToString());
                                    ObjDAL.SetColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                                    ObjDAL.SetColumnData("BillDate", SqlDbType.Date, dtpBillDate.Value.ToString("yyyy-MM-dd"));
                                    ObjDAL.SetColumnData("Rate", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["Rate"].ToString());
                                    ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, _SubProductID);
                                    _QTY = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["QTY"]);
                                    ObjDAL.SetColumnData("QTY", SqlDbType.Int, _QTY);
                                    ObjDAL.SetColumnData("Sales_Price", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["EndUser"].ToString());
                                    ObjDAL.SetColumnData("AddedRatio", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["AddedRatio"].ToString());
                                    ObjDAL.SetColumnData("SuppossedPrice", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["SuppossedPrice"].ToString());
                                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                                    _ID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.PurchaseInvoiceDetails", true);
                                }
                                else
                                {
                                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                                    dataGridView1.DataSource = dtPurchaseInvoice;
                                    return;
                                }
                            }
                            else
                            {
                                //Update  Purchase bill
                                if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Bill_Details, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
                                {
                                    _ProductID = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["ProductID"]);
                                    _SubProductID = Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["SubProductID"]);
                                    if (_SubProductID > 0)
                                    {
                                        //Update  SubProductID
                                        ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, _ProductID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, dtPurchaseInvoiceBill.Rows[i]["ModelNo"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("BrandID", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["BrandID"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, _SubProductID, clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("EndUser", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["EndUser"].ToString(), clsConnection_DAL.ParamType.Input);
                                        ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                                        b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Update_ProductWiseModelNo");
                                    }
                                    //ObjDAL.UpdateColumnData("ProductID", SqlDbType.Int, _ProductID);
                                    //ObjDAL.UpdateColumnData("SubProductID", SqlDbType.Int, Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["SubProductID"]));
                                    //ObjDAL.UpdateColumnData("ModelNo", SqlDbType.NVarChar, dtPurchaseInvoiceBill.Rows[i]["ModelNo"].ToString());
                                    //ObjDAL.UpdateColumnData("BrandID", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["BrandID"].ToString());

                                    ObjDAL.UpdateColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                                    ObjDAL.UpdateColumnData("BillDate", SqlDbType.Date, dtpBillDate.Value.ToString("yyyy-MM-dd"));
                                    ObjDAL.UpdateColumnData("QTY", SqlDbType.Int, Convert.ToInt32(dtPurchaseInvoiceBill.Rows[i]["QTY"]));
                                    ObjDAL.UpdateColumnData("Rate", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["Rate"].ToString());
                                    ObjDAL.UpdateColumnData("Sales_Price", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["EndUser"].ToString());
                                    ObjDAL.UpdateColumnData("AddedRatio", SqlDbType.Int, dtPurchaseInvoiceBill.Rows[i]["AddedRatio"].ToString());
                                    ObjDAL.UpdateColumnData("SuppossedPrice", SqlDbType.Decimal, dtPurchaseInvoiceBill.Rows[i]["SuppossedPrice"].ToString());
                                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    _ID = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.PurchaseInvoiceDetails", "PurchaseInvoiceDetailsID = " + PurchaseInvoiceDetailsID + "");
                                }
                                else
                                {
                                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                                    dataGridView1.DataSource = dtPurchaseInvoice;
                                    return;
                                }
                            }
                        //}
                    }

                    if (_ID > 0)
                    {
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                        clsUtility.ShowInfoMessage("Purchase Invoice is Saved Successfully..", clsUtility.strProjectTitle);
                        ClearAll();
                        grpPurchaseBillDetail.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Purchase Invoice is not Saved Successfully..", clsUtility.strProjectTitle);
                    }
                    ObjDAL.ResetData();
                }
                else
                {
                    clsUtility.ShowInfoMessage("There is No Purchase Invoice available to Save", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("QTY or Total bill amount can't be negative", clsUtility.strProjectTitle);
            }
            dataGridView1.DataSource = dtPurchaseInvoice;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                ClearAll();
                grpPurchaseBillDetail.Enabled = false;
            }
        }

        private void Purchase_Bill_Details_Load(object sender, EventArgs e)
        {
            //btnSearch.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            FillSupplierData();
            FillBrandData();
            InitItemTable();
            dtDefaultBillDetails = (DataTable)dataGridView1.DataSource;
        }

        private void btnSearch_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnSearch_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            try
            {
                dataGridView1.Columns["ProductID"].Visible = false;
                dataGridView1.Columns["BrandID"].Visible = false;
                dataGridView1.Columns["SubProductID"].Visible = false;
                if (dataGridView1.Columns.Contains("ModelNo"))
                {
                    dataGridView1.Columns["ModelNo"].HeaderText = "Style No";
                }
                if (dataGridView1.Columns.Contains("ProductName"))
                {
                    dataGridView1.Columns["ProductName"].HeaderText = "Item Name";
                }
                if (dataGridView1.Columns.Contains("BrandName"))
                {
                    dataGridView1.Columns["BrandName"].HeaderText = "Brand Name";
                }
                if (dataGridView1.Columns.Contains("PurchaseInvoiceDetailsID"))
                {
                    dataGridView1.Columns["PurchaseInvoiceDetailsID"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("PurchaseInvoiceID"))
                {
                    dataGridView1.Columns["PurchaseInvoiceID"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("SupplierID"))
                {
                    dataGridView1.Columns["SupplierID"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("BillDate"))
                {
                    dataGridView1.Columns["BillDate"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("BillValue"))
                {
                    dataGridView1.Columns["BillValue"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("TotalQTY"))
                {
                    dataGridView1.Columns["TotalQTY"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("LocalValue"))
                {
                    dataGridView1.Columns["LocalValue"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("LocalBillValue"))
                {
                    dataGridView1.Columns["LocalBillValue"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("ColDelete"))
                {
                    dataGridView1.Columns.Remove("ColDelete");
                }
                DataGridViewButtonColumn ColDelete = new DataGridViewButtonColumn();
                ColDelete.DataPropertyName = "Delete";
                ColDelete.HeaderText = "Delete";
                ColDelete.Name = "ColDelete";
                ColDelete.Text = "Delete";
                ColDelete.UseColumnTextForButtonValue = true;
                //dataGridView1.Columns.Add(ColDelete);
                dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColDelete });
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, txtPurchaseInvoiceID.Text, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BillDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtLoadData = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtLoadData))
                {
                    double TotalValueBill = 0, TotalQTYBill = 0, LocalValue = 0;

                    dtpBillDate.Value = dtLoadData.Rows[0]["BillDate"] != DBNull.Value ? Convert.ToDateTime(dtLoadData.Rows[0]["BillDate"]) : DateTime.Now;
                    cmbSupplier.SelectedValue = Convert.ToInt32(dtLoadData.Rows[0]["SupplierID"]);
                    TotalValueBill = Convert.ToDouble(dtLoadData.Rows[0]["BillValue"]);
                    TotalQTYBill = Convert.ToDouble(dtLoadData.Rows[0]["TotalQTY"]);
                    LocalValue = Convert.ToDouble(dtLoadData.Rows[0]["LocalValue"]);
                    LocalBillValue = Convert.ToDouble(dtLoadData.Rows[0]["LocalBillValue"]);
                    txtTotalValueBill.Text = TotalValueBill.ToString();
                    txtTotalQTYBill.Text = TotalQTYBill.ToString();
                    PurchaseInvoiceID = Convert.ToInt32(dtLoadData.Rows[0]["PurchaseInvoiceID"]);

                    txtDiffQty.Text = (Convert.ToInt32(txtTotalQTYBill.Text) - pTotalQTY).ToString();
                    txtDiffValue.Text = (Convert.ToDouble(txtTotalValueBill.Text) - pTotalAmt).ToString();

                    txtCurrencyRate.Text = Math.Round((LocalValue / TotalValueBill), 3).ToString();
                    txtNewRate.Text = Math.Round((LocalBillValue / TotalValueBill), 3).ToString();

                    txtProductName.Focus();

                    if (ObjUtil.ValidateTable((DataTable)dataGridView1.DataSource))
                    {
                        DataTable dt = (DataTable)dataGridView1.DataSource;
                        dt.Rows.Clear();
                        dataGridView1.DataSource = dt;
                    }

                    if (Convert.ToInt32(dtLoadData.Rows[0]["ProductID"]) > 0 && Convert.ToInt32(txtDiffQty.Text) == 0 && Convert.ToDouble(txtDiffValue.Text) == 0)
                    {
                        grpPurchaseBillDetail.Enabled = false;
                        CalculateSubTotal();
                    }
                    else
                    {
                        grpPurchaseBillDetail.Enabled = true;
                        if (Convert.ToInt32(dtLoadData.Rows[0]["ProductID"]) > 0)
                        {
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = dtLoadData;
                            dtPurchaseInvoice = dtLoadData;
                        }
                        else if (ObjUtil.ValidateTable((DataTable)dataGridView1.DataSource))
                        {
                            if (ObjUtil.ValidateTable(dtDefaultBillDetails))
                            {
                                dtDefaultBillDetails.Rows.Clear();
                            }
                            dataGridView1.DataSource = dtDefaultBillDetails;
                        }
                    }
                    btnSave.Enabled = true;
                    CalculateSubTotal();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Purchase Invoice is not available for Bill No. " + txtSupplierBillNo.Text, clsUtility.strProjectTitle);
                    grpPurchaseBillDetail.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Purchase Invoice is not available for Bill No. " + txtSupplierBillNo.Text, clsUtility.strProjectTitle);
                grpPurchaseBillDetail.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ", clsUtility.strProjectTitle);
                txtSupplierBillNo.Focus();
                return;
            }
            cmbAddRatio.Enabled = true;
            cmbAddRatio.SelectedIndex = 0;
            LoadData();
        }

        private void txtSupplierBillNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSupplierBillNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_ProductDetails_Popup '" + txtProductName.Text + "'");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        linkAddPurchaseBillItems.Enabled = true;
                        ObjUtil.SetControlData(txtProductName, "ItemName");
                        ObjUtil.SetControlData(txtProductID, "ProductID");

                        ObjUtil.ShowDataPopup(dt, txtProductName, this, grpPurchaseBillDetail);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["ProductID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["CategoryID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += Purchase_Bill_Details_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += Purchase_Bill_Details_KeyDown;
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
                        if (ObjUtil.IsControlTextEmpty(txtProductID))
                        {
                            clsUtility.ShowInfoMessage("Item Name " + txtProductName.Text + " is not available in Item Master.", clsUtility.strProjectTitle);
                            txtProductName.Focus();
                        }
                        linkAddPurchaseBillItems.Enabled = false;
                    }
                }
                else
                {
                    txtProductID.Clear();
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void Purchase_Bill_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtStyleNo.Focus();
                linkAddPurchaseBillItems.Enabled = true;
            }
        }

        private void Purchase_Bill_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtStyleNo.Focus();
                linkAddPurchaseBillItems.Enabled = true;
            }
        }

        private void AddRowToItemDetails()
        {
            int pEnteredQTY = 0;
            pEnteredQTY = Convert.ToInt32(txtTotalQTYEntered.Text) + Convert.ToInt32(txtQTY.Text);
            if (pEnteredQTY <= Convert.ToInt32(txtTotalQTYBill.Text))
            {
                if (pCurrencyRate > 0)
                {
                    pAddRatio = Convert.ToInt32(cmbAddRatio.SelectedItem) * 0.01;

                    DataRow dRow = dtPurchaseInvoice.NewRow();
                    dRow["PurchaseInvoiceDetailsID"] = 0;
                    dRow["ProductID"] = txtProductID.Text;
                    dRow["SubProductID"] = SubProductID;
                    dRow["ProductName"] = txtProductName.Text;
                    dRow["BrandID"] = cmbBrand.SelectedValue;
                    dRow["BrandName"] = cmbBrand.Text;
                    dRow["ModelNo"] = txtStyleNo.Text;
                    dRow["QTY"] = txtQTY.Text;
                    dRow["Rate"] = txtRate.Text;
                    double pTotal = Math.Round((Convert.ToInt32(txtQTY.Text) * Convert.ToDouble(txtRate.Text)), 2);
                    dRow["Total"] = pTotal;
                    //double pLocalCost = Math.Round((pTotal / Convert.ToInt32(txtQTY.Text)) * pCurrencyRate, 2);
                    double pLocalCost = Math.Round(Convert.ToDouble(txtRate.Text) * pCurrencyRate, 2);
                    dRow["LocalCost"] = pLocalCost;
                    dRow["AddedRatio"] = cmbAddRatio.SelectedItem;
                    dRow["SuppossedPrice"] = pLocalCost + (pLocalCost * pAddRatio);

                    if (ObjUtil.IsControlTextEmpty(txtSalesPrice))
                    {
                        txtSalesPrice.Text = "0";
                    }
                    dRow["EndUser"] = txtSalesPrice.Text;
                    //dRow["Delete"] = "Delete";

                    dtPurchaseInvoice.Rows.Add(dRow);
                    dtPurchaseInvoice.AcceptChanges();
                    dataGridView1.DataSource = dtPurchaseInvoice;
                    btnSave.Enabled = true;

                    CalculateSubTotal();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Currency Rate can't be 0.", clsUtility.strProjectTitle);
                    return;
                }
            }
            else if (pEnteredQTY == Convert.ToInt32(txtTotalQTYBill.Text))
            {
                clsUtility.ShowInfoMessage("Purchase Invoiced QTY and Entered QTY is already same", clsUtility.strProjectTitle);
            }
            else
            {
                clsUtility.ShowInfoMessage("Entered QTY can not greater then Purchase Invoice QTY", clsUtility.strProjectTitle);
            }
        }

        private void InitItemTable()
        {
            dtPurchaseInvoice.Columns.Add("PurchaseInvoiceDetailsID");
            dtPurchaseInvoice.Columns.Add("ProductID");
            dtPurchaseInvoice.Columns.Add("SubProductID");
            dtPurchaseInvoice.Columns.Add("ProductName");
            dtPurchaseInvoice.Columns.Add("ModelNo");
            dtPurchaseInvoice.Columns.Add("BrandID");
            dtPurchaseInvoice.Columns.Add("BrandName");
            dtPurchaseInvoice.Columns.Add("QTY");
            dtPurchaseInvoice.Columns.Add("Rate");
            dtPurchaseInvoice.Columns.Add("Total");
            dtPurchaseInvoice.Columns.Add("LocalCost");
            dtPurchaseInvoice.Columns.Add("AddedRatio");
            dtPurchaseInvoice.Columns.Add("SuppossedPrice");
            dtPurchaseInvoice.Columns.Add("EndUser");
            //dtPurchaseInvoice.Columns.Add("Delete");

            //DataGridViewButtonColumn ColDelete = new DataGridViewButtonColumn();
            //ColDelete.DataPropertyName = "Delete";
            //ColDelete.HeaderText = "Delete";
            //ColDelete.Name = "ColDelete";
            //ColDelete.Text = "Delete";
            //ColDelete.UseColumnTextForButtonValue = true;

            dtPurchaseInvoice.AcceptChanges();
            dataGridView1.DataSource = dtPurchaseInvoice;
            //dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            //ColDelete});
        }

        private bool CheckDuplicateStyleNo()
        {
            DataTable dtModelNo = new DataTable();
            DataRow[] dRow = dtPurchaseInvoice.Select("ModelNo= '" + txtStyleNo.Text.Trim() + "' AND BrandID=" + cmbBrand.SelectedValue);
            if (dRow.Length == 0)
            {
                ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtStyleNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("BrandID", SqlDbType.Int, cmbBrand.SelectedValue, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_IsModelBrandExists");
                if (ds != null && ds.Tables.Count > 0)
                {
                    dtModelNo = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dtModelNo))
                    {
                        if (Convert.ToInt32(dtModelNo.Rows[0]["IsAdd"]) == 0)
                        {
                            clsUtility.ShowInfoMessage(dtModelNo.Rows[0]["Msg"].ToString(), clsUtility.strProjectTitle);
                            return true;
                        }
                        else
                        {
                            SubProductID = Convert.ToInt32(dtModelNo.Rows[0]["SubProductID"]);
                            if (SubProductID > 0)
                            {
                                decimal EndUser = Convert.ToDecimal(dtModelNo.Rows[0]["EndUser"]);
                                decimal UserEndUser = Convert.ToDecimal(txtSalesPrice.Text);
                                string EndUserpricechange = string.Empty;
                                if (EndUser != UserEndUser)
                                {
                                    EndUserpricechange = "\n Entered Sales Price is different from previous Entered";
                                }
                                bool msg = clsUtility.ShowQuestionMessage("ModelNo. " + txtStyleNo.Text.Trim() + " is already exists for Item " + txtProductName.Text + "" + EndUserpricechange + "\n Do you want to Update it?", clsUtility.strProjectTitle);
                                if (msg)
                                {
                                    return false;
                                }
                                else
                                {
                                    clsUtility.ShowInfoMessage("Enter New Model No. for Item " + txtProductName.Text, clsUtility.strProjectTitle);
                                    return true;
                                }
                            }
                            return false;
                        }
                    }
                }
                ObjDAL.ResetData();
            }
            else if (dRow.Length > 0)
            {
                clsUtility.ShowInfoMessage("ModelNo. " + txtStyleNo.Text.Trim() + " is already exists for Item " + dRow[0]["ProductName"] + " in List", clsUtility.strProjectTitle);
                return true;
            }
            return false;
        }

        private void linkAddPurchaseBillItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Bill_Details, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    AddRowToItemDetails();
                    ClearPurchaseInvoiceBillDetails();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void CalculateSubTotal()
        {
            pTotalQTY = 0;
            pTotalAmt = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["QTY"].Value != DBNull.Value && dataGridView1.Rows[i].Cells["Total"].Value != DBNull.Value)
                {
                    pTotalQTY += Convert.ToInt32(dataGridView1.Rows[i].Cells["QTY"].Value);
                    pTotalAmt += Convert.ToDouble(dataGridView1.Rows[i].Cells["Total"].Value);
                }
            }
            txtTotalQTYEntered.Text = pTotalQTY.ToString();
            txtTotalValueEntered.Text = pTotalAmt.ToString();

            txtDiffQty.Text = (Convert.ToInt32(txtTotalQTYBill.Text) - pTotalQTY).ToString();
            txtDiffValue.Text = Math.Round(Convert.ToDouble(txtTotalValueBill.Text) - pTotalAmt, 2).ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ColDelete")
                {
                    int pPurchaseInvoiceDetailsID = dataGridView1.Rows[e.RowIndex].Cells["PurchaseInvoiceDetailsID"].Value == DBNull.Value
                        ? 0 : Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["PurchaseInvoiceDetailsID"].Value);

                    if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Bill_Details, clsFormRights.Operation.Delete) || clsUtility.IsAdmin || pPurchaseInvoiceDetailsID == 0)
                    {
                        DialogResult d = MessageBox.Show("Are you sure want to delete ? ", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (d == DialogResult.Yes)
                        {
                            int a = 0;
                            DataTable dt = (DataTable)dataGridView1.DataSource;

                            if (pPurchaseInvoiceDetailsID > 0)
                            {
                                a = ObjDAL.DeleteData(clsUtility.DBName + ".[dbo].[PurchaseInvoiceDetails]", "PurchaseInvoiceDetailsID=" + pPurchaseInvoiceDetailsID);
                            }
                            if (a > 0 || pPurchaseInvoiceDetailsID == 0)
                            {
                                dt.Rows[e.RowIndex].Delete();
                                dt.AcceptChanges();
                            }
                            else
                            {
                                clsUtility.ShowInfoMessage("Unable to delete Style No. " + dataGridView1.Rows[e.RowIndex].Cells["ModelNo"].Value, clsUtility.strProjectTitle);
                            }
                            dataGridView1.DataSource = dt;
                            CalculateSubTotal();
                            txtProductName.Focus();
                        }
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                    }
                }
            }
        }

        private void cmbAddRatio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pAddRatio = Convert.ToInt32(cmbAddRatio.SelectedItem) * 0.01;
        }

        private void txtSupplierBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierBillNo.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtSupplierBillNo.Text + "'");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSupplierBillNo, "SupplierBillNo");
                        ObjUtil.SetControlData(txtPurchaseInvoiceID, "PurchaseInvoiceID");
                        ObjUtil.ShowDataPopup(dt, txtSupplierBillNo, this, this);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["PurchaseInvoiceID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["SupplierID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += Purchase_Bill_Details_Supplier_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += Purchase_Bill_Details_Supplier_KeyDown;
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
                    }
                }
                else
                {
                    txtPurchaseInvoiceID.Clear();
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void Purchase_Bill_Details_Supplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();

                cmbAddRatio.Enabled = true;
                cmbAddRatio.SelectedIndex = 0;

                LoadData();
                cmbSupplier_SelectionChangeCommitted(sender, e);
            }
        }

        private void cmbSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbSupplier.SelectedIndex >= 0)
            {
                a = ObjDAL.ExecuteScalarInt("SELECT BrandID FROM " + clsUtility.DBName + ".[dbo].[BrandMaster] WITH(NOLOCK) WHERE SupplierID=" + cmbSupplier.SelectedValue);
                GetLocalCurrencyRate();
            }
            if (a > 0)
            {
                cmbBrand.SelectedValue = a;
            }
            else
            {
                cmbBrand.SelectedIndex = -1;
            }
        }

        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled)
            {
                clsUtility.ShowInfoMessage("Enter Only Number for QTY...", clsUtility.strProjectTitle);
                txtQTY.Focus();
            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtRate, e);
            if (e.Handled)
            {
                clsUtility.ShowInfoMessage("Enter Only Number for Rate...", clsUtility.strProjectTitle);
                txtRate.Focus();
            }
        }

        private void txtSalesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtSalesPrice, e);
            if (e.Handled)
            {
                clsUtility.ShowInfoMessage("Enter Only Number Sales Price...", clsUtility.strProjectTitle);
                txtSalesPrice.Focus();
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dataGridView1.Columns[e.Column.Index].ReadOnly = true;
            if (dataGridView1.Columns.Contains("QTY"))
            {
                dataGridView1.Columns["QTY"].ReadOnly = false;
            }
            if (dataGridView1.Columns.Contains("Rate"))
            {
                dataGridView1.Columns["Rate"].ReadOnly = false;
            }
            if (dataGridView1.Columns.Contains("AddedRatio"))
            {
                dataGridView1.Columns["AddedRatio"].ReadOnly = false;
            }
            if (dataGridView1.Columns.Contains("EndUser"))
            {
                dataGridView1.Columns["EndUser"].ReadOnly = false;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            e.Cancel = false;
            int column = dataGridView1.CurrentCell.ColumnIndex;
            string headerText = dataGridView1.Columns[column].HeaderText;
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
            else if (headerText == "AddedRatio")
            {
                if (e.FormattedValue == DBNull.Value || e.FormattedValue.ToString() == "")
                {
                    clsUtility.ShowInfoMessage("Enter AddedRatio..");
                    e.Cancel = true;
                }
                else if (Convert.ToInt32(e.FormattedValue) < 100 || Convert.ToInt32(e.FormattedValue) > 300)
                {
                    clsUtility.ShowInfoMessage("Enter Ratio between 100 to 300..");
                    e.Cancel = true;
                }
                return;
            }
            else if (headerText == "Rate")
            {
                if (e.FormattedValue == DBNull.Value || e.FormattedValue.ToString() == "")
                {
                    clsUtility.ShowInfoMessage("Enter Rate..");
                    e.Cancel = true;
                }
                else if (Convert.ToDecimal(e.FormattedValue) == 0)
                {
                    clsUtility.ShowInfoMessage("Enter Valid Rate..");
                    e.Cancel = true;
                }
                return;
            }
            else if (headerText == "EndUser")
            {
                if (e.FormattedValue == DBNull.Value || e.FormattedValue.ToString() == "")
                {
                    clsUtility.ShowInfoMessage("Enter EndUser Price..");
                    e.Cancel = true;
                }
                else if (Convert.ToDecimal(e.FormattedValue) == 0)
                {
                    clsUtility.ShowInfoMessage("Enter Valid EndUser Price..");
                    e.Cancel = true;
                }
                return;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int column = dataGridView1.CurrentCell.ColumnIndex;
            string headerText = dataGridView1.Columns[column].HeaderText;

            if (headerText == "Rate" || headerText == "EndUser")
            {
                e.Control.KeyPress += Decimal_Control_KeyPress;
            }
            else if (headerText == "QTY" || headerText == "AddedRatio")
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

        private void Decimal_Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string k = e.KeyChar.ToString();
            TextBox txt = (TextBox)sender;
            e.Handled = ObjUtil.IsDecimal(txt, e);
        }

        private void Purchase_Bill_Details_Supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();

                cmbAddRatio.Enabled = true;
                cmbAddRatio.SelectedIndex = 0;
                LoadData();
                cmbSupplier_SelectionChangeCommitted(sender, e);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Columns[e.ColumnIndex].Name == "Rate" ||
            //        dataGridView1.Columns[e.ColumnIndex].Name == "QTY" || dataGridView1.Columns[e.ColumnIndex].Name == "AddedRatio")
            //    {
            //        int QTY = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value);
            //        double Rate = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Rate"].Value);
            //        double Total = QTY * Rate;

            //        double LocalCost = Math.Round((Total / QTY) * pCurrencyRate, 2);
            //        double AddRatio = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AddedRatio"].Value) * 0.01;

            //        bool s = dataGridView1.CurrentCell.Selected;
            //        bool ed = dataGridView1.CurrentCell.IsInEditMode;

            //        dataGridView1.Rows[e.RowIndex].Cells["SuppossedPrice"].Value = Math.Round(LocalCost + (LocalCost * AddRatio), 2).ToString();
            //        dataGridView1.Rows[e.RowIndex].Cells["Total"].Value = Math.Round(Total, 2).ToString();

            //        CalculateSubTotal();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    clsUtility.ShowInfoMessage(ex.Message, clsUtility.strProjectTitle);
            //}
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Rate" ||
                        dataGridView1.Columns[e.ColumnIndex].Name == "QTY" || dataGridView1.Columns[e.ColumnIndex].Name == "AddedRatio")
                {
                    int QTY = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value);
                    double Rate = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Rate"].Value);
                    double Total = QTY * Rate;

                    //double LocalCost = Math.Round((Total / QTY) * pCurrencyRate, 2);
                    double LocalCost = Math.Round(Rate * pCurrencyRate, 2);
                    double AddRatio = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AddedRatio"].Value) * 0.01;

                    dataGridView1.Rows[e.RowIndex].Cells["SuppossedPrice"].Value = Math.Round(LocalCost + (LocalCost * AddRatio), 2).ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["Total"].Value = Math.Round(Total, 2).ToString();

                    CalculateSubTotal();
                }
            }
        }

        private void GetLocalCurrencyRate()
        {
            if (cmbSupplier.SelectedValue != null)
            {
                object Rate = ObjDAL.ExecuteScalar("SELECT TOP 1 CurrencyRate FROM " + clsUtility.DBName + ".[dbo].[CurrencyRateSetting] WITH(NOLOCK) WHERE CountryID=(SELECT CountryID FROM " + clsUtility.DBName + ".[dbo].SupplierMaster WITH(NOLOCK) WHERE SupplierID=" + cmbSupplier.SelectedValue + ")");
                pCurrencyRate = Convert.ToDouble(Rate);
            }
        }
    }
}