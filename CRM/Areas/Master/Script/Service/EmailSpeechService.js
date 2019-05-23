(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("EmailSpeechService", ["$http",
            function ($http) {
                var EmailSpeechServiceViewModel = {};
                var _AddSpeech = function (speechData) {
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSpeech/SaveSpeechMaster",
                        data: speechData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
                var _GetSpeechById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/EmailSpeech/GetSpeechById/" + id,
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteSpeech = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSpeech/DeleteSpeech",
                        data: '{id:"' + id + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateSpeech = function (speechData) {
                    return $http({
                        method: "POST",
                        url: "/Master/EmailSpeech/UpdateSpeech",
                        data: speechData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                EmailSpeechServiceViewModel.AddSpeech = _AddSpeech;
                EmailSpeechServiceViewModel.UpdateSpeech = _UpdateSpeech;
                EmailSpeechServiceViewModel.DeleteSpeech = _DeleteSpeech;
                EmailSpeechServiceViewModel.GetSpeechById = _GetSpeechById;
                return EmailSpeechServiceViewModel


            }])
})()
