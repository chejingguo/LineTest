using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data.OleDb;
using System.Data;
using ADOX;

namespace BTLProductLine
{
    public static class TDataBase
    {
        static TDataBase()
        {
        }
        public static void CreateDataBase(string fileName, string Pwd)
        {
            if (File.Exists(fileName))
            {
                string delFile = fileName;
                File.Delete(delFile);
            }
            string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Jet OLEDB:Database Password=" + Pwd + ";Jet OLEDB:Engine Type=5";
            //创建数据库
            ADOX.Catalog catalog = new Catalog();
            try
            {
                catalog.Create(conn);
                //cat.Create("Provider = Microsoft.Jet.OLEDB.4.0;Data Source = D:\\NewMDB.mdb;");
            }
            catch (Exception ex)
            {
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
            //连接数据库
            ADODB.Connection cn = new ADODB.Connection();
            cn.Open(conn, null, null, -1);
            catalog.ActiveConnection = cn;

            //新建表
            ADOX.Table table = new ADOX.Table();
            table.Name = "kqData";

            ADOX.Column column = new ADOX.Column();
            column.Name = "kqBarCode";
            //column.Properties[].Value= true;
            table.Columns.Append(column, DataTypeEnum.adVarChar, 50);

            //设置主键
            table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "kqBarCode", "", "");

            table.Columns.Append("FileName", DataTypeEnum.adVarWChar, 50);
            table.Columns.Append("FileDate", DataTypeEnum.adDate, 0);
            table.Columns.Append("FileSize", DataTypeEnum.adInteger, 9);
            table.Columns.Append("OrderID", DataTypeEnum.adInteger, 9);

            /*
            ADOX.Column column = new ADOX.Column();
            column.ParentCatalog = catalog;
            column.Type = ADOX.DataTypeEnum.adInteger; // 必须先设置字段类型
            column.Name = "ID";
            column.DefinedSize = 9;
            column.Properties["AutoIncrement"].Value = true;
            table.Columns.Append(column, DataTypeEnum.adInteger, 0);

            //设置主键
            table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "ID", "", "");

            table.Columns.Append("FileName", DataTypeEnum.adVarWChar, 50);
            table.Columns.Append("FileDate", DataTypeEnum.adDate, 0);
            table.Columns.Append("FileSize", DataTypeEnum.adInteger, 9);
            table.Columns.Append("OrderID", DataTypeEnum.adInteger, 9);
            table.Columns.Append("Sha1", DataTypeEnum.adVarWChar, 50);
             */ 

            try
            {
                catalog.Tables.Append(table);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
            //此处一定要关闭连接，否则添加数据时候会出错

            table = null;
            catalog = null;
            System.Windows.Forms.Application.DoEvents();
            cn.Close();

        }
        public static void ConnectDataBase()
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();
            if (connection.State == ConnectionState.Open)
                System.Windows.Forms.MessageBox.Show("连接成功");
            else
                System.Windows.Forms.MessageBox.Show("连接失败！");

            connection.Close();
        }

        public static void InsertData(string tb_name,string mKey_name, string mKey_value)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "insert into " + tb_name + "(" + mKey_name + ") values(" + "'" + mKey_value + "'" + ")";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(sql+"Error:"+ex.Message);
            }
        }

        public static void DeleteData(string tb_name, string mKey_name, string mKey_value)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "delete from" + tb_name + "(" + mKey_name + ") values(" + mKey_value + ")";
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static void DeleteDataKQ(string statt_time,string end_time)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "delete * From 卡钳数据 where 日期时间 between # " + statt_time + " # and # " + end_time + " #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

        }
        public static void DeleteDataDZ(string statt_time, string end_time)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "delete * From 电装数据 where 日期时间 between # " + statt_time + " # and # " + end_time + " #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            cmd.ExecuteNonQuery();
            connection.Close();

        }
        public static void ModifyData(string tb_name, string mKey_name, string mKey_value, string column_name, string column_value)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            //string sql = "update data set longitude=12 where ID=10000";
            string sql = "update " + tb_name + " set " + column_name + "=" + column_value + " where " + mKey_name + "=" + "'" + mKey_value + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(sql + "Error:" + ex.Message);
            }
        }
        public static void ModifyData(string tb_name, string mKey_name, string mKey_value, string column_name, float column_value)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            //string sql = "update data set longitude=12 where ID=10000";
            string sql = "update " + tb_name + " set " + column_name + "=" + column_value + " where " + mKey_name + "=" + "'" + mKey_value + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(sql + "Error:" + ex.Message);
            }
        }
        public static void ModifyData(string str_sql)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            //string sql = "update data set longitude=12 where ID=10000";
            string sql = str_sql;
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(sql + "Error:" + ex.Message);
            }
        }

        public static bool InquireIsBarCode(string tb_name, string mKey_name, string mKey_value)//mainkey barcode
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From " + tb_name + " where " + mKey_name + "=" + "'" + mKey_value + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                bool blnflag = false;
                if (reader.HasRows)
                {
                    blnflag = true;
                    reader.Close();
                    connection.Close();
                    return blnflag;
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return blnflag;
                }
            }
            catch (Exception ex)
            {    
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(sql + "Error:" + ex.Message);
                return false;
            }
        }

        public static string InquireData(string tb_name, string column_name,string mKey_value, int index)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From " + tb_name + " where " + column_name + "=" + "'" + mKey_value + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", 0);
            OleDbDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string strdata = null;

            if (reader.HasRows)
            {
                strdata = reader[index].ToString();
                reader.Close();
                connection.Close();
                return strdata;
            }
            else
            {
                reader.Close();
                connection.Close();
                return strdata;
            }
        }

        public static DataTable InquireData(string str1,string str2)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From 卡钳数据 where 日期时间 between # "+ str1+" # and # "+str2+" #";
            //string sql = "Select * From kqData where 日期时间 between # 2014/9/2 # and # 2014/9/27 #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql, connection);
            da1.Fill(dt);

            return dt;
        }
        public static DataTable InquireDataAllKQ(string str1, string str2)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From 卡钳全部数据 where 日期时间 between # " + str1 + " # and # " + str2 + " #";
            //string sql = "Select * From kqData where 日期时间 between # 2014/9/2 # and # 2014/9/27 #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql, connection);
            da1.Fill(dt);

            return dt;
        }
        public static DataTable InquireDataAllDZ(string str1, string str2)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From 电装全部数据 where 日期时间 between # " + str1 + " # and # " + str2 + " #";
            //string sql = "Select * From kqData where 日期时间 between # 2014/9/2 # and # 2014/9/27 #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql, connection);
            da1.Fill(dt);

            return dt;
        }
        public static DataTable InquireData1(string str1, string str2)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            connection.Open();

            string sql = "Select * From 电装数据 where 日期时间 between # " + str1 + " # and # " + str2 + " #";
            //string sql = "Select * From kqData where 日期时间 between # 2014/9/2 # and # 2014/9/27 #";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            DataTable dt = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql, connection);
            da1.Fill(dt);

            return dt;
        }

        public static void AdjustData(string sql)
        {
            string ConStr = "Provider= Microsoft.Jet.OLEDB.4.0;data source =Location.mdb;Jet OLEDB:Database Password=123456";
            OleDbConnection connection = new OleDbConnection(ConStr);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }

            //string sql = "update data set longitude=12 where ID=10000";
            OleDbCommand cmd = new OleDbCommand(sql, connection);

            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
        }
    }
}
