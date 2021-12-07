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
        string strBarcode = string.Empty;

        DataTable dtStoreTransfer = new DataTable();
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmTransferCheck_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            btnClear.BackgroundImage = B_Leave;

            txtBarCode.Focus();
            pnlViolet.BackColor = Color.Violet;
            LoadData();
        }

        private void LoadData()
        {
            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, StoreBillDetailsID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_ItemDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                dtStoreTransfer = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtStoreTransfer))
                {
                    dgvProductDetails.DataSource = dtStoreTransfer;
                    txtDate.Text = Convert.ToDateTime(dtStoreTransfer.Rows[0]["BillDate"]).ToShortDateString();
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
                strBarcode = txtBarCode.Text.Trim();
                DataRow[] drow = dtStoreTransfer.Select("Barcode='" + txtBarCode.Text + "'");
                if (drow.Length > 0)
                {
                    int pTotal = 0;
                    int pEnterQty = drow[0]["EnterQTY"].ToString() == "" ? 0 : Convert.ToInt32(drow[0]["EnterQTY"]);
                    int pBillQty = Convert.ToInt32(drow[0]["BillQTY"]);
                    //pTotal = pBillQty + pEnterQty;
                    pTotal = pEnterQty + 1;
                    if (pBillQty == pTotal)
                    {
                        drow[0]["CellColor"] = "Green";
                    }
                    else if (pBillQty < pTotal && drow[0]["CellColor"].ToString() != "Violet")
                    {
                        drow[0]["CellColor"] = "Orange";
                    }
                    drow[0]["EnterQTY"] = pTotal;
                    dtStoreTransfer.AcceptChanges();



                    dgvProductDetails.DataSource = dtStoreTransfer;

                    picProduct.Image = GetProductPhoto(Convert.ToInt32(drow[0]["SubProductID"]));

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
                            row["TransferItemID"] = dt.Rows[0]["TransferItemID"];
                            row["StoreBillDetailsID"] = dt.Rows[0]["StoreBillDetailsID"];
                            row["Barcode"] = dt.Rows[0]["Barcode"];
                            row["CellColor"] = dt.Rows[0]["CellColor"];
                            row["StyleNo"] = dt.Rows[0]["StyleNo"];
                            row["ProductID"] = dt.Rows[0]["ProductID"];
                            row["SubProductID"] = dt.Rows[0]["SubProductID"];
                            row["BillQTY"] = 0;
                            row["EnterQTY"] = 1;
                            row["Item"] = dt.Rows[0]["Item"];
                            row["Color"] = dt.Rows[0]["Color"];
                            row["Size"] = dt.Rows[0]["Size"];
                            row["Total"] = dt.Rows[0]["Total"];
                            row["ColorID"] = dt.Rows[0]["ColorID"];
                            row["SizeID"] = dt.Rows[0]["SizeID"];
                            dtStoreTransfer.Rows.Add(row);
                            dtStoreTransfer.AcceptChanges();
                            dgvProductDetails.DataSource = dtStoreTransfer;

                            picProduct.Image = GetProductPhoto(Convert.ToInt32(dt.Rows[0]["SubProductID"]));

                            txtBarCode.Clear();
                            txtBarCode.Focus();
                        }
                        else
                        {
                            clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + txtBarCode.Text, clsUtility.strProjectTitle);
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
            try
            {
                dgvProductDetails.ClearSelection();
                ObjUtil.SetRowNumber(dgvProductDetails);
                ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, Color.White);

                dgvProductDetails.Columns["TransferItemID"].Visible = false;
                dgvProductDetails.Columns["StoreBillDetailsID"].Visible = false;
                dgvProductDetails.Columns["ProductID"].Visible = false;
                dgvProductDetails.Columns["SubProductID"].Visible = false;
                dgvProductDetails.Columns["BillDate"].Visible = false;
                dgvProductDetails.Columns["BillNo"].Visible = false;
                dgvProductDetails.Columns["TotalQTY"].Visible = false;
                dgvProductDetails.Columns["Total"].Visible = false;
                dgvProductDetails.Columns["CellColor"].Visible = false;
                dgvProductDetails.Columns["ColorID"].Visible = false;
                dgvProductDetails.Columns["SizeID"].Visible = false;

                ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill, Color.White);
                dgvProductDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;

                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    if (strBarcode != string.Empty)
                    {
                        if (dgvProductDetails.Rows[i].Cells["Barcode"].Value.ToString() == "" + strBarcode + "")
                        {
                            dgvProductDetails.Rows[i].Cells["Barcode"].Selected = true;
                            dgvProductDetails.FirstDisplayedScrollingRowIndex = i;
                        }
                    }
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

                //dgvProductDetails.ClearSelection();
                //dgvProductDetails.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
                //dgvProductDetails.RowsDefaultCellStyle.SelectionForeColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                //clsUtility.ShowErrorMessage(ex.Message);
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
                bool b = false;
                for (int i = 0; i < dtStoreTransfer.Rows.Count; i++)
                {
                    ObjDAL.SetStoreProcedureData("TransferItemID", SqlDbType.Int, dtStoreTransfer.Rows[i]["TransferItemID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, dtStoreTransfer.Rows[i]["StoreBillDetailsID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("ProductID", SqlDbType.Int, dtStoreTransfer.Rows[i]["ProductID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("SubProductID", SqlDbType.Int, dtStoreTransfer.Rows[i]["SubProductID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, dtStoreTransfer.Rows[i]["StyleNo"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("Barcode", SqlDbType.NVarChar, dtStoreTransfer.Rows[i]["Barcode"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("BillQTY", SqlDbType.Int, dtStoreTransfer.Rows[i]["BillQTY"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("EnterQTY", SqlDbType.Int, dtStoreTransfer.Rows[i]["EnterQTY"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("ColorID", SqlDbType.Int, dtStoreTransfer.Rows[i]["ColorID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("SizeID", SqlDbType.Int, dtStoreTransfer.Rows[i]["SizeID"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("Total", SqlDbType.Decimal, dtStoreTransfer.Rows[i]["Total"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("CellColor", SqlDbType.VarChar, dtStoreTransfer.Rows[i]["CellColor"], clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);

                    b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Insert_StoreTransferItemDetails_Voilet");
                }
                if (b)
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
                    if (Convert.ToInt32(dtStoreTransfer.Rows[i]["BillQTY"]) == 0)
                    {
                        dtStoreTransfer.Rows[i]["EnterQTY"] = 1;
                        continue;
                    }
                    dtStoreTransfer.Rows[i]["EnterQTY"] = 0;
                    dtStoreTransfer.Rows[i]["CellColor"] = "Red";
                }
                dtStoreTransfer.AcceptChanges();
                dgvProductDetails.DataSource = dtStoreTransfer;

                txtBarCode.Focus();
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
                //int pSubProductID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["SubProductID"].Value);
                int pSubProductID = Convert.ToInt32(dgvProductDetails.Rows[e.RowIndex].Cells["SubProductID"].Value);
                picProduct.Image = GetProductPhoto(pSubProductID);
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void DeleteVioletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmTransferCheck, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                if (dgvProductDetails.CurrentCell != null && dgvProductDetails.CurrentCell.RowIndex >= 0)
                {
                    int row = dgvProductDetails.CurrentCell.RowIndex;

                    DataTable dt = (DataTable)dgvProductDetails.DataSource;
                    dt.Rows[row].Delete();
                    dt.AcceptChanges();
                    //dgvProductDetails.Rows.RemoveAt(dgvProductDetails.SelectedRows[0].Index);
                    //dgvProductDetails.EndEdit();
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    clsUtility.ShowInfoMessage("Select Barcode to delete Violet", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                //if (dgvProductDetails.SelectedRows != null && dgvProductDetails.SelectedRows.Count > 0)
                if (dgvProductDetails.CurrentCell != null && dgvProductDetails.CurrentCell.RowIndex >= 0)
                {
                    int row = dgvProductDetails.CurrentCell.RowIndex;
                    if (dgvProductDetails.Rows[row].Cells["CellColor"].Value.ToString() == "Violet")
                    {
                        contextMenuStrip1.Items[0].Enabled = true;
                    }
                    else
                    {
                        contextMenuStrip1.Items[0].Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }

        private void dgvProductDetails_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            //EnterQTY
            //dgvProductDetails.Columns[e.Column.Index].ReadOnly = true;
            //if (dgvProductDetails.Columns.Contains("EnterQTY") && clsUtility.IsAdmin)
            //{
            //    dgvProductDetails.Columns["EnterQTY"].ReadOnly = false;
            //}
        }
    }
}