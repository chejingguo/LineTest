namespace BTLProductLine
{
    partial class ProductLineForm
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
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barCodeRefer = new System.Windows.Forms.ToolStripMenuItem();
            this.dateRefer = new System.Windows.Forms.ToolStripMenuItem();
            this.parameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.productUserGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.excitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.timeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DataBasePar = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.parameterToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.excitToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1284, 25);
            this.mainMenuStrip.TabIndex = 6;
            this.mainMenuStrip.Text = "帮助";
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barCodeRefer,
            this.dateRefer});
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.informationToolStripMenuItem.Text = "信息查询";
            // 
            // barCodeRefer
            // 
            this.barCodeRefer.Name = "barCodeRefer";
            this.barCodeRefer.Size = new System.Drawing.Size(124, 22);
            this.barCodeRefer.Text = "条码查询";
            this.barCodeRefer.Click += new System.EventHandler(this.informationRefer_Click);
            // 
            // dateRefer
            // 
            this.dateRefer.Name = "dateRefer";
            this.dateRefer.Size = new System.Drawing.Size(124, 22);
            this.dateRefer.Text = "日期查询";
            this.dateRefer.Click += new System.EventHandler(this.dateRefer_Click);
            // 
            // parameterToolStripMenuItem
            // 
            this.parameterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productParameter,
            this.AdvancedParameter,
            this.DataBasePar});
            this.parameterToolStripMenuItem.Name = "parameterToolStripMenuItem";
            this.parameterToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.parameterToolStripMenuItem.Text = "参数设置";
            // 
            // productParameter
            // 
            this.productParameter.Name = "productParameter";
            this.productParameter.Size = new System.Drawing.Size(152, 22);
            this.productParameter.Text = "产品参数";
            this.productParameter.Click += new System.EventHandler(this.productParameter_Click);
            // 
            // AdvancedParameter
            // 
            this.AdvancedParameter.Name = "AdvancedParameter";
            this.AdvancedParameter.Size = new System.Drawing.Size(152, 22);
            this.AdvancedParameter.Text = "高级设置";
            this.AdvancedParameter.Click += new System.EventHandler(this.AdvancedParameter_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productAbout,
            this.productUserGuide});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.helpToolStripMenuItem.Text = "帮助信息";
            // 
            // productAbout
            // 
            this.productAbout.Name = "productAbout";
            this.productAbout.Size = new System.Drawing.Size(124, 22);
            this.productAbout.Text = "关于";
            this.productAbout.Click += new System.EventHandler(this.productAbout_Click);
            // 
            // productUserGuide
            // 
            this.productUserGuide.Name = "productUserGuide";
            this.productUserGuide.Size = new System.Drawing.Size(124, 22);
            this.productUserGuide.Text = "使用说明";
            this.productUserGuide.Click += new System.EventHandler(this.productUserGuide_Click);
            // 
            // excitToolStripMenuItem
            // 
            this.excitToolStripMenuItem.Name = "excitToolStripMenuItem";
            this.excitToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.excitToolStripMenuItem.Text = "退出系统";
            this.excitToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 717);
            this.panel1.TabIndex = 9;
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeToolStripStatusLabel,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 720);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.MainStatusStrip.Size = new System.Drawing.Size(1284, 22);
            this.MainStatusStrip.TabIndex = 10;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // timeToolStripStatusLabel
            // 
            this.timeToolStripStatusLabel.Name = "timeToolStripStatusLabel";
            this.timeToolStripStatusLabel.Size = new System.Drawing.Size(152, 17);
            this.timeToolStripStatusLabel.Text = "timeToolStripStatusLabel";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(889, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(224, 17);
            this.toolStripStatusLabel1.Text = "芜湖哈特机器人产业技术研究院有限公司";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DataBasePar
            // 
            this.DataBasePar.Name = "DataBasePar";
            this.DataBasePar.Size = new System.Drawing.Size(152, 22);
            this.DataBasePar.Text = "数据管理";
            this.DataBasePar.Click += new System.EventHandler(this.DataBasePar_Click);
            // 
            // ProductLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1284, 742);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "ProductLineForm";
            this.Text = "伯特利EPB生产线系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ProductLineForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductLineForm_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productParameter;
        private System.Windows.Forms.ToolStripMenuItem barCodeRefer;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel timeToolStripStatusLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem AdvancedParameter;
        private System.Windows.Forms.ToolStripMenuItem dateRefer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem productUserGuide;
        private System.Windows.Forms.ToolStripMenuItem DataBasePar;
    }
}

