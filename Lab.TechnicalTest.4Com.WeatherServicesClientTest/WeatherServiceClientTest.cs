using Lab.TechnicalTest._4Com.WeatherServicesClient;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using Moq;

namespace Lab.TechnicalTest._4Com.WeatherServicesClientTest
{
    [TestFixture]
    public class WeatherServiceClientTest
    {
        private string accweatherserviceUrl = "http://localhost:60827/Weather/london";
        private string bbcweatherserviceUrl = "http://localhost:60819/Weather/london";

        [Test]
        public void GetWeatherResultAsync_Success()
        {

            // arrange
            Mock<IWeatherServiceReadersFactory> weatherServiceReadersFactory = new Mock<IWeatherServiceReadersFactory>();
            Mock<IWeatherServiceReader> bbccWeatherServiceReader = new Mock<IWeatherServiceReader>();

            bbccWeatherServiceReader.Setup(x => x.WeatherReaderAsync(It.IsAny<HttpClient>())).Returns(async () => 
            {
                await Task.Yield();
                return new WeatherResultInCelsiusAndKph
                {
                    Temperature = 20, 
                    WindSpeed = 16
                };
               
            });

            Mock<IWeatherServiceReader> accuWeatherServiceReader = new Mock<IWeatherServiceReader>();

            accuWeatherServiceReader.Setup(x => x.WeatherReaderAsync(It.IsAny<HttpClient>())).Returns(async () =>
            {
                await Task.Yield();
                return new WeatherResultInCelsiusAndKph
                {
                    Temperature = 10,
                    WindSpeed = 8
                };
            });

            weatherServiceReadersFactory.Setup(x => x.CreateBbcWeatherServiceReader(bbcweatherserviceUrl)).Returns(bbccWeatherServiceReader.Object);

            weatherServiceReadersFactory.Setup(x => x.CreateAccuWeatherServiceReader(accweatherserviceUrl)).Returns(accuWeatherServiceReader.Object);

            // act
            IWeatherServiceClient weatherServiceClient = new WeatherServiceClient(weatherServiceReadersFactory.Object);
            WeatherResultInCelsiusAndKph weatherResult = null;
            Task.Run(async () =>
            {
                weatherResult = await weatherServiceClient.GetWeatherResultAsync(new HttpClient(), "london");
            }).Wait();

            // assert
            Assert.IsTrue(weatherResult.Temperature == 15 && weatherResult.WindSpeed == 12);
        }
    }
}
