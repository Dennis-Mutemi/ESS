using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using ZamaraESS.FinService;
using ZamaraESS.ProcService;
using BC21;
using ZamaraESS.HrService;
using System.Web.Configuration;
using ZamaraESS.HrServiceZarib;
using ZamaraESS.ProcServiceZarib;
using ZamaraESS.FinServiceZarib;
using static System.Net.WebRequestMethods;


namespace ZamaraESS
{
    public class MyComponents : Page
    {

        public static string Company_Name = ConfigurationManager.AppSettings["COMPANY"];
        public static Boolean SendEmail(string address, string subject, string message)
        {
            Boolean x = false;

            try
            {
                string email = ConfigurationManager.AppSettings["EMAIL"];
                string password = ConfigurationManager.AppSettings["PASSWORD"];
                string smtp = ConfigurationManager.AppSettings["SMTP"];
                string port = ConfigurationManager.AppSettings["PORT"];

                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient(smtp, Convert.ToInt32(port));

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);

                x = true;

            }
            catch (Exception Ex)
            {
                Ex.Data.Clear();
            }
            return x;
        }
        public static string FormatDateWithoutSymbols(DateTime Date2Format)
        {
            string s = "";

            try
            {
                string y, m, d, hr, mn, sc;

                y = Date2Format.Year.ToString();

                m = Date2Format.Month.ToString();
                if (m.Length == 1) m = "0" + m;

                d = Date2Format.Day.ToString();
                if (d.Length == 1) d = "0" + d;

                hr = Date2Format.Hour.ToString();
                if (hr.Length == 1) hr = "0" + hr;

                mn = Date2Format.Minute.ToString();
                if (mn.Length == 1) mn = "0" + mn;

                sc = Date2Format.Second.ToString();
                if (sc.Length == 1) sc = "0" + sc;

                s = y + m + d + " " + hr + mn + sc;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return s;
        }

        public static string BaseSiteUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }
        public static Control FindControlRecursive(Control Root, string Id)
        {

            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);

                if (FoundCtl != null)

                    return FoundCtl;
            }
            return null;
        }
        public static HrService.hrservice HrService
        {
            get
            {
                var ws = new HrService.hrservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }

        public static HrServiceZarib.hrservice HrServiceZarib
        {
            get
            {
                var ws = new HrServiceZarib.hrservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }
        public static FinService.finservice FinService
        {
            get
            {
                var ws = new FinService.finservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }
        public static FinServiceZarib.finservice FinServiceZarib
        {
            get
            {
                var ws = new FinServiceZarib.finservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }
        public static ProcService.procservice ProcService
        {
            get
            {
                var ws = new ProcService.procservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }

        public static ProcServiceZarib.procservice ProcServiceZarib
        {
            get
            {
                var ws = new ProcServiceZarib.procservice();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                        ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);

                    //ws.UseDefaultCredentials = true;
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }
        public static NAV OdataService

        {
            get
            {
                var serviceRoot = "";
                if (HttpContext.Current.Session["Company"].ToString() == "ZAAC")
                {
                    serviceRoot = "http://192.168.111.86:7113/ZamaraESS/ODataV4/Company('ZAAC')";
                }
                else
                {
                    serviceRoot = "http://192.168.111.86:7113/ZamaraESS/ODataV4/Company('ZARIB')";
                }

                var context = new NAV(new Uri(serviceRoot));

                try
                {
                    context.BuildingRequest += Context_BuildingRequest;
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return context;
            }
        }
        private static void Context_BuildingRequest(object sender, Microsoft.OData.Client.BuildingRequestEventArgs e)
        {
            //e.RequestUri = new Uri(e.RequestUri.ToString().Replace("V4/", "V4/Company('My%20Company')/"));
            e.Headers.Add("Authorization", "Basic WmFtYXJhXFpTVkNfTkFWSVNJT046S0NCITM2NWM=");
        }
        public static bool ValidNumber(string numberToValidate)
        {
            bool b = false;
            try
            {
                numberToValidate = ValidateNumber(numberToValidate);

                if (numberToValidate.Length > 0)
                {
                    //throw exception if not double number.
                    double d = Convert.ToDouble(numberToValidate);

                    //success/valid double number
                    b = true;
                }
            }
            catch (Exception ex)
            {
                //cSite.SendErrorToDeveloper(ex);
                ex.Data.Clear();
            }
            return b;
        }

        public static string ValidateNumber(string Entry)
        {
            string r = Entry;

            try
            {
                Entry = ValidateEntry(Entry);

                string s = ",()";//sql illegal entry characters

                Entry = Entry.Trim();

                char[] c = s.ToCharArray();

                for (int i = 0; i < c.Length; i++)
                {
                    Entry = Entry.Replace(c[i].ToString(), "");
                }
                r = Entry;
            }
            catch (Exception)
            {
                throw;
            }
            return r;
        }
        public static string ValidateEntry(string Entry)
        {
            string r = Entry;
            try
            {
                if (Entry.Length > 250) Entry = Entry.Substring(0, 250);

                string s = "'";//sql illegal entry characters

                Entry = Entry.Trim();//remove spaces

                char[] c = s.ToCharArray();

                for (int i = 0; i < c.Length; i++)
                    if (Entry.Contains(c[i].ToString()))
                    {
                        //Entry = Entry.Replace(c[i].ToString(), "" );//blank
                        Entry = Entry.Replace(c[i].ToString(), "\'" + c[i].ToString());//escape character
                    }

                s = "--";//sql illegal entry characters

                if (Entry.Contains(s))
                    Entry = Entry.Replace(s, "");//blank

                r = Entry;
            }
            catch (Exception)
            {
                throw;
            }
            return r;
        }
        public static bool IsNumeric(string no)
        {
            double result;
            if (double.TryParse(no, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
