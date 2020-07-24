using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmPettyCash : Form
    {
        public frmPettyCash()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            txtPettyCash.Clear();

            txtPettyCash.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtPettyCash))
            {
                clsUtility.ShowInfoMessage("Enter Petty Cash       ", clsUtility.strProjectTitle);
                txtPettyCash.Focus();
                return false;
            }
            return true;
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_PettyCashAmt");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validateform())
            {
                ObjDAL.SetStoreProcedureData("PettyCashAmt", SqlDbType.Decimal, txtPettyCash.Text, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("CreatedBy", SqlDbType.Int, clsUtility.LoginID, clsConnection_DAL.ParamType.Input);
                bool b = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_Insert_PettyCashAmt");
                if (b)
                {
                    clsUtility.ShowInfoMessage("Petty Cash book Saved Successfully..", clsUtility.strProjectTitle);
                    ClearAll();
                    LoadData();
                }
                else
                {
                    clsUtility.ShowInfoMessage("Petty Cash book not Saved Successfully..", clsUtility.strProjectTitle);
                }
                ObjDAL.ResetData();
            }
        }

        private void frmPettyCash_Load(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            LoadData();
        }

        private void btnSave_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
            LoadData();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.Columns["PettyCashExpID"].Visible = false;
            lblTotalRecords.Text = "Total Records : " + dataGridView1.Rows.Count;
        }

        private void txtPettyCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtPettyCash, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
            }
        }
        private void txtPettyCash_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtPettyCash_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }
    }
}