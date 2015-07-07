using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serilog.Sinks.Kato.Tests
{
    [TestClass]
    public class KatoMarkdownTests
    {
        ILogger _logger;

        [TestInitialize]
        public void Initialize()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new KatoSink(Config.roomId, "Markdown Test", KatoRenderer.Markdown))
                .CreateLogger();
        }

        [TestMethod]
        public void LogLevelTests()
        {
            _logger.Fatal("# Fatal");
            _logger.Error("## Error");
            _logger.Warning("### Warning");
            _logger.Information("Information");
            _logger.Debug("**Debug**");
            _logger.Verbose("~~Verbose~~");
        }
    }
}
