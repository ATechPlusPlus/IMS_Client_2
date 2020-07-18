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

        private void LoadData()
        {
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_StoreTransfer_List");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dgvTransferWatch.DataSource = dt;
                    if (dgvTransferWatch.Columns.Contains("ColView"))
                    {
                        dgvTransferWatch.Columns.Remove("ColView");
                    }
                    DataGridViewButtonColumn ColView = new DataGridViewButtonColumn();
                    ColView.DataPropertyName = "View";
                    ColView.HeaderText = "View";
                    ColView.Name = "ColView";
                    ColView.Text = "View";
                    ColView.UseColumnTextForButtonValue = true;
                    //dataGridView1.Columns.Add(ColView);
                    dgvTransferWatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ColView });
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
    }
}
