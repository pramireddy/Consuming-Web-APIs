/// <reference path="angular.min.js" />
(function () {
    var weatherService = function ($http) {
        var getWeatherResult = function (location, temperatureUnit, windSpeedUnit) {
            var url = "http://localhost:63388/api/weather?location=" + location + "&temperatureUnit=" + temperatureUnit + "&windSpeedUnit=" + windSpeedUnit;
            return $http.get(url).then(function (response) {
                return response.data;
            });
        };
        return {
            getWeatherResult: getWeatherResult
        };
    };
    var weatherModule = angular.module("weatherModule");
    weatherModule.factory("weatherService", weatherService);
})();