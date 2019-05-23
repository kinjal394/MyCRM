using CRM.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class ExportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Form["Mode"]))
                {
                    try
                    {
                        // string mode = Convert.ToString(Request.Form["mode"]);
                        // GridData og = new GridData(mode, true);
                        GridReqData objGrid = new GridReqData();
                        objGrid.Filter = Convert.ToString(Request.Form["Filter"]);
                        objGrid.Mode = Convert.ToString(Request.Form["Mode"]);
                        objGrid.FixClause = (Convert.ToString(Request.Form["FixClause"]) == "") ? null : Convert.ToString(Request.Form["FixClause"]);
                        objGrid.Columns = (Convert.ToString(Request.Form["Columns"]) == "") ? null : Convert.ToString(Request.Form["Columns"]);
                        objGrid.PageNumber = -1;
                        objGrid.RecordPerPage = 10;
                        Dictionary<string, string> abc = new Dictionary<string, string>();
                        abc[Convert.ToString(Request.Form["SortColumn"])] = Convert.ToString(Request.Form["SortOrder"]);
                        objGrid.Sort = abc;
                        GridData obj = new GridData(objGrid, true);
                        //  return obj.JsonData;
                    }
                    catch (Exception)
                    {
                        //ex.SetLog("For Export Data");
                    }
                    finally
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallFunctionToHideImg", "HideLoadingImg();", true);
                    }
                }
            }
        }
    }
}