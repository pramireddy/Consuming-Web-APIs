using Lab.TechnicalTest._4Com.WeatherServicesClient;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using Lab.TechnicalTest._4Com.WeatherTest.Utilities.Enumerations;
using Lab.TechnicalTest._4Com.WeatherTest.Utilities.Extensions.DataTypes;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lab.TechnicalTest._4Com.WeatherTest.App.Controllers
{
    public class WeatherController : ApiController
    {
        private IWeatherServiceClient _weatherServiceClient;


        ///// <summary>
        ///// TO DO: DI
        ///// </summary>
        ///// <param name="weatherServiceClient"></param>
        //public WeatherController(IWeatherServiceClient weatherServiceClient)
        //{
        //    _weatherServiceClient = weatherServiceClient;
        //}


        public WeatherController()
        {
            IWeatherServiceReadersFactory weatherServiceReadesrFactory = new WeatherServiceReadersFactory();
            _weatherServiceClient = new WeatherServiceClient(weatherServiceReadesrFactory);
        }

        [HttpGet]
        public async Task<WeatherResultInCelsiusAndKph> Get(string location, string temperatureUnit, string windSpeedUnit)
        {
            TemperatureUnits inputTemperatureUnit;
            WindSpeedUnits inputWindSpeedUnit;

            if (!Enum.TryParse(temperatureUnit, out inputTemperatureUnit) ||
                !Enum.TryParse(windSpeedUnit, out inputWindSpeedUnit))
                return null;

            WeatherResultInCelsiusAndKph weatherResultInCelsiusAndKph = 
                            await _weatherServiceClient.GetWeatherResultAsync(new HttpClient(),location);

            return new WeatherResultInCelsiusAndKph
            {
                // TO DO: AutoMapper
                Temperature = TemperatureUnits.Celsius == inputTemperatureUnit ?
                              weatherResultInCelsiusAndKph.Temperature : weatherResultInCelsiusAndKph.Temperature.ConvertCelsiusToFahrenheit(),
                WindSpeed = WindSpeedUnits.Kph == inputWindSpeedUnit ?
                            weatherResultInCelsiusAndKph.WindSpeed : weatherResultInCelsiusAndKph.WindSpeed.ConvertKphToMph()

            };
        }
    }
}
