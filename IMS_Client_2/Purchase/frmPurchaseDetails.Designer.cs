namespace IMS_Client_2.Purchase
{
    partial class frmPurchaseDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseDetails));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSupplierBillNo = new System.Windows.Forms.Label();
            this.txtSupplierBillNo = new System.Windows.Forms.TextBox();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.txtPurchaseInvoiceID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvPurchaseInvoice = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPurchaseInvoiceDetail = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvPurchaseItem = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoice)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoiceDetail)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItem)).BeginInit();
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
            this.panel2.Size = new System.Drawing.Size(1017, 40);
            this.panel2.TabIndex = 112;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(148, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Purchase Details";
            // 
            // lblSupplierBillNo
            // 
            this.lblSupplierBillNo.AutoSize = true;
            this.lblSupplierBillNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSupplierBillNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierBillNo.Location = new System.Drawing.Point(12, 61);
            this.lblSupplierBillNo.Name = "lblSupplierBillNo";
            this.lblSupplierBillNo.Size = new System.Drawing.Size(107, 17);
            this.lblSupplierBillNo.TabIndex = 249;
            this.lblSupplierBillNo.Text = "Supplier Bill No :";
            // 
            // txtSupplierBillNo
            // 
            this.txtSupplierBillNo.BackColor = System.Drawing.Color.White;
            this.txtSupplierBillNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSupplierBillNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSupplierBillNo.Location = new System.Drawing.Point(125, 59);
            this.txtSupplierBillNo.Name = "txtSupplierBillNo";
            this.txtSupplierBillNo.Size = new System.Drawing.Size(207, 25);
            this.txtSupplierBillNo.TabIndex = 247;
            this.txtSupplierBillNo.TextChanged += new System.EventHandler(this.txtSupplierBillNo_TextChanged);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnViewDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnViewDetails.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewDetails.Location = new System.Drawing.Point(338, 59);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(93, 25);
            this.btnViewDetails.TabIndex = 248;
            this.btnViewDetails.Text = "View Detail";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // txtPurchaseInvoiceID
            // 
            this.txtPurchaseInvoiceID.BackColor = System.Drawing.Color.White;
            this.txtPurchaseInvoiceID.Enabled = false;
            this.txtPurchaseInvoiceID.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtPurchaseInvoiceID.Location = new System.Drawing.Point(536, 53);
            this.txtPurchaseInvoiceID.Name = "txtPurchaseInvoiceID";
            this.txtPurchaseInvoiceID.Size = new System.Drawing.Size(50, 25);
            this.txtPurchaseInvoiceID.TabIndex = 250;
            this.txtPurchaseInvoiceID.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.dgvPurchaseInvoice);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(990, 114);
            this.groupBox1.TabIndex = 251;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PurchaseInvoice";
            // 
            // dgvPurchaseInvoice
            // 
            this.dgvPurchaseInvoice.AllowUserToAddRows = false;
            this.dgvPurchaseInvoice.AllowUserToDeleteRows = false;
            this.dgvPurchaseInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPurchaseInvoice.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseInvoice.Location = new System.Drawing.Point(6, 24);
            this.dgvPurchaseInvoice.Name = "dgvPurchaseInvoice";
            this.dgvPurchaseInvoice.ReadOnly = true;
            this.dgvPurchaseInvoice.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPurchaseInvoice.Size = new System.Drawing.Size(978, 84);
            this.dgvPurchaseInvoice.TabIndex = 266;
            this.dgvPurchaseInvoice.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPurchaseInvoice_DataBindingComplete);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dgvPurchaseInvoiceDetail);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(993, 103);
            this.groupBox2.TabIndex = 252;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Purchase Invoice Details";
            // 
            // dgvPurchaseInvoiceDetail
            // 
            this.dgvPurchaseInvoiceDetail.AllowUserToAddRows = false;
            this.dgvPurchaseInvoiceDetail.AllowUserToDeleteRows = false;
            this.dgvPurchaseInvoiceDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPurchaseInvoiceDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseInvoiceDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseInvoiceDetail.Location = new System.Drawing.Point(9, 15);
            this.dgvPurchaseInvoiceDetail.Name = "dgvPurchaseInvoiceDetail";
            this.dgvPurchaseInvoiceDetail.ReadOnly = true;
            this.dgvPurchaseInvoiceDetail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPurchaseInvoiceDetail.Size = new System.Drawing.Size(978, 82);
            this.dgvPurchaseInvoiceDetail.TabIndex = 267;
            this.dgvPurchaseInvoiceDetail.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPurchaseInvoiceDetail_DataBindingComplete);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.dgvPurchaseItem);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 349);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(993, 233);
            this.groupBox3.TabIndex = 253;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Purchase Item Details";
            // 
            // dgvPurchaseItem
            // 
            this.dgvPurchaseItem.AllowUserToAddRows = false;
            this.dgvPurchaseItem.AllowUserToDeleteRows = false;
            this.dgvPurchaseItem.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseItem.Location = new System.Drawing.Point(3, 21);
            this.dgvPurchaseItem.Name = "dgvPurchaseItem";
            this.dgvPurchaseItem.ReadOnly = true;
            this.dgvPurchaseItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPurchaseItem.Size = new System.Drawing.Size(987, 209);
            this.dgvPurchaseItem.TabIndex = 268;
            this.dgvPurchaseItem.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPurchaseItem_DataBindingComplete);
            // 
            // frmPurchaseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1017, 589);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPurchaseInvoiceID);
            this.Controls.Add(this.lblSupplierBillNo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtSupplierBillNo);
            this.Controls.Add(this.btnViewDetails);
            this.MaximizeBox = false;
            this.Name = "frmPurchaseDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPurchaseDetails_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoice)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseInvoiceDetail)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSupplierBillNo;
        private System.Windows.Forms.TextBox txtSupplierBillNo;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.TextBox txtPurchaseInvoiceID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvPurchaseInvoice;
        private System.Windows.Forms.DataGridView dgvPurchaseInvoiceDetail;
        private System.Windows.Forms.DataGridView dgvPurchaseItem;
    }
}