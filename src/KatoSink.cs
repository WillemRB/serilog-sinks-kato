using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Kato
{
    public class KatoSink : ILogEventSink
    {
        private readonly string _roomId;

        private readonly string _name;

        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// The URL to which the message will be POSTed.
        /// </summary>
        private string RoomUrl
        {
            get
            {
                return String.Format(@"https://api.kato.im/rooms/{0}/simple", _roomId);
            }
        }

        /// <summary>
        /// Constructor for a new Kato Sink.
        /// </summary>
        /// <param name="roomId">The Id that is obtained from the room you want the messages to show in.</param>
        /// <param name="name">The name that is used in the room.</param>
        public KatoSink(string roomId, string name)
        {
            _roomId = roomId;
            _name = name;

            _settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
        }

        public void Emit(LogEvent logEvent)
        {
            var message = new KatoMessage()
            {
                From = _name,
                Renderer = KatoRenderer.Default,
                Color = GetColor(logEvent.Level),
                Text = logEvent.RenderMessage(),
            };

            var body = JsonConvert.SerializeObject(message, _settings);

            SendRequest(body);
        }

        /// <summary>
        /// POST a message to the Kato room.
        /// </summary>
        /// <param name="body"></param>
        private void SendRequest(string body)
        {
            var request = WebRequest.Create(RoomUrl);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var stream = request.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                stream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
        }

        /// <summary>
        /// Map the LogEventLevel to a HTML color.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private string GetColor(LogEventLevel level)
        {
            switch (level)
            {
                case LogEventLevel.Fatal:
                    return "darkred";
                case LogEventLevel.Error:
                    return "red";
                case LogEventLevel.Warning:
                    return "darkorange";
                case LogEventLevel.Information:
                    return "lightskyblue";
                case LogEventLevel.Debug:
                    return "seashell";
                case LogEventLevel.Verbose:
                    return "snow";
                default:
                    return "slateblue";
            }
        }
    }
}
