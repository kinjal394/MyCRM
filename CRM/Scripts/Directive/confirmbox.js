(function () {
    'use strict';
    angular
        .module("CRMApp.Directives")
        .directive("finalConfirmBox", [
            "$rootScope",
            function ($rootScope) {
                return {
                    restrict: "A",
                    scope: {
                        callback: "&callback",
                        message: "@",
                        cancelcallback: "&cancelcallback"
                    },
                    link: function (scope, element, attrs) {
                        element.on("click", function () {
                            bootbox.confirm(scope.message, function (result) {
                                if (result) {
                                    scope.callback();
                                } else {
                                    scope.cancelcallback();
                                }
                            });
                        });

                    }
                }
            }]);
})()

