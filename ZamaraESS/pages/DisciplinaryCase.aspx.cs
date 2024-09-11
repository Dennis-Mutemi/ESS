using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class DisciplinaryCase : System.Web.UI.Page
    {
        public static int TableID = 60466;
        public static string StaffName = "";
        public static string StaffUserId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string RecStatus = Request.QueryString["status"].ToString();
            string paged = Request.QueryString["Tp"].ToString();

            if (!IsPostBack)
            {
                LoadEmployees();
                LoadOffenceType();
                LoadOffenceCategory();
                LoadCommittee();
                LoadAction();
                string userId = Session["UserID"].ToString();
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paged == "old")
                {
                    if (RecStatus == "New")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        NewLines.Visible = false;
                        HRRemark.Visible = false;
                        update.Visible = true;
                        Committee.Visible = false;
                        Actiondiv.Visible = false;
                        ActionLines.Visible = false;
                        Button1.Visible = false;
                        lbtnSubmit.Enabled = true;
                    }
                    else if (RecStatus == "Submitted")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        OffendorNo.Enabled = false;
                        lbtnSubmit.Visible = false;
                        OffenceType.Enabled = false;
                        OffenceCategory.Enabled = false;
                        Casedate.Enabled = false;
                        update.Visible = true;
                        DisciplinaryNo.Visible = false;
                        Committee.Visible = false;
                        Actiondiv.Visible = false;
                        ActionLines.Visible = false;
                        Button1.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        DisciplinaryActionLine.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        HrRemarkDiv.Visible = true;
                        Description.Enabled = false;
                        HRRemark.Enabled = false;
                    }
                    else if (Request.QueryString["status"].ToString() == "Show Cause")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        Suspectremark.Visible = true;
                        OffendorNo.Enabled = false;
                        update.Visible = true;
                        update.Enabled = true;
                        OffenceType.Enabled = false;
                        OffenceCategory.Enabled = false;
                        DisciplinaryActionLine.Columns[3].Visible = false;
                        Casedate.Enabled = false;
                        Description.Enabled = false;
                        HrRemarkDiv.Visible = true;
                        HRRemark.Enabled = false;
                        Suspect.Enabled = true;
                        Committee.Visible = false;
                        Actiondiv.Visible = false;
                        ActionLines.Visible = false;
                        Button1.Visible = false;
                        NewLines.Visible = true;
                    }
                    else if (Request.QueryString["status"].ToString() == "Show Cause Response")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        OffendorNo.Enabled = false;
                        update.Visible = true;
                        OffenceType.Enabled = false;
                        OffenceCategory.Enabled = false;
                        Casedate.Enabled = false;
                        Description.Enabled = false;
                        DisciplinaryNo.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        HrRemarkDiv.Visible = true;
                        HRRemark.Enabled = false;
                        Suspect.Enabled = false;
                        Suspectremark.Visible = true;
                        Actiondiv.Visible = false;
                        ActionLines.Visible = false;
                        Button1.Visible = false;
                        NewLines.Visible = true;
                        DecisionSummary.Visible = true;
                        Decision.Enabled = false;
                        update.Visible = true;
                        lbtnSubmit.Visible = false;
                    }
                    if (Request.QueryString["status"].ToString() == "Ongoing")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        BindGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        ActionLine.Columns[3].Visible = false;
                        ActionLine.Columns[3].Visible = false;
                        DisciplinaryActionLine.Columns[3].Visible = false;
                        DisciplinaryNo.Visible = false;
                        lbtnSubmit.Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        HrRemarkDiv.Visible = true;
                        HRRemark.Enabled = false;
                        Suspect.Enabled = false;
                        Suspectremark.Visible = true;
                        DecisionSummary.Visible = true;
                        Decision.Enabled = false;
                        Actiondiv.Visible = false;
                        Button1.Visible = false;
                        Committee.Visible = false;
                        update.Visible = true;
                        Start.Visible = true;
                        startdate.Enabled = false;
                        EndDate.Visible = true;
                        End.Enabled = false;
                        Committee.Visible = false;
                        Button1.Visible = false;
                        ActionLine.Visible = true;
                        DisciplinaryActionLine.Visible = true;
                        NewLines.Visible = false;

                    }
                    if (Request.QueryString["status"].ToString() == "Closed")
                    {
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        BindviewData(Request.QueryString["An"].ToString());
                        BindGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        ViewDisciplinaryInformation(Request.QueryString["An"].ToString());
                        DisciplinaryNo.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        ActionLine.Columns[3].Visible = false;
                        DisciplinaryActionLine.Columns[3].Visible = false;
                        lbtnSubmit.Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        HrRemarkDiv.Visible = true;
                        HRRemark.Enabled = false;
                        Suspect.Enabled = false;
                        Suspectremark.Visible = true;
                        DisciplinaryActionLine.Visible = true;
                        DecisionSummary.Visible = true;
                        Decision.Enabled = false;
                        update.Visible = true;
                        Actiondiv.Visible = false;
                        ActionLine.Visible = true;
                        Button1.Visible = false;
                        Start.Visible = true;
                        startdate.Enabled = false;
                        EndDate.Visible = true;
                        End.Enabled = false;
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;

                    Committee.Visible = false;
                    Actiondiv.Visible = false;
                    ActionLines.Visible = false;
                    Button1.Visible = false;
                    NewLines.Visible = false;
                    lbtnSubmit.Enabled = true;
                    update.Visible = true;
                }
            }
        }

        protected void LoadEmployees()
        {
            try
            {
                string name = Session["username"].ToString();
                this.OffendorNo.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Employee--", "");
                this.OffendorNo.Items.Add(li);
                var data = MyComponents.OdataService.GetEmployees.AddQueryOption("$filter", "EmployeeStatus eq 'active'");

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
                    this.OffendorNo.Items.Add(li);


                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadOffenceType()
        {
            try
            {
                this.OffenceType.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Offence Type--", "");
                this.OffenceType.Items.Add(li);

                var data = MyComponents.OdataService.GetOffenceTypes.Execute();

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
                    this.OffenceType.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadOffenceCategory()
        {
            try
            {
                this.OffenceCategory.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Offence Category--", "");
                this.OffenceCategory.Items.Add(li);

                var data = MyComponents.OdataService.GetOffenceCategory.Execute();

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
                    this.OffenceCategory.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
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

        protected void Back_Click(object sender, EventArgs e)
        {
            Server.Transfer("CaseListing.aspx", true);
        }
        protected void ViewDisciplinaryInformation(String number)
        {
            try
            {
                var data = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "No eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    OffendorNo.Text = collectionz.OffenderNo.ToString();
                    OffenceType.Text = collectionz.OffenceType.ToString();
                    OffenceCategory.Text = collectionz.OffenceCategory.ToString();
                    Casedate.Text = collectionz.CaseDate.ToString();
                    Description.Text = collectionz.CaseDescription.ToString();
                    HRRemark.Text = collectionz.HRRemark.ToString();
                    Suspect.Text = collectionz.SuspectRemark.ToString();
                    Decision.Text = collectionz.DecisionSummary.ToString();
                    startdate.Text = collectionz.StartDate.ToString();
                    End.Text = collectionz.EndDate.ToString();
                    EmployeeNos.Text = collectionz.EmployeeNo.ToString();
                    if (Request.QueryString["status"].ToString() == "Show Cause")
                    {
                        String EmpNo = Session["username"].ToString();
                        EmpNo = collectionz.EmployeeNo;

                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void NewView()
        {
            MultiView1.ActiveViewIndex = 1;
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                string CaseDate = Casedate.Text.ToString();
                string Desc = Description.Text.ToString();
                string Emp = OffendorNo.SelectedValue;
                string OffType = OffenceType.SelectedValue;
                string OffCategory = OffenceCategory.SelectedValue.ToString();
                string SuspectRemark = Suspect.Text.ToString();

                // validate disciplinary application controls
                if (String.IsNullOrEmpty(Emp))
                {
                    Message("Please enter Offendor Name");
                    OffendorNo.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(CaseDate))
                {
                    Message("Please enter Case date");
                    Casedate.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(OffType))
                {
                    Message("Please enter Offence Type");
                    OffenceType.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(OffCategory))
                {
                    Message("Please Select Offence Category");
                    OffenceCategory.Focus();
                    return;
                }

                if (Convert.ToDateTime(CaseDate) > Convert.ToDateTime(DateTime.Today))
                {
                    Message("Case Date  (" + CaseDate + ") cannot be future date");
                    Casedate.Focus();
                    return;
                }
                if (Request.QueryString["status"].ToString() == "Show Cause")
                {
                    if (String.IsNullOrEmpty(SuspectRemark))
                    {
                        Message("Please enter Suspect Remark");
                        Suspect.Focus();
                        return;
                    }
                }
                String UserID = Session["UserID"].ToString();
                String DisNo = string.Empty;
                String paged = Request.QueryString["Tp"].ToString();

                if (paged == "old")
                {
                    DisNo = Request.QueryString["An"].ToString();
                }
                else
                {
                    DisNo = Session["DisNo"].ToString();
                }
                string rtmsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtmsg = MyComponents.HrService.updateDisciplinaryData(DisNo, Emp, OffType, OffCategory, Desc, Convert.ToDateTime(CaseDate), SuspectRemark, UserID);
                }
                else
                {
                    rtmsg = MyComponents.HrServiceZarib.updateDisciplinaryData(DisNo, Emp, OffType, OffCategory, Desc, Convert.ToDateTime(CaseDate), SuspectRemark, UserID);
                }
                if (!string.IsNullOrEmpty(rtmsg))
                {
                    if (rtmsg == "Yes")
                    {
                        DisciplinaryNo.Text = DisNo;
                        BindAttachmentGridviewData(DisNo);
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
                var data = MyComponents.OdataService.GetActionLines.AddQueryOption("$filter", "DocumentNo eq '" + number + "'");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);

                ActionLine.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                ActionLine.DataBind();

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
                var data = MyComponents.OdataService.GetDisciplinaryLines.AddQueryOption("$filter", "CaseNo eq '" + number + "'");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);

                DisciplinaryActionLine.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                DisciplinaryActionLine.DataBind();

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }

        protected void redirect_toCaseListing()
        {
            Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
            Server.Transfer("Disciplinary Case.aspx", true);
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = Session["DisNo"].ToString();
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
                BindAttachmentGridviewData(Session["DisNo"].ToString());
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void newback_Click(object sender, EventArgs e)
        {
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
                        RrtMsg = MyComponents.HrService.SubmitDisciplinaryApplication(Request.QueryString["An"].ToString());
                    }
                    else
                    {
                        RrtMsg = MyComponents.HrServiceZarib.SubmitDisciplinaryApplication(Request.QueryString["An"].ToString());
                    }
                    if (!String.IsNullOrEmpty(RrtMsg))
                    {
                        SuccessMessage(RrtMsg);
                        Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
                        Server.Transfer("CaseListing.aspx", true);
                    }
                }
                else
                {
                    String DiscNo = DisciplinaryNo.Text;
                    String RrtMsg = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RrtMsg = MyComponents.HrService.SubmitDisciplinaryApplication(DiscNo);
                    }
                    else
                    {
                        RrtMsg = MyComponents.HrServiceZarib.SubmitDisciplinaryApplication(DiscNo);
                    }
                    if (!String.IsNullOrEmpty(RrtMsg))
                    {
                        SuccessMessage(RrtMsg);
                        Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
                        Server.Transfer("CaseListing.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }

        }

        protected void btnLine_Click(object sender, EventArgs e)
        {
            string Employeen = Session["username"].ToString();
            try
            {
                if (String.IsNullOrEmpty(Employeen))
                {
                    Message("Please Select Employee Name.");
                    EmployeeNos.Focus();
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.AddDisciplinaryLines(Employeen, Request.QueryString["An"].ToString());
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.AddDisciplinaryLines(Employeen, Request.QueryString["An"].ToString());
                }

                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "HR")
                    {
                        BindGridviewData(Session["CaseNo"].ToString());
                        EmployeeNos.SelectedIndex = 0;
                        EmployeeNos.Text = null;
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

        protected void lbtnCancll_Click(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click1(object sender, EventArgs e)
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
                                string doc = Session["DisNo"].ToString();
                                //Save File Name to table
                                #region commented - using webservice
                                String DiscNo = DisciplinaryNo.Text;
                                string rtnMsg = "";
                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg= MyComponents.HrService.DocumentAttachment(3, FileType, DiscNo, base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.HrServiceZarib.DocumentAttachment(3, FileType, DiscNo, base64String, fileName);
                                }
                                if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        Message("File Uploaded successfully!");
                                        BindAttachmentGridviewData(DiscNo);
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type alredy exists.");
                                        BindAttachmentGridviewData(DiscNo);
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

        protected void LoadCommittee()
        {
            try
            {
                //string name = Session["username"].ToString();
                this.EmployeeNos.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Employee--", "");
                this.EmployeeNos.Items.Add(li);
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
                    this.EmployeeNos.Items.Add(li);


                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadAction()
        {
            try
            {
                this.Action.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Action--", "");
                this.Action.Items.Add(li);
                var data = MyComponents.OdataService.GetActionLines.Execute();
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.ActionNo))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.ActionDescription,
                                  ym.ActionNo
                              );
                    this.Action.Items.Add(li);

                    alreadyEncountered.Add(ym.ActionNo);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string ActionNo = Action.SelectedItem.Value;
            string ActionDescription = Descriptiondiv.ToString();
            ;
            try
            {
                if (String.IsNullOrEmpty(ActionNo))
                {
                    Message("Please Select Action.");
                    Action.Focus();
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.CreateActionLine(ActionNo, ActionDescription);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.CreateActionLine(ActionNo, ActionDescription);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "HR")
                    {
                        BindGridviewData(Session["ActionNo"].ToString());
                        Action.SelectedIndex = 0;
                        Action.Text = null;
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
        protected void View_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string DOCID = arg[0];
            string DocNo = Request.QueryString["An"].ToString();
            try
            {
                if (DocNo != "")
                {
                    string base64Document = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        base64Document = MyComponents.HrService.ViewAttachment(Convert.ToInt32(DOCID), Request.QueryString["An"].ToString());
                    }
                    else
                    {
                        base64Document = MyComponents.HrServiceZarib.ViewAttachment(Convert.ToInt32(DOCID), Request.QueryString["An"].ToString());
                    }
                    viewdoc.Visible = true;

                    string Content = "data:application/pdf;base64," + base64Document;

                    myPDF.Attributes.Add("src", Content);
                }
                else
                {
                    String DiscNo = DisciplinaryNo.Text;
                    string base64Document = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        base64Document = MyComponents.HrService.ViewAttachment(Convert.ToInt32(DOCID), DiscNo);
                    }
                    else
                    {
                        base64Document = MyComponents.HrServiceZarib.ViewAttachment(Convert.ToInt32(DOCID), DiscNo);
                    }
                    viewdoc.Visible = true;

                    string Content = "data:application/pdf;base64," + base64Document;

                    myPDF.Attributes.Add("src", Content);
                }
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
                var data = MyComponents.OdataService.GetActionLines.Execute();
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.ActionNo))
                    {
                        continue;
                    }
                    Descriptiondiv.Text = ym.ActionDescription;
                    Descriptiondiv.Enabled = false;
                    alreadyEncountered.Add(ym.ActionNo);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }

        }
        protected void gvAttachmentLines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the LinkButton inside the ItemTemplate
                LinkButton Cancel = e.Row.FindControl("Cancel") as LinkButton;

                // Set the visibility of the button directly
                if (Cancel != null)
                {
                    String paged = Request.QueryString["Tp"];
                    if (paged != null)
                    {
                        string RecStatus = Request.QueryString["status"].ToString();
                        Session["DisNo"] = Request.QueryString["An"].ToString();


                        if (RecStatus == "New")
                        {
                            Cancel.Visible = true;
                        }
                        else if (RecStatus == "Ongoing")
                        {
                            Cancel.Visible = false;
                        }
                    }
                }
            }
        }

    }
}