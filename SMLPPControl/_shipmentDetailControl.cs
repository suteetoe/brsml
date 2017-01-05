using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPPControl
{
    public partial class _shipmentDetailControl : UserControl
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

        public _shipmentDetailControl()
        {
            InitializeComponent();
        }

        void build()
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                    this._screenButtom.Visible = true;
                    break;
            }

            this._screenTop.transControlType = this.transControlType;
            this._shipmentGrid.transControlType = this.transControlType;
            this._screenButtom.transControlType = this.transControlType;

        }
    }
}
