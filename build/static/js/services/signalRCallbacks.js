angular.module('chui').service("SignalRCallbacks", [function() {
    $.connection.hub.url = "http://localhost:8080/signalr"; // TODO: Remove hardcoded localhost domain
    var hub = $.connection.messageHub;
    var callbacks = {};
    hub.client.sendMessage = function(dispatchId, message) {
        if(callbacks[dispatchId]) {
            angular.forEach(callbacks[dispatchId], function(callback) {
                callback(message); // TODO: Auto parse as json here?
            });
        }
    }
    $.connection.hub.start( { jsonp: true });
    return callbacks;
}]);