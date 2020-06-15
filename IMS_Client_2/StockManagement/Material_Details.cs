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
    public partial class Material_Details : Form
    {
        public Material_Details()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        private void FillStoreData()
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".[dbo].[StoreMaster]", "StoreID,StoreName", "ISNULL(ActiveStatus,1)=1", "StoreName ASC");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbShop.DataSource = dt;
                cmbShop.DisplayMember = "StoreName";
                cmbShop.ValueMember = "StoreID";
                cmbShop.SelectedIndex = -1;
            }
        }

        private void txtSearchByProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchByProductName.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_ProductDetails_Popup '" + txtSearchByProductName.Text + "'");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtSearchByProductName, "ProductName");
                        ObjUtil.SetControlData(txtProductID, "ProductID");
                        ObjUtil.ShowDataPopup(dt, txtSearchByProductName, this, groupBox1);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["ProductID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["CategoryID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += Material_Details_SearchByProductName_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += Material_Details_SearchByProductName_KeyDown;
                    }
                    else
                    {

                        ObjUtil.CloseAutoExtender();
                    }
                }
                else
                {
                    txtProductID.Clear();
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Material_Details_SearchByProductName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void Material_Details_SearchByProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSearchByProductName.SelectionStart = txtSearchByProductName.MaxLength;
                txtSearchByProductName.Focus();
                SearchByProductID();
            }
        }

        private void SearchByProductID()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details " + txtProductID.Text + ",NULL");
            if (ObjUtil.ValidateTable(dt))
            {
                dgvProductDetails.DataSource = dt;
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void SearchByShopID()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details NULL," + cmbShop.SelectedValue);
            if (ObjUtil.ValidateTable(dt))
            {
                dgvProductDetails.DataSource = dt;
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void Material_Details_KeyDown(object sender, KeyEventArgs e)
        {
            txtSearchByProductName.Focus();
        }

        private void Material_Details_Load(object sender, EventArgs e)
        {
            FillStoreData();
            LoadData();
            rdShowAll.Checked = true;
        }
        private void LoadData()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_Material_Details 0,0");
            if (ObjUtil.ValidateTable(dt))
            {
                dgvProductDetails.DataSource = dt;
            }
            else
            {
                dgvProductDetails.DataSource = null;
            }
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchByShopID();
        }

        private void rdSearchByStore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByStore.Checked)
            {
                cmbShop.Enabled = true;
                cmbShop.Focus();
            }
            else
            {
                cmbShop.Enabled = false;
                cmbShop.SelectedIndex = -1;
            }
        }

        private void rdSearchByItem_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSearchByItem.Checked)
            {
                txtSearchByProductName.Enabled = true;
                txtSearchByProductName.Focus();
            }
            else
            {
                txtSearchByProductName.Enabled = false;
                txtSearchByProductName.Clear();
            }
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                LoadData();
            }
            else
            {
                cmbShop.SelectedIndex = -1;
                txtSearchByProductName.Clear();
            }
        }

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.ColumnHeader);
            //ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvProductDetails.Columns["ProductID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["CategoryID"].Visible = false;
            dgvProductDetails.Columns["SizeTypeID"].Visible = false;
            dgvProductDetails.Columns["SizeID"].Visible = false;
        }

        private void txtSearchByProductName_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSearchByProductName_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }
    }
}
