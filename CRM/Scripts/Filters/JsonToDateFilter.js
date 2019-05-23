angular.module('CRMApp.Filters').filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(x.substr(6)));
        else return null;
    };
});