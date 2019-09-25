using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;



namespace ConfWebAccess.Models
{
    public static class Utility
    {
       
      
        public static int GetQuarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        public static DateTime GetStartDatefromQuarter(int year, int quarter)
        {
            return new DateTime(year, quarter * 3 - 2, 1);
        }

        public static DateTime GetEndDatefromQuarter(int year, int quarter)
        {
            var startdate = new DateTime(year, quarter * 3 - 2, 1);
            return startdate.AddMonths(3).AddDays(-1);
        }

        public static string GetUserType()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string userid = HttpContext.Current.User.Identity.GetUserId();
                DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();
                var s = context.AspNetUsers.Find(userid);
                return s.SysUserType.TypeName;

            }
            return "error";
        }

        public static string SendEmail50(IEnumerable<AspNetUser> users, string subject, string body, bool BCCOnly, bool isHTML, string path)
        {

            try
            {

                DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();
                var emailinfo = context.SysSettings.Where(x => x.Item == "EmailInfo").FirstOrDefault();

                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient(emailinfo.Value1);


                if (users.Count() == 0) return "Fail: No Users";


                m.From = new MailAddress(emailinfo.Value2);


                foreach (var user in users.Distinct())
                {
                    if (BCCOnly)
                        m.Bcc.Add(user.Email);
                    else
                        m.To.Add(user.Email);
                }


                m.Subject = subject;


                m.Body = body;

                //if (path != "") m.Attachments.Add(new Attachment(path));


                if (path != "") m.Body = body + "<br><a href='http://www.ptpdms.com/Temp/" + path + "'>Click here for attachment</a>";

                sc.Port = int.Parse(emailinfo.Value5);
                sc.Credentials = new System.Net.NetworkCredential(emailinfo.Value3, emailinfo.Value4);
                // sc.EnableSsl = true;

                m.IsBodyHtml = isHTML;

                sc.Send(m);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }

        public static string SendEmail(IEnumerable<AspNetUser> users, string subject, string body, bool BCCOnly, bool IsHTML, string path)
        {
            string rtn = "";



            if (users.Count() >= 50)
            {

                int cnt = users.Count() / 50;
                for (int i = 0; i <= cnt; i++)
                {
                    var fiveitems = users.Skip(i * 50).Take(50);
                    rtn = SendEmail50(fiveitems, subject, body, BCCOnly, IsHTML, path);
                    if (rtn != "Success") return rtn;
                }
            }
            else
            {
                rtn = SendEmail50(users, subject, body, BCCOnly, IsHTML, path);
            }


            return rtn;
        }

        public static string SendEmail(string emailaddress, string subject, string body, bool ishtml, string path)
        {

            try
            {

                DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();
                var emailinfo = context.SysSettings.Where(x => x.Item == "EmailInfo").FirstOrDefault();

                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient(emailinfo.Value1);


                m.From = new MailAddress(emailinfo.Value2);
                m.To.Add(emailaddress);

                m.Subject = subject;

                m.Body = body;



                // if (path != "") m.Attachments.Add(new Attachment(path));

                if (path != "") m.Body = body + "<br><a href='http://www.ptpdms.com/Temp/" + path + "'>Click here for attachment</a>";

                sc.Port = int.Parse(emailinfo.Value5);
                sc.Credentials = new System.Net.NetworkCredential(emailinfo.Value3, emailinfo.Value4);
                // sc.EnableSsl = true;

                m.IsBodyHtml = ishtml;

                sc.Send(m);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }



  

        public static bool UploadSupportFile(HttpPostedFileBase upload, int supportid)
        {
            Random rand = new Random();
            DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();

            var importfile = new SysFilePath
            {
                FileName = "Support_" + rand.Next(1000, 9999).ToString() + "_" + System.IO.Path.GetFileName(upload.FileName),
                LinkId = supportid,
                FileType = "Support File",
                UploadDate = DateTime.Now
            };

            context.SysFilePaths.Add(importfile);
            context.SaveChanges();

            string namefromdb = "~/Temp/" + importfile.FileName;

            string path = HttpContext.Current.Server.MapPath(namefromdb);

            upload.SaveAs(path);

            return true;

        }

        public static bool UploadPowerPoint(HttpPostedFileBase upload, int abstractid)
        {
            Random rand = new Random();
            DB_A0B2A3_conferenceEntities context = new DB_A0B2A3_conferenceEntities();

            var importfile = new SysFilePath
            {
                FileName = "PPFile_" + rand.Next(1000, 9999).ToString() + "_" + System.IO.Path.GetFileName(upload.FileName),
                LinkId = abstractid,
                FileType = "PowerPoint File",
                UploadDate = DateTime.Now
            };

            context.SysFilePaths.Add(importfile);
            context.SaveChanges();

            string namefromdb = "~/Temp/" + importfile.FileName;

            string path = HttpContext.Current.Server.MapPath(namefromdb);

            upload.SaveAs(path);

            return true;

        }



    }

    public class ListPair
    {
        public int Id { get; set; }
        public string Ident { get; set; }
        public string Name { get; set; }
        public string ExtendedName
        {
            get
            {
                return string.Format("{0} \t| {1}", Ident, Name);

            }

        }

        public string Description
        {
            get
            {
                return string.Format("{0} \t| {1}", Ident, Name);

            }

        }
    }
}