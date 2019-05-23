


(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("SMSSpeechService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllSMSSpeech = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/SMSSpeech"
                    });
                }

                list.AddSMSSpeech = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/SMSSpeech/SaveSMSSpeech",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteSMSSpeech = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/SMSSpeech/DeleteSMSSpeech?SMSId=" + data
                    })
                }
                list.GetSMSSpeechById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/SMSSpeech/GetSMSSpeechById?SMSId=" + id
                    });
                }
                return list;
            }])
})()