namespace IMS_Client_2.StockManagement
{
    partial class frmStockDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockDetails));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvStockDetails = new System.Windows.Forms.DataGridView();
            this.PicItem = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdSearchByCategory = new System.Windows.Forms.RadioButton();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtSearchByStyleNo = new System.Windows.Forms.TextBox();
            this.rdSearchByStyleNo = new System.Windows.Forms.RadioButton();
            this.txtSearchByBarcode = new System.Windows.Forms.TextBox();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.rdSearchByColor = new System.Windows.Forms.RadioButton();
            this.rdSearchByBarCode = new System.Windows.Forms.RadioButton();
            this.txtSearchByProductName = new System.Windows.Forms.TextBox();
            this.rdSearchByItem = new System.Windows.Forms.RadioButton();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtProductID);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1196, 40);
            this.panel2.TabIndex = 114;
            // 
            // txtProductID
            // 
            this.txtProductID.BackColor = System.Drawing.Color.White;
            this.txtProductID.Location = new System.Drawing.Point(634, 6);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(38, 20);
            this.txtProductID.TabIndex = 235;
            this.txtProductID.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Stock Details";
            // 
            // dgvStockDetails
            // 
            this.dgvStockDetails.AllowUserToAddRows = false;
            this.dgvStockDetails.AllowUserToDeleteRows = false;
            this.dgvStockDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockDetails.Location = new System.Drawing.Point(8, 238);
            this.dgvStockDetails.Name = "dgvStockDetails";
            this.dgvStockDetails.ReadOnly = true;
            this.dgvStockDetails.Size = new System.Drawing.Size(1176, 317);
            this.dgvStockDetails.TabIndex = 208;
            this.dgvStockDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockDetails_CellClick);
            this.dgvStockDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvStockDetails_DataBindingComplete);
            // 
            // PicItem
            // 
            this.PicItem.BackColor = System.Drawing.Color.Transparent;
            this.PicItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicItem.Location = new System.Drawing.Point(966, 47);
            this.PicItem.Name = "PicItem";
            this.PicItem.Size = new System.Drawing.Size(218, 185);
            this.PicItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicItem.TabIndex = 233;
            this.PicItem.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdSearchByCategory);
            this.groupBox1.Controls.Add(this.cmbCategory);
            this.groupBox1.Controls.Add(this.txtSearchByStyleNo);
            this.groupBox1.Controls.Add(this.rdSearchByStyleNo);
            this.groupBox1.Controls.Add(this.txtSearchByBarcode);
            this.groupBox1.Controls.Add(this.cmbColor);
            this.groupBox1.Controls.Add(this.rdSearchByColor);
            this.groupBox1.Controls.Add(this.rdSearchByBarCode);
            this.groupBox1.Controls.Add(this.txtSearchByProductName);
            this.groupBox1.Controls.Add(this.rdSearchByItem);
            this.groupBox1.Controls.Add(this.rdShowAll);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(952, 119);
            this.groupBox1.TabIndex = 232;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // rdSearchByCategory
            // 
            this.rdSearchByCategory.AutoSize = true;
            this.rdSearchByCategory.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByCategory.Location = new System.Drawing.Point(645, 28);
            this.rdSearchByCategory.Name = "rdSearchByCategory";
            this.rdSearchByCategory.Size = new System.Drawing.Size(108, 21);
            this.rdSearchByCategory.TabIndex = 239;
            this.rdSearchByCategory.Text = "By Category :";
            this.rdSearchByCategory.UseVisualStyleBackColor = true;
            this.rdSearchByCategory.CheckedChanged += new System.EventHandler(this.rdSearchByCategory_CheckedChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Enabled = false;
            this.cmbCategory.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(762, 26);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(181, 27);
            this.cmbCategory.TabIndex = 238;
            this.cmbCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbCategory_SelectionChangeCommitted);
            // 
            // txtSearchByStyleNo
            // 
            this.txtSearchByStyleNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByStyleNo.Enabled = false;
            this.txtSearchByStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByStyleNo.Location = new System.Drawing.Point(443, 27);
            this.txtSearchByStyleNo.Name = "txtSearchByStyleNo";
            this.txtSearchByStyleNo.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByStyleNo.TabIndex = 236;
            this.txtSearchByStyleNo.TextChanged += new System.EventHandler(this.txtSearchByStyleNo_TextChanged);
            this.txtSearchByStyleNo.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByStyleNo.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // rdSearchByStyleNo
            // 
            this.rdSearchByStyleNo.AutoSize = true;
            this.rdSearchByStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByStyleNo.Location = new System.Drawing.Point(336, 28);
            this.rdSearchByStyleNo.Name = "rdSearchByStyleNo";
            this.rdSearchByStyleNo.Size = new System.Drawing.Size(105, 21);
            this.rdSearchByStyleNo.TabIndex = 235;
            this.rdSearchByStyleNo.Text = "By Style No :";
            this.rdSearchByStyleNo.UseVisualStyleBackColor = true;
            this.rdSearchByStyleNo.CheckedChanged += new System.EventHandler(this.rdSearchByStyleNo_CheckedChanged);
            // 
            // txtSearchByBarcode
            // 
            this.txtSearchByBarcode.BackColor = System.Drawing.Color.White;
            this.txtSearchByBarcode.Enabled = false;
            this.txtSearchByBarcode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByBarcode.Location = new System.Drawing.Point(127, 74);
            this.txtSearchByBarcode.Name = "txtSearchByBarcode";
            this.txtSearchByBarcode.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByBarcode.TabIndex = 234;
            this.txtSearchByBarcode.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchByBarcode_KeyDown);
            this.txtSearchByBarcode.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // cmbColor
            // 
            this.cmbColor.DropDownHeight = 506;
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.Enabled = false;
            this.cmbColor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.IntegralHeight = false;
            this.cmbColor.Location = new System.Drawing.Point(443, 73);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(181, 27);
            this.cmbColor.TabIndex = 233;
            this.cmbColor.SelectionChangeCommitted += new System.EventHandler(this.cmbColor_SelectionChangeCommitted);
            // 
            // rdSearchByColor
            // 
            this.rdSearchByColor.AutoSize = true;
            this.rdSearchByColor.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByColor.Location = new System.Drawing.Point(336, 76);
            this.rdSearchByColor.Name = "rdSearchByColor";
            this.rdSearchByColor.Size = new System.Drawing.Size(86, 21);
            this.rdSearchByColor.TabIndex = 232;
            this.rdSearchByColor.Text = "By Color :";
            this.rdSearchByColor.UseVisualStyleBackColor = true;
            this.rdSearchByColor.CheckedChanged += new System.EventHandler(this.rdSearchByColor_CheckedChanged);
            // 
            // rdSearchByBarCode
            // 
            this.rdSearchByBarCode.AutoSize = true;
            this.rdSearchByBarCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByBarCode.Location = new System.Drawing.Point(15, 76);
            this.rdSearchByBarCode.Name = "rdSearchByBarCode";
            this.rdSearchByBarCode.Size = new System.Drawing.Size(107, 21);
            this.rdSearchByBarCode.TabIndex = 230;
            this.rdSearchByBarCode.Text = "By BarCode :";
            this.rdSearchByBarCode.UseVisualStyleBackColor = true;
            this.rdSearchByBarCode.CheckedChanged += new System.EventHandler(this.rdSearchByBarCode_CheckedChanged);
            // 
            // txtSearchByProductName
            // 
            this.txtSearchByProductName.BackColor = System.Drawing.Color.White;
            this.txtSearchByProductName.Enabled = false;
            this.txtSearchByProductName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByProductName.Location = new System.Drawing.Point(127, 27);
            this.txtSearchByProductName.Name = "txtSearchByProductName";
            this.txtSearchByProductName.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByProductName.TabIndex = 229;
            this.txtSearchByProductName.TextChanged += new System.EventHandler(this.txtSearchByProductName_TextChanged);
            this.txtSearchByProductName.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByProductName.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // rdSearchByItem
            // 
            this.rdSearchByItem.AutoSize = true;
            this.rdSearchByItem.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByItem.Location = new System.Drawing.Point(15, 28);
            this.rdSearchByItem.Name = "rdSearchByItem";
            this.rdSearchByItem.Size = new System.Drawing.Size(81, 21);
            this.rdSearchByItem.TabIndex = 199;
            this.rdSearchByItem.Text = "By Item :";
            this.rdSearchByItem.UseVisualStyleBackColor = true;
            this.rdSearchByItem.CheckedChanged += new System.EventHandler(this.rdSearchByItem_CheckedChanged);
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdShowAll.Location = new System.Drawing.Point(645, 74);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(79, 21);
            this.rdShowAll.TabIndex = 106;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = true;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // frmStockDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1196, 563);
            this.Controls.Add(this.PicItem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvStockDetails);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmStockDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Details";
            this.Load += new System.EventHandler(this.frmStockDetails_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvStockDetails;
        private System.Windows.Forms.PictureBox PicItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSearchByBarcode;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.RadioButton rdSearchByColor;
        private System.Windows.Forms.RadioButton rdSearchByBarCode;
        private System.Windows.Forms.TextBox txtSearchByProductName;
        private System.Windows.Forms.RadioButton rdSearchByItem;
        private System.Windows.Forms.RadioButton rdShowAll;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.TextBox txtSearchByStyleNo;
        private System.Windows.Forms.RadioButton rdSearchByStyleNo;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.RadioButton rdSearchByCategory;
    }
}