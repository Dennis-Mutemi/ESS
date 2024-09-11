using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveStatements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                GenerateLeaveStatement();
            }
        }
        protected void GenerateLeaveStatement()
        {
            try
            {
                string Base64Report = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    Base64Report = MyComponents.HrService.GenerateLeaveStatement(Session["username"].ToString());
                }
                else
                {
                    Base64Report = MyComponents.HrServiceZarib.GenerateLeaveStatement(Session["username"].ToString());
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