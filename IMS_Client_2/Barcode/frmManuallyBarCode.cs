using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Barcode
{
    public partial class frmManuallyBarCode : Form
    {
        public frmManuallyBarCode()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmManuallyBarCode_Load(object sender, EventArgs e)
        {
            btnPrintManualBarcode.BackgroundImage = B_Leave;

            LoadData();
        }

        private void btnPrintManualBarcode_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }
        private void btnPrintManualBarcode_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void txtPurchaseInvoice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPurchaseInvoice.TextLength > 0)
                {
                    //DataTable dt = ObjCon.ExecuteSelectStatement("EXEC  " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtPurchaseInvoice.Text + "', 1");
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC  " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtPurchaseInvoice.Text + "', 0");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtPurchaseInvoice, "SupplierBillNo");
                        ObjUtil.SetControlData(txtPurchaseID, "PurchaseInvoiceID");

                        ObjUtil.ShowDataPopup(dt, txtPurchaseInvoice, this, this);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            // if there is only one column                
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["PurchaseInvoiceID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["SupplierID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += frmManuallyBarCode_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += frmManuallyBarCode_KeyDown; ;
                    }
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmManuallyBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                // LoadData();
            }
        }

        private void frmManuallyBarCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //LoadData();
        }

        private void LoadData()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT SupplierBillNo FROM PurchaseInvoice WITH(NOLOCK) WHERE PurchaseInvoiceID IN(SELECT DISTINCT PurchaseInvoiceID FROM ProductStockMaster WITH(NOLOCK) where QTY > 0) ");
            if (ObjUtil.ValidateTable(dt))
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = null;
                //txtPurchaseID.Clear();
                clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
            }
        }

        private void SearchByStyleNo()
        {
            ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, txtPurchaseID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, cmbListBox.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("PrintStaus", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtPurchaseInvDetails = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtPurchaseInvDetails))
                {
                    dataGridView1.DataSource = dtPurchaseInvDetails;
                    //txtPurchaseID.Text = "";
                }
                else
                {
                    dataGridView1.DataSource = dtPurchaseInvDetails;
                    //txtPurchaseID.Clear();
                    clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
                }
            }
            ObjDAL.ResetData();
        }

        private void btnPrintManualBarcode_Click(object sender, EventArgs e)
        {
            if (txtPurchaseInvoice.TextLength > 0)
            {
                ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, txtPurchaseID.Text, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("SupplierBillNo", SqlDbType.NVarChar, txtPurchaseInvoice.Text.Trim(), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PurchaseInvoice_Barcode_Generated");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dtPurchaseInvDetails = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dtPurchaseInvDetails))
                    {
                        clsUtility.ShowInfoMessage(dtPurchaseInvDetails.Rows[0]["Msg"].ToString(), clsUtility.strProjectTitle);

                        LoadData();
                    }
                }
                ObjDAL.ResetData();
            }
            else
            {
                clsUtility.ShowInfoMessage("Enter INvoice Number..", clsUtility.strProjectTitle);
                txtPurchaseInvoice.Focus();
            }
        }

        private void cmbListBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //SearchByStyleNo();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
        }
    }
}
