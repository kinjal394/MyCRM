

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("InvoiceTypeService", ["$http",
            function ($http) {
                var list = {};
                list.getAllShipment = function () {
                    return $http({
                        method: "POST",
                        url: "/Master/ModeShipment"
                    });
                }

                list.SaveInvoiceType = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/InvoiceTypeMaster/SaveInvoiceType",
                        data: data,
                        contentType: "application/json"
                    })
                }


                list.UpdateInvoiceType = function (data) {
                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/InvoiceTypeMaster/UpdateInvoiceType",
                        data: data,
                        contentType: "application/json"
                    })
                }



                list.DeleteInvoiceType = function (id) {
                    return $http({
                        method: "POST",
                        url: "/Master/InvoiceTypeMaster/DeleteInvoiceType?InvoiceTypeId=" + id
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