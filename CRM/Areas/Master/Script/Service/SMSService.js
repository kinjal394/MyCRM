(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("SMSService", ["$http",
            function ($http) {

                var list = {};

                list.SetSMSData = function (data) {

                    return $http({
                        method: "POST",
                        url: "/Master/SMS/SetSMS",
                        data: data,
                        contentType: "application/json"
                    })
                }

                return list;
            }])
})()