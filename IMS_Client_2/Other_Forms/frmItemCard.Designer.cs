namespace IMS_Client_2.Other_Forms
{
    partial class frmItemCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemCard));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.rdSearchByBarCode = new System.Windows.Forms.RadioButton();
            this.txtSearchByBarCode = new System.Windows.Forms.TextBox();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.rdSearchByModelNo = new System.Windows.Forms.RadioButton();
            this.rdSearchByItemName = new System.Windows.Forms.RadioButton();
            this.txtSearchByItemName = new System.Windows.Forms.TextBox();
            this.txtSearchByModelNo = new System.Windows.Forms.TextBox();
            this.dgvPriceDetails = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReceivedQty = new System.Windows.Forms.TextBox();
            this.txtSoldQty = new System.Windows.Forms.TextBox();
            this.txtCurrentQty = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSoldPer = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCurrentStockCost = new System.Windows.Forms.TextBox();
            this.txtSoldCost = new System.Windows.Forms.TextBox();
            this.txtReceivedCost = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtRevenue = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProfitMargin = new System.Windows.Forms.TextBox();
            this.txtAchivedMarginCost = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.panel2.Size = new System.Drawing.Size(971, 40);
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
            this.label12.Size = new System.Drawing.Size(94, 22);
            this.label12.TabIndex = 82;
            this.label12.Text = "Item Card";
            // 
            // grpFilter
            // 
            this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilter.BackColor = System.Drawing.Color.Transparent;
            this.grpFilter.Controls.Add(this.rdSearchByBarCode);
            this.grpFilter.Controls.Add(this.txtSearchByBarCode);
            this.grpFilter.Controls.Add(this.txtProductID);
            this.grpFilter.Controls.Add(this.rdSearchByModelNo);
            this.grpFilter.Controls.Add(this.rdSearchByItemName);
            this.grpFilter.Controls.Add(this.txtSearchByItemName);
            this.grpFilter.Controls.Add(this.txtSearchByModelNo);
            this.grpFilter.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilter.Location = new System.Drawing.Point(5, 45);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(954, 119);
            this.grpFilter.TabIndex = 200;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter By";
            // 
            // rdSearchByBarCode
            // 
            this.rdSearchByBarCode.AutoSize = true;
            this.rdSearchByBarCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByBarCode.Location = new System.Drawing.Point(385, 27);
            this.rdSearchByBarCode.Name = "rdSearchByBarCode";
            this.rdSearchByBarCode.Size = new System.Drawing.Size(108, 21);
            this.rdSearchByBarCode.TabIndex = 238;
            this.rdSearchByBarCode.Text = "BarCode No :";
            this.rdSearchByBarCode.UseVisualStyleBackColor = true;
            this.rdSearchByBarCode.CheckedChanged += new System.EventHandler(this.rdSearchByBarCode_CheckedChanged);
            // 
            // txtSearchByBarCode
            // 
            this.txtSearchByBarCode.BackColor = System.Drawing.Color.White;
            this.txtSearchByBarCode.Enabled = false;
            this.txtSearchByBarCode.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByBarCode.Location = new System.Drawing.Point(501, 25);
            this.txtSearchByBarCode.Name = "txtSearchByBarCode";
            this.txtSearchByBarCode.Size = new System.Drawing.Size(207, 25);
            this.txtSearchByBarCode.TabIndex = 237;
            this.txtSearchByBarCode.Enter += new System.EventHandler(this.txtModelNo_Enter);
            this.txtSearchByBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchByBarCode_KeyDown);
            this.txtSearchByBarCode.Leave += new System.EventHandler(this.txtModelNo_Leave);
            // 
            // txtProductID
            // 
            this.txtProductID.BackColor = System.Drawing.Color.White;
            this.txtProductID.Location = new System.Drawing.Point(777, 64);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(38, 25);
            this.txtProductID.TabIndex = 236;
            this.txtProductID.Visible = false;
            // 
            // rdSearchByModelNo
            // 
            this.rdSearchByModelNo.AutoSize = true;
            this.rdSearchByModelNo.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByModelNo.Location = new System.Drawing.Point(21, 27);
            this.rdSearchByModelNo.Name = "rdSearchByModelNo";
            this.rdSearchByModelNo.Size = new System.Drawing.Size(92, 21);
            this.rdSearchByModelNo.TabIndex = 202;
            this.rdSearchByModelNo.Text = "Model No :";
            this.rdSearchByModelNo.UseVisualStyleBackColor = true;
            this.rdSearchByModelNo.CheckedChanged += new System.EventHandler(this.rdSearchByModelNo_CheckedChanged);
            // 
            // rdSearchByItemName
            // 
            this.rdSearchByItemName.AutoSize = true;
            this.rdSearchByItemName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSearchByItemName.Location = new System.Drawing.Point(400, 66);
            this.rdSearchByItemName.Name = "rdSearchByItemName";
            this.rdSearchByItemName.Size = new System.Drawing.Size(100, 21);
            this.rdSearchByItemName.TabIndex = 201;
            this.rdSearchByItemName.Text = "Item Name :";
            this.rdSearchByItemName.UseVisualStyleBackColor = true;
            this.rdSearchByItemName.Visible = false;
            this.rdSearchByItemName.CheckedChanged += new System.EventHandler(this.rdSearchByItemName_CheckedChanged);
            // 
            // txtSearchByItemName
            // 
            this.txtSearchByItemName.BackColor = System.Drawing.Color.White;
            this.txtSearchByItemName.Enabled = false;
            this.txtSearchByItemName.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByItemName.Location = new System.Drawing.Point(504, 64);
            this.txtSearchByItemName.Name = "txtSearchByItemName";
            this.txtSearchByItemName.Size = new System.Drawing.Size(207, 25);
            this.txtSearchByItemName.TabIndex = 200;
            this.txtSearchByItemName.Visible = false;
            this.txtSearchByItemName.TextChanged += new System.EventHandler(this.txtSearchByItemName_TextChanged);
            this.txtSearchByItemName.Enter += new System.EventHandler(this.txtModelNo_Enter);
            this.txtSearchByItemName.Leave += new System.EventHandler(this.txtModelNo_Leave);
            // 
            // txtSearchByModelNo
            // 
            this.txtSearchByModelNo.BackColor = System.Drawing.Color.White;
            this.txtSearchByModelNo.Enabled = false;
            this.txtSearchByModelNo.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSearchByModelNo.Location = new System.Drawing.Point(119, 25);
            this.txtSearchByModelNo.Name = "txtSearchByModelNo";
            this.txtSearchByModelNo.Size = new System.Drawing.Size(207, 25);
            this.txtSearchByModelNo.TabIndex = 198;
            this.txtSearchByModelNo.TextChanged += new System.EventHandler(this.txtSearchByModelNo_TextChanged);
            this.txtSearchByModelNo.Enter += new System.EventHandler(this.txtModelNo_Enter);
            this.txtSearchByModelNo.Leave += new System.EventHandler(this.txtModelNo_Leave);
            // 
            // dgvPriceDetails
            // 
            this.dgvPriceDetails.AllowUserToAddRows = false;
            this.dgvPriceDetails.AllowUserToDeleteRows = false;
            this.dgvPriceDetails.AllowUserToResizeColumns = false;
            this.dgvPriceDetails.AllowUserToResizeRows = false;
            this.dgvPriceDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPriceDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPriceDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvPriceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPriceDetails.Location = new System.Drawing.Point(5, 170);
            this.dgvPriceDetails.Name = "dgvPriceDetails";
            this.dgvPriceDetails.ReadOnly = true;
            this.dgvPriceDetails.RowHeadersVisible = false;
            this.dgvPriceDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPriceDetails.Size = new System.Drawing.Size(959, 80);
            this.dgvPriceDetails.TabIndex = 259;
            this.dgvPriceDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPriceDetails_CellClick);
            this.dgvPriceDetails.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPriceDetails_DataBindingComplete);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 374);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(717, 270);
            this.dataGridView1.TabIndex = 260;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.picProduct);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(728, 374);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 270);
            this.groupBox2.TabIndex = 261;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Item Photo";
            // 
            // picProduct
            // 
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picProduct.Location = new System.Drawing.Point(3, 21);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(235, 246);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 209;
            this.picProduct.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 201;
            this.label2.Text = "Received";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 262;
            this.label3.Text = "Sold";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 263;
            this.label4.Text = "Current Stock";
            // 
            // txtReceivedQty
            // 
            this.txtReceivedQty.BackColor = System.Drawing.Color.White;
            this.txtReceivedQty.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtReceivedQty.Location = new System.Drawing.Point(136, 15);
            this.txtReceivedQty.Name = "txtReceivedQty";
            this.txtReceivedQty.ReadOnly = true;
            this.txtReceivedQty.Size = new System.Drawing.Size(73, 25);
            this.txtReceivedQty.TabIndex = 201;
            // 
            // txtSoldQty
            // 
            this.txtSoldQty.BackColor = System.Drawing.Color.White;
            this.txtSoldQty.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSoldQty.Location = new System.Drawing.Point(136, 43);
            this.txtSoldQty.Name = "txtSoldQty";
            this.txtSoldQty.ReadOnly = true;
            this.txtSoldQty.Size = new System.Drawing.Size(73, 25);
            this.txtSoldQty.TabIndex = 264;
            // 
            // txtCurrentQty
            // 
            this.txtCurrentQty.BackColor = System.Drawing.Color.White;
            this.txtCurrentQty.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtCurrentQty.Location = new System.Drawing.Point(136, 73);
            this.txtCurrentQty.Name = "txtCurrentQty";
            this.txtCurrentQty.ReadOnly = true;
            this.txtCurrentQty.Size = new System.Drawing.Size(73, 25);
            this.txtCurrentQty.TabIndex = 265;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtSoldPer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCurrentQty);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSoldQty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtReceivedQty);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 106);
            this.groupBox1.TabIndex = 262;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Units";
            // 
            // txtSoldPer
            // 
            this.txtSoldPer.BackColor = System.Drawing.Color.White;
            this.txtSoldPer.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSoldPer.Location = new System.Drawing.Point(50, 44);
            this.txtSoldPer.Name = "txtSoldPer";
            this.txtSoldPer.ReadOnly = true;
            this.txtSoldPer.Size = new System.Drawing.Size(67, 25);
            this.txtSoldPer.TabIndex = 266;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.txtCurrentStockCost);
            this.groupBox4.Controls.Add(this.txtSoldCost);
            this.groupBox4.Controls.Add(this.txtReceivedCost);
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(254, 259);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(130, 106);
            this.groupBox4.TabIndex = 267;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cost";
            // 
            // txtCurrentStockCost
            // 
            this.txtCurrentStockCost.BackColor = System.Drawing.Color.White;
            this.txtCurrentStockCost.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtCurrentStockCost.Location = new System.Drawing.Point(11, 75);
            this.txtCurrentStockCost.Name = "txtCurrentStockCost";
            this.txtCurrentStockCost.ReadOnly = true;
            this.txtCurrentStockCost.Size = new System.Drawing.Size(98, 25);
            this.txtCurrentStockCost.TabIndex = 265;
            // 
            // txtSoldCost
            // 
            this.txtSoldCost.BackColor = System.Drawing.Color.White;
            this.txtSoldCost.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtSoldCost.Location = new System.Drawing.Point(11, 45);
            this.txtSoldCost.Name = "txtSoldCost";
            this.txtSoldCost.ReadOnly = true;
            this.txtSoldCost.Size = new System.Drawing.Size(98, 25);
            this.txtSoldCost.TabIndex = 264;
            // 
            // txtReceivedCost
            // 
            this.txtReceivedCost.BackColor = System.Drawing.Color.White;
            this.txtReceivedCost.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtReceivedCost.Location = new System.Drawing.Point(11, 16);
            this.txtReceivedCost.Name = "txtReceivedCost";
            this.txtReceivedCost.ReadOnly = true;
            this.txtReceivedCost.Size = new System.Drawing.Size(98, 25);
            this.txtReceivedCost.TabIndex = 201;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.txtRevenue);
            this.groupBox5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(411, 260);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(150, 106);
            this.groupBox5.TabIndex = 268;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Revenue";
            // 
            // txtRevenue
            // 
            this.txtRevenue.BackColor = System.Drawing.Color.White;
            this.txtRevenue.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtRevenue.Location = new System.Drawing.Point(11, 45);
            this.txtRevenue.Name = "txtRevenue";
            this.txtRevenue.ReadOnly = true;
            this.txtRevenue.Size = new System.Drawing.Size(120, 25);
            this.txtRevenue.TabIndex = 264;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.txtProfitMargin);
            this.groupBox6.Controls.Add(this.txtAchivedMarginCost);
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(583, 260);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(381, 106);
            this.groupBox6.TabIndex = 269;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Margin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 266;
            this.label6.Text = "Profit Margin  :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 17);
            this.label5.TabIndex = 201;
            this.label5.Text = "Achived Margin Cost :";
            // 
            // txtProfitMargin
            // 
            this.txtProfitMargin.BackColor = System.Drawing.Color.White;
            this.txtProfitMargin.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtProfitMargin.Location = new System.Drawing.Point(191, 63);
            this.txtProfitMargin.Name = "txtProfitMargin";
            this.txtProfitMargin.ReadOnly = true;
            this.txtProfitMargin.Size = new System.Drawing.Size(149, 25);
            this.txtProfitMargin.TabIndex = 265;
            // 
            // txtAchivedMarginCost
            // 
            this.txtAchivedMarginCost.BackColor = System.Drawing.Color.White;
            this.txtAchivedMarginCost.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.txtAchivedMarginCost.Location = new System.Drawing.Point(190, 24);
            this.txtAchivedMarginCost.Name = "txtAchivedMarginCost";
            this.txtAchivedMarginCost.ReadOnly = true;
            this.txtAchivedMarginCost.Size = new System.Drawing.Size(149, 25);
            this.txtAchivedMarginCost.TabIndex = 264;
            // 
            // frmItemCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS_Client_2.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(971, 650);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dgvPriceDetails);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmItemCard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Card";
            this.Load += new System.EventHandler(this.frmItemCard_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TextBox txtSearchByModelNo;
        private System.Windows.Forms.TextBox txtSearchByItemName;
        private System.Windows.Forms.DataGridView dgvPriceDetails;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReceivedQty;
        private System.Windows.Forms.TextBox txtSoldQty;
        private System.Windows.Forms.TextBox txtCurrentQty;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSoldPer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtCurrentStockCost;
        private System.Windows.Forms.TextBox txtSoldCost;
        private System.Windows.Forms.TextBox txtReceivedCost;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtRevenue;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtProfitMargin;
        private System.Windows.Forms.TextBox txtAchivedMarginCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdSearchByModelNo;
        private System.Windows.Forms.RadioButton rdSearchByItemName;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.RadioButton rdSearchByBarCode;
        private System.Windows.Forms.TextBox txtSearchByBarCode;
    }
}