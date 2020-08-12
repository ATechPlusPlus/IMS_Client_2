using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using CoreApp;
using ZXing;

namespace IMS_Client_2.Barcode
{
    public partial class frmQuickBarCodePrint : Form
    {
        public frmQuickBarCodePrint()
        {
            InitializeComponent();
        }
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        private void frmQuickBarCodePrint_Load(object sender, EventArgs e)
        {
            btnPrintManualBarcode.BackgroundImage = B_Leave;
           
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
        CoreApp.clsConnection_DAL ObjCon = new clsConnection_DAL(true);

        clsUtility ObjUtil = new clsUtility();
        private void btnPrintManualBarcode_Click(object sender, EventArgs e)
        {
            if (!IsBarCodeSettings())
            {
                clsUtility.ShowInfoMessage("Please set the Barcode design before barcode printing." + Environment.NewLine + "To Design Barcode, Open Barcode designer from Barcode menu from main window.");
                return;
            }
            ObjCon.SetStoreProcedureData("BarCodeNo", SqlDbType.NVarChar, txtBarcodenumber.Text, clsConnection_DAL.ParamType.Input);
            DataSet ds= ObjCon.ExecuteStoreProcedure_Get("SPR_GetQuickBarCodeDetails");
            if (ObjUtil.ValidateDataSet(ds))
            {
                dgvProductDetails.DataSource = ds.Tables[0];
                if (dgvProductDetails.Rows.Count>0)
                {
                    PrintBarCode();

                }
                else
                {
                    clsUtility.ShowInfoMessage("can not find the barcode number : " + txtBarcodenumber.Text + ".");
                }
                
            }
        }
        private bool IsBarCodeSettings()
        {
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT BarCodeSetting FROM " + clsUtility.DBName + ".dbo.tblBarCodeSettings WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dataTable))
            {
                if (dataTable.Rows[0]["BarCodeSetting"] != DBNull.Value && dataTable.Rows[0]["BarCodeSetting"].ToString().Trim().Length>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        DataGridViewRow _PrintRowData;
        string _Current_BarCodeNumber = "";
        string strCompanyName = "";
        private void PrintBarCode()
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

            _PrintRowData = dgvProductDetails.Rows[0];
            _Current_BarCodeNumber = dgvProductDetails.Rows[0].Cells["BarcodeNo"].Value.ToString();


            for (int Q = 0; Q < numericUpDown1.Value; Q++)
            {
                doc.Print();
            }



            this.Focus();
                this.BringToFront();
            clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);
            numericUpDown1.Value = 1;
                this.Activate();
              

               
            
        }
        private string GetBarCodeSettings()
        {
            string strBarCodeSettings = null;
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT BarCodeSetting FROM " + clsUtility.DBName + ".dbo.tblBarCodeSettings WITH(NOLOCK)");
            if (ObjUtil.ValidateTable(dataTable))
            {
                if (dataTable.Rows[0]["BarCodeSetting"] != DBNull.Value)
                {
                    strBarCodeSettings = dataTable.Rows[0]["BarCodeSetting"].ToString();
                }
            }
            return strBarCodeSettings;
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
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


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

                object cat = ObjCon.ExecuteScalar(str);
                if (cat != null)
                {
                    objLable.Text = cat.ToString();
                }
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

    }
}
