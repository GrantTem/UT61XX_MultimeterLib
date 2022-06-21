/***************************************************************************   
** Company：iFancyit
** Author: 蜗牛君 
** Create Date: 2022-06-14
** Descriptions: 
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultimeterLib
{
    public class Structs
    {
        public struct TestValue
        {

            public Enums.Mode mode;

            public Enums.Unit unit;

            public double num;

            public int progress;

            public Enums.Bitmask1 bitmask1;
        
            public Enums.Bitmask2 bitmask2;

            public Enums.Bitmask3 bitmask3;

        }
    }
}
