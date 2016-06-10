using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public class WeatherServiceClient : IWeatherServiceClient
    {
        private IList<IWeatherServiceReader> _weatherServiceReaders;
        private IWeatherServiceReadersFactory _weatherServiceReadersFactory;
        private readonly NameValueCollection _appSettings;

        public WeatherServiceClient(IWeatherServiceReadersFactory weatherServiceReaderFactory)
        {
             _weatherServiceReadersFactory = weatherServiceReaderFactory;
            ConfigurationManager.RefreshSection("appSettings");
            _appSettings = ConfigurationManager.AppSettings;
        }

        public async Task<WeatherResultInCelsiusAndKph> GetWeatherResultAsync(HttpClient httpClient,string location)
        {
            IList<Task<WeatherResultInCelsiusAndKph>> weatherResults = new List<Task<WeatherResultInCelsiusAndKph>>();
            _weatherServiceReaders = CreateWeatherServiceReaders(location);

            foreach(IWeatherServiceReader weatherReader in _weatherServiceReaders)
            {
                weatherResults.Add(weatherReader.WeatherReaderAsync(httpClient));
            }
            try
            {
                await Task.WhenAll(weatherResults);
            }
            catch(Exception exception)
            {
                throw new HttpRequestException("Failed to get Weather results", exception);
            }

            return new WeatherResultInCelsiusAndKph
            {
                Temperature = Math.Round(weatherResults.Select(x => x.Result.Temperature).Average()),
                WindSpeed = Math.Round(weatherResults.Select(x => x.Result.WindSpeed).Average(), 1)
            };
        }

        private IList<IWeatherServiceReader> CreateWeatherServiceReaders(string location)
        {
            return new List<IWeatherServiceReader>
            {
                _weatherServiceReadersFactory.CreateAccuWeatherServiceReader(_appSettings["accweatherserviceUrl"] + location),
                _weatherServiceReadersFactory.CreateBbcWeatherServiceReader(_appSettings["bbcweatherserviceUrl"] + location)
            };
        }
    }
}
