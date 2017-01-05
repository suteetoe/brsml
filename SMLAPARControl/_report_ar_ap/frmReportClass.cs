using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SMLERPAPARControl._report_ar_ap
{
    class frmReportClass
    {
        DateTime _start;
        int _page = 0;
        Font _font = new Font("Segoe UI", 14);

        public Font _fontStandard = new Font("Angsana New", 12, FontStyle.Regular);
        private float _drawScaleResult;
        public Point _topLeftPaperResult = new Point(20, 20);
        //       
        //
        public int _pageMax = 0;
        public int _pageCurrent = 0;
        public float _lineSpaceing = 80;
        //
        public ArrayList _objectList;
        public ArrayList _pageList;
        //
        public float _topMargin = 0;
        public float _leftMargin = 0;




    }


}
