namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public class WeatherServiceReadersFactory : IWeatherServiceReadersFactory
    {
        IWeatherServiceReader IWeatherServiceReadersFactory.CreateAccuWeatherServiceReader(string accweatherserviceUrl)
        {
            return new AccuWeatherServiceReader(accweatherserviceUrl);
        }

        IWeatherServiceReader IWeatherServiceReadersFactory.CreateBbcWeatherServiceReader(string bbcweatherserviceUrl)
        {
            return new BbcWeatherServiceReader(bbcweatherserviceUrl);
        }
    }
}
