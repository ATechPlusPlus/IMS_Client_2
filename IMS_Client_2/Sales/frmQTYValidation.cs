using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Sales
{
    public partial class frmQTYValidation : Form
    {
        public frmQTYValidation()
        {
            InitializeComponent();
        }

        CoreApp.clsUtility ObjUtil = new CoreApp.clsUtility();

        public int TotalQTY = 0;
        public static int QTYConfirmation = 0;
        int incLeft = 5;

        
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmQTYValidation_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            btnClose.BackgroundImage = B_Leave;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            QTYConfirmation = 0;
            if (TotalQTY != Convert.ToInt32(txtQTY.Text))
            {
                incLeft--;
                label3.Text = incLeft.ToString();
                CoreApp.clsUtility.ShowErrorMessage("Enter QTY doesn't match with current bill QTY", CoreApp.clsUtility.strProjectTitle);
                txtQTY.Focus();
                if (incLeft == 0)
                {
                    QTYConfirmation = -1;
                    this.Close();
                }
            }
            else
            {
                QTYConfirmation = 1;
                this.Close();
            }
        }

        private void btnSaveData_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnSaveData_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void txtQTY_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtQTY_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, Color.White);
        }

        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsNumeric(e);
            if (e.Handled == true)
            {
                CoreApp.clsUtility.ShowInfoMessage("Enter Only Numbers...", CoreApp.clsUtility.strProjectTitle);
            }
        }
    }
}