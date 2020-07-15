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

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public int TotalQTY = 0;
        private void frmQTYValidation_Load(object sender, EventArgs e)
        {

        }
        public static int QTYConfirmation = 0;
        int incLeft = 5;
        private void btnSaveData_Click(object sender, EventArgs e)
        {

            QTYConfirmation = 0;
            if (TotalQTY!=Convert.ToInt32(txtQTY.Text))
            {
                incLeft--;
                label3.Text = incLeft.ToString();
              CoreApp.clsUtility.ShowErrorMessage("Enter QTY doesn't match with current bill QTY",CoreApp.clsUtility.strProjectTitle);
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
    }
}
