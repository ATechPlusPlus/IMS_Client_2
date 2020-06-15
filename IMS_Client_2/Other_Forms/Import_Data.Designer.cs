﻿namespace IMS.Other_Forms
{
    partial class Import_Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Import_Data));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.lkbBrowseForProductData = new System.Windows.Forms.LinkLabel();
            this.lkbCancelForProductData = new System.Windows.Forms.LinkLabel();
            this.btnImportProductMaster = new System.Windows.Forms.Button();
            this.txtImportProductData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbnTruncate = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(793, 49);
            this.panel2.TabIndex = 217;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(15, 11);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(257, 26);
            this.label12.TabIndex = 82;
            this.label12.Text = "Import Data from Excel";
            // 
            // lkbBrowseForProductData
            // 
            this.lkbBrowseForProductData.AutoSize = true;
            this.lkbBrowseForProductData.BackColor = System.Drawing.Color.Transparent;
            this.lkbBrowseForProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkbBrowseForProductData.Location = new System.Drawing.Point(379, 105);
            this.lkbBrowseForProductData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lkbBrowseForProductData.Name = "lkbBrowseForProductData";
            this.lkbBrowseForProductData.Size = new System.Drawing.Size(82, 21);
            this.lkbBrowseForProductData.TabIndex = 218;
            this.lkbBrowseForProductData.TabStop = true;
            this.lkbBrowseForProductData.Text = "Browse...";
            this.lkbBrowseForProductData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkbBrowseForProductData_LinkClicked);
            // 
            // lkbCancelForProductData
            // 
            this.lkbCancelForProductData.AutoSize = true;
            this.lkbCancelForProductData.BackColor = System.Drawing.Color.Transparent;
            this.lkbCancelForProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lkbCancelForProductData.Location = new System.Drawing.Point(483, 105);
            this.lkbCancelForProductData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lkbCancelForProductData.Name = "lkbCancelForProductData";
            this.lkbCancelForProductData.Size = new System.Drawing.Size(61, 21);
            this.lkbCancelForProductData.TabIndex = 220;
            this.lkbCancelForProductData.TabStop = true;
            this.lkbCancelForProductData.Text = "Cancel";
            this.lkbCancelForProductData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkbCancelForProductData_LinkClicked);
            // 
            // btnImportProductMaster
            // 
            this.btnImportProductMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImportProductMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportProductMaster.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.btnImportProductMaster.Location = new System.Drawing.Point(553, 100);
            this.btnImportProductMaster.Name = "btnImportProductMaster";
            this.btnImportProductMaster.Size = new System.Drawing.Size(180, 30);
            this.btnImportProductMaster.TabIndex = 221;
            this.btnImportProductMaster.Text = "Import Product Data";
            this.btnImportProductMaster.UseVisualStyleBackColor = true;
            this.btnImportProductMaster.Click += new System.EventHandler(this.btnImportProductMaster_Click);
            // 
            // txtImportProductData
            // 
            this.txtImportProductData.BackColor = System.Drawing.Color.White;
            this.txtImportProductData.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtImportProductData.Location = new System.Drawing.Point(20, 101);
            this.txtImportProductData.Margin = new System.Windows.Forms.Padding(4);
            this.txtImportProductData.Name = "txtImportProductData";
            this.txtImportProductData.ReadOnly = true;
            this.txtImportProductData.Size = new System.Drawing.Size(332, 29);
            this.txtImportProductData.TabIndex = 222;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 172);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 17);
            this.label1.TabIndex = 224;
            this.label1.Text = "NA";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 150);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(739, 18);
            this.progressBar1.TabIndex = 223;
            // 
            // tbnTruncate
            // 
            this.tbnTruncate.Location = new System.Drawing.Point(740, 100);
            this.tbnTruncate.Name = "tbnTruncate";
            this.tbnTruncate.Size = new System.Drawing.Size(41, 29);
            this.tbnTruncate.TabIndex = 225;
            this.tbnTruncate.Text = "Delete";
            this.tbnTruncate.UseVisualStyleBackColor = true;
            this.tbnTruncate.Click += new System.EventHandler(this.tbnTruncate_Click);
            // 
            // Import_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::IMS.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(793, 602);
            this.Controls.Add(this.tbnTruncate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtImportProductData);
            this.Controls.Add(this.btnImportProductMaster);
            this.Controls.Add(this.lkbCancelForProductData);
            this.Controls.Add(this.lkbBrowseForProductData);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Import_Data";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Data";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel lkbBrowseForProductData;
        private System.Windows.Forms.LinkLabel lkbCancelForProductData;
        private System.Windows.Forms.Button btnImportProductMaster;
        private System.Windows.Forms.TextBox txtImportProductData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button tbnTruncate;
    }
}