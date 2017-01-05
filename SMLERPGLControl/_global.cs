using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGLControl
{
    public class _global
    {
    }

    public class _vatRequestData
    {
        public string _vatDocNo;
        public DateTime _vatDate;
        public decimal _vatRate;
        /// <summary>
        /// ฐานภาษี
        /// </summary>
        public decimal _vatBaseAmount;
        /// <summary>
        /// ยอดภาษี
        /// </summary>
        public decimal _vatAmount;
        /// <summary>
        /// รวมภาษี
        /// </summary>
        public decimal _vatTotalAmount;
        /// <summary>
        /// ยอดยกเว้นภาษี
        /// </summary>
        public decimal _vatExceptAmount;
    }
}
