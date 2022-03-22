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

        private void frmBulkPriceUpdate_Load(object sender, EventArgs e)
        {
            btnBrowse.BackgroundImage = Properties.Resources.B_on;
            btnUpdatePrice.BackgroundImage = Properties.Resources.B_on;
        }
        clsUtility ObjUtil = new clsUtility();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;

                try
                {
                    stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = excelReader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        dtExcelData = result.Tables[0];
                        if (ObjUtil.ValidateTable(dtExcelData))
                        {
                            dtExcelData.Rows.RemoveAt(0);
                            dtExcelData.AcceptChanges();

                            dtExcelData.Columns[0].ColumnName = "Style No";
                            dtExcelData.Columns[1].ColumnName = "Sale Price";
                            dtExcelData.Columns[2].ColumnName = "Brand";

                            dgvBulkPriceUpdate.DataSource = dtExcelData;
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }
        FileStream stream;
        IExcelDataReader excelReader;
        DataTable dtExcelData;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(Application.StartupPath + "//Template/BulkPriceUpdate.xlsx"))
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "//Template");
            }
          
        }

        private void dgvBulkPriceUpdate_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvBulkPriceUpdate);
            ObjUtil.SetDataGridProperty(dgvBulkPriceUpdate, DataGridViewAutoSizeColumnsMode.Fill);
          
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            if(clsUtility.ShowQuestionMessage("Are you sure, you want to update all price data"))
            {


            }
        }
    }
}
