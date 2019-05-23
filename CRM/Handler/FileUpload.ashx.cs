using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace CRM.Handler
{
    public class vmImages
    {
        public string imageName { get; set; }
        public string imagePath { get; set; }
        public string thumbPath { get; set; }
    }
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<vmImages> uniqueFilenames = new List<vmImages>();
            string TempImgPath = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"]);
            if (!System.IO.Directory.Exists(TempImgPath.ToString()))
            {
                System.IO.Directory.CreateDirectory(TempImgPath.ToString());
            }
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                if (files.Count > 1)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        string uniqueFilename = string.Format("{2}_{1}_{0}{3}", System.DateTime.Now.ToString("ddMMyyhhmmssfffffff"), new Random().Next(1, 100), Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
                        string tempMainPath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"].ToString()), uniqueFilename);
                        file.SaveAs(tempMainPath);
                        uniqueFilenames.Add(
                              new vmImages
                              {
                                  imageName = uniqueFilename,
                                  imagePath = "/" + ConfigurationManager.AppSettings["TempImgPath"].ToString() + "/" + uniqueFilename,
                                  thumbPath = "/" + ConfigurationManager.AppSettings["TempThumbImgPath"].ToString() + "/" + uniqueFilename
                              });
                        // SLEEP THREAD TO GENERATE UNIQUE FILE NAMES
                        Thread.Sleep(100);
                    }
                }
                else
                {
                    HttpPostedFile file = files[0];
                    string uniqueFilename = string.Format("{2}_{1}_{0}{3}", System.DateTime.Now.ToString("ddMMyyhhmmssfffffff"), new Random().Next(1, 100), Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
                    string tempMainPath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["TempImgPath"].ToString()), uniqueFilename);
                    file.SaveAs(tempMainPath);
                    uniqueFilenames.Add(
                        new vmImages
                        {
                            imageName = uniqueFilename,
                            imagePath = "/" + ConfigurationManager.AppSettings["TempImgPath"].ToString() + "/" + uniqueFilename,
                            thumbPath = "/" + ConfigurationManager.AppSettings["TempThumbImgPath"].ToString() + "/" + uniqueFilename
                        });
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(uniqueFilenames));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


//Upload.upload({
//        url: "../../../../Handler/FileUpload.ashx",
//        method: "POST",
//        file: file,
//}).then(function(result)
//{
//    if (result.status == 200)
//    {
//        //get result in file name
//    }
//    else
//    {
//                    $scope.ExcelFile = '';
//    }
//});
