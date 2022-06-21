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
    public class UT61BPlus_Multimeter:Multimeter
    {

        public const ushort vid = 0x10c4;
        public const ushort pid = 0xea80;

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UT61BPlus_Multimeter(uint index)
            : base(vid, pid,index)
        {

        }

        #endregion

        #region 成员变量

        #endregion

   

        #region  功能函数
 

        #endregion

    }




}
