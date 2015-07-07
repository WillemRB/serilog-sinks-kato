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
        public string From { get; set; }

        /// <summary>
        /// Any HTML color
        /// </summary>
        public string Color { get; set; }

        public KatoRenderer Renderer { get; set; }

        public string Text { get; set; }
    }
}
