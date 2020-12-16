using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2
{
    public partial class SplashWindow : Form
    {
        public SplashWindow()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL;
        clsUtility ObjUtil = new clsUtility();

        String path1 = String.Empty;
        bool IsPath = false;
        byte i = 0; //for closing Registrationprocess if system datetime is changes

        private void SplashWindow_Load(object sender, EventArgs e)
        {
            lblversion.Text = lblversion.Text + " " + Application.ProductVersion;
            clsUtility.strProjectTitle = "Inventory Management System";

            if (System.IO.Directory.Exists("AppConfig") && System.IO.File.Exists("AppConfig\\ServerConfig.sc"))
            {
                ObjDAL = new clsConnection_DAL(true);
                clsUtility.DBName = ObjDAL.GetCurrentDBName(true);
                AddStatusDate();// for adding current datetime if current datetime is greater than previous statusdate's datetime
                IsPath = true;
            }
            else
            {
                lblStatus.Text = lblStatus.Text + " UnRegistered Version";
            }
            timer1.Enabled = true;
            timer1.Start();
        }

        private void AddStatusDate()
        {
            DataTable dt = ObjDAL.GetData(clsUtility.DBName + ".dbo.RegistrationDetails", "PcName='" + Environment.MachineName + "'", "RegistrationID");
            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime status = Convert.ToDateTime(ObjUtil.Decrypt(dt.Rows[0]["StatusDate"].ToString(), true));
                DateTime ExpDate = Convert.ToDateTime(ObjUtil.Decrypt(dt.Rows[0]["ExpiryDate"].ToString(), true));

                //if (dt.Rows[0]["StatusDate"] != DBNull.Value && Convert.ToDateTime(dt.Rows[0]["StatusDate"]) <= DateTime.Now && Convert.ToDateTime(dt.Rows[0]["ExpiryDate"]) >= DateTime.Now)
                if (dt.Rows[0]["StatusDate"] != DBNull.Value && status <= ExpDate)
                {
                    if (status <= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt")))
                    {
                        ObjDAL.UpdateColumnData("StatusDate", SqlDbType.VarChar, ObjUtil.Encrypt(DateTime.Now.ToString(), true));
                        ObjDAL.UpdateData(clsUtility.DBName + ".dbo.RegistrationDetails", "PcName='" + Environment.MachineName + "'");
                    }
                }
                if (dt.Rows[0]["SoftKey"] == DBNull.Value)
                {
                    lblStatus.Text = lblStatus.Text + " UnRegistered Version";
                }
                else
                {
                    lblStatus.Text = lblStatus.Text + " Registered Version";
                }
            }
            else
            {
                lblStatus.Text = lblStatus.Text + " UnRegistered Version";
            }
        }

        private bool CheckReg()
        {
            DataTable dt = ObjDAL.GetData(clsUtility.DBName + ".dbo.RegistrationDetails", "PcName='" + Environment.MachineName + "'", "RegistrationID");
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsKeyEnter"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["IsKeyEnter"]) == false)
                {
                    return false;
                }
                DateTime status = Convert.ToDateTime(ObjUtil.Decrypt(dt.Rows[0]["StatusDate"].ToString(), true));
                DateTime ExpDate = Convert.ToDateTime(ObjUtil.Decrypt(dt.Rows[0]["ExpiryDate"].ToString(), true));
                if (status <= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt")))  //checking wheather user is changed datetime or not
                {
                    if (dt.Rows[0]["SoftKey"] == DBNull.Value)
                    {
                        return false;
                    }
                    else if (ExpDate > status) //checking wheather sotware is expire or not
                    {
                        return true;
                    }
                    else //if software is expired than this condition will execute
                    {
                        timer1.Stop();
                        i = 1;
                        clsUtility.ShowInfoMessage("Trail Version is Over \nIf you want to buy this Product Contact to Admin", clsUtility.strProjectTitle);
                        Application.Exit();
                        return false;
                    }
                }
                else if (status > Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd h:mm:ss tt")))//if user has changed datetime than this condition will execute
                {
                    timer1.Stop();
                    i = 1;
                    clsUtility.ShowInfoMessage("Change the your System date & Time\n for accessing this Software", clsUtility.strProjectTitle);
                    Application.Exit();
                    return false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["IsTrail"]) == false && ExpDate > status)
                {
                    return true;
                }
                return true;
            }
            else //first time this condition will execute
            {
                ObjDAL.SetColumnData("PcName", SqlDbType.NVarChar, Environment.MachineName);
                ObjDAL.SetColumnData("StatusDate", SqlDbType.VarChar, ObjUtil.Encrypt(DateTime.Now.ToString("yyyy/MM/dd"), true));
                ObjDAL.SetColumnData("IsTrail", SqlDbType.Bit, 1);
                ObjDAL.SetColumnData("RegDate", SqlDbType.Date, DateTime.Now);
                ObjDAL.SetColumnData("ExpiryDate", SqlDbType.VarChar, ObjUtil.Encrypt(DateTime.Now.AddDays(7).ToString("yyyy/MM/dd"), true));
                ObjDAL.SetColumnData("IsKeyEnter", SqlDbType.Bit, 0);

                if (ObjDAL.InsertData(clsUtility.DBName + ".dbo.RegistrationDetails", true) > 0)
                {
                    return false;
                }
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval == 4000)
            {
                try
                {
                    if (!IsPath)
                    {
                        timer1.Stop();
                        RegistrationProcess.frmServerConnect Obj = new RegistrationProcess.frmServerConnect();
                        Obj.ClientDBName = "IMS_Client_2";
                        //Obj.exeName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                        this.Hide();
                        Obj.BringToFront();
                        Obj.Show();
                    }
                    else if (CheckReg())
                    {
                        timer1.Stop();

                        frmLogin Obj = new frmLogin();
                        this.Hide();
                        Obj.BringToFront();
                        Obj.Show();
                        //login window
                    }
                    else
                    {
                        timer1.Stop();
                        if (i == 0)
                        {
                            RegistrationProcess.frmCustomerRegister reg = new RegistrationProcess.frmCustomerRegister();
                            //reg.exeName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                            this.Hide();
                            reg.BringToFront();
                            reg.Show();
                        }
                    }
                }
                catch
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                    this.Close();
                }
            }
        }
    }
}