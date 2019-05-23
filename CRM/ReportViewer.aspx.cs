using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using CRM_Repository.ServiceContract;
using CRM_Repository.Service;
using CRM_Repository.DTOModel;
using System.Net;

namespace CRM
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (sessionUtils.HasUserLogin())
            {
                ReportPara obj = new ReportPara();
                string a = obj.ReportName;

                string ReportFile = "";
                DataSet ds = new DataSet();
                ReportDocument rptDoc = new ReportDocument();

                #region Set Report File Path with name
                if (obj.ReportName == "Quotation")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptQuotation.rpt");
                else if (obj.ReportName == "Employee")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptEmployee.rpt");
                else if (obj.ReportName == "Daily")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptDailyRepotingSystem.rpt");
                else if (obj.ReportName == "DailyReport")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptDailyReportSystem.rpt");
                else if (obj.ReportName == "PurchaseOrder")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptPurchaseOrder.rpt");
                else if (obj.ReportName == "ProductDetail")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptProductDetails.rpt");
                else if (obj.ReportName == "PerformaInvoice")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptPerforma.rpt");
                else if (obj.ReportName == "PurOrder")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptPurOrder.rpt");
                else if (obj.ReportName == "SaleOrder")
                    ReportFile = Server.MapPath("~/Reporting/Reports/RptSalesOrder.rpt");

                #endregion

                #region Set Data for Report
                if (ViewState["dt"] == null)
                {
                    if (obj.ReportName == "Quotation")
                    {
                        Quotation_Repository objData = new Quotation_Repository();
                        ds = objData.GetQuotationReportData(obj);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dr["ComLogo"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ComLogoImgPath"].ToString() + "/" + Convert.ToString(dr["ComLogo"]);

                        }
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            dr["Photo"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["Photo"]);
                            if (!string.IsNullOrEmpty(dr["PhotoLink"].ToString()))
                            {
                                string photo = "";
                                string[] tokens = Convert.ToString(dr["PhotoLink"]).Split('|');
                                for (int i = 0; i <= tokens.Length - 1; i++)
                                {
                                    if (photo == "")
                                    {
                                        photo = "<a href='" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i] + "</a>";
                                    }
                                    else
                                    {
                                        photo += ", " + "<a href='" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i] + "</a>";
                                    }

                                }
                                dr["PhotoLink"] = photo;
                                //dr["PhotoLink"] = Convert.ToString("<a href='" + dr["GPlusLink"] + "' target = '_blank'>" + dr["GPlusLink"] + "</a>");
                            }
                            if (!string.IsNullOrEmpty(dr["VideoLink"].ToString()))
                            {
                                string Video = "";
                                string[] tokens = Convert.ToString(dr["VideoLink"]).Split('|');
                                for (int i = 0; i <= tokens.Length - 1; i++)
                                {
                                    if (Video == "")
                                    {
                                        Video = "<a href='" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i].Trim() + "</a>";
                                    }
                                    else
                                    {
                                        Video += ", " + "<a href='" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i].Trim() + "</a>";
                                    }

                                }
                                dr["VideoLink"] = Video;
                                //dr["PhotoLink"] = Convert.ToString("<a href='" + dr["GPlusLink"] + "' target = '_blank'>" + dr["GPlusLink"] + "</a>");
                            }
                            //if (!string.IsNullOrEmpty(dr["FbLink"].ToString()))
                            //    dr["FbLink"] = Convert.ToString("<a href='" + dr["FbLink"] + "' target = '_blank'>" + dr["FbLink"] + "</a>");

                        }
                        ds.Tables[1].AcceptChanges();
                    }
                    else if (obj.ReportName == "Employee")
                    {
                        User_Repository objData = new User_Repository();
                        ds = objData.GetEmployeeReportData(obj);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dr["Photo"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["UserImagePath"].ToString() + "/" + Convert.ToString(dr["Photo"]);//Server.MapPath("~/UploadImages/UserImage/" + Convert.ToString(dr["Photo"]));
                        }
                        ds.Tables[0].AcceptChanges();
                    }
                    else if (obj.ReportName == "Daily")
                    {
                        Attendance_Repository objData = new Attendance_Repository();
                        ds = objData.GetDailyReportData(obj);

                    }
                    else if (obj.ReportName == "DailyReport")
                    {
                        Attendance_Repository objData = new Attendance_Repository();
                        ds = objData.GetDailyReport(obj);

                    }
                    else if (obj.ReportName == "PurchaseOrder")
                    {
                        PurchaseOrder_Repository objData = new PurchaseOrder_Repository();
                        ds = objData.GetPurchaseOrderReport(obj);

                    }
                    else if (obj.ReportName == "ProductDetail")
                    {
                        Product_Repository objData = new Product_Repository();

                        ds = objData.GetProductDetail(obj);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            dr["MainPhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["MainPhoto"]);
                            dr["FinishPhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["FinishPhoto"]);
                            dr["SecondPhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["SecondPhoto"]);
                            dr["TherdPhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["TherdPhoto"]);
                            dr["FourPhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["FourPhoto"]);
                            dr["FivePhoto"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + Convert.ToString(dr["FivePhoto"]);


                            if (!string.IsNullOrEmpty(dr["PhotosLink"].ToString()))
                            {
                                string photo = "";
                                string[] tokens = Convert.ToString(dr["PhotosLink"]).Split('|');
                                for (int i = 0; i <= tokens.Length - 1; i++)
                                {
                                    if (photo == "")
                                    {
                                        photo = "<a href='" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i] + "</a>";
                                    }
                                    else
                                    {
                                        photo += "<br> <a href='" + System.Configuration.ConfigurationManager.AppSettings["ProductImagePath"].ToString() + "/" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i] + "</a>";
                                    }

                                }
                                dr["PhotosLink"] = photo;
                                //dr["PhotoLink"] = Convert.ToString("<a href='" + dr["GPlusLink"] + "' target = '_blank'>" + dr["GPlusLink"] + "</a>");
                            }

                            if (!string.IsNullOrEmpty(dr["Socialmedialink"].ToString()))
                            {
                                string Social = "";
                                string[] tokens = Convert.ToString(dr["Socialmedialink"]).Split('|');
                                for (int i = 0; i <= tokens.Length - 1; i++)
                                {
                                    string aa = tokens[i].Trim().Split(' ')[2];
                                    if (Social == "")
                                    {
                                        Social = tokens[i].Trim().Split(' ')[0] + ":  <a href='" + tokens[i].Trim().Split(' ')[2] + "' target = '_blank'>" + tokens[i].Trim().Split(' ')[2] + "</a>";
                                    }
                                    else
                                    {
                                        Social += "<br> " + tokens[i].Trim().Split(' ')[0] + ":  <a href='" + tokens[i].Trim().Split(' ')[2] + "' target = '_blank'>" + tokens[i].Trim().Split(' ')[2] + "</a>";
                                    }

                                }
                                dr["Socialmedialink"] = Social;

                            }



                            if (!string.IsNullOrEmpty(dr["ProductVideo"].ToString()))
                            {
                                string Video = "";
                                string[] tokens = Convert.ToString(dr["ProductVideo"]).Split('|');
                                for (int i = 0; i <= tokens.Length - 1; i++)
                                {
                                    if (Video == "")
                                    {
                                        Video = "<a href='" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i].Trim() + "</a>";
                                    }
                                    else
                                    {
                                        Video += " <br><a href='" + tokens[i].Trim() + "' target = '_blank'>" + tokens[i].Trim() + "</a>";
                                    }

                                }
                                dr["ProductVideo"] = Video;

                            }

                        }
                        ds.Tables[0].AcceptChanges();

                    }
                    else if (obj.ReportName == "PerformaInvoice")
                    {
                        PerformaInvoice_Repository objData = new PerformaInvoice_Repository();
                        ds = objData.GetPerformaReportData(obj);

                    }
                    else if (obj.ReportName == "PurOrder")
                    {
                        PurchaseOrder_Repository objData = new PurchaseOrder_Repository();
                        ds = objData.GetPurchaseOrderReportData(obj);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dr["ComLogo"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ComLogoImgPath"].ToString() + "/" + Convert.ToString(dr["ComLogo"]);

                        }
                        ds.Tables[0].AcceptChanges();

                    }
                    else if (obj.ReportName == "SaleOrder")
                    {
                        SalesOrder_Repository objData = new SalesOrder_Repository();
                        ds = objData.GetSaleOrderReportData(obj);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dr["ComLogo"] = System.Configuration.ConfigurationManager.AppSettings["MainHost"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["ComLogoImgPath"].ToString() + "/" + Convert.ToString(dr["ComLogo"]);

                        }
                        ds.Tables[0].AcceptChanges();
                    }

                }
                else
                {
                    ds = (DataSet)ViewState["dt"];
                }
                #endregion

                if (ReportFile == "")
                {
                    return;
                }
                rptDoc.Load(ReportFile);
                rptDoc.SetDataSource(ds);
                if (obj.ReportName != "PurOrder" && obj.ReportName != "PerformaInvoice" && obj.ReportName != "SaleOrder")
                {
                    if (ds.Tables.Count >= 2)
                        rptDoc.Subreports[0].SetDataSource(ds.Tables[1]);

                }

                ViewState["dt"] = ds;
                CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.GroupTree;
                CrystalReportViewer1.ReportSource = rptDoc;
            }
            else
            {

            }
        }
    }
}