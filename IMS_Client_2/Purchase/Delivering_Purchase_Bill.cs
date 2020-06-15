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
        int ProductID = 0;
        int OldSize = 0;
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
        }
        private void FillCategoryData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName", "ISNULL(ActiveStatus,1)=1", "CategoryName ASC");
            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "CategoryName";
            cmbDepartment.ValueMember = "CategoryID";

            cmbDepartment.SelectedIndex = -1;
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

            //clsUtility.IsAdmin = true;//removed

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning, clsUtility.IsAdmin);

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
            ObjUtil.SetTextHighlightColor(sender, Color.White);
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
        private void listBoxModelNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataRow[] dRow = dtPurchaseInvoice.Select("ModelNo= '" + listBoxModelNo.SelectedItem + "'");
            if (dRow.Length > 0)
            {
                //cmbSizeType.Enabled = true;
                pPurchaseInvoiceID = Convert.ToInt32(dRow[0]["PurchaseInvoiceID"]);
                ID = Convert.ToInt32(dRow[0]["DeliveryPurchaseID1"]);
                ProductID = Convert.ToInt32(dRow[0]["ProductID"]);
                cmbBrand.SelectedValue = dRow[0]["BrandID"];
                cmbCountry.SelectedValue = dRow[0]["CountryID"];
                cmbDepartment.SelectedValue = dRow[0]["CategoryID"];
                txtItemName.Text = dRow[0]["ProductName"].ToString();
                txtTotalQTYBill.Text = dRow[0]["QTY"].ToString();
                cmbSizeType.SelectedValue = dRow[0]["SizeTypeID"].ToString();
                cmbStore.SelectedValue = dRow[0]["StoreID"].ToString();
                Load_Color_SizeData();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            //ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["PurchaseInvoiceDetailsID"].Visible = false;
            dataGridView1.Columns["PurchaseInvoiceID"].Visible = false;
            dataGridView1.Columns["DeliveryPurchaseID1"].Visible = false;
            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["BrandID"].Visible = false;
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CategoryID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
            dataGridView1.Columns["SizeTypeID"].Visible = false;
            dataGridView1.Columns["StoreID"].Visible = false;
            //lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void cmbSizeType_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void Load_Color_SizeData()
        {
            dtPurchaseQTYColor = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_PurchaseInvoice_Color_Size] '" + txtSupplierBillNo.Text + "', " + listBoxModelNo.SelectedItem + "");
            if (ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                dgvQtycolor.DataSource = null;
                dgvQtycolor.DataSource = dtPurchaseQTYColor;
                dgvQtycolor.Refresh();
                dgvQtycolor.ReadOnly = true;
                cmbSizeType.Enabled = false;
            }
            else
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
                dgvQtycolor.ReadOnly = false;
                dgvQtycolor.DataSource = null;
                cmbSizeType.Enabled = true;
            }
        }
        private void listBoxModelNo_MouseClick(object sender, MouseEventArgs e)
        {
            DataRow[] dRow = dtPurchaseInvoice.Select("ModelNo= '" + listBoxModelNo.SelectedItem + "'");
            if (dRow.Length > 0)
            {
                //cmbSizeType.Enabled = true;
                pPurchaseInvoiceID = Convert.ToInt32(dRow[0]["PurchaseInvoiceID"]);
                ID = Convert.ToInt32(dRow[0]["DeliveryPurchaseID1"]);
                ProductID = Convert.ToInt32(dRow[0]["ProductID"]);
                cmbBrand.SelectedValue = dRow[0]["BrandID"];
                cmbCountry.SelectedValue = dRow[0]["CountryID"];
                cmbDepartment.SelectedValue = dRow[0]["CategoryID"];
                txtItemName.Text = dRow[0]["ProductName"].ToString();
                txtTotalQTYBill.Text = dRow[0]["QTY"].ToString();
                cmbSizeType.SelectedValue = dRow[0]["SizeTypeID"].ToString();
                cmbStore.SelectedValue = dRow[0]["StoreID"].ToString();
                Load_Color_SizeData();
            }
            else
                cmbSizeType.Enabled = false;
        }
        private void dgvQtycolor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int pTotalEnteredQTY = 0;
            if (e.ColumnIndex == 0)
            {
                int a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.ColorMaster", "ColorName = '" + dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value + "' AND ISNULL(ActiveStatus,1) = 1");
                if (a <= 0)
                {
                    flag = 1;
                    clsUtility.ShowInfoMessage("Color Name " + dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value + " is not present in Color Master", clsUtility.strProjectTitle);
                    dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value = OldColorName;
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
                    clsUtility.ShowInfoMessage("Entered QTY can't be greater then QTY Bill", clsUtility.strProjectTitle);
                    dgvQtycolor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = OldSize;
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
        private void ClearAll()
        {
            txtPurchaseInvoiceID.Clear();
            txtSupplierBillNo.Clear();
            txtItemName.Clear();
            txtDiffQty.Text = "0";
            txtTotalQTY.Text = "0";
            txtTotalQTYBill.Text = "0";
            txtTotalQTYEntered.Text = "0";
            cmbBrand.SelectedIndex = -1;
            cmbCountry.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            cmbSizeType.SelectedIndex = -1;
            cmbStore.SelectedIndex = -1;
            listBoxModelNo.Items.Clear();
            dtPurchaseQTYColor.Clear();
            dtPurchaseInvoice.Clear();
            dgvQtycolor.DataSource = null;
            //dataGridView1.DataSource = null;
        }
        private void Clear_ColorSize()
        {
            //txtPurchaseInvoiceID.Clear();
            txtItemName.Clear();
            txtDiffQty.Text = "0";
            txtTotalQTY.Text = "0";
            txtTotalQTYBill.Text = "0";
            txtTotalQTYEntered.Text = "0";
            cmbSizeType.SelectedIndex = -1;
            dtPurchaseQTYColor.Clear();
            dgvQtycolor.DataSource = null;
            listBoxModelNo.ClearSelected();
        }
        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ", clsUtility.strProjectTitle);
                txtSupplierBillNo.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbStore))
            {
                clsUtility.ShowInfoMessage("Select Store for Supplier Bill No " + txtSupplierBillNo.Text, clsUtility.strProjectTitle);
                cmbStore.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbSizeType))
            {
                clsUtility.ShowInfoMessage("Select Size Type           ", clsUtility.strProjectTitle);
                cmbSizeType.Focus();
                return false;
            }
            else if (!ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                clsUtility.ShowInfoMessage("Enter Color and Qty           ", clsUtility.strProjectTitle);
                dgvQtycolor.Focus();
                return false;
            }
            else if (ObjUtil.ValidateTable(dtPurchaseQTYColor))
            {
                for (int i = 0; i < dtPurchaseQTYColor.Rows.Count; i++)
                {
                    if (dtPurchaseQTYColor.Rows[i]["Color"] == DBNull.Value)
                    {
                        dtPurchaseQTYColor.Rows[i].Delete();
                    }
                    else if (dtPurchaseQTYColor.Rows[i]["ColorID"] == DBNull.Value)
                    {
                        clsUtility.ShowInfoMessage("Enter Valid Colour Name.         ", clsUtility.strProjectTitle);
                        return false;
                    }
                }
                dtPurchaseQTYColor.AcceptChanges();
                return true;
            }
            if (Convert.ToInt32(txtDiffQty) < 0)
            {
                clsUtility.ShowInfoMessage("Entered QTY can't be greater then Billed QTY", clsUtility.strProjectTitle);
                dgvQtycolor.Focus();
                return false;
            }
            return true;
        }
        private bool DuplicateUser(int i)
        {
            string listbox = listBoxModelNo.SelectedItem.ToString();
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "SizeTypeID=" + cmbSizeType.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "' AND ModelNo='" + listBoxModelNo.SelectedItem + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "SizeTypeID=" + cmbSizeType.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "'" +
                    "AND ModelNo='" + listBoxModelNo.SelectedItem + "' AND DeliveryPurchaseID1 !=" + i);
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
            ObjDAL.SetColumnData("DeliveryPurchaseID1", SqlDbType.Int, ID);
            for (int i = 3; i < dtPurchaseQTYColor.Columns.Count - 1; i++)
            {
                //ObjDAL.SetColumnData("Col" + (i - 2), SqlDbType.VarChar, dtPurchaseQTYColor.Columns[i].ColumnName);
                string col = dtPurchaseQTYColor.Columns[i].ColumnName.ToString();
                DataRow[] drow = dtSize.Select("Size = '" + col + "' AND SizeTypeID=" + cmbSizeType.SelectedValue);
                if (drow.Length > 0)
                {
                    ObjDAL.SetColumnData("Col" + (i - 2), SqlDbType.Int, drow[0]["SizeID"].ToString());
                }
                //ObjDAL.SetColumnData("Col" + (i - 2), SqlDbType.VarChar, dtPurchaseQTYColor.Columns[i].ColumnName);
            }
            ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            return ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill2", true);
        }

        private int DataSavedDeliveryPurchaseBill3(int ID1, int ID2)
        {
            int a = 0;
            for (int i = 0; i < dtPurchaseQTYColor.Rows.Count; i++)
            {
                ObjDAL.SetColumnData("DeliveryPurchaseID1", SqlDbType.Int, ID1);
                ObjDAL.SetColumnData("DeliveryPurchaseID2", SqlDbType.Int, ID2);
                ObjDAL.SetColumnData("ColorID", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["ColorID"]));
                for (int j = 3; j < dtPurchaseQTYColor.Columns.Count - 1; j++)
                {
                    ObjDAL.SetColumnData("Col" + (j - 2), SqlDbType.Int, !string.IsNullOrEmpty(dtPurchaseQTYColor.Rows[i][j].ToString()) ?
                        Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : 0);
                    //ObjDAL.SetColumnData("Col" + (j - 2), SqlDbType.Int, !string.IsNullOrEmpty(dtPurchaseQTYColor.Rows[i][j].ToString()) ?
                    //    Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : (int?)null);
                }
                ObjDAL.SetColumnData("Total", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["Total"]));
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                a = ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill3", true);
            }
            return a;
        }
        private int DataUpdateDeliveryPurchaseBill2(int ID)
        {
            dtSize = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SizeMaster", "SizeID,Size,SizeTypeID", "ISNULL(ActiveStatus,1) = 1 AND SizeTypeID = " + cmbSizeType.SelectedValue, null);
            for (int i = 3; i < dtPurchaseQTYColor.Columns.Count - 1; i++)
            {
                //ObjDAL.UpdateColumnData("Col" + (i - 2), SqlDbType.VarChar, dtPurchaseQTYColor.Columns[i].ColumnName);
                string col = dtPurchaseQTYColor.Columns[i].ColumnName.ToString();
                DataRow[] drow = dtSize.Select("Size = '" + col + "' AND SizeTypeID=" + cmbSizeType.SelectedValue);
                if (drow.Length > 0)
                {
                    ObjDAL.UpdateColumnData("Col" + (i - 2), SqlDbType.Int, drow[0]["SizeID"].ToString());
                }
            }
            ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
            ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
            return ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill2", "DeliveryPurchaseID1=" + ID);
        }
        private int DataUpdateDeliveryPurchaseBill3(int ID1, int ID2)
        {
            int a = 0;
            for (int i = 0; i < dtPurchaseQTYColor.Rows.Count; i++)
            {
                ObjDAL.UpdateColumnData("ColorID", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["ColorID"]));
                for (int j = 3; j < dtPurchaseQTYColor.Columns.Count - 1; j++)
                {
                    ObjDAL.UpdateColumnData("Col" + (j - 2), SqlDbType.Int, dtPurchaseQTYColor.Rows[i][j] != DBNull.Value ? Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : 0);
                    //ObjDAL.UpdateColumnData("Col" + (j - 2), SqlDbType.Int, dtPurchaseQTYColor.Rows[i][j] != DBNull.Value ? Convert.ToInt32(dtPurchaseQTYColor.Rows[i][j]) : (int?)null);
                }
                ObjDAL.UpdateColumnData("Total", SqlDbType.Int, Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["Total"]));
                ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                a = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill3", "DeliveryPurchaseID1=" + ID1 + " AND DeliveryPurchaseID2=" + ID2 + " AND DeliveryPurchaseID3 = " + Convert.ToInt32(dtPurchaseQTYColor.Rows[i]["DeliveryPurchaseID3"]));
            }
            return a;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtSupplierBillNo.Enabled = true;
            btnSearch.Enabled = true;
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpPurchaseBillDetail.Enabled = true;
            txtSupplierBillNo.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        int DeliveryPurchaseBillID = 0;
                        ObjDAL.SetColumnData("PurchaseInvoiceID", SqlDbType.Int, pPurchaseInvoiceID);
                        ObjDAL.SetColumnData("SupplierBillNo", SqlDbType.NVarChar, txtSupplierBillNo.Text.Trim());
                        ObjDAL.SetColumnData("SizeTypeID", SqlDbType.Int, cmbSizeType.SelectedValue);
                        ObjDAL.SetColumnData("ModelNo", SqlDbType.NVarChar, listBoxModelNo.SelectedItem);
                        ObjDAL.SetColumnData("ProductID", SqlDbType.Int, ProductID);
                        ObjDAL.SetColumnData("StoreID", SqlDbType.Int, cmbStore.SelectedValue);
                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        DeliveryPurchaseBillID = ObjDAL.InsertData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", true);
                        if (DeliveryPurchaseBillID > 0)
                        {
                            int DeliveryPurchaseBillID2 = DataSavedDeliveryPurchaseBill2(DeliveryPurchaseBillID);
                            int DeliveryPurchaseBillID3 = DataSavedDeliveryPurchaseBill3(DeliveryPurchaseBillID, DeliveryPurchaseBillID2);
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage(clsUtility.MsgDataSaved, clsUtility.strProjectTitle);
                            Clear_ColorSize();
                            //LoadData();
                            LoadModelData();
                            txtSupplierBillNo.Enabled = false;
                            btnSearch.Enabled = false;
                            //grpPurchaseBillDetail.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved, clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Purchase Invoice '" + txtSupplierBillNo.Text + "' is already exist..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                        txtSupplierBillNo.Focus();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpPurchaseBillDetail.Enabled = true;
                txtSupplierBillNo.Focus();
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                dgvQtycolor.ReadOnly = false;
                dgvQtycolor.Enabled = true;
                //dgvQtycolor.AllowUserToAddRows = true;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        int DeliveryPurchaseBillID = 0;
                        ObjDAL.UpdateColumnData("ProductID", SqlDbType.Int, ProductID);
                        ObjDAL.UpdateColumnData("SupplierBillNo", SqlDbType.NVarChar, txtSupplierBillNo.Text.Trim());
                        ObjDAL.UpdateColumnData("SizeTypeID", SqlDbType.Int, cmbSizeType.SelectedValue);
                        ObjDAL.UpdateColumnData("ModelNo", SqlDbType.NVarChar, listBoxModelNo.SelectedItem);
                        ObjDAL.UpdateColumnData("StoreID", SqlDbType.Int, cmbStore.SelectedValue);
                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                        DeliveryPurchaseBillID = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.DeliveryPurchaseBill1", "DeliveryPurchaseID1=" + ID);
                        if (DeliveryPurchaseBillID > 0)
                        {
                            int DeliveryPurchaseBillID2 = DataUpdateDeliveryPurchaseBill2(ID);
                            int DeliveryPurchaseBillID3 = DataUpdateDeliveryPurchaseBill3(ID, ID);
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage(clsUtility.MsgDataUpdated, clsUtility.strProjectTitle);
                            Clear_ColorSize();
                            //LoadData();
                            LoadModelData();
                            txtSupplierBillNo.Enabled = false;
                            btnSearch.Enabled = false;
                            //grpPurchaseBillDetail.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage(clsUtility.MsgDatanotSaved, clsUtility.strProjectTitle);
                            ObjDAL.ResetData();
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Purchase Invoice '" + txtSupplierBillNo.Text + "' is already exist..", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                        txtSupplierBillNo.Focus();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Delivering_Purchase_Bill, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                bool d = clsUtility.ShowQuestionMessage("Are you sure want to delete Supplier Bill No. '" + txtSupplierBillNo.Text + "'", clsUtility.strProjectTitle);
                if (d)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Delete_PurchaseInvoice_Color_Size " + ID);
                    if (ObjUtil.ValidateTable(dt))
                    {
                        clsUtility.ShowInfoMessage("Supplier Bill No. '" + txtSupplierBillNo.Text + "' is deleted", clsUtility.strProjectTitle);
                        ClearAll();
                        //LoadData();
                        LoadModelData();
                        grpPurchaseBillDetail.Enabled = false;
                        //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete, clsUtility.IsAdmin);
                        ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterDelete);
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Supplier Bill No. '" + txtSupplierBillNo.Text + "' is not deleted", clsUtility.strProjectTitle);
                        ObjDAL.ResetData();
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage(clsUtility.MsgActionCancel, clsUtility.strProjectTitle);
            if (b)
            {
                ClearAll();
                //LoadData();
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                grpPurchaseBillDetail.Enabled = false;
            }
        }
        private void dgvQtycolor_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //ObjUtil.SetDataGridProperty(dgvQtycolor, DataGridViewAutoSizeColumnsMode.ColumnHeader);
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
                if (dgvQtycolor.Columns.Contains("Total"))
                {
                    txtTotalQTYEntered.Text = dgvQtycolor.Rows[e.RowIndex].Cells["Total"].Value.ToString();
                }
                CalcTotalColorQTY();
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void LoadData()
        {
            if (!ObjUtil.IsControlTextEmpty(txtPurchaseInvoiceID))
            {
                dtPurchaseInvoice = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_Delivering_PurchaseInvoice_BillDetails] " + txtPurchaseInvoiceID.Text + ",0");

                if (ObjUtil.ValidateTable(dtPurchaseInvoice))
                {
                    listBoxModelNo.Items.Clear();
                    for (int i = 0; i < dtPurchaseInvoice.Rows.Count; i++)
                    {
                        listBoxModelNo.Items.Add(dtPurchaseInvoice.Rows[i]["ModelNo"].ToString());
                    }
                    grpPurchaseBillDetail.Enabled = true;
                    //dataGridView1.DataSource = dtPurchaseInvoice;
                    object qty = dtPurchaseInvoice.Compute("SUM(QTY)", null);
                    txtTotalQTY.Text = qty.ToString();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Purchase Invoice Bill is already done OR not available for Bill No. " + txtSupplierBillNo.Text, clsUtility.strProjectTitle);
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
                //dtPurchaseInvoice = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_Delivering_PurchaseInvoice_BillDetails] " + txtPurchaseInvoiceID.Text + ",1");

                DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".[dbo].[Get_Delivering_PurchaseInvoice_BillDetails] " + txtPurchaseInvoiceID.Text + ",1");

                if (ObjUtil.ValidateTable(dt))
                {
                    //dtPurchaseInvoice = dt;
                    //dataGridView1.DataSource = dtPurchaseInvoice;
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    //clsUtility.ShowInfoMessage("Purchase Invoice Bill is already done OR not available for Bill No. " + txtSupplierBillNo.Text, clsUtility.strProjectTitle);
                    //grpPurchaseBillDetail.Enabled = false;
                    //txtTotalQTY.Text = "0";
                    dataGridView1.DataSource = null;
                }
            }
        }
        private void txtSupplierBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierBillNo.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup " + txtSupplierBillNo.Text + ",0");
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
                cmbStore.Enabled = true;
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
            OldColorName = dgvQtycolor.Rows[e.RowIndex].Cells["Color"].Value.ToString();
            if (e.ColumnIndex > 2)
            {
                OldSize = dgvQtycolor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value ?
                    Convert.ToInt32(dgvQtycolor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) : 0;
            }
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
        private void dgvQtycolor_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.FormattedValue.ToString() != "")
            {
                dgvQtycolor.Rows[e.RowIndex].ErrorText = "";
                int newInteger = 0;
                if (dgvQtycolor.Rows[e.RowIndex].IsNewRow) { return; }
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    dgvQtycolor.Rows[e.RowIndex].ErrorText = "Size must be a Positive integer";
                }
            }
        }
    }
}