using System;
using System.Collections.Generic;
using System.Text;

namespace MyLib
{
    public class _CalculatorParseEventArgs : EventArgs
    {
        #region State

        private string _m_Original = String.Empty;
        private string _m_Parsed = String.Empty;

        #endregion //--State


        #region Construction

        /// <summary>
        /// Constructs a new CalculatorParseEventArgs with the given string.
        /// </summary>
        /// <param name="original"></param>
        public _CalculatorParseEventArgs(string original)
        {
            _m_Original = null == original ? String.Empty : original;
            _m_Parsed = _m_Original;
        }
        #endregion //--Construction


        #region Public Interface

        /// <summary>
        /// Gets the original string value.
        /// </summary>
        public string _Original
        {
            get { return _m_Original; }
        }

        /// <summary>
        /// Gets or sets the parsed string value.
        /// </summary>
        public string _Parsed
        {
            get { return _m_Parsed; }
            set { _m_Parsed = value; }
        }

        /// <summary>
        /// Gets the double of the parsed string. Will return 0.0
        /// if the value cannot be parsed to a double.
        /// </summary>
        /// <returns></returns>
        public double _GetResult()
        {
            double d = 0.0;
            Double.TryParse(_m_Parsed, out d);

            return d;
        }
        #endregion //--Public Interface
    }
}
