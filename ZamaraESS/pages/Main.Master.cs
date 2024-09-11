using iTextSharp.tool.xml.html.head;
using Microsoft.OData.Edm;
using System;
using System.Globalization;


namespace ZamaraESS.pages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            GetProfilePicFromNav(Session["username"].ToString());
            hidesidebar("/pages/Home.aspx");          

        }

        public void GetProfilePicFromNav(string username)
        {
            string ProfilePicBase64 = "", profilePic = "", signature = "";
            try
            {
                if (Session["Company"].ToString() == "ZAAC")
                {
                    ProfilePicBase64 = MyComponents.HrService.GetProfilePicture(username);
                    signature = MyComponents.HrService.GetSignature(username);
                }
                else
                {
                    ProfilePicBase64 = MyComponents.HrServiceZarib.GetProfilePicture(username);
                    signature = MyComponents.HrServiceZarib.GetSignature(username);
                }
                ImgProfilePic.ImageUrl = "data:image/png;base64," + ProfilePicBase64;
                logoutimage.ImageUrl = "data:image/png;base64," + ProfilePicBase64;
                if (ProfilePicBase64 == "")
                {                    
                    if (Session["gnder"].ToString()== "Male")
                    {
                        profilePic = "profile_m";
                    }
                    else
                    {
                        profilePic = "profile_f";
                    }
                    ImgProfilePic.ImageUrl = "~/images/" + profilePic + ".png";
                    logoutimage.ImageUrl = "~/images/" + profilePic + ".png";
                }


                string dateString = Session["Hod"].ToString();               

                if (DateTime.TryParseExact(dateString, "MM/dd/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    // Format the DateTime object to the desired format
                    string formattedDate = date.ToString("dd/MM/yyyy");
                    Hd.InnerText = formattedDate;
                }  
                
                pphone.InnerText=Session["pphone"].ToString();
                email.InnerText= Session["email"].ToString();                
                manager.InnerText= Session["manager"].ToString();
                empname.InnerText = Session["name"].ToString();               
                depart.InnerText = Session["depart"].ToString();
                title.InnerText = Session["title"].ToString();
                postion.InnerText = Session["postion"].ToString();
                company.InnerText = Session["Company"].ToString();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        protected void hidesidebar(string page)
        {
            // Get the current page URL
            string currentPage = Request.Url.AbsolutePath;
            
             // Check if the current page matches the provided page
            if (currentPage.Equals(page, StringComparison.OrdinalIgnoreCase))
            {
                 navbarNavDropdow.Visible = false;
            }

        }
        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/Default.aspx");
        }
    }
 }