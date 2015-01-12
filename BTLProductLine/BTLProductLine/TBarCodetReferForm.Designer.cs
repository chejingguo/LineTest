namespace BTLProductLine
{
    partial class TBarCodeReferForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.inputBarCodetextBox = new System.Windows.Forms.TextBox();
            this.findBarCodebutton = new System.Windows.Forms.Button();
            this.saveBarCodebutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.barCodeRichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.barCodeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.findBarCodebutton1 = new System.Windows.Forms.Button();
            this.inputBarCodetextBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入卡钳条码：";
            // 
            // inputBarCodetextBox
            // 
            this.inputBarCodetextBox.Location = new System.Drawing.Point(110, 29);
            this.inputBarCodetextBox.Name = "inputBarCodetextBox";
            this.inputBarCodetextBox.Size = new System.Drawing.Size(100, 21);
            this.inputBarCodetextBox.TabIndex = 7;
            // 
            // findBarCodebutton
            // 
            this.findBarCodebutton.Location = new System.Drawing.Point(229, 28);
            this.findBarCodebutton.Name = "findBarCodebutton";
            this.findBarCodebutton.Size = new System.Drawing.Size(75, 23);
            this.findBarCodebutton.TabIndex = 8;
            this.findBarCodebutton.Text = "查询";
            this.findBarCodebutton.UseVisualStyleBackColor = true;
            this.findBarCodebutton.Click += new System.EventHandler(this.findBarCodebutton_Click);
            // 
            // saveBarCodebutton
            // 
            this.saveBarCodebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBarCodebutton.Location = new System.Drawing.Point(938, 28);
            this.saveBarCodebutton.Name = "saveBarCodebutton";
            this.saveBarCodebutton.Size = new System.Drawing.Size(75, 23);
            this.saveBarCodebutton.TabIndex = 9;
            this.saveBarCodebutton.Text = "打印条码";
            this.saveBarCodebutton.UseVisualStyleBackColor = true;
            this.saveBarCodebutton.Click += new System.EventHandler(this.saveBarCodebutton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.barCodeRichTextBox1);
            this.panel1.Controls.Add(this.barCodeRichTextBox);
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 572);
            this.panel1.TabIndex = 10;
            // 
            // barCodeRichTextBox1
            // 
            this.barCodeRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barCodeRichTextBox1.Location = new System.Drawing.Point(490, 4);
            this.barCodeRichTextBox1.Name = "barCodeRichTextBox1";
            this.barCodeRichTextBox1.Size = new System.Drawing.Size(568, 565);
            this.barCodeRichTextBox1.TabIndex = 1;
            this.barCodeRichTextBox1.Text = "电装条码显示区域：";
            // 
            // barCodeRichTextBox
            // 
            this.barCodeRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barCodeRichTextBox.BackColor = System.Drawing.Color.White;
            this.barCodeRichTextBox.Location = new System.Drawing.Point(0, 4);
            this.barCodeRichTextBox.Name = "barCodeRichTextBox";
            this.barCodeRichTextBox.ReadOnly = true;
            this.barCodeRichTextBox.Size = new System.Drawing.Size(484, 565);
            this.barCodeRichTextBox.TabIndex = 0;
            this.barCodeRichTextBox.Text = "卡钳条码显示区域：";
            // 
            // findBarCodebutton1
            // 
            this.findBarCodebutton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.findBarCodebutton1.Location = new System.Drawing.Point(718, 28);
            this.findBarCodebutton1.Name = "findBarCodebutton1";
            this.findBarCodebutton1.Size = new System.Drawing.Size(75, 23);
            this.findBarCodebutton1.TabIndex = 14;
            this.findBarCodebutton1.Text = "查询";
            this.findBarCodebutton1.UseVisualStyleBackColor = true;
            this.findBarCodebutton1.Click += new System.EventHandler(this.findBarCodebutton1_Click);
            // 
            // inputBarCodetextBox1
            // 
            this.inputBarCodetextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBarCodetextBox1.Location = new System.Drawing.Point(600, 29);
            this.inputBarCodetextBox1.Name = "inputBarCodetextBox1";
            this.inputBarCodetextBox1.Size = new System.Drawing.Size(100, 21);
            this.inputBarCodetextBox1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(502, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "请输入电装条码：";
            // 
            // TBarCodeReferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 632);
            this.Controls.Add(this.findBarCodebutton1);
            this.Controls.Add(this.inputBarCodetextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.saveBarCodebutton);
            this.Controls.Add(this.findBarCodebutton);
            this.Controls.Add(this.inputBarCodetextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "TBarCodeReferForm";
            this.ShowInTaskbar = false;
            this.Text = "条码信息查询";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TProductReferForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TProductReferForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputBarCodetextBox;
        private System.Windows.Forms.Button findBarCodebutton;
        private System.Windows.Forms.Button saveBarCodebutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox barCodeRichTextBox;
        private System.Windows.Forms.RichTextBox barCodeRichTextBox1;
        private System.Windows.Forms.Button findBarCodebutton1;
        private System.Windows.Forms.TextBox inputBarCodetextBox1;
        private System.Windows.Forms.Label label4;
    }
}