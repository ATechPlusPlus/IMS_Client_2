using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Report.Report_Forms
{
    public partial class frmSalesByDepartment : Form
    {
        public frmSalesByDepartment()
        {
            InitializeComponent();
        }
        CoreApp.clsConnection_DAL ObjCon = new CoreApp.clsConnection_DAL(true);
        CoreApp.clsUtility ObjUtil = new CoreApp.clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void frmEmployeeSales_Load(object sender, EventArgs e)
        {
            btnPrinterSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            this.reportViewer1.RefreshReport();
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

        private void btnPrinterSave_Click(object sender, EventArgs e)
        {

        }
    }
}