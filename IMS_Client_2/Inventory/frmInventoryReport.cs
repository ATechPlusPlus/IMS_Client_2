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

namespace IMS_Client_2.Inventory
{
    public partial class frmInventoryReport : Form
    {
        public frmInventoryReport()
        {
            InitializeComponent();
        }

        private void frmInventoryReport_Load(object sender, EventArgs e)
        {
            LoadReport();

        }
        public DataTable dtIventory { get; set; }

        public string StoreName { get; set; }
        public string ScanBy { get; set; }

        public int pMasterScanID { get; set; }

        public DateTime? CompareDDate { get; set; }
        CoreApp.clsConnection_DAL ObjDAL = new CoreApp.clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        public string _SysQTY { get; set; }
        public string _InventoryQTY { get; set; }
        public string _DiffQTY { get; set; }

        public string _SysRate { get; set; }
        public string _InvRate { get; set; }
        public string _DiffRate { get; set; }
        private void LoadReport()
        {

            ObjDAL.SetStoreProcedureData("MasterScanID", SqlDbType.Int, pMasterScanID, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_GenerateInventoryReport");
            if (ObjUtil.ValidateDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                if (ObjUtil.ValidateTable(dt))
                {
                    dtIventory = dt;

                }

            }
            else

                reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("dsInventory", dtIventory);

            // creating the parameter with the extact name as in the report.
            ReportParameter param1 = new ReportParameter("parmStoreName", StoreName, true);
            ReportParameter param2 = new ReportParameter("parmScanBy", ScanBy, true);
            ReportParameter param3 = null;
            if (CompareDDate != null)
            {
                param3 = new ReportParameter("parmCompareDate", CompareDDate.Value.Date.ToString(), true);
            }

            ReportParameter p1 = new ReportParameter("ParmSysQTY", _SysQTY, true);
            ReportParameter p2 = new ReportParameter("ParmInventoryQTY", _InventoryQTY, true);
            ReportParameter p3 = new ReportParameter("ParmDiffQTY", _DiffQTY, true);

            ReportParameter p4 = new ReportParameter("ParmSysRate", _SysRate, true);
            ReportParameter p5 = new ReportParameter("ParmInventoryRate", _InventoryQTY, true);
            ReportParameter p6 = new ReportParameter("ParmDiffRate", _DiffRate, true);


            // adding the parameter in the report dynamically
            reportViewer1.LocalReport.SetParameters(param1);
            reportViewer1.LocalReport.SetParameters(param2);

            if (CompareDDate != null)
            {
                reportViewer1.LocalReport.SetParameters(param3);
            }

            reportViewer1.LocalReport.SetParameters(p1);
            reportViewer1.LocalReport.SetParameters(p2);
            reportViewer1.LocalReport.SetParameters(p3);
            reportViewer1.LocalReport.SetParameters(p4);
            reportViewer1.LocalReport.SetParameters(p5);
            reportViewer1.LocalReport.SetParameters(p6);
            

            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 75;
            this.reportViewer1.RefreshReport();

            this.reportViewer1.RefreshReport();
        }
    }
}