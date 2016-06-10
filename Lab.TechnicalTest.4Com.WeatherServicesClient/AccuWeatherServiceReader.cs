using System;
using System.Threading.Tasks;
using System.Net.Http;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using System.Threading;

namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public class AccuWeatherServiceReader : IWeatherServiceReader
    {
        private string _accweatherserviceUrl;

        public AccuWeatherServiceReader(string bbcweatherserviceUrl)
        {
            _accweatherserviceUrl = bbcweatherserviceUrl;
        }

        public async Task<WeatherResultInCelsiusAndKph> WeatherReaderAsync(HttpClient httpClient)
        {
            try
            {
                var requestUri = new Uri(_accweatherserviceUrl);
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await httpClient.SendAsync(request, new CancellationTokenSource(TimeSpan.FromMilliseconds(30000)).Token);
                response.EnsureSuccessStatusCode();
                AccuWeatherTestResult accuWeatherTestResult = await response.Content.ReadAsAsync<AccuWeatherTestResult>();
                return new WeatherResultInCelsiusAndKph
                {
                    Temperature = accuWeatherTestResult.TemperatureCelsius,
                    WindSpeed = accuWeatherTestResult.WindSpeedKph
                };
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("Accu Weather API:: Failed to get Weather results ", exception);
            }
        }
    }
}
