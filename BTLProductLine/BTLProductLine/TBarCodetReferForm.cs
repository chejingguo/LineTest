using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using System.IO;

namespace BTLProductLine
{
    public partial class TBarCodeReferForm : Form
    {
        public TBarCodeReferForm()
        {
            InitializeComponent();
        }

        private void TProductReferForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void TProductReferForm_Load(object sender, EventArgs e)
        {
            inputBarCodetextBox.Text = "B49P2003";
            inputBarCodetextBox1.Text = "AA4XH1003";
        }

        private void saveBarCodebutton_Click(object sender, EventArgs e)
        {
            if (barCodeRichTextBox.Text == "")
                MessageBox.Show("当前没有条码信息！");
            else
            {
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string content = barCodeRichTextBox.Text + barCodeRichTextBox1.Text+ Environment.NewLine;;
                    File.WriteAllText(saveFileDialog1.FileName + ".txt", content, Encoding.UTF8); 
                    
                    MessageBox.Show("保存成功！");
                }
            }
            //TDataBase.CreateDataBase("1.mdb","1");
        }

        private void findBarCodebutton_Click(object sender, EventArgs e)
        {
            string temp_barcode = "";
            if (inputBarCodetextBox.Text == "")
                MessageBox.Show("请输入条码！");
            else
            //barCodeRichTextBox.Text = "This is my bar code!"+"\n"+inputBarCodetextBox.Text;
            {
                string str_find = inputBarCodetextBox.Text;

                string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
                OleDbConnection con = new OleDbConnection(ConStr);
                con.Open();

                string sql = "Select * From 卡钳全部数据 where 卡钳条码=" + "'" + str_find + "'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                try
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    
                    barCodeRichTextBox.Text = "卡钳编号：" + reader[0].ToString() + "\n" +
                    "日期时间：" + reader[1].ToString() + "\n" +
                    "卡钳条码：" + reader[2].ToString() + "\n" +
                    "卡钳物料：" + reader[3].ToString() + "\n" +
                    "电装条码：" + reader[4].ToString() + "\n" +
                    "电装物料：" + reader[5].ToString() + "\n" +
                    "KQ10合格标志：" + reader[6].ToString() + "\n" +
                    "KQ10实时压力：" + reader[7].ToString() + "\n" +
                    "KQ10最大位移：" + reader[8].ToString() + "\n" +
                    "KQ20合格标志：" + reader[9].ToString() + "\n" +
                    "KQ20滑阻力：" + reader[10].ToString() + "\n" +
                    "KQ20压入力：" + reader[11].ToString() + "\n" +
                    "KQ30合格标志：" + reader[12].ToString() + "\n" +
                    "KQ30低压密封性：" + reader[13].ToString() + "\n" +
                    "KQ40合格标志：" + reader[14].ToString() + "\n" +
                    "KQ40低压密封性：" + reader[15].ToString() + "\n" +
                    "KQ50合格标志：" + reader[16].ToString() + "\n" +
                    "KQ60合格标志：" + reader[17].ToString() + "\n" +
                    "KQ60夹紧力：" + reader[18].ToString() + "\n" +
                    "KQ70合格标志：" + reader[19].ToString() + "\n" +
                    "KQ70 1号转矩：" + reader[20].ToString() + "\n" +
                    "KQ70 1号角度：" + reader[21].ToString() + "\n" +
                    "KQ70 2号转矩：" + reader[22].ToString() + "\n" +
                    "KQ70 2号角度：" + reader[23].ToString() + "\n" +
                    "KQ80合格标志：" + reader[24].ToString() + "\n" +
                    "KQ80最大滑阻阻力：" + reader[25].ToString() + "\n";
                    temp_barcode = reader[4].ToString();
                    reader.Close();
                    con.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);//显示异常信息
                    TLogClass tLogClass = new TLogClass();
                    tLogClass.WriteLogFile(ex.Message);
                }

                string str_find1 = temp_barcode;

                string ConStr1 = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
                OleDbConnection con1 = new OleDbConnection(ConStr1);
                con1.Open();

