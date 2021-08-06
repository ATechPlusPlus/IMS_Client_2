using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Inventory
{
    public partial class frmScanInventoryCompare : Form
    {
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        public int pMasterScanID = 0;
        int StoreID = 0;
        public int comparestatus = 0;
        public frmScanInventoryCompare()
        {
            InitializeComponent();
        }

        public DateTime? ComparedDate { get; set; }
        public string ScanBy { get; set; }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("MasterScanID", SqlDbType.Int, pMasterScanID, clsConnection_DAL.ParamType.Input);
            if (!ObjUtil.IsControlTextEmpty(txtBarCode))
            {
                ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.BigInt, txtBarCode.Text.Trim(), clsConnection_DAL.ParamType.Input);
            }

            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ScanInventoryCompare");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                    txtStoreName.Text = dt.Rows[0]["StoreName"].ToString();
                    if (dt.Rows[0]["Compared Date"] != DBNull.Value)
                    {
                        ComparedDate = Convert.ToDateTime(dt.Rows[0]["Compared Date"]);
                    }
                    else
                    {
                        ComparedDate = null;
                    }
                    StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);

                    int _SUBPID = Convert.ToInt32(dt.Rows[0]["SubProductID"]);
                    picProduct.Image = GetProductPhoto(Convert.ToInt32(_SUBPID));
                }
                else
                {
                    if (!ObjUtil.IsControlTextEmpty(txtBarCode))
                        clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + txtBarCode.Text.Trim());

                    dgvProductDetails.DataSource = null;

                    picProduct.Image = null;
                }
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void frmScanInventoryList_Load(object sender, EventArgs e)
        {
            txtDiffQTY.Text = "0";
            txtSystemQTY.Text = "0";
            txtInventoryQTY.Text = "0";

            txtDiffRate.Text = "0";
            txtSystemRate.Text = "0";
            txtInventoryRate.Text = "0";

            btnSaveData.BackgroundImage = B_Leave;
            btncancel.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;


            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            LoadData();
            DataBindTextBox();
            //HighlightDiffQTY();
            if (comparestatus == 0)
            {
                btnSaveData.Enabled = true;
            }
            else
            {
                btnSaveData.Enabled = false;
            }
        }

        private void DataBindTextBox()
        {
            DataTable dt = (DataTable)dgvProductDetails.DataSource;
            if (ObjUtil.ValidateTable(dt))
            {
                object systemqty = dt.Compute("SUM([System QTY])", string.Empty);
                object inventoryqty = dt.Compute("SUM([Inventory QTY])", string.Empty);
                int diffqty = Convert.ToInt32(inventoryqty) - Convert.ToInt32(systemqty);

                object systemrate = dt.Compute("SUM([System Rate])", string.Empty);
                object inventoryrate = dt.Compute("SUM([Inventory Rate])", string.Empty); ;
                double diffrate = Convert.ToDouble(inventoryrate) - Convert.ToDouble(systemrate);

                txtSystemQTY.Text = systemqty.ToString();
                txtInventoryQTY.Text = inventoryqty.ToString();
                txtDiffQTY.Text = diffqty.ToString();

                txtSystemRate.Text = systemrate.ToString();
                txtInventoryRate.Text = inventoryrate.ToString();
                txtDiffRate.Text = Math.Round(diffrate, 3).ToString();
            }
        }

        private void HighlightDiffQTY()
        {
            try
            {
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    int qty = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["Diff QTY"].Value);
                    if (qty < 0)
                    {
                        dgvProductDetails.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    //else
                    //{
                    //    dgvProductDetails.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    //}
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }
        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvProductDetails.ClearSelection();
            ObjUtil.SetRowNumber(dgvProductDetails);
            

            //ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.DisplayedCells, Color.White);
            //ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, Color.White);
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["SizeID"].Visible = false;
            dgvProductDetails.Columns["ColorID"].Visible = false;
            dgvProductDetails.Columns["SubProductID"].Visible = false;
            dgvProductDetails.Columns["StoreName"].Visible = false;
            dgvProductDetails.Columns["Compared Date"].Visible = false;

            //dgvProductDetails.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
            //dgvProductDetails.RowsDefaultCellStyle.SelectionForeColor = Color.Transparent;

            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, Color.White);
            dgvProductDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;

            if (!ObjUtil.IsControlTextEmpty(txtBarCode))
            {
                foreach (DataGridViewRow row in dgvProductDetails.Rows)
                {
                    if (row.Cells["Barcode"].Value.ToString() == "" + txtBarCode.Text.Trim() + "")
                    {
                        dgvProductDetails.Rows[row.Index].Cells["Barcode"].Selected = true;
                        dgvProductDetails.FirstDisplayedScrollingRowIndex = row.Index;

                        break;
                    }
                }
                txtBarCode.Clear();
            }

            HighlightDiffQTY();
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
                            imgProduct = IMS_Client_2.Properties.Resources.NoImage;
                        }
                    }
                    else
                    {
                        imgProduct = IMS_Client_2.Properties.Resources.NoImage;
                    }
                }
            }
            return imgProduct;
        }

        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            int _SUBPID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["SubProductID"].Value);
            picProduct.Image = GetProductPhoto(Convert.ToInt32(_SUBPID));
        }

        private void btnSaveData_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnSaveData_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmScanInventoryCompare, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                int diffqty = txtDiffQTY.Text.Length == 0 ? 0 : Convert.ToInt32(txtDiffQTY.Text);
                if (diffqty != 0)
                {
                    bool q = clsUtility.ShowQuestionMessage("Are you sure want to update Inventory QTY?");
                    if (q)
                    {
                        ObjDAL.SetStoreProcedureData("MasterScanID", SqlDbType.Int, pMasterScanID, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, StoreID, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                        ObjDAL.SetStoreProcedureData("Flag", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Output);
                        ObjDAL.SetStoreProcedureData("Msg", SqlDbType.VarChar, 0, clsConnection_DAL.ParamType.Output);
                        bool b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Update_StockColorSizeMaster");
                        if (b)
                        {
                            DataTable dt = ObjDAL.GetOutputParmData();
                            if (ObjUtil.ValidateTable(dt))
                            {
                                clsUtility.ShowInfoMessage(dt.Rows[1][1].ToString());
                                if (Convert.ToInt32(dt.Rows[0][1]) == 1)
                                {
                                    LoadData();
                                    HighlightDiffQTY();
                                    btnSaveData.Enabled = false;
                                }
                            }
                        }
                        ObjDAL.ResetData();
                    }
                }
                else
                {
                    clsUtility.ShowInfoMessage("No Need to update System QTY..");
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProductDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //dgvProductDetails.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvProductDetails.Columns[e.Column.Index].ReadOnly = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmInventoryReport frmInventoryReport = new frmInventoryReport();
            frmInventoryReport.pMasterScanID = pMasterScanID;
            frmInventoryReport.ScanBy = ScanBy;
            frmInventoryReport.CompareDDate = ComparedDate;
            frmInventoryReport.StoreName = txtStoreName.Text;

            frmInventoryReport._SysQTY = txtSystemQTY.Text;
            frmInventoryReport._InventoryQTY = txtInventoryQTY.Text;
            frmInventoryReport._DiffQTY = txtDiffQTY.Text;

            frmInventoryReport._SysRate = txtSystemRate.Text;
            frmInventoryReport._InvRate = txtInventoryRate.Text;
            frmInventoryReport._DiffRate = txtDiffRate.Text;

            frmInventoryReport.ShowDialog();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBarCode.Text.Trim().Length != 0)
            {
                if (e.KeyData == Keys.Enter)
                {
                    //LoadData();
                    DataTable dt = (DataTable)dgvProductDetails.DataSource;
                    dgvProductDetails.DataSource = null;
                    dgvProductDetails.DataSource = dt;

                    DataBindTextBox();
                    //HighlightDiffQTY();
                    if (comparestatus == 0)
                    {
                        btnSaveData.Enabled = true;
                    }
                    else
                    {
                        btnSaveData.Enabled = false;
                    }
                }
            }
        }

        private void txtBarCode_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtBarCode_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, System.Drawing.Color.White);
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarCode.Text.Trim().Length == 0)
            {
                LoadData();
                DataBindTextBox();
                //HighlightDiffQTY();
                if (comparestatus == 0)
                {
                    btnSaveData.Enabled = true;
                }
                else
                {
                    btnSaveData.Enabled = false;
                }
            }
        }

        private void frmScanInventoryCompare_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
        }
    }
}