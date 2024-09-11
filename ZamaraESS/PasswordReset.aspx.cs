using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZamaraESS
{
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.QueryString.AllKeys.Contains("k") && !Request.QueryString.AllKeys.Contains("u"))
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            txtNewPass.Focus();
        }

        protected void LbtnLogin_Click(object sender, EventArgs e)
        {
            string otp = Request.QueryString["k"].ToString();
            string username = Request.QueryString["u"].ToString();
            string NewPass = txtNewPass.Text.ToString();
            string ConfirmNewPass = txtConfirmpassword.Text.ToString();

            if (string.IsNullOrEmpty(NewPass))
            {
                LblError.Text = "Please Enter New Password.";
                txtNewPass.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ConfirmNewPass))
            {
                LblError.Text = "Plesae Enter Confirm Password.";
                txtConfirmpassword.Focus();
                return;
            }

            if (NewPass != ConfirmNewPass)
            {
                LblError.Text = "Your new passwords do not match!";
                txtNewPass.Focus();
                return;
            }

            try
            {

                if (ValidatePassword(NewPass) == false)
                {
                    LblError.Text = "Password must be at least 8 characters and must include at least one upper case letter, one lower case letter and one numeric digit.";
                }
                else
                {
                    string returnMsg = MyComponents.HrService.ResetPasswordWithOTP(username, otp, NewPass);
                    if (!String.IsNullOrEmpty(returnMsg))
                    {
                        if (returnMsg == "SUCCESS")
                        {
                            Session.Abandon();
                            Session.Clear();
                            Session.Remove("username");
                            Session.RemoveAll();
                            SuccessMessage("Password reset successfuly.");
                        }
                        else
                        {
                            LblError.Text = returnMsg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LblError.Text = ex.Message;
            }
        }
        public bool ValidatePassword(string password)
        {
            bool r = false;
            string patternPassword = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$";
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (Regex.IsMatch(password, patternPassword))
                    {
                        r = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
            return r;
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "Default.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(),"ClientScript", strScript.ToString());
        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(),"ClientScript", strScript.ToString());
        }
    }
}