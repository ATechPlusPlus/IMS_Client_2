using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreApp;
using ZXing;

namespace IMS_Client_2.Barcode
{
    public partial class frmBarCode : Form
    {
        public frmBarCode()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjDAL = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        //Form1 obj;

        string strCompanyName = "";
        string CurrentPurchaseInvoiceID = "";
        DataGridViewRow _PrintRowData;
        string _Current_BarCodeNumber = "";

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmBarCode_Load(object sender, EventArgs e)
        {
            btnPrintManualBarcode.BackgroundImage = B_Leave;
            btnPrintBarcode.BackgroundImage = B_Leave;
            btnSearch.BackgroundImage = B_Leave;
            btnResetPrint.BackgroundImage = B_Leave;

            strCompanyName = GetCompanyName();

            txtPurchaseInvoice.Focus();
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

        private void dgvProductDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dgvProductDetails);
            ObjUtil.SetDataGridProperty(dgvProductDetails, DataGridViewAutoSizeColumnsMode.Fill);

            for (int i = 0; i < dgvProductDetails.Columns.Count; i++)
            {
                if (i == 0)
                {
                    dgvProductDetails.Columns[i].ReadOnly = false;
                    continue;
                }
                dgvProductDetails.Columns[i].ReadOnly = true;
            }

            dgvProductDetails.Columns["ProductStockID"].Visible = false;
            dgvProductDetails.Columns["StoreID"].Visible = false;
            dgvProductDetails.Columns["SubProductID"].Visible = false;
        }

