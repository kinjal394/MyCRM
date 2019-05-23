/// <reference path="D:\Raju_Data\CRM\CRM_Proj\CRM\Content/lib/angular/angular.js" />

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CountryService", ["$http",
            function ($http) {
                var list = {};
                list.GetAllCountry = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/Country"
                    });
                }

                list.GetCountryFlag = function () {
                    return $http({
                        method: 'post',
                        url: '/Home/getCountryData',
                        contentType: 'application/json; charset=utf-8',
                        data: {},
                        responseType: 'json',
                    }).then(function (result) {
                        var data = [];
                        _.each(result.data, function (value, key, list) {
                            if (value.CountryCallCode == "+91") {
                                data.push({
                                    icon: '<span><img src="/Content/lib/CountryFlags/flags/' + value.CountryFlag + '"  /></span>',
                                    code: value.CountryCallCode,
                                    name: value.CountryName,
                                    id: value.CountryId,
                                    ticked: true
                                })
                            }
                            else {
                                data.push({
                                    icon: '<span><img src="/Content/lib/CountryFlags/flags/' + value.CountryFlag + '"  /></span>',
                                    code: value.CountryCallCode,
                                    name: value.CountryName,
                                    id: value.CountryId,
                                    ticked: false
                                })
                            }
                        })
                        return data;
                    });
                }


                list.AddCountry = function (data, file) {
                    debugger;
                    if (parseInt(data.CountryId) > 0) {
                        var CountryId = data.CountryId;
                        var CountryFlag = data.CountryFlag;
                    }
                    else {
                        var CountryId = "0";
                        var CountryFlag = "";
                    }

                    var CountryName = data.CountryName;
                    var CountryAlphaCode = data.CountryAlphaCode;
                    var CountryCallCode = data.CountryCallCode;

                    var formData = new FormData();

                    formData.append("CountryId", CountryId);
                    formData.append("CountryFlag", CountryFlag);
                    formData.append("CountryName", CountryName);
                    formData.append("CountryAlphaCode", CountryAlphaCode);
                    formData.append("CountryCallCode", CountryCallCode);
                    formData.append("file", file);

                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/Country/SaveCountry",
                        data: formData,
                        withCredentials: true,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    })

                    // $http.post("/Master/Country/SaveCountry", formData,
                    // {
                    //    withCredentials: true,
                    //    headers: { 'Content-Type': undefined },
                    //    transformRequest: angular.identity
                    //})
                }

                list.DeleteCountry = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/Country/DeleteCountry?CountryId=" + data
                    })
                }

                // GETById DATA
                list.GetByIdCountry = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/Country/GetByIdCountry?CountryID=" + id
                    });
                }

                return list;
            }])
})()