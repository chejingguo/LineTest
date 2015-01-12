using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

//声明对象
using MSExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace BTLProductLine
{
    public partial class TDateReferForm : Form
    {
        public TDateReferForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string start_date =dateTimePicker1.Value.ToShortDateString();
            string end_date = dateTimePicker2.Value.ToShortDateString();
            DataTable datatable = TDataBase.InquireData(start_date, end_date);

            dataGridView1.DataSource = datatable;
            //File.Copy("C:\\Location.mdb", "d:\\Location.mdb", true);
        }

        private void TDateReferForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1
            dataGridView1.DataSource = null;
            string start_date = dateTimePicker1.Value.ToShortDateString();
            string end_date = dateTimePicker2.Value.ToShortDateString();
            DataTable datatable = TDataBase.InquireData1(start_date, end_date);

            dataGridView1.DataSource = datatable;
            //File.Copy("C:\\Location.mdb", "d:\\Location.mdb", true);
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            //首先判断控件中是否有数据
            if (this.dataGridView1.Rows.Count != 0)
            {
                //this.saveFileDialog1.FileName =".xls";
                //this.saveFileDialog1.ShowDialog();
                MSExcel.Application xlsApp = new MSExcel.Application();
                if(xlsApp==null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }
                else
                {
                    string xlsfilename = "";
                    MSExcel.Workbooks workbooks = xlsApp.Workbooks;
                    MSExcel.Workbook workbook = xlsApp.Workbooks.Add(MSExcel.XlWBATemplate.xlWBATWorksheet);
                    MSExcel.Worksheet worksheet = (MSExcel.Worksheet)workbook.Worksheets[1];//取得sheet1
                    //写入表头
                    for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
                    {
                        worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                    }
                    //写入数值
                    for (int r = 1; r < this.dataGridView1.Rows.Count; r++)
                    {
                        for (int i = 0; i < dataGridView1.ColumnCount; i++)
                        {
                            worksheet.Cells[r + 1, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                    if (xlsfilename!="")
                    {
                        try
                        {
                            workbook.Saved=true;
                            workbook.SaveCopyAs(this.saveFileDialog1.FileName);
                        }
                        catch(Exception ex)
                        {
                            //MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                            TLogClass tLogClass = new TLogClass();
                            tLogClass.WriteLogFile(ex.Message);
                        }
                    }
                    xlsApp.Quit();
                    GC.Collect();
                    //MessageBox.Show("文件保存成功", "提示", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("请先打开表格文件");
            }
        }

        private void btnFinishReferKQ_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string start_date = dateTimePicker1.Value.ToShortDateString();
            string end_date = dateTimePicker2.Value.ToShortDateString();
            DataTable datatable = TDataBase.InquireDataAllKQ(start_date, end_date);

            dataGridView1.DataSource = datatable;
        }

        private void btnFinishReferDZ_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string start_date = dateTimePicker1.Value.ToShortDateString();
            string end_date = dateTimePicker2.Value.ToShortDateString();
            DataTable datatable = TDataBase.InquireDataAllDZ(start_date, end_date);

            dataGridView1.DataSource = datatable;
        }

    }
}
