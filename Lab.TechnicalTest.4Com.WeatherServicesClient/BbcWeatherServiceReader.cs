using System;
using System.Threading.Tasks;
using System.Net.Http;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using Lab.TechnicalTest._4Com.WeatherTest.Utilities.Extensions.DataTypes;
using System.Threading;

namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public class BbcWeatherServiceReader : IWeatherServiceReader
    {
        private string _bbcweatherserviceUrl;

        public BbcWeatherServiceReader(string bbcweatherserviceUrl)
        {
            _bbcweatherserviceUrl = bbcweatherserviceUrl;
        }

        public async Task<WeatherResultInCelsiusAndKph> WeatherReaderAsync(HttpClient httpClient)
        {
            try
            {
                var requestUri = new Uri(_bbcweatherserviceUrl);
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var response = await httpClient.SendAsync(request, new CancellationTokenSource(TimeSpan.FromMilliseconds(30000)).Token);
                response.EnsureSuccessStatusCode();
                BbcWeatherResult bbcWeatherResult = await response.Content.ReadAsAsync<BbcWeatherResult>();

                return new WeatherResultInCelsiusAndKph
                {
                    Temperature = bbcWeatherResult.TemperatureFahrenheit.ConvertFahrenheitToCelsius(),
                    WindSpeed = bbcWeatherResult.WindSpeedMph.ConvertMphToKph()
                };
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("Bbc Weather API:: Failed to get Weather results", exception);
            }
        }
    }
}
