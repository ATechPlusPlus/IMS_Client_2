namespace IMS_Client_2.StockManagement
{
    partial class frmTransferWatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransferWatch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvTransferWatch = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdSearchByAll = new System.Windows.Forms.RadioButton();
            this.rdByBillStatus = new System.Windows.Forms.RadioButton();
            this.cmbBillStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.rdByDate = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferWatch)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(969, 40);
            this.panel2.TabIndex = 114;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Transfer Watch";
            // 
            // dgvTransferWatch
            // 
            this.dgvTransferWatch.AllowUserToAddRows = false;
            this.dgvTransferWatch.AllowUserToDeleteRows = false;
            this.dgvTransferWatch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransferWatch.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransferWatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransferWatch.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransferWatch.Location = new System.Drawing.Point(4, 128);
            this.dgvTransferWatch.Name = "dgvTransferWatch";
            this.dgvTransferWatch.ReadOnly = true;
            this.dgvTransferWatch.Size = new System.Drawing.Size(953, 293);
            this.dgvTransferWatch.TabIndex = 211;
            this.dgvTransferWatch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransferWatch_CellClick);
            this.dgvTransferWatch.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvTransferWatch_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdSearchByAll);
            this.groupBox1.Controls.Add(this.rdByBillStatus);
            this.groupBox1.Controls.Add(this.cmbBillStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.rdByDate);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(7, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(950, 66);
            this.groupBox1.TabIndex = 212;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // rdSearchByAll
            // 
            this.rdSearchByAll.AutoSize = true;
            this.rdSearchByAll.Checked = true;
            this.rdSearchByAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByAll.Location = new System.Drawing.Point(821, 28);
            this.rdSearchByAll.Name = "rdSearchByAll";
            this.rdSearchByAll.Size = new System.Drawing.Size(79, 21);
            this.rdSearchByAll.TabIndex = 255;
            this.rdSearchByAll.TabStop = true;
            this.rdSearchByAll.Text = "Show All";
            this.rdSearchByAll.UseVisualStyleBackColor = true;
            this.rdSearchByAll.CheckedChanged += new System.EventHandler(this.rdSearchByAll_CheckedChanged);
            // 
            // rdByBillStatus
            // 
            this.rdByBillStatus.AutoSize = true;
            this.rdByBillStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByBillStatus.Location = new System.Drawing.Point(427, 28);
            this.rdByBillStatus.Name = "rdByBillStatus";
            this.rdByBillStatus.Size = new System.Drawing.Size(113, 21);
            this.rdByBillStatus.TabIndex = 254;
            this.rdByBillStatus.Text = "By Bill Status :";
            this.rdByBillStatus.UseVisualStyleBackColor = true;
            this.rdByBillStatus.CheckedChanged += new System.EventHandler(this.rdByBillStatus_CheckedChanged);
            // 
            // cmbBillStatus
            // 
            this.cmbBillStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillStatus.Enabled = false;
            this.cmbBillStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillStatus.FormattingEnabled = true;
            this.cmbBillStatus.Items.AddRange(new object[] {
            "Not Received",
            "Received"});
            this.cmbBillStatus.Location = new System.Drawing.Point(555, 25);
            this.cmbBillStatus.Name = "cmbBillStatus";
            this.cmbBillStatus.Size = new System.Drawing.Size(198, 27);
            this.cmbBillStatus.TabIndex = 253;
            this.cmbBillStatus.SelectionChangeCommitted += new System.EventHandler(this.cmbBillStatus_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 32);
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
            this.dtpToDate.Location = new System.Drawing.Point(256, 28);
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
            this.dtpFromDate.Location = new System.Drawing.Point(95, 28);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(107, 25);
            this.dtpFromDate.TabIndex = 249;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // rdByDate
            // 
            this.rdByDate.AutoSize = true;
            this.rdByDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByDate.Location = new System.Drawing.Point(8, 28);
            this.rdByDate.Name = "rdByDate";
            this.rdByDate.Size = new System.Drawing.Size(83, 21);
            this.rdByDate.TabIndex = 248;
            this.rdByDate.Text = "By Date :";
            this.rdByDate.UseVisualStyleBackColor = true;
            this.rdByDate.CheckedChanged += new System.EventHandler(this.rdByDate_CheckedChanged);
            // 
            // frmTransferWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(969, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvTransferWatch);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmTransferWatch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Watch";
            this.Load += new System.EventHandler(this.frmTransferWatch_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferWatch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvTransferWatch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.RadioButton rdByDate;
        private System.Windows.Forms.RadioButton rdByBillStatus;
        private System.Windows.Forms.ComboBox cmbBillStatus;
        private System.Windows.Forms.RadioButton rdSearchByAll;
    }
}