using System.Net.Http;
using System.Threading.Tasks;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;

namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public interface IWeatherServiceReader
    {
        Task<WeatherResultInCelsiusAndKph> WeatherReaderAsync(HttpClient httpClient);
    }
}
