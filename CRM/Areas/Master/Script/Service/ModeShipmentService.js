

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("ModeShipmentService", ["$http",
            function ($http) {
                var list = {};
                list.getAllShipment = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment"
                    });
                }

                list.SaveModeShipment = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment/SaveModeShipment",
                        data: data,
                        contentType: "application/json"
                    })
                }

                list.DeleteShip = function (data) {
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment/DeleteShip?ShipmentId=" + data
                    })
                }

                //list.EditShip = function (data) {
                //    return $http({
                //        method: "POST",
                //        url: "/Master/ModeShipment/EditShip?ShipmentId=" + data
                //    })
                //}

                // GETById DATA
                list.GetShipById = function (id) {
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