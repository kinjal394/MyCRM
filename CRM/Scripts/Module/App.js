/// <reference path="E:\Urvish\CRM\Project\CRM\Content/lib/angular/angular.js" />
(function () {
    angular.module("CRMApp", [
                    "CRMApp.Controllers",
                    "CRMApp.Directives",
                    "CRMApp.Filters",
                    "CRMApp.Services",
                    "CRMApp.factory",
                    "ngTable",
                    "ui.bootstrap",
                    "ui.bootstrap.tpls",
                    "ngFileUpload",
                    "chart.js"
    ]);

    angular.module("CRMApp.Controllers", ["isteven-multi-select", 'ngTagsInput', "ui.calendar", "ngCkeditor"]);
    angular.module("CRMApp.Directives", []);
    angular.module("CRMApp.Filters", []);
    angular.module("CRMApp.Services", []);
    angular.module("CRMApp.factory", []);

    angular.module("CRMApp").run([
            "$rootScope", "$location", "$interval",
            function ($rootScope, $location, $interval) {
                $rootScope.IsAjaxLoading = false;
                $rootScope.UserType = 0;
               //LIVE
                //$rootScope.webApiUrl = "Live";
                $rootScope.webApiUrl = "Demo";
                //var lastDigestRun = Date.now();
                //$rootScope.Expired = 5;
                //var idleCheck = $interval(function () {
                //    var now = Date.now();
                //    if (now - lastDigestRun > 1 * 60 * 1000) {
                //        alert("logout");
                //        $rootScope.Expired = 1;
                //    }
                //}, 60 * 1000);

                $rootScope.$on('$routeChangeStart', function (evt) {
                    lastDigestRun = Date.now();
                });

                $rootScope.errorHandler = function (error) {
                    if (error.data) {
                        if (error.data.ExceptionMessage)
                            toastr.error(error.data.ExceptionMessage);
                        else if (error.data.Message)
                            toastr.error(error.data.Message);
                        else
                            toastr.error(error.data);
                    }
                    else
                        toastr.error(error);
                }


            }]);
    angular.module("CRMApp").config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    }]);

    angular.module("CRMApp").factory('httpAuthInterceptor', function ($q) {
        return {
            'responseError': function (response) {
                if ([401, 403].indexOf(response.status) >= 0) {
                    window.location.href = angular.fromJson(response.data).LogOnUrl.Url; //NEW CODE
                    //window.location.href = response.data.LogOnUrl.Url; // OLD CODE
                    return response;
                } else {
                    // window.location.href = "/Login/Index";
                    //return $q.reject(rejection);
                }
            }
        };
    });
    angular.module("CRMApp").config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('httpAuthInterceptor');
    }]);




})()