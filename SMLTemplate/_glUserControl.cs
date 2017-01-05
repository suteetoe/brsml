using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SMLTemplate
{
    public partial class _glUserControl : UserControl
    {
        string _fieldScreenCode = "screen_code";
        string _fieldLineNumber = "line_number";
        string _fieldActionCode = "action_code";
        string _fieldActionName = "account_name";
        string _fieldCondition = "condition";
        string _fieldAccountDebit = "account_code_debit";
        string _fieldAccountCredit = "account_code_credit";
        string _tableName = "gl_doc_format";
        string _fieldGuidCompare = "guid_compare";

        public _glUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            //this._grid._table_name = this._tableName;
            this._grid._addColumn(this._fieldScreenCode, 1, 100, 10, false, false);
            this._grid._addColumn(this._fieldLineNumber, 1, 100, 10);
            this._grid._addColumn(this._fieldActionCode, 1, 100, 10);
            this._grid._addColumn(this._fieldActionName, 1, 100, 40);
            this._grid._addColumn(this._fieldAccountDebit, 1, 100, 10);
            this._grid._addColumn(this._fieldAccountCredit, 1, 100, 10);
            this._grid._addColumn(this._fieldCondition, 1, 100, 10);
            this._grid._addColumn(this._fieldGuidCompare, 1, 100, 5, false, false);
            this._grid._afterAddRow += new MyLib.AfterAddRowEventHandler(_grid__afterAddRow);
            this._grid._calcPersentWidthToScatter();
            this._grid._removeAllEnable(true);
            this._grid.Invalidate();
            this._systemComboBox.SelectedIndexChanged += new EventHandler(_systemComboBox_SelectedIndexChanged);
            this._screenComboBox.SelectedIndexChanged += new EventHandler(_screenComboBox_SelectedIndexChanged);
            //
            /*StreamReader __re = File.OpenText("d:\\full\\jead.txt");
            string input = null;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myQuery.Append("<query>truncate table " + this._tableName + "</query>");
            while ((input = __re.ReadLine()) != null)
            {
                string[] __split = input.Split(',');
                __myQuery.Append("<query>");
                __myQuery.Append("insert into " + this._tableName + " (screen_code,line_number,account_name) values (\'zzz\'," + __split[0].ToString() + ",\'" + __split[1].ToString() + "\')");
                __myQuery.Append("</query>");
            }
            __myQuery.Append("</node>");
            string __queryString = __myQuery.ToString();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryString);
            __re.Close();*/
            //

            //StreamReader __re = File.OpenText("d:\\full\\jead.txt");            
            //string input = null;
            //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            //StringBuilder __myQuery = new StringBuilder();
            //__myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //__myQuery.Append("<query>truncate table " + this._tableName + "</query>");
            //while ((input = __re.ReadLine()) != null)
            //{
            //    string[] __split = input.Split('\t');
            //    string __action_code = (__split[1].ToString().Length > 0) ? __split[1].ToString() : "0";
            //    __myQuery.Append("<query>");
            //    __myQuery.Append("insert into " + this._tableName + " (screen_code,action_code,account_name) values (\'zzz\'," + __action_code + ",\'" + __split[2].ToString() + "\')");
            //   // __myQuery.Append("insert into " + this._tableName + " (screen_code,line_number,action_code,account_name) values (\'zzz\'," + __split[0].ToString() + "," + __action_code + ",\'" + __split[2].ToString() + "\')");
            //    __myQuery.Append("</query>");
            //}
            //__myQuery.Append("</node>");
            //string __queryString = __myQuery.ToString();
            //string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryString);
            //__re.Close();

            this._load();
        }

        void _screenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._load();
        }

        void _systemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                สต๊อก
                ซื้อ
                ขาย
                เจ้าหนี้
                ลูกหนี้
                เงินสด/ธนาคาร
                สินทรัพย์
             */
            this._screenComboBox.Items.Clear();
            switch (this._systemComboBox.SelectedIndex)
            {
                case 0: // สต๊อก
                    this._screenComboBox.Items.Add("IF,รับสินค้าสำเร็จรูป");
                    this._screenComboBox.Items.Add("IO,เบิกสินค้า/วัตถุดิบ");
                    this._screenComboBox.Items.Add("IR,รับคืนสินค้า/วัตถุดิบจากการเบิก");
                    this._screenComboBox.Items.Add("IM,โอนสินค้า/วัตถุดิบ");
                    this._screenComboBox.Items.Add("IA,ปรับปรุงสต๊อกสินค้า/วัตถุดิบ (เกิน)");
                    this._screenComboBox.Items.Add("IS,ปรับปรุงสต๊อกสินค้า/วัตถุดิบ (ขาด)");
                    break;
                case 1: // ซื้อ
                    this._screenComboBox.Items.Add("PD,จ่ายเงินล่วงหน้า");
                    this._screenComboBox.Items.Add("PDR,รับคืนเงินล่วงหน้า");
                    this._screenComboBox.Items.Add("PC,จ่ายเงินมัดจำ");
                    this._screenComboBox.Items.Add("PCR,รับคืนเงินมัดจำ");
                    this._screenComboBox.Items.Add("PI,รับสินค้าแบบพาเชียล");
                    this._screenComboBox.Items.Add("PIA,ส่งคืน/ราคาผิด");
                    this._screenComboBox.Items.Add("PIU,ตั้งหนี้");
                    this._screenComboBox.Items.Add("PIC,ลดหนี้ี้");
                    this._screenComboBox.Items.Add("PID,เพิ่ิ่มหนี้");
                    this._screenComboBox.Items.Add("PU,ซื้อสินค้า");
                    this._screenComboBox.Items.Add("PT,ลดหนี้เจ้าหนี้, ลดสินค้า");
                    this._screenComboBox.Items.Add("PA,เพิ่มหนี้เจ้าหนี้,เพิ่มสินค้า");
                    break;
                case 2: // ขาย
                    this._screenComboBox.Items.Add("SD,รับเงินล่วงหน้า");
                    this._screenComboBox.Items.Add("SDR,คืนเงินล่วงหน้า");
                    this._screenComboBox.Items.Add("SRV,รับเงินมัดจำ");
                    this._screenComboBox.Items.Add("SRT,คืนเงินมัดจำ");
                    this._screenComboBox.Items.Add("SI,ขายสินค้า/บริการ");
                    this._screenComboBox.Items.Add("ST,รับคืนคืนสินค้า/ลดหนี้");
                    this._screenComboBox.Items.Add("SA,เพิ่มหนี้");
                    this._screenComboBox.Items.Add("SIP,ขาย POS");
                    break;
                case 3: // เจ้าหนี้
                    this._screenComboBox.Items.Add("COB,ตั้งหนี้อื่นๆ(เจ้าหนี้)");
                    this._screenComboBox.Items.Add("CCO,ลดหนี้อื่นๆ(เจ้าหนี้)");
                    this._screenComboBox.Items.Add("CDO,เพิ่มหนี้อื่นๆ(เจ้าหนี้)");
                    this._screenComboBox.Items.Add("DE,จ่ายชำระหนี้(เจ้าหนี้)");
                    this._screenComboBox.Items.Add("CWO,ตัดหนี้สูญ(เจ้าหนี้)");
                    break;
                case 4: // ลูกหนี้
                    this._screenComboBox.Items.Add("AOB,ตั้งหนี้อื่นๆ(ลูกหนี้)");
                    this._screenComboBox.Items.Add("ACO,ลดหนี้อื่นๆ(ลูกหนี้)");
                    this._screenComboBox.Items.Add("ADO,เพิ่มหนี้อื่นๆ(ลูกหนี้)");
                    this._screenComboBox.Items.Add("EE,รับชำระหนี้/ออกใบเสร็จรับเงิน");
                    this._screenComboBox.Items.Add("AWO,ตัดหนี้สูญ(ลูกหนี้)");
                    break;
                case 5: // เงินสดธนาคาร
                    this._screenComboBox.Items.Add("OI,รายได้อื่น ๆ");
                    this._screenComboBox.Items.Add("OCN,ลดหนี้รายได้อื่น ๆ");
                    this._screenComboBox.Items.Add("ODN,เพิ่มหนี้รายได้อื่น ๆ");
                    this._screenComboBox.Items.Add("EPO,ค่าใช้จ่ายอื่น ๆ");
                    this._screenComboBox.Items.Add("EPC,ลดหนี้ค่าใช้จ่ายอื่น ๆ");
                    this._screenComboBox.Items.Add("EPD,เพิ่มหนี้ค่าใช้จ่ายอื่น ๆ");
                    this._screenComboBox.Items.Add("PCR,บันทึกรับเงินสดย่อย");
                    this._screenComboBox.Items.Add("PCD,ขอเบิกเงินสดย่อย");
                    this._screenComboBox.Items.Add("PRM,บันทึกรับคืนเงินสดย่อย");
                    this._screenComboBox.Items.Add("DM,บันทึกฝากเงิน");
                    this._screenComboBox.Items.Add("WM,บันทึกถอนเงิน");
                    this._screenComboBox.Items.Add("CDE,บันทึกเปลี่ยนเช็คนำฝาก");
                    this._screenComboBox.Items.Add("CHD,บันทึกนำฝากเช็ครับ");
                    this._screenComboBox.Items.Add("CHN,บันทึกนำเช็ครับเข้าใหม่");
                    this._screenComboBox.Items.Add("CHP,บันทึกเช็ครับผ่าน");
                    this._screenComboBox.Items.Add("CRC,ยกเลิกเช็ครับ");
                    this._screenComboBox.Items.Add("CP,บันทึกเช็คจ่าย");
                    this._screenComboBox.Items.Add("CPE,บันทึกเปลี่ยนเช็คจ่าย");
                    this._screenComboBox.Items.Add("CPP,บันทึกเช็คจ่ายผ่าน");
                    this._screenComboBox.Items.Add("CPR,บันทึกเช็คจ่ายคืน");
                    this._screenComboBox.Items.Add("CR,บันทึกเช็ครับ");
                    this._screenComboBox.Items.Add("CRD,บันทึกขั้นเงินบัตรเครดิต");
                    this._screenComboBox.Items.Add("CRT,บันทึกเช็ครับคืน");
                    this._screenComboBox.Items.Add("CSR,ขายลดเช็ครับ");
                    this._screenComboBox.Items.Add("CPB,เช็คจ่ายยกมา");
                    this._screenComboBox.Items.Add("CHB,เช็็ครับยกมา");
                    this._screenComboBox.Items.Add("CHC,ยกเลิกเช็คจ่าย");
                    this._screenComboBox.Items.Add("BCH,บันทึกค่าใช้จ่ายธนาคาร");
                    this._screenComboBox.Items.Add("IMI,บันทึกเงินโอนเข้า");
                    this._screenComboBox.Items.Add("IFB,บันทึกรายได้จากธนาคาร");
                    this._screenComboBox.Items.Add("TM,โอนเงินระหว่างธนาคาร");
                    break;
            }
        }

        void _grid__afterAddRow(object sender, int row)
        {
            if (this._screenComboBox.SelectedItem != null)
            {
                string __newGuid = Guid.NewGuid().ToString();
                this._grid._cellUpdate(row, this._fieldGuidCompare, __newGuid, false);
                string[] __split = this._screenComboBox.SelectedItem.ToString().Split(',');
                if (__split.Length > 0)
                {
                    string __screenCode = __split[0].ToString();
                    this._grid._cellUpdate(row, this._fieldScreenCode, "[" + __screenCode + "]", false);
                }
                this._grid._cellUpdate(row, this._fieldLineNumber, (row+1).ToString(), false);
            }
        }

        private void _load()
        {
            this._grid._clear();
            if (this._screenComboBox.SelectedItem != null)
            {
                string[] __split = this._screenComboBox.SelectedItem.ToString().Split(',');
                if (__split.Length > 0)
                {
                    string __screenCode = __split[0].ToString();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __data = __myFrameWork._queryShort("select * from " + this._tableName + " where screen_code=\'[" + __screenCode + "]\' order by line_number").Tables[0];
                    this._grid._loadFromDataTable(__data);
                    for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                    {
                        string __guid = this._grid._cellGet(__row, this._fieldGuidCompare).ToString().Trim();
                        if (__guid.Length == 0)
                        {
                            string __newGuid = Guid.NewGuid().ToString();
                            this._grid._cellUpdate(__row, this._fieldGuidCompare, __newGuid, false);
                        }
                    }
                }
            }
            this._grid.Invalidate();
        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._screenComboBox.SelectedItem != null)
            {
                string[] __split = this._screenComboBox.SelectedItem.ToString().Split(',');
                if (__split.Length > 0)
                {
                    string __screenCode = __split[0].ToString();
                    this._grid._updateRowIsChangeAll(true);
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __myQuery.Append("<query>delete from " + this._tableName + " where screen_code=\'[" + __screenCode + "]\'</query>");
                    __myQuery.Append(this._grid._createQueryForInsert(this._tableName, "", ""));
                    __myQuery.Append("</node>");
                    string __queryString = __myQuery.ToString();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryString);
                    MessageBox.Show((__result.Length == 0) ? "Success" : __result.ToString());
                }
            }
        }
    }
}
