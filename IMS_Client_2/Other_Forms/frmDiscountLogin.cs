using CoreApp;
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
    public partial class frmDiscountLogin : Form
    {
        public frmDiscountLogin()
        {
            InitializeComponent();
        }
        public static bool IsValidAdmin = false;
        clsUtility objUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.UserManagement", "UserID,UserName,Password,IsAdmin", "Password='" + objUtil.Encrypt(txtDiscountPass.Text, true) + "' and ISNULL(Isblock,0)=0", "UserID DESC");
            //int a = ObjDAL.ExecuteScalarInt("select Count(*) From CyberCafeManagement.dbo.login where UserName='" + txtUserName.Text.Trim() + "' and Password='" + txtPassword.Text.Trim() + "'");                 
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsAdmin"]))
                {
                    IsValidAdmin = true;
                    this.Close();
                }
                else
                {
                    IsValidAdmin = false;
                    clsUtility.ShowInfoMessage("User is not an Admin. Please enter admin password.", clsUtility.strProjectTitle);
                }
              
                
            }
            else
            {
                IsValidAdmin = false;
                clsUtility.ShowInfoMessage("Invalid password..", clsUtility.strProjectTitle);
            }
        }
    }
}
