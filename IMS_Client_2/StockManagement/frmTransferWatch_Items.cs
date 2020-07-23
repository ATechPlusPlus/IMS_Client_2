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
    public partial class frmTransferWatch_Items : Form
    {
        public frmTransferWatch_Items()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        public int pStoreBillDetailsID = 0;
        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, pStoreBillDetailsID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_ListItems");
            //DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_ItemDetails");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvTransferWatch.DataSource = dt;
                    txtBillNo.Text = dt.Rows[0]["BillNo"].ToString();
                    txtStoreName.Text = dt.Rows[0]["StoreName"].ToString();

                    txtBillDate.Text = Convert.ToDateTime(dt.Rows[0]["BillDate"]).ToString("yyyy-MM-dd");
                    txtReceiveBillDate.Text = dt.Rows[0]["ReceiveBillDate"] != DBNull.Value ?
                        Convert.ToDateTime(dt.Rows[0]["ReceiveBillDate"]).ToString("yyyy-MM-dd") : "";

                    txtTotalBillQTY.Text = dt.Rows[0]["TotalQTY"].ToString();
                    txtTotalEnterQTY.Text = dt.Rows[0]["ReceivedTotalQTY"].ToString();

                }
                else
                {
                    dgvTransferWatch.DataSource = null;
                }
            }
        }

        private void frmTransferWatch_Items_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvTransferWatch_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvTransferWatch);
            ObjUtil.SetDataGridProperty(dgvTransferWatch, DataGridViewAutoSizeColumnsMode.DisplayedCells);

            dgvTransferWatch.Columns["TransferItemID"].Visible = false;
            dgvTransferWatch.Columns["StoreBillDetailsID"].Visible = false;
            dgvTransferWatch.Columns["ProductID"].Visible = false;
            dgvTransferWatch.Columns["BillDate"].Visible = false;
            dgvTransferWatch.Columns["BillNo"].Visible = false;
            dgvTransferWatch.Columns["TotalQTY"].Visible = false;
            dgvTransferWatch.Columns["Total"].Visible = false;
            dgvTransferWatch.Columns["CellColor"].Visible = false;
            //dgvTransferWatch.Columns["StoreName"].Visible = false;
            dgvTransferWatch.Columns["ReceivedTotalQTY"].Visible = false;
            dgvTransferWatch.Columns["ReceiveBillNo"].Visible = false;
            dgvTransferWatch.Columns["ReceiveBillDate"].Visible = false;

            for (int i = 0; i < dgvTransferWatch.Rows.Count; i++)
            {
                if (dgvTransferWatch.Rows[i].Cells["CellColor"].Value.ToString() == "Green")
                {
                    dgvTransferWatch.Rows[i].Cells["State"].Style.BackColor = Color.Green;
                }
                else if (dgvTransferWatch.Rows[i].Cells["CellColor"].Value.ToString() == "Red")
                {
                    dgvTransferWatch.Rows[i].Cells["State"].Style.BackColor = Color.Red;
                }
                else if (dgvTransferWatch.Rows[i].Cells["CellColor"].Value.ToString() == "Orange")
                {
                    dgvTransferWatch.Rows[i].Cells["State"].Style.BackColor = Color.Orange;
                }
            }
            dgvTransferWatch.ClearSelection();

            dgvTransferWatch.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;
            dgvTransferWatch.RowsDefaultCellStyle.SelectionForeColor = Color.Transparent;
            //dgvTransferWatch.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
    }
}