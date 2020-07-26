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

        private void frmStockDetails_Load(object sender, EventArgs e)
        {
            //LoadData();
            FillColorData();
        }

        private void dgvStockDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvStockDetails);
            //ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            ObjUtil.SetDataGridProperty(dgvStockDetails, DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvStockDetails.Columns["ProductID"].Visible = false;
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
                LoadData();
            }
            else
            {
                cmbColor.SelectedIndex = -1;
                txtSearchByProductName.Clear();
                txtSearchByBarcode.Clear();
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
                        ObjUtil.SetControlData(txtSearchByProductName, "ProductName");
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
            //DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details " + txtProductID.Text + ",NULL");

            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
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
            //DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details " + txtProductID.Text + ",NULL");

            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, cmbColor.SelectedValue, clsConnection_DAL.ParamType.Input);
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
            //DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details " + txtProductID.Text + ",NULL");

            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, txtSearchByBarcode.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
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
            if (dgvStockDetails.SelectedRows.Count > 0)
            {
                GetProductImage(dgvStockDetails.SelectedRows[0].Cells["ProductID"].Value.ToString());
            }
        }

        private void GetProductImage(string ProductID)
        {
            string ImageID = "";
            DataTable dtPhotoNumber = ObjDAL.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".[dbo].[ProductMaster] WITH(NOLOCK) WHERE ProductID=" + ProductID + "");
            if (ObjUtil.ValidateTable(dtPhotoNumber))
            {
                ImageID = dtPhotoNumber.Rows[0]["Photo"].ToString();
            }
            else
            {
                PicItem.Image = null;
            }
            DataTable dtImagePath = ObjDAL.ExecuteSelectStatement(" SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
            if (ObjUtil.ValidateTable(dtImagePath))
            {
                if (dtImagePath.Rows[0]["ImagePath"] != DBNull.Value)
                {
                    string ImgPath = dtImagePath.Rows[0]["ImagePath"].ToString();
                    string extension = dtImagePath.Rows[0]["Extension"].ToString();

                    string imgFile = ImgPath + "//" + ImageID + extension;
                    if (System.IO.File.Exists(imgFile))
                    {
                        PicItem.Image = Image.FromFile(imgFile);
                    }
                    else
                    {
                        PicItem.Image = null;
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                }
            }
        }
    }
}
