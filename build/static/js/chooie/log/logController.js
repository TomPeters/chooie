angular.module('chooie').controller("LogController", ['$scope', 'LogService', 'ClientMessenger',
    function($scope, logService, clientMessenger) {

        var getDateFromLogTimeString = function(timeString) {
            if(timeString.indexOf("/Date") !== -1) {
                return new Date(parseInt(timeString.replace("/Date(", "").replace(")/",""), 10))
            }
            return new Date(timeString);
        };

        var createLogViewModel = function(logMessage) {
            switch(logMessage.Severity) {
                case 0:
                    logMessage.Severity = "Info";
                    break;
                case 1:
                    logMessage.Severity = "Warning";
                    break;
                case 2:
                    logMessage.Severity = "Error";
                    break;
                default:
                    logMessage.Severity = "Unknown";
            }
            var time = getDateFromLogTimeString(logMessage.Time);
            logMessage.Time = time.toLocaleTimeString();
            return logMessage;
        };

        var getLogPromise = logService.getLog().success(function(logMessages) {
            $scope.logMessages = logMessages.map(createLogViewModel);
        });

        var addNewLogMessage = function(newLogMessageJson) {
            var newLogMessage = JSON.parse(newLogMessageJson);
            getLogPromise.success(function() {
                $scope.logMessages.push(createLogViewModel(newLogMessage));
            })
        };

        clientMessenger.registerCallback("Log Message", addNewLogMessage);
    }]);