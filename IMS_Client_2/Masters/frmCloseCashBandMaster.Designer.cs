namespace IMS_Client_2.Masters
{
    partial class frmCloseCashBandMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCloseCashBandMaster));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpClosingBALMaster = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbActiveStatus = new System.Windows.Forms.ComboBox();
            this.txtCashBand = new System.Windows.Forms.TextBox();
            this.lblActiveStatusOfCustomer = new System.Windows.Forms.Label();
            this.lblCashBand = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.grpCashBandGridview = new System.Windows.Forms.GroupBox();
            this.dgvCashBand = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.grpClosingBALMaster.SuspendLayout();
            this.grpCashBandGridview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashBand)).BeginInit();
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
            this.panel2.Size = new System.Drawing.Size(628, 40);
            this.panel2.TabIndex = 111;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Closing Balance Master";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(294, 61);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(71, 25);
            this.btnUpdate.TabIndex = 115;
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
            this.btnCancel.Location = new System.Drawing.Point(458, 61);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 25);
            this.btnCancel.TabIndex = 117;
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
            this.btnDelete.Location = new System.Drawing.Point(376, 61);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 25);
            this.btnDelete.TabIndex = 116;
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
            this.btnEdit.Location = new System.Drawing.Point(214, 61);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(71, 25);
            this.btnEdit.TabIndex = 114;
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
            this.btnSave.Location = new System.Drawing.Point(132, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 25);
            this.btnSave.TabIndex = 113;
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
            this.btnAdd.Location = new System.Drawing.Point(43, 61);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 25);
            this.btnAdd.TabIndex = 112;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // grpClosingBALMaster
            // 
            this.grpClosingBALMaster.BackColor = System.Drawing.Color.Transparent;
            this.grpClosingBALMaster.Controls.Add(this.label2);
            this.grpClosingBALMaster.Controls.Add(this.label8);
            this.grpClosingBALMaster.Controls.Add(this.cmbActiveStatus);
            this.grpClosingBALMaster.Controls.Add(this.txtCashBand);
            this.grpClosingBALMaster.Controls.Add(this.lblActiveStatusOfCustomer);
            this.grpClosingBALMaster.Controls.Add(this.lblCashBand);
            this.grpClosingBALMaster.Enabled = false;
            this.grpClosingBALMaster.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpClosingBALMaster.Location = new System.Drawing.Point(65, 109);
            this.grpClosingBALMaster.Margin = new System.Windows.Forms.Padding(2);
            this.grpClosingBALMaster.Name = "grpClosingBALMaster";
            this.grpClosingBALMaster.Padding = new System.Windows.Forms.Padding(2);
            this.grpClosingBALMaster.Size = new System.Drawing.Size(445, 142);
            this.grpClosingBALMaster.TabIndex = 118;
            this.grpClosingBALMaster.TabStop = false;
            this.grpClosingBALMaster.Text = "Closing Balance Master";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(404, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 284;
            this.label2.Text = "*";
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
            // cmbActiveStatus
            // 
            this.cmbActiveStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActiveStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActiveStatus.FormattingEnabled = true;
            this.cmbActiveStatus.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbActiveStatus.Location = new System.Drawing.Point(161, 81);
            this.cmbActiveStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbActiveStatus.Name = "cmbActiveStatus";
            this.cmbActiveStatus.Size = new System.Drawing.Size(240, 25);
            this.cmbActiveStatus.TabIndex = 3;
            // 
            // txtCashBand
            // 
            this.txtCashBand.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashBand.Location = new System.Drawing.Point(161, 25);
            this.txtCashBand.Margin = new System.Windows.Forms.Padding(2);
            this.txtCashBand.MaxLength = 10;
            this.txtCashBand.Name = "txtCashBand";
            this.txtCashBand.Size = new System.Drawing.Size(240, 25);
            this.txtCashBand.TabIndex = 0;
            this.txtCashBand.Enter += new System.EventHandler(this.txtCashBand_Enter);
            this.txtCashBand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCashBand_KeyPress);
            this.txtCashBand.Leave += new System.EventHandler(this.txtCashBand_Leave);
            // 
            // lblActiveStatusOfCustomer
            // 
            this.lblActiveStatusOfCustomer.AutoSize = true;
            this.lblActiveStatusOfCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblActiveStatusOfCustomer.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveStatusOfCustomer.Location = new System.Drawing.Point(23, 85);
            this.lblActiveStatusOfCustomer.Name = "lblActiveStatusOfCustomer";
            this.lblActiveStatusOfCustomer.Size = new System.Drawing.Size(94, 17);
            this.lblActiveStatusOfCustomer.TabIndex = 190;
            this.lblActiveStatusOfCustomer.Text = "Active Status :";
            // 
            // lblCashBand
            // 
            this.lblCashBand.AutoSize = true;
            this.lblCashBand.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCashBand.Location = new System.Drawing.Point(23, 29);
            this.lblCashBand.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCashBand.Name = "lblCashBand";
            this.lblCashBand.Size = new System.Drawing.Size(80, 17);
            this.lblCashBand.TabIndex = 0;
            this.lblCashBand.Text = "Cash Band :";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalRecords.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(12, 427);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(121, 17);
            this.lblTotalRecords.TabIndex = 221;
            this.lblTotalRecords.Text = "Total Records : 0";
            // 
            // grpCashBandGridview
            // 
            this.grpCashBandGridview.BackColor = System.Drawing.Color.Transparent;
            this.grpCashBandGridview.Controls.Add(this.dgvCashBand);
            this.grpCashBandGridview.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.grpCashBandGridview.Location = new System.Drawing.Point(11, 266);
            this.grpCashBandGridview.Margin = new System.Windows.Forms.Padding(2);
            this.grpCashBandGridview.Name = "grpCashBandGridview";
            this.grpCashBandGridview.Padding = new System.Windows.Forms.Padding(2);
            this.grpCashBandGridview.Size = new System.Drawing.Size(579, 156);
            this.grpCashBandGridview.TabIndex = 220;
            this.grpCashBandGridview.TabStop = false;
            this.grpCashBandGridview.Text = "List Of Cash Band";
            // 
            // dgvCashBand
            // 
            this.dgvCashBand.AllowUserToAddRows = false;
            this.dgvCashBand.AllowUserToDeleteRows = false;
            this.dgvCashBand.BackgroundColor = System.Drawing.Color.White;
            this.dgvCashBand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCashBand.Location = new System.Drawing.Point(4, 18);
            this.dgvCashBand.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCashBand.Name = "dgvCashBand";
            this.dgvCashBand.ReadOnly = true;
            this.dgvCashBand.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvCashBand.RowTemplate.Height = 24;
            this.dgvCashBand.Size = new System.Drawing.Size(571, 133);
            this.dgvCashBand.TabIndex = 0;
            this.dgvCashBand.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCashBand_CellDoubleClick);
            this.dgvCashBand.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCashBand_DataBindingComplete);
            // 
            // frmCloseBALMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(628, 453);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.grpCashBandGridview);
            this.Controls.Add(this.grpClosingBALMaster);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCloseBALMaster";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Closing Balance Master";
            this.Load += new System.EventHandler(this.frmCloseBALMaster_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpClosingBALMaster.ResumeLayout(false);
            this.grpClosingBALMaster.PerformLayout();
            this.grpCashBandGridview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashBand)).EndInit();
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
        private System.Windows.Forms.GroupBox grpClosingBALMaster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbActiveStatus;
        private System.Windows.Forms.TextBox txtCashBand;
        private System.Windows.Forms.Label lblActiveStatusOfCustomer;
        private System.Windows.Forms.Label lblCashBand;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.GroupBox grpCashBandGridview;
        private System.Windows.Forms.DataGridView dgvCashBand;
    }
}