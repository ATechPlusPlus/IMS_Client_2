using CoreApp;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.StockManagement
{
    public partial class frmBulkPriceUpdate : Form
    {
        public frmBulkPriceUpdate()
        {
            InitializeComponent();
        }
        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);

        FileStream stream;
        IExcelDataReader excelReader;
        DataTable dtExcelData;

        Image B_Leave = Properties.Resources.B_click;
        Image B_Enter = Properties.Resources.B_on;

        private void frmBulkPriceUpdate_Load(object sender, EventArgs e)
        {
            btnBrowse.BackgroundImage = B_Leave;
            btnUpdatePrice.BackgroundImage = B_Leave;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = excelReader.AsDataSet();
                    if (result != null && result.Tables.Count > 0)
                    {
                        dtExcelData = result.Tables[0];
                        if (ObjUtil.ValidateTable(dtExcelData))
                        {
                            dtExcelData.Rows.RemoveAt(0);
                            dtExcelData.AcceptChanges();

                            dtExcelData.Columns[0].ColumnName = "StyleNo";
                            dtExcelData.Columns[1].ColumnName = "SalePrice";
                            dtExcelData.Columns[2].ColumnName = "Brand";

                            dgvBulkPriceUpdate.DataSource = dtExcelData;
                        }
                    }
                    excelReader.Close();
                    //stream.Flush();
                    stream.Close();
                }
                catch (Exception ex)
                {
                    string temp = "LoginID: " + clsUtility.LoginID + " ";
                    ObjUtil.WriteToFile(temp + ex.ToString(), "Error");

                    clsUtility.ShowErrorMessage(ex.ToString());
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(Application.StartupPath + "//Template/BulkPriceUpdate.xlsx"))
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "//Template");
            }
            else
            {
                clsUtility.ShowInfoMessage("Excel Template not found at :" + Application.StartupPath + "//Template/BulkPriceUpdate.xlsx");
            }
        }

        private void dgvBulkPriceUpdate_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvBulkPriceUpdate);
            ObjUtil.SetDataGridProperty(dgvBulkPriceUpdate, DataGridViewAutoSizeColumnsMode.Fill);

            if (dgvBulkPriceUpdate.Rows.Count >= 0)
            {
                dgvBulkPriceUpdate.Columns[0].HeaderText = "Style No";
                dgvBulkPriceUpdate.Columns[1].HeaderText = "Sales Price";
                dgvBulkPriceUpdate.Columns[2].HeaderText = "Brand";
            }

        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsUtility.ShowQuestionMessage("Are you sure, you want to update all price data"))
                {
                    var dtExcelTable = dgvBulkPriceUpdate.DataSource as DataTable;

                    var spDataTable = ConvertToSpTable(dtExcelTable);
                    if (spDataTable != null)
                    {
                        ObjDAL.SetStoreProcedureData("ExcelTable", SqlDbType.Structured, spDataTable);
                        ObjDAL.SetStoreProcedureData("LoginBy", SqlDbType.Int, clsUtility.LoginID);
                        ObjDAL.SetStoreProcedureData("Flag", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Output);
                        bool result = ObjDAL.ExecuteStoreProcedure_DML(clsUtility.DBName + ".dbo.SPR_BulkPriceUpdate");
                        if (result)
                        {
                            DataTable dt = ObjDAL.GetOutputParmData();
                            if (ObjUtil.ValidateTable(dt))
                            {
                                int Flag = Convert.ToInt32(dt.Rows[0][1]);
                                if (Flag == 1)
                                    clsUtility.ShowInfoMessage("Sales price has been udpated.");
                                else
                                    clsUtility.ShowErrorMessage("Sales price has not been udpated due to Model No. or Brand is mismatching.");
                            }
                            else
                            {
                                clsUtility.ShowErrorMessage("Sales price has not been udpated.");
                            }
                        }
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Excel data not in correct format.");
                    }
                }
            }
            catch (Exception ex)
            {
                string temp = " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
        }

        private DataTable ConvertToSpTable(DataTable dt)
        {
            try
            {
                DataTable dtEx = new DataTable();
                dtEx.Columns.Add("StyleNo", typeof(string));
                dtEx.Columns.Add("SalePrice", typeof(decimal));
                dtEx.Columns.Add("Brand", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dRow = dtEx.NewRow();
                    dRow["StyleNo"] = dt.Rows[i][0].ToString();
                    dRow["SalePrice"] = Convert.ToDecimal(dt.Rows[i][1]);
                    dRow["Brand"] = dt.Rows[i][2].ToString();

                    dtEx.Rows.Add(dRow);
                }

                return dtEx;
            }
            catch (Exception ex)
            {
                string temp = " LoginID: " + clsUtility.LoginID + " ";
                ObjUtil.WriteToFile(temp + ex.ToString(), "Error");
            }
            return null;
        }

        private void btnBrowse_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnBrowse_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }
    }
}