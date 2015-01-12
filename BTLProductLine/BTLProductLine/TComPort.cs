using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO.Ports;
using System.Threading;
using System.Xml.Linq;

namespace BTLProductLine
{
    class TComPort
    {
        //Define 2 serialPorts,serialPort1，serialPort2
        private static SerialPort serialPort_kq = new SerialPort();
        private static SerialPort serialPort_dz = new SerialPort();

        private static SerialPort serialPort_mark = new SerialPort();

        private static bool dz_flag = false;
        private static bool kq_flag = false;
        private static string kq_BarCode = "";
        private static string dz_BarCode = "";

        public static bool[] stopKQ = new bool[8]{true,true,true,true,true,true,true,true};
        public static bool[] stopDZ = new bool[8]{true,true,true,true,true,true,true,true};

        static TComPort()
        {
            InitialPort();
            serialPort_kq.DataReceived += new SerialDataReceivedEventHandler(OnDataReceivedPortKQ);
            serialPort_dz.DataReceived += new SerialDataReceivedEventHandler(OnDataReceivedPortDZ);

            serialPort_mark.DataReceived += new SerialDataReceivedEventHandler(OnDataReceivedPortMark);
        }

        //initalize serial port
        private static void InitialPort()
        {
            //serialPort1 initialization
            serialPort_kq.PortName = "COM10";
            serialPort_kq.BaudRate = 9600;
            serialPort_kq.DataBits = 7;
            serialPort_kq.Parity = Parity.Even;
            serialPort_kq.StopBits = StopBits.Two;

            //serialPort2 initialization
            serialPort_dz.PortName = "COM9";
            serialPort_dz.BaudRate = 9600;
            serialPort_dz.DataBits = 7;
            serialPort_dz.Parity = Parity.Even;
            serialPort_dz.StopBits = StopBits.Two;

            //serialPort2 initialization
            serialPort_mark.PortName = "COM11";
            serialPort_mark.BaudRate = 9600;
            serialPort_mark.DataBits = 7;
            serialPort_mark.Parity = Parity.Even;
            serialPort_mark.StopBits = StopBits.Two;

            try
            {
                serialPort_kq.Open();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
            try
            {
                serialPort_dz.Open();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }

            try
            {
                serialPort_mark.Open();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
        }

        //whether seriaPorts open 
        public static bool IsOpenPort()
        {
            //if (serialPort_kq.IsOpen || serialPort_dz.IsOpen || serialPort_mark.IsOpen)
            if (serialPort_kq.IsOpen || serialPort_dz.IsOpen || serialPort_mark.IsOpen)
                return true;
            else
                return false;
        }

        //close serialPort1，serialPort2，serialPort3
        public static void ClosePort()
        {
            serialPort_kq.Close();
            serialPort_dz.Close();
            serialPort_mark.Close();
        }

        //write datas to serialPort i
        public static void WriteDataToBuff(int i, string str)
        {
            try
            {
                if (i == 1)
                    serialPort_kq.Write(str);
                else if (i == 2)
                    serialPort_dz.Write(str);
                else if(i==3)
                    serialPort_mark.Write(str);
                else
                    return;
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);//显示异常信息
                TLogClass tLogClass = new TLogClass();
                tLogClass.WriteLogFile(ex.Message);
            }
        }

        //read data from equipment i by serial port i and some order
        public static void ReadDataFromPLC(int icom, int iequi_num, string str_newdata)
        {
            string senddata;
            senddata = "@" + TBarCodeParse.i_index2s_index(iequi_num) + str_newdata;// @+index(10)+"RD" + "1000" + "0020"
            senddata = TBarCodeParse.ToHostLinkData(senddata);
            WriteDataToBuff(icom, senddata);
        }
        //write datas to equipment i from serial port i and some order
        public static void WriteDataToPLC(int icom, int iequi_num, string str_newdata)
        {
            string str_data;
            if (icom == 1)
            {
                str_data = "@" + TBarCodeParse.i_index2s_index(iequi_num) + str_newdata;// @+index(10)+"WD" + "1000" + "0002"
                str_data = TBarCodeParse.ToHostLinkData(str_data);//data+xor+\r
                WriteDataToBuff(1, str_data);
            }
            if (icom == 2)
            {
                str_data = "@" + TBarCodeParse.i_index2s_index(iequi_num) + str_newdata;// "WD" + "1000" + "0002"
                str_data = TBarCodeParse.ToHostLinkData(str_data);
                WriteDataToBuff(2, str_data);
            }
            if (icom == 3)
            {
                str_data = "@" + TBarCodeParse.i_index2s_index(iequi_num) + str_newdata;// "WD" + "1000" + "0002"
                str_data = TBarCodeParse.ToHostLinkData(str_data);
                WriteDataToBuff(3, str_data);
            }
        }

        //the thread of reading dz equipment
        public static void ReadEquipment()
        {
            int i, j = 10;
            for (i = 1; i < 9; i++)//read 16 machines,DZ1-DZ8,OP10-OP17
            {
                if (IsOpenPort())
                {
                    if (stopKQ[i - 1])
                    {
                        ReadDataFromPLC((int)ComNumber.COM10, j, "RD10000030"); //read barcode,D1000~D1018
                        //stopKQ[i - 1] = false;
                    }
                    if (stopDZ[i - 1])
                    {
                        ReadDataFromPLC((int)ComNumber.COM9, i, "RD10000030");  //read barcode,D1000~D1018
                        //stopDZ[i - 1] = false;
                    }
                }
                Thread.Sleep(350);
                if (i == 8)
                {
                    i = 0;//read 8 machine repeatly.
                    j = 9;
                }
                j++;
            }

            //
            DateTime dtBegin = new DateTime(2014, 12, 12, 12, 0, 0);
            DateTime dtEnd = new DateTime(2015, 6, 12, 12, 0, 0);
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0, DateTimeKind.Unspecified);
            if (dt >= dtBegin && dt <= dtEnd)
            {
            }
            else
            {
                Thread.Sleep(5000);
            }
            //
            //
            //
        }

