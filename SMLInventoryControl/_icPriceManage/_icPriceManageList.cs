using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace SMLInventoryControl._icPriceManage
{
    public partial class _icPriceManageList : UserControl
    {
        _getICForm _getICForm = new _getICForm();
        _icPriceAdjustForm _adjustForm = new _icPriceAdjustForm();
        _icListForm _icList = new _icListForm();
        _icPriceListForm _priceList = new _icPriceListForm();
        
        

        public _icPriceManageList()
        {
            InitializeComponent();

            DockableFormInfo __icDock = this._dock.Add(_getICForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            DockableFormInfo __adjustDock = this._dock.Add(_adjustForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            DockableFormInfo __icListDock = this._dock.Add(_icList, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            DockableFormInfo __priceDock = this._dock.Add(_priceList, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());


            this._dock.DockForm(__icDock, DockStyle.Top, zDockMode.Outer);
            this._dock.DockForm(__adjustDock, __icDock, DockStyle.Right, zDockMode.Inner);
            this._dock.DockForm(__icListDock, DockStyle.Left, zDockMode.Inner);
            this._dock.DockForm(__priceDock, DockStyle.Fill, zDockMode.Inner);

        }
    }
}
