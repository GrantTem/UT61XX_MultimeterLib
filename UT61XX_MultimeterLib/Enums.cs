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
    public class Enums
    {


        public enum Mode
        {
            ACV = 0,
            ACmV,
            DCV,
            DCmV,
            Hz,
            Percent,  //%
            OHM,
            CONT,
            DIDOE,
            CAP,
            Celsius, //'°C',
            Fahrenheit, //'°F', 
            DCuA,
            ACuA,
            DCmA,
            ACmA,
            DCA,
            ACA,
            HFE,
            Live,
            NCV,
            LozV,
            // ACA, not clear below
            // DCA, 
            // LPF,
            // AC/DC,
            // LPF,
            // AC+DC,
            // LPF, 
            // AC+DC, 
            // INRUSH

        }


        public enum Bitmask1
        {
            Max = 0,
            Min,
            Hold,
            Rel
        }

        public enum Bitmask2
        {
            Auto = 0,
            Battery,
            HvWarning
        }


        public enum Bitmask3
        {
            DC = 0,
            PeakMax,
            PeakMin,
            BarPol
        }




        public enum Unit
        {
            Percent,  //%
            A,
            V,
            mA,
            mV,
            uA,
            nF,
            uF,
            mF,
            Ω,
            Hz,
            kHz,
            MHz,
            kΩ,
            MΩ,
            Celsius, //'°C',
            Fahrenheit, //'°F', 
            B,
            NCV,
        }



        //# units based on mode and range
        //_UNITS = {
        //    '%': { '0': '%'},
        //              'AC+DC': { '1': 'A'},
        //              'AC+DC2': { '1': 'A'},
        //              'AC/DC': { '0': 'V', '1': 'V', '2': 'V', '3': 'V'},
        //              'ACA': { '1': 'A'},
        //              'ACV': { '0': 'V', '1': 'V', '2': 'V', '3': 'V'},
        //              'ACmA': { '0': 'mA', '1': 'mA'},
        //              'ACmV': { '0': 'mV'},
        //              'ACuA': { '0': 'uA', '1': 'uA'},
        //              'CAP': {
        //        '0': 'nF',
        //                      '1': 'nF',
        //                      '2': 'uF',
        //                      '3': 'uF',
        //                      '4': 'uF',
        //                      '5': 'mF',
        //                      '6': 'mF',
        //                      '7': 'mF'},
        //              'CONT': { '0': 'Ω'},
        //              'DCA': { '1': 'A'},
        //              'DCV': { '0': 'V', '1': 'V', '2': 'V', '3': 'V'},
        //              'DCmA': { '0': 'mA', '1': 'mA'},
        //              'DCmV': { '0': 'mV'},
        //              'DCuA': { '0': 'uA', '1': 'uA'},
        //              'DIDOE': { '0': 'V'},
        //              'Hz': {
        //        '0': 'Hz',
        //                     '1': 'Hz',
        //                     '2': 'kHz',
        //                     '3': 'kHz',
        //                     '4': 'kHz',
        //                     '5': 'MHz',
        //                     '6': 'MHz',
        //                     '7': 'MHz'},
        //              'LPF': { '0': 'V', '1': 'V', '2': 'V', '3': 'V'},
        //              'LozV': { '0': 'V', '1': 'V', '2': 'V', '3': 'V'},
        //              'OHM': {
        //        '0': 'Ω',
        //                      '1': 'kΩ',
        //                      '2': 'kΩ',
        //                      '3': 'kΩ',
        //                      '4': 'MΩ',
        //                      '5': 'MΩ',
        //                      '6': 'MΩ'},
        //              '°C': { '0': '°C', '1': '°C'},
        //              '°F': { '0': '°F', '1': '°F'},
        //              'HFE': { '0': 'B'},
        //              'NCV': { '0': 'NCV'}
        //}


    }
}
