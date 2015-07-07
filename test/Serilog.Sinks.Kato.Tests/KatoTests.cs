﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Sinks.Kato;

namespace Serilog.Sinks.Kato.Tests
{
    [TestClass]
    public class KatoTests
    {
        private string _roomId = "";

        private ILogger _logger;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new KatoSink(_roomId, "UnitTest"))
                .CreateLogger();
        }

        [TestMethod]
        public void LogLevelTests()
        {
            _logger.Fatal("Fatal");
            _logger.Error("Error");
            _logger.Warning("Warning");
            _logger.Information("Information");
            _logger.Debug("Debug");
            _logger.Verbose("Verbose");
        }

        [TestMethod]
        public void VariableTest()
        {
            var v1 = "First Value";
            var v2 = "Second Value";

            _logger.Information("1: {v1}, 2: {v2}", v1, v2);
        }
    }
}
