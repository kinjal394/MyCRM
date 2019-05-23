(function () {
    "use strict";
    angular.module("CRMApp.Services")
     .service("OutwardCourierService", ["$http",
          function ($http) {

              var list = {};

              list.CreateUpdate = function (data) {
                  return $http({
                      method: "POST",
                      url: "/Master/OutwardCourier/CreateUpdate",
                      data: data,
                      contentType: "application/json"
                  })
              }

              list.Delete = function (Id) {
                  return $http({
                      method: "POST",
                      url: "/Master/OutwardCourier/Delete?Id=" + Id
                  })
              }

              list.GetById = function (Id) {
                  return $http({
                      method: "Get",
                      url: "/Master/OutwardCourier/GetById?Id=" + Id
                  });
              }

              list.FetchAllInfoById = function (Id) {
                  return $http({
                      method: "Get",
                      url: "/Master/OutwardCourier/FetchAllInfoById?Id=" + Id
                  });

              }
              list.FetchAddressById = function (Id, AddressId) {
                  console.log(Id);
                  console.log(AddressId);
                  return $http({
                      method: "Get",
                      url: "/Master/OutwardCourier/FetchAddressById?Id=" + Id + "&AddressId=" + AddressId // + "&type=" + type
                  });

              }

              list.OutwardCourierInfo = function () {
                  return $http({
                      method: "POST",
                      url: "/Master/OutwardCourier/OutwardCourierInfo"
                  });
              }
              return list;

          }])
})()