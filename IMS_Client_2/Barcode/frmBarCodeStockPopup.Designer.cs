namespace IMS_Client_2.Barcode
{
    partial class frmBarCodeStockPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBarCodeStockPopup));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.radManual = new System.Windows.Forms.RadioButton();
            this.radQTY = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnPrintManualBarcode = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(362, 40);
            this.panel2.TabIndex = 111;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Bard Code ";
            // 
            // radManual
            // 
            this.radManual.AutoSize = true;
            this.radManual.BackColor = System.Drawing.Color.Transparent;
            this.radManual.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radManual.Location = new System.Drawing.Point(35, 77);
            this.radManual.Name = "radManual";
            this.radManual.Size = new System.Drawing.Size(137, 21);
            this.radManual.TabIndex = 228;
            this.radManual.TabStop = true;
            this.radManual.Text = "Print Manual QTY";
            this.radManual.UseVisualStyleBackColor = false;
            this.radManual.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radQTY
            // 
            this.radQTY.AutoSize = true;
            this.radQTY.BackColor = System.Drawing.Color.Transparent;
            this.radQTY.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radQTY.Location = new System.Drawing.Point(35, 120);
            this.radQTY.Name = "radQTY";
            this.radQTY.Size = new System.Drawing.Size(153, 21);
            this.radQTY.TabIndex = 229;
            this.radQTY.TabStop = true;
            this.radQTY.Text = "Use QTY from Stock";
            this.radQTY.UseVisualStyleBackColor = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(178, 77);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(71, 25);
            this.numericUpDown1.TabIndex = 231;
            // 
            // btnPrintManualBarcode
            // 
            this.btnPrintManualBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintManualBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintManualBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintManualBarcode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintManualBarcode.Location = new System.Drawing.Point(116, 162);
            this.btnPrintManualBarcode.Name = "btnPrintManualBarcode";
            this.btnPrintManualBarcode.Size = new System.Drawing.Size(133, 25);
            this.btnPrintManualBarcode.TabIndex = 232;
            this.btnPrintManualBarcode.Text = "Print Barcode";
            this.btnPrintManualBarcode.UseVisualStyleBackColor = true;
            this.btnPrintManualBarcode.Click += new System.EventHandler(this.btnPrintManualBarcode_Click);
            this.btnPrintManualBarcode.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnPrintManualBarcode.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // frmBarCodeStockPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(362, 222);
            this.Controls.Add(this.btnPrintManualBarcode);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.radQTY);
            this.Controls.Add(this.radManual);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBarCodeStockPopup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bar Code";
            this.Load += new System.EventHandler(this.frmBarCodeStockPopup_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radManual;
        private System.Windows.Forms.RadioButton radQTY;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnPrintManualBarcode;
    }
}