using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ZamaraESS.pages
{
    public partial class ConvertLeavePlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                ConvertPlan();
            }

        }
        protected void ConvertPlan()
        {
            try
            {
                string AppNo = Request.QueryString["appNo"].ToString();
                string Status = Request.QueryString["status"].ToString();

                if (Status == "Approved")
                {
                    string returnMsg = MyComponents.HrService.ConvertLeaveSchedule(AppNo);
                    if (returnMsg == "SUCCESS")
                    {
                        RedirectMessage("Leave Plan Converted Successfully.");
                    }
                    else
                    {
                        SuccessMessage(returnMsg);
                    }
                }
                else
                {
                    SuccessMessage("You can only convert approved leave plan.");
                }
            }
            catch (Exception ex)
            {
                SuccessMessage("ERROR: " + ex.Message.ToString());
            }
            

        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(),"ClientScript", strScript.ToString());
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "LeaveSchedules.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(),"ClientScript", strScript.ToString());
        }
        public void RedirectMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "LeaveListing.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(),"ClientScript", strScript.ToString());
        }
    }
}