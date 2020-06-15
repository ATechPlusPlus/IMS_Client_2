namespace IMS_Client_2.Masters
{
    partial class Customer_Master
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer_Master));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomerActiveStatus = new System.Windows.Forms.ComboBox();
            this.txtCustomerPhoneNo = new System.Windows.Forms.TextBox();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblActiveStatusOfCustomer = new System.Windows.Forms.Label();
            this.lblCustomerPhoneNo = new System.Windows.Forms.Label();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.grpCustomerSearch = new System.Windows.Forms.GroupBox();
            this.txtSearchByCustomer = new System.Windows.Forms.TextBox();
            this.rdShowAllOfCustomer = new System.Windows.Forms.RadioButton();
            this.rdSearchByCustomerName = new System.Windows.Forms.RadioButton();
            this.grpCustomerGridview = new System.Windows.Forms.GroupBox();
            this.dgvCustomerMaster = new System.Windows.Forms.DataGridView();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpCustomer.SuspendLayout();
            this.grpCustomerSearch.SuspendLayout();
            this.grpCustomerGridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 45);
            this.panel1.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(13, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 22);
            this.label12.TabIndex = 83;
            this.label12.Text = "Customer Master";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(314, 59);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(71, 25);
            this.btnUpdate.TabIndex = 5;
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
            this.btnCancel.Location = new System.Drawing.Point(478, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 25);
            this.btnCancel.TabIndex = 7;
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
            this.btnDelete.Location = new System.Drawing.Point(396, 59);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 25);
            this.btnDelete.TabIndex = 6;
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
            this.btnEdit.Location = new System.Drawing.Point(234, 59);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 25);
            this.btnEdit.TabIndex = 3;
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
            this.btnSave.Location = new System.Drawing.Point(152, 59);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 25);
            this.btnSave.TabIndex = 2;
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
            this.btnAdd.Location = new System.Drawing.Point(63, 59);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // grpCustomer
            // 
            this.grpCustomer.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomer.Controls.Add(this.label2);
            this.grpCustomer.Controls.Add(this.label1);
            this.grpCustomer.Controls.Add(this.label8);
            this.grpCustomer.Controls.Add(this.cmbCustomerActiveStatus);
            this.grpCustomer.Controls.Add(this.txtCustomerPhoneNo);
            this.grpCustomer.Controls.Add(this.txtCustomerAddress);
            this.grpCustomer.Controls.Add(this.txtCustomerName);
            this.grpCustomer.Controls.Add(this.lblActiveStatusOfCustomer);
            this.grpCustomer.Controls.Add(this.lblCustomerPhoneNo);
            this.grpCustomer.Controls.Add(this.lblCustomerAddress);
            this.grpCustomer.Controls.Add(this.lblCustomerName);
            this.grpCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpCustomer.Location = new System.Drawing.Point(17, 102);
            this.grpCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomer.Size = new System.Drawing.Size(575, 197);
            this.grpCustomer.TabIndex = 0;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Customer Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(404, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 284;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(404, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 17);
            this.label1.TabIndex = 283;
            this.label1.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(404, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 17);
            this.label8.TabIndex = 282;
            this.label8.Text = "*";
            // 
            // cmbCustomerActiveStatus
            // 
            this.cmbCustomerActiveStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerActiveStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomerActiveStatus.FormattingEnabled = true;
            this.cmbCustomerActiveStatus.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbCustomerActiveStatus.Location = new System.Drawing.Point(161, 161);
            this.cmbCustomerActiveStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerActiveStatus.Name = "cmbCustomerActiveStatus";
            this.cmbCustomerActiveStatus.Size = new System.Drawing.Size(240, 25);
            this.cmbCustomerActiveStatus.TabIndex = 3;
            // 
            // txtCustomerPhoneNo
            // 
            this.txtCustomerPhoneNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPhoneNo.Location = new System.Drawing.Point(161, 62);
            this.txtCustomerPhoneNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomerPhoneNo.Name = "txtCustomerPhoneNo";
            this.txtCustomerPhoneNo.Size = new System.Drawing.Size(240, 25);
            this.txtCustomerPhoneNo.TabIndex = 1;
            this.txtCustomerPhoneNo.Enter += new System.EventHandler(this.txtCustomerName_Enter);
            this.txtCustomerPhoneNo.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.BackColor = System.Drawing.Color.White;
            this.txtCustomerAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerAddress.Location = new System.Drawing.Point(161, 99);
            this.txtCustomerAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomerAddress.Multiline = true;
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(240, 50);
            this.txtCustomerAddress.TabIndex = 2;
            this.txtCustomerAddress.Enter += new System.EventHandler(this.txtCustomerName_Enter);
            this.txtCustomerAddress.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(161, 25);
            this.txtCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(240, 25);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.Enter += new System.EventHandler(this.txtCustomerName_Enter);
            this.txtCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerName_KeyDown);
            this.txtCustomerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerName_KeyPress);
            this.txtCustomerName.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // lblActiveStatusOfCustomer
            // 
            this.lblActiveStatusOfCustomer.AutoSize = true;
            this.lblActiveStatusOfCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblActiveStatusOfCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveStatusOfCustomer.Location = new System.Drawing.Point(23, 165);
            this.lblActiveStatusOfCustomer.Name = "lblActiveStatusOfCustomer";
            this.lblActiveStatusOfCustomer.Size = new System.Drawing.Size(94, 17);
            this.lblActiveStatusOfCustomer.TabIndex = 190;
            this.lblActiveStatusOfCustomer.Text = "Active Status :";
            // 
            // lblCustomerPhoneNo
            // 
            this.lblCustomerPhoneNo.AutoSize = true;
            this.lblCustomerPhoneNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCustomerPhoneNo.Location = new System.Drawing.Point(23, 66);
            this.lblCustomerPhoneNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerPhoneNo.Name = "lblCustomerPhoneNo";
            this.lblCustomerPhoneNo.Size = new System.Drawing.Size(74, 17);
            this.lblCustomerPhoneNo.TabIndex = 2;
            this.lblCustomerPhoneNo.Text = "Phone No :";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCustomerAddress.Location = new System.Drawing.Point(23, 116);
            this.lblCustomerAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(64, 17);
            this.lblCustomerAddress.TabIndex = 1;
            this.lblCustomerAddress.Text = "Address :";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCustomerName.Location = new System.Drawing.Point(23, 29);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(51, 17);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Name :";
            // 
            // grpCustomerSearch
            // 
            this.grpCustomerSearch.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomerSearch.Controls.Add(this.txtSearchByCustomer);
            this.grpCustomerSearch.Controls.Add(this.rdShowAllOfCustomer);
            this.grpCustomerSearch.Controls.Add(this.rdSearchByCustomerName);
            this.grpCustomerSearch.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpCustomerSearch.Location = new System.Drawing.Point(17, 319);
            this.grpCustomerSearch.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomerSearch.Name = "grpCustomerSearch";
            this.grpCustomerSearch.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomerSearch.Size = new System.Drawing.Size(579, 55);
            this.grpCustomerSearch.TabIndex = 8;
            this.grpCustomerSearch.TabStop = false;
            this.grpCustomerSearch.Text = "Search";
            // 
            // txtSearchByCustomer
            // 
            this.txtSearchByCustomer.Enabled = false;
            this.txtSearchByCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByCustomer.Location = new System.Drawing.Point(161, 20);
            this.txtSearchByCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchByCustomer.Name = "txtSearchByCustomer";
            this.txtSearchByCustomer.Size = new System.Drawing.Size(240, 25);
            this.txtSearchByCustomer.TabIndex = 1;
            this.txtSearchByCustomer.TextChanged += new System.EventHandler(this.txtSearchByCustomer_TextChanged);
            this.txtSearchByCustomer.Enter += new System.EventHandler(this.txtCustomerName_Enter);
            this.txtSearchByCustomer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerName_KeyPress);
            this.txtSearchByCustomer.Leave += new System.EventHandler(this.txtCustomerName_Leave);
            // 
            // rdShowAllOfCustomer
            // 
            this.rdShowAllOfCustomer.AutoSize = true;
            this.rdShowAllOfCustomer.Checked = true;
            this.rdShowAllOfCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.rdShowAllOfCustomer.Location = new System.Drawing.Point(445, 23);
            this.rdShowAllOfCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.rdShowAllOfCustomer.Name = "rdShowAllOfCustomer";
            this.rdShowAllOfCustomer.Size = new System.Drawing.Size(79, 21);
            this.rdShowAllOfCustomer.TabIndex = 2;
            this.rdShowAllOfCustomer.TabStop = true;
            this.rdShowAllOfCustomer.Text = "Show All";
            this.rdShowAllOfCustomer.UseVisualStyleBackColor = true;
            this.rdShowAllOfCustomer.CheckedChanged += new System.EventHandler(this.rdShowAllOfCustomer_CheckedChanged);
            // 
            // rdSearchByCustomerName
            // 
            this.rdSearchByCustomerName.AutoSize = true;
            this.rdSearchByCustomerName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.rdSearchByCustomerName.Location = new System.Drawing.Point(13, 22);
            this.rdSearchByCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.rdSearchByCustomerName.Name = "rdSearchByCustomerName";
            this.rdSearchByCustomerName.Size = new System.Drawing.Size(151, 21);
            this.rdSearchByCustomerName.TabIndex = 0;
            this.rdSearchByCustomerName.Text = "By Customer Name :";
            this.rdSearchByCustomerName.UseVisualStyleBackColor = true;
            this.rdSearchByCustomerName.CheckedChanged += new System.EventHandler(this.rdSearchByCustomerName_CheckedChanged);
            // 
            // grpCustomerGridview
            // 
            this.grpCustomerGridview.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomerGridview.Controls.Add(this.dgvCustomerMaster);
            this.grpCustomerGridview.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.grpCustomerGridview.Location = new System.Drawing.Point(17, 390);
            this.grpCustomerGridview.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomerGridview.Name = "grpCustomerGridview";
            this.grpCustomerGridview.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomerGridview.Size = new System.Drawing.Size(579, 156);
            this.grpCustomerGridview.TabIndex = 218;
            this.grpCustomerGridview.TabStop = false;
            this.grpCustomerGridview.Text = "List Of Customers";
            // 
            // dgvCustomerMaster
            // 
            this.dgvCustomerMaster.AllowUserToAddRows = false;
            this.dgvCustomerMaster.AllowUserToDeleteRows = false;
            this.dgvCustomerMaster.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomerMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerMaster.Location = new System.Drawing.Point(4, 18);
            this.dgvCustomerMaster.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCustomerMaster.Name = "dgvCustomerMaster";
            this.dgvCustomerMaster.ReadOnly = true;
            this.dgvCustomerMaster.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvCustomerMaster.RowTemplate.Height = 24;
            this.dgvCustomerMaster.Size = new System.Drawing.Size(571, 133);
            this.dgvCustomerMaster.TabIndex = 0;
            this.dgvCustomerMaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomerMaster_CellDoubleClick);
            this.dgvCustomerMaster.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCustomerMaster_DataBindingComplete);
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRecords.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(18, 551);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(121, 17);
            this.lblTotalRecords.TabIndex = 219;
            this.lblTotalRecords.Text = "Total Records : 0";
            // 
            // Customer_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(628, 569);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.grpCustomerGridview);
            this.Controls.Add(this.grpCustomerSearch);
            this.Controls.Add(this.grpCustomer);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Customer_Master";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Master";
            this.Load += new System.EventHandler(this.Customer_Master_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            this.grpCustomerSearch.ResumeLayout(false);
            this.grpCustomerSearch.PerformLayout();
            this.grpCustomerGridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.GroupBox grpCustomerSearch;
        private System.Windows.Forms.GroupBox grpCustomerGridview;
        private System.Windows.Forms.Label lblCustomerPhoneNo;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblActiveStatusOfCustomer;
        private System.Windows.Forms.TextBox txtCustomerPhoneNo;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.ComboBox cmbCustomerActiveStatus;
        private System.Windows.Forms.RadioButton rdShowAllOfCustomer;
        private System.Windows.Forms.RadioButton rdSearchByCustomerName;
        private System.Windows.Forms.TextBox txtSearchByCustomer;
        private System.Windows.Forms.DataGridView dgvCustomerMaster;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
    }
}