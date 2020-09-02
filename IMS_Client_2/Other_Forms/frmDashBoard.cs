using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Other_Forms
{
    public partial class frmDashBoard : Form
    {
        public frmDashBoard()
        {
            InitializeComponent();
        }

        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        decimal TotalHeaderRate, TotalHeaderQTY;

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            DataTable dtDashBoard = ObjDAL.ExecuteSelectStatement("SELECT COUNT(1) FROM " + clsUtility.DBName + ".[dbo].[tblDashBoard] WITH(NOLOCK)");
            if (dtDashBoard.Rows.Count == 0)
            {
                if (clsFormRights.HasFormRight(clsFormRights.Forms.frmDashBoardSettings) || clsUtility.IsAdmin)
                {
                    frmDashBoardSettings frmDashBoardSettings = new frmDashBoardSettings();
                    frmDashBoardSettings.ShowDialog();
                    LoadShop();
                }
                else
                {
                    clsUtility.ShowInfoMessage("You have no rights to perform this task", clsUtility.strProjectTitle);
                }
            }
            else
            {
                LoadShop();
                ShowDashBoardDate();
            }
        }

        private string GetShopeName(string shopID)
        {
            string strShope = "NA";
            DataTable dt = ObjDAL.ExecuteSelectStatement("SELECT StoreName FROM " + clsUtility.DBName + ".[dbo].[StoreMaster] WITH(NOLOCK) WHERE StoreID=" + shopID);
            if (ObjUtil.ValidateTable(dt))
            {
                strShope = dt.Rows[0]["StoreName"].ToString();
            }
            return strShope;
        }

        private void LoadShop()
        {
            TotalHeaderRate = 0;
            TotalHeaderQTY = 0;

            flowLayoutPanel1.Controls.Clear();
            DataTable dtDashBoard = ObjDAL.ExecuteSelectStatement("SELECT * FROM " + clsUtility.DBName + ".[dbo].[tblDashBoard] WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dtDashBoard))
            {
                string[] strShops = dtDashBoard.Rows[0]["Shopes"].ToString().Split(',');
                int index = Convert.ToInt32(dtDashBoard.Rows[0]["Specification"]);

                DateTime fromDate = Convert.ToDateTime(dtDashBoard.Rows[0]["FromDate"]);
                DateTime toDate = Convert.ToDateTime(dtDashBoard.Rows[0]["ToDate"]);
                int TimerInterval = Convert.ToInt32(dtDashBoard.Rows[0]["RefreshRate"]) * 1000;

                timer1.Interval = TimerInterval;
                timer1.Enabled = true;
                for (int i = 0; i < strShops.Length; i++)
                {
                    string shopID = strShops[i];
                    //string TotalQTY_Rate = "";
                    //string reportQuery = "";

                    string strShopeName = GetShopeName(shopID);

                    #region Commented Inline SQL Query
                    //if (index == 0) //Employee ( Sales Man)
                    //{
                    //    reportQuery = " SELECT v1.Name as SalesMan  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                    " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                     " ON v1.id = v2.InvoiceID " +
                    //                     " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                     " AND v2.Rate>0 Group by Name";


                    //    TotalQTY_Rate = "select SUM(QTY) as TotalQTY, SUM(Rate) as TotalRate  from ( SELECT v1.Name as SalesMan  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                    " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                     " ON v1.id = v2.InvoiceID " +
                    //                     " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                     "  AND  v2.Rate>0 Group by Name ) as tb";

                    //}
                    //else if (index == 1) //Color
                    //{
                    //    reportQuery = " SELECT  v2.ColorName as Color  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                   " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                    " ON v1.id = v2.InvoiceID " +
                    //                    " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                    "  AND v2.Rate>0 Group by ColorName";

                    //    TotalQTY_Rate = " select SUM(QTY) as TotalQTY,SUM(Rate) as TotalRate from ( SELECT  v2.ColorName as Color  , SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                   " ON v1.id = v2.InvoiceID " +
                    //                   " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                   " AND v2.Rate>0 Group by ColorName ) as tb";

                    //}
                    //else if (index == 2) //Product 
                    //{
                    //    reportQuery = " SELECT  v2.ProductName as ItemName, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                   " ON v1.id = v2.InvoiceID " +
                    //                   " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                   " AND v2.Rate>0 Group by  v2.ProductName";


                    //    TotalQTY_Rate = " select SUM(QTY) as TotalQTY,SUM(Rate) as TotalRate from ( SELECT v2.ProductName as ItemName, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                 " ON v1.id = v2.InvoiceID " +
                    //                 " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                 "  AND v2.Rate>0 Group by  v2.ProductName ) as tb";

                    //}
                    //else if (index == 3) //Invoice/Bill No
                    //{
                    //    reportQuery = " SELECT  v1.InvoiceNumber as InvoiceNo, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                   " ON v1.id = v2.InvoiceID " +
                    //                   " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                   "AND v2.Rate>0  Group by  v1.InvoiceNumber";


                    //    TotalQTY_Rate = "select SUM(QTY) as TotalQTY,SUM(Rate) as TotalRate from ( SELECT  v1.InvoiceNumber as InvoiceNo, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //            " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //             " ON v1.id = v2.InvoiceID " +
                    //             " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //             " AND v2.Rate>0 Group by  v1.InvoiceNumber ) as tb";

                    //}
                    //else if (index == 4) // Category Department
                    //{
                    //    reportQuery = " SELECT   (select CategoryName from CategoryMaster where CategoryID=v2.CategoryID )  as Category, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                   " ON v1.id = v2.InvoiceID " +
                    //                   " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                   "AND v2.Rate>0  Group by  v2.CategoryID";

                    //    TotalQTY_Rate = "  select SUM(QTY) as TotalQTY,SUM(Rate) as TotalRate from (  SELECT   (select CategoryName from CategoryMaster where CategoryID=v2.CategoryID )  as Category, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                                         " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                                          " ON v1.id = v2.InvoiceID " +
                    //                                          " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                                          "  AND v2.Rate>0  Group by  v2.CategoryID ) as tb";


                    //}
                    //else if (index == 5) // Model No
                    //{
                    //    reportQuery = " SELECT  v2.ModelNo, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                  " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                   " ON v1.id = v2.InvoiceID " +
                    //                   " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                   "AND v2.Rate>0 Group by  v2.ModelNo";

                    //    TotalQTY_Rate = "   select SUM(QTY) as TotalQTY,SUM(Rate) as TotalRate from (  SELECT   v2.ModelNo, SUM(v2.QTY) as QTY, SUM(v2.Rate) as Rate  FROM  " +
                    //                                         " " + clsUtility.DBName + ".dbo.View_SalesBillDetails v1 JOIN " + clsUtility.DBName + ".dbo.View_SalesDetails v2 " +
                    //                                          " ON v1.id = v2.InvoiceID " +
                    //                                          " where v1.ShopeID = " + shopID + " AND v1.InvoiceDate between '" + fromDate.ToString("yyyy-MM-dd") + "' AND '" + toDate.ToString("yyyy-MM-dd") + "' " +
                    //                                          " AND v2.Rate>0 Group by  v2.ModelNo ) as tb";


                    //}
                    #endregion

                    string TotalQTYValue = "0";
                    string TotalRateValue = "0";
                    string TotalDiscountValue = "0"; string TotalGrandTotalValue = "0";

                    ObjDAL.SetStoreProcedureData("index", SqlDbType.Int, index, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, shopID, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("FromDate", SqlDbType.Date, fromDate, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("ToDate", SqlDbType.Date, toDate, clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("ReportType", SqlDbType.VarChar, "Dash", clsConnection_DAL.ParamType.Input);
                    ObjDAL.SetStoreProcedureData("GenericColumn", SqlDbType.VarChar, "", clsConnection_DAL.ParamType.Output);//Default value

                    DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.SPR_GetDashBoard_MiniSales_Report");
                    if (ObjUtil.ValidateDataSet(ds))
                    {
                        DataTable dtQTY_Rate = ds.Tables[1];
                        //DataTable dtQTY_Rate = ObjDAL.ExecuteSelectStatement(TotalQTY_Rate);
                        if (ObjUtil.ValidateTable(dtQTY_Rate))
                        {
                            TotalQTYValue = dtQTY_Rate.Rows[0]["TotalQTY"].ToString() == "" ? "0" : dtQTY_Rate.Rows[0]["TotalQTY"].ToString();
                            TotalRateValue = dtQTY_Rate.Rows[0]["TotalRate"].ToString() == "" ? "0" : dtQTY_Rate.Rows[0]["TotalRate"].ToString();
                            TotalDiscountValue = dtQTY_Rate.Rows[0]["TotalDiscount"].ToString() == "" ? "0" : dtQTY_Rate.Rows[0]["TotalDiscount"].ToString();
                            TotalGrandTotalValue = dtQTY_Rate.Rows[0]["GrandTotal"].ToString() == "" ? "0" : dtQTY_Rate.Rows[0]["GrandTotal"].ToString();

                            //if (TotalQTYValue.Trim().Length == 0)
                            //{
                            //    TotalQTYValue = "0";
                            //}
                            //if (TotalRateValue.Trim().Length == 0)
                            //{
                            //    TotalRateValue = "0";
                            //}
                        }
                    }
                    ObjDAL.ResetData();

                    SPanel sPanel = new SPanel();
                    sPanel.Size = new Size(271, 364);
                    sPanel.BackColor = Color.FromArgb(33, 140, 116);

                    Label lblShopeName = new Label();
                    lblShopeName.Location = label4.Location;
                    lblShopeName.Font = label4.Font;
                    lblShopeName.ForeColor = label4.ForeColor;
                    lblShopeName.Text = strShopeName;
                    lblShopeName.Size = new Size(257, 22);
                    lblShopeName.AutoSize = false;
                    lblShopeName.TextAlign = ContentAlignment.MiddleCenter;
                    sPanel.Controls.Add(lblShopeName);


                    Label lblTotalQTY = new Label();
                    lblTotalQTY.Text = "Total QTY :";
                    lblTotalQTY.Font = label5.Font;
                    lblTotalQTY.ForeColor = label5.ForeColor;
                    lblTotalQTY.Location = label5.Location;
                    sPanel.Controls.Add(lblTotalQTY);

                    Label QTYvalue = new Label();
                    QTYvalue.Text = TotalQTYValue;
                    QTYvalue.Font = label7.Font;
                    QTYvalue.ForeColor = label7.ForeColor;
                    QTYvalue.Location = label7.Location;
                    sPanel.Controls.Add(QTYvalue);


                    Label lblTotalRate = new Label();
                    lblTotalRate.Text = "Total Rate :";
                    lblTotalRate.Font = label6.Font;
                    lblTotalRate.ForeColor = label6.ForeColor;
                    lblTotalRate.Location = label6.Location;
                    sPanel.Controls.Add(lblTotalRate);


                    Label lblTotalRateValue = new Label();
                    lblTotalRateValue.Text = TotalRateValue;
                    lblTotalRateValue.Font = label8.Font;
                    lblTotalRateValue.ForeColor = label8.ForeColor;
                    lblTotalRateValue.Location = label8.Location;
                    sPanel.Controls.Add(lblTotalRateValue);


                    Label lblDiscount1 = new Label();
                    lblDiscount1.Text = "Total Discount :";
                    lblDiscount1.Font = new Font("Times new Roman", 11.2F, FontStyle.Bold);
                    lblDiscount1.AutoSize = false;
                    lblDiscount1.Size = lblDiscount.Size;
                    lblDiscount1.ForeColor = lblDiscount.ForeColor;
                    lblDiscount1.Location = lblDiscount.Location;
                    sPanel.Controls.Add(lblDiscount1);

                    Label lblDiscountValue1 = new Label();
                    lblDiscountValue1.Text = TotalDiscountValue;
                    lblDiscountValue1.Font = lblDiscountValue.Font;
                    lblDiscountValue1.ForeColor = lblDiscountValue.ForeColor;
                    lblDiscountValue1.Location = lblDiscountValue.Location;
                    sPanel.Controls.Add(lblDiscountValue1);


                    Label lblTotalAmt1 = new Label();
                    lblTotalAmt1.Text = "Total Grand Amt :";
                    lblTotalAmt1.Size = lblTotalAmt.Size;
                    lblTotalAmt1.Font = lblTotalAmt.Font;
                    lblTotalAmt1.ForeColor = lblTotalAmt.ForeColor;
                    lblTotalAmt1.Location = lblTotalAmt.Location;
                    sPanel.Controls.Add(lblTotalAmt1);

                    Label lblTotalAmtValue = new Label();
                    lblTotalAmtValue.Text = TotalGrandTotalValue;
                    lblTotalAmtValue.Font = lblGrandTotalAmtValue.Font;
                    lblTotalAmtValue.ForeColor = lblGrandTotalAmtValue.ForeColor;
                    lblTotalAmtValue.Location = lblGrandTotalAmtValue.Location;
                    sPanel.Controls.Add(lblTotalAmtValue);


                    DataGridView dgv = new DataGridView();
                    //dgv.DataSource = ObjDAL.ExecuteSelectStatement(reportQuery);
                    dgv.DataSource = ds.Tables[0];
                    dgv.DataBindingComplete += Dgv_DataBindingComplete;
                    dgv.Location = dataGridView1.Location;
                    dgv.Size = dataGridView1.Size;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.ColumnHeadersHeight = dgv.ColumnHeadersHeight + 2;
                    sPanel.Controls.Add(dgv);

                    flowLayoutPanel1.Controls.Add(sPanel);

                    //TotalHeaderRate = TotalHeaderRate + Convert.ToDecimal(TotalRateValue); 
                    TotalHeaderRate = TotalHeaderRate + Convert.ToDecimal(TotalGrandTotalValue);
                    TotalHeaderQTY = TotalHeaderQTY + Convert.ToDecimal(TotalQTYValue);
                }
                lblHeaderQTY.Text = TotalHeaderQTY.ToString();
                lblHeaderTotalRate.Text = TotalHeaderRate.ToString();
            }
            else
            {
                frmDashBoardSettings frmDashBoardSettings = new frmDashBoardSettings();
                frmDashBoardSettings.ShowDialog();
                this.Close();
            }
        }
        private void ShowDashBoardDate()
        {
            DataTable dtdate = ObjDAL.ExecuteSelectStatement("SELECT FromDate,ToDate FROM tblDashBoard WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dtdate))
            {
                lblDate.Text = Convert.ToDateTime(dtdate.Rows[0]["FromDate"]).ToShortDateString() + " to " + Convert.ToDateTime(dtdate.Rows[0]["ToDate"]).ToShortDateString();
            }
        }
        private void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                ObjUtil.SetDataGridProperty(dgv, DataGridViewAutoSizeColumnsMode.Fill);
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToAddRows = false;
                dgv.Columns[0].ReadOnly = true;
                dgv.Columns[1].ReadOnly = true;
                dgv.Columns[2].ReadOnly = true;

                dgv.DefaultCellStyle.SelectionBackColor = Color.White;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(dgv.Rows[i].Cells["Rate"].Value) < 0)
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Black;

                        dgv.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadShop();
        }

        private void frmDashBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmDashBoardSettings settings = new frmDashBoardSettings();
            settings.ShowDialog();

            LoadShop();
            ShowDashBoardDate();
        }
    }
}