using System;
using System.Collections.Generic;
using System.Text;

namespace MyLib
{
    public class _CalculatorFormatEventArgs : EventArgs
    {
        #region State

        private double _m_Result = 0.0;
        private string _m_FormattedResult = String.Empty;

        #endregion //--State


        #region Construction

        /// <summary>
        /// Constructs a new CalculatorFormatEventArgs with the result of
        /// the calculation.
        /// </summary>
        /// <param name="result"></param>
        public _CalculatorFormatEventArgs(double result)
        {
            _m_Result = result;
            _m_FormattedResult = _m_Result.ToString();
        }
        #endregion //--Construction


        #region Public Interface

        /// <summary>
        /// Gets the result of the calculation.
        /// </summary>
        public double _Result
        {
            get { return _m_Result; }
        }

        /// <summary>
        /// Gets or sets the formatted result.
        /// </summary>
        public string _FormattedResult
        {
            get { return _m_FormattedResult; }
            set { _m_FormattedResult = value; }
        }
        #endregion //--Public Interface
    }
}
