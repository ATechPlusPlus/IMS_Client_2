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

                    ReportDataSource rds = new ReportDataSource("ds_SalesDepartmentReport", dt);

                    reportViewer1.LocalReport.DataSources.Add(rds);

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
                cmbDepartment.SelectedValue = "CategoryID";
                cmbDepartment.DisplayMember = "CategoryName";
                cmbDepartment.DataSource = dt;
            }
            else
            {
                cmbDepartment.DataSource = null;
            }
            cmbDepartment.SelectedIndex = -1;
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