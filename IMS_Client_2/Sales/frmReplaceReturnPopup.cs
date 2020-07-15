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
    public partial class frmReplaceReturnPopup : Form
    {
        public frmReplaceReturnPopup()
        {
            InitializeComponent();
        }
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        private void frmReplaceReturnPopup_Load(object sender, EventArgs e)
        {
            btnOk.BackgroundImage = B_Leave;
        }
        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtOldInvoiceNumber.Text.Trim().Length == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Old Invoice Number.", clsUtility.strProjectTitle);
                return;
            }
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT ID FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails WITH(NOLOCK) WHERE InvoiceNumber='" + txtOldInvoiceNumber.Text + "'");
            if (dataTable.Rows.Count == 0)
            {
                clsUtility.ShowInfoMessage("No Invoice found for Invoice Number: " + txtOldInvoiceNumber.Text, clsUtility.strProjectTitle);
                return;
            }
            else
            {
                Sales.Sales_Invoice sales_Invoice = new Sales_Invoice();
                sales_Invoice.txtInvoiceNumber.Text = this.txtOldInvoiceNumber.Text;
                sales_Invoice.Text = "Replace/Return";
                sales_Invoice.lblActiveStatus.ForeColor = Color.Maroon;
                sales_Invoice.IsReplaceReturnMode = true;
                sales_Invoice.OldInvoiceID = dataTable.Rows[0]["ID"].ToString();
                sales_Invoice.Show();
                this.Close();
            }
        }
    }
}
