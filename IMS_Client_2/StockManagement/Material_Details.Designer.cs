namespace IMS_Client_2.StockManagement
{
    partial class Material_Details
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Material_Details));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearchByStyleNo = new System.Windows.Forms.TextBox();
            this.rdSearchByStyleNo = new System.Windows.Forms.RadioButton();
            this.txtSearchByBarcode = new System.Windows.Forms.TextBox();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.rdSearchByColor = new System.Windows.Forms.RadioButton();
            this.rdSearchByBarCode = new System.Windows.Forms.RadioButton();
            this.txtSearchByProductName = new System.Windows.Forms.TextBox();
            this.rdSearchByItem = new System.Windows.Forms.RadioButton();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.rdSearchByStore = new System.Windows.Forms.RadioButton();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.dgvProductDetails = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printBarcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PicItem = new System.Windows.Forms.PictureBox();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtProductID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1129, 40);
            this.panel2.TabIndex = 110;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Material Details";
            // 
            // txtProductID
            // 
            this.txtProductID.BackColor = System.Drawing.Color.White;
            this.txtProductID.Location = new System.Drawing.Point(750, 9);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(38, 20);
            this.txtProductID.TabIndex = 230;
            this.txtProductID.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtSearchByStyleNo);
            this.groupBox1.Controls.Add(this.rdSearchByStyleNo);
            this.groupBox1.Controls.Add(this.txtSearchByBarcode);
            this.groupBox1.Controls.Add(this.cmbColor);
            this.groupBox1.Controls.Add(this.rdSearchByColor);
            this.groupBox1.Controls.Add(this.rdSearchByBarCode);
            this.groupBox1.Controls.Add(this.txtSearchByProductName);
            this.groupBox1.Controls.Add(this.rdSearchByItem);
            this.groupBox1.Controls.Add(this.cmbShop);
            this.groupBox1.Controls.Add(this.rdSearchByStore);
            this.groupBox1.Controls.Add(this.rdShowAll);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(952, 120);
            this.groupBox1.TabIndex = 206;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // txtSearchByStyleNo
            // 
            this.txtSearchByStyleNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByStyleNo.Enabled = false;
            this.txtSearchByStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByStyleNo.Location = new System.Drawing.Point(762, 24);
            this.txtSearchByStyleNo.Name = "txtSearchByStyleNo";
            this.txtSearchByStyleNo.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByStyleNo.TabIndex = 237;
            this.txtSearchByStyleNo.TextChanged += new System.EventHandler(this.txtSearchByStyleNo_TextChanged);
            this.txtSearchByStyleNo.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByStyleNo.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // rdSearchByStyleNo
            // 
            this.rdSearchByStyleNo.AutoSize = true;
            this.rdSearchByStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByStyleNo.Location = new System.Drawing.Point(656, 24);
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
            this.txtSearchByBarcode.Location = new System.Drawing.Point(159, 73);
            this.txtSearchByBarcode.Name = "txtSearchByBarcode";
            this.txtSearchByBarcode.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByBarcode.TabIndex = 234;
            this.txtSearchByBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchByBarcode_KeyDown);
            this.txtSearchByBarcode.MouseEnter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByBarcode.MouseLeave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // cmbColor
            // 
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.Enabled = false;
            this.cmbColor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(452, 69);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(181, 27);
            this.cmbColor.TabIndex = 233;
            this.cmbColor.SelectionChangeCommitted += new System.EventHandler(this.cmbColor_SelectionChangeCommitted);
            // 
            // rdSearchByColor
            // 
            this.rdSearchByColor.AutoSize = true;
            this.rdSearchByColor.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByColor.Location = new System.Drawing.Point(365, 71);
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
            this.rdSearchByBarCode.Location = new System.Drawing.Point(17, 71);
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
            this.txtSearchByProductName.Location = new System.Drawing.Point(452, 24);
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
            this.rdSearchByItem.Location = new System.Drawing.Point(365, 24);
            this.rdSearchByItem.Name = "rdSearchByItem";
            this.rdSearchByItem.Size = new System.Drawing.Size(81, 21);
            this.rdSearchByItem.TabIndex = 199;
            this.rdSearchByItem.Text = "By Item :";
            this.rdSearchByItem.UseVisualStyleBackColor = true;
            this.rdSearchByItem.CheckedChanged += new System.EventHandler(this.rdSearchByItem_CheckedChanged);
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.Enabled = false;
            this.cmbShop.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(159, 24);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(181, 27);
            this.cmbShop.TabIndex = 198;
            this.cmbShop.SelectionChangeCommitted += new System.EventHandler(this.cmbShop_SelectionChangeCommitted);
            // 
            // rdSearchByStore
            // 
            this.rdSearchByStore.AutoSize = true;
            this.rdSearchByStore.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByStore.Location = new System.Drawing.Point(17, 27);
            this.rdSearchByStore.Name = "rdSearchByStore";
            this.rdSearchByStore.Size = new System.Drawing.Size(125, 21);
            this.rdSearchByStore.TabIndex = 197;
            this.rdSearchByStore.Text = "By Store/Shope :";
            this.rdSearchByStore.UseVisualStyleBackColor = true;
            this.rdSearchByStore.CheckedChanged += new System.EventHandler(this.rdSearchByStore_CheckedChanged);
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.Checked = true;
            this.rdShowAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdShowAll.Location = new System.Drawing.Point(656, 74);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(79, 21);
            this.rdShowAll.TabIndex = 106;
            this.rdShowAll.TabStop = true;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = true;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // dgvProductDetails
            // 
            this.dgvProductDetails.AllowUserToAddRows = false;
            this.dgvProductDetails.AllowUserToDeleteRows = false;
            this.dgvProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductDetails.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvProductDetails.Location = new System.Drawing.Point(12, 173);
            this.dgvProductDetails.Name = "dgvProductDetails";
            this.dgvProductDetails.ReadOnly = true;
            this.dgvProductDetails.Size = new System.Drawing.Size(1105, 271);
            this.dgvProductDetails.TabIndex = 207;
            this.dgvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductDetails_CellClick);
            this.dgvProductDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductDetails_DataBindingComplete);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printBarcodeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 26);
            // 
            // printBarcodeToolStripMenuItem
            // 
            this.printBarcodeToolStripMenuItem.Name = "printBarcodeToolStripMenuItem";
            this.printBarcodeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.printBarcodeToolStripMenuItem.Text = "Print Barcode";
            this.printBarcodeToolStripMenuItem.Click += new System.EventHandler(this.printBarcodeToolStripMenuItem_Click);
            // 
            // PicItem
            // 
            this.PicItem.BackColor = System.Drawing.Color.Transparent;
            this.PicItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicItem.Location = new System.Drawing.Point(970, 46);
            this.PicItem.Name = "PicItem";
            this.PicItem.Size = new System.Drawing.Size(147, 120);
            this.PicItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicItem.TabIndex = 231;
            this.PicItem.TabStop = false;
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRecords.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(9, 447);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(121, 17);
            this.lblTotalRecords.TabIndex = 232;
            this.lblTotalRecords.Text = "Total Records : 0";
            // 
            // Material_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1129, 464);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.PicItem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvProductDetails);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Material_Details";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Details";
            this.Load += new System.EventHandler(this.Material_Details_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.RadioButton rdSearchByStore;
        private System.Windows.Forms.RadioButton rdShowAll;
        private System.Windows.Forms.DataGridView dgvProductDetails;
        private System.Windows.Forms.RadioButton rdSearchByItem;
        private System.Windows.Forms.TextBox txtSearchByProductName;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.TextBox txtSearchByBarcode;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.RadioButton rdSearchByColor;
        private System.Windows.Forms.RadioButton rdSearchByBarCode;
        private System.Windows.Forms.PictureBox PicItem;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.RadioButton rdSearchByStyleNo;
        private System.Windows.Forms.TextBox txtSearchByStyleNo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printBarcodeToolStripMenuItem;
    }
}