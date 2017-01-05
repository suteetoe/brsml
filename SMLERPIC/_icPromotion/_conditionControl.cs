using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC._icPromotion
{
    public partial class _conditionControl : UserControl
    {
        public _gridItemClass _gridItem = new _gridItemClass();
        public _gridPriceAndDiscountClass _gridPriceAndDiscount = new _gridPriceAndDiscountClass();
        public _gridGroupClass _gridGroup = new _gridGroupClass();

        public _conditionControl()
        {
            InitializeComponent();
            this._gridItem.Dock = DockStyle.Fill;
            this._panelCondition.Controls.Add(this._gridItem);
            this._gridPriceAndDiscount.Dock = DockStyle.Fill;
            //
            this._gridGroup.Dock = DockStyle.Fill;
            this._panelGroup.Controls.Add(this._gridGroup);
        }

        public void _build(_promotionCaseEnum promotionCase)
        {
            this._gridItem._build(promotionCase);
            this._gridGroup._build(promotionCase);
            this._gridItem.Invalidate();
        }

        private void _priceAndDiscountRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this._panelWork.Controls.Clear();
            this._panelWork.Controls.Add(this._gridPriceAndDiscount);
        }
    }
}
