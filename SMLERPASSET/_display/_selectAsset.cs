using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPASSET._display
{
    public class _selectAsset : MyLib._myScreen
    {
        _selectAssetType _screenType = _selectAssetType._depreciateByDay;

        public _selectAsset()
        {
            this.AutoSize = true;
            _createScreen();
        }

        void _createScreen()
        {
            if (_screenType == _selectAssetType._depreciateByDay)
            {
                this._maxColumn = 4;
                this._addComboBox(0, 0, "as_resource.process_mode", true, new string[]{"as_resource.by_revenue", "as_resource.by_account", "as_resource.by_total_date"}, false);
                this._addTextBox(0, 1, 1, 0, "as_resource.period", 1, 10, 1, true, false, true);
                this._addDateBox(0, 2, 1, 0, "as_resource.date_begin", 1, true, false);
                this._addDateBox(0, 3, 1, 0, "as_resource.date_end", 1, true, false);
            }
            else if (_screenType == _selectAssetType._depreciateByMonth)
            {
                this._maxColumn = 4;
                this._addComboBox(0, 0, "as_resource.process_mode", true, new string[] {"as_resource.by_revenue", "as_resource.by_account", "as_resource.by_total_date"}, false);
                this._addTextBox(0, 1, 1, 0, "as_resource.period", 1, 10, 1, true, false, true);
                this._addDateBox(0, 2, 1, 0, "as_resource.date_begin", 1, true, false);
                this._addDateBox(0, 3, 1, 0, "as_resource.date_end", 1, true, false);
            }
            else if (_screenType == _selectAssetType._depreciateByYear)
            {
                this._maxColumn = 4;
                this._addComboBox(0, 0, "as_resource.process_mode", true, new string[] { "as_resource.by_revenue", "as_resource.by_account", "as_resource.by_total_date" }, false);
                this._addTextBox(0, 1, 1, 0, "as_resource.period", 1, 10, 1, true, false, true);
                this._addDateBox(0, 2, 1, 0, "as_resource.date_begin", 1, true, false);
                this._addDateBox(0, 3, 1, 0, "as_resource.date_end", 1, true, false);
            }
            this.Invalidate();
        }

        public _selectAssetType ScreenType
        {
            get
            {
                return _screenType;
            }
            set
            {
                _screenType = value;
                this.SuspendLayout();
                this.Controls.Clear();
                _createScreen();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }

    }
    public enum _selectAssetType
    {
        _depreciateByDay,
        _depreciateByMonth,
        _depreciateByYear
    }

}
