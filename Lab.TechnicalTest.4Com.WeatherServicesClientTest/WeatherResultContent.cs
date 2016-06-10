using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lab.TechnicalTest._4Com.WeatherServicesClientTest
{
    public class WeatherResultContent : HttpContent
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public WeatherResultContent(object value)
        {

            Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jw = new JsonTextWriter(new StreamWriter(_stream)) { Formatting = Formatting.Indented };
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            _stream.Position = 0;

        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return _stream.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _stream.Length;
            return true;
        }
    }
}