                string sql1 = "Select * From 电装全部数据 where 电装条码=" + "'" + str_find1 + "'";
                OleDbCommand cmd1 = new OleDbCommand(sql1, con1);
                try
                {
                    OleDbDataReader reader1 = cmd1.ExecuteReader();

                    reader1.Read();
                    barCodeRichTextBox1.Text = "电装编号：" + reader1[0].ToString() + "\n" +
                    "日期时间：" + reader1[1].ToString() + "\n" +
                    "电装编号：" + reader1[2].ToString() + "\n" +
                    "电装编号1：" + reader1[3].ToString() + "\n" +
                    "电装物料：" + reader1[4].ToString() + "\n" +
                    "DZ05合格标志：" + reader1[5].ToString() + "\n" +
                    "DZ10合格标志：" + reader1[6].ToString() + "\n" +
                    "DZ20合格标志：" + reader1[7].ToString() + "\n" +
                    "DZ20中心销最大力：" + reader1[8].ToString() + "\n" +
                    "DZ20最大位移：" + reader1[9].ToString() + "\n" +
                    "DZ20同步轮力最大力：" + reader1[10].ToString() + "\n" +
                    "DZ30合格标志：" + reader1[11].ToString() + "\n" +
                    "DZ40合格标志：" + reader1[12].ToString() + "\n" +
                    "DZ50合格标志：" + reader1[13].ToString() + "\n" +
                    "DZ60合格标志：" + reader1[14].ToString() + "\n" +
                    "DZ60泄漏结果值：" + reader1[15].ToString() + "\n" +
                    "DZ70合格标志：" + reader1[16].ToString() + "\n" +
                    "DZ70启动电流：" + reader1[17].ToString() + "\n" +
                    "DZ70空载电流：" + reader1[18].ToString() + "\n" +
                    "DZ70负载电流：" + reader1[19].ToString() + "\n";
                    reader1.Close();
                    con1.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);//显示异常信息
                    TLogClass tLogClass = new TLogClass();
                    tLogClass.WriteLogFile(ex.Message);
                }

            }
        }

        private void findBarCodebutton1_Click(object sender, EventArgs e)
        {
            if (inputBarCodetextBox.Text == "")
                MessageBox.Show("请输入条码！");
            else
            //barCodeRichTextBox.Text = "This is my bar code!"+"\n"+inputBarCodetextBox.Text;
            {
                string str_find = inputBarCodetextBox1.Text;

                string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
                OleDbConnection con = new OleDbConnection(ConStr);
                con.Open();

                string sql = "Select * From 电装全部数据 where 电装条码1=" + "'" + str_find + "'";
                OleDbCommand cmd = new OleDbCommand(sql, con);
                try
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    
                    reader.Read();
                    barCodeRichTextBox1.Text = "电装编号：" + reader[0].ToString() + "\n" +
                    "日期时间：" + reader[1].ToString() + "\n" +
                    "电装条码：" + reader[2].ToString() + "\n" +
                    "电装条码1：" + reader[3].ToString() + "\n" +
                    "电装物料：" + reader[4].ToString() + "\n" +
                    "DZ05合格标志：" + reader[5].ToString() + "\n" +
                    "DZ10合格标志：" + reader[6].ToString() + "\n" +
                    "DZ20合格标志：" + reader[7].ToString() + "\n" +
                    "DZ20中心销最大力：" + reader[8].ToString() + "\n" +
                    "DZ20最大位移：" + reader[9].ToString() + "\n" +
                    "DZ20同步轮力最大力：" + reader[10].ToString() + "\n" +
                    "DZ30合格标志：" + reader[11].ToString() + "\n" +
                    "DZ40合格标志：" + reader[12].ToString() + "\n" +
                    "DZ50合格标志：" + reader[13].ToString() + "\n" +
                    "DZ60合格标志：" + reader[14].ToString() + "\n" +
                    "DZ60泄漏结果值：" + reader[15].ToString() + "\n" +
                    "DZ70合格标志：" + reader[16].ToString() + "\n" +
                    "DZ70启动电流：" + reader[17].ToString() + "\n" +
                    "DZ70空载电流：" + reader[18].ToString() + "\n" +
                    "DZ70负载电流：" + reader[19].ToString() + "\n";
                    reader.Close();
                    con.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);//显示异常信息
                    TLogClass tLogClass = new TLogClass();
                    tLogClass.WriteLogFile(ex.Message);
                }

            }
        }


    }
}
