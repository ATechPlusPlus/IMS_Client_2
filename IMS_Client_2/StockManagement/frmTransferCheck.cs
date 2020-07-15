using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;
namespace IMS_Client_2.StockManagement
{
    public partial class frmTransferCheck : Form
    {
        public frmTransferCheck()
        {
            InitializeComponent();
        }
      
        private void frmTransferCheck_Load(object sender, EventArgs e)
        {

        }
        public int StoreBillDetailsID = 0;
        public int fromShopID = 0;
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
           
          
        }
     

        CoreApp.clsUtility ObjUtil = new clsUtility();
      
    }
}
