namespace IMS_Client_2.Inventory
{
    partial class frmScanInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanInventory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.PicProductMaster = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdFrom = new System.Windows.Forms.ComboBox();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalQTY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvProductDetails = new System.Windows.Forms.DataGridView();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarcodeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adj_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SizeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpSalesDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicProductMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(894, 40);
            this.panel2.TabIndex = 118;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Check Inventory";
            // 
            // PicProductMaster
            // 
            this.PicProductMaster.BackColor = System.Drawing.Color.Transparent;
            this.PicProductMaster.Image = ((System.Drawing.Image)(resources.GetObject("PicProductMaster.Image")));
            this.PicProductMaster.Location = new System.Drawing.Point(721, 46);
            this.PicProductMaster.Name = "PicProductMaster";
            this.PicProductMaster.Size = new System.Drawing.Size(161, 113);
            this.PicProductMaster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicProductMaster.TabIndex = 210;
            this.PicProductMaster.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 212;
            this.label2.Text = "Store :";
            // 
            // cmdFrom
            // 
            this.cmdFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdFrom.Enabled = false;
            this.cmdFrom.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFrom.FormattingEnabled = true;
            this.cmdFrom.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmdFrom.Location = new System.Drawing.Point(103, 87);
            this.cmdFrom.Name = "cmdFrom";
            this.cmdFrom.Size = new System.Drawing.Size(180, 27);
            this.cmdFrom.TabIndex = 211;
            this.cmdFrom.SelectionChangeCommitted += new System.EventHandler(this.cmdFrom_SelectionChangeCommitted);
            // 
            // picProduct
            // 
            this.picProduct.BackColor = System.Drawing.Color.Transparent;
            this.picProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProduct.Location = new System.Drawing.Point(17, 433);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(175, 123);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 265;
            this.picProduct.TabStop = false;
            // 
            // txtValue
            // 
            this.txtValue.BackColor = System.Drawing.Color.White;
            this.txtValue.Enabled = false;
            this.txtValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(748, 433);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(134, 25);
            this.txtValue.TabIndex = 264;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(664, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 17);
            this.label6.TabIndex = 263;
            this.label6.Text = "Toatl Value :";
            // 
            // txtTotalQTY
            // 
            this.txtTotalQTY.BackColor = System.Drawing.Color.White;
            this.txtTotalQTY.Enabled = false;
            this.txtTotalQTY.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQTY.Location = new System.Drawing.Point(748, 470);
            this.txtTotalQTY.Name = "txtTotalQTY";
            this.txtTotalQTY.ReadOnly = true;
            this.txtTotalQTY.Size = new System.Drawing.Size(134, 25);
            this.txtTotalQTY.TabIndex = 262;
            this.txtTotalQTY.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(664, 473);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 261;
            this.label5.Text = "Toatl QTY :";
            this.label5.Visible = false;
            // 
            // dgvProductDetails
            // 
            this.dgvProductDetails.AllowUserToAddRows = false;
            this.dgvProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductName,
            this.BarcodeNo,
            this.Rate,
            this.BillQTY,
            this.StockQTY,
            this.Adj_Amount,
            this.ColorID,
            this.Color,
            this.Size,
            this.SizeID,
            this.Total,
            this.ColDelete});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProductDetails.Location = new System.Drawing.Point(17, 159);
            this.dgvProductDetails.Name = "dgvProductDetails";
            this.dgvProductDetails.Size = new System.Drawing.Size(865, 268);
            this.dgvProductDetails.TabIndex = 260;
            this.dgvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductDetails_CellClick);
            this.dgvProductDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductDetails_DataBindingComplete);
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "ProductID";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "ProductName";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BarcodeNo
            // 
            this.BarcodeNo.DataPropertyName = "BarcodeNo";
            this.BarcodeNo.HeaderText = "BarcodeNo";
            this.BarcodeNo.Name = "BarcodeNo";
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // BillQTY
            // 
            this.BillQTY.DataPropertyName = "BillQTY";
            this.BillQTY.HeaderText = "QTY";
            this.BillQTY.Name = "BillQTY";
            // 
            // StockQTY
            // 
            this.StockQTY.DataPropertyName = "StockQTY";
            this.StockQTY.HeaderText = "StockQTY";
            this.StockQTY.Name = "StockQTY";
            this.StockQTY.Visible = false;
            // 
            // Adj_Amount
            // 
            this.Adj_Amount.DataPropertyName = "Adj_Amount";
            this.Adj_Amount.HeaderText = "Adj_Amount";
            this.Adj_Amount.Name = "Adj_Amount";
            this.Adj_Amount.Visible = false;
            // 
            // ColorID
            // 
            this.ColorID.DataPropertyName = "ColorID";
            this.ColorID.HeaderText = "ColorID";
            this.ColorID.Name = "ColorID";
            this.ColorID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColorID.Visible = false;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";
            // 
            // Size
            // 
            this.Size.DataPropertyName = "Size";
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            // 
            // SizeID
            // 
            this.SizeID.DataPropertyName = "SizeID";
            this.SizeID.HeaderText = "SizeID";
            this.SizeID.Name = "SizeID";
            this.SizeID.Visible = false;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // ColDelete
            // 
            this.ColDelete.DataPropertyName = "Delete";
            this.ColDelete.HeaderText = "Delete";
            this.ColDelete.Name = "ColDelete";
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.Color.White;
            this.txtBarCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Location = new System.Drawing.Point(103, 126);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(180, 25);
            this.txtBarCode.TabIndex = 258;
            this.txtBarCode.Enter += new System.EventHandler(this.txtBarCode_Enter);
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            this.txtBarCode.Leave += new System.EventHandler(this.txtBarCode_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 259;
            this.label4.Text = "BarCode :";
            // 
            // dtpSalesDate
            // 
            this.dtpSalesDate.Enabled = false;
            this.dtpSalesDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSalesDate.Location = new System.Drawing.Point(103, 54);
            this.dtpSalesDate.Name = "dtpSalesDate";
            this.dtpSalesDate.Size = new System.Drawing.Size(181, 25);
            this.dtpSalesDate.TabIndex = 257;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 266;
            this.label1.Text = "Scan Date  :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btncancel);
            this.panel1.Controls.Add(this.btnSaveData);
            this.panel1.Location = new System.Drawing.Point(0, 559);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 40);
            this.panel1.TabIndex = 267;
            // 
            // btncancel
            // 
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btncancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(806, 9);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(76, 25);
            this.btncancel.TabIndex = 251;
            this.btncancel.Text = "Clear";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            this.btncancel.Enter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btncancel.Leave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnSaveData
            // 
            this.btnSaveData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveData.Location = new System.Drawing.Point(724, 9);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(76, 25);
            this.btnSaveData.TabIndex = 250;
            this.btnSaveData.Text = "Save";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            this.btnSaveData.Enter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnSaveData.Leave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // frmScanInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 599);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picProduct);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTotalQTY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvProductDetails);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpSalesDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdFrom);
            this.Controls.Add(this.PicProductMaster);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.Name = "frmScanInventory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Inventory";
            this.Load += new System.EventHandler(this.frmScanInventory_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicProductMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox PicProductMaster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmdFrom;
        private System.Windows.Forms.PictureBox picProduct;
        public System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtTotalQTY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvProductDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarcodeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adj_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn SizeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn ColDelete;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpSalesDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnSaveData;
    }
}