namespace IMS_Client_2.Purchase
{
    partial class Purchase_Invoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Purchase_Invoice));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.rdSearchByShipment = new System.Windows.Forms.RadioButton();
            this.txtSearchByShipmentNo = new System.Windows.Forms.TextBox();
            this.grpGridview = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.grpPurchaseInvoice = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSupplierPopup = new System.Windows.Forms.Button();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.txtTotalQTY = new System.Windows.Forms.TextBox();
            this.lblBillValue = new System.Windows.Forms.Label();
            this.txtBillValue = new System.Windows.Forms.TextBox();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.lblShipmentNo = new System.Windows.Forms.Label();
            this.txtShipmentNo = new System.Windows.Forms.TextBox();
            this.lblSupplierBillNo = new System.Windows.Forms.Label();
            this.txtSupplierBillNo = new System.Windows.Forms.TextBox();
            this.grpCurrencyRate = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.txtCurrencyRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpForeignCurrency = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtForeignExp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtForeignDiscount = new System.Windows.Forms.TextBox();
            this.txtNetValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpLocalCurrency = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLocalValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLocalExp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLocalBillValue = new System.Windows.Forms.TextBox();
            this.txtNewPriceRate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpGridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpPurchaseInvoice.SuspendLayout();
            this.grpCurrencyRate.SuspendLayout();
            this.grpForeignCurrency.SuspendLayout();
            this.grpLocalCurrency.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(978, 40);
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
            this.label12.Size = new System.Drawing.Size(150, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Purchase Invoice";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(373, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(71, 25);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnUpdate.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(537, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(455, 57);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 25);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnDelete.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(293, 57);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 25);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnEdit.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnEdit.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(211, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(122, 57);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 25);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdShowAll);
            this.groupBox1.Controls.Add(this.rdSearchByShipment);
            this.groupBox1.Controls.Add(this.txtSearchByShipmentNo);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 399);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(951, 51);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.Checked = true;
            this.rdShowAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdShowAll.Location = new System.Drawing.Point(474, 19);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(79, 21);
            this.rdShowAll.TabIndex = 2;
            this.rdShowAll.TabStop = true;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = true;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // rdSearchByShipment
            // 
            this.rdSearchByShipment.AutoSize = true;
            this.rdSearchByShipment.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByShipment.Location = new System.Drawing.Point(18, 20);
            this.rdSearchByShipment.Name = "rdSearchByShipment";
            this.rdSearchByShipment.Size = new System.Drawing.Size(130, 21);
            this.rdSearchByShipment.TabIndex = 0;
            this.rdSearchByShipment.Text = "By Shipment No :";
            this.rdSearchByShipment.UseVisualStyleBackColor = true;
            this.rdSearchByShipment.CheckedChanged += new System.EventHandler(this.rdSearchByShipment_CheckedChanged);
            // 
            // txtSearchByShipmentNo
            // 
            this.txtSearchByShipmentNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByShipmentNo.Enabled = false;
            this.txtSearchByShipmentNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByShipmentNo.Location = new System.Drawing.Point(157, 17);
            this.txtSearchByShipmentNo.Name = "txtSearchByShipmentNo";
            this.txtSearchByShipmentNo.Size = new System.Drawing.Size(183, 25);
            this.txtSearchByShipmentNo.TabIndex = 1;
            this.txtSearchByShipmentNo.TextChanged += new System.EventHandler(this.txtSearchByShipmentNo_TextChanged);
            this.txtSearchByShipmentNo.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtSearchByShipmentNo.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // grpGridview
            // 
            this.grpGridview.BackColor = System.Drawing.Color.Transparent;
            this.grpGridview.Controls.Add(this.dataGridView1);
            this.grpGridview.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGridview.Location = new System.Drawing.Point(15, 455);
            this.grpGridview.Name = "grpGridview";
            this.grpGridview.Size = new System.Drawing.Size(951, 175);
            this.grpGridview.TabIndex = 234;
            this.grpGridview.TabStop = false;
            this.grpGridview.Text = "List of Purchases Invoice";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(939, 147);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRecords.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(12, 637);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(121, 17);
            this.lblTotalRecords.TabIndex = 236;
            this.lblTotalRecords.Text = "Total Records : 0";
            // 
            // grpPurchaseInvoice
            // 
            this.grpPurchaseInvoice.BackColor = System.Drawing.Color.Transparent;
            this.grpPurchaseInvoice.Controls.Add(this.label15);
            this.grpPurchaseInvoice.Controls.Add(this.label14);
            this.grpPurchaseInvoice.Controls.Add(this.label13);
            this.grpPurchaseInvoice.Controls.Add(this.label11);
            this.grpPurchaseInvoice.Controls.Add(this.label10);
            this.grpPurchaseInvoice.Controls.Add(this.btnSupplierPopup);
            this.grpPurchaseInvoice.Controls.Add(this.lblTotalQty);
            this.grpPurchaseInvoice.Controls.Add(this.txtTotalQTY);
            this.grpPurchaseInvoice.Controls.Add(this.lblBillValue);
            this.grpPurchaseInvoice.Controls.Add(this.txtBillValue);
            this.grpPurchaseInvoice.Controls.Add(this.cmbSupplier);
            this.grpPurchaseInvoice.Controls.Add(this.lblSupplier);
            this.grpPurchaseInvoice.Controls.Add(this.dtpBillDate);
            this.grpPurchaseInvoice.Controls.Add(this.lblBillDate);
            this.grpPurchaseInvoice.Controls.Add(this.lblShipmentNo);
            this.grpPurchaseInvoice.Controls.Add(this.txtShipmentNo);
            this.grpPurchaseInvoice.Controls.Add(this.lblSupplierBillNo);
            this.grpPurchaseInvoice.Controls.Add(this.txtSupplierBillNo);
            this.grpPurchaseInvoice.Enabled = false;
            this.grpPurchaseInvoice.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPurchaseInvoice.Location = new System.Drawing.Point(20, 92);
            this.grpPurchaseInvoice.Name = "grpPurchaseInvoice";
            this.grpPurchaseInvoice.Size = new System.Drawing.Size(950, 106);
            this.grpPurchaseInvoice.TabIndex = 0;
            this.grpPurchaseInvoice.TabStop = false;
            this.grpPurchaseInvoice.Text = "Purchase Invoice";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(332, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 17);
            this.label15.TabIndex = 294;
            this.label15.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(931, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 17);
            this.label14.TabIndex = 293;
            this.label14.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(699, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 17);
            this.label13.TabIndex = 292;
            this.label13.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(699, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 17);
            this.label11.TabIndex = 291;
            this.label11.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(332, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 17);
            this.label10.TabIndex = 290;
            this.label10.Text = "*";
            // 
            // btnSupplierPopup
            // 
            this.btnSupplierPopup.BackColor = System.Drawing.Color.Transparent;
            this.btnSupplierPopup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSupplierPopup.BackgroundImage")));
            this.btnSupplierPopup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSupplierPopup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupplierPopup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSupplierPopup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierPopup.Location = new System.Drawing.Point(350, 67);
            this.btnSupplierPopup.Name = "btnSupplierPopup";
            this.btnSupplierPopup.Size = new System.Drawing.Size(27, 27);
            this.btnSupplierPopup.TabIndex = 4;
            this.btnSupplierPopup.UseVisualStyleBackColor = false;
            this.btnSupplierPopup.Click += new System.EventHandler(this.btnSupplierPopup_Click);
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalQty.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(720, 67);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(78, 17);
            this.lblTotalQty.TabIndex = 239;
            this.lblTotalQty.Text = "Total QTY :";
            // 
            // txtTotalQTY
            // 
            this.txtTotalQTY.BackColor = System.Drawing.Color.White;
            this.txtTotalQTY.Location = new System.Drawing.Point(804, 68);
            this.txtTotalQTY.Name = "txtTotalQTY";
            this.txtTotalQTY.Size = new System.Drawing.Size(126, 25);
            this.txtTotalQTY.TabIndex = 6;
            this.txtTotalQTY.TextChanged += new System.EventHandler(this.txtBillValue_TextChanged);
            this.txtTotalQTY.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtTotalQTY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalQTY_KeyDown);
            this.txtTotalQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalQTY_KeyPress);
            this.txtTotalQTY.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // lblBillValue
            // 
            this.lblBillValue.AutoSize = true;
            this.lblBillValue.BackColor = System.Drawing.Color.Transparent;
            this.lblBillValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillValue.Location = new System.Drawing.Point(386, 67);
            this.lblBillValue.Name = "lblBillValue";
            this.lblBillValue.Size = new System.Drawing.Size(71, 17);
            this.lblBillValue.TabIndex = 237;
            this.lblBillValue.Text = "Bill Value :";
            // 
            // txtBillValue
            // 
            this.txtBillValue.BackColor = System.Drawing.Color.White;
            this.txtBillValue.Location = new System.Drawing.Point(491, 67);
            this.txtBillValue.Name = "txtBillValue";
            this.txtBillValue.Size = new System.Drawing.Size(206, 25);
            this.txtBillValue.TabIndex = 5;
            this.txtBillValue.TextChanged += new System.EventHandler(this.txtBillValue_TextChanged);
            this.txtBillValue.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtBillValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillValue_KeyDown);
            this.txtBillValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillValue_KeyPress);
            this.txtBillValue.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(125, 67);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(206, 27);
            this.cmbSupplier.Sorted = true;
            this.cmbSupplier.TabIndex = 3;
            this.cmbSupplier.SelectionChangeCommitted += new System.EventHandler(this.cmbSupplier_SelectionChangeCommitted);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.BackColor = System.Drawing.Color.Transparent;
            this.lblSupplier.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.Location = new System.Drawing.Point(10, 67);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(62, 17);
            this.lblSupplier.TabIndex = 234;
            this.lblSupplier.Text = "Supplier :";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBillDate.Location = new System.Drawing.Point(804, 28);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(126, 25);
            this.dtpBillDate.TabIndex = 2;
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.BackColor = System.Drawing.Color.Transparent;
            this.lblBillDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDate.Location = new System.Drawing.Point(720, 28);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(67, 17);
            this.lblBillDate.TabIndex = 232;
            this.lblBillDate.Text = "Bill Date :";
            // 
            // lblShipmentNo
            // 
            this.lblShipmentNo.AutoSize = true;
            this.lblShipmentNo.BackColor = System.Drawing.Color.Transparent;
            this.lblShipmentNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipmentNo.Location = new System.Drawing.Point(386, 28);
            this.lblShipmentNo.Name = "lblShipmentNo";
            this.lblShipmentNo.Size = new System.Drawing.Size(91, 17);
            this.lblShipmentNo.TabIndex = 231;
            this.lblShipmentNo.Text = "Shipment No :";
            // 
            // txtShipmentNo
            // 
            this.txtShipmentNo.BackColor = System.Drawing.Color.White;
            this.txtShipmentNo.Location = new System.Drawing.Point(491, 28);
            this.txtShipmentNo.Name = "txtShipmentNo";
            this.txtShipmentNo.Size = new System.Drawing.Size(206, 25);
            this.txtShipmentNo.TabIndex = 1;
            this.txtShipmentNo.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtShipmentNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShipmentNo_KeyDown);
            this.txtShipmentNo.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // lblSupplierBillNo
            // 
            this.lblSupplierBillNo.AutoSize = true;
            this.lblSupplierBillNo.BackColor = System.Drawing.Color.Transparent;
            this.lblSupplierBillNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierBillNo.Location = new System.Drawing.Point(11, 28);
            this.lblSupplierBillNo.Name = "lblSupplierBillNo";
            this.lblSupplierBillNo.Size = new System.Drawing.Size(111, 17);
            this.lblSupplierBillNo.TabIndex = 229;
            this.lblSupplierBillNo.Text = "Supplier Bill No. :";
            // 
            // txtSupplierBillNo
            // 
            this.txtSupplierBillNo.BackColor = System.Drawing.Color.White;
            this.txtSupplierBillNo.Location = new System.Drawing.Point(125, 28);
            this.txtSupplierBillNo.Name = "txtSupplierBillNo";
            this.txtSupplierBillNo.Size = new System.Drawing.Size(206, 25);
            this.txtSupplierBillNo.TabIndex = 0;
            this.txtSupplierBillNo.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtSupplierBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSupplierBillNo_KeyDown);
            this.txtSupplierBillNo.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // grpCurrencyRate
            // 
            this.grpCurrencyRate.BackColor = System.Drawing.Color.Transparent;
            this.grpCurrencyRate.Controls.Add(this.label2);
            this.grpCurrencyRate.Controls.Add(this.cmbCountry);
            this.grpCurrencyRate.Controls.Add(this.txtCurrencyRate);
            this.grpCurrencyRate.Controls.Add(this.label1);
            this.grpCurrencyRate.Enabled = false;
            this.grpCurrencyRate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCurrencyRate.Location = new System.Drawing.Point(20, 203);
            this.grpCurrencyRate.Name = "grpCurrencyRate";
            this.grpCurrencyRate.Size = new System.Drawing.Size(709, 51);
            this.grpCurrencyRate.TabIndex = 1;
            this.grpCurrencyRate.TabStop = false;
            this.grpCurrencyRate.Text = "Currency";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(381, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 242;
            this.label2.Text = "Currency Rate :";
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(128, 17);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(206, 27);
            this.cmbCountry.Sorted = true;
            this.cmbCountry.TabIndex = 0;
            this.cmbCountry.SelectionChangeCommitted += new System.EventHandler(this.cmbCountry_SelectionChangeCommitted);
            // 
            // txtCurrencyRate
            // 
            this.txtCurrencyRate.BackColor = System.Drawing.Color.White;
            this.txtCurrencyRate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrencyRate.Location = new System.Drawing.Point(486, 19);
            this.txtCurrencyRate.Name = "txtCurrencyRate";
            this.txtCurrencyRate.ReadOnly = true;
            this.txtCurrencyRate.Size = new System.Drawing.Size(206, 25);
            this.txtCurrencyRate.TabIndex = 1;
            this.txtCurrencyRate.Text = "0";
            this.txtCurrencyRate.TextChanged += new System.EventHandler(this.txtCurrencyRate_TextChanged);
            this.txtCurrencyRate.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtCurrencyRate.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 240;
            this.label1.Text = "Country :";
            // 
            // grpForeignCurrency
            // 
            this.grpForeignCurrency.BackColor = System.Drawing.Color.Transparent;
            this.grpForeignCurrency.Controls.Add(this.label3);
            this.grpForeignCurrency.Controls.Add(this.txtForeignExp);
            this.grpForeignCurrency.Controls.Add(this.label5);
            this.grpForeignCurrency.Controls.Add(this.txtForeignDiscount);
            this.grpForeignCurrency.Controls.Add(this.txtNetValue);
            this.grpForeignCurrency.Controls.Add(this.label6);
            this.grpForeignCurrency.Enabled = false;
            this.grpForeignCurrency.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpForeignCurrency.Location = new System.Drawing.Point(22, 267);
            this.grpForeignCurrency.Name = "grpForeignCurrency";
            this.grpForeignCurrency.Size = new System.Drawing.Size(342, 126);
            this.grpForeignCurrency.TabIndex = 2;
            this.grpForeignCurrency.TabStop = false;
            this.grpForeignCurrency.Text = "Foreign Currency";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 242;
            this.label3.Text = "Foreign Expenses :";
            // 
            // txtForeignExp
            // 
            this.txtForeignExp.BackColor = System.Drawing.Color.White;
            this.txtForeignExp.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForeignExp.Location = new System.Drawing.Point(149, 21);
            this.txtForeignExp.Name = "txtForeignExp";
            this.txtForeignExp.Size = new System.Drawing.Size(183, 25);
            this.txtForeignExp.TabIndex = 0;
            this.txtForeignExp.Text = "0";
            this.txtForeignExp.TextChanged += new System.EventHandler(this.txtBillValue_TextChanged);
            this.txtForeignExp.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtForeignExp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtForeignExp_KeyPress);
            this.txtForeignExp.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 17);
            this.label5.TabIndex = 246;
            this.label5.Text = "Foreign Discount(%) :";
            // 
            // txtForeignDiscount
            // 
            this.txtForeignDiscount.BackColor = System.Drawing.Color.White;
            this.txtForeignDiscount.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtForeignDiscount.Location = new System.Drawing.Point(150, 54);
            this.txtForeignDiscount.Name = "txtForeignDiscount";
            this.txtForeignDiscount.Size = new System.Drawing.Size(183, 25);
            this.txtForeignDiscount.TabIndex = 1;
            this.txtForeignDiscount.Text = "0";
            this.txtForeignDiscount.TextChanged += new System.EventHandler(this.txtBillValue_TextChanged);
            this.txtForeignDiscount.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtForeignDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtForeignExp_KeyPress);
            this.txtForeignDiscount.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // txtNetValue
            // 
            this.txtNetValue.BackColor = System.Drawing.Color.White;
            this.txtNetValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetValue.Location = new System.Drawing.Point(150, 87);
            this.txtNetValue.Name = "txtNetValue";
            this.txtNetValue.ReadOnly = true;
            this.txtNetValue.Size = new System.Drawing.Size(183, 25);
            this.txtNetValue.TabIndex = 2;
            this.txtNetValue.Text = "0";
            this.txtNetValue.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtNetValue.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 248;
            this.label6.Text = "Net Value :";
            // 
            // grpLocalCurrency
            // 
            this.grpLocalCurrency.BackColor = System.Drawing.Color.Transparent;
            this.grpLocalCurrency.Controls.Add(this.label9);
            this.grpLocalCurrency.Controls.Add(this.txtLocalValue);
            this.grpLocalCurrency.Controls.Add(this.label4);
            this.grpLocalCurrency.Controls.Add(this.txtLocalExp);
            this.grpLocalCurrency.Controls.Add(this.label7);
            this.grpLocalCurrency.Controls.Add(this.txtLocalBillValue);
            this.grpLocalCurrency.Controls.Add(this.txtNewPriceRate);
            this.grpLocalCurrency.Controls.Add(this.label8);
            this.grpLocalCurrency.Enabled = false;
            this.grpLocalCurrency.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLocalCurrency.Location = new System.Drawing.Point(378, 267);
            this.grpLocalCurrency.Name = "grpLocalCurrency";
            this.grpLocalCurrency.Size = new System.Drawing.Size(588, 126);
            this.grpLocalCurrency.TabIndex = 3;
            this.grpLocalCurrency.TabStop = false;
            this.grpLocalCurrency.Text = "Local Currency";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 17);
            this.label9.TabIndex = 241;
            this.label9.Text = "Local Value :";
            // 
            // txtLocalValue
            // 
            this.txtLocalValue.BackColor = System.Drawing.Color.White;
            this.txtLocalValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalValue.Location = new System.Drawing.Point(134, 18);
            this.txtLocalValue.Name = "txtLocalValue";
            this.txtLocalValue.ReadOnly = true;
            this.txtLocalValue.Size = new System.Drawing.Size(183, 25);
            this.txtLocalValue.TabIndex = 0;
            this.txtLocalValue.Text = "0";
            this.txtLocalValue.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtLocalValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtForeignExp_KeyPress);
            this.txtLocalValue.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 242;
            this.label4.Text = "Local Expenses :";
            // 
            // txtLocalExp
            // 
            this.txtLocalExp.BackColor = System.Drawing.Color.White;
            this.txtLocalExp.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalExp.Location = new System.Drawing.Point(134, 51);
            this.txtLocalExp.Name = "txtLocalExp";
            this.txtLocalExp.Size = new System.Drawing.Size(183, 25);
            this.txtLocalExp.TabIndex = 1;
            this.txtLocalExp.Text = "0";
            this.txtLocalExp.TextChanged += new System.EventHandler(this.txtBillValue_TextChanged);
            this.txtLocalExp.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtLocalExp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtForeignExp_KeyPress);
            this.txtLocalExp.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(333, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 252;
            this.label7.Text = "New Price Rate :";
            // 
            // txtLocalBillValue
            // 
            this.txtLocalBillValue.BackColor = System.Drawing.Color.White;
            this.txtLocalBillValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalBillValue.Location = new System.Drawing.Point(134, 84);
            this.txtLocalBillValue.Name = "txtLocalBillValue";
            this.txtLocalBillValue.ReadOnly = true;
            this.txtLocalBillValue.Size = new System.Drawing.Size(183, 25);
            this.txtLocalBillValue.TabIndex = 2;
            this.txtLocalBillValue.Text = "0";
            this.txtLocalBillValue.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtLocalBillValue.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // txtNewPriceRate
            // 
            this.txtNewPriceRate.BackColor = System.Drawing.Color.White;
            this.txtNewPriceRate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPriceRate.Location = new System.Drawing.Point(445, 84);
            this.txtNewPriceRate.Name = "txtNewPriceRate";
            this.txtNewPriceRate.ReadOnly = true;
            this.txtNewPriceRate.Size = new System.Drawing.Size(126, 25);
            this.txtNewPriceRate.TabIndex = 3;
            this.txtNewPriceRate.Text = "0";
            this.txtNewPriceRate.Enter += new System.EventHandler(this.txtSupplierBillNo_Enter);
            this.txtNewPriceRate.Leave += new System.EventHandler(this.txtSupplierBillNo_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 250;
            this.label8.Text = "Local Bill Value :";
            // 
            // Purchase_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(978, 658);
            this.Controls.Add(this.grpLocalCurrency);
            this.Controls.Add(this.grpForeignCurrency);
            this.Controls.Add(this.grpCurrencyRate);
            this.Controls.Add(this.grpPurchaseInvoice);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpGridview);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Purchase_Invoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Invoice";
            this.Load += new System.EventHandler(this.Purchase_Invoice_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpGridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpPurchaseInvoice.ResumeLayout(false);
            this.grpPurchaseInvoice.PerformLayout();
            this.grpCurrencyRate.ResumeLayout(false);
            this.grpCurrencyRate.PerformLayout();
            this.grpForeignCurrency.ResumeLayout(false);
            this.grpForeignCurrency.PerformLayout();
            this.grpLocalCurrency.ResumeLayout(false);
            this.grpLocalCurrency.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdShowAll;
        private System.Windows.Forms.RadioButton rdSearchByShipment;
        private System.Windows.Forms.TextBox txtSearchByShipmentNo;
        private System.Windows.Forms.GroupBox grpGridview;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.GroupBox grpPurchaseInvoice;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TextBox txtTotalQTY;
        private System.Windows.Forms.Label lblBillValue;
        private System.Windows.Forms.TextBox txtBillValue;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.Label lblShipmentNo;
        private System.Windows.Forms.TextBox txtShipmentNo;
        private System.Windows.Forms.Label lblSupplierBillNo;
        private System.Windows.Forms.TextBox txtSupplierBillNo;
        private System.Windows.Forms.GroupBox grpCurrencyRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.TextBox txtCurrencyRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpForeignCurrency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtForeignExp;
        private System.Windows.Forms.GroupBox grpLocalCurrency;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLocalExp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtForeignDiscount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNetValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNewPriceRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLocalBillValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLocalValue;
        private System.Windows.Forms.Button btnSupplierPopup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
    }
}