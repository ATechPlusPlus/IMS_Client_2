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
        CoreApp.clsConnection_DAL ObjCon = new clsConnection_DAL(true);

        private void frmReceivedBranchTransfer_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string query= "select StoreTransferID, "+
                            "(select StoreName from StoreMaster where StoreID = sm.FromStore) as Branch, BillNo,"+
                            " BillDate, TotalQTY, ' ' as Sender, ' ' as Post, 'Identical' as Identical"+

                             " from IMS_Client_2.[dbo].[tblStoreTransferBillDetails] as sm";

          DataTable dt=  ObjCon.ExecuteSelectStatement(query);
            if (dt.Rows.Count>0)
            {
                dgvProductDetails.DataSource = dt;
            }
        }
        CoreApp.clsUtility ObjUtil = new clsUtility();
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

                frmTransferCheck.StoreBillDetailsID = Convert.ToInt32(dgvProductDetails.SelectedRows[0].Cells["TransferID"].Value);
                frmTransferCheck.Show();
            }
        }
    }
}
