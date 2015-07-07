using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Serilog.Sinks.Kato
{
    public enum KatoRenderer
    {
        Default,
        Code,
        Markdown,
    }

    internal class KatoMessage
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
