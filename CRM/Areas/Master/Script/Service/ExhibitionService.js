(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ExhibitionService", ["$http",
            function ($http) {
                var ExhibitionViewModel = {};
                var _getAllCountries = function () {
                  return  $http({
                        method: 'Get',
                        url: '/Master/Exhibition/GetCountry'
                    }).success(function (data) {
                       return data;
                    }).error(function (data, status, headers, config) {
                        return 'Unexpected Error';
                    });
                }
                var _getStateByID = function (countryId) {
                    return $http({
                        method: 'Get',
                        url: '/Master/Exhibition/GetStates?CountryID=' + countryId
                    }).success(function (data) {
                        return data;
                    }).error(function (data, status, headers, config) {
                        return 'Unexpected Error';
                    });
                }
                var _getCityByID = function (stateId) {
                    return $http({
                        method: 'Get',
                        url: '/Master/Exhibition/GetCity?StateId=' + stateId
                    }).success(function (data) {
                        return data;
                    }).error(function (data, status, headers, config) {
                        return 'Unexpected Error';
                    });
                }
                var _getAreaByID = function (AreaId) {
                    return $http({
                        method: 'Get',
                        url: '/Master/Exhibition/GetArea?AreaId=' + AreaId
                    }).success(function (data) {
                        return data;
                    }).error(function (data, status, headers, config) {
                        return 'Unexpected Error';
                    });
                }
                var _saveExhibition = function (exhibitionData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Exhibition/SaveExhibition",
                        data: exhibitionData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _DeleteExhibition = function (ExId) {
                    return $http({
                        method: "POST",
                        url: "/Master/Exhibition/DeleteExhibition",
                        data: '{id:"' + ExId + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }
                var _UpdateExhibition = function (exhibitionData) {
                    return $http({
                        method: "POST",
                        url: "/Master/Exhibition/UpdateExhibition",
                        data: exhibitionData
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }
             
                     
                ExhibitionViewModel.getAllCountries = _getAllCountries;
                ExhibitionViewModel.getStateByID = _getStateByID;
                ExhibitionViewModel.getCityByID = _getCityByID;
                ExhibitionViewModel.saveExhibition = _saveExhibition;
                ExhibitionViewModel.UpdateExhibition = _UpdateExhibition;
                ExhibitionViewModel.DeleteExhibition = _DeleteExhibition;
                return ExhibitionViewModel;
            }])
})()