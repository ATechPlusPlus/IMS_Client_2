namespace IMS_Client_2.Inventory
{
    partial class frmScanInventoryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanInventoryList));
            this.dgvProductDetails = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdSearchByStore = new System.Windows.Forms.RadioButton();
            this.cmbSearchByStore = new System.Windows.Forms.ComboBox();
            this.rdSearchByAll = new System.Windows.Forms.RadioButton();
            this.rdByCompareStatus = new System.Windows.Forms.RadioButton();
            this.cmbCompareStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.rdByDate = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProductDetails
            // 
            this.dgvProductDetails.AllowUserToAddRows = false;
            this.dgvProductDetails.AllowUserToDeleteRows = false;
            this.dgvProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductDetails.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvProductDetails.Location = new System.Drawing.Point(12, 124);
            this.dgvProductDetails.Name = "dgvProductDetails";
            this.dgvProductDetails.ReadOnly = true;
            this.dgvProductDetails.Size = new System.Drawing.Size(1068, 299);
            this.dgvProductDetails.TabIndex = 262;
            this.dgvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductDetails_CellClick);
            this.dgvProductDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductDetails_DataBindingComplete);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1092, 40);
            this.panel2.TabIndex = 271;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(198, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Scanned Inventory List";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdSearchByStore);
            this.groupBox1.Controls.Add(this.cmbSearchByStore);
            this.groupBox1.Controls.Add(this.rdSearchByAll);
            this.groupBox1.Controls.Add(this.rdByCompareStatus);
            this.groupBox1.Controls.Add(this.cmbCompareStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.rdByDate);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1068, 66);
            this.groupBox1.TabIndex = 213;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // rdSearchByStore
            // 
            this.rdSearchByStore.AutoSize = true;
            this.rdSearchByStore.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByStore.Location = new System.Drawing.Point(377, 26);
            this.rdSearchByStore.Name = "rdSearchByStore";
            this.rdSearchByStore.Size = new System.Drawing.Size(85, 21);
            this.rdSearchByStore.TabIndex = 257;
            this.rdSearchByStore.Text = "By Store :";
            this.rdSearchByStore.UseVisualStyleBackColor = true;
            this.rdSearchByStore.CheckedChanged += new System.EventHandler(this.rdSearchByStore_CheckedChanged);
            // 
            // cmbSearchByStore
            // 
            this.cmbSearchByStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchByStore.Enabled = false;
            this.cmbSearchByStore.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchByStore.FormattingEnabled = true;
            this.cmbSearchByStore.Items.AddRange(new object[] {
            "Pending",
            "Done"});
            this.cmbSearchByStore.Location = new System.Drawing.Point(462, 23);
            this.cmbSearchByStore.Name = "cmbSearchByStore";
            this.cmbSearchByStore.Size = new System.Drawing.Size(198, 27);
            this.cmbSearchByStore.TabIndex = 256;
            this.cmbSearchByStore.SelectionChangeCommitted += new System.EventHandler(this.cmbSearchByStore_SelectionChangeCommitted);
            // 
            // rdSearchByAll
            // 
            this.rdSearchByAll.AutoSize = true;
            this.rdSearchByAll.Checked = true;
            this.rdSearchByAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByAll.Location = new System.Drawing.Point(981, 26);
            this.rdSearchByAll.Name = "rdSearchByAll";
            this.rdSearchByAll.Size = new System.Drawing.Size(79, 21);
            this.rdSearchByAll.TabIndex = 255;
            this.rdSearchByAll.TabStop = true;
            this.rdSearchByAll.Text = "Show All";
            this.rdSearchByAll.UseVisualStyleBackColor = true;
            this.rdSearchByAll.CheckedChanged += new System.EventHandler(this.rdSearchByAll_CheckedChanged);
            // 
            // rdByCompareStatus
            // 
            this.rdByCompareStatus.AutoSize = true;
            this.rdByCompareStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByCompareStatus.Location = new System.Drawing.Point(681, 26);
            this.rdByCompareStatus.Name = "rdByCompareStatus";
            this.rdByCompareStatus.Size = new System.Drawing.Size(148, 21);
            this.rdByCompareStatus.TabIndex = 254;
            this.rdByCompareStatus.Text = "By Compare Status :";
            this.rdByCompareStatus.UseVisualStyleBackColor = true;
            this.rdByCompareStatus.CheckedChanged += new System.EventHandler(this.rdByCompareStatus_CheckedChanged);
            // 
            // cmbCompareStatus
            // 
            this.cmbCompareStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompareStatus.Enabled = false;
            this.cmbCompareStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompareStatus.FormattingEnabled = true;
            this.cmbCompareStatus.Items.AddRange(new object[] {
            "Pending",
            "Done"});
            this.cmbCompareStatus.Location = new System.Drawing.Point(834, 23);
            this.cmbCompareStatus.Name = "cmbCompareStatus";
            this.cmbCompareStatus.Size = new System.Drawing.Size(134, 27);
            this.cmbCompareStatus.TabIndex = 253;
            this.cmbCompareStatus.SelectionChangeCommitted += new System.EventHandler(this.cmbCompareStatus_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 251;
            this.label1.Text = "To : ";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(246, 24);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(109, 25);
            this.dtpToDate.TabIndex = 250;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Enabled = false;
            this.dtpFromDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(90, 24);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(107, 25);
            this.dtpFromDate.TabIndex = 249;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // rdByDate
            // 
            this.rdByDate.AutoSize = true;
            this.rdByDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByDate.Location = new System.Drawing.Point(8, 26);
            this.rdByDate.Name = "rdByDate";
            this.rdByDate.Size = new System.Drawing.Size(83, 21);
            this.rdByDate.TabIndex = 248;
            this.rdByDate.Text = "By Date :";
            this.rdByDate.UseVisualStyleBackColor = true;
            this.rdByDate.CheckedChanged += new System.EventHandler(this.rdByDate_CheckedChanged);
            // 
            // frmScanInventoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1092, 430);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvProductDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmScanInventoryList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanned Inventory List";
            this.Load += new System.EventHandler(this.frmScanInventoryList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdSearchByAll;
        private System.Windows.Forms.RadioButton rdByCompareStatus;
        private System.Windows.Forms.ComboBox cmbCompareStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.RadioButton rdByDate;
        private System.Windows.Forms.RadioButton rdSearchByStore;
        private System.Windows.Forms.ComboBox cmbSearchByStore;
    }
}