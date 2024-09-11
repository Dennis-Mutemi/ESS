using Jint;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveCalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/Default.aspx");

            }
        }

        public string AppliedLeaves()
        {
            dynamic data = null;
            LeaveCalendar leave = new LeaveCalendar();
            DateTime currentDateTime = DateTime.Now;
            DateTime utcDateTime = currentDateTime.ToUniversalTime();

            // Convert to OData format
            string odataFormattedDate = utcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

            try
            {
                // Remove single quotes around the date in the OData query
                data = MyComponents.OdataService.GetLeaveApplications
                       .AddQueryOption("$filter", $"End_Date gt {odataFormattedDate} and Status eq 'Approved'")
                       .ToList();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while processing the OData request.", ex);
            }

            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }

    }
}