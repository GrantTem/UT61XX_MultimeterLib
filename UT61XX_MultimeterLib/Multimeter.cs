/***************************************************************************************   
** Company：iFancyit
** Author: 蜗牛君 
** Create Date: 2022-06-14
** Descriptions: 
** Remark:
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Threading;
using CP2110Lib;

namespace MultimeterLib
{
    public class Multimeter
    {
        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Multimeter(ushort vid, ushort pid, uint index)
            : base()
        {
            this.index = index;
            this.com = new CP2110(vid, pid, index);
            //this.com.BaudRate = 2400;
            //this.com.Port = COM;
        }

        #endregion

        #region 成员变量
        public uint index;


        private const int SteadyTime = 200;
        private CP2110 com;

        #endregion

        #region  功能函数
        public bool Connect()
        {
            try
            {
                if (com.Connect(9600, CP2110.HID_UART_DATA_BITS.HID_UART_EIGHT_DATA_BITS, CP2110.HID_UART_PARITY.HID_UART_NO_PARITY, CP2110.HID_UART_STOP_BIT.HID_UART_SHORT_STOP_BIT, CP2110.HID_UART_FLOW_CONTROL.HID_UART_NO_FLOW_CONTROL))
                {
                    return true;
                }
                else
                {
                    Operation.strErrorInfo = "请检查串口是否被占用！";
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                Operation.strErrorInfo = ex.ToString();
                return false;
            }
        }



        /// <summary>
        /// FCS校验
        /// </summary>
        public static bool CheckFCS(byte high, byte low, byte[] msg_ptr, int length)
        {

            byte x;

            ushort xorResult;

            xorResult = 0;

            for (x = 0; x < length; x++)
            {
                xorResult = (ushort)(xorResult + msg_ptr[x]);
            }

            if (((high << 8) + low) != xorResult)
            {
                return false;
            }

            return true;
        }



        int ProCodeCount = 19;



        private void GetValidResult(byte[] btResult, ref List<Structs.TestValue> Datas)
        {
            try
            {
                Structs.TestValue value = new Structs.TestValue();
                /********************************************                
                //come form https://github.com/ljakob/unit_ut61eplus
"""
protocol of UT61+
parts from an USB trace, parts from experimenting myself, parts from https://github.com/gulux/Uni-T-CP2110
and many 'inspirations' form the decompiled bluetooth app
example response in mV AC
ab . => header
cd . => header
10   => number of bytes that follow including 'checksum'
01   => mode
30 0 => range (character starting at '0')
20   => digit MSB (can be ' ' or '-') ! number can also be ' OL.  '
20   => digit
35 5 => digit
33 3 => digit
2e . => digit
35 5 => digit
34 4 => digit LSB
01   => progress1
00   => progress2 => progress = progress1*10 + progress2 - meaning is not clear yet
30 0 => Bitmask: Max,Min,Hold,Rel
34 4 => Bitmask: !Auto,Battery,HvWarning
30 0 => Bitmask: !DC,PeakMax,PeakMin,BarPol
03   => sum over all - MSB - sum from 0xab to 0x30
8d . => sum over all - LSB
"""

                example:
                TX:AB CD 03 5E 01 D9

                RX:ab cd 10 0c 30 20 20 20 20 30 2e 30 00 00 30 30 30 03 62   
                ********************************************/
                if (btResult.Length < ProCodeCount) //长度不够
                {
                    return;
                }

                int iCount = 0;

                if (btResult[iCount++] != 0xab)
                {
                    return;
                }

                if (btResult[iCount++] != 0xcd)
                {
                    return;
                }

                if (btResult[iCount++] != 0x10)
                {
                    return;
                }


                value.mode = (Enums.Mode)Enum.ToObject(typeof(Enums.Mode), btResult[iCount]);
                iCount++;


                byte range = (byte)(btResult[iCount] - 0x30);

                iCount++;


                switch (value.mode)
                {
                    case Enums.Mode.Percent:
                        value.unit = Enums.Unit.Percent;
                        break;

                    case Enums.Mode.ACV:
                        value.unit = Enums.Unit.V;
                        break;

                    case Enums.Mode.ACmA:
                        value.unit = Enums.Unit.mA;
                        break;
                    case Enums.Mode.ACmV:
                        value.unit = Enums.Unit.mV;
                        break;

                    case Enums.Mode.ACuA:
                        value.unit = Enums.Unit.uA;
                        break;

                    case Enums.Mode.CAP:
                        switch (range)
                        {

                            case 0:
                                value.unit = Enums.Unit.nF;
                                break;
                            case 1:
                                value.unit = Enums.Unit.nF;
                                break;
                            case 2:
                                value.unit = Enums.Unit.uF;
                                break;
                            case 3:
                                value.unit = Enums.Unit.uF;
                                break;
                            case 4:
                                value.unit = Enums.Unit.uF;
                                break;
                            case 5:
                                value.unit = Enums.Unit.mF;
                                break;
                            case 6:
                                value.unit = Enums.Unit.mF;
                                break;
                            case 7:
                                value.unit = Enums.Unit.mF;
                                break;
                            default:
                                break;
                        }
                        break;

                    case Enums.Mode.CONT:
                        value.unit = Enums.Unit.Ω;
                        break;
                    case Enums.Mode.DCA:
                        value.unit = Enums.Unit.A;
                        break;
                    case Enums.Mode.DCV:
                        value.unit = Enums.Unit.V;
                        break;
                    case Enums.Mode.DCmA:
                        value.unit = Enums.Unit.mA;
                        break;
                    case Enums.Mode.DCmV:
                        value.unit = Enums.Unit.mV;
                        break;
                    case Enums.Mode.DCuA:
                        value.unit = Enums.Unit.uA;
                        break;
                    case Enums.Mode.DIDOE:
                        value.unit = Enums.Unit.V;
                        break;
                    case Enums.Mode.Hz:

                        switch (range)
                        {
                            case 0:
                                value.unit = Enums.Unit.Hz;
                                break;
                            case 1:
                                value.unit = Enums.Unit.Hz;
                                break;
                            case 2:
                                value.unit = Enums.Unit.kHz;
                                break;
                            case 3:
                                value.unit = Enums.Unit.kHz;
                                break;
                            case 4:
                                value.unit = Enums.Unit.kHz;
                                break;
                            case 5:
                                value.unit = Enums.Unit.MHz;
                                break;
                            case 6:
                                value.unit = Enums.Unit.MHz;
                                break;
                            case 7:
                                value.unit = Enums.Unit.MHz;
                                break;
                            default:
                                break;
                        }
                        break;

                    case Enums.Mode.LozV:
                        value.unit = Enums.Unit.V;
                        break;
                    case Enums.Mode.OHM:

                        switch (range)
                        {
                            case 0:
                                value.unit = Enums.Unit.Ω;
                                break;
                            case 1:
                                value.unit = Enums.Unit.kΩ;
                                break;
                            case 2:
                                value.unit = Enums.Unit.kΩ;
                                break;
                            case 3:
                                value.unit = Enums.Unit.kΩ;
                                break;
                            case 4:
                                value.unit = Enums.Unit.MΩ;
                                break;
                            case 5:
                                value.unit = Enums.Unit.MΩ;
                                break;
                            case 6:
                                value.unit = Enums.Unit.MΩ;
                                break;
                            default:
                                break;
                        }
                        break;
                    case Enums.Mode.Celsius:

                        value.unit = Enums.Unit.Celsius;
                        break;
                    case Enums.Mode.Fahrenheit:
                        value.unit = Enums.Unit.Fahrenheit;
                        break;
                    case Enums.Mode.HFE:
                        value.unit = Enums.Unit.B;
                        break;
                    case Enums.Mode.NCV:
                        value.unit = Enums.Unit.NCV;
                        break;

                    default:
                        break;


                }



                string contents = Encoding.UTF8.GetString(btResult, iCount, 7);

                iCount += 7;

                value.num = Convert.ToDouble(contents);


                value.progress = btResult[iCount++] * 10;
                value.progress += btResult[iCount++];


                value.bitmask1 = (Enums.Bitmask1)Enum.ToObject(typeof(Enums.Bitmask1), btResult[iCount] - 0x30);
                iCount++;

                value.bitmask2 = (Enums.Bitmask2)Enum.ToObject(typeof(Enums.Bitmask2), btResult[iCount] - 0x30);
                iCount++;

                value.bitmask3 = (Enums.Bitmask3)Enum.ToObject(typeof(Enums.Bitmask3), btResult[iCount] - 0x30);
                iCount++;




                if (!CheckFCS(btResult[iCount++], btResult[iCount++], btResult, ProCodeCount - 2))
                {

                    return;
                }


                Datas.Add(value);
                byte[] ResultTemp = new byte[btResult.Length - ProCodeCount];
                Array.Copy(btResult, ProCodeCount, ResultTemp, 0, btResult.Length - ProCodeCount);

                btResult = ResultTemp;

                GetValidResult(btResult, ref Datas);
            }
            catch (Exception ex)
            {

            }
        }


        public bool GetValue(out Structs.TestValue value)
        {
            value = new Structs.TestValue();
            byte[] RxData = new byte[1];



            this.com.SendAndReceive(new byte[] { 0xAB, 0xCD, 0x03, 0x5E, 0x01, 0xD9 }, out RxData); //deal the situation above


            List<Structs.TestValue> Datas = new List<Structs.TestValue>();
            GetValidResult(RxData, ref Datas);
            if (Datas.Count != 0)
            {
                value = Datas[0];
                return true;
            }
            else
            {
                Operation.strErrorInfo = "No value get";
                return false;
            }

        }

        public bool Disconnect()
        {
            try
            {
                com.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                Operation.strErrorInfo = ex.ToString();
                return false;
            }
        }

        #endregion

    }




}





