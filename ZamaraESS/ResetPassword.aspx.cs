﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNewPass.Focus();
        }

        protected void LbtnLogin_Click(object sender, EventArgs e)
        {
            string username = Request.QueryString["sd"].ToString();
            string email = Request.QueryString["em"].ToString();
            string NewPass = txtNewPass.Text.ToString();
            string ConfirmNewPass = txtConfirmpassword.Text.ToString();

            if (string.IsNullOrEmpty(NewPass) || string.IsNullOrEmpty(ConfirmNewPass))
            {
                Message("Passwords cannot be null");
                return;
            }

            if (NewPass != ConfirmNewPass)
            {
                Message("Your new passwords do not match!");
                return;
            }
            // Sp Reset password
            try
            {

                if (ValidatePassword(NewPass) == false)
                {
                    Message("Password must be at least 8 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit.");
                }
                else
                {
                    string staffLoginInfo = MyComponents.HrService.ChangeStaffPassword(username, NewPass);
                    if (!String.IsNullOrEmpty(staffLoginInfo))
                    {
                        string returnMsg = "";
                        string[] strdelimiters = new string[] { "::" };
                        string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                        returnMsg = staffLoginInfo_arr[0];
                        if (returnMsg == "SUCCESS")
                        {
                            try
                            {
                                string body = "Your Portal Password has been successfully reset, Use the below password to login<br /><br />";
                                body += "<b>Password: " + NewPass + " </b> <br /><br />";
                                if (MyComponents.SendEmail(email, "Portal Password", body))
                                {
                                    SuccessMessage("Password reset successfuly! A copy of the password has also been sent to your email; " + email.ToString() + " for reference.");
                                }
                                else
                                {
                                    Message("Error, failed to send email. Please Contact System Administrator!");
                                }
                            }
                            catch (Exception ex)
                            {
                                Message("ERROR: " + ex.Message.ToString());
                            }

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
                ex.Data.Clear();
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