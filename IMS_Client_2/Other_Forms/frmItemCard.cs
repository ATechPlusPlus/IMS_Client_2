using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmItemCard : Form
    {
        public frmItemCard()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        private void txtSearchByModelNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchByModelNo.Text.Length > 0)
                {
                    ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByModelNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
                    DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StyleNo_Popup");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (ObjUtil.ValidateTable(dt))
                        {
                            ObjUtil.SetControlData(txtSearchByModelNo, "StyleNo");
                            ObjUtil.ShowDataPopup(dt, txtSearchByModelNo, this, grpFilter);

                            if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                            {
                                ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                                if (ObjUtil.GetDataPopup().ColumnCount > 0)
                                {
                                    ObjUtil.SetDataPopupSize(180, 0);
                                }
                            }
                            ObjUtil.GetDataPopup().CellClick += frmItemCard_SearchByStyleNo_CellClick;
                            ObjUtil.GetDataPopup().KeyDown += frmItemCard_SearchByStyleNo_KeyDown;
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

        private void frmItemCard_SearchByStyleNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByModelNo.SelectionStart = txtSearchByModelNo.MaxLength;
                txtSearchByModelNo.Focus();
                SearchByStyleNo();
            }
        }

        private void frmItemCard_SearchByStyleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByModelNo.SelectionStart = txtSearchByModelNo.MaxLength;
                txtSearchByModelNo.Focus();
                SearchByStyleNo();
            }
        }

        private void SearchByStyleNo()
        {
            //ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByModelNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemCard_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPriceDetails.DataSource = dt;
                    Calculation(dt);
                    FillItemDetailsByModelNo();
                }
                else
                {
                    dgvPriceDetails.DataSource = null;
                    dataGridView1.DataSource = null;
                    Reset();
                }
            }
            ObjDAL.ResetData();
        }

        private void FillItemDetailsByModelNo()
        {
            //ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, txtSearchByModelNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemCard_Material_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void txtSearchByItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchByItemName.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_ProductDetails_Popup '" + txtSearchByItemName.Text + "'");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSearchByItemName, "ItemName");
                        ObjUtil.SetControlData(txtProductID, "ProductID");
                        ObjUtil.ShowDataPopup(dt, txtSearchByItemName, this, grpFilter);

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
                        ObjUtil.GetDataPopup().CellClick += frmItemCard_SearchByProductName_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += frmItemCard_SearchByProductName_KeyDown;
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

        private void frmItemCard_SearchByProductName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByItemName.SelectionStart = txtSearchByItemName.MaxLength;
                txtSearchByItemName.Focus();
                SearchByProductID();
            }
        }

        private void frmItemCard_SearchByProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByItemName.SelectionStart = txtSearchByItemName.MaxLength;
                txtSearchByItemName.Focus();
                SearchByProductID();
            }
        }

        private void SearchByProductID()
        {
            //ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemCard_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPriceDetails.DataSource = dt;
                    Calculation(dt);
                }
                else
                {
                    dgvPriceDetails.DataSource = null;
                    dataGridView1.DataSource = null;
                    Reset();
                }
            }
            ObjDAL.ResetData();
        }

        private void SearchByBarCodeNo()
        {
            //ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, txtProductID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, txtSearchByBarCode.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemCard_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvPriceDetails.DataSource = dt;
                    Calculation(dt);
                    FillItemDetailsByBarCodeNo();
                }
                else
                {
                    dgvPriceDetails.DataSource = null;
                    dataGridView1.DataSource = null;
                    Reset();
                }
            }
            ObjDAL.ResetData();
        }

        private void txtSearchByBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtSearchByBarCode.Text.Trim().Length > 0)
            {
                SearchByBarCodeNo();
            }
        }

        private void FillItemDetailsByBarCodeNo()
        {
            //ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, txtSearchByBarCode.Text.Trim(), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ItemCard_Material_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }

        private void dgvPriceDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvPriceDetails, DataGridViewAutoSizeColumnsMode.Fill);

            dgvPriceDetails.Columns["ProductID"].Visible = false;
            dgvPriceDetails.Columns["SubProductID"].Visible = false;
            dgvPriceDetails.Columns["QTY"].Visible = false;
        }

        private void Calculation(DataTable dt)
        {
            int CurrentQty = Convert.ToInt32(dt.Rows[0]["QTY"]);
            int SoldQty = Convert.ToInt32(dt.Rows[0]["SoldQTY"]);
            int ReceivedQty = CurrentQty + SoldQty;

            decimal Soldprice = Convert.ToDecimal(dt.Rows[0]["EndUser"]);
            decimal Localprice = Convert.ToDecimal(dt.Rows[0]["LocalCost"]);

            txtCurrentQty.Text = CurrentQty.ToString();
            txtSoldQty.Text = SoldQty.ToString();
            txtReceivedQty.Text = ReceivedQty.ToString();
            //int temp = SoldQty == 0 ? 1 : SoldQty;                            // Handle divide by zero exception
            txtSoldPer.Text = Math.Round( (Convert.ToDouble(SoldQty) / Convert.ToDouble(ReceivedQty)) * 100 ,3).ToString()+"%";

            txtReceivedCost.Text = (Localprice * ReceivedQty).ToString();
            txtSoldCost.Text = (Localprice * SoldQty).ToString();
            txtCurrentStockCost.Text = (CurrentQty * Localprice).ToString();

            txtRevenue.Text = (SoldQty * Soldprice).ToString();
            txtRevenue.BackColor = Color.Green;

            txtAchivedMarginCost.Text = (Convert.ToDecimal(txtRevenue.Text) - Convert.ToDecimal(txtReceivedCost.Text)).ToString();
            if (Convert.ToDecimal(txtAchivedMarginCost.Text) < 0)
            {
                txtAchivedMarginCost.BackColor = Color.Red;
            }
            else
            {
                txtAchivedMarginCost.BackColor = Color.White;
            }

            txtProfitMargin.Text = (Convert.ToDecimal(txtRevenue.Text) - Convert.ToDecimal(txtSoldCost.Text)).ToString();
        }

        private void Reset()
        {
            txtCurrentQty.Text = "0";
            txtSoldQty.Text = "0";
            txtReceivedQty.Text = "0";
            txtSoldPer.Text = "0";
            txtReceivedCost.Text = "0";
            txtSoldCost.Text = "0";
            txtCurrentStockCost.Text = "0";
            txtRevenue.Text = "0";
            txtAchivedMarginCost.Text = "0";
            txtProfitMargin.Text = "0";
        }

        private void dgvPriceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int pSubProductID = Convert.ToInt32(dgvPriceDetails.SelectedRows[0].Cells["SubProductID"].Value);
                picProduct.Image = GetProductPhoto(pSubProductID);
            }
            else
            {
                picProduct.Image = null;
            }
        }

        private Image GetProductPhoto(int SubProductID)
        {
            Image imgProduct = null;
            ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, SubProductID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ProductPhoto");
            if (ObjUtil.ValidateDataSet(ds))
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
                            imgProduct = Properties.Resources.NoImage;
                        }
                    }
                    else
                    {
                        imgProduct = Properties.Resources.NoImage;
                        //clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                    }
                }
            }
            return imgProduct;
        }

        private void rdSearchByModelNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByModelNo.Checked)
            {
                txtSearchByModelNo.Enabled = true;
                txtSearchByModelNo.Focus();
            }
            else
            {
                txtSearchByModelNo.Enabled = false;
                txtSearchByModelNo.Clear();
            }
        }

        private void rdSearchByBarCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByBarCode.Checked)
            {
                txtSearchByBarCode.Enabled = true;
                txtSearchByBarCode.Focus();
            }
            else
            {
                txtSearchByBarCode.Enabled = false;
                txtSearchByBarCode.Clear();
            }
        }

        private void rdSearchByItemName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByItemName.Checked)
            {
                txtSearchByItemName.Enabled = true;
                txtSearchByItemName.Focus();
            }
            else
            {
                txtSearchByItemName.Enabled = false;
                txtSearchByItemName.Clear();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);

            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["SubProductID"].Visible = false;
            dataGridView1.Columns["Item Name"].Visible = false;
            dataGridView1.Columns["Style No"].Visible = false;
        }

        private void txtModelNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtModelNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void frmItemCard_Load(object sender, EventArgs e)
        {
            Reset();
        }
    }
}