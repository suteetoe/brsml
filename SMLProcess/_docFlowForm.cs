using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SMLProcess
{
    public partial class _docFlowForm : Form
    {
        private ArrayList _columnObject;
        private ArrayList _docFlowObject = new ArrayList();
        private MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

        public _docFlowForm(_g.g._transControlTypeEnum icTransTypeEnum, string refTransFieldName, string refFieldName, string docNo)
        {
            InitializeComponent();
            // Create Object
            _docFlowObjectClass __docFlow = this._createTransDocFlow(icTransTypeEnum, refTransFieldName, refFieldName, docNo, Guid.NewGuid().ToString());
            if (__docFlow != null)
            {
                // Create Control
                this._columnObject = new ArrayList();
                for (int __column = 0; __column < 100; __column++)
                {
                    this._columnObject.Add(new _columnObjectClass());
                }
                //
                _createControl(__docFlow, 0, -1, -1);
                for (int __column = 0; __column < 100; __column++)
                {
                    _columnObjectClass __getObject = (_columnObjectClass)this._columnObject[__column];
                    for (int __row = 0; __row < 100; __row++)
                    {
                        _gridObjectClass __getData = (_gridObjectClass)__getObject._row[__row];
                        if (__getData._cells != null)
                        {
                            string __name = (__getData._cells._transMode == 1) ? _g.g._transFlagGlobal._transName(__getData._cells._icTransType) : _g.g._transFlagGlobal._transName(__getData._cells._icTransType);
                            _docFlowUserControl __newControl = new _docFlowUserControl(__name, __getData._cells);
                            __newControl.Location = new Point(((__newControl.Width + 70) * __column) + this.Padding.Left, ((__newControl.Height + 10) * __row) + this.Padding.Top);
                            this.Controls.Add(__newControl);
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Pen __myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            foreach (Control __getControlParent in this.Controls)
            {
                if (__getControlParent.GetType() == typeof(_docFlowUserControl))
                {
                    _docFlowUserControl __getParent = (_docFlowUserControl)__getControlParent;
                    foreach (Control __getControlNext in this.Controls)
                    {
                        _docFlowUserControl __getNext = (_docFlowUserControl)__getControlNext;
                        if (__getParent._guidNext.Equals(__getNext._guidParent))
                        {
                            // y=แนวนอน,x=แนวตั้ง
                            int __x1 = __getParent.Location.X + __getParent.Width;
                            int __y1 = __getParent.Location.Y + (__getParent.Height / 2);
                            int __x2 = __getNext.Location.X;
                            int __y2 = __getNext.Location.Y + (__getParent.Height / 2);
                            e.Graphics.DrawLine(__myPen, new Point(__x1, __y1), new Point(__x2, __y2));
                        }
                    }
                }
            }
            __myPen.Dispose();
            base.OnPaint(e);
        }

        private string _guidNew()
        {
            return Guid.NewGuid().ToString();
        }

        private void _createControl(_docFlowObjectClass docFlow, int columnNumber, int rowBegin, int columnBegin)
        {
            for (int __loop = 0; __loop < docFlow._docFlowDetailObject.Count; __loop++)
            {
                _docFlowDetailObjectClass __docs = (_docFlowDetailObjectClass)docFlow._docFlowDetailObject[__loop];
                for (int __row = 0; __row < __docs._nodes.Count; __row++)
                {
                    if (__docs._nodes[__row] != null)
                    {
                        _createControl((_docFlowObjectClass)__docs._nodes[__row], columnNumber + 1, __loop, columnNumber);
                    }
                }
                _columnObjectClass __getData = (_columnObjectClass)this._columnObject[columnNumber];
                ((_gridObjectClass)__getData._row[__getData.__rowCount++])._cells = __docs;
                this._columnObject[columnNumber] = (_columnObjectClass)__getData;
            }
        }

        /// <summary>
        /// ดึงรายการแม่จาก ic_trans
        /// </summary>
        /// <param name="icTransTypeEnum"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        private _docFlowDetailObjectClass _createTransCurrentNode(_g.g._transControlTypeEnum icTransTypeEnum, string docNo, string refDocNo, string guidParent)
        {
            _docFlowObjectClass __docFlow = new _docFlowObjectClass();
            string __queryDoc = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time + "," + _g.d.ic_trans._total_amount +
                 " from " + _g.d.ic_trans._table +
                 " where " + _g.d.ic_trans._doc_no + "=\'" + docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(icTransTypeEnum).ToString();
            DataTable __data = __myFrameWork._queryShort(__queryDoc).Tables[0];
            if (__data.Rows.Count != 0)
            {
                _docFlowDetailObjectClass __docDetail = new _docFlowDetailObjectClass();
                __docDetail._refDocNo = refDocNo;
                __docDetail._docNo = docNo;
                __docDetail._docDate = MyLib._myGlobal._convertDateFromQuery(__data.Rows[0][_g.d.ic_trans._doc_date].ToString());
                __docDetail._docTime = __data.Rows[0][_g.d.ic_trans._doc_time].ToString();
                __docDetail._icTransType = _g.g._transFlagGlobal._transFlag(icTransTypeEnum);
                __docDetail._amount = MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_trans._total_amount].ToString());
                __docDetail._transMode = 1;
                __docDetail._guidParent = guidParent;
                __docDetail._guidNext = Guid.NewGuid().ToString();
                return __docDetail;
            }
            return null;
        }

        /// <summary>
        /// ดึงรายการแม่จาก apartrans
        /// </summary>
        /// <param name="transTypeEnum"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        private _docFlowDetailObjectClass _createApArTransCurrentNode(_g.g._transControlTypeEnum transTypeEnum, string docNo, string refDocNo, string guidParent)
        {
            _docFlowObjectClass __docFlow = new _docFlowObjectClass();
            string __queryDoc = "select " + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._total_net_value +
                 " from " + _g.d.ap_ar_trans._table +
                 " where " + _g.d.ap_ar_trans._doc_no + "=\'" + docNo + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(transTypeEnum).ToString();
            DataTable __data = __myFrameWork._queryShort(__queryDoc).Tables[0];
            if (__data.Rows.Count != 0)
            {
                _docFlowDetailObjectClass __docDetail = new _docFlowDetailObjectClass();
                __docDetail._refDocNo = refDocNo;
                __docDetail._docNo = docNo;
                __docDetail._docDate = MyLib._myGlobal._convertDateFromQuery(__data.Rows[0][_g.d.ap_ar_trans._doc_date].ToString());
                __docDetail._docTime = "17:00";
                __docDetail._icTransType = _g.g._transFlagGlobal._transFlag(transTypeEnum);
                __docDetail._amount = MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ap_ar_trans._total_net_value].ToString());
                __docDetail._transMode = 2;
                __docDetail._guidParent = guidParent;
                __docDetail._guidNext = Guid.NewGuid().ToString();
                return __docDetail;
            }
            return null;
        }

        /// <summary>
        /// ดึงรายการอ้างอิง ic_trans_detail
        /// </summary>
        /// <param name="icTransTypeEnum"></param>
        /// <param name="refDetailFieldName">อ้างอืงใน ic_trans_detail</param>
        /// <param name="refFieldName">อ้างอิงใน ic_trans</param>
        /// <param name="refDocNo">เอกสารอ้างอิง</param>
        /// <returns></returns>
        private DataTable _selectTransDetailRef(_g.g._transControlTypeEnum icTransTypeEnum, string refDetailFieldName, string refFieldName, string refDocNo)
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("select distinct " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._trans_flag);
            __query.Append(" from " + _g.d.ic_trans_detail._table);
            __query.Append(" where " + refDetailFieldName + "=\'" + refDocNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(icTransTypeEnum).ToString());
            if (refFieldName.Length > 0)
            {
                __query.Append(" union all ");
                __query.Append("select distinct " + _g.d.ic_trans._doc_no + " as " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans._trans_flag + " as " + _g.d.ic_trans_detail._trans_flag);
                __query.Append(" from " + _g.d.ic_trans._table);
                __query.Append(" where " + refFieldName + "=\'" + refDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(icTransTypeEnum).ToString());
            }
            StringBuilder __queryTemp = new StringBuilder();
            __queryTemp.Append("select distinct " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._trans_flag);
            __queryTemp.Append(" from (" + __query.ToString() + ") as temp1");
            return __myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        private DataTable _selectTransRef(_g.g._transControlTypeEnum icTransTypeEnum, string refDetailFieldName, string refFieldName, string refDocNo)
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans_detail._trans_flag);
            __query.Append(" from " + _g.d.ic_trans._table);
            __query.Append(" where " + refDetailFieldName + "=\'" + refDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(icTransTypeEnum).ToString());
            //if (refFieldName.Length > 0)
            //{
            //    __query.Append(" union all ");
            //    __query.Append("select distinct " + _g.d.ic_trans._doc_no + " as " + _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans._trans_flag + " as " + _g.d.ic_trans_detail._trans_flag);
            //    __query.Append(" from " + _g.d.ic_trans._table);
            //    __query.Append(" where " + refFieldName + "=\'" + refDocNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(icTransTypeEnum).ToString());
            //}
            //StringBuilder __queryTemp = new StringBuilder();
            //__queryTemp.Append("select distinct " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans_detail._trans_flag);
            //__queryTemp.Append(" from (" + __query.ToString() + ") as temp1");
            return __myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        private DataTable _selectCBTransDetailRef(_g.g._transControlTypeEnum icTransTypeEnum, string refDetailFieldName, string refFieldName, string refDocNo, string extraWhere)
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("select " + _g.d.cb_trans_detail._doc_no + "," + _g.d.cb_trans_detail._trans_flag + "," + _g.d.cb_trans_detail._doc_date + "," + _g.d.cb_trans_detail._amount );
            __query.Append(" from " + _g.d.cb_trans_detail._table);
            __query.Append(" where " + refDetailFieldName + "=\'" + refDocNo + "\' ");

            return __myFrameWork._queryShort(__query.ToString()).Tables[0];

        }

        /// <summary>
        /// ดึงรายการอ้างอิงจาก araptrans
        /// </summary>
        /// <param name="transTypeEnum"></param>
        /// <param name="refDetailFieldName"></param>
        /// <param name="refFieldName"></param>
        /// <param name="refDocNo"></param>
        /// <returns></returns>
        private DataTable _selectApArTransRef(_g.g._transControlTypeEnum transTypeEnum, string refDetailFieldName, string refFieldName, string refDocNo, string extraWhere)
        {
            StringBuilder __query = new StringBuilder();
            __query.Append("select distinct " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._trans_flag);
            __query.Append(" from " + _g.d.ap_ar_trans_detail._table);
            __query.Append(" where " + refDetailFieldName + "=\'" + refDocNo + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(transTypeEnum).ToString());
            if (extraWhere.Length > 0)
            {
                __query.Append(" and (" + extraWhere + ")");
            }
            if (refFieldName.Length > 0)
            {
                __query.Append(" union all ");
                __query.Append("select distinct " + _g.d.ap_ar_trans._doc_no + " as " + " as " + _g.d.ap_ar_trans_detail._doc_ref + "," + _g.d.ap_ar_trans._trans_flag + " as " + _g.d.ap_ar_trans_detail._trans_flag);
                __query.Append(" from " + _g.d.ap_ar_trans._table);
                __query.Append(" where " + refFieldName + "=\'" + refDocNo + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(transTypeEnum).ToString());
            }
            StringBuilder __queryTemp = new StringBuilder();
            __queryTemp.Append("select distinct " + _g.d.ap_ar_trans_detail._doc_no + "," + _g.d.ap_ar_trans_detail._trans_flag);
            __queryTemp.Append(" from (" + __query.ToString() + ") as temp1");
            return __myFrameWork._queryShort(__query.ToString()).Tables[0];
        }

        private void _addNode(ArrayList source, _docFlowObjectClass value)
        {
            if (value != null)
            {
                source.Add(value);
            }
        }

        //private _docFlowObjectClass _createCBTransDocFlow(_g.g._transControlTypeEnum transTypeEnum, string refDetalFieldName, string refFieldName, string docNo, string extraWhere, string guidParent)
        //{
        //    switch(transTypeEnum)
        //    {
                
        //    }
        //}

        /// <summary>
        /// สร้าง Flow จาก ictrans
        /// </summary>
        /// <param name="transTypeEnum"></param>
        /// <param name="refDetalFieldName"></param>
        /// <param name="refFieldName"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        private _docFlowObjectClass _createArApTransDocFlow(_g.g._transControlTypeEnum transTypeEnum, string refDetalFieldName, string refFieldName, string docNo, string extraWhere, string guidParent)
        {
            switch (transTypeEnum)
            {
                // ซื้อ,เจ้าหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    {
                        DataTable __docTransRef = this._selectApArTransRef(transTypeEnum, refDetalFieldName, "", docNo, extraWhere);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ap_ar_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createApArTransCurrentNode(transTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้, _g.d.ap_ar_trans_detail._doc_ref, "", __getDocNo, "", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    {
                        DataTable __docTransRef = this._selectApArTransRef(transTypeEnum, refDetalFieldName, "", docNo, extraWhere);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ap_ar_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createApArTransCurrentNode(transTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                // ขาย, ลูกหนี้
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    {
                        DataTable __docTransRef = this._selectApArTransRef(transTypeEnum, refDetalFieldName, "", docNo, extraWhere);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ap_ar_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createApArTransCurrentNode(transTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, _g.d.ap_ar_trans_detail._doc_ref, "", __getDocNo, "", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    {
                        DataTable __docTransRef = this._selectApArTransRef(transTypeEnum, refDetalFieldName, "", docNo, extraWhere);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ap_ar_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createApArTransCurrentNode(transTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
            }
            return null;
        }

        /// <summary>
        /// สร้าง Flow จาก ictrans
        /// </summary>
        /// <param name="icTransTypeEnum"></param>
        /// <param name="refDetalFieldName"></param>
        /// <param name="refFieldName"></param>
        /// <param name="docNo"></param>
        /// <returns></returns>
        private _docFlowObjectClass _createTransDocFlow(_g.g._transControlTypeEnum icTransTypeEnum, string refDetalFieldName, string refFieldName, string docNo, string guidParent)
        {
            switch (icTransTypeEnum)
            {
                #region สินค้า
                #endregion
                #region ซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);

                            // รายการตัดจ่าย


                            if (__getDoc != null)
                            {
                                //this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                        /*
                        DataTable __docTransRef = this._selectCBTransDetailRef(icTransTypeEnum, _g.d.cb_trans_detail._trans_number, "", docNo, "");
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();

                        // current
                        _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, docNo, "", guidParent);
                        __docFlow._docFlowDetailObject.Add(__getDoc);

                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            //string __transFlag = __docTransRef.Rows[__row][_g.d.ic_trans_detail._tra]

                            //_docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                            }
                            _docFlowDetailObjectClass __docDetail = new _docFlowDetailObjectClass();
                            __docDetail._refDocNo = docNo;
                            __docDetail._docNo = __getDocNo;
                            __docDetail._docDate = MyLib._myGlobal._convertDateFromQuery(__docTransRef.Rows[0][_g.d.cb_trans_detail._doc_date].ToString());
                            __docDetail._docTime = ""; // __data.Rows[0][_g.d.ic_trans._doc_time].ToString();
                            __docDetail._icTransType = MyLib._myGlobal._intPhase(__docTransRef.Rows[0][_g.d.cb_trans_detail._trans_flag].ToString()); //_g.g._transFlagGlobal._transFlag(icTransTypeEnum);
                            __docDetail._amount = MyLib._myGlobal._decimalPhase(__docTransRef.Rows[0][_g.d.cb_trans_detail._amount].ToString());
                            __docDetail._transMode = 1;
                            __docDetail._guidParent = guidParent;
                            __docDetail._guidNext = Guid.NewGuid().ToString();

                            __docFlow._docFlowDetailObject.Add(__docDetail);
                        }
                        return __docFlow;*/
                    }

             
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                //this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }

                #endregion
                #region ขาย
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                //this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                //this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก, _g.d.ic_trans._doc_ref, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                #endregion
                // สินค้า
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                // ซื้อ
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ, _g.d.ic_trans_detail._ref_doc_no, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ, _g.d.ic_trans_detail._ref_doc_no, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, "", __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, _g.d.ap_ar_trans_detail._doc_ref + "=\'\' or " + _g.d.ap_ar_trans_detail._doc_ref + " is null", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                // ขาย
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, _g.d.ic_trans._doc_ref, __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_สั่งขาย, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_สั่งขาย, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, refFieldName, docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, "", __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, _g.d.ap_ar_trans_detail._doc_ref + "=\'\' or " + _g.d.ap_ar_trans_detail._doc_ref + " is null", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, "", __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, _g.d.ap_ar_trans_detail._doc_ref + "=\'\' or " + _g.d.ap_ar_trans_detail._doc_ref + " is null", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                this._addNode(__getDoc._nodes, this._createTransDocFlow(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก, _g.d.ic_trans_detail._ref_doc_no, "", __getDocNo, __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, "", __getDoc._guidNext));
                                this._addNode(__getDoc._nodes, this._createArApTransDocFlow(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้, _g.d.ap_ar_trans_detail._billing_no, "", __getDocNo, _g.d.ap_ar_trans_detail._doc_ref + "=\'\' or " + _g.d.ap_ar_trans_detail._doc_ref + " is null", __getDoc._guidNext));
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก:
                    {
                        DataTable __docTransRef = this._selectTransDetailRef(icTransTypeEnum, refDetalFieldName, "", docNo);
                        _docFlowObjectClass __docFlow = new _docFlowObjectClass();
                        for (int __row = 0; __row < __docTransRef.Rows.Count; __row++)
                        {
                            string __getDocNo = __docTransRef.Rows[__row][_g.d.ic_trans_detail._doc_no].ToString();
                            _docFlowDetailObjectClass __getDoc = this._createTransCurrentNode(icTransTypeEnum, __getDocNo, docNo, guidParent);
                            if (__getDoc != null)
                            {
                                __docFlow._docFlowDetailObject.Add(__getDoc);
                            }
                        }
                        return __docFlow;
                    }
            }
            return null;
        }

        public _docFlowForm(_g.g._transControlTypeEnum apArTransControlTypeEnum, string docNo)
        {
            InitializeComponent();
            // Create Object
        }
    }

    public class _docFlowUserControl : _docFlowStyle
    {
        public string __docNo = "";
        int __row = 0;
        string __formatNumberAmount = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
        public string _guidParent;
        public string _guidNext;

        public _docFlowUserControl(string name, _docFlowDetailObjectClass data)
        {
            this.GroupTitle = name;
            this.Width = 300;
            this.Height = 150;
            this._guidParent = data._guidParent;
            this._guidNext = data._guidNext;
            this._createLabel("เลขที่เอกสาร", data._docNo);
            this._createLabel("วันที่-เวลา", MyLib._myGlobal._convertDateToString(data._docDate, true) + "-" + data._docTime);
            this._createLabel("มูลค่าเอกสาร", String.Format(__formatNumberAmount, data._amount));
            this._createLabel("เอกสารต้นทาง", data._refDocNo);
        }

        private void _createLabel(string labelName, string value)
        {
            MyLib._myLabel __label = new MyLib._myLabel();
            __label.Text = labelName;
            __label.Location = new Point(5, (this.__row * 15) + 30);
            __label.AutoSize = true;
            this.Controls.Add(__label);
            // value
            MyLib._myLabel __labelValue = new MyLib._myLabel();
            __labelValue.Text = value;
            __labelValue.Font = new Font(__labelValue.Font, FontStyle.Bold);
            __labelValue.Location = new Point(80, (this.__row * 15) + 30);
            __labelValue.AutoSize = true;
            this.Controls.Add(__labelValue);
            this.__row++;
        }
    }

    public class _columnObjectClass
    {
        public ArrayList _row = new ArrayList();
        public _columnObjectClass()
        {
            for (int __row = 0; __row < 100; __row++)
            {
                this._row.Add(new _gridObjectClass());
            }
        }
        public int __rowCount = 0;
    }

    public class _gridObjectClass
    {
        public _docFlowDetailObjectClass _cells = null;
    }

    public class _docFlowDetailObjectClass
    {
        public int _transMode;
        public int _icTransType;
        public string _docNo;
        public string _refDocNo;
        public DateTime _docDate;
        public string _docTime;
        public decimal _amount;
        public string _user;
        public string _guidParent;
        public string _guidNext;
        public ArrayList _nodes = new ArrayList();
    }

    public class _docFlowObjectClass
    {
        public ArrayList _docFlowDetailObject = new ArrayList();
    }

    public class _followListClass
    {
        public string _transFlag;
        public string _condition;
    }
}
