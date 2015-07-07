using System;
using System.Net;
using System.Text;
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
        private readonly KatoRenderer _renderer;

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
        /// Constructor for a new Kato Sink
        /// </summary>
        /// <param name="roomId">The Id that is obtained from the room you want the messages to show in.</param>
        /// <param name="name">The name that is used in the room.</param>
        public KatoSink(string roomId, string name)
            : this(roomId, name, KatoRenderer.Default)
        {
        }

        /// <summary>
        /// Constructor for a new Kato Sink.
        /// </summary>
        /// <param name="roomId">The Id that is obtained from the room you want the messages to show in.</param>
        /// <param name="name">The name that is used in the room.</param>
        /// <param name="renderer">The renderer that should be used for the message.</param>
        public KatoSink(string roomId, string name, KatoRenderer renderer)
        {
            _roomId = roomId;
            _name = name;
            _renderer = renderer;

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
                Renderer = _renderer,
                Color = GetColor(logEvent.Level),
                Text = logEvent.RenderMessage(),
            };

            SendRequest(message);
        }

        /// <summary>
        /// POST a message to the Kato room.
        /// </summary>
        /// <param name="body"></param>
        private void SendRequest(KatoMessage message)
        {
            var body = JsonConvert.SerializeObject(message, _settings);

            var request = WebRequest.Create(RoomUrl);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var stream = request.GetRequestStream())
            {
                var bytes = Encoding.UTF8.GetBytes(body);
                stream.Write(bytes, 0, bytes.Length);
            }

            var response =  request.GetResponse();
            // TODO: Handle response

            return;
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
