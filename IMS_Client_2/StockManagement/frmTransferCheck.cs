using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.StockManagement
{
    public partial class frmTransferCheck : Form
    {
        public frmTransferCheck()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        public int StoreBillDetailsID = 0;
        public int fromShopID = 0;

        DataTable dtStoreTransfer = new DataTable();
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmTransferCheck_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            btncancel.BackgroundImage = B_Leave;

            txtBarCode.Focus();
            pnlViolet.BackColor = Color.Violet;
            LoadData();
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, StoreBillDetailsID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_ItemDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                dtStoreTransfer = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtStoreTransfer))
                {
                    dgvProductDetails.DataSource = dtStoreTransfer;
                    txtDate.Text = Convert.ToDateTime(dtStoreTransfer.Rows[0]["BillDate"]).ToString("yyyy-MM-dd");
                    txtTotalQTY.Text = dtStoreTransfer.Rows[0]["TotalQTY"].ToString();
                    txtBillNo.Text = dtStoreTransfer.Rows[0]["BillNo"].ToString();

                    object rate = dtStoreTransfer.Compute("SUM(Total)", string.Empty);
                    txtValue.Text = rate.ToString();
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
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataRow[] drow = dtStoreTransfer.Select("Barcode='" + txtBarCode.Text + "'");
                if (drow.Length > 0)
                {
                    int pTotal = 0;
                    int pEnterQty = drow[0]["EnterQTY"].ToString() == "" ? 0 : Convert.ToInt32(drow[0]["EnterQTY"]);
                    int pBillQty = Convert.ToInt32(drow[0]["BillQTY"]);
                    pTotal = pBillQty + pEnterQty;
                    if (pBillQty == pTotal)
                    {
                        drow[0]["CellColor"] = "Green";
                    }
                    else
                    {
                        drow[0]["CellColor"] = "Orange";
                    }
                    drow[0]["EnterQTY"] = pTotal;
                    dtStoreTransfer.AcceptChanges();
                    dgvProductDetails.DataSource = dtStoreTransfer;

                    txtBarCode.Clear();
                    txtBarCode.Focus();
                }
                else
                {
                    ObjDAL.SetStoreProcedureData("BillNo", SqlDbType.NVarChar, txtBillNo.Text.Trim(), clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("BarCode", SqlDbType.Int, txtBarCode.Text, clsConnection_DAL.ParamType.Input);
                    DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ProductDetails_ForVioletColor");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (ObjUtil.ValidateTable(dt))
                        {

                            DataRow row = dtStoreTransfer.NewRow();
                            row["Barcode"] = dt.Rows[0]["Barcode"];
                            row["CellColor"] = dt.Rows[0]["CellColor"];
                            row["ModelNo"] = dt.Rows[0]["ModelNo"];
                            row["ProductID"] = dt.Rows[0]["ProductID"];
                            row["BillQTY"] = 0;
                            row["EnterQTY"] = 1;
                            row["Item"] = dt.Rows[0]["Item"];
                            row["Color"] = dt.Rows[0]["Color"];
                            row["Size"] = dt.Rows[0]["Size"];
                            dtStoreTransfer.Rows.Add(row);
                            dtStoreTransfer.AcceptChanges();
                            dgvProductDetails.DataSource = dtStoreTransfer;

                            txtBarCode.Clear();
                            txtBarCode.Focus();
                        }
                    }
                    return;
                }
            }
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

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);

            dgvProductDetails.Columns["TransferItemID"].Visible = false;
            dgvProductDetails.Columns["StoreBillDetailsID"].Visible = false;
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["BillDate"].Visible = false;
            dgvProductDetails.Columns["BillNo"].Visible = false;
            dgvProductDetails.Columns["TotalQTY"].Visible = false;
            dgvProductDetails.Columns["Total"].Visible = false;
            dgvProductDetails.Columns["CellColor"].Visible = false;

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                if (dgvProductDetails.Rows[i].Cells["CellColor"].Value.ToString() == "Green")
                {
                    dgvProductDetails.Rows[i].Cells["State"].Style.BackColor = Color.Green;
                }
                else if (dgvProductDetails.Rows[i].Cells["CellColor"].Value.ToString() == "Red")
                {
                    dgvProductDetails.Rows[i].Cells["State"].Style.BackColor = Color.Red;
                }
                else if (dgvProductDetails.Rows[i].Cells["CellColor"].Value.ToString() == "Orange")
                {
                    dgvProductDetails.Rows[i].Cells["State"].Style.BackColor = Color.Orange;
                }
                else if (dgvProductDetails.Rows[i].Cells["CellColor"].Value.ToString() == "Violet")
                {
                    dgvProductDetails.Rows[i].Cells["State"].Style.BackColor = Color.Violet;
                }
            }
            dgvProductDetails.ClearSelection();
        }

        private Image GetProductPhoto(int ProductID)
        {
            Image imgProduct = null;
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".dbo.ProductMaster WITH(NOLOCK) WHERE ProductID=" + ProductID);
            if (ObjUtil.ValidateTable(dt))
            {
                if (dt.Rows[0]["Photo"] != DBNull.Value)
                {
                    DataTable dtImagePath = ObjDAL.ExecuteSelectStatement("  SELECT ImagePath, Extension FROM " + clsUtility.DBName + ".dbo.DefaultStoreSetting WITH(NOLOCK) WHERE MachineName='" + Environment.MachineName + "'");
                    if (ObjUtil.ValidateTable(dtImagePath))
                    {
                        if (dtImagePath.Rows[0]["ImagePath"] != DBNull.Value)
                        {
                            string ImgPath = dtImagePath.Rows[0]["ImagePath"].ToString();
                            string extension = dtImagePath.Rows[0]["Extension"].ToString();

                            string imgFile = ImgPath + "//" + dt.Rows[0]["Photo"].ToString() + extension;
                            if (File.Exists(imgFile))
                            {
                                imgProduct = Image.FromFile(imgFile);
                            }
                            else
                            {
                                imgProduct = null;
                            }
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);
                        }
                    }
                }
            }
            return imgProduct;
        }

        private bool ValidateEnterQTY()
        {
            object EnterQTY = dtStoreTransfer.Compute("SUM(EnterQTY)", string.Empty);
            if (Convert.ToInt32(EnterQTY) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (ValidateEnterQTY())
            {
                int a = 0;
                DataRow[] drow = dtStoreTransfer.Select("BillQTY=0");
                drow[0].Delete();
                dtStoreTransfer.AcceptChanges();
                for (int i = 0; i < dtStoreTransfer.Rows.Count; i++)
                {
                    ObjDAL.UpdateColumnData("EnterQTY", SqlDbType.Int, dtStoreTransfer.Rows[i]["EnterQTY"]);
                    ObjDAL.UpdateColumnData("UpdatedBy", SqlDbType.Int, clsUtility.LoginID);
                    ObjDAL.UpdateColumnData("UpdatedOn", SqlDbType.DateTime, DateTime.Now);
                    a = ObjDAL.UpdateData(clsUtility.DBName + ".dbo.tblStoreTransferItemDetails", "TransferItemID=" + dtStoreTransfer.Rows[i]["TransferItemID"]);
                }
                if (a > 0)
                {
                    clsUtility.ShowInfoMessage("Entered QTY details Updated.", clsUtility.strProjectTitle);
                    this.Close();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Entered QTY details is not Updated.", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("Scan transfer Items.", clsUtility.strProjectTitle);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            bool b = clsUtility.ShowQuestionMessage("Are you sure want to reset Entered QTY?", clsUtility.strProjectTitle);
            if (b)
            {
                for (int i = 0; i < dtStoreTransfer.Rows.Count; i++)
                {
                    dtStoreTransfer.Rows[i]["EnterQTY"] = 0;
                    dtStoreTransfer.Rows[i]["CellColor"] = "Red";
                }
                dtStoreTransfer.AcceptChanges();
                dgvProductDetails.DataSource = dtStoreTransfer;
            }
        }

        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1)
                {
                    return;
                }

                int ProductID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["ProductID"].Value);
                picProduct.Image = GetProductPhoto(ProductID);
            }
            catch (Exception ex)
            {
                clsUtility.ShowInfoMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }
    }
}