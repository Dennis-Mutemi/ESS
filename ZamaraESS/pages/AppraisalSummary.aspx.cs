using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class AppraisalSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                Loadperiods();

            }
        }

        protected void period_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {            
                String Appraisalperiod = period.SelectedValue;
                string Base64Report = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                   Base64Report = MyComponents.HrService.GenerateReports(6, Session["username"].ToString(), Appraisalperiod);

                }
                else
                {
                    Base64Report = MyComponents.HrServiceZarib.GenerateReports(6, Session["username"].ToString(), Appraisalperiod);
                }                  

                if (Base64Report.Length > 0)
                {
                    string Content = "data:application/pdf;base64," + Base64Report;
                    myPDF.Attributes.Add("src", Content);
                }
                else
                {
                    Message("There is no report within the filter.");
                    myPDF.Attributes.Add("src", null);
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message);
            }
        }

        protected void Loadperiods()
        {
            try
            {
                this.period.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Appraisal Period--", "");
                this.period.Items.Add(li);
                var data = MyComponents.OdataService.GetAppraisalPeriods.Execute();

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.Code))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.Code
                              );
                    this.period.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {

                ex.Data.Clear();
            }
        }

        private void Message(string p)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + p + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
    }
}