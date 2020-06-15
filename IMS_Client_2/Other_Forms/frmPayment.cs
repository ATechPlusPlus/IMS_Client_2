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
        public static string strPaymentAutoID = "";

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        
        clsUtility ObjUtil = new clsUtility();

        private void button1_Click(object sender, EventArgs e)
        {
            strPaymentAutoID = txtPaymentAutoID.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtPaymentAutoID.Clear(); 
            strPaymentAutoID = "";
            this.Close();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
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
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled == true)
            {
                clsUtility.ShowInfoMessage("Enter Only Number...", clsUtility.strProjectTitle);
            }
        }
    }
}
