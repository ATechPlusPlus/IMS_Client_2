using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.StockManagement
{
    public partial class frmItemWiseModelNo : Form
    {
        public frmItemWiseModelNo()
        {
            InitializeComponent();
        }
        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        private void frmItemWiseModelNo_Load(object sender, EventArgs e)
        {
            FillStoreData();

            cmbShop.SelectedValue = frmHome.Home_StoreID;
            rdSearchByShop.Checked = true;
            cmbShop.Enabled = true;

            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            SearchByStoreID();

            groupBox1.Focus();
        }

        private void LoadItemModelDetails()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, '0', clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemWiseModelNo");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtItemModel = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtItemModel))
                {
                    dgvProductDetails.DataSource = dtItemModel;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbShop.DataSource = dt;
                cmbShop.DisplayMember = "StoreName";
                cmbShop.ValueMember = "StoreID";
                cmbShop.SelectedIndex = -1;
            }
        }

        private void SearchByProductID()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, '0', clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemWiseModelNo");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtItemModel = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtItemModel))
                {
                    dgvProductDetails.DataSource = dtItemModel;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
        }

        private void SearchByStoreID()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, '0', clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, cmbShop.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemWiseModelNo");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtItemModel = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtItemModel))
                {
                    dgvProductDetails.DataSource = dtItemModel;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void SearchByModelNo()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByStyleNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemWiseModelNo");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtItemModel = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtItemModel))
                {
                    dgvProductDetails.DataSource = dtItemModel;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void SearchByBarcodeNo()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, '0', clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.BigInt, txtSearchByBarcode.Text, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemWiseModelNo");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtItemModel = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtItemModel))
                {
                    dgvProductDetails.DataSource = dtItemModel;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvProductDetails.Columns["SubProductID"].Visible = false;
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["BrandID"].Visible = false;

            lblTotalRecord.Text = "Total Records : " + dgvProductDetails.Rows.Count;
        }

        private void rdSearchByProductName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByProductName.Checked)
            {
                txtSearchByProductName.Enabled = true;
                txtSearchByProductName.Focus();
            }
            else
            {
                txtSearchByProductName.Enabled = false;
                txtSearchByProductName.Clear();
            }
        }

        private void txtSearchByProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchByProductName.Text.Length == 0)
                {
                    dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                    //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
                    dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

                    LoadItemModelDetails();
                    return;
                }
                if (txtSearchByProductName.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_ProductDetails_Popup '" + txtSearchByProductName.Text + "'");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSearchByProductName, "ItemName");
                        ObjUtil.SetControlData(txtProductID, "ProductID");
                        ObjUtil.ShowDataPopup(dt, txtSearchByProductName, this, groupBox1);

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
                        ObjUtil.GetDataPopup().CellClick += frmItemWiseModelNo_SearchByProductName_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += frmItemWiseModelNo_SearchByProductName_KeyDown;
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
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
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void frmItemWiseModelNo_SearchByProductName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void frmItemWiseModelNo_SearchByProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void rdSearchByModelNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByModelNo.Checked)
            {
                txtSearchByStyleNo.Enabled = true;
                txtSearchByStyleNo.Focus();
            }
            else
            {
                txtSearchByStyleNo.Enabled = false;
                txtSearchByStyleNo.Clear();
            }
        }

        private void rdSearchByShop_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByShop.Checked)
            {
                cmbShop.Enabled = true;
                cmbShop.Focus();
            }
            else
            {
                cmbShop.Enabled = false;
                cmbShop.SelectedIndex = -1;
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
                dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

                LoadItemModelDetails();
            }
            else
            {
                cmbShop.SelectedIndex = -1;
                txtSearchByProductName.Clear();
                txtSearchByStyleNo.Clear();
                txtSearchByBarcode.Clear();
            }
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbShop.SelectedValue != null)
            {
                SearchByStoreID();
            }
        }

        private void txtSearchByStyleNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchByStyleNo.Text.Length > 0)
                {
                    ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByStyleNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
                    DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StyleNo_Popup");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (ObjUtil.ValidateTable(dt))
                        {
                            ObjUtil.SetControlData(txtSearchByStyleNo, "StyleNo");
                            ObjUtil.ShowDataPopup(dt, txtSearchByStyleNo, this, groupBox1);

                            if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                            {
                                ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                                if (ObjUtil.GetDataPopup().ColumnCount > 0)
                                {
                                    ObjUtil.SetDataPopupSize(180, 0);
                                }
                            }
                            ObjUtil.GetDataPopup().CellClick += frmItemWiseModelNo_SearchByStyleNo_CellClick;
                            ObjUtil.GetDataPopup().KeyDown += frmItemWiseModelNo_SearchByStyleNo_KeyDown;
                        }
                        else
                        {
                            ObjUtil.CloseAutoExtender();
                        }
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
                    }
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void frmItemWiseModelNo_SearchByStyleNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByStyleNo.SelectionStart = txtSearchByStyleNo.MaxLength;
                txtSearchByStyleNo.Focus();
                SearchByModelNo();
            }
        }

        private void frmItemWiseModelNo_SearchByStyleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByStyleNo.SelectionStart = txtSearchByStyleNo.MaxLength;
                txtSearchByStyleNo.Focus();
                SearchByModelNo();
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
            return imgProduct;
        }
        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            int pSubProductID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["SubProductID"].Value);
            PicItem.Image = GetProductPhoto(pSubProductID);
        }

        private void txtSearchByProductName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSearchByProductName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void dgvProductDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgvProductDetails.Columns[e.Column.Index].ReadOnly = true;
            if (dgvProductDetails.Columns.Contains("EndUser"))
            {
                dgvProductDetails.Columns["EndUser"].ReadOnly = false;
            }
        }

        private void dgvProductDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            e.Cancel = false;
            int column = dgvProductDetails.CurrentCell.ColumnIndex;
            string headerText = dgvProductDetails.Columns[column].HeaderText;

            if (headerText == "EndUser")
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

        private void dgvProductDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvProductDetails.Columns[e.ColumnIndex].Name == "EndUser")
                {
                    try
                    {
                        int SubProductID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["SubProductID"].Value);
                        int ProductID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["ProductID"].Value);
                        decimal Rate = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells["EndUser"].Value);

                        ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, SubProductID, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, ProductID, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("EndUser", SqlDbType.Decimal, Rate, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);

                        DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Update_EndUser_Price");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (ObjUtil.ValidateTable(dt))
                            {
                                clsUtility.ShowInfoMessage(dt.Rows[0]["Msg"].ToString());
                            }
                        }
                        //int a = ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".[dbo].[tblProductWiseModelNo] SET EndUser=" + Rate + " WHERE SubProductID=" + SubProductID);
                        //if (a > 0)
                        //{
                        //    clsUtility.ShowInfoMessage("EndUser Price is Updated..");
                        //}
                        //else
                        //{
                        //    clsUtility.ShowInfoMessage("Unable to Update EndUser Price..");
                        //}
                    }
                    catch (Exception ex)
                    {
                        clsUtility.ShowErrorMessage(ex.ToString());
                    }
                }
            }
        }

        private void dgvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int column = dgvProductDetails.CurrentCell.ColumnIndex;
            string headerText = dgvProductDetails.Columns[column].HeaderText;

            if (headerText == "EndUser")
            {
                e.Control.KeyPress += Decimal_Control_KeyPress;
            }
        }

        private void Decimal_Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string k = e.KeyChar.ToString();
            TextBox txt = (TextBox)sender;
            e.Handled = ObjUtil.IsDecimal(txt, e);
        }

        private void rdSearchByBarcodeNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByBarcodeNo.Checked)
            {
                txtSearchByBarcode.Enabled = true;
                txtSearchByBarcode.Focus();
            }
            else
            {
                txtSearchByBarcode.Enabled = false;
                txtSearchByBarcode.Clear();
            }
        }

        private void txtSearchByBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (ObjUtil.IsControlTextEmpty(txtSearchByBarcode))
                {
                    clsUtility.ShowInfoMessage("Please Enter Barcode number.");
                    txtSearchByBarcode.Focus();
                    return;
                }
                SearchByBarcodeNo();
            }
        }
    }
}