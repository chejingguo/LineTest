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
    public partial class TAdvancedForm : Form
    {

        public TAdvancedForm()
        {
            InitializeComponent();
        }

        private void TAdvancedForm_Load(object sender, EventArgs e)
        {
            checkBoxKQ10.Enabled = false;
            checkBoxKQ50.Enabled = false;

            checkBoxDZ05.Enabled = false;
            checkBoxDZ10.Enabled = false;
            checkBoxDZ35.Enabled = false;

            XmlClass xmlClass = new XmlClass();
            checkBoxKQ10.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ10", "userflag"));
            checkBoxKQ20.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ20", "userflag"));
            checkBoxKQ30.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ30", "userflag"));
            checkBoxKQ40.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ40", "userflag"));
            checkBoxKQ50.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ50", "userflag"));
            checkBoxKQ60.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ60", "userflag"));
            checkBoxKQ70.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ70", "userflag"));
            checkBoxKQ80.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("AB", "KQ80", "userflag"));

            checkBoxDZ05.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ05", "userflag"));
            checkBoxDZ10.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ10", "userflag"));
            checkBoxDZ20.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ20", "userflag"));
            checkBoxDZ35.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ35", "userflag"));
            checkBoxDZ40.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ40", "userflag"));
            checkBoxDZ50.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ50", "userflag"));
            checkBoxDZ60.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ60", "userflag"));
            checkBoxDZ70.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue("A", "DZ70", "userflag"));

            comboBoxTypeDZ.Items.AddRange(new object[] { "A", "B" });
            comboBoxTypeDZ.SelectedIndex = 0;
        }

        private void delayTime(double secend)
        {
            DateTime tempTime = DateTime.Now;
            while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
            Application.DoEvents();
        }

        private void buttonSaveSelEqu_Click(object sender, EventArgs e)
        {
            XmlClass xmlClass = new XmlClass();
            xmlClass.WriteNameSectionValue("AB", "KQ10", "userflag", checkBoxKQ10.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ20", "userflag", checkBoxKQ20.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ30", "userflag", checkBoxKQ30.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ40", "userflag", checkBoxKQ40.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ50", "userflag", checkBoxKQ50.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ60", "userflag", checkBoxKQ60.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ70", "userflag", checkBoxKQ70.Checked.ToString());
            xmlClass.WriteNameSectionValue("AB", "KQ80", "userflag", checkBoxKQ80.Checked.ToString());

            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ05", "userflag", checkBoxDZ05.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ10", "userflag", checkBoxDZ10.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ20", "userflag", checkBoxDZ20.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ35", "userflag", checkBoxDZ35.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ40", "userflag", checkBoxDZ40.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ50", "userflag", checkBoxDZ50.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ60", "userflag", checkBoxDZ60.Checked.ToString());
            xmlClass.WriteNameSectionValue(comboBoxTypeDZ.Text, "DZ70", "userflag", checkBoxDZ70.Checked.ToString());

            System.Windows.Forms.MessageBox.Show("数据已保存！");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxPass.Text == "1qaz2wsx")
            {
                panelPass.Visible = false;
            }
            else
            {
                MessageBox.Show("密码不正确，请重新输入？");
            }
        }

        private void comboBoxTypeDZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XmlClass xmlClass = new XmlClass();
                checkBoxDZ05.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ05", "userflag"));
                checkBoxDZ10.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ10", "userflag"));
                checkBoxDZ20.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ20", "userflag"));
                checkBoxDZ35.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ35", "userflag"));
                checkBoxDZ40.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ40", "userflag"));
                checkBoxDZ50.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ50", "userflag"));
                checkBoxDZ60.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ60", "userflag"));
                checkBoxDZ70.Checked = Convert.ToBoolean(xmlClass.ReadNameSectionValue(comboBoxTypeDZ.Text, "DZ70", "userflag"));

            }
        }

        private void textBoxPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxPass.Text == "1qaz2wsx")
                {
                    panelPass.Visible = false;
                }
                else
                {
                    MessageBox.Show("密码不正确，请重新输入？");
                }
            }
        }

        private void TAdvancedForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
