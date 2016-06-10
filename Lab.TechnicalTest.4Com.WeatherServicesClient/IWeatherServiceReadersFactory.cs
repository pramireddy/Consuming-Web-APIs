namespace Lab.TechnicalTest._4Com.WeatherServicesClient
{
    public interface IWeatherServiceReadersFactory
    {
        IWeatherServiceReader CreateBbcWeatherServiceReader(string bbcweatherserviceUrl);
        IWeatherServiceReader CreateAccuWeatherServiceReader(string accweatherserviceUrl);
    }
}
