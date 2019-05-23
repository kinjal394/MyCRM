(function () {
    "use strict";
    angular.module("CRMApp.Services")
     .service("InwardCourierService", ["$http",
          function ($http) {

              var list = {};

              list.CreateUpdate = function (data) {
                  return $http({
                      method: "POST",
                      url: "/Master/InwardCourier/CreateUpdate",
                      data: data,
                      contentType: "application/json"
                  })
              }

              list.Delete = function (id) {
                  return $http({
                      method: "POST",
                      url: "/Master/InwardCourier/Delete?Id=" + id
                  })
              }

              list.GetById = function (id) {
                  return $http({
                      method: "Get",
                      url: "/Master/InwardCourier/GetById?Id=" + id
                  });
              }
              list.FetchAllInfoById = function (Id) {
                  return $http({
                      method: "Get",
                      url: "/Master/InwardCourier/FetchAllInfoById?Id=" + Id
                  });

              }
              list.InwardCourierInfo = function () {
                  return $http({
                      method: "POST",
                      url: "/Master/InwardCourier/InwardCourierInfo"
                  });
              }
              return list;

          }])
})()