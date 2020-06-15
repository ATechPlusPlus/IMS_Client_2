using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Barcode
{
    public partial class uRectangle : UserControl
    {
        public uRectangle()
        {
            InitializeComponent();
        }
       
        private void uRectangle_Load(object sender, EventArgs e)
        {
            BorderColor = Color.Black;
            RectangleBorderStyle = ButtonBorderStyle.Solid;
        }
        public Color BorderColor { get; set; }
        public ButtonBorderStyle RectangleBorderStyle { get; set; }
        private void uRectangle_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //draw the border and drag handles
            ControlPaint.DrawBorder(g, this.ClientRectangle, BorderColor, RectangleBorderStyle);
        }

        private void uRectangle_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
