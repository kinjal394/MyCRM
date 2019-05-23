(function () {
    "use strict";
    angular.module("CRMApp.Services")
            .service("CompanyService", ["$http",
            function ($http) {
                var CompanyViewModel = {};

                var _addCompany = function (company, file) {
                    console.log(company);

                    var ComId = "0";
                    var ComLogo = company.Setlogo;
                    var ComName = company.ComName;
                    var RegOffAdd = company.RegOffAdd;
                    var CorpOffAdd = company.CorpOffAdd;
                    var TelNos = company.TelNos;
                    var Email = company.Email;
                    var Web = company.Web;
                    var ComCode = company.ComCode;
                    var TaxDetails = company.TaxDetails;

                    var formData = new FormData();
                    formData.append("ComId", ComId);
                    formData.append("ComLogo", ComLogo == undefined ? null : ComLogo);
                    formData.append("ComName", ComName);
                    formData.append("RegOffAdd", RegOffAdd);
                    formData.append("CorpOffAdd", CorpOffAdd);
                    formData.append("TelNos", TelNos);
                    formData.append("Email", Email);
                    formData.append("Web", Web);
                    formData.append("ComCode", ComCode);
                    formData.append("TaxDetails", TaxDetails);

                    formData.append("file", file);

                    return $http({
                        method: "POST",
                        url: "/Master/Company/saveCompany",
                        data: formData,
                        withCredentials: true,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _DeleteComapny = function (Comid) {
                    return $http({
                        method: "POST",
                        url: "/Master/Company/DeleteCompany",
                        data: '{id:"' + Comid + '"}'
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })

                }

                var _UpdateCompany = function (Comapny, file) {

                    console.log(Comapny);

                    if (parseInt(Comapny.ComId) > 0) {
                        var CompanyId = Comapny.ComId;
                        var ComLogo = Comapny.Setlogo;
                    }
                    var ComName = Comapny.ComName;
                    var RegOffAdd = Comapny.RegOffAdd;
                    var CorpOffAdd = Comapny.CorpOffAdd;
                    var TelNos = Comapny.TelNos;
                    var Email = Comapny.Email;
                    var Web = Comapny.Web;
                    var ComCode = Comapny.ComCode;
                    var TaxDetails = Comapny.TaxDetails;

                    var formData = new FormData();

                    formData.append("ComId", CompanyId);
                    formData.append("ComLogo", ComLogo);
                    formData.append("ComName", ComName);
                    formData.append("RegOffAdd", RegOffAdd);
                    formData.append("CorpOffAdd", CorpOffAdd);
                    formData.append("TelNos", TelNos);
                    formData.append("Email", Email);
                    formData.append("Web", Web);
                    formData.append("Web", Web);
                    formData.append("ComCode", ComCode);
                    formData.append("TaxDetails", TaxDetails);
                    return $http({
                        method: "POST",
                        url: "/Master/Company/UpdateCompany",
                        data: formData,
                        withCredentials: true,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                var _GetCompanyById = function (id) {
                    return $http({
                        method: "GET",
                        url: "/Master/Company/GetCompanyById/" + id,
                    }).success(function (data) {
                        return data
                    }).error(function (e) {
                        return e
                    })
                }

                CompanyViewModel.addCompany = _addCompany;
                CompanyViewModel.Update = _UpdateCompany;
                CompanyViewModel.DeleteCompany = _DeleteComapny;
                CompanyViewModel.GetCompanyById = _GetCompanyById;
                return CompanyViewModel;


            }])
})()
