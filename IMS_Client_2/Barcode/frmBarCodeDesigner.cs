using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CoreApp;

namespace IMS_Client_2.Barcode
{
    public partial class frmBarCodeDesigner : Form
    {
        public frmBarCodeDesigner()
        {
            InitializeComponent();
        }
        clsConnection_DAL ObjCon = new clsConnection_DAL(true);
        clsUtility ObjUtil = new clsUtility();
        Form1 obj;
        Control pageControl;
        Point DragStart = Point.Empty;
        Control tgSelectedControl;
        Control SelectedControl;
        Direction direction;
        //Point newLocation;
        //Size newSize;

        string strTemplate;
        const int DRAG_HANDLE_SIZE = 7;
        //int mouseX, mouseY;
        bool Dragging = false;
        bool isLoad = false;

        void obj_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics, obj.ClientRectangle, Color.Black, ButtonBorderStyle.Dashed);
            //base.OnPaint(e);
        }
        
        private void SaveBarCodeSettings(string BarCodeData)
        {
            int count = ObjCon.CountRecords(clsUtility.DBName + ".dbo.tblBarCodeSettings");
            if (count == 0)
            {
                ObjCon.SetColumnData("BarCodeSetting", SqlDbType.NVarChar, BarCodeData);
                ObjCon.InsertData(clsUtility.DBName + ".dbo.tblBarCodeSettings", false);
            }
            else
            {

               int r= ObjCon.ExecuteNonQuery("Update " + clsUtility.DBName + ".dbo.tblBarCodeSettings set BarCodeSetting='" + BarCodeData + "'");
              
            }
        }
        private void GetBarCodeSettingFromFile()
        {

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
                else
                {
                    clsUtility.ShowInfoMessage("No Barcode Template found.", clsUtility.strProjectTitle);
                }
            }
            else
            {
                clsUtility.ShowInfoMessage("No Barcode Template found.", clsUtility.strProjectTitle);
            }
            return strBarCodeSettings;
        }
        public enum Direction
        {
            Any,
            Horizontal,
            Vertical,
            NW,
            N,
            NE,
            W,
            E,
            SW,
            S,
            SE,
            None
        }

        public void Init(Control control, Control container, Direction direction)
        {
            pageControl = control;
        }
        void obj_Click(object sender, EventArgs e)
        {
            ReRegisterAll();
            obj.ClearSelectedControl();
            propertyGrid1.SelectedObject = obj;
        }
        public void RemoveMouseMoveEvent()
        {
            obj.MouseMove -= new MouseEventHandler(control_MouseMove);
        }

        public void UnRegisterAll(Object tgctr = null)
        {
            cmbProperty.SelectedIndex = -1;

            tgSelectedControl = (Control)tgctr;
            pageControl.MouseDown -= new MouseEventHandler(control_MouseDown);
            pageControl.MouseMove -= new MouseEventHandler(control_MouseMove);
            pageControl.MouseUp -= new MouseEventHandler(control_MouseUp);
        }
        public void ReRegisterAll()
        {
            pageControl.MouseDown -= new MouseEventHandler(control_MouseDown);
            pageControl.MouseMove -= new MouseEventHandler(control_MouseMove);
            pageControl.MouseUp -= new MouseEventHandler(control_MouseUp);

            pageControl.MouseDown += new MouseEventHandler(control_MouseDown);
            pageControl.MouseMove += new MouseEventHandler(control_MouseMove);
            pageControl.MouseUp += new MouseEventHandler(control_MouseUp);
        }
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragStart = new Point(e.X, e.Y);
            pageControl.Capture = true;

            DrawControlBorder(pageControl);
            pageControl.Invalidate();

        }
        /// <summary>
        /// Draw a border and drag handles around the control, on mouse down and up.
        /// </summary>
        /// <param name="sender"></param>
        private void DrawControlBorder(object sender)
        {
            Control control = (Control)sender;
            //define the border to be drawn, it will be offset by DRAG_HANDLE_SIZE / 2
            //around the control, so when the drag handles are drawn they will be seem
            //connected in the middle.
            Rectangle Border = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE / 2),
                new Size(control.Size.Width + DRAG_HANDLE_SIZE,
                    control.Size.Height + DRAG_HANDLE_SIZE));
            //define the 8 drag handles, that has the size of DRAG_HANDLE_SIZE
            Rectangle NW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle N = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle NE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y - DRAG_HANDLE_SIZE),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle W = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle E = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height / 2 - DRAG_HANDLE_SIZE / 2),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SW = new Rectangle(
                new Point(control.Location.X - DRAG_HANDLE_SIZE,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle S = new Rectangle(
                new Point(control.Location.X + control.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));
            Rectangle SE = new Rectangle(
                new Point(control.Location.X + control.Width,
                    control.Location.Y + control.Height),
                new Size(DRAG_HANDLE_SIZE, DRAG_HANDLE_SIZE));

            //get the form graphic
            Graphics g = this.CreateGraphics();
            //draw the border and drag handles
            ControlPaint.DrawBorder(g, Border, Color.Black, ButtonBorderStyle.Dashed);
            //ControlPaint.DrawGrabHandle(g, NW, true, true);
            //ControlPaint.DrawGrabHandle(g, N, true, true);
            //ControlPaint.DrawGrabHandle(g, NE, true, true);
            //ControlPaint.DrawGrabHandle(g, W, true, true);
            //ControlPaint.DrawGrabHandle(g, E, true, true);
            //ControlPaint.DrawGrabHandle(g, SW, true, true);
            //ControlPaint.DrawGrabHandle(g, S, true, true);
            //ControlPaint.DrawGrabHandle(g, SE, true, true);
            g.Dispose();
        }
        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                if (direction != Direction.Vertical)
                    pageControl.Left = Math.Max(0, e.X + pageControl.Left - DragStart.X);
                if (direction != Direction.Horizontal)
                    pageControl.Top = Math.Max(0, e.Y + pageControl.Top - DragStart.Y);

                this.Refresh();

                DrawControlBorder(pageControl);
                pageControl.Invalidate();
            }
        }
        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            this.Refresh();
            Dragging = false;
            pageControl.Capture = false;

            DrawControlBorder(pageControl);
            pageControl.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Get the direction and display correct cursor
            if (SelectedControl != null)
            {
                Point pos = this.PointToClient(MousePosition);
                //check if the mouse cursor is within the drag handle
                if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NW;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SE;
                    Cursor = Cursors.SizeNWSE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y)
                {
                    direction = Direction.N;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width / 2 - DRAG_HANDLE_SIZE / 2) &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width / 2 + DRAG_HANDLE_SIZE / 2 &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE)
                {
                    direction = Direction.S;
                    Cursor = Cursors.SizeNS;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.W;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE &&
                    pos.Y >= SelectedControl.Location.Y + SelectedControl.Height / 2 - DRAG_HANDLE_SIZE / 2 &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height / 2 + DRAG_HANDLE_SIZE / 2))
                {
                    direction = Direction.E;
                    Cursor = Cursors.SizeWE;
                }
                else if ((pos.X >= SelectedControl.Location.X + SelectedControl.Width &&
                    pos.X <= SelectedControl.Location.X + SelectedControl.Width + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y))
                {
                    direction = Direction.NE;
                    Cursor = Cursors.SizeNESW;
                }
                else if ((pos.X >= SelectedControl.Location.X - DRAG_HANDLE_SIZE &&
                    pos.X <= SelectedControl.Location.X + DRAG_HANDLE_SIZE) &&
                    (pos.Y >= SelectedControl.Location.Y + SelectedControl.Height - DRAG_HANDLE_SIZE &&
                    pos.Y <= SelectedControl.Location.Y + SelectedControl.Height + DRAG_HANDLE_SIZE))
                {
                    direction = Direction.SW;
                    Cursor = Cursors.SizeNESW;
                }
                else
                {
                    Cursor = Cursors.Default;
                    direction = Direction.None;
                }
            }
            else
            {
                direction = Direction.None;
                Cursor = Cursors.Default;
            }
            #endregion
        }

        private void timerDisableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRegisterAll();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            isLoad = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (isLoad)
            {
                isLoad = false;
            }
            else
            {
                obj.Size = new Size((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                this.Refresh();
                Dragging = false;
                pageControl.Capture = false;

                DrawControlBorder(pageControl);
                pageControl.Invalidate();
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (isLoad)
            {
                isLoad = false;
            }
            else
            {
                obj.Size = new Size((int)numericUpDown1.Value, (int)numericUpDown2.Value);

                this.Refresh();
                Dragging = false;
                pageControl.Capture = false;

                DrawControlBorder(pageControl);

                pageControl.Invalidate();
            }
        }
        private void AddNewPage()
        {
            obj = new Form1();
            obj.Show();
            obj.ObjHome = this;
            obj.TopLevel = false;

            propertyGrid1.SelectedObject = obj;

            obj.Text = "New Page";
            obj.Location = new Point(124, 185);
            obj.Size = new System.Drawing.Size(300, 120);
            SelectedControl = obj;

            obj.MouseDown += new MouseEventHandler(control_MouseDown);
            obj.Click += obj_Click;
            obj.MouseMove += new MouseEventHandler(control_MouseMove);
            obj.Paint += obj_Paint;
            obj.MouseUp += new MouseEventHandler(control_MouseUp);
            obj.ContextMenuStrip = this.contextMenuStrip1;
            Init(obj, obj, Direction.Any);
            this.Controls.Add(obj);
            obj.ObjHome = this;
            DrawControlBorder(obj);
            obj.Invalidate();
            timer1.Start();
            obj.PG = this.propertyGrid1;
            numericUpDown1.Value = obj.Width;
            numericUpDown2.Value = obj.Height;
            isLoad = true;
        }
        private void newPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                if (obj == null)
            {
                AddNewPage();
            }
            else
            {
                DialogResult d = MessageBox.Show("A new page will remove the current designed page." + Environment.NewLine + "It is recommended to save or export the current page before creating a new blank page." + Environment.NewLine + "Are you sure, you want to create new page?", "Designer Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Controls.Remove(obj);
                    this.Invalidate();

                    AddNewPage();
                }
            }
            strTemplate = "";
        }

        private void lableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.AddLable(this.contextMenuStrip1, propertyGrid1);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.DeleteControl();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.CopyControl();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.PasteControl();
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.GetSelectedControl().BringToFront();
        }

        private void boxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.AddRectangle(contextMenuStrip1, propertyGrid1);
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.GetSelectedControl().SendToBack();
        }

        private void setLeftAlignmentRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignRef();
        }

        private void setLeftAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignment();
        }

        private void setRightAlignmentRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignRef();
        }

        private void setRightAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignment();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignRef();
        }

        private void lAlignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignment();
        }

        private void rRefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignRef();
        }

        private void rAlignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignment();
        }

        private void pictureBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.AddPictureBox(contextMenuStrip1, propertyGrid1);
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.AddLine(contextMenuStrip1, propertyGrid1);
        }

        private void buttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj.AddButton(contextMenuStrip1, propertyGrid1);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void WriteToLog(string s)
        {
            strTemplate = strTemplate + s + Environment.NewLine;
        }
        private void LoadTemplate(bool IsFromFile)
        {
            string strBarCodeSettingValue = "";
            if (IsFromFile)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    strBarCodeSettingValue= File.ReadAllText(openFileDialog.FileName);
                }
            }
            else
            {
                 strBarCodeSettingValue = GetBarCodeSettings();
            }
      
            if (strBarCodeSettingValue != null)
            {
                string[] strfiles = strBarCodeSettingValue.Split('\n');

                if (strfiles.Length > 0)
                {
                    this.Controls.Remove(obj);
                    this.Invalidate();

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

                                    objLable.MouseEnter += new EventHandler(obj.control_MouseEnter);
                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    objLable.MouseDown += new MouseEventHandler(obj.control_MouseDown);
                                    objLable.MouseMove += new MouseEventHandler(obj.control_MouseMove);
                                    objLable.MouseUp += new MouseEventHandler(obj.control_MouseUp);
                                    objLable.Click += obj.ctrl_Click;
                                    objLable.DoubleClick += obj.ctrl_DoubleClick;
                                    objLable.ContextMenuStrip = contextMenuStrip1;
                                    objLable.Click += new EventHandler(obj.ctrl_Click);

                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objLable);

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

                                    objLable.MouseEnter += new EventHandler(obj.control_MouseEnter);
                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    objLable.MouseDown += new MouseEventHandler(obj.control_MouseDown);
                                    objLable.MouseMove += new MouseEventHandler(obj.control_MouseMove);
                                    objLable.MouseUp += new MouseEventHandler(obj.control_MouseUp);
                                    objLable.Click += obj.ctrl_Click;
                                    objLable.DoubleClick += obj.ctrl_DoubleClick;
                                    objLable.ContextMenuStrip = contextMenuStrip1;
                                    objLable.Click += new EventHandler(obj.ctrl_Click);

                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objLable);

                                }
                                else if (strInfo[0] == "PictureBox")
                                {
                                    PictureBox objPicBox = new PictureBox();

                                    objPicBox.Size = new Size(Convert.ToInt32(strInfo[5]), Convert.ToInt32(strInfo[6]));
                                    objPicBox.Location = new Point(Convert.ToInt32(strInfo[7]), Convert.ToInt32(strInfo[8]));
                                    objPicBox.BorderStyle = BorderStyle.FixedSingle;
                                    objPicBox.MouseEnter += new EventHandler(obj.control_MouseEnter);
                                    objPicBox.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    objPicBox.MouseDown += new MouseEventHandler(obj.control_MouseDown);
                                    objPicBox.MouseMove += new MouseEventHandler(obj.control_MouseMove);
                                    objPicBox.MouseUp += new MouseEventHandler(obj.control_MouseUp);
                                    objPicBox.Click += obj.ctrl_Click;
                                    objPicBox.DoubleClick += obj.ctrl_DoubleClick;
                                    objPicBox.ContextMenuStrip = contextMenuStrip1;
                                    objPicBox.Click += new EventHandler(obj.ctrl_Click);
                                    objPicBox.Image = IMS_Client_2.Properties.Resources.barCode;
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
                                    objRec.ContextMenuStrip = contextMenuStrip1;
                                    objRec.Click += new EventHandler(obj.ctrl_Click);

                                    objRec.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objRec);
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

                                    objLable.MouseEnter += new EventHandler(obj.control_MouseEnter);
                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    objLable.MouseDown += new MouseEventHandler(obj.control_MouseDown);
                                    objLable.MouseMove += new MouseEventHandler(obj.control_MouseMove);
                                    objLable.MouseUp += new MouseEventHandler(obj.control_MouseUp);
                                    objLable.Click += obj.ctrl_Click;
                                    objLable.DoubleClick += obj.ctrl_DoubleClick;
                                    objLable.ContextMenuStrip = contextMenuStrip1;
                                    objLable.Click += new EventHandler(obj.ctrl_Click);

                                    objLable.MouseLeave += new EventHandler(obj.control_MouseLeave);
                                    obj.Controls.Add(objLable);
                                }
                            }
                        }
                        this.Focus();
                        this.Activate();

                        clsUtility.ShowInfoMessage("Template Loaded Successfully.", "Designer Tool");
                    }
                }
            }
        }
        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCodeDesigner, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                strTemplate = "";
                //Type-IsBold-Family-argb(int)-fsize(float)-w-h-x-y-text-backColor(int)-RecBorderStyle-borderStyle-borderColor
                WriteToLog(obj.BackColor.ToArgb().ToString() + "@" + obj.Size.Width + "@" + obj.Size.Height);

                for (int i = 0; i < obj.Controls.Count; i++)
                {
                    // if control is not visible then don't save.
                    if (!obj.Controls[i].Visible)
                    {
                        continue;
                    }
                    string type = obj.Controls[i].GetType().Name;
                    bool b = obj.Controls[i].Font.Bold;
                    string family = obj.Controls[i].Font.FontFamily.Name;
                    int arbg = obj.Controls[i].ForeColor.ToArgb();
                    float fsize = obj.Controls[i].Font.Size;
                    int w = obj.Controls[i].Size.Width;
                    int h = obj.Controls[i].Size.Height;
                    int x = obj.Controls[i].Location.X;
                    int y = obj.Controls[i].Location.Y;
                    string strtag = "";
                    if (obj.Controls[i].Tag != null)
                    {
                        strtag = obj.Controls[i].Tag.ToString();
                    }

                    string text = obj.Controls[i].Text;
                    int backColor = obj.Controls[i].BackColor.ToArgb();
                    string RecBorderStyle = "";
                    string borderStyle = "";
                    int borderColor = 0;

                    if (obj.Controls[i].GetType() == typeof(uRectangle))
                    {
                        uRectangle rc = (uRectangle)obj.Controls[i];
                        RecBorderStyle = rc.RectangleBorderStyle.ToString();
                        borderStyle = rc.BorderStyle.ToString();
                        borderColor = rc.BorderColor.ToArgb();
                    }

                    WriteToLog(type + "@" + b + "@" + family + "@" + arbg + "@" + fsize + "@" + w + "@" + h + "@" + x + "@" + y + "@" + text + "@" + backColor + "@" + RecBorderStyle + "@" + borderStyle + "@" + borderColor + "@" + strtag);
                }
                SaveBarCodeSettings(strTemplate);
                clsUtility.ShowInfoMessage("Barcode Template has been saved.", CoreApp.clsUtility.strProjectTitle);
                
            }

        }

        private void loadTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTemplate(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignRef();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obj.SetLeftAlignment();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignRef();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obj.SetTopAlignment();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obj!=null)
            {
                obj.Controls.Clear();
                strTemplate = "";

            }
           
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            obj.AddVerticalLable(contextMenuStrip1, propertyGrid1);
        }

        private void deleteBarcodeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCodeDesigner, clsFormRights.Operation.Delete) || clsUtility.IsAdmin)
            {
                if (CoreApp.clsUtility.ShowQuestionMessage("Are you sure, you want to delete the bar code settings", CoreApp.clsUtility.strProjectTitle))
                {
                    ObjCon.ExecuteNonQuery("UPDATE " + clsUtility.DBName + ".dbo.tblBarCodeSettings SET BarCodeSetting=NULL ");
                    clsUtility.ShowInfoMessage("Barcode setting deleted.", clsUtility.strProjectTitle);
                }
            }


           
        }

        private void cmbProperty_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (tgSelectedControl != null)
            {
                tgSelectedControl.Tag = cmbProperty.Text;
                tgSelectedControl.Text = cmbProperty.Text;
            }
            else
            {
                clsUtility.ShowInfoMessage("Please select a control in design area", clsUtility.strProjectTitle);
            }
        }

        private void exportAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCodeDesigner, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {

                int Width = obj.Size.Width;
                int Height = obj.Size.Height;

                Bitmap bm = new Bitmap(Width, Height);

                obj.DrawToBitmap(bm, new Rectangle(0, 0, Width, Height));

                SaveFileDialog Obj = new SaveFileDialog();
                Obj.Filter = "Png Image File (*.png)|*.png|All files (*.*)|*.*";
                Obj.FileName = "Default Image";
                if (Obj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bm.Save(Obj.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    DialogResult d = MessageBox.Show("Exported Succesfully.Do you want to open?", "Designer Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Obj.FileName);
                    }
                }
            }

        }
        string _exportDatafile = "";
        private void exportAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFormRights.HasFormRight(clsFormRights.Forms.frmBarCodeDesigner, clsFormRights.Operation.Save) || clsUtility.IsAdmin)
            {
                strTemplate = "";
                //Type-IsBold-Family-argb(int)-fsize(float)-w-h-x-y-text-backColor(int)-RecBorderStyle-borderStyle-borderColor
                WriteToLog(obj.BackColor.ToArgb().ToString() + "@" + obj.Size.Width + "@" + obj.Size.Height);

                for (int i = 0; i < obj.Controls.Count; i++)
                {
                    string type = obj.Controls[i].GetType().Name;
                    bool b = obj.Controls[i].Font.Bold;
                    string family = obj.Controls[i].Font.FontFamily.Name;
                    int arbg = obj.Controls[i].ForeColor.ToArgb();
                    float fsize = obj.Controls[i].Font.Size;
                    int w = obj.Controls[i].Size.Width;
                    int h = obj.Controls[i].Size.Height;
                    int x = obj.Controls[i].Location.X;
                    int y = obj.Controls[i].Location.Y;
                    string strtag = "";
                    if (obj.Controls[i].Tag != null)
                    {
                        strtag = obj.Controls[i].Tag.ToString();
                    }

                    string text = obj.Controls[i].Text;
                    int backColor = obj.Controls[i].BackColor.ToArgb();
                    string RecBorderStyle = "";
                    string borderStyle = "";
                    int borderColor = 0;

                    if (obj.Controls[i].GetType() == typeof(uRectangle))
                    {
                        uRectangle rc = (uRectangle)obj.Controls[i];
                        RecBorderStyle = rc.RectangleBorderStyle.ToString();
                        borderStyle = rc.BorderStyle.ToString();
                        borderColor = rc.BorderColor.ToArgb();
                    }

                    WriteToLog(type + "@" + b + "@" + family + "@" + arbg + "@" + fsize + "@" + w + "@" + h + "@" + x + "@" + y + "@" + text + "@" + backColor + "@" + RecBorderStyle + "@" + borderStyle + "@" + borderColor + "@" + strtag);
                }
                SaveFileDialog Obj = new SaveFileDialog();
                Obj.Filter = ".dat Data File (*.dat)|*.dat|All files (*.*)|*.*";
                Obj.FileName = "Template 01";
                if (Obj.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    StreamWriter sw = new StreamWriter(Obj.FileName);
                    sw.WriteLine(strTemplate);
                    sw.Close();
                    MessageBox.Show("Template Saved Successfully.", "Designer Tool");
                }
            }
            
        }

        private void importTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTemplate(true);
        }

        private void lineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            obj.AddLine(contextMenuStrip1, propertyGrid1);
        }
    }
}