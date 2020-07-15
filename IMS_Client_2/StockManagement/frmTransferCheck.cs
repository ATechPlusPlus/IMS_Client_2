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
        CoreApp.clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        private void frmTransferCheck_Load(object sender, EventArgs e)
        {

        }
        public int StoreBillDetailsID = 0;
        public int fromShopID = 0;
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (txtBarCode.Text.Trim().Length != 0)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GetItemDetailsByProductID(txtBarCode.Text);
                }

            }
        }
        private bool CheckProductQTY(string _BarCoeNumber, decimal CurQTY)
        {
            string strSQL = "SELECT QTY FROM  " + clsUtility.DBName + ".[dbo].[tblStoreTransferItemDetails] WITH(NOLOCK) WHERE BarcodeNo=" + _BarCoeNumber + " AND StoreBillDetailsID=" + StoreBillDetailsID;

            decimal TotalQTY = Convert.ToDecimal(ObjCon.ExecuteScalar(strSQL));

            if (CurQTY > TotalQTY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        DataTable dtItemDetails = new DataTable();
        private bool IsItemExist(string barCode)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            if (dRow.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void InitItemTable()
        {
            dtItemDetails.Columns.Add("ProductID");
            dtItemDetails.Columns.Add("ProductName");
            dtItemDetails.Columns.Add("BarcodeNo");
            dtItemDetails.Columns.Add("ColorID");
            dtItemDetails.Columns.Add("Color");
            dtItemDetails.Columns.Add("SizeID");
            dtItemDetails.Columns.Add("Size");
            dtItemDetails.Columns.Add("QTY");
            dtItemDetails.Columns.Add("Rate");

            dtItemDetails.Columns.Add("OIRate");
            dtItemDetails.Columns.Add("Adj_Amount");

            dtItemDetails.Columns.Add("Total");
            dtItemDetails.Columns.Add("Delete");


        }
        private void UpdateQTYByOne(string barCode, decimal rate)
        {
            DataRow[] dRow = dtItemDetails.Select("BarcodeNo='" + barCode + "'");
            // add one qty
            decimal NewQTY = Convert.ToDecimal(dRow[0]["QTY"]) + 1;
            if (CheckProductQTY(barCode, Convert.ToDecimal(NewQTY)))
            {
                // set to col
                dRow[0]["QTY"] = NewQTY.ToString();
                // cal total
                decimal total = rate * NewQTY;
                // set the total
                dRow[0]["Total"] = total.ToString();
                dtItemDetails.AcceptChanges();
                dgvProductDetails.DataSource = dtItemDetails;
            }
            else
            {
                clsUtility.ShowInfoMessage("No QTY avaiable for the given Product.", clsUtility.strProjectTitle);
            }
        }
        private Image GetProductPhoto(int ProductID)
        {
            Image imgProduct = null;
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT Photo FROM " + clsUtility.DBName + ".dbo.ProductMaster WHERE ProductID=" + ProductID);
            if (ObjUtil.ValidateTable(dt))
            {
                if (dt.Rows[0]["Photo"] != DBNull.Value)
                {
                    DataTable dtImagePath = ObjCon.ExecuteSelectStatement("  select ImagePath, Extension from [IMS_Client_2].dbo.DefaultStoreSetting where MachineName='" + Environment.MachineName + "'");
                    if (dtImagePath.Rows.Count > 0)
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
                            MessageBox.Show("Image file for the selected product doesn't exist.", clsUtility.strProjectTitle);

                        }
                    }
                }
            }
            return imgProduct;
        }
        private void AddRowToItemDetails(string productID, string name, string qty, string rate, string total,
        string BarCode, string SizeID, string Size, string ColorID, string Color)
        {
            DataRow dRow = dtItemDetails.NewRow();
            dRow["ProductID"] = productID;
            dRow["ProductName"] = name;
            dRow["QTY"] = qty;
            dRow["Rate"] = rate;
            dRow["Total"] = total;
            dRow["Delete"] = "Delete";
            dRow["ColorID"] = ColorID;
            dRow["Color"] = Color;
            dRow["SizeID"] = SizeID;
            dRow["Size"] = Size;
            dRow["BarcodeNo"] = BarCode;



            dtItemDetails.Rows.Add(dRow);
            dtItemDetails.AcceptChanges();

            dgvProductDetails.DataSource = dtItemDetails;
        }
        private void CalculateGrandTotal()
        {
            decimal NewBillAmount = 0.0M;
            decimal TotalQTY = 0;
         

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                NewBillAmount += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["Total"].Value);
            }

            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                TotalQTY += Convert.ToDecimal(dgvProductDetails.Rows[i].Cells["QTY"].Value);
            }


            txtTotalQTY.Text = TotalQTY.ToString();
            txtValue.Text = NewBillAmount.ToString();



        }

        CoreApp.clsUtility ObjUtil = new clsUtility();
        private void GetItemDetailsByProductID(string _BarCodeValue)
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.GetProductDetailsByBarCode " + fromShopID + ", " + _BarCodeValue);
            if (ObjUtil.ValidateTable(dt))
            {
                string pID = dt.Rows[0]["ProductID"].ToString();
                string name = dt.Rows[0]["ProductName"].ToString();
                string rate = dt.Rows[0]["Rate"].ToString();
                string barCode = dt.Rows[0]["BarcodeNo"].ToString();
                string qty = "1";
                string SizeID = dt.Rows[0]["SizeID"].ToString();
                string Size = dt.Rows[0]["Size"].ToString();
                string ColorID = dt.Rows[0]["ColorID"].ToString();
                string ColorName = dt.Rows[0]["ColorName"].ToString();
                decimal total = Convert.ToDecimal(rate) * Convert.ToDecimal(qty);

                if (CheckProductQTY(barCode, Convert.ToDecimal(qty)))
                {
                    // if Item already there in the grid, then just increase the QTY
                    if (IsItemExist(barCode))
                    {
                        UpdateQTYByOne(barCode.ToString(), Convert.ToDecimal(rate));
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));
                    }
                    else
                    {
                        AddRowToItemDetails(pID, name, qty, rate, total.ToString(), barCode, SizeID, Size, ColorID, ColorName);
                        picProduct.Image = GetProductPhoto(Convert.ToInt32(pID));

                    }

                    txtBarCode.Clear();

                    CalculateGrandTotal();
                    dgvProductDetails.ClearSelection();
                    txtBarCode.Focus();
                }
                else
                {
                    clsUtility.ShowInfoMessage("No QTY avaiable for the Product : " + txtBarCode.Text, clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Product Found for the barcode value : " + _BarCodeValue, clsUtility.strProjectTitle);
            }
        }
    }
}
