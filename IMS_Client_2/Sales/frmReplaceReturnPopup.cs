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

                if (cmbInvoice.Text.Trim().Length!=0)
                {
                    txtOldInvoiceNumber.Text = cmbInvoice.Text;
                }

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

                txtMobileNumber.Clear();
                pnlSelectInvoice.Visible = false;
                cmbInvoice.DataSource = null;
                lblDate.Text = "NA";
                cmbInvoice.SelectedIndex = -1;

            }
            else
            {
                strReplaceInvoiceNumber = "";
                txtOldInvoiceNumber.Enabled = true;
            }
        }

        private void txtMobileNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtMobileNumber.Text.Trim().Length>0)
            {
                txtOldInvoiceNumber.Clear();
            }
        }

        private void txtOldInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtOldInvoiceNumber.Text.Trim().Length>0)
            {
                if (txtMobileNumber.Text.Length>0)
                {
                    txtMobileNumber.Clear();
                    pnlSelectInvoice.Visible = false;
                    cmbInvoice.DataSource = null;
                    lblDate.Text = "NA";
                    cmbInvoice.SelectedIndex = -1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtMobileNumber.Text.Trim().Length==0)
            {
                clsUtility.ShowInfoMessage("Please enter mobile number.");
                return;
            }
            string strQ1 = "select CustomerID from CustomerMaster where PhoneNo='" + txtMobileNumber.Text + "'";

            int CustID = ObjCon.ExecuteScalarInt(strQ1);
            if (CustID == 0)
            {
                clsUtility.ShowInfoMessage("Can not find the given mobile number in the system.");
                return;
            }
            else
            {

               DataTable dataTable= ObjCon.ExecuteSelectStatement("select InvoiceNumber, InvoiceDate from SalesInvoiceDetails where CustomerID="+CustID+" order by Id desc");
                if (dataTable.Rows.Count>0)
                {

                    cmbInvoice.DataSource = dataTable;
                    cmbInvoice.DisplayMember = "InvoiceNumber";
                    cmbInvoice.ValueMember = "InvoiceDate";

                    cmbInvoice.SelectedIndex = -1;
                }

                pnlSelectInvoice.Visible = true;


            }
        }

        private void cmbInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
          
        }

        private void cmbInvoice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbInvoice.SelectedValue != null && cmbInvoice.SelectedIndex!=-1)
            {
                lblDate.Text = cmbInvoice.SelectedValue.ToString();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbInvoice.SelectedIndex==-1)
            {
                clsUtility.ShowInfoMessage("Select a bill from the given list.", clsUtility.strProjectTitle);
                return;

            }
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT ID FROM " + clsUtility.DBName + ".dbo.SalesInvoiceDetails WITH(NOLOCK) WHERE InvoiceNumber='" + cmbInvoice.Text + "'");
            if (dataTable.Rows.Count == 0)
            {
                clsUtility.ShowInfoMessage("No Invoice found for Invoice Number: " + txtOldInvoiceNumber.Text, clsUtility.strProjectTitle);
                return;
            }
            else
            {
              
             

                string InvoiceID = dataTable.Rows[0]["ID"].ToString();
                Report.frmSalesInvoiceReport frmSalesInvoice = new Report.frmSalesInvoiceReport();
                frmSalesInvoice.InvoiceID = Convert.ToInt32(InvoiceID);
                frmSalesInvoice.IsDirectPrint = false;
                frmSalesInvoice.ShowDialog();
            }

         
        }
    }
}
