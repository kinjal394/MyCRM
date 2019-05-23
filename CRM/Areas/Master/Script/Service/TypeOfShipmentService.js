

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("TypeOfShipmentService", ["$http",
            function ($http) {
                var list = {};
                list.getAllShipment = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment"
                    });
                }

                list.SaveTypeOfShipment = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TypeOfShipment/SaveTypeOfShipment",
                        data: data,
                        contentType: "application/json"
                    })
                }


                list.UpdateTypeOfShipment = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/TypeOfShipment/UpdateTypeOfShipment",
                        data: data,
                        contentType: "application/json"
                    })
                }

                

                list.DeleteTypeOfShipment = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/TypeOfShipment/DeleteTypeOfShipment?TypeOfShipmentId=" + id
                    })
                }

                //list.EditShip = function (data) {
                //    return $http({
                //        method: "POST",
                //        url: "/Master/ModeShipment/EditShip?ShipmentId=" + data
                //    })
                //}

                // GETById DATA
                list.GetTypeOfShipmentById = function (id) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment/GetShipById?ShipmentId=" + id
                    });
                }
                return list;
            }])
})()


//(function () {
//    "use strict";
//    angular.module("CRMApp.Services")
//            .service("ModeShipmentService", ["$http",
//            function ($http) {
//                var ModeShipmentViewModel = {};


//                var _addModeshipment = function (ModeShipment) {
//                    return $http({
//                        method: "POST",
//                        url: "/Master/ModeShipment/SaveModeShipment",
//                        data: ModeShipment
//                    }).success(function (data) {
//                        return data
//                    }).error(function (e) {
//                        return e
//                    })
//                }



//                ModeShipmentViewModel.addmodeshipment = _addModeshipment;
//                return ModeShipmentViewModel


//            }])
//})()