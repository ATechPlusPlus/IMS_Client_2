using CoreApp;
using Microsoft.Reporting.WinForms;
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
    public partial class frmEmployeeSales : Form
    {
        public frmEmployeeSales()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        int EmployeeID = 0;

        private void frmEmployeeSales_Load(object sender, EventArgs e)
        {
            btnPrinterSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;
            BindEmployee();
            cmbEmployee.SelectedIndex = -1;

            dtFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }
        private void BindEmployee()
        {
            DataTable dt = ObjCon.ExecuteSelectStatement("SELECT Empid,[Name] FROM " + clsUtility.DBName + ".dbo.EmployeeDetails WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dt))
            {
                cmbEmployee.DataSource = dt;
                cmbEmployee.DisplayMember = "Name";
                cmbEmployee.ValueMember = "Empid";
            }
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
            if (cmbEmployee.SelectedIndex == -1)
            {
                EmployeeID = 0;
            }
            else
            {
                EmployeeID = (int)(cmbEmployee.SelectedValue);
            }
            reportViewer1.LocalReport.DataSources.Clear();

            ObjCon.SetStoreProcedureData("EmpID", SqlDbType.Int, EmployeeID);
            ObjCon.SetStoreProcedureData("FromDate", SqlDbType.Date, dtFromDate.Value.ToString("yyyy-MM-dd"));
            ObjCon.SetStoreProcedureData("ToDate", SqlDbType.Date, dtToDate.Value.ToString("yyyy-MM-dd"));
            ObjCon.SetStoreProcedureData("Admintraviteexp", SqlDbType.Decimal, Convert.ToDecimal(txtAdmin.Text));
            DataSet ds = ObjCon.ExecuteStoreProcedure_Get(CoreApp.clsUtility.DBName + ".dbo.SPR_Get_EmployeeWiseSales_Report");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dtEmployeeWiseSales = ds.Tables[0];
                ReportDataSource rds = new ReportDataSource("dsEmployeeSales", dtEmployeeWiseSales);

                string datefilter = dtFromDate.Value.ToShortDateString() + " To " + dtToDate.Value.ToShortDateString();
                ReportParameter param1 = new ReportParameter("parmFromDate", datefilter, true);

                ReportParameter param3 = new ReportParameter("parmAdminExp", txtAdmin.Text, true);

                // adding the parameter in the report dynamically
                reportViewer1.LocalReport.SetParameters(param1);

                reportViewer1.LocalReport.SetParameters(param3);

                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                this.reportViewer1.RefreshReport();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbEmployee.SelectedIndex = -1;
            txtAdmin.Text = "0";
            dtFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            reportViewer1.LocalReport.DataSources.Clear();
        }
    }
}