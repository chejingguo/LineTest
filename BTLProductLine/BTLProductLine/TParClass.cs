using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace BTLProductLine
{
    class TParClass
    {

        public  string ReadText(string str,string type)
        {
             string strLine=null;
             try
             {
                FileStream aFile = new FileStream(str,FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                string Line = sr.ReadLine();
                 while(Line != null)
                 {
                     if (Line.Substring(0, 1).Equals(type, StringComparison.Ordinal))
                        strLine = Line;
                    Line = sr.ReadLine();
                 }
                 sr.Close();
                 return strLine;
             }
             catch (IOException ex)
             {
                 TLogClass tLogClass = new TLogClass();
                 tLogClass.WriteLogFile(ex.Message);
                 return ex.ToString();

             }
        }

        public int GetParIndexValue(string str,int start_spos, int len)
        {
            string temp = str.Substring(start_spos, len);
            return Convert.ToInt16(temp);
        }
    }
}
