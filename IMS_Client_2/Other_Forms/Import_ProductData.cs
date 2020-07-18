using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using ExcelDataReader;
using System.IO;
using System.Windows.Forms;
using CoreApp;

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

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        public delegate void dShowProgressPercent(int cur, int Max);
        public delegate void dsetLable(string str);

        FileStream stream;
        IExcelDataReader excelReader;
        DataTable dtExcelData;
        private void lkbBrowseForProductData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            strFilePath = "";
            Obj.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (Obj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                strFilePath = Obj.FileName;
                txtImportProductData.Text = strFilePath;
                try
                {
                    SetProgressPercent(1, dtExcelData.Rows.Count);
                    stream = new FileStream(Obj.FileName, FileMode.Open);
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = excelReader.AsDataSet();
                    if (result.Tables.Count > 0)
                    {
                        dtExcelData = result.Tables[0];
                        if (ObjUtil.ValidateTable(dtExcelData))
                        {
                            dataGridView1.DataSource = dtExcelData;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
                }
            }
        }

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
            excelReader.Close();
            stream.Close();
            return;
        }

        //private void Insert_UpdateData(string ProductName, string CategoryID, string Rate, string ActiveStatus, string CreatedBy)
        private void Insert_UpdateData(string ProductName, string CategoryID,string ProductArabicName)
        {
            try
            {
                ObjDAL.SetColumnData("ProductName", SqlDbType.NVarChar, ProductName);
                ObjDAL.SetColumnData("CategoryID", SqlDbType.Int, CategoryID);
                ObjDAL.SetColumnData("ProductArabicName", SqlDbType.NVarChar, ProductArabicName);
                //ObjDAL.SetColumnData("Rate", SqlDbType.NVarChar, Rate);
                //ObjDAL.SetColumnData("ActiveStatus", SqlDbType.NVarChar, ActiveStatus);
                //ObjDAL.SetColumnData("CreatedBy", SqlDbType.NVarChar, 0);
                int a = ObjDAL.InsertData(clsUtility.DBName + ".dbo.ProductMaster", true);
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
                try
                {
                    if (ObjUtil.ValidateTable(dtExcelData))
                    {
                        for (int i = 1; i < dtExcelData.Rows.Count; i++)
                        {

                            Insert_UpdateData(dtExcelData.Rows[i][0].ToString(),
                                             dtExcelData.Rows[i][1].ToString(),
                                             dtExcelData.Rows[i][2].ToString()
                                            );

                            SetProgressPercent(i, dtExcelData.Rows.Count);
                            SetLableText(i.ToString() + "/" + dtExcelData.Rows.Count.ToString());
                        }
                        excelReader.Close();
                        stream.Close();
                        
                        SetProgressPercent(0, 100);
                        SetLableText("Operation completed");
                        clsUtility.ShowInfoMessage("Data Imported.", clsUtility.strProjectTitle);
                        this.Close();
                    }
                    else
                    {
                        clsUtility.ShowInfoMessage("Please Select Valid Items file.", clsUtility.strProjectTitle);
                    }
                }
                catch (Exception ex)
                {
                    clsUtility.ShowErrorMessage(ex.ToString(), clsUtility.strProjectTitle);
                }
            }
        }

        private void tbnTruncate_Click(object sender, EventArgs e)
        {
            ObjDAL.ExecuteSelectStatement("truncate table dbo.ProductMaster");
            clsUtility.ShowInfoMessage("Table Deleted", clsUtility.strProjectTitle);
        }

        private void btnImportProductMaster_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Enter;
        }

        private void btnImportProductMaster_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackgroundImage = B_Leave;
        }

        private void Import_ProductData_Load(object sender, EventArgs e)
        {
            btnImportProductMaster.BackgroundImage = B_Leave;
        }
    }
}