        //manage the data of sent and received by the event of serial port 1
        public static void OnDataReceivedPortKQ(object sender, SerialDataReceivedEventArgs e)
        {
            string kq_receivedata = serialPort_kq.ReadTo("\r");//datas from "@" to " \r"
            if (TBarCodeParse.IsCorrectData(kq_receivedata) && (kq_receivedata.Length > 10))//whether the data is correct.
            {
                int int_equip_number = TBarCodeParse.GetNumber(kq_receivedata);//find the number of equipment
                switch (int_equip_number)
                {
                    case (int)KQEquipmentNumber.OP10:
                        {
                            stopKQ[0] = false;
                            ProcessKQ10(kq_receivedata);
                            stopKQ[0] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP20:
                        {
                            stopKQ[1] = false;
                            ProcessKQ20(kq_receivedata);
                            stopKQ[1] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP30:
                        {
                            stopKQ[2] = false;
                            ProcessKQ30(kq_receivedata);
                            stopKQ[2] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP40:
                        {
                            stopKQ[3] = false;
                            ProcessKQ40(kq_receivedata);
                            stopKQ[3] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP50:
                        {
                            stopKQ[4] = false;
                            ProcessKQ50(kq_receivedata);
                            stopKQ[4] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP60:
                        {
                            stopKQ[5] = false;
                            ProcessKQ60(kq_receivedata);
                            stopKQ[5] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP70:
                        {
                            stopKQ[6] = false;
                            ProcessKQ70(kq_receivedata);
                            stopKQ[6] = true;
                            break;
                        }
                    case (int)KQEquipmentNumber.OP80:
                        {
                            stopKQ[7] = false;
                            ProcessKQ80(kq_receivedata);
                            stopKQ[7] = true;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private static void ProcessKQ10(string strData)
        {
            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))//whether D1006 ==1;
            {
                string str0 = "'"; string strtime = DateTime.Now.ToShortDateString();//string strtime = "'" + DateTime.Now.ToShortDateString() + "'";

                float kq10_realtime_pres = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 12;
                float kq10_max_distance = TBarCodeParse.GetParData(strData, 15 * 4, 4) / 1200;

                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP10, "WD10060002");
                TDataBase.InsertData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));//database barcode
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "卡钳物料", TParameterForm.kq_material);//database material
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "日期时间", string.Format("{0}{1}{2}", str0, strtime, str0));//database time

                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ10实时压力", kq10_realtime_pres);//database flag of correct
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ10最大位移", kq10_max_distance);//database flag of correct
                
                SetKQEquipFlag(TBarCodeParse.GetBarCodeData(strData, 8));//update custom eqiupment flag

                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ10合格标志", "true");//database flag of correct
            }
        }
        private static void ProcessKQ20(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ10_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP20, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP20, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP20, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq20_slip_resistance = TBarCodeParse.GetParData(strData, 17 * 4, 4) * 5 / 6;
                float kq20_max_presure = TBarCodeParse.GetParData(strData, 13 * 4, 4) * 5 / 6;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ20合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP20, "WD10060002");

                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ20滑阻力", kq20_slip_resistance);//database flag of correct
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ20最大压力", kq20_max_presure);//database flag of correct
            }
        }
        private static void ProcessKQ30(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ20_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP30, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP30, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP30, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq30_low_dif_pre = (TBarCodeParse.GetParData(strData, 19 * 4, 4) - 6000) / 6;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ30合格标志", "true");//database
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ30差压结果", kq30_low_dif_pre);//database flag of correct

                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP30, "WD10060002");
            }
        }
        private static void ProcessKQ40(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ30_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP40, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP40, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP40, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq40_hig_dif_pre = (TBarCodeParse.GetParData(strData, 13 * 4, 4) - 6000) / 6;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ40合格标志", "true");//database
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ40差压结果", kq40_hig_dif_pre);//database flag of correct

                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP40, "WD10060002");
            }
        }
        private static void ProcessKQ50(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool kq_bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));
                bool dz_bln_BarCode = TDataBase.InquireIsBarCode("电装数据", "电装条码1", TBarCodeParse.GetBarCodeData(strData, 9)) || TDataBase.InquireIsBarCode("电装数据", "电装条码", TBarCodeParse.GetBarCodeData(strData, 9));

                if (kq_bln_BarCode)
                {
                    kq_BarCode = TBarCodeParse.GetBarCodeData(strData, 8);
                    bool kq_bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ40_Flag_Index));
                    if (kq_bln_Flag)
                    {
                        if (dz_flag)
                        {
                            TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000002");
                            kq_flag = false;
                            dz_flag = false;
                        }
                        else
                        {
                            TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000016");
                            kq_flag = true;
                        }
                    }
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000003");
                }

                if (dz_bln_BarCode)
                {
                    dz_BarCode = TBarCodeParse.GetBarCodeData(strData, 9);
                    bool dz_bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码1", dz_BarCode, (int)DZDataBase.DZ70_Flag_Index)) || Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", dz_BarCode, (int)DZDataBase.DZ70_Flag_Index));
                    if (dz_bln_Flag)
                    {
                        if (kq_flag)
                        {
                            TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000002");
                            kq_flag = false;
                            dz_flag = false;
                        }
                        else
                        {
                            TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000015");
                            dz_flag = true;
                        }
                    }
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                TDataBase.ModifyData("卡钳数据", "卡钳条码", kq_BarCode, "KQ50合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP50, "WD10060002");

                TDataBase.ModifyData("卡钳数据", "卡钳条码", kq_BarCode, "电装条码", "'" + dz_BarCode + "'");//database
                TDataBase.ModifyData("卡钳数据", "卡钳条码", kq_BarCode, "电装物料", TParameterForm.dz_material);//database material

            }
        }
        private static void ProcessKQ60(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ50_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP60, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP60, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP60, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq60_clamp_force = TBarCodeParse.GetParData(strData, 16 * 4, 4) / 2;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ60合格标志", "true");//database
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ60夹紧力", kq60_clamp_force);//database flag of correct

                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP60, "WD10060002");
            }
        }
        private static void ProcessKQ70(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ60_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP70, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP70, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP70, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq70_torque1 = TBarCodeParse.GetParData(strData, 12 * 4, 4) / 10;
                float kq70_angle1 = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 10;
                float kq70_torque2 = TBarCodeParse.GetParData(strData, 15 * 4, 4) / 10;
                float kq70_angle2 = TBarCodeParse.GetParData(strData, 26 * 4, 4) / 10;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ70合格标志", "true");//database

                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ70转矩1", kq70_torque1);//database flag of correct
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ70角度1", kq70_angle1);//database flag of correct
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ70转矩2", kq70_torque2);//database flag of correct
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ70角度2", kq70_angle2);//database flag of correct

                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP70, "WD10060002");
            }
        }
        private static void ProcessKQ80(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                bool bln_BarCode = TDataBase.InquireIsBarCode("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8));

                if (bln_BarCode)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), (int)KQDataBase.KQ70_Flag_Index));
                    if (bln_Flag)
                    {

                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP80, "WD10000002");

                        TComPort.WriteDataToBuff((int)ComNumber.COM11, TBarCodeParse.GetBarCodeData(strData, 8));
                    }
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP80, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP80, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                float kq80_max_slip = (TBarCodeParse.GetParData(strData, 13 * 4, 4) - 3000) / 60;
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ80合格标志", "true");//database
                TDataBase.ModifyData("卡钳数据", "卡钳条码", TBarCodeParse.GetBarCodeData(strData, 8), "KQ80最大滑阻力", kq80_max_slip);//database flag of correct
                TComPort.WriteDataToPLC((int)ComNumber.COM10, (int)KQEquipmentNumber.OP80, "WD10060002");
            }
        }

        public static void OnDataReceivedPortDZ(object sender, SerialDataReceivedEventArgs e)
        {
            string dz_receivedata = serialPort_dz.ReadTo("\r");//datas from "@" to " \r"
            if (TBarCodeParse.IsCorrectData(dz_receivedata) && (dz_receivedata.Length > 10))
            {
                int int_equip_number = TBarCodeParse.GetNumber(dz_receivedata);

                if (TParameterForm.dz_kind == "A")
                {
                    switch (int_equip_number)
                    {
                        case (int)DZEquipmentNumber.DZ05:
                            {
                                stopDZ[0] = false;
                                ProcessDZ05A(dz_receivedata);
                                stopDZ[0] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ10:
                            {
                                stopDZ[1] = false;
                                ProcessDZ10A(dz_receivedata);
                                stopDZ[1] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ20:
                            {
                                stopDZ[2] = false;
                                ProcessDZ20A(dz_receivedata);
                                stopDZ[2] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ35:
                            {
                                stopDZ[3] = false;
                                ProcessDZ35A(dz_receivedata);
                                stopDZ[3] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ40:
                            {
                                stopDZ[4] = false;
                                ProcessDZ40A(dz_receivedata);
                                stopDZ[4] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ50:
                            {
                                stopDZ[5] = false;
                                ProcessDZ50A(dz_receivedata);
                                stopDZ[5] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ60:
                            {
                                stopDZ[6] = false;
                                ProcessDZ60A(dz_receivedata);
                                stopDZ[6] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ70:
                            {
                                stopDZ[7] = false;
                                ProcessDZ70A(dz_receivedata);
                                stopDZ[7] = true;
                                break;
                            }
                    }
                }
                if (TParameterForm.dz_kind == "B")
                {
                    switch (int_equip_number)
                    {
                        case (int)DZEquipmentNumber.DZ05:
                            {
                                stopDZ[0] = false;
                                ProcessDZ05B(dz_receivedata);
                                stopDZ[0] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ10:
                            {
                                stopDZ[1] = false;
                                ProcessDZ10B(dz_receivedata);
                                stopDZ[1] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ20:
                            {
                                stopDZ[2] = false;
                                ProcessDZ20B(dz_receivedata);
                                stopDZ[2] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ35:
                            {
                                stopDZ[3] = false;
                                ProcessDZ35B(dz_receivedata);
                                stopDZ[3] = true;
                                break;
                            }

                        case (int)DZEquipmentNumber.DZ40:
                            {
                                stopDZ[4] = false;
                                ProcessDZ40B(dz_receivedata);
                                stopDZ[4] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ50:
                            {
                                stopDZ[5] = false;
                                ProcessDZ50B(dz_receivedata);
                                stopDZ[5] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ60:
                            {
                                stopDZ[6] = false;
                                ProcessDZ60B(dz_receivedata);
                                stopDZ[6] = true;
                                break;
                            }
                        case (int)DZEquipmentNumber.DZ70:
                            {
                                stopDZ[7] = false;
                                ProcessDZ70B(dz_receivedata);
                                stopDZ[7] = true;
                                break;
                            }
                    }
                }
            }
        }

        private static void ProcessDZ05A(string strData)
        { }
        private static void ProcessDZ10A(string strData)
        {
            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ10, "WD10060002");

                string str_bar_code7 = TBarCodeParse.GetBarCodeData(strData, 7);
                string str_time = "'" + DateTime.Now.ToShortDateString() + "'";
                TDataBase.InsertData("电装数据", "电装条码", str_bar_code7);//database barcode
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "电装物料", TParameterForm.dz_material);//database material
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "日期时间", str_time);//database time

                SetDZEquipFlag(TBarCodeParse.GetBarCodeData(strData, 7));
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "DZ10合格标志", "true");//database flag of correct
            }
        }
        private static void ProcessDZ20A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code7 = TBarCodeParse.GetBarCodeData(strData, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code7, (int)DZDataBase.DZ10_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code7 = TBarCodeParse.GetBarCodeData(strData, 7);
                float dz20_max_cen_force = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 30;
                float dz20_max_dis = TBarCodeParse.GetParData(strData, 28 * 4, 4) / 600;
                float dz20_max_syn_force = TBarCodeParse.GetParData(strData, 26 * 4, 4) / 30;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "DZ20合格标志", "true");//database

                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "DZ20最大滑阻阻力", dz20_max_cen_force);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "DZ20最大位移", dz20_max_dis);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code7, "DZ20同步轮力最大力", dz20_max_syn_force);//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10060002");
            }
        }
        private static void ProcessDZ35A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code7 = TBarCodeParse.GetBarCodeData(strData, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code7, (int)DZDataBase.DZ20_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                string str_bar_code_offset7 = str_bar_code9.Substring(2, 7);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ35合格标志", "true");//database
                str_bar_code9 = "'" + str_bar_code9 + "'";
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "电装条码1", str_bar_code9);//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10060002");
            }
        }
        private static void ProcessDZ40A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code_offset7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code_offset7, (int)DZDataBase.DZ35_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ40合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10060002");
            }
        }
        private static void ProcessDZ50A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code_offset7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code_offset7, (int)DZDataBase.DZ40_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ50合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10060002");
            }
        }
        private static void ProcessDZ60A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code_offset7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code_offset7, (int)DZDataBase.DZ50_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                float dz60_leakage_value = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 40;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ60合格标志", "true");//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ60泄漏结果值", dz60_leakage_value);//database


                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10060002");
            }
        }
        private static void ProcessDZ70A(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code_offset7);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code_offset7, (int)DZDataBase.DZ60_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code_offset7 = TBarCodeParse.GetBarCodeData(strData, 9).Substring(2, 7);
                float dz70_starting_current = TBarCodeParse.GetParData(strData, 26 * 4, 4) / 100;
                float dz70_noload_current = TBarCodeParse.GetParData(strData, 28 * 4, 4) / 100;
                float dz70_load_current = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 100;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ70合格标志", "true");//database

                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ70启动电流", dz70_starting_current);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ70空载电流", dz70_noload_current);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code_offset7, "DZ70负载电流", dz70_load_current);//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10060002");
            }
        }
        //
        private static void ProcessDZ05B(string strData)
        {
            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ05, "WD10060002");
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                string str_time = "'" + DateTime.Now.ToShortDateString() + "'";

                TDataBase.InsertData("电装数据", "电装条码", str_bar_code9);//database barcode           
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "电装物料", TParameterForm.dz_material);//database material
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "日期时间", str_time);//database time

                SetDZEquipFlag(TBarCodeParse.GetBarCodeData(strData, 9));

                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ05合格标志", "true");//database flag of correct

            }
        }
        private static void ProcessDZ10B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ05_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ10, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ10, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ10, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ10合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ10, "WD10060002");
            }
        }
        private static void ProcessDZ20B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ10_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10000003");
            }


            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                float dz20_max_cen_force = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 30;
                float dz20_max_dis = TBarCodeParse.GetParData(strData, 28 * 4, 4) / 600;
                float dz20_max_syn_force = TBarCodeParse.GetParData(strData, 26 * 4, 4) / 30;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ20合格标志", "true");//database

                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ20最大滑阻阻力", dz20_max_cen_force);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ20最大位移", dz20_max_dis);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ20同步轮力最大力", dz20_max_syn_force);//database

                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ20, "WD10060002");
            }
        }
        private static void ProcessDZ35B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ20_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000003");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ35, "WD10000003");
            }
        }
        private static void ProcessDZ40B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ20_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ40合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ40, "WD10060002");
            }
        }
        private static void ProcessDZ50B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ40_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ50合格标志", "true");//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ50, "WD10060002");
            }
        }
        private static void ProcessDZ60B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ50_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                float dz60_leakage_value = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 40;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ60合格标志", "true");//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ60泄漏结果值", dz60_leakage_value);//database
                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ60, "WD10060002");
            }
        }
        private static void ProcessDZ70B(string strData)
        {
            if (TBarCodeParse.GetDataD1000(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                bool bln_bar_code = TDataBase.InquireIsBarCode("电装数据", "电装条码", str_bar_code9);

                if (bln_bar_code)
                {
                    bool bln_Flag = Convert.ToBoolean(TDataBase.InquireData("电装数据", "电装条码", str_bar_code9, (int)DZDataBase.DZ60_Flag_Index));
                    if (bln_Flag)
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000002");
                    else
                        TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000003");
                }
                else
                    TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10000003");
            }

            if (TBarCodeParse.GetDataD1006(strData).Equals("0001", StringComparison.Ordinal))
            {
                string str_bar_code9 = TBarCodeParse.GetBarCodeData(strData, 9);
                float dz70_starting_current = TBarCodeParse.GetParData(strData, 26 * 4, 4) / 100;
                float dz70_noload_current = TBarCodeParse.GetParData(strData, 28 * 4, 4) / 100;
                float dz70_load_current = TBarCodeParse.GetParData(strData, 24 * 4, 4) / 100;
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ70合格标志", "true");//database

                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ70启动电流", dz70_starting_current);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ70空载电流", dz70_noload_current);//database
                TDataBase.ModifyData("电装数据", "电装条码", str_bar_code9, "DZ70负载电流", dz70_load_current);//database

                TComPort.WriteDataToPLC((int)ComNumber.COM9, (int)DZEquipmentNumber.DZ70, "WD10060002");
            }
        }

        public static void OnDataReceivedPortMark(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort_mark.ReadExisting();//datas from "@" to " \r"
        }

        private static void SetKQEquipFlag(string strBar)
        {
            XmlClass xmlClass = new XmlClass();
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ10合格标志", xmlClass.ReadNameSectionValue("AB", "KQ10", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ20合格标志", xmlClass.ReadNameSectionValue("AB", "KQ20", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ30合格标志", xmlClass.ReadNameSectionValue("AB", "KQ30", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ40合格标志", xmlClass.ReadNameSectionValue("AB", "KQ40", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ50合格标志", xmlClass.ReadNameSectionValue("AB", "KQ50", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ60合格标志", xmlClass.ReadNameSectionValue("AB", "KQ60", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ70合格标志", xmlClass.ReadNameSectionValue("AB", "KQ70", "userflag"));
            TDataBase.ModifyData("卡钳数据", "卡钳条码", strBar, "KQ80合格标志", xmlClass.ReadNameSectionValue("AB", "KQ80", "userflag"));
        }
        private static void SetDZEquipFlag(string strBar)
        {
            XmlClass xmlClass = new XmlClass();
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ05合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ05", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ10合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ10", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ20合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ20", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ35合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ35", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ40合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ40", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ50合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ50", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ60合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ60", "userflag"));
            TDataBase.ModifyData("电装数据", "电装条码", strBar, "DZ70合格标志", xmlClass.ReadNameSectionValue(TParameterForm.dz_kind, "DZ70", "userflag"));
        }
    }
}
