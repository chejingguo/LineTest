using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTLProductLine
{
    class TBarCodeParse
    {
        public TBarCodeParse()
        {
        }
        //input index,return string of decimalism
        public static string i_index2s_index(int index)
        {
            string s_index;
            if (index < 10)
                s_index = "0" + index.ToString();
            else
                s_index = index.ToString();

            return s_index;
        }
        //input str, return XOR check of string type
        public static string CalculateXOR2Hex(string str)
        {
            string strCheck;
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(str);
            byte check = (byte)(byteArray[0] ^ byteArray[1]);
            for (int i = 2; i < byteArray.Length; i++)
            {
                check = (byte)(check ^ byteArray[i]);
            }
            strCheck = check.ToString("X");//byte to hex
            return strCheck;
        }

        //change data to the format of hostlink
        public static string ToHostLinkData(string str)
        {
            string hostlinkdata = "";
            hostlinkdata = str + CalculateXOR2Hex(str) + "*" + "\r";//data format:@+01+RD+1000+0001+52+*+\r

            return hostlinkdata;
        }

        //whether receive the data correctly  
        public static bool IsCorrectData(string str)
        {
            //int slength = str.Length;
            if ((str.IndexOf("@") >=0) && (str.IndexOf("*") >= 0)&&(str.IndexOf("*"))>(str.IndexOf("@")))
            {
                int spos1 = str.IndexOf("@");
                int spos2 = str.IndexOf("*");
                string s_data = str.Substring(spos1, spos2-spos1 - 2);//Get datas between "*" and "@" ,except XOR
                string xordata = str.Substring(spos2-2,2);//Get XOR

                string flag_res = str.Substring(spos1+5,2);//Flag of communication
                bool f_result = flag_res.Equals("00", StringComparison.Ordinal);//if true,it means communication is normal

                if (f_result)
                {
                    if (xordata.Equals(CalculateXOR2Hex(s_data) ,StringComparison.Ordinal)  && s_data.Length>7)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }

        //Get the number of equipment IP
        public static int GetNumber(string str)
        {
            int spos1 = str.IndexOf("@");

            string s_data = str.Substring(spos1+1, 2);
            int  i_number = System.Int32.Parse(s_data);

            return i_number;
        }
        //Get data of equipment D1006
        public static string GetDataD1006(string str)
        {
            int spos1 = str.IndexOf("@");

            string s_data = str.Substring(spos1 + 31, 4);
            return s_data;
        }

        //Get data of equipment D1000
        public static string GetDataD1000(string str)
        {
            int spos1 = str.IndexOf("@");
            string s_data = str.Substring(spos1 + 7, 4);
            return s_data;
        }

        //whether get response code ,for example : 00 means normal
        public static string GetResponseCode(string str)
        {
            int spos1 = str.IndexOf("@");

            string s_data = str.Substring(spos1 + 5, 2);

            return s_data;
        }

        //obtain all data except head and tail,from start addr to end addr
        public static string GetAllData(string str)//read D data
        {
            int spos1 = str.IndexOf("@");
            int spos2 = str.IndexOf("*");

            string s_data = str.Substring(spos1 + 7, spos2 - spos1 - 9);
            return s_data;
        }

        //get barcode data from D1001 to D1005
       public static string GetBarCodeData(string str,int strlen)//D1001-1005
       {
           int spos1 = str.IndexOf("@");
           int spos2 = str.IndexOf("*");

           string s_data = str.Substring(spos1 + 7, spos2 - spos1 - 9);
           s_data = s_data.Substring(4, 20);
           if (strlen==7)
               return DataToBarCode(s_data,7);
           else if (strlen == 9)
               return DataToBarCode(s_data,9);
           else
               return DataToBarCode(s_data,8);
       }

       //input plc data ,output real barcode,A49A41005 
       public static string DataToBarCode(string strHex, int len)
       {
           string strbar = ""; string strTemp = "";

           byte[] by = new byte[strHex.Length / 2];
           for (int i = 0; i < strHex.Length / 2; i++)
           {
               strTemp = strHex.Substring(i * 2, 2);
               by[i] = Convert.ToByte(strTemp, 16);
           }
           char i0 = Convert.ToChar(by[0]); char i1 = Convert.ToChar(by[1]); char i2 = Convert.ToChar(by[2]); char i3 = Convert.ToChar(by[3]);
           char i4 = Convert.ToChar(by[4]); char i5 = Convert.ToChar(by[5]); char i6 = Convert.ToChar(by[6]); char i7 = Convert.ToChar(by[7]);
           char i8 = Convert.ToChar(by[8]); char i9 = Convert.ToChar(by[9]);
           if (len == 7)
           {
               strbar = i1.ToString() + i0.ToString() + i3.ToString() + i2.ToString() + i5.ToString() + i4.ToString() + i7.ToString();//type+year+month+day+classes+index(001)
               return strbar;
           }
           else if (len == 8)
           {
               if ((i7.ToString() == "\0") || (i6.ToString() == "\0"))
                   strbar = "ErrorBarCode";
               else
                   strbar = i1.ToString() + i0.ToString() + i3.ToString() + i2.ToString() + i5.ToString() + i4.ToString() + i7.ToString() + i6.ToString();//type+year+month+day+classes+index(001)
               return strbar;
           }
           else if (len == 9)
           {
               if ((i9.ToString() == "\0")|| (i7.ToString() == "\0"))
                   strbar = "ErrorBarCode";
               else
                   strbar = i1.ToString() + i0.ToString() + i3.ToString() + i2.ToString() + i5.ToString() + i4.ToString() + i7.ToString() + i6.ToString() + i9.ToString();
               return strbar;
           }
           else
               return "ErrorBarCode";
       }

        //Qualify Flag of product
       public static bool IsQualifyFlag(string str)
       {
           int spos1 = str.IndexOf("@");
           int spos2 = str.IndexOf("*");
           string qualifydata = str.Substring(spos1 + 7, 4);
           if(qualifydata.Equals("0002" ,StringComparison.Ordinal))
               return true;
           else
               return false;
       }

        //get XOR of received data
       public static string GetXorData(string str)
       {
           int spos2 = str.IndexOf("*");
           string s_data = str.Substring(spos2-2, 2);
           return s_data;
       }

        //whether barcode have number
       public static bool IsEmptyBarCode(string str)
       {
           int spos1 = str.IndexOf("@");
           string strBarCode = str.Substring(spos1+7+8,20);//@01RD00+D0999+D1000+D1001-D1005+...
           if (strBarCode.Equals("00000000000000000000", StringComparison.Ordinal))
               return true;
           else
               return false;
       }

       //input plc data ,output real barcode,A49A41005 
       public static float GetParData(string str_resp, int offset,int len)
       {
           float float_value = 0;
           string str_all = GetAllData(str_resp);
           string str_data = str_all.Substring(offset, len);

           float_value = StrToInt(str_data);
           return float_value;
       }

       public static int StrToInt(string strhex_4)
       {
           int intbar = 0; string strTemp = "";

           byte[] by = new byte[strhex_4.Length];
           for (int i = 0; i < strhex_4.Length; i++)
           {
               strTemp = strhex_4.Substring(i, 1);
               by[i] = Convert.ToByte(strTemp, 16);
           }
           int i0 = Convert.ToInt16(by[0]); int i1 = Convert.ToInt16(by[1]);
           int i2 = Convert.ToInt16(by[2]); int i3 = Convert.ToInt16(by[3]);

           intbar = i0 * 16 * 16 * 16 + i1 * 16 * 16 + i2 * 16 + i3;
           return intbar;
       }



    }
}
