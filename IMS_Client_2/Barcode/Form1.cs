using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Barcode
{
    public partial class Form1 : Form
    {
        const int DRAG_HANDLE_SIZE = 7;
        int mouseX, mouseY;
        Control SelectedControl;
        Direction direction;
        Point newLocation;
        Size newSize;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //start the timer that will be monitoring the mouse movement every 500 ms.
            timer1.Start();
            txtSupplierBillNo.Focus();
        }

        public void control_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
            Cursor = Cursors.SizeAll;
        }
        public void control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Start();
        }
        public void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Invalidate();  //unselect other control
                SelectedControl = (Control)sender;
                Control control = (Control)sender;
                mouseX = -e.X;
                mouseY = -e.Y;
                control.Invalidate();
                DrawControlBorder(sender);
                // propertyGrid1.SelectedObject = SelectedControl;a
            }
        }
        public void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = (Control)sender;
                Point nextPosition = new Point();
                nextPosition = this.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY);
                control.Location = nextPosition;
                Invalidate();
            }
        }
        public void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = (Control)sender;
                Cursor.Clip = System.Drawing.Rectangle.Empty;
                control.Invalidate();
                DrawControlBorder(control);
            }
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
            ControlPaint.DrawBorder(g, Border, Color.Gray, ButtonBorderStyle.Dotted);
            ControlPaint.DrawGrabHandle(g, NW, true, true);
            ControlPaint.DrawGrabHandle(g, N, true, true);
            ControlPaint.DrawGrabHandle(g, NE, true, true);
            ControlPaint.DrawGrabHandle(g, W, true, true);
            ControlPaint.DrawGrabHandle(g, E, true, true);
            ControlPaint.DrawGrabHandle(g, SW, true, true);
            ControlPaint.DrawGrabHandle(g, S, true, true);
            ControlPaint.DrawGrabHandle(g, SE, true, true);
            g.Dispose();
        }

        /// <summary>
        /// Resize the selected control, minimum width and height is 20.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null && e.Button == MouseButtons.Left)
            {
                timer1.Stop();
                Invalidate();

                if (SelectedControl.Height < 20)
                {
                    SelectedControl.Height = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }
                else if (SelectedControl.Width < 20)
                {
                    SelectedControl.Width = 20;
                    direction = Direction.None;
                    Cursor = Cursors.Default;
                    return;
                }
                //get the current mouse position relative the the app
                Point pos = this.PointToClient(MousePosition);
                #region resize the control in 8 directions
                if (direction == Direction.NW)
                {
                    //north west, location and width, height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width - (newLocation.X - SelectedControl.Location.X),
                        SelectedControl.Size.Height - (newLocation.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SE)
                {
                    //south east, width and height change
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Size.Width + (newLocation.X - SelectedControl.Size.Width - SelectedControl.Location.X),
                        SelectedControl.Height + (newLocation.Y - SelectedControl.Height - SelectedControl.Location.Y));
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.N)
                {
                    //north, location and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.S)
                {
                    //south, only the height changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(SelectedControl.Width,
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.W)
                {
                    //west, location and width will change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        SelectedControl.Height);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.E)
                {
                    //east, only width changes
                    newLocation = new Point(pos.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height);
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.SW)
                {
                    //south west, location, width and height change
                    newLocation = new Point(pos.X, SelectedControl.Location.Y);
                    newSize = new Size(SelectedControl.Width - (pos.X - SelectedControl.Location.X),
                        pos.Y - SelectedControl.Location.Y);
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                else if (direction == Direction.NE)
                {
                    //north east, location, width and height change
                    newLocation = new Point(SelectedControl.Location.X, pos.Y);
                    newSize = new Size(pos.X - SelectedControl.Location.X,
                        SelectedControl.Height - (pos.Y - SelectedControl.Location.Y));
                    SelectedControl.Location = newLocation;
                    SelectedControl.Size = newSize;
                }
                #endregion
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedControl != null)
                DrawControlBorder(SelectedControl);
            timer1.Start();
        }
        public frmBarCodeDesigner ObjHome;
        public void RemoveTimerTickEvent()
        {
        }
        public void RegTimerTickEvent()
        {
        }
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Track the mouse movement and display the correct cursor when it's over
        /// one of the drag handle of the selected control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && SelectedControl != null)
            {
                Controls.Remove(SelectedControl);
                //propertyGrid1.SelectedObject = null;
                Invalidate();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Label ctrl = new Label();
            ctrl.Text = "_____";
            ctrl.Location = new Point(50, 50);
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            Controls.Add(ctrl);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PictureBox ctrl = new PictureBox();

            ctrl.Location = new Point(50, 50);
            ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            Controls.Add(ctrl);
        }
        Control ctrCopyControl;
        public void CopyControl()
        {
            ctrCopyControl = SelectedControl;
        }

        public void PasteControl()
        {
            if (ctrCopyControl != null && ctrCopyControl.GetType() == typeof(Label))
            {
                Label ctrl = new Label();
                Point p = this.PointToClient(Cursor.Position);

                ctrl.Text = ctrCopyControl.Text;
                ctrl.Location = new Point(p.X, p.Y);
                ctrl.MouseEnter += new EventHandler(control_MouseEnter);
                ctrl.MouseLeave += new EventHandler(control_MouseLeave);
                ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
                ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
                ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
                ctrl.Click += ctrl_Click;
                ctrl.ContextMenuStrip = ctrCopyControl.ContextMenuStrip;
                ctrl.ForeColor = ctrCopyControl.ForeColor;
                ctrl.BackColor = ctrCopyControl.BackColor;
                ctrl.Font = ctrCopyControl.Font;
                ctrl.DoubleClick += ctrl_DoubleClick;
                this.Controls.Add(ctrl);
            }
        }
        int XRefPoint;
        int YRefPoint;
        public void SetLeftAlignRef()
        {
            XRefPoint = SelectedControl.Location.X;
        }
        public void SetTopAlignRef()
        {
            YRefPoint = SelectedControl.Location.Y;
        }
        public void SetLeftAlignment()
        {
            if (XRefPoint != 0)
            {
                SelectedControl.Location = new Point(XRefPoint, SelectedControl.Location.Y);
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Please set the Left Alignment Ref first.", "Designer Tool");
            }
        }
        public void SetTopAlignment()
        {
            if (YRefPoint != 0)
            {
                SelectedControl.Location = new Point(SelectedControl.Location.X, YRefPoint);
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Please set the Top Alignment Ref first.", "Designer Tool");
            }
        }
        public void ClearSelectedControl()
        {
            SelectedControl = null;
        }
        public Control GetSelectedControl()
        {
            return SelectedControl;
        }
        public void DeleteControl()
        {
            this.Controls.Remove(SelectedControl);
            this.Refresh();
            this.Invalidate();
        }
        public PropertyGrid PG;
        public void AddLable(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            Label ctrl = new Label();
            ctrl.Text = "New Lable";
            ctrl.Font = new System.Drawing.Font("Times new Roman", 11.2F, FontStyle.Regular);
            ctrl.Location = new Point(86, 3);
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += ctrl_Click;
            ctrl.DoubleClick += ctrl_DoubleClick;
            ctrl.ContextMenuStrip = cntx;

            this.Controls.Add(ctrl);
        }
        public void AddVerticalLable(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            IMS_Client_2.Barcode.VerticalLabel ctrl = new IMS_Client_2.Barcode.VerticalLabel();
            ctrl.Text = "Vertical Lable";
            ctrl.Size = new Size(21, 96);
            ctrl.Location = new Point(15, 13);
            ctrl.Font = new System.Drawing.Font("Times new Roman", 11.2F, FontStyle.Regular);

            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += ctrl_Click;
            ctrl.DoubleClick += ctrl_DoubleClick;
            ctrl.ContextMenuStrip = cntx;
            this.Controls.Add(ctrl);
        }

        public void ctrl_DoubleClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            richTextBox1.Text = lbl.Text;
            pnlPopup.Location = new Point(lbl.Location.X, lbl.Location.Y + 20);
            pnlPopup.Visible = true;
            pnlPopup.BringToFront();
        }
        public void AddPictureBox(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            PictureBox ctrl = new PictureBox();

            ctrl.Location = new Point(43, 26);
            ctrl.Size = new System.Drawing.Size(180, 60);
            ctrl.BorderStyle = BorderStyle.FixedSingle;
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Image = IMS_Client_2.Properties.Resources.barCode;
            ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
            ctrl.Click += ctrl_Click;
            ctrl.ContextMenuStrip = cntx;
            this.Controls.Add(ctrl);
        }
        public void AddLine(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            Line ctrl = new Line();

            ctrl.Location = new Point(50, 50);
            ctrl.Size = new System.Drawing.Size(50, 1);

            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += ctrl_Click;
            ctrl.ContextMenuStrip = cntx;
            this.Controls.Add(ctrl);
        }
        public void AddButton(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            Button ctrl = new Button();
            ctrl.Text = "New Button";
            ctrl.Location = new Point(50, 50);
            ctrl.Size = new System.Drawing.Size(100, 50);

            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += ctrl_Click;
            ctrl.ContextMenuStrip = cntx;
            this.Controls.Add(ctrl);
        }
        public void AddRectangle(ContextMenuStrip cntx, PropertyGrid p)
        {
            PG = p;
            uRectangle ctrl = new uRectangle();

            ctrl.Location = new Point(50, 50);
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            ctrl.Click += ctrl_Click;
            ctrl.ContextMenuStrip = cntx;
            this.Controls.Add(ctrl);
        }
        public void ctrl_Click(object sender, EventArgs e)
        {
            ObjHome.UnRegisterAll(sender);
            PG.SelectedObject = sender;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Button ctrl = new Button();
            ctrl.Text = "New Button";
            ctrl.Location = new Point(50, 50);
            ctrl.MouseEnter += new EventHandler(control_MouseEnter);
            ctrl.MouseLeave += new EventHandler(control_MouseLeave);
            ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
            ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
            ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            this.Controls.Add(ctrl);
        }

        enum Direction
        {
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

        private void button2_Click(object sender, EventArgs e)
        {
            decimal number3 = 0;
            bool canConvert = false;
            try
            {
                canConvert = decimal.TryParse(txtSupplierBillNo.Text, out number3);
                if (canConvert == true)
                    MessageBox.Show("number3 now " + number3);
                else
                    MessageBox.Show("number3 is not a valid decimal");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "false");

            }
            txtSupplierBillNo.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtSupplierBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsDecimal(txtSupplierBillNo, e);
            //if (e.Handled == true)
            //{
            //    CoreApp.clsUtility.ShowInfoMessage("Enter Only Numbers...", CoreApp.clsUtility.strProjectTitle);
            //}

            //e.Handled = IsAlphaNumeric(e);
            if (e.Handled == true)
            {
                CoreApp.clsUtility.ShowInfoMessage("Enter Only Charactor or Number...", CoreApp.clsUtility.strProjectTitle);
            }
        }

        public bool IsDecimal(Control c, KeyPressEventArgs e)
        {
            bool b = false;
            try
            {
                label1.Text = Convert.ToInt32(e.KeyChar).ToString();
                TextBox txt = (TextBox)c;
                if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar == 46)
                {
                    if ( (txt.SelectionStart == 0 && e.KeyChar != 46 && !txt.Text.Contains(".") ) || e.KeyChar == 8)
                        b = false;
                    else
                        b = true;

                    if (txt.SelectionStart >= 1 && e.KeyChar == 46 && !txt.Text.Contains("."))
                        b = false;

                    else if (txt.SelectionStart >= 1 && e.KeyChar != 46)
                        b = false;
                }
                else
                {
                    b = true;
                }
                return b;
            }
            catch (Exception)
            {
                return true;
            }
        }

        private bool IsAlphaNumeric(KeyPressEventArgs e)
        {
            CoreApp.clsUtility ObjUtil = new CoreApp.clsUtility();
            try
            {
                label1.Text = Convert.ToInt32(e.KeyChar).ToString();
                if (ObjUtil.IsString(e) == false || ObjUtil.IsNumeric(e) == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SelectedControl.Text = richTextBox1.Text;
            pnlPopup.Visible = false;
        }
    }
}
