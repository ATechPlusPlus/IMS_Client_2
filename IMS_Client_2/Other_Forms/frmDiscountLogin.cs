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
        public static string VoucherNo = string.Empty;

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private bool ValidateInputs()
        {
            if (ObjUtil.IsControlTextEmpty(txtDiscountPass))
            {
                clsUtility.ShowInfoMessage("Please Enter Admin's password to allow discount..");
                txtDiscountPass.Focus();
                return false;
            }
            if (ObjUtil.IsControlTextEmpty(txtVoucherNo))
            {
                clsUtility.ShowInfoMessage("Please Enter Voucher Number..");
                txtVoucherNo.Focus();
                return false;
            }
            if (txtVoucherNo.TextLength < 6)
            {
                clsUtility.ShowInfoMessage("Please Enter Valid Voucher Number..");
                txtVoucherNo.Focus();
                return false;
            }
            return true;
        }
        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                //DataTable dt = ObjDAL.GetDataCol(clsUtility.DBName + ".dbo.UserManagement", "UserID,UserName,Password,IsAdmin", "Password='" + ObjUtil.Encrypt(txtDiscountPass.Text, true) + "' AND ISNULL(Isblock,0)=0", "UserID DESC");
                ObjDAL.SetStoreProcedureData("Pass", SqlDbType.VarChar, ObjUtil.Encrypt(txtDiscountPass.Text, true), clsConnection_DAL.ParamType.Input);
                ObjDAL.SetStoreProcedureData("VoucherNo", SqlDbType.VarChar, txtVoucherNo.Text, clsConnection_DAL.ParamType.Input);
                DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_CheckDiscountPass_VoucherNo");
                if (ObjUtil.ValidateDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    if (ObjUtil.ValidateTable(dt))
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["IsAdmin"]) || clsFormRights.HasFormRight(clsFormRights.Forms.frmDiscountLogin, clsFormRights.Operation.Save))
                        {
                            if (Convert.ToInt32(dt.Rows[0]["VoucherCount"]) == 0)
                            {
                                IsValidAdmin = true;
                                VoucherNo = txtVoucherNo.Text;
                                this.Close();
                            }
                            else
                            {
                                IsValidAdmin = false;
                                clsUtility.ShowInfoMessage("Voucher Number "+txtVoucherNo.Text+" is already Used..");
                                txtVoucherNo.Focus();
                            }
                        }
                        else
                        {
                            IsValidAdmin = false;
                            clsUtility.ShowInfoMessage("User is not an Admin. Please enter admin password.");
                            txtDiscountPass.Focus();
                        }
                    }
                    else
                    {
                        IsValidAdmin = false;
                        clsUtility.ShowInfoMessage("Invalid password..");
                        txtDiscountPass.Focus();
                    }
                }
            }
        }

        private void frmDiscountLogin_Load(object sender, EventArgs e)
        {
            btnSaveData.BackgroundImage = B_Leave;
            txtDiscountPass.Focus();
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

        private void txtVoucherNo_Enter(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender);
        }

        private void txtVoucherNo_Leave(object sender, EventArgs e)
        {
            ObjUtil.SetTextHighlightColor(sender, System.Drawing.Color.White);
        }

        private void txtVoucherNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ObjUtil.IsAlphaNumeric(e);
            if (e.Handled)
            {
                clsUtility.ShowInfoMessage("Please Enter valid Voucher Number..");
                txtVoucherNo.Focus();
            }
        }
    }
}