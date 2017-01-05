using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPPControl
{
    public class _shipmentScreenMore : MyLib._myScreen
    {


        private SMLPPGlobal.g._ppControlTypeEnum _transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;

        public SMLPPGlobal.g._ppControlTypeEnum transControlType
        {
            get
            {
                return this._transControlType;
            }
            set
            {
                this._transControlType = value;
                this.build();
            }
        }

        void build()
        {
            if (this.transControlType == SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
            {
                return;
            }

            this.SuspendLayout();

            // this._textBoxSearch += _shipmentScreenTop__textBoxSearch;
            //this._textBoxChanged += _shipmentScreenTop__textBoxChanged;

            int __row = 0;
            this._table_name = _g.d.pp_shipment._table;
            this._maxColumn = 2;

            {
                if (this.transControlType != SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
                {
                    switch (this.transControlType)
                    {
                       
                        case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                            {
                                this._table_name = _g.d.ic_trans._table;

                                this._maxColumn = 2;

                                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_2, 2, 1, 0, true, false, true);
                                __row += 2;
                                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_3, 2, 1, 0, true, false, true);

                                __row += 2;
                                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_4, 2, 1, 0, true, false, true);

                                __row += 2;
                                this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark_5, 2, 1, 0, true, false, true);

                            }
                            break;
                    }
                }
            }

            this.Invalidate();
            this.ResumeLayout();
        }


    }
}
