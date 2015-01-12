namespace BTLProductLine
{
    partial class TDataBaseManageForm
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
            this.dataBaseMenu = new System.Windows.Forms.MenuStrip();
            this.barCodeKQ = new System.Windows.Forms.TextBox();
            this.barCodeDZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddDZ = new System.Windows.Forms.Button();
            this.AddKQ = new System.Windows.Forms.Button();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.ClearDZ = new System.Windows.Forms.Button();
            this.ClearKQ = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataBaseMenu
            // 
            this.dataBaseMenu.Location = new System.Drawing.Point(0, 0);
            this.dataBaseMenu.Name = "dataBaseMenu";
            this.dataBaseMenu.Size = new System.Drawing.Size(810, 24);
            this.dataBaseMenu.TabIndex = 0;
            this.dataBaseMenu.Text = "menuStrip1";
            // 
            // barCodeKQ
            // 
            this.barCodeKQ.Location = new System.Drawing.Point(82, 173);
            this.barCodeKQ.Name = "barCodeKQ";
            this.barCodeKQ.Size = new System.Drawing.Size(100, 21);
            this.barCodeKQ.TabIndex = 1;
            // 
            // barCodeDZ
            // 
            this.barCodeDZ.Location = new System.Drawing.Point(82, 103);
            this.barCodeDZ.Name = "barCodeDZ";
            this.barCodeDZ.Size = new System.Drawing.Size(100, 21);
            this.barCodeDZ.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "卡钳条码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "电装条码：";
            // 
            // AddDZ
            // 
            this.AddDZ.Location = new System.Drawing.Point(204, 101);
            this.AddDZ.Name = "AddDZ";
            this.AddDZ.Size = new System.Drawing.Size(75, 23);
            this.AddDZ.TabIndex = 4;
            this.AddDZ.Text = "添加DZ条码";
            this.AddDZ.UseVisualStyleBackColor = true;
            this.AddDZ.Click += new System.EventHandler(this.AddDZ_Click);
            // 
            // AddKQ
            // 
            this.AddKQ.Location = new System.Drawing.Point(204, 171);
            this.AddKQ.Name = "AddKQ";
            this.AddKQ.Size = new System.Drawing.Size(75, 23);
            this.AddKQ.TabIndex = 5;
            this.AddKQ.Text = "添加KQ条码";
            this.AddKQ.UseVisualStyleBackColor = true;
            this.AddKQ.Click += new System.EventHandler(this.AddKQ_Click);
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(82, 36);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(106, 21);
            this.dateTimePickerStart.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "开始日期：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(202, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "结束日期：";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(278, 36);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(106, 21);
            this.dateTimePickerEnd.TabIndex = 6;
            // 
            // ClearDZ
            // 
            this.ClearDZ.Location = new System.Drawing.Point(407, 37);
            this.ClearDZ.Name = "ClearDZ";
            this.ClearDZ.Size = new System.Drawing.Size(75, 23);
            this.ClearDZ.TabIndex = 4;
            this.ClearDZ.Text = "清除DZ条码";
            this.ClearDZ.UseVisualStyleBackColor = true;
            this.ClearDZ.Click += new System.EventHandler(this.ClearDZ_Click);
            // 
            // ClearKQ
            // 
            this.ClearKQ.Location = new System.Drawing.Point(505, 37);
            this.ClearKQ.Name = "ClearKQ";
            this.ClearKQ.Size = new System.Drawing.Size(75, 23);
            this.ClearKQ.TabIndex = 4;
            this.ClearKQ.Text = "清除KQ条码";
            this.ClearKQ.UseVisualStyleBackColor = true;
            this.ClearKQ.Click += new System.EventHandler(this.ClearKQ_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(889, 557);
            this.panel1.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(245, 147);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(434, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TDataBaseManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 414);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.AddKQ);
            this.Controls.Add(this.ClearKQ);
            this.Controls.Add(this.ClearDZ);
            this.Controls.Add(this.AddDZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barCodeDZ);
            this.Controls.Add(this.barCodeKQ);
            this.Controls.Add(this.dataBaseMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.dataBaseMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TDataBaseManageForm";
            this.Text = "数据库管理";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip dataBaseMenu;
        private System.Windows.Forms.TextBox barCodeKQ;
        private System.Windows.Forms.TextBox barCodeDZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddDZ;
        private System.Windows.Forms.Button AddKQ;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button ClearDZ;
        private System.Windows.Forms.Button ClearKQ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}