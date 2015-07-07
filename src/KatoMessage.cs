using Newtonsoft.Json;
using Serilog.Sinks.Kato.Utils;

namespace Serilog.Sinks.Kato
{
    [JsonConverter(typeof(JsonToStringConverter))]
    public class KatoRenderer
    {
        private string _name;

        private KatoRenderer(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public static KatoRenderer Default { get { return new KatoRenderer("default"); } }

        public static KatoRenderer Code { get { return new KatoRenderer("code"); } }

        public static KatoRenderer Markdown { get { return new KatoRenderer("markdown"); } }
    }

    public class KatoMessage
    {
        /// <summary>
        /// The name that is used in the Kato room.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Any HTML color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// The name of the renderer used for the message.
        /// </summary>
        public KatoRenderer Renderer { get; set; }

        /// <summary>
        /// The message to be posted in the room.
        /// </summary>
        public string Text { get; set; }
    }
}
