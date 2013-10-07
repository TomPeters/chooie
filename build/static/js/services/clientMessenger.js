angular.module('chui').factory("ClientMessenger", ["SignalRCallbacks", function(callbacks) {
    return {
        registerCallback: function(dispatchId, callback) {
            if(!callbacks[dispatchId]) {
                callbacks[dispatchId] = [];
            }
            callbacks[dispatchId].push(callback);
        }
        // TODO: Add method to 'unregister' a callback
    }
}]);