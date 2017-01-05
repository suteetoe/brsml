using System;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace SMLProcess
{
    public class _docFlow
    {
        /// <summary>
        /// ประมวลผลใบเสนอซื้อ,การอนุมัติ
        /// ใบเสนอซื้อจะถูกอ้างอิงจาก การอนุมัติ และยกเลิกเท่านั้น
        /// ใบอนุมัติเสนอซื้อ จะถูกอ้างอิงจากการสั่งซื้อเท่านั้น
        /// </summary>
        public void _processAll(_g.g._transControlTypeEnum transFlag, string itemCodePack, string docNoPack)
        {
            _docFlowThread __proces = new _docFlowThread(transFlag, itemCodePack, docNoPack);
            Thread __thread = new Thread(new ThreadStart(__proces._processAll));
            __thread.Start();
        }
    }
}
