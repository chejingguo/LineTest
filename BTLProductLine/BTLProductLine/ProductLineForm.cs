using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

//custom namespace
using BTLProductLine;


namespace BTLProductLine
{
    //KQEquipmentNumber
    enum KQEquipmentNumber { OP10 = 10, OP20 = 11, OP30 = 12, OP40 = 13, OP50 = 14, OP60 = 15, OP70 = 16, OP80 = 17 };//(int)KQEquipmentNumber.OP10 convert to int

    //DataBase number of KQ Equipment
    enum KQDataBase
    {
        NO_Index = 0,  日期时间_Index = 1,  kqBarCode_Index = 2,  kqMaterial_Index = 3,  dzBarCode_Index = 4,  dzMaterial_Index = 5, 
        KQ10_Index = 6, KQ10_RealTimePresure_Index=7, KQ10_MaxDisplacement_Index=8, 
        KQ20_Flag_Index = 9,  KQ20_SlipResistance_Index=10, KQ20_MaxPresure_Index=11,
        KQ30_Flag_Index = 12,  KQ30_LowDifPressure_Index=13,
        KQ40_Flag_Index = 14,  KQ40_HighDifPressure_Index=15,
        KQ50_Flag_Index = 16,
        KQ60_Flag_Index = 17, KQ60_ClampForce_Index = 18,
        KQ70_Flag_Index = 19, KQ70_Torque1_Index = 20, KQ70_Angle1_Index = 21, KQ70_Torque2_Index = 22, KQ70_Angle2_Index = 23,
        KQ80_Flag_Index = 24, KQ80_MaxSlipResistance_Index = 25
    };

    //DZEquipmentNumber
    enum DZEquipmentNumber { DZ05 = 1, DZ10 = 2, DZ20 = 3, DZ35 = 4, DZ40 = 5, DZ50 = 6, DZ60 = 7, DZ70 = 8 };

    //DataBase number of DZ Equipment
    enum DZDataBase 
    {
        NO_Index = 0, 日期时间_Index = 1, dzBarCode1_Index = 2, dzBarCode2_Index = 3, dzMaterial_Index = 4, 
        DZ05_Flag_Index = 5, 
        DZ10_Flag_Index = 6,
        DZ20_Flag_Index = 7, DZ20_CentaMaxForce_Index=8, DZ20_MaxDisplacement_Index=9,	DZ20_SynWheelMaxForce_Index=10,
        DZ35_Flag_Index = 11,
        DZ40_Flag_Index = 12,
        DZ50_Flag_Index = 13,
        DZ60_Flag_Index = 14, DZ60_LeakageValue_Index = 15,
        DZ70_Flag_Index = 16, DZ70_StartingCurrent_Index = 17, DZ70_NoloadCurrent_Index = 18, DZ70_LoadCurrent_Index = 19,
    };

    //Serial Ports number
    enum ComNumber { COM10 = 1, COM9 = 2, COM11 = 3 }; 

    public partial class ProductLineForm : Form
    {
        public ProductLineForm()
        {
            InitializeComponent();
        }

        private void ProductLineForm_Load(object sender, EventArgs e)
        {
            //load  background image
            this.panel1.BackgroundImage = System.Drawing.Image.FromFile(System.Windows.Forms.Application.StartupPath + "\\123.jpg");
            //KQ
            TComPort.WriteDataToPLC((int)ComNumber.COM10, 10, "WD10170001");//KQ Kind :A;
            delayTime(0.2);
            //DZ D1018=1 used to set kind of dz in equipment 1(op05),2(op10),4(op35),defaut is A;
            TComPort.WriteDataToPLC((int)ComNumber.COM9, 1, "WD10180001");
            delayTime(0.2);
            TComPort.WriteDataToPLC((int)ComNumber.COM9, 2, "WD10180001");
            delayTime(0.2);
            TComPort.WriteDataToPLC((int)ComNumber.COM9, 4, "WD10180001");

            delayTime(0.2);
            TComPort.WriteDataToPLC((int)ComNumber.COM9, 1, "WD10170001");//D1017=1...8 used to set type of dz in equipment 1(DZ05);
            delayTime(0.2);
            TComPort.WriteDataToPLC((int)ComNumber.COM9, 4, "WD10170001");//D1017=1...8 used to set type of dz in equipment 4(DZ35)


            //Thread define and initialize
            ThreadStart worker1 = new ThreadStart(TComPort.ReadEquipment);
            Thread threadEquipment = new Thread(worker1);
            threadEquipment.IsBackground = true;
            threadEquipment.Start();
        }

        private void productParameter_Click(object sender, EventArgs e)
        {
            TParameterForm tParameterForm = new TParameterForm();
            tParameterForm.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TComPort.ClosePort();
            this.Close();
        }

        private void productAbout_Click(object sender, EventArgs e)
        {
            TProductAboutBox tProductAboutBox = new TProductAboutBox();
            tProductAboutBox.Show();
        }

        private void informationRefer_Click(object sender, EventArgs e)
        {
            TBarCodeReferForm tProductReferForm = new TBarCodeReferForm();
            tProductReferForm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MainStatusStrip.Items[0].Text = "系统时间:"+DateTime.Now.ToString();
        }

        private void AdvancedParameter_Click(object sender, EventArgs e)
        {
            TAdvancedForm tAdvancedForm = new TAdvancedForm();
            tAdvancedForm.ShowDialog();
        }

        private void dateRefer_Click(object sender, EventArgs e)
        {
            TDateReferForm tDateReferForm = new TDateReferForm();
            tDateReferForm.ShowDialog();
        }
        private void delayTime(double secend)
        {
            DateTime tempTime = DateTime.Now;
            while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
                Application.DoEvents();
        }

        private void productUserGuide_Click(object sender, EventArgs e)
        {
            string helpfile = "EPB_Info.chm";
            Help.ShowHelp(this, helpfile);

        }
        private void ProductLineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //update loaationAllData from location
            //KQ
            string sql="";
            sql = "insert into 卡钳全部数据 select * from 卡钳数据 where KQ80合格标志=true";
            TDataBase.AdjustData(sql);
            sql = "delete * From 卡钳数据 where KQ80合格标志=true";
            TDataBase.AdjustData(sql);
            //DZ
            sql = "insert into 电装全部数据 select * from 电装数据 where DZ70合格标志=true";
            TDataBase.AdjustData(sql);
            sql = "delete * From 电装数据 where DZ70合格标志=true";
            TDataBase.AdjustData(sql);

            //clear parameter
            XmlClass xmlClass = new XmlClass();
            xmlClass.WriteNameSectionValue("AB", "KQ10", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ20", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ30", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ40", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ50", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ60", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ70", "userflag", "False");
            xmlClass.WriteNameSectionValue("AB", "KQ80", "userflag", "False");

            xmlClass.WriteNameSectionValue("A", "DZ05", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ10", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ20", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ35", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ40", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ50", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ60", "userflag", "False");
            xmlClass.WriteNameSectionValue("A", "DZ70", "userflag", "False");

            xmlClass.WriteNameSectionValue("B", "DZ05", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ10", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ20", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ35", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ40", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ50", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ60", "userflag", "False");
            xmlClass.WriteNameSectionValue("B", "DZ70", "userflag", "False");
        }

        private void DataBasePar_Click(object sender, EventArgs e)
        {
            TDataBaseManageForm tDataBaseManageForm = new TDataBaseManageForm();
            tDataBaseManageForm.ShowDialog();
        }
    }
}
