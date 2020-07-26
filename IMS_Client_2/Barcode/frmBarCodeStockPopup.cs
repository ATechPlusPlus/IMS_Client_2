using CoreApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Barcode
{
    public partial class frmBarCodeStockPopup : Form
    {
        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;
        public frmBarCodeStockPopup()
        {
            InitializeComponent();
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

        private void frmBarCodeStockPopup_Load(object sender, EventArgs e)
        {
            btnPrintManualBarcode.BackgroundImage = B_Leave;
            strCompanyName = GetStoreName();
        }
        private string GetStoreName()
        {
            return Convert.ToString(ObjCon.ExecuteScalar("SELECT CompanyName FROM " + clsUtility.DBName + ".dbo.CompanyMaster WITH(NOLOCK)"));
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radManual.Checked)
            {
                numericUpDown1.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
            }
        }
        CoreApp.clsConnection_DAL ObjCon = new CoreApp.clsConnection_DAL(true);
        CoreApp.clsUtility ObjUtil = new CoreApp.clsUtility();
      
        public  DataGridViewSelectedRowCollection dgvRows;
        
        string _Current_BarCodeNumber = "";
        private string GetBarcodeNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjCon.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Barcode_Sequance");
            return LastID.ToString();
        }
        private void UpdateProductBardCodeImageNo(string productID, string ImageNumber)
        {
            DataTable dtProductID = ObjCon.ExecuteSelectStatement("select Photo   FROM [IMS_Client_2].[dbo].[ProductMaster] where ProductID=" + productID);
            if (dtProductID.Rows.Count > 0)
            {
                if (dtProductID.Rows[0]["Photo"] == DBNull.Value)
                {
                    string sqlUpdate = "update[IMS_Client_2].[dbo].[ProductMaster] " +
                                        " set Photo = '" + ImageNumber + "' where ProductID=" + productID;

                    ObjCon.ExecuteNonQuery(sqlUpdate);
                }

            }
        }
        DataGridViewRow _PrintRowData;
        private string GetBarCodeSettings()
        {
            string strBarCodeSettings = null;
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT BarCodeSetting FROM " + clsUtility.DBName + ".dbo.tblBarCodeSettings WITH(NOLOCK)");
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["BarCodeSetting"] != DBNull.Value)
                {
                    strBarCodeSettings = dataTable.Rows[0]["BarCodeSetting"].ToString();
                }
            }
            return strBarCodeSettings;
        }
        Form1 obj;
        private void AddNewPage()
        {
            obj = new Form1();
            obj.Show();
            obj.TopLevel = false;
            obj.Text = "New Page";
            obj.Location = new Point(0, 0);
            obj.Size = new System.Drawing.Size(300, 120);
            obj.Invalidate();
        }
        string strCompanyName = "";
        private void SetBarCodeValues(Control objLable, DataGridViewRow selectedRow)
        {
            if (objLable.Tag.ToString().Trim() == "ProductName")
            {
                objLable.Text = selectedRow.Cells["ProductName"].Value.ToString();
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
                objLable.Text = selectedRow.Cells["Rate"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "BarcodeNo")
            {
                objLable.Text = selectedRow.Cells["barcodeno"].Value.ToString();
            }
        }
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
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
                        string ProductID = _PrintRowData.Cells["ProductID"].Value.ToString();
                        //obj.Controls.Clear();
                        //    obj.Refresh();
                        AddNewPage();

                        if (strfiles.Length > 0)
                        {
                            for (int i = 0; i < strfiles.Length; i++)
                            {
                                string[] strInfo = strfiles[i].Split('@');

                                if (i == 0)
                                {
                                    obj.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[0]));
                                    obj.Size = new Size(Convert.ToInt32(strInfo[1]), Convert.ToInt32(strInfo[2]));
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

                                        objLable.BackColor = Color.FromArgb(Convert.ToInt32(strInfo[10]));
                                        objLable.Tag = strInfo[14];

                                        SetBarCodeValues(objLable, _PrintRowData);

                                        obj.Controls.Add(objLable);
                                        obj.Refresh();

                                        string caption3 = string.Format(objLable.Text);
                                        g.DrawString(caption3, objLable.Font, System.Drawing.Brushes.Black, objLable.Location.X, objLable.Location.Y);
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

                                        objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                        obj.Controls.Add(objLable);
                                        obj.Refresh();

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
                                        objPicBox.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                        obj.Controls.Add(objPicBox);

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

                                        objRec.MouseEnter += new EventHandler(obj.control_MouseEnter);
                                        objRec.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                        objRec.MouseDown += new MouseEventHandler(obj.control_MouseDown);
                                        objRec.MouseMove += new MouseEventHandler(obj.control_MouseMove);
                                        objRec.MouseUp += new MouseEventHandler(obj.control_MouseUp);
                                        objRec.Click += obj.ctrl_Click;
                                        objRec.DoubleClick += obj.ctrl_DoubleClick;

                                        objRec.Click += new EventHandler(obj.ctrl_Click);

                                        objRec.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                        obj.Controls.Add(objRec);
                                        obj.Refresh();
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
                                        obj.Controls.Add(objLable);
                                        obj.Refresh();
                                    }
                                }
                            }
                        }
                    }
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

        private void btnPrintManualBarcode_Click(object sender, EventArgs e)
        {
            if (radManual.Checked)
            {
                PrintDialog pd = new PrintDialog();
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;
                //if (pd.ShowDialog() == DialogResult.OK)/
                for (int i = 0; i < dgvRows.Count; i++)
                {
                    
                        _PrintRowData = dgvRows[i];

                        int PID = Convert.ToInt32(dgvRows[i].Cells["ProductID"].Value);
                        int QTY = Convert.ToInt32(dgvRows[i].Cells["QTY"].Value);

                        int SizeID = Convert.ToInt32(dgvRows[i].Cells["SizeID"].Value);
                        int ColorID = Convert.ToInt32(dgvRows[i].Cells["ColorID"].Value);

                        // check if barcode number exist
                        DataTable dtBarCodeNumber = ObjCon.ExecuteSelectStatement("SELECT BarcodeNo FROM  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster WITH(NOLOCK) WHERE ProductID=" + PID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID);
                        if (ObjUtil.ValidateTable(dtBarCodeNumber))
                        {
                            if (dtBarCodeNumber.Rows[0]["BarcodeNo"] != DBNull.Value && dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString().Length >= 0)
                            {
                                _Current_BarCodeNumber = dtBarCodeNumber.Rows[0]["BarcodeNo"].ToString();
                            }
                            else
                            {
                                _Current_BarCodeNumber = GetBarcodeNumber();

                                // update the bar code in  ProductStockColorSizeMaster ( main stock table)
                                string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID;

                                ObjCon.ExecuteNonQuery(strUpdat);

                                // update the bar code in [ProductStockMaster]
                                string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + 
                             " AND SizeID=" + SizeID + " AND ColorID=" + ColorID;
                                ObjCon.ExecuteNonQuery(strUpdate2);
                            }
                        }
                        else
                        {
                            _Current_BarCodeNumber = GetBarcodeNumber();

                            string strUpdat = "UPDATE  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster " +
                                " SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND ColorID=" + ColorID + " AND SizeID=" + SizeID;
                            ObjCon.ExecuteNonQuery(strUpdat);
                        }
                        UpdateProductBardCodeImageNo(PID.ToString(), _Current_BarCodeNumber);
                        for (int Q = 0; Q < numericUpDown1.Value; Q++)
                        {
                            doc.Print();
                        }
                    
                }
                this.Focus();
                this.BringToFront();
                clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);
            }
            else if (radQTY.Checked)
            {

            }
        }
    }
}
