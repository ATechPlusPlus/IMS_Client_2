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
    public partial class frmReceivedBranchTransfer : Form
    {
        public frmReceivedBranchTransfer()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        int StoreBillDetailsID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmReceivedBranchTransfer_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            btnClose.BackgroundImage = B_Leave;
            LoadData();
        }
        private void LoadData()
        {
            //string query = "select StoreTransferID, " +
            //                "(select StoreName from StoreMaster where StoreID = sm.FromStore) as Branch, BillNo," +
            //                " BillDate, TotalQTY, ' ' as Sender, ' ' as Post, 'Identical' as Identical" +

            //                 " from " + clsUtility.DBName + ".[dbo].[tblStoreTransferBillDetails] as sm";

            //DataTable dt = ObjDAL.ExecuteSelectStatement(query);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ReceiveBranch_Transfer_List");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    dgvProductDetails.DataSource = null;
                }
            }
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
        }

        private void dgvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (dgvProductDetails.Columns[e.ColumnIndex].Name == "Identical")
            {
                StockManagement.frmTransferCheck frmTransferCheck = new frmTransferCheck();
                frmTransferCheck.fromShopID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["FromStore"].Value);
                StoreBillDetailsID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["TransferID"].Value);
                frmTransferCheck.StoreBillDetailsID = StoreBillDetailsID;
                frmTransferCheck.Show();
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

        private void lnkRefreshData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadData();
        }

        private string GenerateReceiveBillNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Seq_StoreTransferReceiveBill");
            string InvoiceNumber = "RECEBILL-" + LastID;

            return InvoiceNumber;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            //Create SP and Store into two tables tblStoreTransferReceiveBillDetails,tblStoreTransferReceiveBillItemDetails
            if (StoreBillDetailsID > 0)
            {
                ObjDAL.SetStoreProcedureData("ReceiveBillNo", SqlDbType.NVarChar, GenerateReceiveBillNumber(), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, StoreBillDetailsID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Insert_ReceiveBillDetails");
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        clsUtility.ShowInfoMessage(dt.Rows[0]["Msg"].ToString(), clsUtility.strProjectTitle);
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("There is no Update found for Receive Bill details.", clsUtility.strProjectTitle);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}