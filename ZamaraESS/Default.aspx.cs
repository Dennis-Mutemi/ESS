using System;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Web;
using System.Web.UI;

namespace ZamaraESS
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtusername.Focus();
        }

        protected void LbtnLogin_Click(object sender, EventArgs e)
        {
            string Company = ddlCompany.Value.ToString();
            if (string.IsNullOrEmpty(Company))
            {
                LblError.Text = "Please select your company";                
                ddlCompany.Focus();
                return;
            }
            else
            {
                Session["Company"] = Company;
            }
            string pass = txtpassword.Value.ToString();
            string user = txtusername.Value.ToString();
            if (string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(user))
            {
                LblError.Text = "Username or Password cannot be null!";
                return;
            }

            LoginStaffUsingAD();

            /*if (ValidStaffNo(user) == false)
            {
                LblError.Text = "Invalid Staff No";
                txtusername.Focus();
                return;
            }

            //Check Password Change Status
            if (ChangedPassStatus())
            {
                LoginForChangedPass();
            }
            else
            {
                LoginForUnchangedPass();
                //LblError.Text = "Your Password has been sent to "+TxtPass.Text+"";
            }*/

        }

        protected void LoginForChangedPass()
        {
            string pass = txtpassword.Value.ToString();
            string user = txtusername.Value.ToString();
            try
            {
                string staffLoginInfo = "";
                #region commented - using webservice
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.HrService.CheckStaffLogin(user, pass);
                }
                else
                {
                    staffLoginInfo = MyComponents.HrServiceZarib.CheckStaffLogin(user, pass);
                }

                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string returnMsg = "", changedPassword = "", staffNo = "", staffName = "", userID = "", Role = "", Department = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                    returnMsg = staffLoginInfo_arr[0];
                    changedPassword = staffLoginInfo_arr[1];
                    if (returnMsg == "SUCCESS")
                    {
                        staffNo = staffLoginInfo_arr[14];
                        staffName = staffLoginInfo_arr[3];
                        userID = staffLoginInfo_arr[4];
                        Role = staffLoginInfo_arr[5];
                        Department = staffLoginInfo_arr[6];

                        Session["username"] = staffNo;
                        Session["StaffName"] = staffName;
                        Session["userID"] = userID;
                        Session["Role"] = Role;
                        Session["Department"] = Department;
                        Response.Redirect("~/pages/Dashboard.aspx");
                    }
                    else
                    {
                        LblError.Text = returnMsg;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        protected void LoginForUnchangedPass()
        {
            string pass = txtpassword.Value.ToString();
            string user = txtusername.Value.ToString();
            try
            {
                #region commented - using webservice
                string staffLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.HrService.CheckStaffLoginForUnchangedPass(user, pass);
                }
                else
                {
                    staffLoginInfo = MyComponents.HrServiceZarib.CheckStaffLoginForUnchangedPass(user, pass);
                }

                //returnMsg::changedPassword::staffNo::staffUserID::staffName
                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string returnMsg = "", staffNo = "", email = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                    returnMsg = staffLoginInfo_arr[0];
                    if (returnMsg == "SUCCESS")
                    {
                        staffNo = staffLoginInfo_arr[14];
                        email = staffLoginInfo_arr[2];

                        Response.Redirect("ResetPassword.aspx?sd=" + staffNo + "&em=" + email);
                        return;
                    }
                    else
                    {
                        LblError.Text = returnMsg;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private bool ValidStaffNo(string staffNo)
        {

            bool r = false;
            try
            {
                #region commented - using webservice
                string staffPassChanged = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffPassChanged = MyComponents.HrService.CheckValidStaffNo(staffNo);
                }
                else
                {
                    staffPassChanged = MyComponents.HrServiceZarib.CheckValidStaffNo(staffNo);
                }

                if (!String.IsNullOrEmpty(staffPassChanged))
                {
                    string returnMsg = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffPassChanged.Split(strdelimiters, StringSplitOptions.None);

                    returnMsg = staffLoginInfo_arr[0];
                    if (returnMsg == "SUCCESS")
                    {
                        r = true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return r;
        }


        private void UpdatePass(string Password)
        {
            try
            {
                if (Session["Company"].ToString() == "ZAAC")
                {
                    MyComponents.HrService.UpdateStaffPass(txtusername.Value.ToString(), Password);
                }
                else
                {
                    MyComponents.HrServiceZarib.UpdateStaffPass(txtusername.Value.ToString(), Password);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
        private bool ChangedPassStatus()
        {
            string username = txtusername.Value.ToString().ToUpper();
            bool b = false;
            try
            {
                #region commented - using webservice
                string staffPassChanged = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffPassChanged = MyComponents.HrService.CheckStaffPasswordChanged(username);
                }
                else
                {
                    staffPassChanged = MyComponents.HrServiceZarib.CheckStaffPasswordChanged(username);
                }

                //returnMsg::changedPassword::staffNo::staffUserID::staffName
                if (!String.IsNullOrEmpty(staffPassChanged))
                {
                    string returnMsg = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffPassChanged.Split(strdelimiters, StringSplitOptions.None);

                    returnMsg = staffLoginInfo_arr[1];
                    if (returnMsg == "Yes")
                    {
                        b = true;
                    }
                }
                #endregion}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return b;
        }
        protected void lbtnForgot_Click(object sender, EventArgs e)
        {
            string user = txtusername.Value.ToString();
            if (string.IsNullOrEmpty(user))
            {
                LblError.Text = "Please enter your Username!";
                txtusername.Focus();
                return;
            }
            if (ValidStaffNo(user) == false)
            {
                LblError.Text = "Invalid Staff No";
                txtusername.Focus();
                return;
            }
            if (ChangedPassStatus() == false)
            {
                LblError.Text = "Warning! Your account is not active, please login with your initial password activate your account!";
                txtusername.Focus();
                return;
            }
            string email = GetStaffEmail(user);
            if (email.Length < 3)
            {
                LblError.Text = "Error! Please visit the HR office to update your official email.";
                return;
            }
            #region Reset Password
            try
            {
                #region commented - using webservice
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                string sRandomOTP = GenerateRandomOTP(8, saAllowedCharacters);
                DateTime ExpiryDateTime = DateTime.Now.AddHours(1);

                string userLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    userLoginInfo = MyComponents.HrService.GeneratePasswordResetOTP(user, sRandomOTP, ExpiryDateTime);
                }
                else
                {
                    userLoginInfo = MyComponents.HrServiceZarib.GeneratePasswordResetOTP(user, sRandomOTP, ExpiryDateTime);
                }

                if (!String.IsNullOrEmpty(userLoginInfo))
                {
                    string otp = "", expiryDateTime = "", firstName = "", resetLink = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] userLoginInfo_arr = userLoginInfo.Split(strdelimiters, StringSplitOptions.None);


                    otp = userLoginInfo_arr[0];
                    expiryDateTime = userLoginInfo_arr[1];
                    firstName = userLoginInfo_arr[2];

                    resetLink = "http://" + HttpContext.Current.Request.Url.Host + "/ESS/PasswordReset.aspx?k=" + otp + "&u=" + user;

                    if (MyComponents.SendEmail(
                                    txtusername.Value.Trim(),
                                    "Reset Portal Password",
                                    "Hello " + firstName + ",<br />" +
                                    "A request was received to reset your account password. If you did not initiate password reset, please contact Zamara service desk immediately at <b>ict@zamaragroup.com</b><br />" +
                                    "Click <a href='" + resetLink + "' target='_blank'>HERE</a> to reset your password. Please note that this link will expire at " + Convert.ToDateTime(expiryDateTime).ToString() + ". <br /><br />" +
                                    "<i>This is a system generate mail. Do not respond.</i>"))
                    {
                        txtusername.Value = null;
                        LblError.Text = "Password reset link has been sent to: " + user.ToLower();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            #endregion
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }
        private string GetStaffEmail(string staffNo)
        {
            string r = "";
            try
            {
                #region commented - using webservice
                string staffLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.HrService.GetStaffMail(staffNo);
                }
                else
                {
                    staffLoginInfo = MyComponents.HrServiceZarib.GetStaffMail(staffNo);
                }

                //returnMsg::changedPassword::staffNo::staffUserID::staffName
                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string email = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);
                    email = staffLoginInfo_arr[0];
                    r = email;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return r;
        }

        protected void LoginStaffUsingAD()
        {
            string user = txtusername.Value.ToString().ToLower() + "@zamara.co.ke";
            string user2 = @"ZAMARA\" + txtusername.Value.ToString().ToUpper();
            string pass = txtpassword.Value.ToString();
            //Boolean tru = true;
            if (AuthenticateAD(user, pass))
            //if (tru)
            {
                try
                {
                    #region commented - using webservice
                    string staffLoginInfo = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        staffLoginInfo = MyComponents.HrService.GetStaffProfileDetails(user2);
                    }
                    else
                    {
                        staffLoginInfo = MyComponents.HrServiceZarib.GetStaffProfileDetails(user2);
                    }

                    if (!String.IsNullOrEmpty(staffLoginInfo))
                    {
                        string returnMsg = "", changedPassword = "", staffNo = "", staffName = "", userID = "", Role = "", Department = "";
                        string[] strdelimiters = new string[] { "::" };
                        string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                        returnMsg = staffLoginInfo_arr[0];
                        changedPassword = staffLoginInfo_arr[1];
                        if (returnMsg != "")
                        {
                            staffNo = staffLoginInfo_arr[15];
                            staffName = staffLoginInfo_arr[3];
                            userID = staffLoginInfo_arr[9];
                            Role = staffLoginInfo_arr[5];
                            Department = staffLoginInfo_arr[6];

                            Session["username"] = staffNo;
                            Session["StaffName"] = staffName;
                            Session["userID"] = user2;
                            Session["Role"] = Role;
                            Session["Department"] = Department;
                            Response.Redirect("~/pages/Dashboard.aspx", false);
                        }
                        else
                        {
                            LblError.Text = returnMsg;
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
            }
            else
            {
                LblError.Text = "Invalid Username or Password.";
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        static string GetPublicIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
        public bool AuthenticateAD(string username, string password)
        {
            bool b = false;
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, "zamara.co.ke")) //192.168.1.251
                {
                    b = context.ValidateCredentials(username, password);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
            return b;
        }
    }
}