using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;
using CRM_Repository.DataServices;
using CRM_Repository.Data;

namespace CRM_Repository
{
    public static class CommonFunctions
    {
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])))
                        {
                            if (pro.PropertyType.Name == "String")
                                pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]));
                            else
                                pro.SetValue(obj, dr[column.ColumnName]);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string ToTimeAgo(this DateTime dt)
        {
            if (dt == DateTime.Now)
                return "about sometime from now";
            TimeSpan span = DateTime.Now - dt;

            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago", years, years == 1 ? "year" : "years");
            }

            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago", months, months == 1 ? "month" : "months");
            }

            if (span.Days > 0)
                return String.Format("about {0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");

            if (span.Hours > 0)
                return String.Format("about {0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");

            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");

            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);

            if (span.Seconds <= 5)
                return "just now";

            return string.Empty;
        }

        public static void ExportToExcel(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.HeaderRow.BackColor = System.Drawing.Color.Blue;
            GridView1.HeaderRow.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderRow.Font.Bold = true;
            //GridView1.Style.Add("word-wrap", "break-word");
            //GridView1.Style.Add("width", "100");
            if (string.IsNullOrEmpty(FileName))
                FileName = "Excel";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
             "attachment;filename=" + FileName + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //TextBox txtAddress = new TextBox();
            //txtAddress.ReadOnly = false;
            //txtAddress.Style.Add("width", "99%");
            foreach (GridViewRow row in GridView1.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Style.Add("class", "textmode");
                    //txtAddress.Style = "width:100%;";
                }
            }
            //GridView1.Attributes.Add("style", "table-layout:fixed");
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //}
            GridView1.RenderControl(hw);
            //string style = @"<!--mce:2-->"; 
            //HttpContext.Current.Response.Write(style);
            //Open a memory stream that you can use to write back to the response
            byte[] byteArray = Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream s = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(s, Encoding.ASCII);
            HttpContext.Current.Response.Write(sr.ReadToEnd());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToWord(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.HeaderRow.BackColor = System.Drawing.Color.Blue;
            GridView1.HeaderRow.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderRow.Font.Bold = true;

            if (string.IsNullOrEmpty(FileName))
                FileName = "Word";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".doc");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToPdf(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Black;
            //GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.HeaderStyle.Font.Size = 12;
            GridView1.Font.Size = 10;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.HeaderRow.BackColor = System.Drawing.Color.Blue;
            GridView1.HeaderRow.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderRow.Font.Bold = true;
            GridView1.Style.Add("width", "100");
            if (string.IsNullOrEmpty(FileName))
                FileName = "Pdf";
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToCsv(this DataTable dt, string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                FileName = "csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";


            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void SetLog(this Exception ex, string msg, Boolean IsRedirect = true)
        {
            if (ex is System.Data.Entity.Validation.DbEntityValidationException)
            {
                foreach (var validationErrors in ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += ";" + string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                    }
                }
            }
            string FileName = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
            if (!File.Exists(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
            {
                using (StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
                {
                    // StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt"), true);
                    sw.WriteLine("Error on " + DateTime.Now + " ,Exception Message:" + ex.Message + ",Inner Message:" + ex.InnerException + ",Line:" + ex.StackTrace + ",Additional Msg:" + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(HttpContext.Current.Server.MapPath("~/Logs/" + FileName + ".txt")))
                {
                    sw.WriteLine("Error on " + DateTime.Now + " ,Exception Message:" + ex.Message + ",Inner Message:" + ex.InnerException + ",Line:" + ex.StackTrace + ",Additional Msg: " + msg);
                    sw.Flush();
                    sw.Close();
                }
            }
            if (IsRedirect)
                throw ex;
        }

        public static decimal ConvertToDecimal(this object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static int ConvertToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime? ConvertToDateTime(this object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return null;
            }
        }

        public static DataAttendance GenerateAttResponse(WorkType ResponseType,bool IsCheck, string Message)
        {
            DataAttendance dataResponse = new DataAttendance();
            dataResponse.WorkTypeId = ResponseType;
            dataResponse.IsCheck = IsCheck;
            dataResponse.Message = Message.ToString();
            return dataResponse;
        }

    }
}
