using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPICInfo
{
    public static class _global
    {
        public static int _infoStkProfitNumber(_infoStkProfitEnum mode)
        {
            switch (mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า: return 0;
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า_เอกสาร: return 3;
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า: return 4;
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร: return 5;
                case _infoStkProfitEnum.กำไรขั้นต้น_เอกสาร_สินค้า: return 6;
                case _infoStkProfitEnum.กำไรขั้นต้น_เอกสาร: return 1;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า: return 2;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า: return 7;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร: return 8;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร: return 9;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า: return 10;
            }
            return -1;
        }
    }

    public enum _infoStkProfitEnum
    {
        กำไรขั้นต้น_สินค้า,
        กำไรขั้นต้น_สินค้า_เอกสาร,
        กำไรขั้นต้น_สินค้า_ลูกค้า,
        กำไรขั้นต้น_สินค้า_ลูกค้า_เอกสาร,
        กำไรขั้นต้น_เอกสาร,
        กำไรขั้นต้น_เอกสาร_สินค้า,
        กำไรขั้นต้น_ลูกค้า,
        กำไรขั้นต้น_ลูกค้า_สินค้า,
        กำไรขั้นต้น_ลูกค้า_สินค้า_เอกสาร,
        กำไรขั้นต้น_ลูกค้า_เอกสาร,
        กำไรขั้นต้น_ลูกค้า_เอกสาร_สินค้า
    }
}
