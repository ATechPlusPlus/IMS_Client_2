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
    public partial class frmItemWiseModelNo : Form
    {
        public frmItemWiseModelNo()
        {
            InitializeComponent();
        }
        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        private void frmItemWiseModelNo_Load(object sender, EventArgs e)
        {
            LoadItemModelDetails();
        }

        private void LoadItemModelDetails()
        {
            string strSql = "  select md.SubProductID,md.ProductID, pm.ProductName, md.ModelNo,bm.BrandName, sm.storeName,md.BrandID,md.StoreID,md.Photo " +
                          " FROM " + clsUtility.DBName + ".[dbo].[tblProductWiseModelNo] md join " + clsUtility.DBName + ".dbo.ProductMaster pm " +
                          " on md.ProductID = pm.ProductID join BrandMaster bm on bm.BrandID = md.BrandID join " +
                                clsUtility.DBName + ".dbo.StoreMaster sm on sm.storeID = md.storeID";

            DataTable dtItemModel = ObjDAL.ExecuteSelectStatement(strSql);
            if (ObjUtil.ValidateTable(dtItemModel))
            {
                dgvProductDetails.DataSource = dtItemModel;
            }
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetDataGridProperty(dgvProductDetails);
            dgvProductDetails.Columns["SubProductID"].Visible = false;
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["BrandID"].Visible = false;
        }
    }
}