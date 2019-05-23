(function () {

    angular.module('CRMApp.Controllers')
        .controller("TimeCtrl",
        ["$scope", "$rootScope", "$timeout",
            Demo1Ctrl]);

    function Demo1Ctrl($scope, $rootScope, $timeout) {
        debugger;
        $rootScope.second = 1800;
        var tick = function () {

            if ($rootScope.second <= 0) {
                $timeout.cancel(tick);
            }
            else {
                $rootScope.second--;
                $scope.clock = Date.now() // get the current time
                $rootScope.timer = "Your session will expire in " + parseInt($rootScope.second / 60) + ":" + parseInt($rootScope.second % 60) + " minutes.";
                $timeout(tick, 1000); // reset the timer
            }
        }

        $timeout(tick, 1000);
    }
})()