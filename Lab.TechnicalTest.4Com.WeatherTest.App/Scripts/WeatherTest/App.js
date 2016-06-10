/// <reference path="angular.min.js" />
(function () {
    var weatherModule = angular.module("weatherModule", []);
    weatherModule.controller("weatherController", function ($scope, weatherService) {
        $scope.location = "London";
        $scope.temperatureUnits = ["Celsius", "Fahrenheit"];
        $scope.windSpeedUnits = ["Kph", "Mph"];
        $scope.temperatureUnit = $scope.temperatureUnits[0];
        $scope.windSpeedUnit = $scope.windSpeedUnits[0];
        $scope.vmweatherResult;

        $scope.search = function() {
            weatherService.getWeatherResult($scope.location, $scope.temperatureUnit, $scope.windSpeedUnit)
                .then(onSuccessCallback, onErrorCallback);
        }

        $scope.clearvmweatherResult = function () {
            $scope.vmweatherResult = null;
            $scope.errorMessage = null;
        };

        function onSuccessCallback(response) {
            $scope.vmweatherResult = response;
        };

        function onErrorCallback(response) {
            $scope.errorMessage = "Failed to get Weather results and Status code is " + response.status;
        };

    });
})();
