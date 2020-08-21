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
    public partial class frmBranchSaleShorting : Form
    {
        public frmBranchSaleShorting()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        DateTime dtDate = DateTime.Now;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void LoadBranch()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbBranch.DataSource = dt;
                cmbBranch.DisplayMember = "StoreName";
                cmbBranch.ValueMember = "StoreID";
                cmbBranch.SelectedIndex = -1;
            }
        }

        private void LoadStores()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1 AND ISNULL(StoreCategory,0)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbToStore.DataSource = dt;
                cmbToStore.DisplayMember = "StoreName";
                cmbToStore.ValueMember = "StoreID";
                cmbToStore.SelectedIndex = -1;
            }
        }
        private void frmBranchSaleShorting_Load(object sender, EventArgs e)
        {
            btnSearch.BackgroundImage = B_Leave;
            btnReset.BackgroundImage = B_Leave;

            btnPrint.BackgroundImage = B_Leave;

            dtpFromDate.MaxDate = DateTime.Now;
            dtpToDate.MaxDate = DateTime.Now;

            LoadBranch();
            LoadStores();

            cmbBranch.SelectedValue = frmHome.Home_StoreID;
        }

        private void GetSelectedItemStockDetails()
        {
            ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, dgvBranchStockDetails.SelectedRows[0].Cells["ProductID"].Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("BarcodeNo", SqlDbType.BigInt, dgvBranchStockDetails.SelectedRows[0].Cells["BarcodeNo"].Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, dgvBranchStockDetails.SelectedRows[0].Cells["ModelNo"].Value, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
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

        private void dgvBranchStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            { return; }

            GetSelectedItemStockDetails();
            int pSubProductID = Convert.ToInt32(dgvBranchStockDetails.SelectedRows[0].Cells["SubProductID"].Value);
            picProduct.Image = GetProductPhoto(pSubProductID);
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
                        //clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                    }
                }
            }
            return imgProduct;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = dtDate;
            dtpToDate.Value = dtDate;
            cmbToStore.SelectedIndex = -1;

            dgvBranchStockDetails.DataSource = null;
            dgvStockDetails.DataSource = null;
        }

        private void SearchBranchSalesShorting()
        {
            dgvBranchStockDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvBranchStockDetails.RowHeadersVisible = false; // set it to false if not needed

            ObjDAL.SetStoreProcedureData("FromStoreID", SqlDbType.Int, cmbToStore.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToStoreID", SqlDbType.BigInt, cmbBranch.SelectedValue, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyy-MM-dd"), clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_BranchShorting_Details");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvBranchStockDetails.DataSource = dt;
                }
                else
                {
                    dgvBranchStockDetails.DataSource = null;
                }
            }
            ObjDAL.ResetData();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ObjUtil.IsControlTextEmpty(cmbBranch))
            {
                clsUtility.ShowInfoMessage("Select Your Branch.", clsUtility.strProjectTitle);
                return;
            }
            if (ObjUtil.IsControlTextEmpty(cmbToStore))
            {
                clsUtility.ShowInfoMessage("Select Store.", clsUtility.strProjectTitle);
                return;
            }
            SearchBranchSalesShorting();
        }

        private void dgvBranchStockDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvBranchStockDetails);
            ObjUtil.SetDataGridProperty(dgvBranchStockDetails, DataGridViewAutoSizeColumnsMode.DisplayedCells);

            dgvBranchStockDetails.Columns["ProductID"].Visible = false;
            dgvBranchStockDetails.Columns["SubProductID"].Visible = false;
            dgvBranchStockDetails.Columns["StoreID"].Visible = false;
            dgvBranchStockDetails.Columns["StoreName"].Visible = false;
            dgvBranchStockDetails.Columns["ColorID"].Visible = false;
            dgvBranchStockDetails.Columns["SizeID"].Visible = false;
        }

        private void dgvStockDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvStockDetails);
            ObjUtil.SetDataGridProperty(dgvStockDetails, DataGridViewAutoSizeColumnsMode.DisplayedCells);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ObjUtil.ValidateTable((DataTable)dgvBranchStockDetails.DataSource))
            {
                Report.Report_Forms.frmBranchShortingReport frmBranchShortingReport = new Report.Report_Forms.frmBranchShortingReport();
                frmBranchShortingReport.Branch = cmbBranch.Text;
                frmBranchShortingReport.ToStore = cmbToStore.Text;
                frmBranchShortingReport.FromDate = "From :  " + dtpFromDate.Value.ToShortDateString() + " To :  " + dtpToDate.Value.ToShortDateString();

                frmBranchShortingReport.ddtBranchShorting = (DataTable)dgvBranchStockDetails.DataSource;
                frmBranchShortingReport.Show();
            }
            else
            {
                clsUtility.ShowInfoMessage("No data found");
            }
        }
    }
}