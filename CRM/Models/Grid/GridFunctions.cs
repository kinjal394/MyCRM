using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using CRM_Repository.DataServices;
using CRM_Repository;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;

namespace CRM.Models.Grid
{
    public class GridFunctions
    {
        GridReqData objReqData = new GridReqData();
        GridData objGrid;
        public GridFunctions(GridData oGrid, GridReqData obj)
        {
            objReqData = obj;
            // objGrid = new GridData(objReqData);
            objGrid = oGrid;
        }


        public string GetSortColumn()
        {
            string sortColumn = "";
            if (objReqData.Sort != null)
            {
                foreach (string key in objReqData.Sort.Keys)
                {
                    sortColumn = key;
                    break;
                }
            }
            else
            {
                return objGrid.SortColumn;
            }
            return sortColumn == "" ? objGrid.SortColumn : sortColumn;
        }

        public string GetSortOrder()
        {
            string sortOrder = "";
            if (objReqData.Sort != null)
            {
                foreach (string key in objReqData.Sort.Keys)
                {
                    sortOrder = objReqData.Sort[key];
                    break;
                }
            }
            else
            {
                return objGrid.SortOrder;
            }
            return sortOrder == "" ? "asc" : sortOrder;
        }

        public string GetWhereClause(string w = "")
        {
            string where = "";
            if (w == "")
                where = "1=1";
            else
                where = w;

            if (objReqData.Filter.Trim() != "{}")
            {
                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(objReqData.Filter);
                foreach (string key in values.Keys)
                {
                    if (!string.IsNullOrEmpty(values[key]))
                    {
                        var realVal = values[key];
                        try
                        {
                            if (realVal.Contains('-') || realVal.Contains('/'))
                            {
                                DateTime b = Convert.ToDateTime(values[key]);
                                realVal = b.ToString("yyyy-MM-dd");
                            }
                        }
                        catch
                        {
                            realVal = values[key];
                        }
                        where += " AND " + key + " LIKE '%" + realVal + "%'";
                    }
                }
            }
            return where;
        }
        public DataTable GetDataTable()
        {
            //GridData og = new GridData(mode);
            SqlParameter[] para = new SqlParameter[8];
            para[0] = new SqlParameter().CreateParameter("@TableName", objGrid.TableName, 2000);
            para[1] = new SqlParameter().CreateParameter("@ColumnsName", objGrid.ColumnsName, 5000);
            para[2] = new SqlParameter().CreateParameter("@SortOrder", GetSortOrder());
            para[3] = new SqlParameter().CreateParameter("@SortColumn", GetSortColumn());
            para[4] = new SqlParameter().CreateParameter("@PageNumber", objReqData.PageNumber);
            para[5] = new SqlParameter().CreateParameter("@RecordPerPage", objReqData.RecordPerPage);
            para[6] = new SqlParameter().CreateParameter("@WhereClause", GetWhereClause(objGrid.WhereClause), 1000);
            para[7] = new SqlParameter().CreateParameter("@MultiOrder", objGrid.MultiOrder, 2000);
            DataTable dt = new dalc().GetDataTable("GetDataForGrid", para);
            //string qry = @"EXEC  '" + objGrid.TableName + "','" + objGrid.ColumnsName + "','" + GetSortOrder() + "','" + GetSortColumn() + "','" + objReqData.PageNumber + "','" + objReqData.RecordPerPage + "','" + GetWhereClause(objGrid.WhereClause).Replace("'", "''") + "'";
            //dt = new dalc().selectbyquerydt(qry);
            return dt;
        }



        public string GetJson<T>()
        {
            DataTable dt = GetDataTable();
            Gdata<T> obj = new Gdata<T>();
            obj.data = dt.ConvertToList<T>();
            obj.recordsTotal = dt.Rows.Count == 0 ? "0" : dt.Rows[0]["TotalRows"].ToString();
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj);
        }

        public void Export(GridReqData obj = null)
        {
            DataTable dt = GetDataTable();
            if (obj != null)
            {
                string[] c = obj.Columns.Split(',');
                string[] s = obj.Columns.Split(',');
                for (int i = 0; i < c.Length; i++)
                {
                    c[i] = c[i].Split(' ')[0].ToString();
                }

                DataTable dtTemp = dt.Copy();
                int j = 0;
                foreach (DataColumn dc in dtTemp.Columns)
                {
                    if (!c.Contains(dc.ColumnName))
                        dt.Columns.Remove(dc.ColumnName);
                    else
                    {
                        dt.Columns[s[j].Split(' ')[0].ToString()].SetOrdinal(j);
                        dt.Columns[s[j].Split(' ')[0].ToString()].ColumnName = s[j].Split('[')[1].Replace("]", "").ToString();
                        j++;
                    }
                }
             
            }
          
            if (dt.Columns.Contains("TotalRows"))
                dt.Columns.Remove("TotalRows");
            if (dt.Columns.Contains("RowNumber"))
                dt.Columns["RowNumber"].ColumnName = "Sr.";
            string type = Convert.ToString(HttpContext.Current.Request.Form["type"]);
            if (type.ToLower() == "excel")
                dt.ExportToExcel(objGrid.ExportedFileName);
            else if (type.ToLower() == "pdf")
                dt.ExportToPdf(objGrid.ExportedFileName);
            else if (type.ToLower() == "word")
                dt.ExportToWord(objGrid.ExportedFileName);
            else if (type.ToLower() == "csv")
                dt.ExportToCsv(objGrid.ExportedFileName);
            else
                dt.ExportToExcel(objGrid.ExportedFileName);
        }
    }

    public class Gdata<T>
    {
        public List<T> data { get; set; }
        public string recordsTotal { get; set; }
    }
}