using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;
using ExcelDataReader;
using System.IO;

namespace IMS_Client_2.Other_Forms
{
    public partial class Import_ProductData : Form
    {
        public Import_ProductData()
        {
            InitializeComponent();
        }

        clsUtility ObjUtil = new clsUtility();
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        OpenFileDialog Obj = new OpenFileDialog();


        string strFilePath;

        private void lkbBrowseForProductData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            strFilePath = "";
            Obj.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (Obj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                strFilePath = Obj.FileName;
                txtImportProductData.Text = strFilePath;
            }
        }

        public delegate void dShowProgressPercent(int cur, int Max);
        public delegate void dsetLable(string str);

        public void SetProgressPercent(int cur, int Max)
        {
            if (progressBar1.InvokeRequired)
            {
                dShowProgressPercent d = new dShowProgressPercent(SetProgressPercent);
                this.Invoke(d, new object[] { cur, Max });
            }
            else
            {
                progressBar1.Maximum = Max;
                progressBar1.Value = cur;
            }
        }

        public void SetLableText(string str)
        {
            if (progressBar1.InvokeRequired)
            {
                dsetLable d = new dsetLable(SetLableText);
                this.Invoke(d, new object[] { str });
            }
            else
            {
                label1.Text = str;
            }

        }

        private void lkbCancelForProductData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtImportProductData.Clear();
            return;
        }

        private void Insert_UpdateData(string ProductName, string CategoryID, string Rate, string ActiveStatus, string CreatedBy)
        {
            try
            {
                ObjDAL.SetColumnData("ProductName", SqlDbType.NVarChar, ProductName);
                ObjDAL.SetColumnData("CategoryID", SqlDbType.NVarChar, CategoryID);
                ObjDAL.SetColumnData("Rate", SqlDbType.NVarChar, Rate);
                ObjDAL.SetColumnData("ActiveStatus", SqlDbType.NVarChar, ActiveStatus);
                ObjDAL.SetColumnData("CreatedBy", SqlDbType.NVarChar, 0);
                int a = ObjDAL.InsertData("ProductMaster", true);
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
            }
        }

        private void btnImportProductMaster_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImportProductData.Text) || string.IsNullOrEmpty(txtImportProductData.Text))
            {
                clsUtility.ShowInfoMessage("Please Select Excel file", clsUtility.strProjectTitle);
            }
            else
            {
                FileStream stream = new FileStream(Obj.FileName, FileMode.Open);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader.AsDataSet();

                DataTable dtExcelData = result.Tables[0];

                for (int i = 1; i < dtExcelData.Rows.Count; i++)
                {

                    Insert_UpdateData(dtExcelData.Rows[i][0].ToString(),
                                    dtExcelData.Rows[i][1].ToString(),
                                    dtExcelData.Rows[i][2].ToString(),
                                    dtExcelData.Rows[i][3].ToString(),
                                    dtExcelData.Rows[i][4].ToString()
                                    );

                    SetProgressPercent(i, dtExcelData.Rows.Count);
                    SetLableText(i.ToString() + "/" + dtExcelData.Rows.Count.ToString());
                }

                excelReader.Close();
                stream.Close();

                SetProgressPercent(0, 100);
                SetLableText("Operation completed");
                MessageBox.Show("Data Imported.");
                this.Close();                

            }
        }



        private void tbnTruncate_Click(object sender, EventArgs e)
        {
            ObjDAL.ExecuteSelectStatement("truncate table dbo.ProductMaster");
            clsUtility.ShowInfoMessage("Table Deleted", clsUtility.strProjectTitle);
        }
    }
}
