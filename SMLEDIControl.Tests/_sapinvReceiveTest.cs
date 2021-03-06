// <copyright file="_sapinvReceiveTest.cs">Copyright ©  2015</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMLEDIControl;

namespace SMLEDIControl.Tests
{
    /// <summary>This class contains parameterized unit tests for _sapinvReceive</summary>
    [PexClass(typeof(_sapinvReceive))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class _sapinvReceiveTest
    {

    }
}
