namespace IMS_Client_2.Barcode
{
    partial class frmBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBarCode));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPurchaseID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grpGridview = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.dgvProductDetails = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SizeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseInvoiceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrintBarcode = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPurchaseInvoice = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnPrintManualBarcode = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.chkPrintRate = new System.Windows.Forms.CheckBox();
            this.cmbListBox = new System.Windows.Forms.ComboBox();
            this.lblStyleNo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.grpGridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtPurchaseID);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(907, 40);
            this.panel2.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Barcode Printing";
            // 
            // txtPurchaseID
            // 
            this.txtPurchaseID.BackColor = System.Drawing.Color.White;
            this.txtPurchaseID.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseID.Location = new System.Drawing.Point(526, 0);
            this.txtPurchaseID.Name = "txtPurchaseID";
            this.txtPurchaseID.Size = new System.Drawing.Size(50, 25);
            this.txtPurchaseID.TabIndex = 227;
            this.txtPurchaseID.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(398, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 25);
            this.btnSearch.TabIndex = 226;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSearch.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnSearch.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // grpGridview
            // 
            this.grpGridview.BackColor = System.Drawing.Color.Transparent;
            this.grpGridview.Controls.Add(this.chkAll);
            this.grpGridview.Controls.Add(this.dgvProductDetails);
            this.grpGridview.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGridview.Location = new System.Drawing.Point(15, 115);
            this.grpGridview.Name = "grpGridview";
            this.grpGridview.Size = new System.Drawing.Size(880, 381);
            this.grpGridview.TabIndex = 214;
            this.grpGridview.TabStop = false;
            this.grpGridview.Text = "Purchase Invoice Items :";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(82, 30);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 1;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.Visible = false;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // dgvProductDetails
            // 
            this.dgvProductDetails.AllowUserToAddRows = false;
            this.dgvProductDetails.AllowUserToDeleteRows = false;
            this.dgvProductDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dgvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.ColItemName,
            this.colQTY,
            this.Rate,
            this.ColColorID,
            this.ColColor,
            this.colProductID,
            this.ColModelNo,
            this.ColSize,
            this.ColorID,
            this.BarcodeNo,
            this.SizeID,
            this.PurchaseInvoiceID});
            this.dgvProductDetails.Location = new System.Drawing.Point(12, 24);
            this.dgvProductDetails.Name = "dgvProductDetails";
            this.dgvProductDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvProductDetails.Size = new System.Drawing.Size(862, 343);
            this.dgvProductDetails.TabIndex = 0;
            this.dgvProductDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductDetails_DataBindingComplete);
            // 
            // colCheck
            // 
            this.colCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCheck.HeaderText = "";
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 40;
            // 
            // ColItemName
            // 
            this.ColItemName.DataPropertyName = "ProductName";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColItemName.DefaultCellStyle = dataGridViewCellStyle26;
            this.ColItemName.HeaderText = "ItemName";
            this.ColItemName.Name = "ColItemName";
            // 
            // colQTY
            // 
            this.colQTY.DataPropertyName = "QTY";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQTY.DefaultCellStyle = dataGridViewCellStyle27;
            this.colQTY.HeaderText = "QTY";
            this.colQTY.Name = "colQTY";
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            // 
            // ColColorID
            // 
            this.ColColorID.DataPropertyName = "ColorID";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColColorID.DefaultCellStyle = dataGridViewCellStyle28;
            this.ColColorID.HeaderText = "ColorID";
            this.ColColorID.Name = "ColColorID";
            this.ColColorID.Visible = false;
            // 
            // ColColor
            // 
            this.ColColor.DataPropertyName = "Color";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColColor.DefaultCellStyle = dataGridViewCellStyle29;
            this.ColColor.HeaderText = "Color";
            this.ColColor.Name = "ColColor";
            // 
            // colProductID
            // 
            this.colProductID.DataPropertyName = "ProductID";
            this.colProductID.HeaderText = "ProductID";
            this.colProductID.Name = "colProductID";
            this.colProductID.Visible = false;
            // 
            // ColModelNo
            // 
            this.ColModelNo.DataPropertyName = "ModelNo";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColModelNo.DefaultCellStyle = dataGridViewCellStyle30;
            this.ColModelNo.HeaderText = "Style No";
            this.ColModelNo.Name = "ColModelNo";
            // 
            // ColSize
            // 
            this.ColSize.DataPropertyName = "Size";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColSize.DefaultCellStyle = dataGridViewCellStyle31;
            this.ColSize.HeaderText = "Size";
            this.ColSize.Name = "ColSize";
            // 
            // ColorID
            // 
            this.ColorID.DataPropertyName = "ColorID";
            this.ColorID.HeaderText = "ColorID";
            this.ColorID.Name = "ColorID";
            this.ColorID.Visible = false;
            // 
            // BarcodeNo
            // 
            this.BarcodeNo.DataPropertyName = "BarcodeNo";
            this.BarcodeNo.HeaderText = "Barcode";
            this.BarcodeNo.Name = "BarcodeNo";
            // 
            // SizeID
            // 
            this.SizeID.DataPropertyName = "SizeID";
            this.SizeID.HeaderText = "SizeID";
            this.SizeID.Name = "SizeID";
            this.SizeID.Visible = false;
            // 
            // PurchaseInvoiceID
            // 
            this.PurchaseInvoiceID.DataPropertyName = "PurchaseInvoiceID";
            this.PurchaseInvoiceID.HeaderText = "PurchaseInvoiceID";
            this.PurchaseInvoiceID.Name = "PurchaseInvoiceID";
            this.PurchaseInvoiceID.Visible = false;
            // 
            // btnPrintBarcode
            // 
            this.btnPrintBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintBarcode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBarcode.Location = new System.Drawing.Point(762, 87);
            this.btnPrintBarcode.Name = "btnPrintBarcode";
            this.btnPrintBarcode.Size = new System.Drawing.Size(133, 25);
            this.btnPrintBarcode.TabIndex = 218;
            this.btnPrintBarcode.Text = "Print All Barcode";
            this.btnPrintBarcode.UseVisualStyleBackColor = true;
            this.btnPrintBarcode.Click += new System.EventHandler(this.button1_Click);
            this.btnPrintBarcode.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnPrintBarcode.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 225;
            this.label4.Text = "Purchase Invoice No :";
            // 
            // txtPurchaseInvoice
            // 
            this.txtPurchaseInvoice.BackColor = System.Drawing.Color.White;
            this.txtPurchaseInvoice.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseInvoice.Location = new System.Drawing.Point(157, 54);
            this.txtPurchaseInvoice.Name = "txtPurchaseInvoice";
            this.txtPurchaseInvoice.Size = new System.Drawing.Size(150, 25);
            this.txtPurchaseInvoice.TabIndex = 223;
            this.txtPurchaseInvoice.TextChanged += new System.EventHandler(this.txtPurchaseInvoice_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(23, 93);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 19);
            this.linkLabel1.TabIndex = 228;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Refresh Data";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnPrintManualBarcode
            // 
            this.btnPrintManualBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintManualBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintManualBarcode.Enabled = false;
            this.btnPrintManualBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintManualBarcode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintManualBarcode.Location = new System.Drawing.Point(762, 50);
            this.btnPrintManualBarcode.Name = "btnPrintManualBarcode";
            this.btnPrintManualBarcode.Size = new System.Drawing.Size(133, 25);
            this.btnPrintManualBarcode.TabIndex = 229;
            this.btnPrintManualBarcode.Text = "Print Manual QTY";
            this.btnPrintManualBarcode.UseVisualStyleBackColor = true;
            this.btnPrintManualBarcode.Click += new System.EventHandler(this.button1_Click_1);
            this.btnPrintManualBarcode.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnPrintManualBarcode.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(663, 52);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(71, 25);
            this.numericUpDown1.TabIndex = 230;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkPrintRate
            // 
            this.chkPrintRate.AutoSize = true;
            this.chkPrintRate.BackColor = System.Drawing.Color.Transparent;
            this.chkPrintRate.Checked = true;
            this.chkPrintRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintRate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrintRate.Location = new System.Drawing.Point(663, 87);
            this.chkPrintRate.Name = "chkPrintRate";
            this.chkPrintRate.Size = new System.Drawing.Size(87, 21);
            this.chkPrintRate.TabIndex = 231;
            this.chkPrintRate.Text = "Print Rate";
            this.chkPrintRate.UseVisualStyleBackColor = false;
            // 
            // cmbListBox
            // 
            this.cmbListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbListBox.FormattingEnabled = true;
            this.cmbListBox.IntegralHeight = false;
            this.cmbListBox.ItemHeight = 19;
            this.cmbListBox.Location = new System.Drawing.Point(442, 53);
            this.cmbListBox.Name = "cmbListBox";
            this.cmbListBox.Size = new System.Drawing.Size(183, 27);
            this.cmbListBox.TabIndex = 296;
            this.cmbListBox.SelectionChangeCommitted += new System.EventHandler(this.cmbListBox_SelectionChangeCommitted);
            // 
            // lblStyleNo
            // 
            this.lblStyleNo.AutoSize = true;
            this.lblStyleNo.BackColor = System.Drawing.Color.Transparent;
            this.lblStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStyleNo.Location = new System.Drawing.Point(354, 59);
            this.lblStyleNo.Name = "lblStyleNo";
            this.lblStyleNo.Size = new System.Drawing.Size(66, 17);
            this.lblStyleNo.TabIndex = 297;
            this.lblStyleNo.Text = "Style No :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 502);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(862, 173);
            this.dataGridView1.TabIndex = 298;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.BackColor = System.Drawing.Color.Transparent;
            this.rdShowAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdShowAll.Location = new System.Drawing.Point(442, 93);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(79, 21);
            this.rdShowAll.TabIndex = 299;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = false;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // frmBarCode
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(907, 677);
            this.Controls.Add(this.rdShowAll);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblStyleNo);
            this.Controls.Add(this.cmbListBox);
            this.Controls.Add(this.chkPrintRate);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnPrintManualBarcode);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtPurchaseInvoice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPrintBarcode);
            this.Controls.Add(this.grpGridview);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBarCode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bar Code Printing";
            this.Activated += new System.EventHandler(this.frmBarCode_Activated);
            this.Load += new System.EventHandler(this.frmBarCode_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpGridview.ResumeLayout(false);
            this.grpGridview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpGridview;
        private System.Windows.Forms.DataGridView dgvProductDetails;
        private System.Windows.Forms.Button btnPrintBarcode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPurchaseInvoice;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.TextBox txtPurchaseID;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnPrintManualBarcode;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox chkPrintRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColColorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SizeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseInvoiceID;
        private System.Windows.Forms.ComboBox cmbListBox;
        private System.Windows.Forms.Label lblStyleNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rdShowAll;
    }
}