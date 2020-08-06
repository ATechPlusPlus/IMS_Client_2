namespace IMS_Client_2.StockManagement
{
    partial class frmItemWiseModelNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemWiseModelNo));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvProductDetails = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.rdSearchByShop = new System.Windows.Forms.RadioButton();
            this.txtSearchByProductName = new System.Windows.Forms.TextBox();
            this.txtSearchByStyleNo = new System.Windows.Forms.TextBox();
            this.rdSearchByModelNo = new System.Windows.Forms.RadioButton();
            this.rdSearchByProductName = new System.Windows.Forms.RadioButton();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.PicItem = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.txtProductID);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(887, 40);
            this.panel2.TabIndex = 112;
            // 
            // txtProductID
            // 
            this.txtProductID.BackColor = System.Drawing.Color.White;
            this.txtProductID.Enabled = false;
            this.txtProductID.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductID.Location = new System.Drawing.Point(415, 9);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(45, 25);
            this.txtProductID.TabIndex = 238;
            this.txtProductID.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(11, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(166, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Item Model Details";
            // 
            // dgvProductDetails
            // 
            this.dgvProductDetails.AllowUserToAddRows = false;
            this.dgvProductDetails.AllowUserToDeleteRows = false;
            this.dgvProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductDetails.Location = new System.Drawing.Point(12, 222);
            this.dgvProductDetails.Name = "dgvProductDetails";
            this.dgvProductDetails.ReadOnly = true;
            this.dgvProductDetails.Size = new System.Drawing.Size(863, 343);
            this.dgvProductDetails.TabIndex = 208;
            this.dgvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductDetails_CellClick);
            this.dgvProductDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductDetails_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbShop);
            this.groupBox1.Controls.Add(this.rdSearchByShop);
            this.groupBox1.Controls.Add(this.txtSearchByProductName);
            this.groupBox1.Controls.Add(this.txtSearchByStyleNo);
            this.groupBox1.Controls.Add(this.rdSearchByModelNo);
            this.groupBox1.Controls.Add(this.rdSearchByProductName);
            this.groupBox1.Controls.Add(this.rdShowAll);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(689, 115);
            this.groupBox1.TabIndex = 209;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.Enabled = false;
            this.cmbShop.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(125, 75);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(181, 27);
            this.cmbShop.TabIndex = 237;
            this.cmbShop.SelectionChangeCommitted += new System.EventHandler(this.cmbShop_SelectionChangeCommitted);
            // 
            // rdSearchByShop
            // 
            this.rdSearchByShop.AutoSize = true;
            this.rdSearchByShop.Checked = true;
            this.rdSearchByShop.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByShop.Location = new System.Drawing.Point(17, 78);
            this.rdSearchByShop.Name = "rdSearchByShop";
            this.rdSearchByShop.Size = new System.Drawing.Size(85, 21);
            this.rdSearchByShop.TabIndex = 236;
            this.rdSearchByShop.TabStop = true;
            this.rdSearchByShop.Text = "By Store :";
            this.rdSearchByShop.UseVisualStyleBackColor = true;
            this.rdSearchByShop.CheckedChanged += new System.EventHandler(this.rdSearchByShop_CheckedChanged);
            // 
            // txtSearchByProductName
            // 
            this.txtSearchByProductName.BackColor = System.Drawing.Color.White;
            this.txtSearchByProductName.Enabled = false;
            this.txtSearchByProductName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByProductName.Location = new System.Drawing.Point(125, 23);
            this.txtSearchByProductName.Name = "txtSearchByProductName";
            this.txtSearchByProductName.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByProductName.TabIndex = 235;
            this.txtSearchByProductName.TextChanged += new System.EventHandler(this.txtSearchByProductName_TextChanged);
            this.txtSearchByProductName.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByProductName.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // txtSearchByStyleNo
            // 
            this.txtSearchByStyleNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByStyleNo.Enabled = false;
            this.txtSearchByStyleNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchByStyleNo.Location = new System.Drawing.Point(480, 23);
            this.txtSearchByStyleNo.Name = "txtSearchByStyleNo";
            this.txtSearchByStyleNo.Size = new System.Drawing.Size(181, 25);
            this.txtSearchByStyleNo.TabIndex = 234;
            this.txtSearchByStyleNo.TextChanged += new System.EventHandler(this.txtSearchByStyleNo_TextChanged);
            this.txtSearchByStyleNo.Enter += new System.EventHandler(this.txtSearchByProductName_Enter);
            this.txtSearchByStyleNo.Leave += new System.EventHandler(this.txtSearchByProductName_Leave);
            // 
            // rdSearchByModelNo
            // 
            this.rdSearchByModelNo.AutoSize = true;
            this.rdSearchByModelNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByModelNo.Location = new System.Drawing.Point(367, 24);
            this.rdSearchByModelNo.Name = "rdSearchByModelNo";
            this.rdSearchByModelNo.Size = new System.Drawing.Size(105, 21);
            this.rdSearchByModelNo.TabIndex = 230;
            this.rdSearchByModelNo.Text = "By Style No :";
            this.rdSearchByModelNo.UseVisualStyleBackColor = true;
            this.rdSearchByModelNo.CheckedChanged += new System.EventHandler(this.rdSearchByModelNo_CheckedChanged);
            // 
            // rdSearchByProductName
            // 
            this.rdSearchByProductName.AutoSize = true;
            this.rdSearchByProductName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByProductName.Location = new System.Drawing.Point(17, 27);
            this.rdSearchByProductName.Name = "rdSearchByProductName";
            this.rdSearchByProductName.Size = new System.Drawing.Size(81, 21);
            this.rdSearchByProductName.TabIndex = 197;
            this.rdSearchByProductName.Text = "By Item :";
            this.rdSearchByProductName.UseVisualStyleBackColor = true;
            this.rdSearchByProductName.CheckedChanged += new System.EventHandler(this.rdSearchByProductName_CheckedChanged);
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdShowAll.Location = new System.Drawing.Point(367, 78);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(79, 21);
            this.rdShowAll.TabIndex = 106;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = true;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // PicItem
            // 
            this.PicItem.BackColor = System.Drawing.Color.Transparent;
            this.PicItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PicItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicItem.Location = new System.Drawing.Point(710, 46);
            this.PicItem.Name = "PicItem";
            this.PicItem.Size = new System.Drawing.Size(165, 170);
            this.PicItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicItem.TabIndex = 234;
            this.PicItem.TabStop = false;
            // 
            // frmItemWiseModelNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(887, 574);
            this.Controls.Add(this.PicItem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvProductDetails);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmItemWiseModelNo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Model Details";
            this.Load += new System.EventHandler(this.frmItemWiseModelNo_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductDetails)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvProductDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSearchByProductName;
        private System.Windows.Forms.TextBox txtSearchByStyleNo;
        private System.Windows.Forms.RadioButton rdSearchByModelNo;
        private System.Windows.Forms.RadioButton rdSearchByProductName;
        private System.Windows.Forms.RadioButton rdShowAll;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.RadioButton rdSearchByShop;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.PictureBox PicItem;
    }
}