using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serilog.Sinks.Kato.Tests
{
    [TestClass]
    public class KatoTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRoomIdThrowsException()
        {
            new KatoSink("Invalid  room Id", "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyRoomIdThrowsException()
        {
            new KatoSink("", "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyNameThrowsException()
        {
            new KatoSink("1", "");
        }
    }
}
