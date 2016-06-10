using Lab.TechnicalTest._4Com.WeatherServicesClient;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Lab.TechnicalTest._4Com.WeatherTest.DataContracts;
using System.Net;

namespace Lab.TechnicalTest._4Com.WeatherServicesClientTest
{
    [TestFixture]
    public class AccuWeatherServiceReaderTest
    {
        [Test]
        public void WeatherReaderAsync_Verify_Request()
        {
            //arrange 
            IWeatherServiceReader accuWeatherServiceReader = new AccuWeatherServiceReader("http://accuweather:60827/Weather/london");

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new WeatherResultContent(new
                {
                    Temperature = 22,
                    WindSpeed = 17
                }),
                StatusCode = HttpStatusCode.OK
            };

            // assert
            var testHttpMessageHandler = new WeatherTestHttpMessageHandler
                (request =>
                {
                    Assert.AreEqual("http://accuweather:60827/Weather/london", request.RequestUri.ToString());
                    Assert.AreEqual(HttpMethod.Get, request.Method);
                }, httpResponseMessage);

            HttpClient httpClient = new HttpClient(testHttpMessageHandler);

            // act
            WeatherResultInCelsiusAndKph weatherResult = null;
            Task.Run(async () =>
            {
                weatherResult = await accuWeatherServiceReader.WeatherReaderAsync(httpClient);
            }).Wait();
        }

        [Test]
        public void WeatherReaderAsync_Fails()
        {
            //arrange 
            IWeatherServiceReader accuWeatherServiceReader = new AccuWeatherServiceReader("http://accuweather:60827/Weather/london");

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            // assert
            var testHttpMessageHandler = new WeatherTestHttpMessageHandler
                (request =>
                {
                    Assert.AreEqual("http://accuweather:60827/Weather/london", request.RequestUri.ToString());
                    Assert.AreEqual(HttpMethod.Get, request.Method);
                }, httpResponseMessage);

            HttpClient httpClient = new HttpClient(testHttpMessageHandler);

           
            //act and assert
            Assert.That(async () =>
            {
                await accuWeatherServiceReader.WeatherReaderAsync(httpClient);
            }, Throws.TypeOf<HttpRequestException>());
        }

        [Test]
        public void WeatherReaderAsync_Success()
        {
            //arrange 
            IWeatherServiceReader accuWeatherServiceReader = new AccuWeatherServiceReader("http://accuweather:60827/Weather/london");

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new WeatherResultContent(new
                {
                    TemperatureCelsius = 10,
                    WindSpeedKph= 8
                }),
                StatusCode = HttpStatusCode.OK
            };

            var testHttpMessageHandler = new WeatherTestHttpMessageHandler
                (request =>
                {
                    Assert.AreEqual("http://accuweather:60827/Weather/london", request.RequestUri.ToString());
                    Assert.AreEqual(HttpMethod.Get, request.Method);
                }, httpResponseMessage);

            HttpClient httpClient = new HttpClient(testHttpMessageHandler);

            // act
            WeatherResultInCelsiusAndKph weatherResult= null;
            Task.Run(async () =>
            {
                weatherResult = await accuWeatherServiceReader.WeatherReaderAsync(httpClient);
            }).Wait();

            // assert
            Assert.IsTrue(weatherResult.Temperature == 10 && weatherResult.WindSpeed == 8 );
        }

        [Test]
        public void WeatherReaderAsync_Fails_ExceptionThrown_When_Pass_InvaidUrl()
        {
            //arrange
            IWeatherServiceReader accuWeatherServiceReader = new AccuWeatherServiceReader("InvalidUrl");
            HttpClient httpClient = new HttpClient();

            //assert
            Assert.That(async () =>
            {
                await accuWeatherServiceReader.WeatherReaderAsync(httpClient);
            },Throws.TypeOf<HttpRequestException>());

        }

    }
}
