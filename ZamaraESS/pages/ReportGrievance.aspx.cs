using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class ReportGrievance : System.Web.UI.Page
    {
        public static int TableID = 60472;
        public static int TableAP = 60473;
        public static string StaffName = "";
        public static string StaffUserId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string RecStatus = Request.QueryString["status"].ToString();
            string paged = Request.QueryString["Tp"].ToString();

            if (!IsPostBack)
            {
                LoadEmployees();
                LoadAggrievedParties();
                LoadResolution();
                LoadGrievanceType();

                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paged == "old")
                {
                    if (RecStatus == "New")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewGrievanceInformation(Request.QueryString["An"].ToString());
                        AggrievedParty.Visible = true;
                        Button1.Visible = false;
                        lbtnSubmit.Enabled = true;

                    }
                    else if (Request.QueryString["status"].ToString() == "Under Investigation")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewGrievanceInformation(Request.QueryString["An"].ToString());
                        AggressorNo.Enabled = false;
                        IncidentDate.Enabled = false;
                        GrievanceNo.Visible = false;
                        Description.Enabled = false;
                        Incident.Enabled = false;
                        AggrievedParty.Visible = true;
                        ResolutionLine.Visible = true;
                        GrievanceType.Enabled = false;
                        Parties.Visible = false;
                        IncidentTime.Disabled = true;
                        gvAttachmentLines.Columns[3].Visible = false;
                        AggrievedParty.Columns[3].Visible = false;
                        ResolutionLine.Columns[4].Visible = false;
                        Resolutiondiv.Visible = false;
                        ResolutionLines.Visible = true;
                        btnUpload.Visible = false;
                        filetoupload.Visible = false;
                        lbtnSubmit.Visible = false;
                        lbnAddLine.Visible = false;
                    }
                    else if (Request.QueryString["status"].ToString() == "Closed")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        ViewGrievanceInformation(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        AggressorNo.Enabled = false;
                        IncidentDate.Enabled = false;
                        Description.Enabled = false;
                        Incident.Enabled = false;
                        GrievanceNo.Visible = false;
                        GrievanceType.Enabled = false;
                        IncidentTime.Disabled = true;
                        gvAttachmentLines.Columns[3].Visible = false;
                        ResolutionLine.Columns[4].Visible = false;
                        Resolutiondiv.Visible = false;
                        ResolutionLines.Visible = true;
                        btnUpload.Visible = false;
                        AggrievedParty.Visible = true;
                        Parties.Visible = false;
                        AggrievedParty.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        lbtnSubmit.Visible = false;
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    Resolutiondiv.Visible = false;
                    newback.Visible = true;
                }
            }
        }
        protected void update_Click(object sender, EventArgs e)
        {
            try
            {
                String EmpNo = AggressorNo.SelectedValue;
                String IncidentDat = IncidentDate.Text.ToString();
                String IncTime = IncidentTime.Value.ToString();
                String IncLocation = Incident.Text.ToString();
                String Griev = GrievanceType.Text.ToString();
                String Desc = Description.Text.ToString();

                // validate Grievance application controls
                if (string.IsNullOrEmpty(EmpNo))
                {
                    Message("please enter incident date");
                    AggressorNo.Focus();
                    return;
                }
                DateTime Incidentdt;

                if (string.IsNullOrEmpty(IncidentDat))
                {
                    Message("please enter incident date");
                    IncidentDate.Focus();
                    return;
                }
                else
                {
                    Incidentdt = DateTime.ParseExact(IncidentDat, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var todaysDate = DateTime.Today;

                    if (Incidentdt > todaysDate)
                    {
                        Message("Incident date cannot be a future date");
                        IncidentDate.Focus();
                        return;
                    }
                }
                if (String.IsNullOrEmpty(IncTime))
                {
                    Message("Please select Incident Time");
                    IncidentTime.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(IncLocation))
                {
                    Message("Please select Incident Location");
                    Incident.Focus();
                    return;
                }
                String UserID = Session["UserID"].ToString();
                string UserName = Session["username"].ToString();
                String GrivNo = string.Empty;
                string paged = Request.QueryString["Tp"].ToString();
                if (paged == "old")
                {
                    GrivNo = Request.QueryString["An"].ToString();
                }
                else
                {
                    GrivNo = Session["GrivNo"].ToString();
                }
                string rtmsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtmsg = MyComponents.HrService.UpdateGrievanceCase(GrivNo, EmpNo, Convert.ToDateTime(IncidentDat), Convert.ToDateTime(IncTime), IncLocation, Griev, Desc, UserID);
                }
                else
                {
                    rtmsg = MyComponents.HrServiceZarib.UpdateGrievanceCase(GrivNo, EmpNo, Convert.ToDateTime(IncidentDat), Convert.ToDateTime(IncTime), IncLocation, Griev, Desc, UserID);
                }                    
                if (!string.IsNullOrEmpty(rtmsg))
                {
                    if (rtmsg == "Yes")
                    {
                        GrievanceNo.Text = GrivNo;
                        BindAttachmentGridviewData(GrivNo);
                        NewView();
                    }
                    else
                    {
                        Message("Failed to update the details");
                    }
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrievanceListing.aspx");
        }
        protected void newback_Click(object sender, EventArgs e)
        {
            // ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
            MultiView1.ActiveViewIndex = 0;
        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Request.QueryString["Tp"].ToString() == "old"))
                {
                    String RrtMsg = "";
                   if (Session["Company"].ToString() == "ZAAC")
                    {
                        RrtMsg = MyComponents.HrService.SubmitGrievance(Request.QueryString["An"].ToString());

                    }
                    else
                    {
                        RrtMsg = MyComponents.HrServiceZarib.SubmitGrievance(Request.QueryString["An"].ToString());
                    }


                        
                    if (!String.IsNullOrEmpty(RrtMsg))
                    {
                        SuccessMessage(RrtMsg);
                        Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
                        Server.Transfer("GrievanceListing.aspx", true);
                    }
                }
                else
                {
                    String GrivNo = GrievanceNo.Text;
                    String RrtMsg = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RrtMsg = MyComponents.HrService.SubmitGrievance(GrivNo);
                    }
                    else
                    {
                        RrtMsg = MyComponents.HrServiceZarib.SubmitGrievance(GrivNo);
                    }
                    if (!String.IsNullOrEmpty(RrtMsg))
                    {
                        SuccessMessage(RrtMsg);
                        Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
                        Server.Transfer("GrievanceListing.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadEmployees()
        {
            try
            {
                string name = Session["username"].ToString();
                this.AggressorNo.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Employee--", "");
                this.AggressorNo.Items.Add(li);
                var data = MyComponents.OdataService.GetEmployees.AddQueryOption("$filter", "EmployeeStatus eq 'active' ");

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.No))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.FullName,
                                  ym.No
                              );
                    this.AggressorNo.Items.Add(li);

                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadAggrievedParties()
        {
            try
            {
                string name = Session["username"].ToString();
                string aggriever = AggressorNo.SelectedValue.ToString();
                ListItem li = null;
                li = new ListItem("--Select Aggrieved Party--", "");
                this.AggrievedNo.Items.Add(li);
                var data = MyComponents.OdataService.GetEmployees.AddQueryOption("$filter", "EmployeeStatus eq 'active' and No ne '" + name + "' and No ne ' " + aggriever + "'");

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.No))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.No
                              );
                    this.AggrievedNo.Items.Add(li);

                    alreadyEncountered.Add(ym.No);
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadGrievanceType()
        {
            try
            {
                this.GrievanceType.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Grievance Type--", "");
                this.GrievanceType.Items.Add(li);

                var data = MyComponents.OdataService.GetGrievanceType.Execute();

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.Code))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.Description,
                                  ym.Code
                              );
                    this.GrievanceType.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadResolution()
        {
            try
            {
                this.Verdict.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Action--", "");
                this.Verdict.Items.Add(li);
                var data = MyComponents.OdataService.GetResolutions.Execute();
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.VerdictCode))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.Description,
                                  ym.VerdictCode
                              );
                    this.Verdict.Items.Add(li);

                    alreadyEncountered.Add(ym.VerdictCode);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "CaseListing.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
        protected void redirect_toGrievanceListing()
        {
            Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
            Server.Transfer("GrievanceListing.aspx", true);
        }
        protected void NewView()
        {
            MultiView1.ActiveViewIndex = 1;
        }
        protected void lbtnCancll_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string EmpNo = arg[0];
            string DocNo = Session["GrivNo"].ToString();
            try
            {
                MyComponents.HrService.RemovePartyLine(DocNo, EmpNo);
                Message("Line deleted successfully");
                BindGridviewData(Session["GrivNo"].ToString());
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void ViewGrievanceInformation(String number)
        {
            try
            {
                var data = MyComponents.OdataService.GetGrievances.AddQueryOption("$filter", "No_ eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    AggressorNo.Text = collectionz.Aggriever_No.ToString();
                    IncidentDate.Text = collectionz.Incident_Date.ToString();
                    Incident.Text = collectionz.Incident_Location.ToString();
                    IncidentTime.Value = collectionz.Incident_Time.ToString();
                    GrievanceType.Text = collectionz.Grievance_Code.ToString();
                    Description.Text = collectionz.General_Description.ToString();
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //Upload to BC
                string filePath = "", fileName = "", fileExt = "";
                try
                {
                    if (IsPostBack && filetoupload.PostedFile != null)
                    {

                        if (filetoupload.PostedFile.FileName.Length > 0)
                        {
                            filePath = filetoupload.PostedFile.FileName;
                            filePath = filePath.Replace(" ", "_");

                            fileName = filetoupload.FileName;
                            fileName = filePath.Replace(" ", "_");
                            fileExt = Path.GetExtension(fileName).Split('.')[1].ToLower();

                            if (fileExt == "pdf")
                            {
                                System.IO.Stream fs = filetoupload.PostedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                string FileType = "Supporting Document";
                                string doc = Session["GrivNo"].ToString();
                                //Save File Name to table
                                #region commented - using webservice
                                String GrievNo = GrievanceNo.Text;
                                string rtnMsg = "";

                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg = MyComponents.HrService.DocumentAttachment(5, FileType, GrievNo, base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.HrServiceZarib.DocumentAttachment(5, FileType, GrievNo, base64String, fileName);
                                }
                                if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        Message("File Uploaded successfully!");
                                        BindAttachmentGridviewData(GrievNo);
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type alredy exists.");
                                        BindAttachmentGridviewData(GrievNo);
                                    }
                                }
                                else
                                {
                                    Message("ERROR: Failed to upload document!");
                                }
                                #endregion

                            }
                            else
                            {
                                //lblError.Text = "<div class='warnig'><img src='images/warning.gif' width=20 height=20 border='none'>Please select files with the following formats. *.pdf, *.zip, *.rar !</div>";
                                Message("Please select file of this type *.pdf");
                            }
                        }
                        else
                        {
                            Message("Please select file a file to upload.");
                            filetoupload.Focus();
                            return;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Message("ERROR: " + Ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        private void BindAttachmentGridviewData(string number)
        {
            try
            {
                var data = MyComponents.OdataService.GetDocumentAttachments.AddQueryOption("$filter", "No eq '" + number + "' and TableID eq " + TableID + "");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);
                gvAttachmentLines.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                gvAttachmentLines.DataBind();
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void BindGridviewData(string number)
        {
            try
            {
                var data = MyComponents.OdataService.GetAggrievedParties.AddQueryOption("$filter", "DocumentNo eq '" + number + "'");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);
                AggrievedParty.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                AggrievedParty.DataBind();
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        private void BindviewData(string number)
        {
            try
            {
                var data = MyComponents.OdataService.GetResolutions.AddQueryOption("$filter", "DocumentNo eq '" + number + "'");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);
                ResolutionLine.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                ResolutionLine.DataBind();
                MultiView1.ActiveViewIndex = 1;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Verdic = Verdict.SelectedItem.Value;
            string Desc = Description.ToString();
            string Rem = Remark.ToString();

            try
            {

                if (String.IsNullOrEmpty(Verdic))
                {
                    Message("Please Select Action.");
                    Verdict.Focus();
                    return;
                }

                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.CreateResolution(Verdic, Desc, Rem);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.CreateResolution(Verdic, Desc, Rem);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "HR")
                    {
                        BindGridviewData(Session["ApNo"].ToString());
                        Verdict.SelectedIndex = 0;
                        Verdict.Text = null;
                        Message(rtnMsg);
                    }
                    else
                    {
                        Message(rtnMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(ex);
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }

        }

        protected void btnLine_Click(object sender, EventArgs e)
        {
            try
            {
                string EmployeeNumber = AggrievedNo.SelectedItem.Value;
                string ActionDescription = Employeediv.Text.ToString();


                if (String.IsNullOrEmpty(EmployeeNumber))
                {
                    Message("Please Select Employee Name Type.");
                    AggrievedNo.Focus();
                    return;
                }
                String GrivNo = string.Empty;
                string paged = Request.QueryString["Tp"].ToString();
                if (paged == "old")
                {
                    GrivNo = Request.QueryString["An"].ToString();
                }

                else
                {
                    GrivNo = Session["GrivNo"].ToString();

                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {

                    rtnMsg = MyComponents.HrService.AddAggrievedLines(EmployeeNumber, GrivNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.AddAggrievedLines(EmployeeNumber, GrivNo);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "SUCCESS")
                    {
                        AggrievedNo.SelectedIndex = 0;
                        AggrievedNo.Text = null;
                        BindGridviewData(GrivNo);
                        Message(rtnMsg);
                    }
                    else
                    {
                        Message(rtnMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(ex);
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = Session["GrivNo"].ToString();
            try
            {
                String rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.RemoveAttachment(Convert.ToInt32(FileID), DocNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.RemoveAttachment(Convert.ToInt32(FileID), DocNo);
                }
                Message(rtnMsg);
                BindAttachmentGridviewData(Session["GrivNo"].ToString());
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }

        protected void Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data = MyComponents.OdataService.GetAggrievedParties.Execute();
                HashSet<string> alreadyEncountered = new HashSet<string>();
                string empcode = AggrievedNo.SelectedValue;
                foreach (var ym in data)
                {
                    if (ym.EmployeeNo == empcode)
                    {
                        Employeediv.Text = ym.EmployeeName;
                        Employeediv.Enabled = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }

    }
}