        private void AddNewPage()
        {
            //try
            //{
            //    obj = new Form1();
            //   // obj.Show();
            //    obj.TopLevel = false;
            //    obj.Text = "New Page";
            //    obj.Location = new Point(0, 0);
            //    obj.Size = new System.Drawing.Size(300, 120);
            //    obj.Invalidate();
            //}
            //catch (System.ComponentModel.Win32Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private string GetBarCodeSettings()
        {
            string strBarCodeSettings = null;
            DataTable dataTable = ObjDAL.ExecuteSelectStatement("SELECT BarCodeSetting FROM " + clsUtility.DBName + ".dbo.tblBarCodeSettings WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dataTable))
            {
                if (dataTable.Rows[0]["BarCodeSetting"] != DBNull.Value)
                {
                    strBarCodeSettings = dataTable.Rows[0]["BarCodeSetting"].ToString();
                }
            }
            return strBarCodeSettings;
        }
        private string GetCompanyName()
        {
            return Convert.ToString(ObjDAL.ExecuteScalar("SELECT CompanyName FROM " + clsUtility.DBName + ".dbo.CompanyMaster WITH(NOLOCK)"));
        }
        private void SetBarCodeValues(Control objLable, DataGridViewRow selectedRow)
        {
            if (objLable.Tag.ToString().Trim() == "ProductName")
            {
                objLable.Text = selectedRow.Cells["ColItemName"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "StoreName")
            {
                objLable.Text = strCompanyName;
            }
            else if (objLable.Tag.ToString().Trim() == "Model")
            {
                objLable.Text = selectedRow.Cells["ColModelNo"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "Color")
            {
                objLable.Text = selectedRow.Cells["ColColor"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "Size")
            {
                objLable.Text = selectedRow.Cells["ColSize"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "Rate")
            {
                try
                {
                    if (chkPrintRate.Checked)
                    {
                        if (selectedRow.Cells["Rate"].Value != DBNull.Value)
                        {
                            //objLable.Text = "KD " + Convert.ToDecimal(selectedRow.Cells["Rate"].Value).ToString("F");
                            objLable.Text = "KD " + Convert.ToDecimal(selectedRow.Cells["Rate"].Value).ToString();
                        }
                    }
                    else
                    {
                        objLable.Text = string.Empty;
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (objLable.Tag.ToString().Trim() == "BarcodeNo")
            {
                objLable.Text = _Current_BarCodeNumber;
                // only for MD
                if (!chkPrintRate.Checked)
                {
                    objLable.Location = new Point(objLable.Location.X, objLable.Location.Y + 23);
                }

            }
            else if (objLable.Tag.ToString().Trim() == "Category")
            {
                string pID = selectedRow.Cells["ColProductID"].Value.ToString();

                string str = "  select  ( select top(1) CategoryName from " + clsUtility.DBName + ".[dbo].[CategoryMaster] WITH(NOLOCK) where CategoryID=p1.CategoryID) as CategoryName " +
                            "from " + clsUtility.DBName + ".[dbo].[ProductMaster] as p1 where p1.ProductID = " + pID;

                object cat = ObjDAL.ExecuteScalar(str);
                if (cat != null)
                {
                    objLable.Text = cat.ToString();
                }
            }
        }


        private string GetBarcodeNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjDAL.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Barcode_Sequance");
            return LastID.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCode, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to print the barcode for all QTY ?", clsUtility.strProjectTitle);
                if (result == false)
                {
                    return;
                }

                PrinterSettings printerSetting = new PrinterSettings();
                if (clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.BarCodePrinter).Trim().Length == 0)
                {
                    bool b = clsUtility.ShowQuestionMessage("Printer Not Configured for barcode. Do you want to print on default printer?", clsUtility.strProjectTitle);
                    if (b == false)
                    {
                        return;
                    }
                }
                printerSetting.PrinterName = clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.BarCodePrinter);

                PrintDialog pd = new PrintDialog();
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings = printerSetting;
                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;

                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    if (dgvProductDetails.Rows[i].Cells["colCHeck"].Value != DBNull.Value && Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["colCHeck"].Value))
                    {
                        _PrintRowData = dgvProductDetails.Rows[i];
                        _Current_BarCodeNumber = dgvProductDetails.Rows[0].Cells["BarcodeNo"].Value.ToString();
                        int QTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColQTY"].Value);

                        #region Manaually BarCode generate
                        //int PID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColProductID"].Value);
                        //int SubProductID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SubProductID"].Value);
                        //int SizeID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SizeID"].Value);
                        //int ColorID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColColorID"].Value);
                        //// check if barcode number exist
                        //DataTable dtBarCodeNumber = ObjCon.ExecuteSelectStatement("select BarcodeNo FROM  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster WITH(NOLOCK) WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID);
                        //if (ObjUtil.ValidateTable(dtBarCodeNumber))
                        //{
                        //    if (dtBarCodeNumber.Rows[0]["BarcodeNo"] != DBNull.Value && dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString().Length >= 0)
                        //    {
                        //        _Current_BarCodeNumber = dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString();
                        //    }
                        //    else
                        //    {
                        //        _Current_BarCodeNumber = GetBarcodeNumber();

                        //        // update the bar code in  ProductStockColorSizeMaster ( main stock table)
                        //        string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID;

                        //        ObjCon.ExecuteNonQuery(strUpdat);

                        //        // update the bar code in [ProductStockMaster]
                        //        string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND PurchaseInvoiceID=" + CurrentPurchaseInvoiceID +
                        //     " AND SizeID=" + SizeID + " AND ColorID=" + ColorID;
                        //        ObjCon.ExecuteNonQuery(strUpdate2);
                        //    }
                        //}
                        //else
                        //{
                        //    _Current_BarCodeNumber = GetBarcodeNumber();

                        //    string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                        //        " SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " ColorID=" + ColorID + " AND SizeID=" + SizeID;
                        //    ObjCon.ExecuteNonQuery(strUpdat);
                        //}
                        //UpdateProductBardCodeImageNo(PID.ToString(), _Current_BarCodeNumber);
                        //UpdateSubProductBardCodeImageNo(PID.ToString(), SubProductID.ToString(), _Current_BarCodeNumber);
                        #endregion

                        for (int Q = 0; Q < QTY; Q++)
                        {
                            doc.Print();
                        }
                        UpdateProductStockMasterStatus(QTY, Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ProductStockID"].Value));
                    }
                }

                this.Focus();
                this.Activate();
                clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);
                this.Focus();
                this.BringToFront();
                RefreshData();
            }
        }

        private void UpdateProductStockMasterStatus(int QTY, int ProductStockID)
        {
            int a = ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockMaster SET PrintCount+=" + QTY + ",UpdatedOn=GETDATE(),UpdatedBy=" + clsUtility.LoginID + " WHERE ProductStockID=" + ProductStockID);
        }
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Print image
                // Bitmap bm = new Bitmap(picBarCode.Image);

                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                e.Graphics.CompositingMode = CompositingMode.SourceCopy;

                //e.Graphics.DrawImage(bm, 0, 0);
                //bm.Dispose();

                using (Graphics g = e.Graphics)
                {
                    string strBarCodeSettingValue = GetBarCodeSettings();
                    if (strBarCodeSettingValue != null)
                    {
                        string[] strfiles = strBarCodeSettingValue.Split('\n');

                        if (strfiles.Length > 0)
                        {
                            //  string ProductID = _PrintRowData.Cells["ColProductID"].Value.ToString();
                            //obj.Controls.Clear();
                            //    obj.Refresh();
                            // AddNewPage();

                            if (strfiles.Length > 0)
                            {
                                for (int i = 0; i < strfiles.Length; i++)
                                {
                                    string[] strInfo = strfiles[i].Split('@');

                                    if (i == 0)
                                    {
                                        // set the back color and size of the page.
                                        // obj.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[0]));
                                        //obj.Size = new Size(Convert.ToInt32(strInfo[1]), Convert.ToInt32(strInfo[2]));
                                    }
                                    else
                                    { //Type-IsBold-Family-argb(int)-fsize(float)-w-h-x-y-text-backColor(int)-RecBorderStyle-borderStyle-borderColor
                                        if (strInfo[0] == "Label")
                                        {
                                            Label objLable = new Label();
                                            if (strInfo[1] == "True")
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Bold);
                                            }
                                            else
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Regular);
                                            }

                                            objLable.ForeColor = Color.FromArgb(Convert.ToInt32(strInfo[3]));
                                            objLable.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                            objLable.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                            objLable.Text = strInfo[9];
                                            objLable.BackColor = Color.Green;
                                            objLable.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[10]));
                                            objLable.Tag = strInfo[14];
                                            try
                                            {
                                                // int to enum
                                                objLable.TextAlign = (ContentAlignment)Convert.ToInt32(strInfo[15]);
                                            }
                                            catch
                                            {
                                            }
                                            SetBarCodeValues(objLable, _PrintRowData);

                                            //obj.Controls.Add(objLable);
                                            //obj.Refresh();

                                            StringFormat sf = new StringFormat();
                                            sf.LineAlignment = StringAlignment.Center;
                                            sf.Alignment = StringAlignment.Center;

                                            string caption3 = string.Format(objLable.Text);
                                            if (objLable.TextAlign == ContentAlignment.MiddleCenter)
                                            {
                                                // get the location of that control respective to bardcode panel
                                                RectangleF drawRect = objLable.ClientRectangle;
                                                drawRect.X = objLable.Location.X;
                                                drawRect.Y = objLable.Location.Y;

                                                // DRAW BACKGROUND REC ( Important for Testing )
                                                //Pen blackPen = new Pen(Color.Red);
                                                //e.Graphics.DrawRectangle(blackPen,drawRect.X,drawRect.Y,drawRect.Width,drawRect.Height);

                                                // Set format of string.
                                                StringFormat drawFormat = new StringFormat();
                                                drawFormat.Alignment = StringAlignment.Center;

                                                g.DrawString(caption3, objLable.Font, System.Drawing.Brushes.Black, drawRect, drawFormat);
                                            }
                                            else
                                            {
                                                g.DrawString(caption3, objLable.Font, System.Drawing.Brushes.Black, objLable.Location.X, objLable.Location.Y);
                                            }

                                        }
                                        else if (strInfo[0] == "VerticalLabel")
                                        {
                                            VerticalLabel objLable = new VerticalLabel();
                                            if (strInfo[1] == "True")
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Bold);
                                            }
                                            else
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Regular);
                                            }
                                            objLable.ForeColor = Color.FromArgb(Convert.ToInt32(strInfo[3]));
                                            objLable.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                            objLable.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                            objLable.Text = strInfo[9];
                                            objLable.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[10]));
                                            objLable.Tag = strInfo[14];

                                            SetBarCodeValues(objLable, _PrintRowData);

                                            //obj.Controls.Add(objLable);
                                            // obj.Refresh();

                                            string captionText = objLable.Text;
                                            Size preferredSize = g.MeasureString(objLable.Text, objLable.Font).ToSize();

                                            // change this value 
                                            int padding = 20;

                                            DrawRotatedTextAt(g, -90, captionText,
                                               objLable.Location.X, objLable.Location.Y + preferredSize.Width + padding, objLable.Font, Brushes.Black);

                                        }
                                        else if (strInfo[0] == "PictureBox")
                                        {
                                            PictureBox objPicBox = new PictureBox();
                                            objPicBox.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                            objPicBox.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                            objPicBox.BorderStyle = BorderStyle.FixedSingle;
                                            objPicBox.Image = Barcode.clsBarCodeUtility.GenerateBarCode(_Current_BarCodeNumber);
                                            objPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                            // objPicBox.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                            //obj.Controls.Add(objPicBox);

                                            // this code is specific to MD :
                                            if (!chkPrintRate.Checked)
                                            {
                                                objPicBox.Height = objPicBox.Height + 15;
                                            }
                                            g.DrawImage(objPicBox.Image, objPicBox.Location.X, objPicBox.Location.Y, objPicBox.Width, objPicBox.Height);

                                        }
                                        else if (strInfo[0] == "uRectangle")
                                        {
                                            uRectangle objRec = new uRectangle();
                                            objRec.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                            objRec.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));

                                            objRec.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[10]));

                                            switch (strInfo[11].Trim())
                                            {
                                                case "None":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.None;
                                                    break;
                                                case "Dotted":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.Dotted;
                                                    break;
                                                case "Dashed":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.Dashed;
                                                    break;
                                                case "Solid":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.Solid;
                                                    break;
                                                case "Inset":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.Inset;
                                                    break;
                                                case "Outset":
                                                    objRec.RectangleBorderStyle = ButtonBorderStyle.Outset;
                                                    break;
                                            }

                                            switch (strInfo[12].Trim())
                                            {
                                                case "None":
                                                    objRec.BorderStyle = BorderStyle.Fixed3D;
                                                    break;
                                                case "FixedSingle":
                                                    objRec.BorderStyle = BorderStyle.FixedSingle;
                                                    break;
                                                case "Fixed3D":
                                                    objRec.BorderStyle = BorderStyle.Fixed3D;
                                                    break;
                                            }
                                            objRec.BorderColor = Color.FromArgb(Convert.ToInt32(strInfo[13].Trim()));


                                        }
                                        else if (strInfo[0] == "Line")
                                        {
                                            Line objLable = new Line();
                                            if (strInfo[1] == "True")
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Bold);
                                            }
                                            else
                                            {
                                                objLable.Font = new Font(strInfo[2], float.Parse(strInfo[4]), FontStyle.Regular);
                                            }

                                            objLable.ForeColor = Color.FromArgb(Convert.ToInt32(strInfo[3]));
                                            objLable.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                            objLable.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                            objLable.Text = strInfo[9];

                                            objLable.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[10]));
                                            //obj.Controls.Add(objLable);
                                            //obj.Refresh();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }
        private void DrawRotatedTextAt(Graphics gr, float angle,
        string txt, int x, int y, Font the_font, Brush the_brush)
        {
            // Save the graphics state.
            GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawString(txt, the_font, the_brush, 0, 0);

            // Restore the graphics state.
            gr.Restore(state);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            //DataTable dtPurchaseInvDetails = ObjCon.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size '" + txtPurchaseID.Text + "'");
            ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, txtPurchaseID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, "0", clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("PrintStaus", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size");
            if (ObjUtil.ValidateDataSet(ds))
            {
                btnResetPrint.Enabled = true;
                DataTable dtPurchaseInvDetails = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtPurchaseInvDetails))
                {
                    dgvProductDetails.DataSource = dtPurchaseInvDetails;
                    CurrentPurchaseInvoiceID = txtPurchaseID.Text;
                    //txtPurchaseID.Text = "";
                }
                else
                {
                    dgvProductDetails.DataSource = dtPurchaseInvDetails;
                    chkAll.Visible = false;
                    CurrentPurchaseInvoiceID = "";
                    //txtPurchaseID.Clear();
                    clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
                    cmbListBox.DataSource = null;
                }
                if (ds.Tables.Count >= 1)
                {
                    DataTable dtStyleNo = ds.Tables[1];
                    if (ObjUtil.ValidateTable(dtStyleNo))
                    {
                        cmbListBox.DataSource = dtStyleNo;
                        cmbListBox.DisplayMember = "ModelNo";
                        cmbListBox.ValueMember = "ModelNo";
                        cmbListBox.SelectedIndex = -1;
                    }
                }
                if (ds.Tables.Count >= 2)
                {
                    DataTable dtProductPrint = ds.Tables[2];
                    if (ObjUtil.ValidateTable(dtProductPrint))
                    {
                        dataGridView1.DataSource = dtProductPrint;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                }
            }
            else
            {
                btnResetPrint.Enabled = false;
            }
            chkAll.Visible = true;
        }

        private void SearchByStyleNo()
        {
            ObjDAL.SetStoreProcedureData("PurchaseInvoiceID", SqlDbType.Int, txtPurchaseID.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("StoreID", SqlDbType.Int, frmHome.Home_StoreID, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("ModelNo", SqlDbType.NVarChar, cmbListBox.Text, clsConnection_DAL.ParamType.Input);
            ObjDAL.SetStoreProcedureData("PrintStaus", SqlDbType.Int, 0, clsConnection_DAL.ParamType.Input);
            DataSet ds = ObjDAL.ExecuteStoreProcedure_Get(clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dtPurchaseInvDetails = ds.Tables[0];
                if (ObjUtil.ValidateTable(dtPurchaseInvDetails))
                {
                    dgvProductDetails.DataSource = dtPurchaseInvDetails;
                    CurrentPurchaseInvoiceID = txtPurchaseID.Text;
                    //txtPurchaseID.Text = "";
                }
                else
                {
                    dgvProductDetails.DataSource = dtPurchaseInvDetails;
                    chkAll.Visible = false;
                    CurrentPurchaseInvoiceID = "";
                    //txtPurchaseID.Clear();
                    clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
                }
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    dgvProductDetails.Rows[i].Cells["colCheck"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    dgvProductDetails.Rows[i].Cells["colCheck"].Value = false;
                }
            }

            dgvProductDetails.EndEdit();
        }

        private void txtPurchaseInvoice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPurchaseInvoice.TextLength > 0)
                {
                    //DataTable dt = ObjCon.ExecuteSelectStatement("EXEC  " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtPurchaseInvoice.Text + "', 1");
                    DataTable dt = ObjDAL.ExecuteSelectStatement("EXEC  " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtPurchaseInvoice.Text + "', 2");
                    if (ObjUtil.ValidateTable(dt))
                    {
                        ObjUtil.SetControlData(txtPurchaseInvoice, "SupplierBillNo");
                        ObjUtil.SetControlData(txtPurchaseID, "PurchaseInvoiceID");

                        ObjUtil.ShowDataPopup(dt, txtPurchaseInvoice, this, this);

                        if (ObjUtil.GetDataPopup() != null && ObjUtil.GetDataPopup().DataSource != null)
                        {
                            // if there is only one column                
                            ObjUtil.GetDataPopup().AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (ObjUtil.GetDataPopup().ColumnCount > 0)
                            {
                                ObjUtil.GetDataPopup().Columns["PurchaseInvoiceID"].Visible = false;
                                ObjUtil.GetDataPopup().Columns["SupplierID"].Visible = false;
                                ObjUtil.SetDataPopupSize(300, 0);
                            }
                        }
                        ObjUtil.GetDataPopup().CellClick += FrmBarCode_CellClick;
                        ObjUtil.GetDataPopup().KeyDown += FrmBarCode_KeyDown; ;
                    }
                }
                else
                {
                    ObjUtil.CloseAutoExtender();
                }
            }
            catch (Exception)
            {
            }
        }

        private void FrmBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                rdShowAll.Checked = false;
                LoadData();
            }
        }

        private void FrmBarCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rdShowAll.Checked = false;
            LoadData();
        }

        private void RefreshData()
        {
            //DataTable dtPurchaseInvDetails = ObjCon.ExecuteSelectStatement("EXEC " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size '" + CurrentPurchaseInvoiceID + "'");
            //if (ObjUtil.ValidateTable(dtPurchaseInvDetails))
            //{
            //    dgvProductDetails.DataSource = dtPurchaseInvDetails;
            //}
            //else
            //{
            //    dgvProductDetails.DataSource = dtPurchaseInvDetails;
            //    chkAll.Visible = false;

            //    clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
            //}
            LoadData();
            chkAll.Visible = true;
            chkAll.Checked = false;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshData();
        }
        private void UpdateProductBardCodeImageNo(string productID, string ImageNumber)
        {
            DataTable dtProductID = ObjDAL.ExecuteSelectStatement("select Photo FROM " + clsUtility.DBName + ".[dbo].[ProductMaster] WITH(NOLOCK) where ProductID=" + productID);
            if (ObjUtil.ValidateTable(dtProductID))
            {
                if (dtProductID.Rows[0]["Photo"] == DBNull.Value)
                {
                    string sqlUpdate = "update " + clsUtility.DBName + ".[dbo].[ProductMaster] " +
                                        " set Photo = '" + ImageNumber + "' where ProductID=" + productID;

                    ObjDAL.ExecuteNonQuery(sqlUpdate);
                }
            }
        }
        private void UpdateSubProductBardCodeImageNo(string productID, string SubProductID, string ImageNumber)
        {
            DataTable dtProductID = ObjDAL.ExecuteSelectStatement("select Photo FROM " + clsUtility.DBName + ".[dbo].[tblProductWiseModelNo] WITH(NOLOCK) where ProductID=" + productID + " AND SubProductID=" + SubProductID);
            if (ObjUtil.ValidateTable(dtProductID))
            {
                if (dtProductID.Rows[0]["Photo"] == DBNull.Value)
                {
                    string sqlUpdate = "update " + clsUtility.DBName + ".[dbo].[tblProductWiseModelNo] " +
                                        " set Photo = '" + ImageNumber + "' where ProductID=" + productID + " AND SubProductID=" + SubProductID;

                    ObjDAL.ExecuteNonQuery(sqlUpdate);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCode, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                if (numericUpDown1.Value <= 0)
                {
                    clsUtility.ShowInfoMessage("Please enter QTY greater than 0", clsUtility.strProjectTitle);
                    return;
                }

                PrintDialog pd = new PrintDialog();
                PrinterSettings printerSetting = new PrinterSettings();
                if (clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.BarCodePrinter).Trim().Length == 0)
                {
                    bool b = clsUtility.ShowQuestionMessage("Printer Not Configured for barcode. Do you want to print on default printer?", clsUtility.strProjectTitle);
                    if (b == false)
                    {
                        return;
                    }
                }
                printerSetting.PrinterName = clsBarCodeUtility.GetPrinterName(clsBarCodeUtility.PrinterType.BarCodePrinter);

                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings = printerSetting;

                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;
                //if (pd.ShowDialog() == DialogResult.OK)/
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    if (dgvProductDetails.Rows[i].Cells["colCHeck"].Value != DBNull.Value && Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["colCHeck"].Value))
                    {
                        _PrintRowData = dgvProductDetails.Rows[i];
                        _Current_BarCodeNumber = dgvProductDetails.Rows[i].Cells["BarcodeNo"].Value.ToString();

                        #region Manaually BarCode generate
                        //int PID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColProductID"].Value);
                        //int SubProductID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SubProductID"].Value);

                        //int QTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColQTY"].Value);

                        //int SizeID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SizeID"].Value);
                        //int ColorID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColColorID"].Value);

                        // check if barcode number exist
                        //DataTable dtBarCodeNumber = ObjCon.ExecuteSelectStatement("SELECT BarcodeNo FROM  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster WITH(NOLOCK) WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID);
                        //if (ObjUtil.ValidateTable(dtBarCodeNumber))
                        //{
                        //    if (dtBarCodeNumber.Rows[0]["BarcodeNo"] != DBNull.Value && dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString().Length >= 0)
                        //    {
                        //        _Current_BarCodeNumber = dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString();

                        //        // update the bar code in [ProductStockMaster]
                        //        string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND PurchaseInvoiceID=" + CurrentPurchaseInvoiceID +
                        //     " AND SizeID=" + SizeID + " AND ColorID=" + ColorID;
                        //        ObjCon.ExecuteNonQuery(strUpdate2);
                        //    }
                        //    else
                        //    {
                        //        _Current_BarCodeNumber = GetBarcodeNumber();

                        //        // update the bar code in  ProductStockColorSizeMaster ( main stock table)
                        //        string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID;

                        //        ObjCon.ExecuteNonQuery(strUpdat);

                        //        // update the bar code in [ProductStockMaster]
                        //        string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND PurchaseInvoiceID=" + CurrentPurchaseInvoiceID +
                        //     " AND SizeID=" + SizeID + " AND ColorID=" + ColorID;
                        //        ObjCon.ExecuteNonQuery(strUpdate2);
                        //    }
                        //}
                        //else
                        //{
                        //    _Current_BarCodeNumber = GetBarcodeNumber();

                        //    string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                        //        " SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND SubProductID=" + SubProductID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID;
                        //    ObjCon.ExecuteNonQuery(strUpdat);
                        //}
                        //UpdateProductBardCodeImageNo(PID.ToString(), _Current_BarCodeNumber);
                        //UpdateSubProductBardCodeImageNo(PID.ToString(), SubProductID.ToString(), _Current_BarCodeNumber);
                        #endregion

                        for (int Q = 0; Q < numericUpDown1.Value; Q++)
                        {
                            doc.Print();
                        }
                        UpdateProductStockMasterStatus(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ProductStockID"].Value));
                    }
                }
                this.Focus();
                this.BringToFront();
                clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);
                this.Activate();
                RefreshData();

                //try
                //{
                //    if (obj!=null)
                //    {
                //        obj.Dispose();
                //        obj = null;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("While disposing : " + ex.Message);
                //}
            }
        }

        private void frmBarCode_Activated(object sender, EventArgs e)
        {
            txtPurchaseInvoice.Focus();
        }

        private void cmbListBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            rdShowAll.Checked = false;
            SearchByStyleNo();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ObjUtil.SetRowNumber(dataGridView1);
            ObjUtil.SetDataGridProperty(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);

            dataGridView1.Columns["ProductStockID"].Visible = false;
            dataGridView1.Columns["StoreID"].Visible = false;
            dataGridView1.Columns["SubProductID"].Visible = false;

            dataGridView1.Columns["PurchaseInvoiceID"].Visible = false;
            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["ColorID"].Visible = false;
            dataGridView1.Columns["SizeID"].Visible = false;
        }

        private void rdShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowAll.Checked)
            {
                if (txtPurchaseInvoice.TextLength > 0)
                {
                    cmbListBox.SelectedIndex = -1;
                    LoadData();
                }
            }
        }

        private void btnResetPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtBarCode = (DataTable)dgvProductDetails.DataSource;
                DataTable dtBarCodePrint = (DataTable)dataGridView1.DataSource;
                if (ObjUtil.ValidateTable(dtBarCode) || ObjUtil.ValidateTable(dtBarCodePrint))
                {
                    int a = ObjDAL.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.ProductStockMaster SET PrintCount = 0 WHERE PurchaseInvoiceID = " + txtPurchaseID.Text + " AND StoreID = " + frmHome.Home_StoreID);
                    if (a > 0)
                    {
                        clsUtility.ShowInfoMessage("BarCode Print Count is reset for Supplier Bill NO. " + txtPurchaseInvoice.Text.Trim());
                        LoadData();
                    }
                    else
                        clsUtility.ShowInfoMessage("Unable to reset BarCode Print Count for Supplier Bill NO. " + txtPurchaseInvoice.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                clsUtility.ShowErrorMessage(ex.ToString());
            }
        }
    }
}