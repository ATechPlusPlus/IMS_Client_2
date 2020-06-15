using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMS_Client_2.Other_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void getallforms()
        {
            richTextBox1.Text = "";
            Type formType = typeof(Form);
            foreach (Type type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
                if (formType.IsAssignableFrom(type))
                {
                    richTextBox1.Text += type.Name+"\n";
                    // type is a Form
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getallforms();
        }
    }
}
