using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTLProductLine
{
    public partial class TParameterForm : Form
    {
        public static string dz_material = "2014";
        public static string kq_material = "2014";

        public static string dz_kind = "A";
        public TParameterForm()
        {
            InitializeComponent();
        }

        private void TParameterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void TParameterForm_Load(object sender, EventArgs e)
        {
            initializeFunction();
        }

        private void initializeFunction()
        {
            //dzTypeComboBox.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            dzKindcomboBox.Items.AddRange(new object[] { "A", "B" });
            dzTypeComboBox.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
            kqTypeComboBox.Items.AddRange(new object[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

            kqMaterialTextBox.Text = kq_material;
            dzMaterialTextBox.Text = dz_material;
            
            dzTypeComboBox.SelectedIndex = 0;
            kqTypeComboBox.SelectedIndex = 0;
            dzKindcomboBox.SelectedIndex = 0;          
        }

        private void parOkButton_Click(object sender, EventArgs e)
        {
            //STOP thread
            for(int i =0;i<8;i++)
            {
                TComPort.stopDZ[i] = false;
                TComPort.stopKQ[i] = false;
            }
            //kq
            string kqKind = kqTypeComboBox.Text;
            if (kqKind == "A")
                TComPort.WriteDataToPLC(1, 10, "WD10170001");
            else if (kqKind == "B")
                TComPort.WriteDataToPLC(1, 10, "WD10170002");
            else if (kqKind == "C")
                TComPort.WriteDataToPLC(1, 10, "WD10170003");
            else if (kqKind == "D")
                TComPort.WriteDataToPLC(1, 10, "WD10170004");
            else if (kqKind == "E")
                TComPort.WriteDataToPLC(1, 10, "WD10170005");
            else if (kqKind == "F")
                TComPort.WriteDataToPLC(1, 10, "WD10170006");
            else if (kqKind == "G")
                TComPort.WriteDataToPLC(1, 10, "WD10170007");
            else if (kqKind == "H")
                TComPort.WriteDataToPLC(1, 10, "WD10170008");
            else if (kqKind == "I")
                TComPort.WriteDataToPLC(1, 10, "WD10170009");
            else if (kqKind == "J")
                TComPort.WriteDataToPLC(1, 10, "WD1017000A");
            else if (kqKind == "K")
                TComPort.WriteDataToPLC(1, 10, "WD1017000B");
            else if (kqKind == "L")
                TComPort.WriteDataToPLC(1, 10, "WD1017000C");
            else if (kqKind == "M")
                TComPort.WriteDataToPLC(1, 10, "WD1017000D");
            else if (kqKind == "N")
                TComPort.WriteDataToPLC(1, 10, "WD1017000E");
            else if (kqKind == "O")
                TComPort.WriteDataToPLC(1, 10, "WD1017000F");
            else if (kqKind == "P")
                TComPort.WriteDataToPLC(1, 10, "WD10170010");
            else if (kqKind == "Q")
                TComPort.WriteDataToPLC(1, 10, "WD10170011");
            else if (kqKind == "R")
                TComPort.WriteDataToPLC(1, 10, "WD10170012");
            else if (kqKind == "S")
                TComPort.WriteDataToPLC(1, 10, "WD10170013");
            else if (kqKind == "T")
                TComPort.WriteDataToPLC(1, 10, "WD10170014");
            else if (kqKind == "U")
                TComPort.WriteDataToPLC(1, 10, "WD10170015");
            else if (kqKind == "V")
                TComPort.WriteDataToPLC(1, 10, "WD10170016");
            else if (kqKind == "W")
                TComPort.WriteDataToPLC(1, 10, "WD10170017");
            else if (kqKind == "X")
                TComPort.WriteDataToPLC(1, 10, "WD10170018");
            else if (kqKind == "Y")
                TComPort.WriteDataToPLC(1, 10, "WD10170019");
            else if (kqKind == "Z")
                TComPort.WriteDataToPLC(1, 10, "WD1017001A");
            //dz.dz kind
            dz_kind = dzKindcomboBox.Text;
           
            if (dzKindcomboBox.Text == "A")
            {
                //D1018=1 used to set kind of dz in equipment 1(op05),2(op10),4(op35)
                TComPort.WriteDataToPLC(2, 1, "WD10180001");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 2, "WD10180001");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 4, "WD10180001");
                delayTime(0.2);

                TComPort.WriteDataToPLC(2, 1, "WD10180001");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 2, "WD10180001");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 4, "WD10180001");
                delayTime(0.2);
            }
            else if (dzKindcomboBox.Text == "B")
            {
                //D1018=2 used to set kind of dz in equipment 1(op05),2(op10),4(op35)
                TComPort.WriteDataToPLC(2, 1, "WD10180002");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 2, "WD10180002");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 4, "WD10180002");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 1, "WD10180002");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 2, "WD10180002");
                delayTime(0.2);
                TComPort.WriteDataToPLC(2, 4, "WD10180002");
                delayTime(0.2);
            }
            //dz type
            string dzType = dzTypeComboBox.Text;
            if (dzType == "A")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170001");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170001");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "B")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170002");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170002");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "C")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170003");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170003");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "D")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170004");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170004");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "E")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170005");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170005");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "F")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170006");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170006");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "G")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170007");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170007");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "H")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170008");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170008");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "I")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170009");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170009");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "J")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000A");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000A");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "K")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000B");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000B");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "L")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000C");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000C");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "M")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000D");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000D");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "N")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000E");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000E");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "O")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017000F");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017000F");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "P")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170010");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170010");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "Q")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170011");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170011");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "R")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170012");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170012");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "S")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170013");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170013");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "T")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170014");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170014");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "U")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170015");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170015");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "V")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170016");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170016");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "W")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170017");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170017");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "X")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170018");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170018");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "Y")
            {
                TComPort.WriteDataToPLC(2, 1, "WD10170019");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD10170019");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            else if (dzType == "Z")
            {
                TComPort.WriteDataToPLC(2, 1, "WD1017001A");//D1017=1...8 used to set type of dz in equipment 4(op10)
                delayTime(0.5);
                TComPort.WriteDataToPLC(2, 4, "WD1017001A");//D1017=1...8 used to set type of dz in equipment 4(op35)
            }
            //START thread
            for (int i = 0; i < 8; i++)
            {
                TComPort.stopDZ[i] = true;
                TComPort.stopKQ[i] = true;
            }
            MessageBox.Show("信息已保存！");
        }

        private void parCancelButton_Click(object sender, EventArgs e)
        {
            dzTypeComboBox.SelectedIndex = 0;
            kqTypeComboBox.SelectedIndex = 0;
            dzKindcomboBox.SelectedIndex = 0;
        }

        private void MaterialAOKbutton_Click(object sender, EventArgs e)
        {
            if (dzMaterialTextBox.Text.Equals("") || kqMaterialTextBox.Text.Equals(""))
            {
                MessageBox.Show("请输入物料条码！");
            }
            else
            {
                dz_material = dzMaterialTextBox.Text;
                kq_material = kqMaterialTextBox.Text;
                MessageBox.Show("信息已保存！");
            }
        }

        private void MaterialCancelbutton_Click(object sender, EventArgs e)
        {
            kqMaterialTextBox.Text = "";
            dzMaterialTextBox.Text = "";
        }

        private void delayTime(double secend)
        {
            DateTime tempTime = DateTime.Now;
            while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
            Application.DoEvents();
        }
    }
}
