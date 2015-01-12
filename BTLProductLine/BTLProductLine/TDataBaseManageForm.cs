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
    public partial class TDataBaseManageForm : Form
    {
        public TDataBaseManageForm()
        {
            InitializeComponent();
        }

        private void AddKQ_Click(object sender, EventArgs e)
        {
            string message = "确定添加数据吗?";
            string caption = "提示";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            // Displays the MessageBox.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (barCodeKQ.Text.Length != 8)
                {
                    System.Windows.Forms.MessageBox.Show("KQ条码格式不正确！");
                    return;
                }
                else
                {
                    string str0 = "'"; string strtime = DateTime.Now.ToShortDateString();//string strtime = "'" + DateTime.Now.ToShortDateString() + "'";
                    TDataBase.InsertData("卡钳数据", "卡钳条码", barCodeKQ.Text);//database barcode
                    TDataBase.ModifyData("卡钳数据", "卡钳条码", barCodeKQ.Text, "KQ10合格标志", "true");//database flag of correct
                    TDataBase.ModifyData("卡钳数据", "卡钳条码", barCodeKQ.Text, "卡钳物料", TParameterForm.kq_material);//database material
                    TDataBase.ModifyData("卡钳数据", "卡钳条码", barCodeKQ.Text, "日期时间", string.Format("{0}{1}{2}", str0, strtime, str0));//database time
                    System.Windows.Forms.MessageBox.Show("数据已添加！");
                }
            }
        }

        private void AddDZ_Click(object sender, EventArgs e)
        {
            string message = "确定添加数据吗?";
            string caption = "提示";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            // Displays the MessageBox.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (!(barCodeDZ.Text.Length == 7 || barCodeDZ.Text.Length == 9))
                {
                    System.Windows.Forms.MessageBox.Show("DZ条码格式不正确！");
                    return;
                }
                else
                {
                    if (barCodeDZ.Text.Length == 7)
                    {
                        string str_time = "'" + DateTime.Now.ToShortDateString() + "'";
                        TDataBase.InsertData("电装数据", "电装条码", barCodeDZ.Text);//database barcode
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "电装物料", TParameterForm.dz_material);//database material
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "日期时间", str_time);//database time
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "DZ10合格标志", "true");//database flag of correct
                    }
                    if (barCodeDZ.Text.Length == 9)
                    {
                        string str_time = "'" + DateTime.Now.ToShortDateString() + "'";
                        TDataBase.InsertData("电装数据", "电装条码", barCodeDZ.Text);//database barcode           
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "电装物料", TParameterForm.dz_material);//database material
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "日期时间", str_time);//database time
                        TDataBase.ModifyData("电装数据", "电装条码", barCodeDZ.Text, "DZ05合格标志", "true");//database flag of correct
                    }
                }
            }

        }

        private void ClearDZ_Click(object sender, EventArgs e)
        {
            string message = "确定删除数据吗?";
            string caption = "提示";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);

            // Displays the MessageBox.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string start_date = dateTimePickerStart.Value.ToShortDateString();
                string end_date = dateTimePickerEnd.Value.ToShortDateString();
                TDataBase.DeleteDataDZ(start_date, end_date);
                System.Windows.Forms.MessageBox.Show("数据已删除！");
            }

        }

        private void ClearKQ_Click(object sender, EventArgs e)
        {
            string message = "确定删除数据吗?";
            string caption = "提示";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            // Displays the MessageBox.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                string start_date = dateTimePickerStart.Value.ToShortDateString();
                string end_date = dateTimePickerEnd.Value.ToShortDateString();
                TDataBase.DeleteDataKQ(start_date, end_date);
                System.Windows.Forms.MessageBox.Show("数据已删除！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1qaz2wsx")
            {
                panel1.Visible = false;
            }
            else
            {
                MessageBox.Show("密码不正确，请重新输入？");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "1qaz2wsx")
                {
                    panel1.Visible = false;
                }
                else
                {
                    MessageBox.Show("密码不正确，请重新输入？");
                }
            }
        }
    }
}
