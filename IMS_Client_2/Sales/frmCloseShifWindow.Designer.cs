namespace IMS_Client_2.Sales
{
    partial class frmCloseShifWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCloseShifWindow));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Cash");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Credits");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Expenses");
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCloseCash = new System.Windows.Forms.Button();
            this.btnOpenCash = new System.Windows.Forms.Button();
            this.grpReturned = new System.Windows.Forms.GroupBox();
            this.lblReturnedAmount = new System.Windows.Forms.Label();
            this.lblReturned = new System.Windows.Forms.Label();
            this.lblShop = new System.Windows.Forms.Label();
            this.lblCashierName = new System.Windows.Forms.Label();
            this.lblCashNo = new System.Windows.Forms.Label();
            this.dgvCloseCash = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotalValue = new System.Windows.Forms.TextBox();
            this.txtCashNo = new System.Windows.Forms.TextBox();
            this.txtCashierName = new System.Windows.Forms.TextBox();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.grpPettyCashBAL = new System.Windows.Forms.GroupBox();
            this.lblPettyCashBAL = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.grpReturned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloseCash)).BeginInit();
            this.grpPettyCashBAL.SuspendLayout();
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
            this.panel2.TabIndex = 112;
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
            this.label12.Text = "Close Shif Window";
            // 
            // btnCloseCash
            // 
            this.btnCloseCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseCash.Enabled = false;
            this.btnCloseCash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloseCash.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseCash.Location = new System.Drawing.Point(153, 550);
            this.btnCloseCash.Name = "btnCloseCash";
            this.btnCloseCash.Size = new System.Drawing.Size(140, 29);
            this.btnCloseCash.TabIndex = 115;
            this.btnCloseCash.Text = "Close Cash";
            this.btnCloseCash.UseVisualStyleBackColor = true;
            this.btnCloseCash.Click += new System.EventHandler(this.btnCloseCash_Click);
            this.btnCloseCash.MouseEnter += new System.EventHandler(this.btnOpenCash_MouseEnter);
            this.btnCloseCash.MouseLeave += new System.EventHandler(this.btnOpenCash_MouseLeave);
            // 
            // btnOpenCash
            // 
            this.btnOpenCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenCash.Enabled = false;
            this.btnOpenCash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenCash.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCash.Location = new System.Drawing.Point(8, 550);
            this.btnOpenCash.Name = "btnOpenCash";
            this.btnOpenCash.Size = new System.Drawing.Size(140, 29);
            this.btnOpenCash.TabIndex = 114;
            this.btnOpenCash.Text = "Open Cash Box";
            this.btnOpenCash.UseVisualStyleBackColor = true;
            this.btnOpenCash.Click += new System.EventHandler(this.btnOpenCash_Click);
            this.btnOpenCash.MouseEnter += new System.EventHandler(this.btnOpenCash_MouseEnter);
            this.btnOpenCash.MouseLeave += new System.EventHandler(this.btnOpenCash_MouseLeave);
            // 
            // grpReturned
            // 
            this.grpReturned.BackColor = System.Drawing.Color.Transparent;
            this.grpReturned.Controls.Add(this.lblReturnedAmount);
            this.grpReturned.Controls.Add(this.lblReturned);
            this.grpReturned.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpReturned.Location = new System.Drawing.Point(12, 133);
            this.grpReturned.Margin = new System.Windows.Forms.Padding(2);
            this.grpReturned.Name = "grpReturned";
            this.grpReturned.Padding = new System.Windows.Forms.Padding(2);
            this.grpReturned.Size = new System.Drawing.Size(281, 58);
            this.grpReturned.TabIndex = 119;
            this.grpReturned.TabStop = false;
            this.grpReturned.Text = "Returned Amount";
            // 
            // lblReturnedAmount
            // 
            this.lblReturnedAmount.AutoSize = true;
            this.lblReturnedAmount.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblReturnedAmount.Location = new System.Drawing.Point(196, 29);
            this.lblReturnedAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReturnedAmount.Name = "lblReturnedAmount";
            this.lblReturnedAmount.Size = new System.Drawing.Size(15, 17);
            this.lblReturnedAmount.TabIndex = 1;
            this.lblReturnedAmount.Text = "0";
            // 
            // lblReturned
            // 
            this.lblReturned.AutoSize = true;
            this.lblReturned.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblReturned.Location = new System.Drawing.Point(57, 29);
            this.lblReturned.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReturned.Name = "lblReturned";
            this.lblReturned.Size = new System.Drawing.Size(132, 17);
            this.lblReturned.TabIndex = 0;
            this.lblReturned.Text = "Returned Amount  = ";
            // 
            // lblShop
            // 
            this.lblShop.AutoSize = true;
            this.lblShop.BackColor = System.Drawing.Color.White;
            this.lblShop.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblShop.Location = new System.Drawing.Point(12, 53);
            this.lblShop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblShop.Name = "lblShop";
            this.lblShop.Size = new System.Drawing.Size(44, 17);
            this.lblShop.TabIndex = 2;
            this.lblShop.Text = "Shop :";
            // 
            // lblCashierName
            // 
            this.lblCashierName.AutoSize = true;
            this.lblCashierName.BackColor = System.Drawing.Color.White;
            this.lblCashierName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCashierName.Location = new System.Drawing.Point(209, 53);
            this.lblCashierName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCashierName.Name = "lblCashierName";
            this.lblCashierName.Size = new System.Drawing.Size(100, 17);
            this.lblCashierName.TabIndex = 120;
            this.lblCashierName.Text = "Cashier Name :";
            // 
            // lblCashNo
            // 
            this.lblCashNo.AutoSize = true;
            this.lblCashNo.BackColor = System.Drawing.Color.White;
            this.lblCashNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblCashNo.Location = new System.Drawing.Point(419, 53);
            this.lblCashNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCashNo.Name = "lblCashNo";
            this.lblCashNo.Size = new System.Drawing.Size(97, 17);
            this.lblCashNo.TabIndex = 121;
            this.lblCashNo.Text = "Cash Number :";
            // 
            // dgvCloseCash
            // 
            this.dgvCloseCash.AllowUserToAddRows = false;
            this.dgvCloseCash.AllowUserToDeleteRows = false;
            this.dgvCloseCash.AllowUserToResizeColumns = false;
            this.dgvCloseCash.AllowUserToResizeRows = false;
            this.dgvCloseCash.BackgroundColor = System.Drawing.Color.White;
            this.dgvCloseCash.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCloseCash.Location = new System.Drawing.Point(38, 204);
            this.dgvCloseCash.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCloseCash.Name = "dgvCloseCash";
            this.dgvCloseCash.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvCloseCash.RowTemplate.Height = 24;
            this.dgvCloseCash.Size = new System.Drawing.Size(353, 297);
            this.dgvCloseCash.TabIndex = 122;
            this.dgvCloseCash.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCloseCash_CellEndEdit);
            this.dgvCloseCash.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCloseCash_CellValidating);
            this.dgvCloseCash.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvCloseCash_ColumnAdded);
            this.dgvCloseCash.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCloseCash_DataBindingComplete);
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(443, 550);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(140, 29);
            this.btnPrint.TabIndex = 123;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnPrint.MouseEnter += new System.EventHandler(this.btnOpenCash_MouseEnter);
            this.btnPrint.MouseLeave += new System.EventHandler(this.btnOpenCash_MouseLeave);
            // 
            // btnPreview
            // 
            this.btnPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.Enabled = false;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPreview.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.Location = new System.Drawing.Point(298, 550);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(140, 29);
            this.btnPreview.TabIndex = 124;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            this.btnPreview.MouseEnter += new System.EventHandler(this.btnOpenCash_MouseEnter);
            this.btnPreview.MouseLeave += new System.EventHandler(this.btnOpenCash_MouseLeave);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblTotal.Location = new System.Drawing.Point(165, 517);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 17);
            this.lblTotal.TabIndex = 125;
            this.lblTotal.Text = "Total:";
            // 
            // txtTotalValue
            // 
            this.txtTotalValue.Enabled = false;
            this.txtTotalValue.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalValue.Location = new System.Drawing.Point(229, 514);
            this.txtTotalValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotalValue.Name = "txtTotalValue";
            this.txtTotalValue.Size = new System.Drawing.Size(162, 25);
            this.txtTotalValue.TabIndex = 126;
            // 
            // txtCashNo
            // 
            this.txtCashNo.BackColor = System.Drawing.Color.White;
            this.txtCashNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashNo.Location = new System.Drawing.Point(422, 75);
            this.txtCashNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCashNo.Name = "txtCashNo";
            this.txtCashNo.ReadOnly = true;
            this.txtCashNo.Size = new System.Drawing.Size(162, 25);
            this.txtCashNo.TabIndex = 128;
            // 
            // txtCashierName
            // 
            this.txtCashierName.BackColor = System.Drawing.Color.White;
            this.txtCashierName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashierName.Location = new System.Drawing.Point(212, 75);
            this.txtCashierName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCashierName.Name = "txtCashierName";
            this.txtCashierName.ReadOnly = true;
            this.txtCashierName.Size = new System.Drawing.Size(162, 25);
            this.txtCashierName.TabIndex = 129;
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.Enabled = false;
            this.cmbShop.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Items.AddRange(new object[] {
            "Active",
            "InActive"});
            this.cmbShop.Location = new System.Drawing.Point(15, 75);
            this.cmbShop.Margin = new System.Windows.Forms.Padding(2);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(162, 25);
            this.cmbShop.TabIndex = 130;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(406, 204);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(192, 104);
            this.listView1.TabIndex = 131;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // grpPettyCashBAL
            // 
            this.grpPettyCashBAL.BackColor = System.Drawing.Color.Transparent;
            this.grpPettyCashBAL.Controls.Add(this.lblPettyCashBAL);
            this.grpPettyCashBAL.Controls.Add(this.label2);
            this.grpPettyCashBAL.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.grpPettyCashBAL.Location = new System.Drawing.Point(324, 133);
            this.grpPettyCashBAL.Margin = new System.Windows.Forms.Padding(2);
            this.grpPettyCashBAL.Name = "grpPettyCashBAL";
            this.grpPettyCashBAL.Padding = new System.Windows.Forms.Padding(2);
            this.grpPettyCashBAL.Size = new System.Drawing.Size(281, 58);
            this.grpPettyCashBAL.TabIndex = 120;
            this.grpPettyCashBAL.TabStop = false;
            this.grpPettyCashBAL.Text = "Petty Cash Balance";
            // 
            // lblPettyCashBAL
            // 
            this.lblPettyCashBAL.AutoSize = true;
            this.lblPettyCashBAL.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.lblPettyCashBAL.Location = new System.Drawing.Point(196, 29);
            this.lblPettyCashBAL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPettyCashBAL.Name = "lblPettyCashBAL";
            this.lblPettyCashBAL.Size = new System.Drawing.Size(15, 17);
            this.lblPettyCashBAL.TabIndex = 1;
            this.lblPettyCashBAL.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.label2.Location = new System.Drawing.Point(43, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Petty Cash Balance  =  ";
            // 
            // frmCloseShifWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(628, 585);
            this.Controls.Add(this.grpPettyCashBAL);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cmbShop);
            this.Controls.Add(this.txtCashierName);
            this.Controls.Add(this.txtCashNo);
            this.Controls.Add(this.txtTotalValue);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvCloseCash);
            this.Controls.Add(this.lblCashNo);
            this.Controls.Add(this.lblCashierName);
            this.Controls.Add(this.lblShop);
            this.Controls.Add(this.grpReturned);
            this.Controls.Add(this.btnCloseCash);
            this.Controls.Add(this.btnOpenCash);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCloseShifWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Close Shif Window";
            this.Load += new System.EventHandler(this.frmCloseShifWindow_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpReturned.ResumeLayout(false);
            this.grpReturned.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloseCash)).EndInit();
            this.grpPettyCashBAL.ResumeLayout(false);
            this.grpPettyCashBAL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCloseCash;
        private System.Windows.Forms.Button btnOpenCash;
        private System.Windows.Forms.GroupBox grpReturned;
        private System.Windows.Forms.Label lblReturned;
        private System.Windows.Forms.Label lblReturnedAmount;
        private System.Windows.Forms.Label lblShop;
        private System.Windows.Forms.Label lblCashierName;
        private System.Windows.Forms.Label lblCashNo;
        private System.Windows.Forms.DataGridView dgvCloseCash;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotalValue;
        private System.Windows.Forms.TextBox txtCashNo;
        private System.Windows.Forms.TextBox txtCashierName;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox grpPettyCashBAL;
        private System.Windows.Forms.Label lblPettyCashBAL;
        private System.Windows.Forms.Label label2;
    }
}