using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Sales
{
    public partial class frmCloseShifWindow : Form
    {
        public frmCloseShifWindow()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        int ID = 0;

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            txtTotalValue.Clear();
        }

        private void LoadData()
        {
            ObjDAL.SetStoreProcedureData("Status", SqlDbType.Int, 1, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_CashBand_Master");
            DataTable dt = ds.Tables[0];
            if (ObjUtil.ValidateTable(dt))
            {
                dgvCloseCash.DataSource = dt;
            }
            else
            {
                dgvCloseCash.DataSource = null;
            }
        }

        private void frmCloseShifWindow_Load(object sender, EventArgs e)
        {
            btnOpenCash.BackgroundImage = B_Leave;
            btnCloseCash.BackgroundImage = B_Leave;
            btnPreview.BackgroundImage = B_Leave;
            btnPrint.BackgroundImage = B_Leave;

            LoadData();
        }

        private void btnOpenCash_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnOpenCash_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void dgvCloseCash_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCloseCash.Columns["CashBandID"].Visible = false;
            dgvCloseCash.Columns["CashBandValue"].Visible = false;
        }
    }
}
