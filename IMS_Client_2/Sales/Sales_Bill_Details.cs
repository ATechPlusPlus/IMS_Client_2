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
    public partial class Sales_Bill_Details : Form
    {
        public Sales_Bill_Details()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);
            dgvProductDetails.Columns["Id"].Visible = false;
            dgvProductDetails.Columns["SalesMan"].Visible = false;
            dgvProductDetails.Columns["ShopeID"].Visible = false;
        }

        private void Sales_Bill_Details_Load(object sender, EventArgs e)
        {
            FillStoreData();
            dtpToDate.MaxDate = DateTime.Now;
            dtpFromDate.MaxDate= DateTime.Now;
            radByDate.Checked = true;
            string condition = " Convert(date,InvoiceDate) between Convert(Date,'" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "') and Convert(date,'" + dtpToDate.Value.ToString("yyyy-MM-dd") + "')";
            LoadData(condition);
        }

        private void LoadData(string strCondition)
        {
            if (strCondition==string.Empty)
            {
                string strQ = "select * from  " + clsUtility.DBName + ".dbo.View_SalesBillDetails";
                dgvProductDetails.DataSource = ObjDAL.ExecuteSelectStatement(strQ);
                lblCOunt.Text = dgvProductDetails.Rows.Count.ToString();
            }
            else
            {
                string strQ = "select * from  " + clsUtility.DBName + ".dbo.View_SalesBillDetails where " + strCondition;
                dgvProductDetails.DataSource = ObjDAL.ExecuteSelectStatement(strQ);
                lblCOunt.Text = dgvProductDetails.Rows.Count.ToString();
            }
        }

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

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                txtInvoiceNumber.Clear();
                txtSalesMan.Clear();
                LoadData(string.Empty);
            }
        }

        private void txtInvoiceNumber_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtInvoiceNumber_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void radByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radByDate.Checked)
            {
                txtInvoiceNumber.Enabled = false;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                cmbShop.Enabled = false;
                txtSalesMan.Enabled = false;
                txtSalesMan.Clear();
                dtpFromDate.Focus();
            }
        }

        private void radByInvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (radByInvoice.Checked)
            {
                txtInvoiceNumber.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                cmbShop.Enabled = false;
                txtSalesMan.Enabled = false;
                txtInvoiceNumber.Focus();
                txtSalesMan.Clear();
            }
            else
            {
                txtInvoiceNumber.Enabled = false;
            }
        }

        private void txtInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text.Trim().Length==0)
            {
                return;
            }
            string strCondition = " InvoiceNumber='"+txtInvoiceNumber.Text+"'";
            LoadData(strCondition);
        }

        private void radBySalesMan_CheckedChanged(object sender, EventArgs e)
        {
            if (radBySalesMan.Checked)
            {
                txtInvoiceNumber.Enabled = false;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                cmbShop.Enabled = false;
                txtSalesMan.Enabled = true;
                txtInvoiceNumber.Clear();
                txtSalesMan.Focus();
            }
        }

        private void txtSalesMan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSalesMan.Text.Trim().Length==0)
                {
                    return;
                }
                DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT Empid,Name FROM " + clsUtility.DBName + ".dbo.employeeDetails WHERE Name LIKE '" + txtSalesMan.Text + "%'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ObjUtil.SetControlData(txtSalesMan, "Name");
                    ObjUtil.SetControlData(txtEmpID, "Empid");
                    ObjUtil.ShowDataPopup(dt, txtSalesMan, this, groupBox1);

                    if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                    {
                        // if there is only one column                
                        ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (ObjUtil.GetDataPopup().ColumnCount > 0)
                        {
                            ObjUtil.GetDataPopup().Columns["Empid"].Visible = false;
                            ObjUtil.SetDataPopupSize(450, 0);
                        }
                    }
                    ObjUtil.GetDataPopup().CellClick += Sales_Bill_Details_CellClick;
                    ObjUtil.GetDataPopup().KeyDown += Sales_Bill_Details_KeyDown;
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Sales_Bill_Details_KeyDown(object sender, KeyEventArgs e)
        {
            string strCondition = "SalesMan='" + txtEmpID.Text + "'";
            LoadData(strCondition);
        }

        private void Sales_Bill_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strCondition = "SalesMan='" + txtEmpID.Text + "'";
            LoadData(strCondition);
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                clsUtility.ShowInfoMessage("From date can not be greater then To Date.", clsUtility.strProjectTitle);
                return;
            }
            string condition = " Convert(date,InvoiceDate) between Convert(Date,'" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "') and Convert(date,'" + dtpToDate.Value.ToString("yyyy-MM-dd") + "')";
            LoadData(condition);
        }

        private void radByShope_CheckedChanged(object sender, EventArgs e)
        {
            if (radByShope.Checked)
            {
                txtInvoiceNumber.Enabled = false;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
                cmbShop.Enabled = true;
                txtSalesMan.Enabled = false;
                txtSalesMan.Clear();
                cmbShop.Focus();
            }
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string condition = "Shopeid=" + cmbShop.SelectedValue.ToString();
            LoadData(condition);
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date>dtpToDate.Value.Date)
            {
                clsUtility.ShowInfoMessage("From date can not be greater then To Date.", clsUtility.strProjectTitle);
                return;
            }
            string condition = " Convert(date,InvoiceDate) between Convert(Date,'" + dtpFromDate.Value.ToString("yyyy-MM-dd") + "') and Convert(date,'" + dtpToDate.Value.ToString("yyyy-MM-dd") + "')";
            LoadData(condition);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgvProductDetails.SelectedRows.Count==0)
            {
                clsUtility.ShowInfoMessage("Select a bill from the given list.", clsUtility.strProjectTitle);
                return;

            }

            string InvoiceID = dgvProductDetails.SelectedRows[0].Cells["id"].Value.ToString();
            Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
            frmSalesInvoice.InvoiceID = Convert.ToInt32(InvoiceID);
            frmSalesInvoice.IsDirectPrint = false;
            frmSalesInvoice.Show();
            
        }
    }
}