namespace IMS_Client_2.Settings
{
    partial class frmOtherSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOtherSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSelectPC = new System.Windows.Forms.ComboBox();
            this.lblSelectPC = new System.Windows.Forms.Label();
            this.lblPCName = new System.Windows.Forms.Label();
            this.btnStorePopup = new System.Windows.Forms.Button();
            this.lblmsg = new System.Windows.Forms.Label();
            this.cmbStoreName = new System.Windows.Forms.ComboBox();
            this.lblSelectStore = new System.Windows.Forms.Label();
            this.cmbStoreCategory = new System.Windows.Forms.ComboBox();
            this.lblStoreCategory = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFileExtension = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.chkArabicPrice = new System.Windows.Forms.CheckBox();
            this.txtFooterNote = new System.Windows.Forms.TextBox();
            this.btnFooterCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFooterSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.cmbInvoicePrinter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBarcodPrinter = new System.Windows.Forms.ComboBox();
            this.btnPrinterCancel = new System.Windows.Forms.Button();
            this.btnPrinterSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtExtension = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSelectPC);
            this.groupBox1.Controls.Add(this.lblSelectPC);
            this.groupBox1.Controls.Add(this.lblPCName);
            this.groupBox1.Controls.Add(this.btnStorePopup);
            this.groupBox1.Controls.Add(this.lblmsg);
            this.groupBox1.Controls.Add(this.cmbStoreName);
            this.groupBox1.Controls.Add(this.lblSelectStore);
            this.groupBox1.Controls.Add(this.cmbStoreCategory);
            this.groupBox1.Controls.Add(this.lblStoreCategory);
            this.groupBox1.Controls.Add(this.lblMachineName);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 59);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(559, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default Shop/Store";
            // 
            // cmbSelectPC
            // 
            this.cmbSelectPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectPC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectPC.FormattingEnabled = true;
            this.cmbSelectPC.Items.AddRange(new object[] {
            "Normal Store",
            "Wearhouse"});
            this.cmbSelectPC.Location = new System.Drawing.Point(200, 88);
            this.cmbSelectPC.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSelectPC.Name = "cmbSelectPC";
            this.cmbSelectPC.Size = new System.Drawing.Size(273, 30);
            this.cmbSelectPC.TabIndex = 197;
            // 
            // lblSelectPC
            // 
            this.lblSelectPC.AutoSize = true;
            this.lblSelectPC.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectPC.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectPC.Location = new System.Drawing.Point(19, 95);
            this.lblSelectPC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectPC.Name = "lblSelectPC";
            this.lblSelectPC.Size = new System.Drawing.Size(93, 21);
            this.lblSelectPC.TabIndex = 196;
            this.lblSelectPC.Text = "Select PC :";
            // 
            // lblPCName
            // 
            this.lblPCName.AutoSize = true;
            this.lblPCName.Location = new System.Drawing.Point(197, 51);
            this.lblPCName.Name = "lblPCName";
            this.lblPCName.Size = new System.Drawing.Size(50, 21);
            this.lblPCName.TabIndex = 195;
            this.lblPCName.Text = "None";
            // 
            // btnStorePopup
            // 
            this.btnStorePopup.BackColor = System.Drawing.Color.Transparent;
            this.btnStorePopup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnStorePopup.BackgroundImage")));
            this.btnStorePopup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStorePopup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStorePopup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStorePopup.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStorePopup.Location = new System.Drawing.Point(483, 208);
            this.btnStorePopup.Margin = new System.Windows.Forms.Padding(4);
            this.btnStorePopup.Name = "btnStorePopup";
            this.btnStorePopup.Size = new System.Drawing.Size(36, 33);
            this.btnStorePopup.TabIndex = 2;
            this.btnStorePopup.UseVisualStyleBackColor = false;
            this.btnStorePopup.Click += new System.EventHandler(this.btnStorePopup_Click);
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.BackColor = System.Drawing.Color.Transparent;
            this.lblmsg.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Crimson;
            this.lblmsg.Location = new System.Drawing.Point(180, 16);
            this.lblmsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(305, 21);
            this.lblmsg.TabIndex = 194;
            this.lblmsg.Text = "No Default Store is selected for this PC";
            // 
            // cmbStoreName
            // 
            this.cmbStoreName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStoreName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStoreName.FormattingEnabled = true;
            this.cmbStoreName.Location = new System.Drawing.Point(201, 209);
            this.cmbStoreName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStoreName.Name = "cmbStoreName";
            this.cmbStoreName.Size = new System.Drawing.Size(273, 30);
            this.cmbStoreName.TabIndex = 3;
            // 
            // lblSelectStore
            // 
            this.lblSelectStore.AutoSize = true;
            this.lblSelectStore.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectStore.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectStore.Location = new System.Drawing.Point(18, 216);
            this.lblSelectStore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectStore.Name = "lblSelectStore";
            this.lblSelectStore.Size = new System.Drawing.Size(109, 21);
            this.lblSelectStore.TabIndex = 192;
            this.lblSelectStore.Text = "Select Store :";
            // 
            // cmbStoreCategory
            // 
            this.cmbStoreCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStoreCategory.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStoreCategory.FormattingEnabled = true;
            this.cmbStoreCategory.Items.AddRange(new object[] {
            "Normal Store",
            "Warehouse"});
            this.cmbStoreCategory.Location = new System.Drawing.Point(201, 148);
            this.cmbStoreCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStoreCategory.Name = "cmbStoreCategory";
            this.cmbStoreCategory.Size = new System.Drawing.Size(273, 30);
            this.cmbStoreCategory.TabIndex = 1;
            this.cmbStoreCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbStoreCategory_SelectionChangeCommitted);
            // 
            // lblStoreCategory
            // 
            this.lblStoreCategory.AutoSize = true;
            this.lblStoreCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblStoreCategory.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreCategory.Location = new System.Drawing.Point(18, 153);
            this.lblStoreCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStoreCategory.Name = "lblStoreCategory";
            this.lblStoreCategory.Size = new System.Drawing.Size(132, 21);
            this.lblStoreCategory.TabIndex = 190;
            this.lblStoreCategory.Text = "Store Category :";
            // 
            // lblMachineName
            // 
            this.lblMachineName.AutoSize = true;
            this.lblMachineName.BackColor = System.Drawing.Color.Transparent;
            this.lblMachineName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineName.Location = new System.Drawing.Point(19, 51);
            this.lblMachineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(171, 21);
            this.lblMachineName.TabIndex = 189;
            this.lblMachineName.Text = "Your Machine Name :";
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(828, 13);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(208, 13);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(95, 31);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFileExtension);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtImagePath);
            this.groupBox2.Controls.Add(this.chkArabicPrice);
            this.groupBox2.Controls.Add(this.txtFooterNote);
            this.groupBox2.Controls.Add(this.btnFooterCancel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnFooterSave);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(11, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(559, 295);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other Settings";
            // 
            // txtFileExtension
            // 
            this.txtFileExtension.BackColor = System.Drawing.Color.White;
            this.txtFileExtension.Location = new System.Drawing.Point(131, 210);
            this.txtFileExtension.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileExtension.Name = "txtFileExtension";
            this.txtFileExtension.Size = new System.Drawing.Size(108, 29);
            this.txtFileExtension.TabIndex = 197;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 217);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 21);
            this.label5.TabIndex = 196;
            this.label5.Text = "Extension :";
            // 
            // txtImagePath
            // 
            this.txtImagePath.BackColor = System.Drawing.Color.White;
            this.txtImagePath.Location = new System.Drawing.Point(131, 172);
            this.txtImagePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(419, 29);
            this.txtImagePath.TabIndex = 195;
            // 
            // chkArabicPrice
            // 
            this.chkArabicPrice.AutoSize = true;
            this.chkArabicPrice.Location = new System.Drawing.Point(81, 133);
            this.chkArabicPrice.Margin = new System.Windows.Forms.Padding(4);
            this.chkArabicPrice.Name = "chkArabicPrice";
            this.chkArabicPrice.Size = new System.Drawing.Size(179, 25);
            this.chkArabicPrice.TabIndex = 193;
            this.chkArabicPrice.Text = "Enable Arabic Price";
            this.chkArabicPrice.UseVisualStyleBackColor = true;
            // 
            // txtFooterNote
            // 
            this.txtFooterNote.BackColor = System.Drawing.Color.White;
            this.txtFooterNote.Location = new System.Drawing.Point(81, 43);
            this.txtFooterNote.Margin = new System.Windows.Forms.Padding(4);
            this.txtFooterNote.Multiline = true;
            this.txtFooterNote.Name = "txtFooterNote";
            this.txtFooterNote.Size = new System.Drawing.Size(468, 68);
            this.txtFooterNote.TabIndex = 0;
            // 
            // btnFooterCancel
            // 
            this.btnFooterCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFooterCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFooterCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFooterCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFooterCancel.Location = new System.Drawing.Point(456, 236);
            this.btnFooterCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnFooterCancel.Name = "btnFooterCancel";
            this.btnFooterCancel.Size = new System.Drawing.Size(95, 31);
            this.btnFooterCancel.TabIndex = 2;
            this.btnFooterCancel.Text = "Cancel";
            this.btnFooterCancel.UseVisualStyleBackColor = true;
            this.btnFooterCancel.Click += new System.EventHandler(this.btnFooterCancel_Click);
            this.btnFooterCancel.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnFooterCancel.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 21);
            this.label3.TabIndex = 192;
            this.label3.Text = "Image Path :";
            // 
            // btnFooterSave
            // 
            this.btnFooterSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFooterSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFooterSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFooterSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFooterSave.Location = new System.Drawing.Point(339, 236);
            this.btnFooterSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnFooterSave.Name = "btnFooterSave";
            this.btnFooterSave.Size = new System.Drawing.Size(101, 31);
            this.btnFooterSave.TabIndex = 1;
            this.btnFooterSave.Text = "Save";
            this.btnFooterSave.UseVisualStyleBackColor = true;
            this.btnFooterSave.Click += new System.EventHandler(this.button2_Click);
            this.btnFooterSave.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnFooterSave.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 21);
            this.label4.TabIndex = 192;
            this.label4.Text = "Note :";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(1, 57);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1161, 562);
            this.tabControl1.TabIndex = 112;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1153, 528);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Default Store Setting";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(580, 13);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(95, 31);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnUpdate.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(704, 13);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 31);
            this.btnDelete.TabIndex = 9;
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
            this.btnEdit.Location = new System.Drawing.Point(456, 13);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 31);
            this.btnEdit.TabIndex = 7;
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
            this.btnSave.Location = new System.Drawing.Point(332, 13);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 31);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(574, 70);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(575, 250);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1153, 528);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtMachineName);
            this.groupBox3.Controls.Add(this.cmbInvoicePrinter);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbBarcodPrinter);
            this.groupBox3.Controls.Add(this.btnPrinterCancel);
            this.groupBox3.Controls.Add(this.btnPrinterSave);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(588, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(559, 295);
            this.groupBox3.TabIndex = 198;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Printer Setting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 21);
            this.label7.TabIndex = 197;
            this.label7.Text = "Machine Name :";
            // 
            // txtMachineName
            // 
            this.txtMachineName.BackColor = System.Drawing.Color.White;
            this.txtMachineName.Location = new System.Drawing.Point(193, 30);
            this.txtMachineName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.ReadOnly = true;
            this.txtMachineName.Size = new System.Drawing.Size(319, 29);
            this.txtMachineName.TabIndex = 196;
            // 
            // cmbInvoicePrinter
            // 
            this.cmbInvoicePrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoicePrinter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInvoicePrinter.FormattingEnabled = true;
            this.cmbInvoicePrinter.Location = new System.Drawing.Point(193, 124);
            this.cmbInvoicePrinter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInvoicePrinter.Name = "cmbInvoicePrinter";
            this.cmbInvoicePrinter.Size = new System.Drawing.Size(319, 30);
            this.cmbInvoicePrinter.TabIndex = 195;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 21);
            this.label6.TabIndex = 194;
            this.label6.Text = "Invoice Printer :";
            // 
            // cmbBarcodPrinter
            // 
            this.cmbBarcodPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodPrinter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBarcodPrinter.FormattingEnabled = true;
            this.cmbBarcodPrinter.Location = new System.Drawing.Point(193, 79);
            this.cmbBarcodPrinter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBarcodPrinter.Name = "cmbBarcodPrinter";
            this.cmbBarcodPrinter.Size = new System.Drawing.Size(319, 30);
            this.cmbBarcodPrinter.TabIndex = 193;
            // 
            // btnPrinterCancel
            // 
            this.btnPrinterCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrinterCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrinterCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrinterCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrinterCancel.Location = new System.Drawing.Point(419, 236);
            this.btnPrinterCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrinterCancel.Name = "btnPrinterCancel";
            this.btnPrinterCancel.Size = new System.Drawing.Size(95, 31);
            this.btnPrinterCancel.TabIndex = 2;
            this.btnPrinterCancel.Text = "Cancel";
            this.btnPrinterCancel.UseVisualStyleBackColor = true;
            this.btnPrinterCancel.Click += new System.EventHandler(this.button1_Click);
            this.btnPrinterCancel.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnPrinterCancel.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // btnPrinterSave
            // 
            this.btnPrinterSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrinterSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrinterSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrinterSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrinterSave.Location = new System.Drawing.Point(301, 236);
            this.btnPrinterSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrinterSave.Name = "btnPrinterSave";
            this.btnPrinterSave.Size = new System.Drawing.Size(101, 31);
            this.btnPrinterSave.TabIndex = 1;
            this.btnPrinterSave.Text = "Save";
            this.btnPrinterSave.UseVisualStyleBackColor = true;
            this.btnPrinterSave.Click += new System.EventHandler(this.button2_Click_1);
            this.btnPrinterSave.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnPrinterSave.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 21);
            this.label8.TabIndex = 192;
            this.label8.Text = "Bar Code Printer :";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtExtension);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1161, 49);
            this.panel2.TabIndex = 111;
            // 
            // txtExtension
            // 
            this.txtExtension.AutoSize = true;
            this.txtExtension.BackColor = System.Drawing.Color.Transparent;
            this.txtExtension.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtension.ForeColor = System.Drawing.Color.White;
            this.txtExtension.Location = new System.Drawing.Point(15, 11);
            this.txtExtension.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(161, 26);
            this.txtExtension.TabIndex = 82;
            this.txtExtension.Text = "Other Settings";
            // 
            // frmOtherSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1161, 622);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOtherSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other Setting";
            this.Load += new System.EventHandler(this.frmOtherSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtExtension;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStoreCategory;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.ComboBox cmbStoreName;
        private System.Windows.Forms.Label lblSelectStore;
        private System.Windows.Forms.ComboBox cmbStoreCategory;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFooterNote;
        private System.Windows.Forms.Button btnFooterCancel;
        private System.Windows.Forms.Button btnFooterSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStorePopup;
        private System.Windows.Forms.CheckBox chkArabicPrice;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFileExtension;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnPrinterCancel;
        private System.Windows.Forms.Button btnPrinterSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBarcodPrinter;
        private System.Windows.Forms.ComboBox cmbInvoicePrinter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label lblPCName;
        private System.Windows.Forms.ComboBox cmbSelectPC;
        private System.Windows.Forms.Label lblSelectPC;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
    }
}