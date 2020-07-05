﻿using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Purchase
{
    public partial class frmPurchaseDetails : Form
    {
        public frmPurchaseDetails()
        {
            InitializeComponent();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (txtSupplierBillNo.Text.Trim().Length==0)
            {
                clsUtility.ShowInfoMessage("Please Enter Bill Number.", clsUtility.strProjectTitle);
                return;


            }

            ObjDAL.SetStoreProcedureData("BillNo", SqlDbType.NVarChar, txtPurchaseInvoiceID.Text,  clsConnection_DAL.ParamType.Input);
          DataSet ds=  ObjDAL.ExecuteStoreProcedure_Get("sp_DetailPurchaseInvoiceReport");

            if (ds.Tables.Count>0)
            {
                dgvPurchaseInvoice.DataSource = ds.Tables[0];
                if (ds.Tables.Count>1)
                {
                    dgvPurchaseInvoiceDetail.DataSource = ds.Tables[1];
                }
                if (ds.Tables.Count>2)
                {
                    dgvPurchaseItem.DataSource = ds.Tables[2];
                }

                dgvPurchaseInvoice.ClearSelection();
                dgvPurchaseInvoiceDetail.ClearSelection();
                dgvPurchaseItem.ClearSelection();
            
            }
        }
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();
        private void txtSupplierBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierBillNo.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup " + txtSupplierBillNo.Text + ",2");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSupplierBillNo, "SupplierBillNo");
                        ObjUtil.SetControlData(txtPurchaseInvoiceID, "PurchaseInvoiceID");
                        ObjUtil.ShowDataPopup(dt, txtSupplierBillNo, this, this);

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
                        //ObjUtil.GetDataPopup().CellClick += frmDiffPurchaseReceived_CellClick;
                        //ObjUtil.GetDataPopup().KeyDown += frmDiffPurchaseReceived_KeyDown;
                    }
                    else
                    {
                        ObjUtil.CloseAutoExtender();
                    }
                }
                else
                {
                    txtPurchaseInvoiceID.Clear();
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {

            }
        }
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void frmPurchaseDetails_Load(object sender, EventArgs e)
        {
            btnViewDetails.BackgroundImage = B_Leave;
            
        }

        private void dgvPurchaseInvoice_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvPurchaseInvoice, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            dgvPurchaseInvoice.Columns["PurchaseInvoiceID"].Visible = false;
        }

        private void dgvPurchaseInvoiceDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvPurchaseInvoiceDetail, DataGridViewAutoSizeColumnsMode.Fill);
            dgvPurchaseInvoiceDetail.Columns["PurchaseInvoiceID"].Visible = false;
        }

        private void dgvPurchaseItem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvPurchaseItem, DataGridViewAutoSizeColumnsMode.Fill);
            dgvPurchaseItem.Columns["PurchaseInvoiceID"].Visible = false;
        }
    }
}
