using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//声明对象
using System.IO;
using MSExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace BTLProductLine
{
    class TBarCodeExcel
    {
        public static void CreateExcel()
        {
            object path = @"D:" + "\\" + "1";//声明文件路径变量path = @textBox2.Text + "\\" + textBox1.Text;

            MSExcel.Application ExcelApp = new MSExcel.ApplicationClass();    //初始化;

            Object Nothing = Missing.Value;
            MSExcel.Workbook ExcelDoc= ExcelApp.Workbooks.Add(Nothing);;

            //如果已经存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            //使用第一个工作表作为插入数据的工作表
            MSExcel.Worksheet ws = (MSExcel.Worksheet)ExcelDoc.Sheets[1];

            //声明一个MSExcel.Range 类型的变量r
            MSExcel.Range r;

            //获得A1处的表格，并赋值
            r = ws.get_Range("A1", "F1");
            r.set_Item(1, 1, "1");
            r.set_Item(1, 2, "2");
            r.set_Item(1, 3, "3");

            //获得A2处的表格，并赋值
            r = ws.get_Range("A2", "A2");
            r.Value2 = "3.6";

            //获得A3处的表格，并赋值
            r = ws.get_Range("A3", "A3");
            r.Value2 = "6.5";

            //WdSaveFormat为Excel文档的保存格式
            object format = MSExcel.XlFileFormat.xlWorkbookNormal;

            //将excelDoc文档对象的内容保存为XLSX文档 
            //ExcelDoc.SaveAs(path, format, Nothing, Nothing, Nothing, Nothing, MSExcel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);
            ExcelDoc.SaveAs(path, Nothing, Nothing, Nothing, Nothing, Nothing, MSExcel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);

            //关闭excelDoc文档对象 
            ExcelDoc.Close(Nothing, Nothing, Nothing);

            //关闭excelApp组件对象 
            ExcelApp.Quit();
            System.Windows.Forms.MessageBox.Show("Excel工作簿被成功创建", "信息提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
