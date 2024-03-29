﻿using System;
using System.CodeDom;
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
    public partial class Delivering_Purchase_Bill : Form
    {
        public Delivering_Purchase_Bill()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        DataTable dtPurchaseQTYColor = new DataTable();
        DataTable dtPurchaseInvoice = new DataTable();
        DataTable dtSize = new DataTable();

        int ID = 0;
        int flag = 0;
        int pPurchaseInvoiceID = 0;
        int ProductID = 0, SubProductID = 0, DeliveryPurchaseID3 = 0;
        string OldColorName = string.Empty;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void FillBrandData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.BrandMaster", "BrandID,BrandName", "ISNULL(ActiveStatus,1)=1", "BrandName ASC");
            cmbBrand.DataSource = dt;
            cmbBrand.DisplayMember = "BrandName";
            cmbBrand.ValueMember = "BrandID";
            cmbBrand.SelectedIndex = -1;
        }

        private void FillCountryData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CountryMaster", "CountryID,CountryName", "ISNULL(ActiveStatus,1)=1", "CountryName ASC");
            cmbCountry.DataSource = dt;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";
            cmbCountry.SelectedIndex = -1;
        }
        private void FillSizeTypeData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeTypeMaster", "SizeTypeID,SizeTypeName", "ISNULL(ActiveStatus,1)=1", "SizeTypeName ASC");
            cmbSizeType.DataSource = dt;
            cmbSizeType.DisplayMember = "SizeTypeName";
            cmbSizeType.ValueMember = "SizeTypeID";
            cmbSizeType.SelectedIndex = -1;
        }

        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.StoreMaster", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            cmbStore.DataSource = dt;
            cmbStore.DisplayMember = "StoreName";
            cmbStore.ValueMember = "StoreID";
            cmbStore.SelectedIndex = -1;

            cmbStore.Enabled = false;
        }
        private void FillCategoryData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName", "ISNULL(ActiveStatus,1)=1", "CategoryName ASC");
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.SelectedIndex = -1;
        }
        private void Delivering_Purchase_Bill_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
            btnSearch.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            FillStoreData();
            FillSizeTypeData();
            FillCountryData();
            FillBrandData();
            FillCategoryData();
            //InitItemTable();
        }
        private void InitItemTable()
        {
            dtPurchaseQTYColor.Columns.Clear();
            dtPurchaseQTYColor.Columns.Add("Color", typeof(string));
            dtPurchaseQTYColor.Columns.Add("ColorID", typeof(int));
            dtPurchaseQTYColor.Columns.Add("DeliveryPurchaseID3", typeof(int));
            dtPurchaseQTYColor.AcceptChanges();
            dgvQtycolor.DataSource = dtPurchaseQTYColor;
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
        private void txtSupplierBillNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }
        private void txtSupplierBillNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, System.Drawing.Color.White);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ", clsUtility.strProjectTitle);
                txtSupplierBillNo.Focus();
                return;
            }
            dgvQtycolor.Enabled = false;
            cmbStore.Enabled = true;
            LoadData();
        }

        private void ResizeGridView()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            }

            else if (WindowState == FormWindowState.Normal)
            {
                ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);

            ResizeGridView();

            dataGridView1.Columns["ModelNo"].HeaderText = "Style No.";
            dataGridView1.Columns["PurchaseInvoiceDetailsID"].Visible = false;
            dataGridView1.Columns["PurchaseInvoiceID"].Visible = false;
            dataGridView1.Columns["DeliveryPurchaseID1"].Visible = false;
            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["SubProductID"].Visible = false;
            dataGridView1.Columns["BrandID"].Visible = false;
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CategoryID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
            dataGridView1.Columns["SizeTypeID"].Visible = false;
            dataGridView1.Columns["StoreID"].Visible = false;
        }

        private void cmbSizeType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillColorSizeGrid();
        }

        private void Load_Color_SizeData()
        {
            dtPurchaseQTYColor = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_PurchaseInvoice_Color_Size] '" + txtSupplierBillNo.Text + "', '" + cmbListBox.Text + "'");
            if (ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                dgvQtycolor.DataSource = null;
                dgvQtycolor.DataSource = dtPurchaseQTYColor;
                dgvQtycolor.Refresh();
                dgvQtycolor.ReadOnly = true;
                cmbSizeType.Enabled = false;
            }
            else
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
                dgvQtycolor.ReadOnly = false;
                dgvQtycolor.DataSource = null;
                cmbSizeType.Enabled = true;
            }
        }

        private void dgvQtycolor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int pTotalEnteredQTY = 0;
                if (e.ColumnIndex == 0 && dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value.ToString() != string.Empty)
                {
                    int a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.ColorMaster", "ColorName = '" + dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value + "' AND ISNULL(ActiveStatus,1) = 1");
                    if (a <= 0)
                    {
                        flag = 1;
                        clsUtility.ShowInfoMessage("Color Name " + dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value + " is not present in Color Master", clsUtility.strProjectTitle);
                        //dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value = OldColorName;
                    }
                    else
                    {
                        int ColorID = ObjDAL.ExecuteScalarInt("SELECT ColorID FROM " + clsUtility.DBName + ".dbo.ColorMaster WITH(NOLOCK) WHERE ColorName = '" + dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value + "' AND ISNULL(ActiveStatus,1) = 1");
                        dtPurchaseQTYColor.Rows[e.RowIndex]["ColorID"] = ColorID;
                        flag = 0;
                    }
                }
                else
                {
                    for (int i = 3; i < dtPurchaseQTYColor.Columns.Count - 1; i++)
                    {
                        pTotalEnteredQTY += dtPurchaseQTYColor.Rows[e.RowIndex][i] != DBNull.Value ?
                            Convert.ToInt32(dtPurchaseQTYColor.Rows[e.RowIndex][i]) : 0;
                    }
                    dgvQtycolor.Rows[e.RowIndex].Cells["Total"].Value = pTotalEnteredQTY;
                    txtTotalQTYEntered.Text = pTotalEnteredQTY.ToString();
                    CalcTotalColorQTY();
                    if (Convert.ToInt32(txtTotalQTYEntered.Text) > Convert.ToInt32(txtTotalQTYBill.Text))
                    {
                        clsUtility.ShowInfoMessage("Entered QTY can't be greater then QTY Bill");
                        //dgvQtycolor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = OldSize;
                    }
                    if (e.RowIndex + 1 == dgvQtycolor.Rows.Count)
                    {
                        DataRow Row = dtPurchaseQTYColor.NewRow();
                        dtPurchaseQTYColor.Rows.Add(Row);
                        dtPurchaseQTYColor.AcceptChanges();
                        dgvQtycolor.DataSource = dtPurchaseQTYColor;
                    }
                }
            }
            catch (InvalidOperationException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void ClearAll()
        {
            txtPurchaseInvoiceID.Clear();
            txtSupplierBillNo.Clear();
            txtSupplierBillNo.Enabled = false;
            txtItemName.Clear();
            txtDiffQty.Text = "0";
            txtTotalQTY.Text = "0";
            txtTotalQTYBill.Text = "0";
            txtTotalQTYEntered.Text = "0";
            cmbBrand.SelectedIndex = -1;
            cmbCountry.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            cmbSizeType.SelectedIndex = -1;
            cmbStore.SelectedIndex = -1;
            cmbListBox.SelectedIndex = -1;
            if (ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                dtPurchaseQTYColor.Clear();
            }
            dtPurchaseInvoice.Clear();
            dgvQtycolor.DataSource = null;
            dataGridView1.DataSource = null;
        }
        private void Clear_ColorSize()
        {
            //txtPurchaseInvoiceID.Clear();
            txtItemName.Clear();
            cmbBrand.SelectedIndex = -1;
            cmbCountry.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            txtDiffQty.Text = "0";
            //txtTotalQTY.Text = "0";
            txtTotalQTYBill.Text = "0";
            txtTotalQTYEntered.Text = "0";
            DeliveryPurchaseID3 = 0;
            cmbSizeType.SelectedIndex = -1;
            dtPurchaseQTYColor.Clear();
            dgvQtycolor.DataSource = null;
        }


        private void FillColorSizeGrid()
        {
            if (cmbSizeType.SelectedValue != null)
            {
                dtSize = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeMaster", "SizeID,Size,SizeTypeID", "ISNULL(ActiveStatus,1) = 1 AND SizeTypeID = " + cmbSizeType.SelectedValue, null);
                if (ObjUtil.ValidateTable(dtSize))
                {
                    InitItemTable();
                    dgvQtycolor.Enabled = true;
                    for (int i = 0; i < dtSize.Rows.Count; i++)
                    {
                        dtPurchaseQTYColor.Columns.Add(dtSize.Rows[i]["Size"].ToString());
                    }
                    dtPurchaseQTYColor.Columns.Add("Total", typeof(int));
                    if (dtPurchaseQTYColor.Rows.Count <= 0)
                    {
                        DataRow dRow = dtPurchaseQTYColor.NewRow();
                        dtPurchaseQTYColor.Rows.Add(dRow);
                    }
                    dtPurchaseQTYColor.AcceptChanges();
                    dgvQtycolor.DataSource = dtPurchaseQTYColor;
                    //Load_Color_SizeData();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Please Select Size Type..");
            }
        }
        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ");
                txtSupplierBillNo.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbStore))
            {
                clsUtility.ShowInfoMessage("Select Store for Supplier Bill No " + txtSupplierBillNo.Text);
                cmbStore.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbSizeType))
            {
                clsUtility.ShowInfoMessage("Select Size Type           ");
                cmbSizeType.Focus();
                return false;
            }
            else if (!ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                clsUtility.ShowInfoMessage("Enter Color and Qty           ");
                dgvQtycolor.Focus();
                return false;
            }
            else if (ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                int total = 0;
                for (int i = 0; i < dtPurchaseQTYColor.Rows.Count; i++)
                {
                    total = dtPurchaseQTYColor.Rows[i]["Total"] == DBNull.Value ? 0 : Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["Total"]);
                    if (dtPurchaseQTYColor.Rows[i]["Color"] == DBNull.Value)
                    {
                        dtPurchaseQTYColor.Rows[i].Delete();
                    }
                    else if (dtPurchaseQTYColor.Rows[i]["ColorID"] == DBNull.Value)
                    {
                        clsUtility.ShowInfoMessage("Enter Valid Colour Name.         ");
                        return false;
                    }
                    else if (total <= 0)
                    {
                        clsUtility.ShowInfoMessage("Enter QTY for " + txtItemName.Text);
                        return false;
                    }
                }
                dtPurchaseQTYColor.AcceptChanges();
                //return true;
            }
            if (Convert.ToInt32(txtDiffQty.Text) < 0)
            {
                clsUtility.ShowInfoMessage("Entered QTY can't be greater then Billed QTY");
                dgvQtycolor.Focus();
                return false;
            }
            else if (Convert.ToInt32(txtDiffQty.Text) > 0)
            {
                clsUtility.ShowInfoMessage("Entered QTY can't be less then Billed QTY");
                dgvQtycolor.Focus();
                return false;
            }
            else if (!ObjUtil.ValidateTable((DataTable)dgvQtycolor.DataSource))
            {
                clsUtility.ShowInfoMessage("Select Size Type OR enter Color for " + txtItemName.Text);
                FillColorSizeGrid();
                return false;
            }
            return true;
        }
        private bool DuplicateUser(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "SizeTypeID=" + cmbSizeType.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "' AND ModelNo='" + cmbListBox.Text + "'");

            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "SizeTypeID=" + cmbSizeType.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "'" +
                    "AND ModelNo='" + cmbListBox.Text + "' AND DeliveryPurchaseID1 !=" + i);
            }
            if (a > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void CalcTotalColorQTY()
        {
            if (dtPurchaseQTYColor.Columns.Contains("Total"))
            {
                object ob = dtPurchaseQTYColor.Compute("SUM(Total)", null);
                txtTotalQTYEntered.Text = ob.ToString();
            }
            int pTotal = txtTotalQTYEntered.Text.Length > 0 ? Convert.ToInt32(txtTotalQTYEntered.Text) : 0;
            txtDiffQty.Text = (Convert.ToInt32(txtTotalQTYBill.Text) - pTotal).ToString();
        }
        private int DataSavedDeliveryPurchaseBill2(int ID)
        {
            try
            {
                ObjDAL.SetColumnData("DeliveryPurchaseID1", SqlDbType.Int, ID);
                for (int i = 3; i < dtPurchaseQTYColor.Columns.Count - 1; i++)
                {
                    string col = dtPurchaseQTYColor.Columns[i].ColumnName.ToString();
                    DataRow[] drow = dtSize.Select("Size = '" + col + "' AND SizeTypeID=" + cmbSizeType.SelectedValue);
                    if (drow.Length > 0)
                    {
                        ObjDAL.SetColumnData("Col" + (i - 2), SqlDbType.Int, drow[0]["SizeID"].ToString());
                    }
                }
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                return ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill2", true);
            }
            catch
            {
                ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID, clsConnection_DAL.ParamType.Input);
                ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Delete_Delivering_PurchaseBill");
                return 0;
            }
        }

        private int DataSavedDeliveryPurchaseBill3(int ID1, int ID2)
        {
            int a = 0;
            try
            {
                for (int i = DeliveryPurchaseID3; i < dtPurchaseQTYColor.Rows.Count; i++)
                {
                    ObjDAL.SetColumnData("DeliveryPurchaseID1", SqlDbType.Int, ID1);
                    ObjDAL.SetColumnData("DeliveryPurchaseID2", SqlDbType.Int, ID2);
                    ObjDAL.SetColumnData("ColorID", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["ColorID"]));
                    for (int j = 3; j < dtPurchaseQTYColor.Columns.Count - 1; j++)
                    {
                        ObjDAL.SetColumnData("Col" + (j - 2), SqlDbType.Int, !string.IsNullOrEmpty(dtPurchaseQTYColor.Rows[i][j].ToString()) ?
                            Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : 0);
                    }
                    ObjDAL.SetColumnData("Total", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["Total"]));
                    ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                    a = ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill3", true);
                }
                return a;
            }
            catch
            {
                ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID, clsConnection_DAL.ParamType.Input);
                ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Delete_Delivering_PurchaseBill");
                return a;
            }
        }
        private int DataUpdateDeliveryPurchaseBill2(int ID)
        {
            dtSize = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeMaster", "SizeID,Size,SizeTypeID", "ISNULL(ActiveStatus,1) = 1 AND SizeTypeID = " + cmbSizeType.SelectedValue, null);
            for (int i = 3; i < dtPurchaseQTYColor.Columns.Count - 1; i++)
            {
                string col = dtPurchaseQTYColor.Columns[i].ColumnName.ToString();
                DataRow[] drow = dtSize.Select("Size = '" + col + "' AND SizeTypeID=" + cmbSizeType.SelectedValue);
                if (drow.Length > 0)
                {
                    ObjDAL.UpdateColumnData("Col" + (i - 2), SqlDbType.Int, drow[0]["SizeID"].ToString());
                }
            }
            ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
            //return ObjDAL.UpdateData(clsUtility.DaBName + ".dbo.DeliveryPurchaseBill2", "DeliveryPurchaseID1=" + ID);
            ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill2", "DeliveryPurchaseID1=" + ID);
            return ObjDAL.ExecuteScalarInt("SELECT DeliveryPurchaseID2 FROM " + clsUtility.DBName + ".dbo.DeliveryPurchaseBill2 WITH(NOLOCK) WHERE DeliveryPurchaseID1=" + ID);
        }
        private int DataUpdateDeliveryPurchaseBill3(int ID1, int ID2)
        {
            int a = 0;
            for (int i = 0; i < dtPurchaseQTYColor.Rows.Count; i++)
            {
                int pDeliveryPurchaseID3 = dtPurchaseQTYColor.Rows[i]["DeliveryPurchaseID3"] != DBNull.Value ? Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["DeliveryPurchaseID3"]) : 0;
                if (pDeliveryPurchaseID3 == 0)
                {
                    DeliveryPurchaseID3 = i;// Storing index of newly record to insert
                    int b = DataSavedDeliveryPurchaseBill3(ID1, ID2);
                    continue;
                }
                ObjDAL.UpdateColumnData("ColorID", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["ColorID"]));
                for (int j = 3; j < dtPurchaseQTYColor.Columns.Count - 1; j++)
                {
                    ObjDAL.UpdateColumnData("Col" + (j - 2), SqlDbType.Int, dtPurchaseQTYColor.Rows[i][j] != DBNull.Value ? Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : 0);
                }
                ObjDAL.UpdateColumnData("Total", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["Total"]));
                ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                a = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill3", "DeliveryPurchaseID1=" + ID1 + " AND DeliveryPurchaseID3 = " + Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["DeliveryPurchaseID3"]));
            }
            return a;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //btnSearch.Enabled = true;
            ClearAll();
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpPurchaseBillDetail.Enabled = true;
            txtSupplierBillNo.Enabled = true;
            txtSupplierBillNo.Focus();
            cmbStore.SelectedValue = frmHome.Home_StoreID;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int DeliveryPurchaseBillID = 0;
            try
            {
                if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
                {
                    if (Validateform())
                    {
                        if (DuplicateUser(0))
                        {
                            ObjDAL.SetColumnData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID);
                            ObjDAL.SetColumnData("SupplierBillNo", SqlDbType.NVarChar, txtSupplierBillNo.Text.Trim());
                            ObjDAL.SetColumnData("SizeTypeID", SqlDbType.Int, cmbSizeType.SelectedValue);
                            ObjDAL.SetColumnData("ModelNo", SqlDbType.NVarChar, cmbListBox.Text);
                            ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                            ObjDAL.SetColumnData("SubProductID", SqlDbType.Int, SubProductID);
                            ObjDAL.SetColumnData("StoreID", SqlDbType.Int, cmbStore.SelectedValue);
                            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                            DeliveryPurchaseBillID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", true);
                            if (DeliveryPurchaseBillID > 0)
                            {
                                DeliveryPurchaseID3 = 0;
                                int DeliveryPurchaseBillID2 = DataSavedDeliveryPurchaseBill2(DeliveryPurchaseBillID);
                                int DeliveryPurchaseBillID3 = DataSavedDeliveryPurchaseBill3(DeliveryPurchaseBillID, DeliveryPurchaseBillID2);
                                if (DeliveryPurchaseBillID2 > 0 && DeliveryPurchaseBillID3 > 0)
                                {
                                    bool b = BarCodeGenerate();
                                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                                    clsUtility.ShowInfoMessage(clsUtility.MsgDataSaved, clsUtility.strProjectTitle);
                                }
                                else
                                {
                                    ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID, clsConnection_DAL.ParamType.Input);
                                    ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Delete_Delivering_PurchaseBill");
                                    clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved, clsUtility.strProjectTitle);
                                }
                                LoadData(); // Added
                                LoadModelData();
                                Clear_ColorSize();
                                txtSupplierBillNo.Enabled = false;
                                btnSearch.Enabled = false;
                                object qty = dtPurchaseInvoice.Compute("SUM(QTY)", null);
                                txtTotalQTY.Text = qty.ToString();
                                //grpPurchaseBillDetail.Enabled = false;
                            }
                            else
                            {
                                clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved);
                            }
                            ObjDAL.ResetData();
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("Purchase Invoice '" + txtSupplierBillNo.Text + "' is already exist..");
                            txtSupplierBillNo.Focus();
                        }
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                }
            }
            catch (Exception ex)
            {
                string temp = "Supplier Bill No.: " + txtSupplierBillNo.Text + " PurchaseInvoiceID: " + pPurchaseInvoiceID + " ProductID: " + ProductID + " SubProductID: " + SubProductID + " DeliveryPurchaseBillID1: " + DeliveryPurchaseBillID + " SizeTypeID: " + cmbSizeType.SelectedValue + " StoreID: " + cmbStore.SelectedValue + " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpPurchaseBillDetail.Enabled = true;
                txtSupplierBillNo.Focus();
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                dgvQtycolor.ReadOnly = false;
                dgvQtycolor.Enabled = true;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int DeliveryPurchaseBillID = 0;
            try
            {
                if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
                {
                    if (Validateform())
                    {
                        if (DuplicateUser(ID))
                        {
                            ObjDAL.UpdateColumnData("ProductID", SqlDbType.Int, ProductID);
                            ObjDAL.UpdateColumnData("SupplierBillNo", SqlDbType.NVarChar, txtSupplierBillNo.Text.Trim());
                            ObjDAL.UpdateColumnData("SizeTypeID", SqlDbType.Int, cmbSizeType.SelectedValue);
                            ObjDAL.UpdateColumnData("ModelNo", SqlDbType.NVarChar, cmbListBox.Text);
                            ObjDAL.UpdateColumnData("StoreID", SqlDbType.Int, cmbStore.SelectedValue);
                            ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                            ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                            DeliveryPurchaseBillID = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "DeliveryPurchaseID1=" + ID);
                            if (DeliveryPurchaseBillID > 0)
                            {
                                int DeliveryPurchaseBillID2 = DataUpdateDeliveryPurchaseBill2(ID);
                                int DeliveryPurchaseBillID3 = DataUpdateDeliveryPurchaseBill3(ID, DeliveryPurchaseBillID2);

                                bool b = BarCodeGenerate();

                                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                                clsUtility.ShowInfoMessage(clsUtility.MsgDataUpdated);
                                Clear_ColorSize();
                                //LoadData();
                                LoadModelData();
                                txtSupplierBillNo.Enabled = false;
                                btnSearch.Enabled = false;
                                //grpPurchaseBillDetail.Enabled = false;
                            }
                            else
                            {
                                clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved);
                            }
                            ObjDAL.ResetData();
                        }
                        else
                        {
                            clsUtility.ShowErrorMessage("Purchase Invoice '" + txtSupplierBillNo.Text + "' is already exist..");
                            txtSupplierBillNo.Focus();
                        }
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task");
                }
            }
            catch (Exception ex)
            {
                string temp = "Supplier Bill No.: " + txtSupplierBillNo.Text + " PurchaseInvoiceID: " + pPurchaseInvoiceID + " ProductID: " + ProductID + " SubProductID: " + SubProductID + " DeliveryPurchaseBillID: " + ID + " SizeTypeID: " + cmbSizeType.SelectedValue + " StoreID: " + cmbStore.SelectedValue + " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int pDeliveryPurchaseID3 = 0;
            try
            {
                if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
                {
                    if (dgvQtycolor.SelectedRows.Count > 0)
                    {
                        bool d = clsUtility.ShowQuestionMessage("Are you sure want to delete Color '" + dgvQtycolor.SelectedRows[0].Cells["Color"].Value + "'");
                        if (d)
                        {
                            pDeliveryPurchaseID3 = Convert.ToInt32(dgvQtycolor.SelectedRows[0].Cells["DeliveryPurchaseID3"].Value);
                            //DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Delete_PurchaseInvoice_Color_Size " + ID);
                            if (pDeliveryPurchaseID3 > 0)
                            {
                                DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Delete_PurchaseInvoice_Color_Size " + pDeliveryPurchaseID3);
                                if (ObjUtil.ValidateTable(dt))
                                {
                                    clsUtility.ShowInfoMessage("Color '" + dgvQtycolor.SelectedRows[0].Cells["Color"].Value + "' is deleted");
                                    LoadData();
                                    LoadModelData();
                                    if (ObjUtil.ValidateTable((DataTable)dataGridView1.DataSource))
                                    {
                                        Clear_ColorSize();
                                    }
                                    else
                                    {
                                        ClearAll();
                                        grpPurchaseBillDetail.Enabled = false;
                                    }
                                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                                }
                                else
                                {
                                    clsUtility.ShowInfoMessage("Color '" + dgvQtycolor.SelectedRows[0].Cells["Color"].Value + "' is not deleted");
                                }
                            }
                            else
                            {
                                DataTable dt = (DataTable)dgvQtycolor.DataSource;
                                DataRow[] drow = dt.Select("ColorID=" + dgvQtycolor.SelectedRows[0].Cells["ColorID"].Value);
                                if (drow.Length > 0)
                                {
                                    drow[0].Delete();
                                    dt.AcceptChanges();
                                    dgvQtycolor.DataSource = dt;

                                    LoadData();
                                    LoadModelData();
                                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                                }
                            }
                        }
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Select Atleast one Record to Delete.");
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                }
            }
            catch (Exception ex)
            {
                string temp = "Supplier Bill No.: " + txtSupplierBillNo.Text + " PurchaseInvoiceID: " + pPurchaseInvoiceID + " ProductID: " + ProductID + " SubProductID: " + SubProductID + " DeliveryPurchaseID3: " + pDeliveryPurchaseID3 + " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel);
            if (b)
            {
                ClearAll();
                //LoadData();
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                grpPurchaseBillDetail.Enabled = false;
            }
        }

        private bool BarCodeGenerate()
        {
            bool b = false;
            try
            {
                ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, SubProductID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, cmbStore.SelectedValue, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("SupplierBillNo", SqlDbType.VarChar, txtSupplierBillNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PurchaseInvoice_Barcode_Generated_Test");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dtItemModel = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dtItemModel))
                    {
                        b = true;
                        //clsUtility.ShowInfoMessage(dtItemModel.Rows[0]["Msg"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                string temp = "Supplier Bill No.: " + txtSupplierBillNo.Text + " PurchaseInvoiceID: " + pPurchaseInvoiceID + " ProductID: " + ProductID + " SubProductID: " + SubProductID + " StoreID: " + cmbStore.SelectedValue + " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
            return b;
        }
        private void dgvQtycolor_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvQtycolor, DataGridViewAutoSizeColumnsMode.Fill);
            if (dgvQtycolor.Columns.Contains("ColorID"))
            {
                dgvQtycolor.Columns["ColorID"].Visible = false;
            }
            if (dgvQtycolor.Columns.Contains("DeliveryPurchaseID3"))
            {
                dgvQtycolor.Columns["DeliveryPurchaseID3"].Visible = false;
            }
            if (dgvQtycolor.Columns.Contains("Total"))
            {
                dgvQtycolor.Columns["Total"].ReadOnly = true;
            }
            dgvQtycolor.AllowUserToAddRows = false;
            dgvQtycolor.AllowUserToOrderColumns = false;
            dgvQtycolor.AllowUserToResizeColumns = false;
            dgvQtycolor.AllowUserToResizeRows = false;
            dgvQtycolor.AllowUserToDeleteRows = false;
        }
        private void dgvQtycolor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }
                if (dgvQtycolor.Columns.Contains("Total"))
                {
                    txtTotalQTYEntered.Text = dgvQtycolor.Rows[e.RowIndex].Cells["Total"].Value.ToString();
                }
                CalcTotalColorQTY();
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }
        private void LoadData()
        {
            if (!ObjUtil.IsControlTextEmpty(txtPurchaseInvoiceID))
            {
                dtPurchaseInvoice = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_Delivering_PurchaseInvoice_BillDetails] " + txtPurchaseInvoiceID.Text + ",0");

                if (ObjUtil.ValidateTable(dtPurchaseInvoice))
                {
                    LoadModelListCMB();
                    grpPurchaseBillDetail.Enabled = true;
                    object qty = dtPurchaseInvoice.Compute("SUM(QTY)", null);
                    txtTotalQTY.Text = qty.ToString();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Purchase Invoice Bill is not available for Bill No. " + txtSupplierBillNo.Text);
                    grpPurchaseBillDetail.Enabled = false;
                    txtTotalQTY.Text = "0";
                    dataGridView1.DataSource = null;
                }
            }
        }
        private void LoadModelData()
        {
            if (!ObjUtil.IsControlTextEmpty(txtPurchaseInvoiceID))
            {
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
                dataGridView1.RowHeadersVisible = false; // set it to false if not needed

                DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_Delivering_PurchaseInvoice_BillDetails] " + txtPurchaseInvoiceID.Text + ",1");
                if (ObjUtil.ValidateTable(dt))
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void LoadModelListCMB()
        {
            cmbListBox.DataSource = dtPurchaseInvoice;
            cmbListBox.DisplayMember = "ModelNo";
            cmbListBox.ValueMember = "ModelNo";
            cmbListBox.SelectedIndex = -1;
        }

        private void txtSupplierBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierBillNo.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtSupplierBillNo.Text + "', 0");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSupplierBillNo, "SupplierBillNo");
                        ObjUtil.SetControlData(txtPurchaseInvoiceID, "PurchaseInvoiceID");
                        ObjUtil.ShowDataPopup(dt, txtSupplierBillNo, this, this);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            // if there is only one column                
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["PurchaseInvoiceID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["SupplierID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += Delivering_Purchase_Bill_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += Delivering_Purchase_Bill_KeyDown;
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
        private void Delivering_Purchase_Bill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();

                dgvQtycolor.Enabled = false;
                //cmbStore.Enabled = true;
                LoadData();
                LoadModelData();
            }
        }
        private void Delivering_Purchase_Bill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();

                dgvQtycolor.Enabled = false;
                cmbStore.Enabled = true;
                LoadData();
                LoadModelData();
            }
        }
        private void dgvQtycolor_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value.ToString() != string.Empty)
            //{
            //    OldColorName = dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value.ToString();
            //}
        }
        private void dgvQtycolor_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvQtycolor.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void btnStorePopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbStore.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbStore.SelectedValue);
            }
            Masters.Store_Master Obj = new Masters.Store_Master();
            Obj.ShowDialog();
            FillStoreData();
            if (a > 0)
            {
                cmbStore.SelectedValue = a;
            }
        }
        private void btnSizeTypePopup_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (cmbSizeType.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbSizeType.SelectedValue);
            }
            Masters.Size_Type_Master Obj = new Masters.Size_Type_Master();
            Obj.ShowDialog();
            FillSizeTypeData();
            if (a > 0)
            {
                cmbSizeType.SelectedValue = a;
            }
        }

        private void GetSelectedCMBModelNo()
        {
            try
            {
                DataRow[] dRow = dtPurchaseInvoice.Select("ModelNo= '" + cmbListBox.Text + "'");
                if (dRow.Length > 0)
                {
                    pPurchaseInvoiceID = Convert.ToInt32(dRow[0]["PurchaseInvoiceID"]);
                    ID = Convert.ToInt32(dRow[0]["DeliveryPurchaseID1"]);
                    ProductID = Convert.ToInt32(dRow[0]["ProductID"]);
                    SubProductID = Convert.ToInt32(dRow[0]["SubProductID"]);
                    cmbBrand.SelectedValue = dRow[0]["BrandID"];
                    cmbCountry.SelectedValue = dRow[0]["CountryID"];
                    cmbCategory.SelectedValue = dRow[0]["CategoryID"];
                    txtItemName.Text = dRow[0]["ItemName"].ToString();
                    txtTotalQTYBill.Text = dRow[0]["QTY"].ToString();

                    cmbSizeType.SelectedValue = dRow[0]["SizeTypeID"].ToString();
                    if (cmbSizeType.SelectedValue != null)
                    {
                        FillColorSizeGrid();
                    }
                    if (dRow[0]["StoreID"] != DBNull.Value && dRow[0]["StoreID"].ToString() != "0")
                    {
                        cmbStore.SelectedValue = dRow[0]["StoreID"].ToString();
                    }
                    Load_Color_SizeData();
                    if (ObjUtil.ValidateTable((DataTable)dgvQtycolor.DataSource))
                    {
                        CalcTotalColorQTY();
                    }
                }
                else
                    cmbSizeType.Enabled = false;
            }
            catch { }
        }

        private void cmbListBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(2000);

            GetSelectedCMBModelNo();

            this.Cursor = Cursors.Default;
        }

        private AutoCompleteStringCollection LoadAllColors()
        {
            AutoCompleteStringCollection str = new AutoCompleteStringCollection();
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT ColorName FROM " + clsUtility.DBName + ".dbo.ColorMaster WITH(NOLOCK) WHERE ISNULL(ActiveStatus,1)=1");
            if (ObjUtil.ValidateTable(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Add(dt.Rows[i]["ColorName"].ToString());
                }
            }
            return str;
        }

        private void Delivering_Purchase_Bill_Resize(object sender, EventArgs e)
        {
            ResizeGridView();
        }

        private void dgvQtycolor_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int column = dgvQtycolor.CurrentCell.ColumnIndex;
            string headerText = dgvQtycolor.Columns[column].HeaderText;
            if (headerText.Equals("Color"))
            {
                TextBox txt = (TextBox)e.Control;
                if (txt != null)
                {
                    dgvQtycolor.Columns["Color"].Width = 100;
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteCustomSource = LoadAllColors();
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }

        private void dgvQtycolor_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.FormattedValue.ToString() != "")
            {
                e.Cancel = false;
                dgvQtycolor.Rows[e.RowIndex].ErrorText = "";
                int newInteger = 0;
                if (dgvQtycolor.Rows[e.RowIndex].IsNewRow) { return; }
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    clsUtility.ShowInfoMessage("Enter Only Numbers..");
                    e.Cancel = true;
                    //dgvQtycolor.Rows[e.RowIndex].ErrorText = "Size must be a Positive integer";
                }
            }
        }
    }
}