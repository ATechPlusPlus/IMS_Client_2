
namespace IMS_Client_2.Purchase
{
    partial class frmPost_Delivering_Purchase_Bill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPost_Delivering_Purchase_Bill));
            this.label10 = new System.Windows.Forms.Label();
            this.txtPurchaseInvoiceID = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalQTY = new System.Windows.Forms.TextBox();
            this.lblDiffQty = new System.Windows.Forms.Label();
            this.txtDiffQty = new System.Windows.Forms.TextBox();
            this.lblTotalQtyBill = new System.Windows.Forms.Label();
            this.txtTotalQTYBill = new System.Windows.Forms.TextBox();
            this.lblTotalEnteredQty = new System.Windows.Forms.Label();
            this.txtTotalQTYEntered = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpPurchaseBillDetail = new System.Windows.Forms.GroupBox();
            this.cmbListBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.cmbBrand = new System.Windows.Forms.ComboBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblStyleNo = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbStore = new System.Windows.Forms.ComboBox();
            this.lblSupplierBillNo = new System.Windows.Forms.Label();
            this.txtSupplierBillNo = new System.Windows.Forms.TextBox();
            this.dgvQtycolor = new System.Windows.Forms.DataGridView();
            this.cmbSizeType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.PicItem = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.grpPurchaseBillDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtycolor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(335, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 17);
            this.label10.TabIndex = 325;
            this.label10.Text = "*";
            // 
            // txtPurchaseInvoiceID
            // 
            this.txtPurchaseInvoiceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPurchaseInvoiceID.BackColor = System.Drawing.Color.White;
            this.txtPurchaseInvoiceID.Enabled = false;
            this.txtPurchaseInvoiceID.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtPurchaseInvoiceID.Location = new System.Drawing.Point(883, 6);
            this.txtPurchaseInvoiceID.Name = "txtPurchaseInvoiceID";
            this.txtPurchaseInvoiceID.Size = new System.Drawing.Size(44, 25);
            this.txtPurchaseInvoiceID.TabIndex = 324;
            this.txtPurchaseInvoiceID.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtPurchaseInvoiceID);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(978, 40);
            this.panel2.TabIndex = 323;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(249, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Post Delivering Purchase Bill";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(813, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 25);
            this.btnSearch.TabIndex = 310;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 643);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 322;
            this.label4.Text = "Total  QTY :";
            // 
            // txtTotalQTY
            // 
            this.txtTotalQTY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalQTY.BackColor = System.Drawing.Color.White;
            this.txtTotalQTY.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtTotalQTY.Location = new System.Drawing.Point(154, 640);
            this.txtTotalQTY.Name = "txtTotalQTY";
            this.txtTotalQTY.ReadOnly = true;
            this.txtTotalQTY.Size = new System.Drawing.Size(133, 25);
            this.txtTotalQTY.TabIndex = 321;
            this.txtTotalQTY.Text = "0";
            // 
            // lblDiffQty
            // 
            this.lblDiffQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiffQty.AutoSize = true;
            this.lblDiffQty.BackColor = System.Drawing.Color.Transparent;
            this.lblDiffQty.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiffQty.Location = new System.Drawing.Point(3, 592);
            this.lblDiffQty.Name = "lblDiffQty";
            this.lblDiffQty.Size = new System.Drawing.Size(113, 17);
            this.lblDiffQty.TabIndex = 319;
            this.lblDiffQty.Text = "Difference QTY :";
            // 
            // txtDiffQty
            // 
            this.txtDiffQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDiffQty.BackColor = System.Drawing.Color.White;
            this.txtDiffQty.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtDiffQty.Location = new System.Drawing.Point(154, 589);
            this.txtDiffQty.Name = "txtDiffQty";
            this.txtDiffQty.ReadOnly = true;
            this.txtDiffQty.Size = new System.Drawing.Size(133, 25);
            this.txtDiffQty.TabIndex = 318;
            this.txtDiffQty.Text = "0";
            // 
            // lblTotalQtyBill
            // 
            this.lblTotalQtyBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalQtyBill.AutoSize = true;
            this.lblTotalQtyBill.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalQtyBill.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQtyBill.Location = new System.Drawing.Point(3, 547);
            this.lblTotalQtyBill.Name = "lblTotalQtyBill";
            this.lblTotalQtyBill.Size = new System.Drawing.Size(134, 17);
            this.lblTotalQtyBill.TabIndex = 317;
            this.lblTotalQtyBill.Text = "Total  QTY with Bill :";
            // 
            // txtTotalQTYBill
            // 
            this.txtTotalQTYBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalQTYBill.BackColor = System.Drawing.Color.White;
            this.txtTotalQTYBill.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtTotalQTYBill.Location = new System.Drawing.Point(154, 544);
            this.txtTotalQTYBill.Name = "txtTotalQTYBill";
            this.txtTotalQTYBill.ReadOnly = true;
            this.txtTotalQTYBill.Size = new System.Drawing.Size(133, 25);
            this.txtTotalQTYBill.TabIndex = 316;
            this.txtTotalQTYBill.Text = "0";
            // 
            // lblTotalEnteredQty
            // 
            this.lblTotalEnteredQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalEnteredQty.AutoSize = true;
            this.lblTotalEnteredQty.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalEnteredQty.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEnteredQty.Location = new System.Drawing.Point(1, 506);
            this.lblTotalEnteredQty.Name = "lblTotalEnteredQty";
            this.lblTotalEnteredQty.Size = new System.Drawing.Size(130, 17);
            this.lblTotalEnteredQty.TabIndex = 315;
            this.lblTotalEnteredQty.Text = "Total  QTY entered :";
            // 
            // txtTotalQTYEntered
            // 
            this.txtTotalQTYEntered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalQTYEntered.BackColor = System.Drawing.Color.White;
            this.txtTotalQTYEntered.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtTotalQTYEntered.Location = new System.Drawing.Point(154, 502);
            this.txtTotalQTYEntered.Name = "txtTotalQTYEntered";
            this.txtTotalQTYEntered.ReadOnly = true;
            this.txtTotalQTYEntered.Size = new System.Drawing.Size(133, 25);
            this.txtTotalQTYEntered.TabIndex = 314;
            this.txtTotalQTYEntered.Text = "0";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(894, 146);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(71, 25);
            this.btnUpdate.TabIndex = 306;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnUpdate.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(894, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 25);
            this.btnCancel.TabIndex = 308;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // grpPurchaseBillDetail
            // 
            this.grpPurchaseBillDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPurchaseBillDetail.BackColor = System.Drawing.Color.Transparent;
            this.grpPurchaseBillDetail.Controls.Add(this.cmbListBox);
            this.grpPurchaseBillDetail.Controls.Add(this.label5);
            this.grpPurchaseBillDetail.Controls.Add(this.txtItemName);
            this.grpPurchaseBillDetail.Controls.Add(this.cmbCountry);
            this.grpPurchaseBillDetail.Controls.Add(this.label2);
            this.grpPurchaseBillDetail.Controls.Add(this.cmbCategory);
            this.grpPurchaseBillDetail.Controls.Add(this.label1);
            this.grpPurchaseBillDetail.Controls.Add(this.lblBrand);
            this.grpPurchaseBillDetail.Controls.Add(this.cmbBrand);
            this.grpPurchaseBillDetail.Controls.Add(this.lblProductName);
            this.grpPurchaseBillDetail.Controls.Add(this.lblStyleNo);
            this.grpPurchaseBillDetail.Enabled = false;
            this.grpPurchaseBillDetail.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPurchaseBillDetail.Location = new System.Drawing.Point(8, 84);
            this.grpPurchaseBillDetail.Name = "grpPurchaseBillDetail";
            this.grpPurchaseBillDetail.Size = new System.Drawing.Size(882, 136);
            this.grpPurchaseBillDetail.TabIndex = 300;
            this.grpPurchaseBillDetail.TabStop = false;
            this.grpPurchaseBillDetail.Text = "Item Details";
            // 
            // cmbListBox
            // 
            this.cmbListBox.DropDownHeight = 506;
            this.cmbListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbListBox.FormattingEnabled = true;
            this.cmbListBox.IntegralHeight = false;
            this.cmbListBox.ItemHeight = 19;
            this.cmbListBox.Location = new System.Drawing.Point(118, 29);
            this.cmbListBox.Name = "cmbListBox";
            this.cmbListBox.Size = new System.Drawing.Size(206, 27);
            this.cmbListBox.TabIndex = 295;
            this.cmbListBox.SelectionChangeCommitted += new System.EventHandler(this.cmbListBox_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(327, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 17);
            this.label5.TabIndex = 294;
            this.label5.Text = "*";
            // 
            // txtItemName
            // 
            this.txtItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemName.BackColor = System.Drawing.Color.White;
            this.txtItemName.Enabled = false;
            this.txtItemName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtItemName.Location = new System.Drawing.Point(596, 63);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(206, 25);
            this.txtItemName.TabIndex = 3;
            // 
            // cmbCountry
            // 
            this.cmbCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCountry.DropDownHeight = 206;
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.Enabled = false;
            this.cmbCountry.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.IntegralHeight = false;
            this.cmbCountry.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbCountry.Location = new System.Drawing.Point(596, 100);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(206, 27);
            this.cmbCountry.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(455, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 254;
            this.label2.Text = "Country :";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownHeight = 306;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Enabled = false;
            this.cmbCategory.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.IntegralHeight = false;
            this.cmbCategory.Location = new System.Drawing.Point(118, 96);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(206, 27);
            this.cmbCategory.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 252;
            this.label1.Text = "Category :";
            // 
            // lblBrand
            // 
            this.lblBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBrand.AutoSize = true;
            this.lblBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblBrand.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrand.Location = new System.Drawing.Point(455, 28);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(51, 17);
            this.lblBrand.TabIndex = 243;
            this.lblBrand.Text = "Brand :";
            // 
            // cmbBrand
            // 
            this.cmbBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBrand.DropDownHeight = 406;
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.Enabled = false;
            this.cmbBrand.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.IntegralHeight = false;
            this.cmbBrand.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbBrand.Location = new System.Drawing.Point(596, 24);
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(206, 27);
            this.cmbBrand.TabIndex = 2;
            // 
            // lblProductName
            // 
            this.lblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductName.AutoSize = true;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(455, 63);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(82, 17);
            this.lblProductName.TabIndex = 229;
            this.lblProductName.Text = "Item Name :";
            // 
            // lblStyleNo
            // 
            this.lblStyleNo.AutoSize = true;
            this.lblStyleNo.BackColor = System.Drawing.Color.Transparent;
            this.lblStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStyleNo.Location = new System.Drawing.Point(19, 34);
            this.lblStyleNo.Name = "lblStyleNo";
            this.lblStyleNo.Size = new System.Drawing.Size(66, 17);
            this.lblStyleNo.TabIndex = 231;
            this.lblStyleNo.Text = "Style No :";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.BackColor = System.Drawing.Color.Transparent;
            this.lblSupplier.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.Location = new System.Drawing.Point(462, 63);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(46, 17);
            this.lblSupplier.TabIndex = 311;
            this.lblSupplier.Text = "Store :";
            // 
            // cmbStore
            // 
            this.cmbStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStore.DropDownHeight = 206;
            this.cmbStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStore.Enabled = false;
            this.cmbStore.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStore.FormattingEnabled = true;
            this.cmbStore.IntegralHeight = false;
            this.cmbStore.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbStore.Location = new System.Drawing.Point(603, 55);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Size = new System.Drawing.Size(206, 27);
            this.cmbStore.TabIndex = 298;
            // 
            // lblSupplierBillNo
            // 
            this.lblSupplierBillNo.AutoSize = true;
            this.lblSupplierBillNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSupplierBillNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierBillNo.Location = new System.Drawing.Point(12, 55);
            this.lblSupplierBillNo.Name = "lblSupplierBillNo";
            this.lblSupplierBillNo.Size = new System.Drawing.Size(107, 17);
            this.lblSupplierBillNo.TabIndex = 309;
            this.lblSupplierBillNo.Text = "Supplier Bill No :";
            // 
            // txtSupplierBillNo
            // 
            this.txtSupplierBillNo.BackColor = System.Drawing.Color.White;
            this.txtSupplierBillNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSupplierBillNo.Location = new System.Drawing.Point(125, 53);
            this.txtSupplierBillNo.Name = "txtSupplierBillNo";
            this.txtSupplierBillNo.Size = new System.Drawing.Size(207, 25);
            this.txtSupplierBillNo.TabIndex = 297;
            this.txtSupplierBillNo.TextChanged += new System.EventHandler(this.txtSupplierBillNo_TextChanged);
            this.txtSupplierBillNo.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtSupplierBillNo.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // dgvQtycolor
            // 
            this.dgvQtycolor.AllowUserToAddRows = false;
            this.dgvQtycolor.AllowUserToDeleteRows = false;
            this.dgvQtycolor.AllowUserToResizeColumns = false;
            this.dgvQtycolor.AllowUserToResizeRows = false;
            this.dgvQtycolor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQtycolor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvQtycolor.BackgroundColor = System.Drawing.Color.White;
            this.dgvQtycolor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQtycolor.Location = new System.Drawing.Point(4, 260);
            this.dgvQtycolor.Name = "dgvQtycolor";
            this.dgvQtycolor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvQtycolor.Size = new System.Drawing.Size(971, 226);
            this.dgvQtycolor.TabIndex = 313;
            this.dgvQtycolor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQtycolor_CellClick);
            this.dgvQtycolor.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQtycolor_CellEndEdit);
            this.dgvQtycolor.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvQtycolor_CellValidating);
            this.dgvQtycolor.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvQtycolor_ColumnAdded);
            this.dgvQtycolor.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvQtycolor_DataBindingComplete);
            this.dgvQtycolor.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvQtycolor_EditingControlShowing);
            // 
            // cmbSizeType
            // 
            this.cmbSizeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSizeType.DropDownHeight = 206;
            this.cmbSizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSizeType.Enabled = false;
            this.cmbSizeType.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSizeType.FormattingEnabled = true;
            this.cmbSizeType.IntegralHeight = false;
            this.cmbSizeType.Location = new System.Drawing.Point(603, 227);
            this.cmbSizeType.Name = "cmbSizeType";
            this.cmbSizeType.Size = new System.Drawing.Size(206, 27);
            this.cmbSizeType.TabIndex = 301;
            this.cmbSizeType.SelectionChangeCommitted += new System.EventHandler(this.cmbSizeType_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(463, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 312;
            this.label3.Text = "Size Tpye :";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(894, 94);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 25);
            this.btnEdit.TabIndex = 328;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnEdit.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnEdit.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // PicItem
            // 
            this.PicItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PicItem.BackColor = System.Drawing.Color.Transparent;
            this.PicItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicItem.Location = new System.Drawing.Point(757, 494);
            this.PicItem.Name = "PicItem";
            this.PicItem.Size = new System.Drawing.Size(218, 194);
            this.PicItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicItem.TabIndex = 329;
            this.PicItem.TabStop = false;
            // 
            // frmPost_Delivering_Purchase_Bill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(978, 692);
            this.Controls.Add(this.PicItem);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTotalQTY);
            this.Controls.Add(this.lblDiffQty);
            this.Controls.Add(this.txtDiffQty);
            this.Controls.Add(this.lblTotalQtyBill);
            this.Controls.Add(this.txtTotalQTYBill);
            this.Controls.Add(this.lblTotalEnteredQty);
            this.Controls.Add(this.txtTotalQTYEntered);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpPurchaseBillDetail);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cmbStore);
            this.Controls.Add(this.lblSupplierBillNo);
            this.Controls.Add(this.txtSupplierBillNo);
            this.Controls.Add(this.dgvQtycolor);
            this.Controls.Add(this.cmbSizeType);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPost_Delivering_Purchase_Bill";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Post Delivering Purchase Bill";
            this.Load += new System.EventHandler(this.frmPost_Delivering_Purchase_Bill_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpPurchaseBillDetail.ResumeLayout(false);
            this.grpPurchaseBillDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtycolor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPurchaseInvoiceID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalQTY;
        private System.Windows.Forms.Label lblDiffQty;
        private System.Windows.Forms.TextBox txtDiffQty;
        private System.Windows.Forms.Label lblTotalQtyBill;
        private System.Windows.Forms.TextBox txtTotalQTYBill;
        private System.Windows.Forms.Label lblTotalEnteredQty;
        private System.Windows.Forms.TextBox txtTotalQTYEntered;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpPurchaseBillDetail;
        private System.Windows.Forms.ComboBox cmbListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblStyleNo;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbStore;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSupplierBillNo;
        private System.Windows.Forms.TextBox txtSupplierBillNo;
        private System.Windows.Forms.DataGridView dgvQtycolor;
        private System.Windows.Forms.ComboBox cmbSizeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.PictureBox PicItem;
    }
}