using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Kato
{
    public class KatoMarkdownSink : AbstractKatoSink, ILogEventSink
    {
        public KatoMarkdownSink(string roomId, string name)
            : base(roomId, name)
        {
        }

        public void Emit(LogEvent logEvent)
        {
            var message = new KatoMessage()
            {
                From = Name,
                Renderer = KatoRenderer.Markdown,
                Color = GetColor(logEvent.Level),
                Text = logEvent.RenderMessage(),
            };

            SendRequest(message);
        }
    }
}
