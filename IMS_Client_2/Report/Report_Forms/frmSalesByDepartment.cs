using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;
using Microsoft.Reporting.WinForms;

namespace IMS_Client_2.Report.Report_Forms
{
    public partial class frmSalesByDepartment : Form
    {
        public frmSalesByDepartment()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        int CategoryID = 0;
        private void frmEmployeeSales_Load(object sender, EventArgs e)
        {
            btnPrinterSave.BackgroundImage = B_Leave;
            btnCancel.BackgroundImage = B_Leave;

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            LoadDepartment();
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
            GenerateReport();
        }

        private void GenerateReport()
        {
            ObjDAL.SetStoreProcedureData("CategoryID", SqlDbType.Int, CategoryID);
            ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, dtpFromDate.Value.ToString("yyyy-MM-dd"));
            ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, dtpToDate.Value.ToString("yyyy-MM-dd"));
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_Get_Sales_Department_Report");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    reportViewer1.LocalReport.DataSources.Clear();

                    ReportParameter param1 = new ReportParameter("parmDateFilter", "Date Filter : " + dtpFromDate.Value.ToShortDateString() + " to " + dtpToDate.Value.ToShortDateString(), true);

                    // adding the parameter in the report dynamically
                    reportViewer1.LocalReport.SetParameters(param1);

                    ReportDataSource rds = new ReportDataSource("dsSalesDepartmentReport", dt);
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.Percent;
                    reportViewer1.ZoomPercent = 100;
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    clsUtility.ShowInfoMessage("No Datafound for the given filter.", clsUtility.strProjectTitle);
                    reportViewer1.LocalReport.DataSources.Clear();
                }
            }
        }

        private void LoadDepartment()
        {
            DataTable dt = null;
            dt = ObjDAL.GetDataCol(CoreApp.clsUtility.DBName + ".dbo.CategoryMaster", "CategoryID,CategoryName", "ISNULL(ActiveStatus,1)=1", "CategoryName");

            if (ObjUtil.ValidateTable(dt))
            {
                cmbDepartment.DataSource = dt;
                cmbDepartment.DisplayMember = "CategoryName";
                cmbDepartment.ValueMember = "CategoryID";

                cmbDepartment.SelectedIndex = -1;
            }
            else
            {
                cmbDepartment.DataSource = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CategoryID = 0;
            cmbDepartment.SelectedIndex = -1;
            reportViewer1.LocalReport.DataSources.Clear();
        }

        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedValue != null)
            {
                CategoryID = Convert.ToInt32(cmbDepartment.SelectedValue);
            }
        }
    }
}