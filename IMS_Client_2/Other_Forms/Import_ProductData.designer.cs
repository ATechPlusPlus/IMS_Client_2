namespace IMS_Client_2.Other_Forms
{
    partial class Import_ProductData
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.tbnTruncate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtImportProductData = new System.Windows.Forms.TextBox();
            this.btnImportProductMaster = new System.Windows.Forms.Button();
            this.lkbCancelForProductData = new System.Windows.Forms.LinkLabel();
            this.lkbBrowseForProductData = new System.Windows.Forms.LinkLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::IMS_Client_2.Properties.Resources.titlebg;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 40);
            this.panel2.TabIndex = 218;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(275, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Import Product Data from Excel";
            // 
            // tbnTruncate
            // 
            this.tbnTruncate.Location = new System.Drawing.Point(555, 71);
            this.tbnTruncate.Margin = new System.Windows.Forms.Padding(2);
            this.tbnTruncate.Name = "tbnTruncate";
            this.tbnTruncate.Size = new System.Drawing.Size(31, 24);
            this.tbnTruncate.TabIndex = 232;
            this.tbnTruncate.Text = "Delete";
            this.tbnTruncate.UseVisualStyleBackColor = true;
            this.tbnTruncate.Visible = false;
            this.tbnTruncate.Click += new System.EventHandler(this.tbnTruncate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 231;
            this.label1.Text = "NA";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 111);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(554, 15);
            this.progressBar1.TabIndex = 230;
            // 
            // txtImportProductData
            // 
            this.txtImportProductData.BackColor = System.Drawing.Color.White;
            this.txtImportProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtImportProductData.Location = new System.Drawing.Point(15, 72);
            this.txtImportProductData.Name = "txtImportProductData";
            this.txtImportProductData.ReadOnly = true;
            this.txtImportProductData.Size = new System.Drawing.Size(250, 25);
            this.txtImportProductData.TabIndex = 229;
            // 
            // btnImportProductMaster
            // 
            this.btnImportProductMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportProductMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportProductMaster.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImportProductMaster.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.btnImportProductMaster.Location = new System.Drawing.Point(415, 71);
            this.btnImportProductMaster.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportProductMaster.Name = "btnImportProductMaster";
            this.btnImportProductMaster.Size = new System.Drawing.Size(135, 24);
            this.btnImportProductMaster.TabIndex = 228;
            this.btnImportProductMaster.Text = "Import Product Data";
            this.btnImportProductMaster.UseVisualStyleBackColor = true;
            this.btnImportProductMaster.Click += new System.EventHandler(this.btnImportProductMaster_Click);
            this.btnImportProductMaster.MouseEnter += new System.EventHandler(this.btnImportProductMaster_MouseEnter);
            this.btnImportProductMaster.MouseLeave += new System.EventHandler(this.btnImportProductMaster_MouseLeave);
            // 
            // lkbCancelForProductData
            // 
            this.lkbCancelForProductData.AutoSize = true;
            this.lkbCancelForProductData.BackColor = System.Drawing.Color.Transparent;
            this.lkbCancelForProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkbCancelForProductData.Location = new System.Drawing.Point(362, 75);
            this.lkbCancelForProductData.Name = "lkbCancelForProductData";
            this.lkbCancelForProductData.Size = new System.Drawing.Size(49, 17);
            this.lkbCancelForProductData.TabIndex = 227;
            this.lkbCancelForProductData.TabStop = true;
            this.lkbCancelForProductData.Text = "Cancel";
            this.lkbCancelForProductData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkbCancelForProductData_LinkClicked);
            // 
            // lkbBrowseForProductData
            // 
            this.lkbBrowseForProductData.AutoSize = true;
            this.lkbBrowseForProductData.BackColor = System.Drawing.Color.Transparent;
            this.lkbBrowseForProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkbBrowseForProductData.Location = new System.Drawing.Point(284, 75);
            this.lkbBrowseForProductData.Name = "lkbBrowseForProductData";
            this.lkbBrowseForProductData.Size = new System.Drawing.Size(66, 17);
            this.lkbBrowseForProductData.TabIndex = 226;
            this.lkbBrowseForProductData.TabStop = true;
            this.lkbBrowseForProductData.Text = "Browse...";
            this.lkbBrowseForProductData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkbBrowseForProductData_LinkClicked);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 154);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(553, 310);
            this.dataGridView1.TabIndex = 233;
            // 
            // Import_ProductData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(595, 489);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbnTruncate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtImportProductData);
            this.Controls.Add(this.btnImportProductMaster);
            this.Controls.Add(this.lkbCancelForProductData);
            this.Controls.Add(this.lkbBrowseForProductData);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Import_ProductData";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Product Data";
            this.Load += new System.EventHandler(this.Import_ProductData_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button tbnTruncate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtImportProductData;
        private System.Windows.Forms.Button btnImportProductMaster;
        private System.Windows.Forms.LinkLabel lkbCancelForProductData;
        private System.Windows.Forms.LinkLabel lkbBrowseForProductData;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}