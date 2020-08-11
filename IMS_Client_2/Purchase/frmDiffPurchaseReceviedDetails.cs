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
    public partial class frmDiffPurchaseReceviedDetails : Form
    {
        public frmDiffPurchaseReceviedDetails()
        {
            InitializeComponent();
        }
        internal int PurchaseInvoiceID = 0;

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            txtSupplierBillNo.Clear();
            PurchaseInvoiceID = 0;
            txtCurrencyName.Clear();
            txtNewRate.Clear();
            txtTotalBillQTY.Clear();
            txtTotalDiffQTY.Clear();
            txtTotalDiffValue.Clear();
            txtTotalQTYReceived.Clear();

            dataGridView1.DataSource = null;
            txtSupplierBillNo.Focus();
        }

        private void LoadData()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_DiffPurchase_Received_Details " + PurchaseInvoiceID);

            int TotalBillQTY = 0, TotalReceivedQTY = 0, TotalDiffQTY = 0;
            double TotalDiffValue = 0;
            if (ObjUtil.ValidateTable(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TotalReceivedQTY += dt.Rows[i]["Receive QTY"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Receive QTY"]) : 0;
                    TotalDiffQTY += dt.Rows[i]["Diff QTY"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Diff QTY"]) : 0;
                    TotalDiffValue += dt.Rows[i]["Diff QTY"] != DBNull.Value ? Convert.ToDouble(dt.Rows[i]["Diff Value"]) : 0;

                    if (i == 0)
                    {
                        TotalBillQTY = dt.Rows[i]["TotalQTY"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["TotalQTY"]) : 0;
                        continue;
                    }
                }
                cmbSupplier.SelectedValue = Convert.ToInt32(dt.Rows[0]["SupplierID"]);
                txtTotalBillQTY.Text = TotalBillQTY.ToString();
                txtTotalQTYReceived.Text = TotalReceivedQTY.ToString();

                //txtTotalDiffQTY.Text = TotalDiffQTY.ToString();
                txtTotalDiffQTY.Text = (TotalReceivedQTY - TotalBillQTY).ToString();

                txtTotalDiffValue.Text = TotalDiffValue.ToString();

                txtSupplierBillNo.Text = dt.Rows[0]["SupplierBillNo"].ToString();
                txtNewRate.Text = dt.Rows[0]["New Rate"].ToString();
                dtpBillDate.Value = Convert.ToDateTime(dt.Rows[0]["BillDate"]);

                object ob = ObjDAL.ExecuteScalar("SELECT CurrencyName FROM " + clsUtility.DBName + ".[dbo].[CurrencyRateSetting] WITH(NOLOCK) WHERE CountryID=" + dt.Rows[0]["CountryID"]);
                txtCurrencyName.Text = ob.ToString();

                dataGridView1.DataSource = dt;
            }
            else
            {
                txtTotalBillQTY.Text = TotalBillQTY.ToString();
                txtTotalQTYReceived.Text = TotalReceivedQTY.ToString();
                txtTotalDiffQTY.Text = TotalDiffQTY.ToString();
                txtTotalDiffValue.Text = TotalDiffValue.ToString();
                dataGridView1.DataSource = null;
            }
        }

        private void frmDiffPurchaseReceviedDetails_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dataGridView1.RowHeadersVisible = false; // set it to false if not needed

            FillSupplierData();
            LoadData();
        }

        private void FillSupplierData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.SupplierMaster", "SupplierID,SupplierName", "ISNULL(ActiveStatus,1)=1", "SupplierName ASC");
            cmbSupplier.DataSource = dt;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";

            cmbSupplier.SelectedIndex = -1;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            dataGridView1.Columns["SubProductID"].Visible = false;
            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["Photo"].Visible = false;
            dataGridView1.Columns["SupplierID"].Visible = false;
            dataGridView1.Columns["CountryID"].Visible = false;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    return;
                }
                int pSubProductID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SubProductID"].Value);
                PicProductImg.Image = GetProductPhoto(pSubProductID);
            }
            catch (Exception ex)
            {
                clsUtility.ShowInfoMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
    }
}