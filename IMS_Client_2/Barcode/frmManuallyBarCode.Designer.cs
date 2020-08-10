namespace IMS_Client_2.Barcode
{
    partial class frmManuallyBarCode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManuallyBarCode));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.lblStyleNo = new System.Windows.Forms.Label();
            this.cmbListBox = new System.Windows.Forms.ComboBox();
            this.txtPurchaseInvoice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrintManualBarcode = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtPurchaseID = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtPurchaseID);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(834, 40);
            this.panel2.TabIndex = 275;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(247, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Manually BarCode Generate";
            // 
            // lblStyleNo
            // 
            this.lblStyleNo.AutoSize = true;
            this.lblStyleNo.BackColor = System.Drawing.Color.Transparent;
            this.lblStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStyleNo.Location = new System.Drawing.Point(372, 63);
            this.lblStyleNo.Name = "lblStyleNo";
            this.lblStyleNo.Size = new System.Drawing.Size(66, 17);
            this.lblStyleNo.TabIndex = 301;
            this.lblStyleNo.Text = "Style No :";
            this.lblStyleNo.Visible = false;
            // 
            // cmbListBox
            // 
            this.cmbListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbListBox.FormattingEnabled = true;
            this.cmbListBox.IntegralHeight = false;
            this.cmbListBox.ItemHeight = 19;
            this.cmbListBox.Location = new System.Drawing.Point(460, 57);
            this.cmbListBox.Name = "cmbListBox";
            this.cmbListBox.Size = new System.Drawing.Size(183, 27);
            this.cmbListBox.TabIndex = 300;
            this.cmbListBox.Visible = false;
            this.cmbListBox.SelectionChangeCommitted += new System.EventHandler(this.cmbListBox_SelectionChangeCommitted);
            // 
            // txtPurchaseInvoice
            // 
            this.txtPurchaseInvoice.BackColor = System.Drawing.Color.White;
            this.txtPurchaseInvoice.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseInvoice.Location = new System.Drawing.Point(160, 58);
            this.txtPurchaseInvoice.Name = "txtPurchaseInvoice";
            this.txtPurchaseInvoice.Size = new System.Drawing.Size(189, 25);
            this.txtPurchaseInvoice.TabIndex = 298;
            this.txtPurchaseInvoice.TextChanged += new System.EventHandler(this.txtPurchaseInvoice_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 299;
            this.label4.Text = "Purchase Invoice No :";
            // 
            // btnPrintManualBarcode
            // 
            this.btnPrintManualBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintManualBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintManualBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintManualBarcode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintManualBarcode.Location = new System.Drawing.Point(659, 59);
            this.btnPrintManualBarcode.Name = "btnPrintManualBarcode";
            this.btnPrintManualBarcode.Size = new System.Drawing.Size(133, 25);
            this.btnPrintManualBarcode.TabIndex = 302;
            this.btnPrintManualBarcode.Text = "BarCode Generate";
            this.btnPrintManualBarcode.UseVisualStyleBackColor = true;
            this.btnPrintManualBarcode.Click += new System.EventHandler(this.btnPrintManualBarcode_Click);
            this.btnPrintManualBarcode.MouseEnter += new System.EventHandler(this.btnPrintManualBarcode_MouseEnter);
            this.btnPrintManualBarcode.MouseLeave += new System.EventHandler(this.btnPrintManualBarcode_MouseLeave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(822, 327);
            this.dataGridView1.TabIndex = 303;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // txtPurchaseID
            // 
            this.txtPurchaseID.BackColor = System.Drawing.Color.White;
            this.txtPurchaseID.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseID.Location = new System.Drawing.Point(449, 9);
            this.txtPurchaseID.Name = "txtPurchaseID";
            this.txtPurchaseID.Size = new System.Drawing.Size(50, 25);
            this.txtPurchaseID.TabIndex = 304;
            this.txtPurchaseID.Visible = false;
            // 
            // frmManuallyBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(834, 436);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPrintManualBarcode);
            this.Controls.Add(this.lblStyleNo);
            this.Controls.Add(this.cmbListBox);
            this.Controls.Add(this.txtPurchaseInvoice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmManuallyBarCode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manually BarCode Generate";
            this.Load += new System.EventHandler(this.frmManuallyBarCode_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblStyleNo;
        private System.Windows.Forms.ComboBox cmbListBox;
        private System.Windows.Forms.TextBox txtPurchaseInvoice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPrintManualBarcode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtPurchaseID;
    }
}