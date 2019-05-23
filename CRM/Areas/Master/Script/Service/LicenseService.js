(function () {
    angular.module("CRMApp.Services", [])
    .service("LicenseService", ["$http",
        function ($http) {
            var list = {};

            // GETById DATA
            list.GetByIdLicense = function (id) {
                return $http({
                    method: "POST",
                    url: "/Master/License/GetByIdLicense?LicenseId=" + id
                })
            }

            //CREATE/UPDATE RECORD
            list.CreateUpdateLicense = function (data) {
                return $http({
                    method: "POST",
                    url: "/Master/License/SaveLicense",
                    data: data,
                    contentType: "application/json"
                })
            }

            // DELETE RECORD
            list.DeleteLicense = function (id) {
                return $http({
                    method: "POST",
                    url: "/Master/License/DeleteLicense?LicenseId=" + id
                });
            }
            return list;
        }]);

})()