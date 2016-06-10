using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.TechnicalTest._4Com.WeatherServicesClientTest
{
    public class WeatherTestHttpMessageHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage> _testingHttpRequestAction;
        private readonly HttpResponseMessage _httpResponseMessage;

        public WeatherTestHttpMessageHandler(Action<HttpRequestMessage> testingHttpRequestAction, HttpResponseMessage httpResponseMessage)
        {
            _testingHttpRequestAction = testingHttpRequestAction;
            _httpResponseMessage = httpResponseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _testingHttpRequestAction(request);

            var responseTask = new TaskCompletionSource<HttpResponseMessage>();

            responseTask.SetResult(_httpResponseMessage);
            return responseTask.Task;

        }
    }
}
