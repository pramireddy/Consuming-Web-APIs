using System;

namespace Lab.TechnicalTest._4Com.WeatherTest.Utilities.Extensions.DataTypes
{
    public static class DoubleExtensions
    {
        public static double ConvertCelsiusToFahrenheit(this double temperatureInCelsius)
        {
            return (temperatureInCelsius * 9 / 5) + 32;
        }

        public static double ConvertFahrenheitToCelsius(this double temperatureInFahrenheit)
        {
            return (temperatureInFahrenheit - 32d) * (5d / 9d);
        }

        public static double ConvertKphToMph(this double windSpeedInKph)
        {
            return Math.Round(windSpeedInKph * 0.621371192d, 1);
        }

        public static double ConvertMphToKph(this double windSpeedInMph)
        {
            return Math.Round(windSpeedInMph * 1.609344d, 1);
        }

    }
}
