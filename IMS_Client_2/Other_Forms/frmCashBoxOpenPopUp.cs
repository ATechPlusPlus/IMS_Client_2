using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmCashBoxOpenPopUp : Form
    {
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        public frmCashBoxOpenPopUp()
        {
            InitializeComponent();
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
            this.Close();
        }

        private void frmCashBoxOpenPopUp_Load(object sender, EventArgs e)
        {
            btnPrint.BackgroundImage = B_Leave;
        }
    }
}
