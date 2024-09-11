using iTextSharp.text;
using Jint;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                getApprovalRequest();
            }
            
        }



        public string Birthdaywishs()
        {
            StringBuilder birthdayBuilder = new StringBuilder();
            try
            {
                var data = MyComponents.OdataService.GetEmployees.AddQueryOption("$filter", "EmployeeStatus eq 'active'").ToList();
               
                    foreach (var empdata in data)
                    {
                        if (IsBirthday((DateTime)empdata.BirthDate))
                        {
                            string ProfilePicBase64 = "", profilePic = "";

                            ProfilePicBase64 = MyComponents.HrService.GetProfilePicture(empdata.No);

                            if (ProfilePicBase64 == "")
                            {
                                if (Session["gnder"].ToString() == "Male")
                                {
                                    profilePic = "profile_m";
                                }
                                else
                                {
                                    profilePic = "profile_f";
                                }
                                ProfilePicBase64 = "~/images/" + profilePic + ".png";

                            }
                            else
                            {
                                ProfilePicBase64 = "data:image/png;base64," + ProfilePicBase64;
                            }
                            birthdayBuilder.Append(
                                $@"<div class=""employee-card shadow-sm"">
                            <img src=""{ProfilePicBase64}"" alt=""Employee Image"">
                            <div class=""font-weight-bold"">                                
                                <p class=""birthday-message""> Happy Birthday, {empdata.FullName}! Wishing you a fantastic day filled with joy and happiness.</p>
                            </div>
                        </div>");

                        }

                    }                

                    if (birthdayBuilder.Length <=0)
                    {
                        birthdayBuilder.Append(
                                 $@"<div class=""employee-card shadow-sm"">                            
                                <div class=""font-weight-bold"">                                
                                    <p class=""birthday-message""> No available celebrations</p>
                                </div>
                            </div>");
                    }                

            }
            catch (Exception ex)
            {

                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
            string birthdays = birthdayBuilder.ToString();
            return birthdays;
        }

            public bool IsBirthday(DateTime birthdate)
            {
                DateTime today = DateTime.Today;
                return today.Month == birthdate.Month && today.Day == birthdate.Day;
            }


            public void getApprovalRequest()
            {

                 string userId= Session["UserID"].ToString();
                try
                {
                    var data = MyComponents.OdataService.GetApprovalEntries.AddQueryOption("$filter", "ApproverID eq '" + userId + "'and Status eq 'Open'").ToList();

                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string json = serializer.Serialize(data);
                    JArray jsonArray = JArray.Parse(json);
                    DataTable dataTable = ConvertJArrayToDataTable(jsonArray);

                    // Bind DataTable to GridView
                    gvAttachmentLines.DataSource = dataTable;
                    gvAttachmentLines.DataBind();

                }
                catch (Exception ex)
                {
                    Message("ERROR: " + ex.Message.ToString());
                    ex.Data.Clear();
            }
            }
            public dynamic getLeaveBalances()
            {
                Dictionary<string, Decimal> Leavebalances = new Dictionary<string, Decimal>();
                try
                {
                    var leavebalance = MyComponents.OdataService.GetLeaveBalances.AddQueryOption("$filter", "Employee_No_ eq '" + Session["username"].ToString() + "'");
                    var leavetypes = MyComponents.OdataService.GetLeaveTypes.AddQueryOption("$filter", "Gender eq '" + Session["gnder"].ToString() + "'");
                    HashSet<string> alreadyEncountered = new HashSet<string>();
                    Decimal leavebal = 0;
                    foreach (var ym in leavetypes)
                    {
                        leavebal = 0;
                        if (!alreadyEncountered.Contains(ym.Code))
                        {
                            foreach (var bal in leavebalance)
                            {
                                if (bal.Leave_Type == ym.Code)
                                {
                                    leavebal = leavebal + Convert.ToDecimal(bal.No__of_Days);
                                }
                            }
                            Leavebalances.Add(ym.Description, leavebal);
                        }

                        alreadyEncountered.Add(ym.Code);
                    }
                }
                catch (Exception exception)

                {
                    Message("ERROR: " + exception.Message.ToString());
                    exception.Data.Clear();
                }
                return Leavebalances;
            }



            private DataTable ConvertJArrayToDataTable(JArray jsonArray)
            {
                DataTable dataTable = new DataTable();

                if (jsonArray.Count > 0)
                {
                    // Create columns
                    foreach (JProperty prop in ((JObject)jsonArray[0]).Properties())
                    {
                        dataTable.Columns.Add(prop.Name, typeof(string));
                    }

                    // Create rows
                    foreach (JObject obj in jsonArray)
                    {
                        DataRow row = dataTable.NewRow();
                        foreach (JProperty prop in obj.Properties())
                        {
                            row[prop.Name] = prop.Value.ToString();
                        }
                        dataTable.Rows.Add(row);
                    }
                }

                return dataTable;
            }

            protected void approve_Click(object sender, EventArgs e)
            {
                string[] arg = new string[2];
                arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
                string EntryNo = arg[0];
                try
            {
                String Rtms = "";//MyComponents.HrService.ApproveRequest(Convert.ToInt32(EntryNo), Session["UserID"].ToString());
                    if (Rtms != "")
                    {
                        Message(Rtms);
                    }
                    else
                    {
                        Message("Failed to reject the request");
                    }
                }
                catch (Exception Ex)
                {

                    Message("Error:" + Ex.Message);
                }
            }
            protected void reject_Click(object sender, EventArgs e)
            {
                string[] arg = new string[2];
                arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
                string EntryNo = arg[0];
                try
                {
                String Rtms = "";//MyComponents.HrService.RejectRequest(Convert.ToInt32(EntryNo), Session["UserID"].ToString());
                    if (Rtms != "")
                    {
                        Message(Rtms);
                    }
                    else
                    {
                        Message("Failed to reject the request");
                    }
                }
                catch (Exception Ex)
                {
                    Message("Error:" + Ex.Message);
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
        }
 } 
