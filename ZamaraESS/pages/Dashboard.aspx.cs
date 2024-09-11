using System;

namespace ZamaraESS.pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                GetStaffDetailsService(Session["userID"].ToString());
                GetSignatureFromNav(Session["username"].ToString());
            }

            //lblUser.Text = Session["StaffName"].ToString();            
        }
        public void GetSignatureFromNav(string username)
        {
            string signature = "";
            try
            {
                if (Session["Company"].ToString() == "ZAAC")
                {
                    signature = MyComponents.HrService.GetSignature(username);
                    ProfileImage.ImageUrl = "data:image/png;base64," + signature;
                }
                else
                {
                    signature = MyComponents.HrServiceZarib.GetSignature(username);
                    ProfileImage.ImageUrl = "data:image/png;base64," + signature;
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }      
        
        protected void GetStaffDetailsService(string username)
        {
            try
            {
                string[] strdelimiters = new string[] { "::" };
                string staffLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.HrService.GetStaffProfileDetails(username);
                }
                else
                {
                    staffLoginInfo = MyComponents.HrServiceZarib.GetStaffProfileDetails(username);
                }

                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string idNum = "", Citizen = "", adddss = "", JobTitle = "", email = "", DoB = "", gender = "", phneNo = "", EmpTitle = "", UserID = "", name = "",empNo="",Dot="",Manager="";

                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                    idNum = staffLoginInfo_arr[0];
                    Citizen = staffLoginInfo_arr[1];
                    adddss = staffLoginInfo_arr[2];
                    JobTitle = staffLoginInfo_arr[3];
                    email = staffLoginInfo_arr[4];
                    EmpTitle = staffLoginInfo_arr[5];
                    DoB = staffLoginInfo_arr[6];
                    gender = staffLoginInfo_arr[7];
                    phneNo = staffLoginInfo_arr[8];
                    UserID = staffLoginInfo_arr[9];
                    name = staffLoginInfo_arr[10];
                    empNo= staffLoginInfo_arr[15];                    
                    Manager = staffLoginInfo_arr[16];


                    LblEmployeeNo.Text = Session["username"].ToString();
                    Session["UserID"] = UserID;
                    LblIDNo.Text = idNum.ToString();
                    LblCitizenship.Text = Citizen.ToString();
                    lblEmail.Text = email.ToString();
                    lblPhoneNo.Text = phneNo.ToString();
                    txtdob.Text = DoB.ToString();
                    Session["manager"]=Manager.ToString();
                    Session["Hod"]= staffLoginInfo_arr[11].ToString();
                    Session["pphone"]=phneNo.ToString();
                    Session["email"]=email.ToString();
                    Session["name"] = name.ToString();
                    Session["gnder"] = gender.ToString();
                    Session["title"] = staffLoginInfo_arr[17];
                    Session["postion"] = staffLoginInfo_arr[14];
                    Session["depart"] = staffLoginInfo_arr[12];
                    if (gender == "Female")
                    {
                        Session["Gender"] = 1;
                    }
                    else if (gender == "Male")
                    {
                        Session["Gender"] = 2;
                    }
                    else
                    {
                        Session["Gender"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string fileName = "", EmpNo = Session["username"].ToString(), base64String = "";

            try
            {
                if (IsPostBack && ProfileImageUpload.PostedFile != null)
                {
                    if (ProfileImageUpload.PostedFile.FileName.Length > 0)
                    {
                        System.IO.Stream fs = ProfileImageUpload.PostedFile.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        fileName = ProfileImageUpload.FileName;

                    }
                }
                //Save File Name to table
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                   //rtnMsg = MyComponents.HrService.updateEmployeedetails(EmpNo, lblPhoneNo.Text, lblEmail.Text, LblIDNo.Text, base64String, fileName);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.updateEmployeedetails(EmpNo, lblPhoneNo.Text, lblEmail.Text, LblIDNo.Text, base64String, fileName);
                }
                   
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    string signature = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        signature = MyComponents.HrService.GetSignature(EmpNo);
                    }
                    else
                    {
                        signature = MyComponents.HrServiceZarib.GetSignature(EmpNo);
                    }
                    ProfileImage.ImageUrl = "data:image/png;base64," + signature;
                    Message(rtnMsg);
                }
                else
                {
                    Message(rtnMsg);
                }
            }
            catch (Exception Ex)
            {

                Message("ERROR: " + Ex.Message.ToString());
            }
        }
    }
}
