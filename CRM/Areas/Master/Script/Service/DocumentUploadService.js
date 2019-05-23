

(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("DocumentUploadService", ["$http",
            function ($http) {
                var list = {};
                //list.GetAllCountry = function () {
                //    return $http({
                //        method: "POST",
                //        url: "/Master/Country"
                //    });
                //}
                list.bindgrid = function (data) {
                    debugger;
                    return $http({
                        method: "Get",
                        url: "/Master/DocumentUpload/bindgrid?EmpId=" + data
                    });
                }
                list.EmployeeBind = function () {
                    debugger;
                    return $http({
                        method: "Get",
                        url: "/Master/DocumentUpload/EmployeeBind"
                    });
                }
                list.DocNameBind = function () {
                    debugger;
                    return $http({
                        method: "Get",
                        url: "/Master/DocumentUpload/DocNameBind"
                    });
                }

                list.AddDocumentUpload = function (data, file) {
                    debugger;
                    if (parseInt(data.EmpDocId) > 0) {
                        var EmpDocId = data.EmpDocId;
                        var Photo = data.Photo;
                        var EmpId = data.EmpId;
                        var DocId = data.DocId;
                    }
                    else {
                        var EmpDocId = "0";
                        var Photo = "";
                        var EmpId = "0";
                        var DocId = "0";
                    }

                    var EmpDocId = "0";
                    var EmpId = data.EmpId;
                    var DocId = data.DocId;
                    var formData = new FormData();
                    formData.append("EmpDocId", EmpDocId);
                    formData.append("EmpId", EmpId);
                    formData.append("DocId", DocId);
                    formData.append("Photo", Photo);
                    formData.append("file", file);

                    debugger;
                    return $http({
                        method: "POST",
                        url: "/Master/DocumentUpload/SaveDocumentUpload",
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


                // GETById DATA
                //list.GetByIdCountry = function (id) {
                //    return $http({
                //        method: "POST",
                //        url: "/Master/Country/GetByIdCountry?CountryID=" + id
                //    });
                //}

                return list;
            }])
})()