/***************************************************************************   
** Company：iFancyit
** Author: 蜗牛君 
** Create Date: 2022-06-14
** Descriptions: 控制函数库
                 -------额外方法
***************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultimeterLib
{
    /// <summary>
    /// 额外方法提供
    /// </summary>
    public static class Operation
    {

        /// <summary>
        /// 错误信息
        /// </summary>
        internal static string strErrorInfo = string.Empty;

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public static string GetErrorInfo()
        {
            return strErrorInfo;
        }


        /// <summary>
        /// 如果byte[]={0x11,0xff,0x45}; 
        ///要显示出string= "0x11,0xff,0x45" 的效果; 
        /// </summary>
        /// <param name="reb"></param>
        /// <returns></returns>
        internal static string DisPackage(byte[] reb)
        {
            string temp = string.Empty;
            foreach (byte b in reb)
                temp += "0x" + b.ToString("X2") + ",";
            return temp.TrimEnd(',');
        }


        /// <summary>
        /// 字节数组转字符串形式
        /// </summary>
        /// <param name="reb"></param>
        /// <returns></returns>
        internal static string ByteToString(byte[] reb)
        {
            string temp = string.Empty;
            foreach (byte b in reb)
                temp += b.ToString("X2");
            return temp;
        }


        /// <summary>
        /// 字节数组大小端转换
        /// </summary>
        /// <param name="reb"></param>
        /// <returns></returns>
        internal static byte[] BigLittleEdgeTrans(byte[] reb)
        {
            byte[] temp = new byte[reb.Length];
            for (int iLoop = reb.Length - 1; iLoop >= 0; iLoop--)
            {
                temp[reb.Length - 1 - iLoop] = reb[iLoop];
            }
            return temp;
        }

        /// <summary>
        /// ReverseData
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        internal static byte[] ReverseData(ushort In)
        {
            byte[] result = new byte[2];
            result[0] = (byte)(In & 0xFF);
            result[1] = (byte)((In & 0xFF00) >> 8);
            return result;
        }

        public static string TransToString(Structs.TestValue value)
        {
            string Result = string.Empty;


            Result += "\r\nmode = ";
            Result +=    Enum.GetName(typeof(Enums.Mode), value.mode);

            Result += "\r\nunit = ";
            Result += Enum.GetName(typeof(Enums.Unit), value.unit);

            Result += String.Format("\r\nnum = {0}", value.num);

            Result += "\r\nbitmask1 = ";
            Result += Enum.GetName(typeof(Enums.Bitmask1), value.bitmask1);

            Result += "\r\nbitmask2 = ";
            Result += Enum.GetName(typeof(Enums.Bitmask2), value.bitmask2);

            Result += "\r\nbitmask3 = ";
            Result += Enum.GetName(typeof(Enums.Bitmask3), value.bitmask3);

            return Result;

        }

    }
}

