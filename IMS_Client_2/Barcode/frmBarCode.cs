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
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();

        Form1 obj;

        string strCompanyName = "";
        string CurrentPurchaseInvoiceID = "";
        DataGridViewRow _PrintRowData;
        string _Current_BarCodeNumber = "";

        Image B_Leave = IMS_Client_2.Properties.Resources.B_click;
        Image B_Enter = IMS_Client_2.Properties.Resources.B_on;

        private void frmBarCode_Load(object sender, EventArgs e)
        {
            btnPrintBarcode.BackgroundImage = B_Leave;

            strCompanyName = GetStoreName();

            btnSearch.BackgroundImage = B_Leave;
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
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
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
        private string GetBarCodeSettings()
        {
            string strBarCodeSettings = null;
            DataTable dataTable = ObjCon.ExecuteSelectStatement("SELECT BarCodeSetting from " + clsUtility.DBName + ".dbo.tblBarCodeSettings");
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["BarCodeSetting"] != DBNull.Value)
                {
                    strBarCodeSettings = dataTable.Rows[0]["BarCodeSetting"].ToString();
                }
            }
            return strBarCodeSettings;
        }
        private string GetStoreName()
        {
            return Convert.ToString(ObjCon.ExecuteScalar("SELECT CompanyName FROM " + clsUtility.DBName + ".dbo.CompanyMaster WITH(NOLOCK)"));
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
                objLable.Text = selectedRow.Cells["Rate"].Value.ToString();
            }
            else if (objLable.Tag.ToString().Trim() == "BarcodeNo")
            {
                objLable.Text = selectedRow.Cells["barcodeno"].Value.ToString();
            }
        }
        private void LoadTemplate(DataGridViewRow _Row)
        {
            string strBarCodeSettingValue = GetBarCodeSettings();
            if (strBarCodeSettingValue != null)
            {
                string[] strfiles = strBarCodeSettingValue.Split('\n');

                if (strfiles.Length > 0)
                {
                    string ProductID = _Row.Cells["ColProductID"].Value.ToString();
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

                                    SetBarCodeValues(objLable, _Row);

                                    obj.Controls.Add(objLable);
                                    obj.Refresh();
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

                                    SetBarCodeValues(objLable, _Row);

                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objLable);
                                    obj.Refresh();
                                }
                                else if (strInfo[0] == "PictureBox")
                                {
                                    PictureBox objPicBox = new PictureBox();

                                    objPicBox.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                    objPicBox.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                    objPicBox.BorderStyle = BorderStyle.FixedSingle;

                                    string barValue = "Product_" + ProductID;

                                    objPicBox.Image = Barcode.clsBarCodeUtility.GenerateBarCode(_Current_BarCodeNumber);
                                    objPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                    objPicBox.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objPicBox);
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
            obj.Refresh();
        }

        private string GetBarcodeNumber()
        {
            //SequenceInvoice : this is a sequance object created in SQL ( this is not a table)
            int LastID = ObjCon.ExecuteScalarInt("SELECT NEXT VALUE FOR " + clsUtility.DBName + ".[dbo].Barcode_Sequance");
            return LastID.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = clsUtility.ShowQuestionMessage("Are you sure, you want to print the barcode for all QTY ?", clsUtility.strProjectTitle);
            if (result == false)
            {
                return;
            }
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
                {
                    if (dgvProductDetails.Rows[i].Cells["colCHeck"].Value != DBNull.Value && Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["colCHeck"].Value))
                    {
                        _PrintRowData = dgvProductDetails.Rows[i];

                        int PID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColProductID"].Value);
                        int QTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColQTY"].Value);

                        int SizeID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SizeID"].Value);
                        int ColorID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColColorID"].Value);

                        // check if barcode number exist
                        DataTable dtBarCodeNumber = ObjCon.ExecuteSelectStatement("select BarcodeNo from  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster where ProductID=" + PID + " and ColorID=" + ColorID + " and SizeID=" + SizeID);
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
                                string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND PurchaseInvoiceID=" + CurrentPurchaseInvoiceID +
                             " and SizeID=" + SizeID + " AND ColorID=" + ColorID;
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

                        for (int Q = 0; Q < QTY; Q++)
                        {
                            doc.Print();
                        }
                    }
                }
            }
            clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);
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
                        string ProductID = _PrintRowData.Cells["ColProductID"].Value.ToString();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            DataTable dtPurchaseInvDetails = ObjCon.ExecuteSelectStatement("exec " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size '" + txtPurchaseID.Text + "'");
            if (dtPurchaseInvDetails != null && dtPurchaseInvDetails.Rows.Count > 0)
            {
                dgvProductDetails.DataSource = dtPurchaseInvDetails;
                CurrentPurchaseInvoiceID = txtPurchaseID.Text;
                txtPurchaseID.Text = "";
            }
            else
            {
                dgvProductDetails.DataSource = dtPurchaseInvDetails;
                chkAll.Visible = false;
                CurrentPurchaseInvoiceID = "";
                txtPurchaseID.Clear();
                clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
            }
            chkAll.Visible = true;
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
                DataTable dt = ObjCon.ExecuteSelectStatement("exec  " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_Popup '" + txtPurchaseInvoice.Text + "', 1");
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
                            ObjUtil.SetDataPopupSize(400, 0);
                        }
                    }
                    //ObjUtil.GetDataPopup().CellClick += frmSalecounter_CellClick;
                    //ObjUtil.GetDataPopup().KeyDown += frmSalecounter_KeyDown;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dtPurchaseInvDetails = ObjCon.ExecuteSelectStatement("exec " + clsUtility.DBName + ".dbo.Get_PurchaseInvoice_BulkPrint_Color_Size '" + CurrentPurchaseInvoiceID + "'");
            if (dtPurchaseInvDetails != null && dtPurchaseInvDetails.Rows.Count > 0)
            {
                dgvProductDetails.DataSource = dtPurchaseInvDetails;
            }
            else
            {
                dgvProductDetails.DataSource = dtPurchaseInvDetails;
                chkAll.Visible = false;

                clsUtility.ShowInfoMessage("No purchase invoice found for the given purhcase number.", clsUtility.strProjectTitle);
            }
            chkAll.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                clsUtility.ShowInfoMessage("Please enter QTY greater than 0", clsUtility.strProjectTitle);
                return;
            }

            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;
            //if (pd.ShowDialog() == DialogResult.OK)/
            for (int i = 0; i < dgvProductDetails.Rows.Count; i++)
            {
                if (dgvProductDetails.Rows[i].Cells["colCHeck"].Value != DBNull.Value && Convert.ToBoolean(dgvProductDetails.Rows[i].Cells["colCHeck"].Value))
                {
                    _PrintRowData = dgvProductDetails.Rows[i];

                    int PID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColProductID"].Value);
                    int QTY = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColQTY"].Value);

                    int SizeID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["SizeID"].Value);
                    int ColorID = Convert.ToInt32(dgvProductDetails.Rows[i].Cells["ColColorID"].Value);

                    // check if barcode number exist
                    DataTable dtBarCodeNumber = ObjCon.ExecuteSelectStatement("select BarcodeNo from  " + clsUtility.DBName + ".dbo.ProductStockColorSizeMaster where ProductID=" + PID + " and ColorID=" + ColorID + " and SizeID=" + SizeID);
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
                            string strUpdate2 = "UPDATE " + clsUtility.DBName + ".[dbo].[ProductStockMaster] SET BarcodeNo='" + _Current_BarCodeNumber + "' WHERE ProductID=" + PID + " AND PurchaseInvoiceID=" + CurrentPurchaseInvoiceID +
                         " and SizeID=" + SizeID + " AND ColorID=" + ColorID;
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

                    for (int Q = 0; Q < numericUpDown1.Value; Q++)
                    {
                        doc.Print();
                    }
                }
            }
            clsUtility.ShowInfoMessage("Operation completed !", clsUtility.strProjectTitle);

        }
    }
    
}