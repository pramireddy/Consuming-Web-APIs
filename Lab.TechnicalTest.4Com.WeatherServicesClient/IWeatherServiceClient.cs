using System.Net.Http;
using System.Threading.Tasks;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;

namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public interface IWeatherServiceClient
    {
        Task<WeatherResultInCelsiusAndKph> GetWeatherResultAsync(HttpClient httpClient, string location);
    }
}