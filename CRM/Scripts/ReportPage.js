$(function () {
    $('.datepicker').datepicker({
        startDate: '-3d'
    });
});
function Search() {
   var qry = "ReportName=" + $("#hdnReportName").val();
    $(".modal-body").find('input').each(function () {
        qry += "&" + $(this).attr("SearchPara") + "=" + $(this).val();
    });
    $("#ReportPage").attr("src", "/ReportViewer.aspx?" + qry);
    $("#myModal").modal("hide");
}