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
    public partial class frmPayment : Form
    {
        public frmPayment()
        {
            InitializeComponent();
        }

        public static string KNET_Number { get; set; }

        public static string VisaNumber { get; set; }

        public static string MasterCarNumber { get; set; }


        public static decimal KNET_Amount { get; set; }
        public static decimal MasterCardAmount { get; set; }
        public static decimal VisaAmount { get; set; }

        public static decimal CashAmount { get; set; }

        public static List<string> lstPaymnetType = new List<string>();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        clsUtility ObjUtil = new clsUtility();

        private void LoadAmount()
        {
            if (lblPaymentMode.Text == "K Net")
            {
                txtAmount.Text = KNET_Amount.ToString();
                txtPaymentAutoID.Text = KNET_Number;
            }
            else if (lblPaymentMode.Text == "Visa")
            {
                txtAmount.Text = VisaAmount.ToString();
                txtPaymentAutoID.Text = VisaNumber;
            }
            else if (lblPaymentMode.Text == "Master Card")
            {
                txtAmount.Text = MasterCardAmount.ToString();
                txtPaymentAutoID.Text = MasterCarNumber;
            }
            else if (lblPaymentMode.Text == "Cash")
            {
                txtAmount.Text = CashAmount.ToString();
                txtPaymentAutoID.Text = "00000000";
                txtAmount.Focus();
            }
        }
        public static void ResetData()
        {
            CashAmount = 0;
            KNET_Amount = 0;
            VisaAmount = 0;
            MasterCardAmount = 0;

            lstPaymnetType.Clear();
            KNET_Number = "";
            VisaNumber = "";
            MasterCarNumber = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Trim().Length == 0 || txtPaymentAutoID.Text.Trim().Length == 0)
            {
                clsUtility.ShowInfoMessage("Please Enter Number and Amount.", clsUtility.strProjectTitle);
                return;
            }

            if (txtAmount.Text == "0" || txtAmount.Text.Trim().Length == 0)
            {
                txtPaymentAutoID.Clear();
                txtAmount.Text = "0";
                txtPaymentAutoID.Clear();

                if (lstPaymnetType.Contains(lblPaymentMode.Text))
                {
                    lstPaymnetType.Remove(lblPaymentMode.Text);
                }

                if (lblPaymentMode.Text == "K Net")
                {
                    KNET_Amount = 0;
                    KNET_Number = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Visa")
                {
                    VisaAmount = Convert.ToDecimal(txtAmount.Text);
                    VisaNumber = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Master Card")
                {
                    MasterCardAmount = Convert.ToDecimal(txtAmount.Text);
                    MasterCarNumber = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Cash")
                {
                    CashAmount = Convert.ToDecimal(txtAmount.Text);
                }
            }
            else
            {
                if (!lstPaymnetType.Contains(lblPaymentMode.Text))
                {
                    lstPaymnetType.Add(lblPaymentMode.Text);
                }

                if (lblPaymentMode.Text == "K Net")
                {
                    KNET_Amount = Convert.ToDecimal(txtAmount.Text);
                    KNET_Number = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Visa")
                {
                    VisaAmount = Convert.ToDecimal(txtAmount.Text);
                    VisaNumber = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Master Card")
                {
                    MasterCardAmount = Convert.ToDecimal(txtAmount.Text);
                    MasterCarNumber = txtPaymentAutoID.Text;
                }
                else if (lblPaymentMode.Text == "Cash")
                {
                    CashAmount = Convert.ToDecimal(txtAmount.Text);

                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtPaymentAutoID.Clear();
            txtAmount.Text = "0";
            this.Close();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            LoadAmount();

            this.Focus();

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

        private void txtPaymentAutoID_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtPaymentAutoID_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void txtPaymentAutoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = ObjUtil.IsNumeric(e);
            //if (e.Handled == true)
            //{
            //    clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
            //}
        }
        public static decimal GetTotalCreditAmount()
        {
            return Convert.ToDecimal(KNET_Amount + MasterCardAmount + VisaAmount);
        }
        public static decimal GetTotalCash()
        {
            return Convert.ToDecimal(CashAmount);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsDecimal(txtAmount, e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
            }
        }

        private void frmPayment_Activated(object sender, EventArgs e)
        {

            if (lblPaymentMode.Text == "K Net")
            {
               
            }
            else if (lblPaymentMode.Text == "Visa")
            {
               
            }
            else if (lblPaymentMode.Text == "Master Card")
            {
              
            }
            else if (lblPaymentMode.Text == "Cash")
            {

                txtAmount.Focus();
            }
        }
    }
}