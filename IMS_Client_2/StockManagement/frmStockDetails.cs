﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;
using IMS_Client_2.Barcode;
using IMS_Client_2.Sales;

namespace IMS_Client_2.StockManagement
{
    public partial class frmStockDetails : Form
    {
        public frmStockDetails()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (dt.Rows.Count > 1)
                    {
                        dgvStockDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvStockDetails.DataSource = null;
                    }
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }
        private void FillColorData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.ColorMaster", "ColorID,ColorName", "ISNULL(ActiveStatus,1)=1", "ColorName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbColor.DataSource = dt;
                cmbColor.DisplayMember = "ColorName";
                cmbColor.ValueMember = "ColorID";
            }
            cmbColor.SelectedIndex = -1;
        }

        private void FillCategoryData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName", "ISNULL(ActiveStatus,1)=1", "CategoryName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
            }
            cmbCategory.SelectedIndex = -1;
        }

        private void frmStockDetails_Load(object sender, EventArgs e)
        {
            FillColorData();
            FillCategoryData();
        }

        private void dgvStockDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvStockDetails);
            ObjUtil.SetDataGridProperty(dgvStockDetails, DataGridViewAutoSizeColumnsMode.Fill);
            if (dgvStockDetails.Columns.Contains("ProductID"))
            {
                dgvStockDetails.Columns["ProductID"].Visible = false;
            }
            if (dgvStockDetails.Columns.Contains("SubProductID"))
            {
                dgvStockDetails.Columns["SubProductID"].Visible = false;
            }
            if (dgvStockDetails.Columns.Contains("Photo"))
            {
                dgvStockDetails.Columns["Photo"].Visible = false;
            }
        }

        private void rdSearchByBarCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByBarCode.Checked)
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

        private void rdSearchByColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByColor.Checked)
            {
                cmbColor.Enabled = true;
                cmbColor.Focus();
            }
            else
            {
                cmbColor.Enabled = false;
                cmbColor.SelectedIndex = -1;
            }
        }

        private void rdSearchByItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByItem.Checked)
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

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                dgvStockDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
                dgvStockDetails.RowHeadersVisible = false; // set it to false if not needed

                LoadData();
            }
            else
            {
                cmbColor.SelectedIndex = -1;
                txtSearchByProductName.Clear();
                txtSearchByBarcode.Clear();
                txtSearchByStyleNo.Clear();
            }
        }

        private void txtSearchByProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
                        ObjUtil.GetDataPopup().CellClick += frmStockDetails_SearchByProductName_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += frmStockDetails_SearchByProductName_KeyDown;
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
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void frmStockDetails_SearchByProductName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void frmStockDetails_SearchByProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void SearchByProductID()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (dt.Rows.Count > 1)
                    {
                        dgvStockDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvStockDetails.DataSource = null;
                    }
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void SearchByColorID()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, cmbColor.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (dt.Rows.Count > 1)
                    {
                        dgvStockDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvStockDetails.DataSource = null;
                    }
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void SearchByBarCodeNo()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, txtSearchByBarcode.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (dt.Rows.Count > 1)
                    {
                        dgvStockDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvStockDetails.DataSource = null;
                    }
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void txtSearchByBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjUtil.IsControlTextEmpty(txtSearchByBarcode))
            {
                return;
            }
            SearchByBarCodeNo();
        }

        private void cmbColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByColorID();
        }

        private void dgvStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvStockDetails.SelectedRows.Count > 0)
            //{
            //    GetProductImage(dgvStockDetails.SelectedRows[0].Cells["ProductID"].Value.ToString());
            //}
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex != dgvStockDetails.Rows.Count - 1)
            {
                int pSubProductID = Convert.ToInt32(dgvStockDetails.SelectedRows[0].Cells["SubProductID"].Value);
                PicItem.Image = GetProductPhoto(pSubProductID);
            }
            else
            {
                PicItem.Image = null;
            }
        }

        private void GetProductImage(string ProductID)
        {
            //string ImageID = "";
            //DataTable dtPhotoNumber = ObjDAL.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".[dbo].[ProductMaster] WITH(NOLOCK) WHERE ProductID=" + ProductID + "");
            //if (ObjUtil.ValidateTable(dtPhotoNumber))
            //{
            //    ImageID = dtPhotoNumber.Rows[0]["Photo"].ToString();
            //}
            //else
            //{
            //    PicItem.Image = null;
            //}
            //DataTable dtImagePath = ObjDAL.ExecuteSelectStatement(" SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
            //if (ObjUtil.ValidateTable(dtImagePath))
            //{
            //    if (dtImagePath.Rows[0]["ImagePath"] != DBNull.Value)
            //    {
            //        string ImgPath = dtImagePath.Rows[0]["ImagePath"].ToString();
            //        string extension = dtImagePath.Rows[0]["Extension"].ToString();

            //        string imgFile = ImgPath + "//" + ImageID + extension;
            //        if (System.IO.File.Exists(imgFile))
            //        {
            //            PicItem.Image = Image.FromFile(imgFile);
            //        }
            //        else
            //        {
            //            PicItem.Image = null;
            //        }
            //    }
            //    else
            //    {
            //        clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
            //    }
            //}
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
                            ObjUtil.GetDataPopup().CellClick += frmStockDetails_SearchByStyleNo_CellClick;
                            ObjUtil.GetDataPopup().KeyDown += frmStockDetails_SearchByStyleNo_KeyDown;
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
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
        private void frmStockDetails_SearchByStyleNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByStyleNo.SelectionStart = txtSearchByStyleNo.MaxLength;
                txtSearchByStyleNo.Focus();
                SearchByStyleNo();
            }
        }

        private void frmStockDetails_SearchByStyleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByStyleNo.SelectionStart = txtSearchByStyleNo.MaxLength;
                txtSearchByStyleNo.Focus();
                SearchByStyleNo();
            }
        }

        private void SearchByStyleNo()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByStyleNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvStockDetails.DataSource = dt;
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void SearchByCategoryID()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, cmbCategory.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("IsAdmin", SqlDbType.Int, clsUtility.IsAdmin, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Material_NewDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    if (dt.Rows.Count > 1)
                    {
                        dgvStockDetails.DataSource = dt;
                    }
                    else
                    {
                        dgvStockDetails.DataSource = null;
                    }
                }
                else
                {
                    dgvStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void rdSearchByStyleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByStyleNo.Checked)
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

        private void rdSearchByCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByCategory.Checked)
            {
                cmbCategory.Enabled = true;
                cmbCategory.Focus();
            }
            else
            {
                cmbCategory.Enabled = false;
                cmbCategory.SelectedIndex = -1;
            }
        }

        private void cmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByCategoryID();
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

        private void txtSearchByProductName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSearchByProductName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvStockDetails.SelectedRows.Count > 0)
            {
                if (clsFormRights.HasFormRight(clsFormRights.Forms.frmQuickBarCodePrint) || clsUtility.IsAdmin)
                {
                    string BarcodeNo = dgvStockDetails.SelectedRows[0].Cells["BarcodeNo"].Value.ToString();
                    frmQuickBarCodePrint frmQuickBarCodePrint = new frmQuickBarCodePrint();
                    frmQuickBarCodePrint.txtBarcodenumber.Text = BarcodeNo;

                    frmQuickBarCodePrint.ShowDialog();
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task");
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Please select your record.");
            }
        }

        private void frmStockDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
    }
}