(function () {
    angular.module("CRMApp.Services", [])
            .service("AskcustomerDetailsService", ["$http",
                function ($http) {
                    var list = {};

                    // GETById DATA
                    list.GetByIdAskcustomerDetails = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AskcustomerDetails/GetByIdAskCustomerDetails?AskCustId=" + id
                        });
                    }

                    //CREATE/UPDATE RECORD
                    list.CreateUpdateAskcustomerDetails = function (data) {

                        return $http({
                            method: "POST",
                            url: "/Master/AskcustomerDetails/SaveAskcustomerDetails",
                            data: data,
                            contentType: "application/json"
                        })
                    }

                    // DELETE RECORD
                    list.DeleteAskcustomerDetails = function (id) {

                        return $http({
                            method: "POST",
                            url: "/Master/AskcustomerDetails/DeleteAskCustomerDetails?AskCustId=" + id
                        })
                    }


                    return list;
                }]);
})()