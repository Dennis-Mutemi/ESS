using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveBalances : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (Session["Role"].ToString() == "Mgt./Supervisor")
                {
                    SuperviseesBalances.Visible = true;
                }
                else
                {
                    SuperviseesBalances.Visible = false;
                }
                GenerateLeaveBalances();
            }
        }

        protected void GenerateLeaveBalances()
        {
            try
            {
                string Base64Report = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    Base64Report = MyComponents.HrService.GenerateLeaveBalance(Session["username"].ToString());
                }
                else
                {
                    Base64Report=MyComponents.HrServiceZarib.GenerateLeaveBalance(Session["username"].ToString());
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
                exception.Data.Clear();
                HttpContext.Current.Response.Write(exception);
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