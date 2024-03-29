﻿using System;
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

        DataTable dtDefault = new DataTable();
        private void frmReceivedBranchTransfer_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            btnClose.BackgroundImage = B_Leave;

            dtDefault = (DataTable)dgvProductDetails.DataSource;
            LoadData();
        }
        private void LoadData()
        {
            dgvProductDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            dgvProductDetails.RowHeadersVisible = false; // set it to false if not needed

            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_ReceiveBranch_Transfer_List");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvProductDetails.DataSource = dt;
                }
                else
                {
                    dgvProductDetails.DataSource = dtDefault;
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
            if (StoreBillDetailsID > 0)
            {
                ObjDAL.SetStoreProcedureData("ReceiveBillNo", SqlDbType.NVarChar, GenerateReceiveBillNumber(), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreBillDetailsID", SqlDbType.Int, StoreBillDetailsID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Insert_ReceiveBillDetails");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Flag"]) == 1)
                        {
                            StoreBillDetailsID = 0;
                            LoadData();
                        }
                        clsUtility.ShowInfoMessage(dt.Rows[0]["Msg"].ToString());
                    }
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("There is no Update found for Receive Bill details.", clsUtility.strProjectTitle);
            }
            ObjDAL.ResetData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}