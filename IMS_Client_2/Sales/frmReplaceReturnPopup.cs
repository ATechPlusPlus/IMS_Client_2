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
        public static bool IsReplaceInvoice = false;
        public static string strReplaceInvoiceNumber = "";
        public static string strInvoiceID = "0";
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
            btnClose.BackgroundImage = B_Leave;


            txtOldInvoiceNumber.Text = strReplaceInvoiceNumber;
            if (strReplaceInvoiceNumber=="NA")
            {
                checkBox1.Checked = true;

            }

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
        private void ShowReplaceWindow()
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
                
                sales_Invoice.Show();
                this.Close();
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DoReplaceInvoice();


        }
        private void DoReplaceInvoice()
        {
            if (checkBox1.Checked == false)
            {
                if (txtOldInvoiceNumber.Text.Trim().Length==0)
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
                    strReplaceInvoiceNumber = this.txtOldInvoiceNumber.Text;
                    IsReplaceInvoice = true;
                    strInvoiceID = dataTable.Rows[0]["ID"].ToString();
                    this.Close();
                }
            }
            else
            {
                strReplaceInvoiceNumber = "NA";
                IsReplaceInvoice = true;
                strInvoiceID = "0";
                this.Close();

            }
        }

     

        private void txtOldInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData==Keys.Enter)
            {
                DoReplaceInvoice();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //IsReplaceInvoice = false;
            //strReplaceInvoiceNumber = "";
            
            //txtOldInvoiceNumber.Clear();
            //txtOldInvoiceNumber.Enabled = false;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtOldInvoiceNumber.Clear();
                strReplaceInvoiceNumber = "NA";
                txtOldInvoiceNumber.Enabled = false;
            }
            else
            {
                strReplaceInvoiceNumber = "";
                txtOldInvoiceNumber.Enabled = true;
            }
        }
    }
}
