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
    public partial class Purchase_Invoice : Form
    {
        public Purchase_Invoice()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        int ID = 0;
        bool IsInvoiceDone = false;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void ClearAll()
        {
            txtSupplierBillNo.Clear();
            txtShipmentNo.Clear();
            txtBillValue.Clear();
            txtTotalQTY.Clear();

            txtCurrencyRate.Clear();
            txtForeignDiscount.Clear();
            txtForeignExp.Clear();
            txtLocalBillValue.Clear();
            txtLocalExp.Clear();
            txtLocalValue.Clear();
            txtNetValue.Clear();
            txtNewPriceRate.Clear();

            cmbSupplier.SelectedIndex = -1;
            cmbCountry.SelectedIndex = -1;

            txtSupplierBillNo.Focus();

            grpCurrencyRate.Enabled = false;
            grpForeignCurrency.Enabled = false;
            grpLocalCurrency.Enabled = false;

            IsInvoiceDone = false;
            ID = 0;
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ", clsUtility.strProjectTitle);
                txtSupplierBillNo.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtShipmentNo))
            {
                clsUtility.ShowInfoMessage("Enter Shipment No           ", clsUtility.strProjectTitle);
                txtShipmentNo.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(cmbSupplier))
            {
                clsUtility.ShowInfoMessage("Select Supplier for " + txtShipmentNo.Text, clsUtility.strProjectTitle);
                cmbSupplier.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtBillValue))
            {
                clsUtility.ShowInfoMessage("Enter Bill Value           ", clsUtility.strProjectTitle);
                txtBillValue.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtTotalQTY))
            {
                clsUtility.ShowInfoMessage("Enter Total Qty           ", clsUtility.strProjectTitle);
                txtTotalQTY.Focus();
                return false;
            }
            else if (ObjUtil.IsControlTextEmpty(txtLocalValue))
            {
                clsUtility.ShowInfoMessage("Local Value can't be empty           ", clsUtility.strProjectTitle);
                return false;
            }
            else if (ObjUtil.IsNumeric(txtLocalValue.Text) && Convert.ToDecimal(txtLocalValue.Text) == 0.0M)
            {
                clsUtility.ShowInfoMessage("Local Value can't be 0           ", clsUtility.strProjectTitle);
                return false;
            }
            return true;
        }

        private bool DuplicateUser(int i)
        {
            int a = 0;
            if (i == 0)
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.PurchaseInvoice", "SupplierID=" + cmbSupplier.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "'");
            }
            else
            {
                a = ObjDAL.CountRecords(clsUtility.DBName + ".dbo.PurchaseInvoice", "SupplierID=" + cmbSupplier.SelectedValue + " AND SupplierBillNo='" + txtSupplierBillNo.Text + "'" +
                    " AND PurchaseInvoiceID !=" + i);
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

        private void LoadData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.PurchaseInvoice", "PurchaseInvoiceID,SupplierBillNo,SupplierID,ShipmentNo,BillDate,BillValue,TotalQTY,Discount,ForeignExp,GrandTotal,LocalValue,LocalExp,LocalBillValue,IsInvoiceDone", "BillDate DESC");

            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void FillSupplierData()
        {
            int a = 0;
            if (cmbSupplier.SelectedIndex >= 0)
            {
                a = Convert.ToInt32(cmbSupplier.SelectedValue);
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID,SupplierName", "ISNULL(ActiveStatus,1)=1", "SupplierName ASC");
            cmbSupplier.DataSource = dt;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";

            if (a > 0)
                cmbSupplier.SelectedValue = a;
            else
                cmbSupplier.SelectedIndex = -1;
        }

        private void FillCountryData()
        {
            int CountryID = 0;
            if (cmbCountry.SelectedIndex >= 0)
            {
                CountryID = Convert.ToInt32(cmbCountry.SelectedValue);
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CountryMaster", "CountryID,CountryName", "ISNULL(ActiveStatus,1)=1", "CountryName ASC");
            cmbCountry.DataSource = dt;
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";

            if (CountryID > 0)
                cmbCountry.SelectedValue = CountryID;
            else
                cmbCountry.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearAll();
            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew, clsUtility.IsAdmin);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterNew);
            grpPurchaseInvoice.Enabled = true;
            grpForeignCurrency.Enabled = true;
            grpLocalCurrency.Enabled = true;
            txtSupplierBillNo.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Invoice, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(0))
                    {
                        ObjDAL.SetColumnData("SupplierBillNo", SqlDbType.NVarChar, txtSupplierBillNo.Text.Trim());
                        ObjDAL.SetColumnData("ShipmentNo", SqlDbType.NVarChar, txtShipmentNo.Text.Trim());
                        ObjDAL.SetColumnData("BillValue", SqlDbType.Decimal, txtBillValue.Text.Trim());
                        ObjDAL.SetColumnData("TotalQTY", SqlDbType.Int, txtTotalQTY.Text.Trim());
                        ObjDAL.SetColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                        ObjDAL.SetColumnData("BillDate", SqlDbType.Date, dtpBillDate.Value.ToString("yyyy-MM-dd"));
                        ObjDAL.SetColumnData("Discount", SqlDbType.Decimal, txtForeignDiscount.Text.Length > 0 ? Convert.ToDecimal(txtForeignDiscount.Text) : 0);
                        ObjDAL.SetColumnData("ForeignExp", SqlDbType.Decimal, txtForeignExp.Text.Length > 0 ? Convert.ToDecimal(txtForeignExp.Text) : 0);
                        ObjDAL.SetColumnData("GrandTotal", SqlDbType.Decimal, txtNetValue.Text.Trim());

                        ObjDAL.SetColumnData("LocalValue", SqlDbType.Decimal, txtLocalValue.Text.Trim());
                        ObjDAL.SetColumnData("LocalExp", SqlDbType.Decimal, txtLocalExp.Text.Length > 0 ? Convert.ToDecimal(txtLocalExp.Text) : 0);
                        ObjDAL.SetColumnData("LocalBillValue", SqlDbType.Decimal, txtLocalBillValue.Text.Trim());

                        ObjDAL.SetColumnData("CreatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test Admin else user
                        if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.PurchaseInvoice", true) > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterSave);
                            clsUtility.ShowInfoMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is Saved Successfully..", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpPurchaseInvoice.Enabled = false;
                            grpForeignCurrency.Enabled = false;
                            grpLocalCurrency.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is not Saved Successfully..", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is already exist..", clsUtility.strProjectTitle);
                        cmbSupplier.Focus();
                    }
                    ObjDAL.ResetData();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Invoice, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterEdit);
                grpPurchaseInvoice.Enabled = true;
                txtSupplierBillNo.Focus();
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;

                grpForeignCurrency.Enabled = true;
                grpLocalCurrency.Enabled = true;
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Invoice, clsFormRights.Operation.Update) || clsUtility.IsAdmin)
            {
                if (Validateform())
                {
                    if (DuplicateUser(ID))
                    {
                        ObjDAL.UpdateColumnData("SupplierBillNo", SqlDbType.VarChar, txtSupplierBillNo.Text.Trim());
                        ObjDAL.UpdateColumnData("ShipmentNo", SqlDbType.VarChar, txtShipmentNo.Text.Trim());
                        ObjDAL.UpdateColumnData("BillValue", SqlDbType.Decimal, txtBillValue.Text.Trim());
                        ObjDAL.UpdateColumnData("TotalQTY", SqlDbType.Int, txtTotalQTY.Text.Trim());
                        ObjDAL.UpdateColumnData("SupplierID", SqlDbType.Int, cmbSupplier.SelectedValue);
                        ObjDAL.UpdateColumnData("BillDate", SqlDbType.Date, dtpBillDate.Value.ToString("yyyy-MM-dd"));
                        ObjDAL.UpdateColumnData("Discount", SqlDbType.Decimal, txtForeignDiscount.Text.Length > 0 ? Convert.ToDecimal(txtForeignDiscount.Text) : 0);
                        ObjDAL.UpdateColumnData("ForeignExp", SqlDbType.Decimal, txtForeignExp.Text.Length > 0 ? Convert.ToDecimal(txtForeignExp.Text) : 0);
                        ObjDAL.UpdateColumnData("GrandTotal", SqlDbType.Decimal, txtNetValue.Text.Trim());

                        ObjDAL.UpdateColumnData("LocalValue", SqlDbType.Decimal, txtLocalValue.Text.Trim());
                        ObjDAL.UpdateColumnData("LocalExp", SqlDbType.Decimal, txtLocalExp.Text.Length > 0 ? Convert.ToDecimal(txtLocalExp.Text) : 0);
                        ObjDAL.UpdateColumnData("LocalBillValue", SqlDbType.Decimal, txtLocalBillValue.Text.Trim());

                        ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID); //if LoginID=0 then Test
                        ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);

                        if (ObjDAL.UpdateData(clsUtility.DBName + ".dbo.PurchaseInvoice", "PurchaseInvoiceID = " + ID + "") > 0)
                        {
                            //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate, clsUtility.IsAdmin);
                            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterUpdate);

                            clsUtility.ShowInfoMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is Updated", clsUtility.strProjectTitle);
                            LoadData();
                            ClearAll();
                            grpPurchaseInvoice.Enabled = false;

                            grpPurchaseInvoice.Enabled = false;
                            grpForeignCurrency.Enabled = false;
                            grpLocalCurrency.Enabled = false;
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is not Updated", clsUtility.strProjectTitle);
                        }
                    }
                    else
                    {
                        clsUtility.ShowErrorMessage("Purchase Invoice for '" + cmbSupplier.Text + "' is already exist..", clsUtility.strProjectTitle);
                        cmbSupplier.Focus();
                    }
                    ObjDAL.ResetData();
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Purchase_Invoice, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                if (!IsInvoiceDone)
                {
                    DialogResult d = MessageBox.Show("Are you sure want to delete Supplier Bill No. '" + txtSupplierBillNo.Text + "'", clsUtility.strProjectTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (d == DialogResult.Yes)
                    {
                        ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, ID, clsConnection_DAL.ParamType.Input);
                        DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Delete_PurchaseInvoice");
                        //if (ObjDAL.DeleteData(clsUtility.DBName + ".dbo.PurchaseInvoice", "PurchaseInvoiceID=" + ID + "") > 0)
                        if (ObjUtil.ValidateTable(ds.Tables[0]))
                        {
                            clsUtility.ShowInfoMessage("Supplier Bill No. '" + txtSupplierBillNo.Text + "' is deleted", clsUtility.strProjectTitle);
                            ClearAll();
                            LoadData();
                            grpPurchaseInvoice.Enabled = false;
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
                    clsUtility.ShowInfoMessage("Supplier Bill No. '" + txtSupplierBillNo.Text + "' is Posted.\n You can't be delete Posted Invoice.", clsUtility.strProjectTitle);
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
                LoadData();
                //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel, clsUtility.IsAdmin);
                ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterCancel);
                grpPurchaseInvoice.Enabled = false;
            }
        }

        private void txtSupplierBillNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSupplierBillNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            {
                try
                {
                    //ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick, clsUtility.IsAdmin);
                    ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.AfterGridClick);
                    ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PurchaseInvoiceID"].Value);
                    IsInvoiceDone = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells["IsInvoiceDone"].Value);

                    txtSupplierBillNo.Text = dataGridView1.SelectedRows[0].Cells["SupplierBillNo"].Value.ToString();
                    cmbSupplier.SelectedValue = dataGridView1.SelectedRows[0].Cells["SupplierID"].Value.ToString();

                    cmbSupplier_SelectionChangeCommitted(sender, e);

                    txtShipmentNo.Text = dataGridView1.SelectedRows[0].Cells["ShipmentNo"].Value.ToString();
                    txtBillValue.Text = dataGridView1.SelectedRows[0].Cells["BillValue"].Value.ToString();
                    txtTotalQTY.Text = dataGridView1.SelectedRows[0].Cells["TotalQTY"].Value.ToString();

                    txtForeignDiscount.Text = dataGridView1.SelectedRows[0].Cells["Discount"].Value.ToString();
                    txtForeignExp.Text = dataGridView1.SelectedRows[0].Cells["ForeignExp"].Value.ToString();
                    txtNetValue.Text = dataGridView1.SelectedRows[0].Cells["GrandTotal"].Value.ToString();

                    txtLocalValue.Text = dataGridView1.SelectedRows[0].Cells["LocalValue"].Value.ToString();
                    txtLocalExp.Text = dataGridView1.SelectedRows[0].Cells["LocalExp"].Value.ToString();
                    txtLocalBillValue.Text = dataGridView1.SelectedRows[0].Cells["LocalBillValue"].Value.ToString();

                    dtpBillDate.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["BillDate"].Value);

                    grpPurchaseInvoice.Enabled = false;
                    txtSupplierBillNo.Focus();
                    if (IsInvoiceDone)
                    {
                        EnableDisableControl(false);
                    }
                    else
                    {
                        EnableDisableControl(true);
                    }

                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
                }
            }
        }
        private void EnableDisableControl(bool b)
        {
            txtTotalQTY.Enabled = b;
            txtBillValue.Enabled = b;
            dtpBillDate.Enabled = b;
        }
        private void txtSupplierBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtShipmentNo.Focus();
                return;
            }
        }

        private void txtShipmentNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbSupplier.Focus();
                return;
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtShipmentNo.Focus();
                return;
            }
        }

        private void txtBillValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtTotalQTY.Focus();
                return;
            }
        }

        private void txtTotalQTY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtBillValue.Focus();
                return;
            }
        }

        private void Purchase_Invoice_Load(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = B_Leave;
            btnSave.BackgroundImage = B_Leave;
            btnEdit.BackgroundImage = B_Leave;
            btnUpdate.BackgroundImage = B_Leave;
            btnDelete.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            ObjUtil.RegisterCommandButtons(btnAdd, btnSave, btnEdit, btnUpdate, btnDelete, btnCancel);
            ObjUtil.SetCommandButtonStatus(clsCommon.ButtonStatus.Beginning);

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dataGridView1.RowHeadersVisible = false; // set it to false if not needed

            LoadData();

            FillSupplierData();
            FillCountryData();
            dtpBillDate.MaxDate = DateTime.Now;
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

        private void rdSearchByShipment_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByShipment.Checked)
            {
                txtSearchByShipmentNo.Enabled = true;
                txtSearchByShipmentNo.Focus();
            }
            else
            {
                txtSearchByShipmentNo.Enabled = false;
                txtSearchByShipmentNo.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtSearchByShipmentNo.Enabled = false;
                txtSearchByShipmentNo.Clear();
                txtSearchByBillNo.Enabled = false;
                txtSearchByBillNo.Clear();
                LoadData();
            }
        }

        private void txtSearchByShipmentNo_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByShipmentNo.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.PurchaseInvoice", "PurchaseInvoiceID,SupplierBillNo,SupplierID,ShipmentNo,BillDate,BillValue,TotalQTY,Discount,ForeignExp,GrandTotal,LocalValue,LocalExp,LocalBillValue,IsInvoiceDone", "ShipmentNo LIKE '%" + txtSearchByShipmentNo.Text + "%'", "BillDate DESC");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            //ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["PurchaseInvoiceID"].Visible = false;
            dataGridView1.Columns["IsInvoiceDone"].Visible = false;
            dataGridView1.Columns["SupplierID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void CalculateTotalBill()
        {
            decimal CurrencyRate = 0.0M;
            decimal BillValue = 0.0M;
            decimal LocalValue = 0.0M;

            decimal ForeignExp = 0.0M;
            decimal ForeignDiscount = 0.0M;
            decimal ForeignNetValue = 0.0M;

            decimal LocalExp = 0.0M;
            decimal LocalNewPriceRate = 0.0M;
            decimal LocalBillValue = 0.0M;

            try
            {
                CurrencyRate = txtCurrencyRate.Text.Length > 0 ? Convert.ToDecimal(txtCurrencyRate.Text) : 0;
                BillValue = txtBillValue.Text.Length > 0 ? Convert.ToDecimal(txtBillValue.Text) : 0;
                ForeignExp = txtForeignExp.Text.Length > 0 ? Convert.ToDecimal(txtForeignExp.Text) : 0;
                ForeignDiscount = txtForeignDiscount.Text.Length > 0 ? Convert.ToDecimal(txtForeignDiscount.Text) : 0;

                LocalExp = txtLocalExp.Text.Length > 0 ? Convert.ToDecimal(txtLocalExp.Text) : 0;
                LocalValue = (BillValue * CurrencyRate);
                txtLocalValue.Text = Math.Round(LocalValue, 2).ToString();

                ForeignNetValue = BillValue - (BillValue * ForeignDiscount * 0.01M) + ForeignExp;
                txtNetValue.Text = Math.Round(ForeignNetValue, 2).ToString();

                LocalBillValue = Math.Round((ForeignNetValue * CurrencyRate) + LocalExp, 2);
                txtLocalBillValue.Text = Math.Round(LocalBillValue, 2).ToString();
                if (BillValue > 0)
                {
                    LocalNewPriceRate = Math.Round((LocalBillValue / BillValue), 2);
                }
                txtNewPriceRate.Text = LocalNewPriceRate.ToString();
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void cmbCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedIndex >= 0)
            {
                object ob = ObjDAL.ExecuteScalar("SELECT CurrencyRate FROM " + clsUtility.DBName + ".dbo.CurrencyRateSetting WITH(NOLOCK) WHERE CountryID = " + cmbCountry.SelectedValue);
                txtCurrencyRate.Text = Convert.ToDecimal(ob).ToString();
                if (ob == null)
                {
                    clsUtility.ShowInfoMessage("Currency Rate is defined for Country " + cmbCountry.Text, clsUtility.strProjectTitle);
                    btnCurrencyRatePopup.Enabled = true;
                }
                else
                    btnCurrencyRatePopup.Enabled = false;
            }
        }

        private void txtBillValue_TextChanged(object sender, EventArgs e)
        {
            if (txtBillValue.Text.Length > 0)
            {
                CalculateTotalBill();
            }
        }

        private void cmbSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedIndex >= 0)
            {
                object ob = ObjDAL.ExecuteScalarInt("SELECT CountryID FROM " + clsUtility.DBName + ".dbo.SupplierMaster WHERE SupplierID = " + cmbSupplier.SelectedValue);
                cmbCountry.SelectedValue = ob;
                cmbCountry_SelectionChangeCommitted(sender, e);

                grpCurrencyRate.Enabled = true;
                grpForeignCurrency.Enabled = true;
                grpLocalCurrency.Enabled = true;
            }
        }

        private void btnSupplierPopup_Click(object sender, EventArgs e)
        {
            //int a = 0;
            //if (cmbSupplier.SelectedIndex >= 0)
            //{
            //    a = Convert.ToInt32(cmbSupplier.SelectedValue);
            //}
            Masters.Supplier_Details Obj = new Masters.Supplier_Details();
            Obj.ShowDialog();
            FillSupplierData();
            //if (a > 0)
            //{
            //    cmbSupplier.SelectedValue = a;
            //}
            //int CountryID = 0;
            //if (cmbCountry.SelectedIndex >= 0)
            //{
            //    CountryID = Convert.ToInt32(cmbCountry.SelectedValue);
            //}
            FillCountryData();
            //if (CountryID > 0)
            //{
            //    cmbCountry.SelectedValue = CountryID;
            //}
        }

        private void txtCurrencyRate_TextChanged(object sender, EventArgs e)
        {
            if (txtCurrencyRate.TextLength > 0)
            {
                CalculateTotalBill();
            }
        }

        private void txtTotalQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...", clsUtility.strProjectTitle);
            }
        }

        private void txtBillValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtBillValue, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...", clsUtility.strProjectTitle);
            }
        }

        private void txtForeignExp_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            e.Handled = ObjUtil.IsDecimal(txt, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Numbers...", clsUtility.strProjectTitle);
            }
        }

        private void rdSearchByBillNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByBillNo.Checked)
            {
                txtSearchByBillNo.Enabled = true;
                txtSearchByBillNo.Focus();
            }
            else
            {
                txtSearchByBillNo.Enabled = false;
                txtSearchByBillNo.Clear();
                rdShowAll.Checked = true;
            }
        }

        private void txtSearchByBillNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSearchByBillNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void txtSearchByBillNo_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchByBillNo.Text.Trim().Length == 0)
            {
                LoadData();
                return;
            }
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.PurchaseInvoice", "PurchaseInvoiceID,SupplierBillNo,SupplierID,ShipmentNo,BillDate,BillValue,TotalQTY,Discount,ForeignExp,GrandTotal,LocalValue,LocalExp,LocalBillValue,IsInvoiceDone", "SupplierBillNo LIKE '%" + txtSearchByBillNo.Text + "%'", "BillDate DESC");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void lnkRefreshData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadData();
            FillSupplierData();
            FillCountryData();
            dtpBillDate.MaxDate = DateTime.Now;
            btnCurrencyRatePopup.Enabled = false;
        }

        private void btnCurrencyRatePopup_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.Currency_Value_Settings) || clsUtility.IsAdmin)
            {
                Settings.Currency_Value_Settings Obj = new Settings.Currency_Value_Settings();
                Obj.ShowDialog();
                cmbSupplier_SelectionChangeCommitted(sender, e);
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
    }
}