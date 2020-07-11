using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Purchase
{
    public partial class frmDiffPurchaseReceived : Form
    {
        public frmDiffPurchaseReceived()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void ClearAll()
        {
            txtSupplierBillNo.Clear();
            txtPurchaseInvoiceID.Clear();
            dataGridView1.DataSource = null;
            btnViewDetails.Enabled = false;
            txtSupplierBillNo.Focus();
        }

        private bool Validateform()
        {
            if (ObjUtil.IsControlTextEmpty(txtSupplierBillNo))
            {
                clsUtility.ShowInfoMessage("Enter Supplier Bill No           ", clsUtility.strProjectTitle);
                txtSupplierBillNo.Focus();
                return false;
            }
            return true;
        }

        private void LoadData()
        {
            DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_DiffPurchase_Received " + txtPurchaseInvoiceID.Text);
            if (ObjUtil.ValidateTable(dt))
            {
                btnViewDetails.Enabled = true;
                dataGridView1.DataSource = dt;
            }
            else
            {
                btnViewDetails.Enabled = false;
                dataGridView1.DataSource = null;
            }
        }

        private void txtSupplierBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplierBillNo.Text.Length > 0)
                {
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtSupplierBillNo.Text + "', 2");
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
                        ObjUtil.GetDataPopup().CellClick += frmDiffPurchaseReceived_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += frmDiffPurchaseReceived_KeyDown;
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

        private void frmDiffPurchaseReceived_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.DataSource != null)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();
                LoadData();
            }
        }

        private void frmDiffPurchaseReceived_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSupplierBillNo.SelectionStart = txtSupplierBillNo.MaxLength;
                txtSupplierBillNo.Focus();
                LoadData();
            }
        }

        private void btnViewDetails_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnViewDetails_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void txtSupplierBillNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtSupplierBillNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void frmDiffPurchaseReceived_Load(object sender, EventArgs e)
        {
            btnViewDetails.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            txtSupplierBillNo.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dataGridView1.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmDiffPurchaseReceived, clsFormRights.Operation.View) || clsUtility.IsAdmin)
            {
                Purchase.frmDiffPurchaseReceviedDetails Obj = new frmDiffPurchaseReceviedDetails();
                Obj.PurchaseInvoiceID = txtPurchaseInvoiceID.Text.Length > 0 ? Convert.ToInt32(this.txtPurchaseInvoiceID.Text) : 0;
                Obj.Show();
            }
            else
            {
                clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
            }
        }
    }
}
