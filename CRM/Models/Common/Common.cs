using CRM_Repository.DataServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CRM
{
    public class Common
    {
        dalc odal = new dalc();
        public string fileupload(HttpPostedFileBase fu, string foldername, int width, int height)
        {
            string filename = "";
            if (fu.FileName != "")
            {
                //string filename1 = Path.GetFileNameWithoutExtension(fu.FileName);
                string ext = Path.GetExtension(fu.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".JPG" || ext == ".JPEG" || ext == ".PNG" || ext == ".GIF")
                {
                    Stream strm = fu.InputStream;
                    // fu.SaveAs(HttpContext.Current.Server.MapPath("~/ProductImages" + filename)); 
                    filename = Guid.NewGuid() + Path.GetExtension(fu.FileName);
                    string filenamewithpath = "~/" + foldername + "/" + filename;
                    GenerateThumbnails(strm, filenamewithpath, width, height);
                }

            }
            return filename;
        }
        public string fileDocupload(HttpPostedFileBase fu, int Filename, string foldername)
        {
            string filename = "";
            if (fu.FileName != "")
            {

                //string filename1 = Path.GetFileNameWithoutExtension(fu.FileName);
                string ext = Path.GetExtension(fu.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".JPG" || ext == ".JPEG" || ext == ".PNG" || ext == ".GIF" || ext == ".pdf" || ext == ".doc" || ext == ".PDF" || ext == ".DOC")
                {
                    Stream strm = fu.InputStream;
                    // fu.SaveAs(HttpContext.Current.Server.MapPath("~/ProductImages" + filename)); 

                    if (ext != ".pdf" && ext != ".doc" && ext != ".PDF" && ext != ".DOC")
                    {
                        filename = Convert.ToString(Filename) + ".jpg";
                        string filenamewithpath = "~/" + foldername + "/" + filename;
                        string filenamewith = "~/" + foldername + "/" + Filename + ".pdf";
                        var image = System.Drawing.Image.FromStream(strm);
                        var thumbnailImg = new Bitmap(image);
                        var thumbGraph = Graphics.FromImage(thumbnailImg);
                        if (File.Exists(HttpContext.Current.Server.MapPath(Convert.ToString(filenamewithpath))) || File.Exists(HttpContext.Current.Server.MapPath(Convert.ToString(filenamewith))))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(filenamewithpath));
                        }
                        thumbnailImg.Save(HttpContext.Current.Server.MapPath(filenamewithpath), image.RawFormat);
                    }
                    else
                    {
                        filename = Convert.ToString(Filename) + ".pdf";
                        string filenamewithpath = "~/" + foldername + "/" + filename;
                        var thumbnailDoc = filenamewithpath;
                        if (File.Exists(HttpContext.Current.Server.MapPath(filenamewithpath)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(filenamewithpath));
                        }

                        fu.SaveAs(HttpContext.Current.Server.MapPath(thumbnailDoc));
                        //thumbnailDoc.(HttpContext.Current.Server.MapPath(filenamewithpath));
                    }


                    //GenerateThumbnails(strm, filenamewithpath, width, height);
                }

            }
            return filename;
        }
        public static void GenerateThumbnails(Stream sourcePath, string targetPath, int width, int height)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                var newWidth = width;
                var newHeight = height;
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(HttpContext.Current.Server.MapPath(targetPath), image.RawFormat);
            }
        }
        public DataTable GetAutoNumber(string type, string code = "", int codeId = 0)
        {
            try
            {
                return odal.selectbyquerydt("select [gurjari_crmuser].[GetAutoNumber]('" + type + "','" + code + "','" + codeId + "')");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void sendmail(string toEmail, string strbody, string subject, string fromEmail, string password, string signature = null)
        {

            try
            {
                using (var client = new SmtpClient())
                {
                    EncryptDecrypt _Encdcrpt = new EncryptDecrypt();
                    client.Host = "smtp.zoho.com";
                    client.EnableSsl = true;
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(fromEmail, _Encdcrpt.Decrypt(password));
                    System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                    System.Net.Mail.MailAddress fromAdd = new System.Net.Mail.MailAddress(fromEmail);
                    mailMsg.From = fromAdd;

                    mailMsg.To.Add(toEmail);

                    mailMsg.IsBodyHtml = true;
                    mailMsg.Priority = MailPriority.High;

                    mailMsg.Subject = subject;

                    string mailbody = strbody.ToString() + "      " + signature;
                    mailMsg.Body = mailbody.Trim();
                    client.Send(mailMsg);
                }
                //System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                //System.Net.Mail.MailAddress fromAdd = new System.Net.Mail.MailAddress("contact@gurjarimall.com");
                //mailMsg.From = fromAdd;

                //mailMsg.To.Add(email);

                //mailMsg.IsBodyHtml = true;
                //mailMsg.Priority = MailPriority.High;

                //mailMsg.Subject = subject;

                //string mailbody = strbody.ToString();
                //mailMsg.Body = mailbody.Trim();
                //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                //smtp.Port = 587;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //smtp.Send(mailMsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
        public void sendsms(string mobileno, string msg)
        {
            string createdURL = "http://sms.raininfotech.in/AppSendSMS?Username=GURJARI&Password=gurjari&SenderId=GURLTD&MobileNo=" + mobileno + "&Message=" + msg;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(createdURL);
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
            }
            catch (Exception generatedExceptionName)
            {
                Console.WriteLine("{0} Exception caught.", generatedExceptionName);
            }
        }
        public string VCardFile(VCardData obj)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-disposition", string.Format("attachment; filename=\"{0}\";", "MyContact.VCF"));
                var str = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/ReportFormat/MyContact.VCF"));
                str = str.Replace("@@Name@@", obj.Name);
                str = str.Replace("@@number@@", obj.Mobileno);
                str = str.Replace("@@Email@@", obj.Email);
                str = str.Replace("@@Address@@", obj.Address);
                str = str.Replace("@@Company@@", obj.Company);
                str = str.Replace("@@ComPhone@@", obj.ComPhone);
                str = str.Replace("@@Website@@", obj.Website);
                str = str.Replace("@@ComAddress@@", obj.ComAddress);
                str = str.Replace("@@city@@", obj.city);
                str = str.Replace("@@state@@", obj.state);
                str = str.Replace("@@country@@", obj.country);
                str = str.Replace("@@Pincode@@", obj.Pincode);
                str = str.Replace("@@Telephone@@", obj.Telephone);

                return str;
            }
            catch (Exception generatedExceptionName)
            {
                Console.WriteLine("{0} Exception caught.", generatedExceptionName);
            }
            return null;
        }
        //public void sendmail(string email, string strbody, string subject)
        //{
        //    try
        //    {

        //        strbody += "<br /><br />If You Dont want any email / Unsubscribe from pipcoin than click on ";
        //        strbody = HttpContext.Current.Server.HtmlEncode(strbody);
        //        var request = (HttpWebRequest)WebRequest.Create("http://trans.mailingservice.in/Email/API/SendEmailXml.aspx");

        //        var postData = @"<apiinfo><api_user>7405249551</api_user><api_key>CBCDE</api_key><from>noreply@mypipcoins.com</from><fromname>My CRM</fromname><replyto>noreply@mypipcoins.com</replyto><to>" + email + "</to><subject>" + subject + "</subject><body><html><body>" + strbody + "</body></html></body><spamlinkrequired>false</spamlinkrequired><unsubscribelinkrequired>false</unsubscribelinkrequired><scheduletime></scheduletime></apiinfo>";
        //        var data = System.Text.Encoding.ASCII.GetBytes(postData);

        //        request.Method = "POST";
        //        request.ContentType = "text/xml; charset=utf-8";
        //        request.ContentLength = data.Length;

        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }

        //        var response = (HttpWebResponse)request.GetResponse();

        //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        //    }
        //    catch (Exception ex)
        //    {
        //        // lblmesg.Text = ex.ToString();
        //    }
        //}
    }
    public class VCardData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Mobileno { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string ComPhone { get; set; }
        public string ComAddress { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string Pincode { get; set; }
        public string Telephone { get; set; }

    }

    public static class CommonFunctions
    {

        public static int GetProperInt(this string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal GetProperDecimal(this string str)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static DateTime? GetProperDate(this string str)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}