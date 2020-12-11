namespace IMS_Client_2.Masters
{
    partial class frmCompanyMaster
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpCustomerGridview = new System.Windows.Forms.GroupBox();
            this.dgvCompanyMaster = new System.Windows.Forms.DataGridView();
            this.grpCustomerSearch = new System.Windows.Forms.GroupBox();
            this.txtSearchByMobileNo = new System.Windows.Forms.TextBox();
            this.rdSearchByCustomerMobileNo = new System.Windows.Forms.RadioButton();
            this.txtSearchByCustomerName = new System.Windows.Forms.TextBox();
            this.rdShowAllOfCustomer = new System.Windows.Forms.RadioButton();
            this.rdSearchByCompanyName = new System.Windows.Forms.RadioButton();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDefaultCompany = new System.Windows.Forms.CheckBox();
            this.txtCompanyEmailID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCompanyMobileNo = new System.Windows.Forms.TextBox();
            this.txtCompanyAddress = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCustomerPhoneNo = new System.Windows.Forms.Label();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.grpCustomerGridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanyMaster)).BeginInit();
            this.grpCustomerSearch.SuspendLayout();
            this.grpCompany.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::IMS_Client_2.Properties.Resources.titlebg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 45);
            this.panel1.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(13, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 22);
            this.label12.TabIndex = 83;
            this.label12.Text = "Company Master";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(392, 59);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(71, 25);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(556, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 25);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(474, 59);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 25);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(312, 59);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 25);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(230, 59);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 25);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(141, 59);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpCustomerGridview
            // 
            this.grpCustomerGridview.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomerGridview.Controls.Add(this.dgvCompanyMaster);
            this.grpCustomerGridview.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.grpCustomerGridview.Location = new System.Drawing.Point(18, 333);
            this.grpCustomerGridview.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomerGridview.Name = "grpCustomerGridview";
            this.grpCustomerGridview.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomerGridview.Size = new System.Drawing.Size(747, 213);
            this.grpCustomerGridview.TabIndex = 221;
            this.grpCustomerGridview.TabStop = false;
            this.grpCustomerGridview.Text = "List Of Company";
            // 
            // dgvCompanyMaster
            // 
            this.dgvCompanyMaster.AllowUserToAddRows = false;
            this.dgvCompanyMaster.AllowUserToDeleteRows = false;
            this.dgvCompanyMaster.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompanyMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompanyMaster.Location = new System.Drawing.Point(7, 22);
            this.dgvCompanyMaster.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCompanyMaster.Name = "dgvCompanyMaster";
            this.dgvCompanyMaster.ReadOnly = true;
            this.dgvCompanyMaster.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvCompanyMaster.RowTemplate.Height = 24;
            this.dgvCompanyMaster.Size = new System.Drawing.Size(731, 182);
            this.dgvCompanyMaster.TabIndex = 0;
            this.dgvCompanyMaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompanyMaster_CellDoubleClick);
            this.dgvCompanyMaster.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCompanyMaster_DataBindingComplete);
            // 
            // grpCustomerSearch
            // 
            this.grpCustomerSearch.BackColor = System.Drawing.Color.Transparent;
            this.grpCustomerSearch.Controls.Add(this.txtSearchByMobileNo);
            this.grpCustomerSearch.Controls.Add(this.rdSearchByCustomerMobileNo);
            this.grpCustomerSearch.Controls.Add(this.txtSearchByCustomerName);
            this.grpCustomerSearch.Controls.Add(this.rdShowAllOfCustomer);
            this.grpCustomerSearch.Controls.Add(this.rdSearchByCompanyName);
            this.grpCustomerSearch.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpCustomerSearch.Location = new System.Drawing.Point(18, 266);
            this.grpCustomerSearch.Margin = new System.Windows.Forms.Padding(2);
            this.grpCustomerSearch.Name = "grpCustomerSearch";
            this.grpCustomerSearch.Padding = new System.Windows.Forms.Padding(2);
            this.grpCustomerSearch.Size = new System.Drawing.Size(747, 55);
            this.grpCustomerSearch.TabIndex = 220;
            this.grpCustomerSearch.TabStop = false;
            this.grpCustomerSearch.Text = "Search By";
            // 
            // txtSearchByMobileNo
            // 
            this.txtSearchByMobileNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByMobileNo.Enabled = false;
            this.txtSearchByMobileNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByMobileNo.Location = new System.Drawing.Point(440, 20);
            this.txtSearchByMobileNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchByMobileNo.Name = "txtSearchByMobileNo";
            this.txtSearchByMobileNo.Size = new System.Drawing.Size(184, 25);
            this.txtSearchByMobileNo.TabIndex = 4;
            // 
            // rdSearchByCustomerMobileNo
            // 
            this.rdSearchByCustomerMobileNo.AutoSize = true;
            this.rdSearchByCustomerMobileNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.rdSearchByCustomerMobileNo.Location = new System.Drawing.Point(340, 22);
            this.rdSearchByCustomerMobileNo.Margin = new System.Windows.Forms.Padding(2);
            this.rdSearchByCustomerMobileNo.Name = "rdSearchByCustomerMobileNo";
            this.rdSearchByCustomerMobileNo.Size = new System.Drawing.Size(95, 21);
            this.rdSearchByCustomerMobileNo.TabIndex = 3;
            this.rdSearchByCustomerMobileNo.Text = "Mobile No :";
            this.rdSearchByCustomerMobileNo.UseVisualStyleBackColor = true;
            // 
            // txtSearchByCustomerName
            // 
            this.txtSearchByCustomerName.BackColor = System.Drawing.Color.White;
            this.txtSearchByCustomerName.Enabled = false;
            this.txtSearchByCustomerName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByCustomerName.Location = new System.Drawing.Point(141, 20);
            this.txtSearchByCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchByCustomerName.Name = "txtSearchByCustomerName";
            this.txtSearchByCustomerName.Size = new System.Drawing.Size(184, 25);
            this.txtSearchByCustomerName.TabIndex = 1;
            // 
            // rdShowAllOfCustomer
            // 
            this.rdShowAllOfCustomer.AutoSize = true;
            this.rdShowAllOfCustomer.Checked = true;
            this.rdShowAllOfCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.rdShowAllOfCustomer.Location = new System.Drawing.Point(641, 22);
            this.rdShowAllOfCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.rdShowAllOfCustomer.Name = "rdShowAllOfCustomer";
            this.rdShowAllOfCustomer.Size = new System.Drawing.Size(79, 21);
            this.rdShowAllOfCustomer.TabIndex = 2;
            this.rdShowAllOfCustomer.TabStop = true;
            this.rdShowAllOfCustomer.Text = "Show All";
            this.rdShowAllOfCustomer.UseVisualStyleBackColor = true;
            // 
            // rdSearchByCompanyName
            // 
            this.rdSearchByCompanyName.AutoSize = true;
            this.rdSearchByCompanyName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.rdSearchByCompanyName.Location = new System.Drawing.Point(7, 22);
            this.rdSearchByCompanyName.Margin = new System.Windows.Forms.Padding(2);
            this.rdSearchByCompanyName.Name = "rdSearchByCompanyName";
            this.rdSearchByCompanyName.Size = new System.Drawing.Size(129, 21);
            this.rdSearchByCompanyName.TabIndex = 0;
            this.rdSearchByCompanyName.Text = "Company Name :";
            this.rdSearchByCompanyName.UseVisualStyleBackColor = true;
            // 
            // grpCompany
            // 
            this.grpCompany.BackColor = System.Drawing.Color.Transparent;
            this.grpCompany.Controls.Add(this.label3);
            this.grpCompany.Controls.Add(this.label2);
            this.grpCompany.Controls.Add(this.chkDefaultCompany);
            this.grpCompany.Controls.Add(this.txtCompanyEmailID);
            this.grpCompany.Controls.Add(this.label4);
            this.grpCompany.Controls.Add(this.label1);
            this.grpCompany.Controls.Add(this.label8);
            this.grpCompany.Controls.Add(this.txtCompanyMobileNo);
            this.grpCompany.Controls.Add(this.txtCompanyAddress);
            this.grpCompany.Controls.Add(this.txtCompanyName);
            this.grpCompany.Controls.Add(this.lblCustomerPhoneNo);
            this.grpCompany.Controls.Add(this.lblCustomerAddress);
            this.grpCompany.Controls.Add(this.lblCustomerName);
            this.grpCompany.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpCompany.Location = new System.Drawing.Point(18, 101);
            this.grpCompany.Margin = new System.Windows.Forms.Padding(2);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Padding = new System.Windows.Forms.Padding(2);
            this.grpCompany.Size = new System.Drawing.Size(747, 153);
            this.grpCompany.TabIndex = 219;
            this.grpCompany.TabStop = false;
            this.grpCompany.Text = "Company Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(344, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 17);
            this.label3.TabIndex = 289;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(711, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 288;
            this.label2.Text = "*";
            // 
            // chkDefaultCompany
            // 
            this.chkDefaultCompany.AutoSize = true;
            this.chkDefaultCompany.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDefaultCompany.Location = new System.Drawing.Point(468, 105);
            this.chkDefaultCompany.Name = "chkDefaultCompany";
            this.chkDefaultCompany.Size = new System.Drawing.Size(146, 21);
            this.chkDefaultCompany.TabIndex = 287;
            this.chkDefaultCompany.Text = "Is Default Company";
            this.chkDefaultCompany.UseVisualStyleBackColor = true;
            // 
            // txtCompanyEmailID
            // 
            this.txtCompanyEmailID.BackColor = System.Drawing.Color.White;
            this.txtCompanyEmailID.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyEmailID.Location = new System.Drawing.Point(102, 105);
            this.txtCompanyEmailID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyEmailID.MaxLength = 100;
            this.txtCompanyEmailID.Name = "txtCompanyEmailID";
            this.txtCompanyEmailID.Size = new System.Drawing.Size(240, 25);
            this.txtCompanyEmailID.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label4.Location = new System.Drawing.Point(17, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 286;
            this.label4.Text = "Email ID :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(344, 65);
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
            this.label8.Location = new System.Drawing.Point(344, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 17);
            this.label8.TabIndex = 282;
            this.label8.Text = "*";
            // 
            // txtCompanyMobileNo
            // 
            this.txtCompanyMobileNo.BackColor = System.Drawing.Color.White;
            this.txtCompanyMobileNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyMobileNo.Location = new System.Drawing.Point(102, 65);
            this.txtCompanyMobileNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyMobileNo.MaxLength = 10;
            this.txtCompanyMobileNo.Name = "txtCompanyMobileNo";
            this.txtCompanyMobileNo.Size = new System.Drawing.Size(240, 25);
            this.txtCompanyMobileNo.TabIndex = 1;
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.BackColor = System.Drawing.Color.White;
            this.txtCompanyAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyAddress.Location = new System.Drawing.Point(468, 21);
            this.txtCompanyAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyAddress.MaxLength = 500;
            this.txtCompanyAddress.Multiline = true;
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.txtCompanyAddress.Size = new System.Drawing.Size(240, 65);
            this.txtCompanyAddress.TabIndex = 3;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BackColor = System.Drawing.Color.White;
            this.txtCompanyName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.Location = new System.Drawing.Point(102, 25);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyName.MaxLength = 100;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(240, 25);
            this.txtCompanyName.TabIndex = 0;
            this.txtCompanyName.Enter += new System.EventHandler(this.txtCompanyName_Enter);
            this.txtCompanyName.Leave += new System.EventHandler(this.txtCompanyName_Leave);
            // 
            // lblCustomerPhoneNo
            // 
            this.lblCustomerPhoneNo.AutoSize = true;
            this.lblCustomerPhoneNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCustomerPhoneNo.Location = new System.Drawing.Point(17, 69);
            this.lblCustomerPhoneNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerPhoneNo.Name = "lblCustomerPhoneNo";
            this.lblCustomerPhoneNo.Size = new System.Drawing.Size(77, 17);
            this.lblCustomerPhoneNo.TabIndex = 2;
            this.lblCustomerPhoneNo.Text = "Mobile No :";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCustomerAddress.Location = new System.Drawing.Point(386, 28);
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
            this.lblCustomerName.Location = new System.Drawing.Point(17, 29);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(51, 17);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Name :";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRecords.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(15, 548);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(121, 17);
            this.lblTotalRecords.TabIndex = 222;
            this.lblTotalRecords.Text = "Total Records : 0";
            // 
            // frmCompanyMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(783, 569);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.grpCustomerGridview);
            this.Controls.Add(this.grpCustomerSearch);
            this.Controls.Add(this.grpCompany);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCompanyMaster";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Master";
            this.Load += new System.EventHandler(this.frmCompanyMaster_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpCustomerGridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanyMaster)).EndInit();
            this.grpCustomerSearch.ResumeLayout(false);
            this.grpCustomerSearch.PerformLayout();
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpCustomerGridview;
        private System.Windows.Forms.DataGridView dgvCompanyMaster;
        private System.Windows.Forms.GroupBox grpCustomerSearch;
        private System.Windows.Forms.TextBox txtSearchByMobileNo;
        private System.Windows.Forms.RadioButton rdSearchByCustomerMobileNo;
        private System.Windows.Forms.TextBox txtSearchByCustomerName;
        private System.Windows.Forms.RadioButton rdShowAllOfCustomer;
        private System.Windows.Forms.RadioButton rdSearchByCompanyName;
        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.TextBox txtCompanyEmailID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCompanyMobileNo;
        private System.Windows.Forms.TextBox txtCompanyAddress;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblCustomerPhoneNo;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.CheckBox chkDefaultCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}