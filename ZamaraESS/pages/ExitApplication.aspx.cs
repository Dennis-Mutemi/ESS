using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class ExitApplication : System.Web.UI.Page
    {

        string[] StaffDetails = new string[3];
        public static int TableID = 60490;
        public static string StaffName = "";
        public static string StaffUserId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                string RecStatus = Request.QueryString["status"].ToString();
                string paged = Request.QueryString["Tp"].ToString();

                LoadLeaveBal();
                LoadExitReason();

                if (paged == "old")
                {
                    if (RecStatus == "New")
                    {
                        ViewExitInformation(Request.QueryString["An"].ToString());
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        Recall.Visible = false;
                        Termination.Visible = false;
                        Acceptancebtn.Visible = false;
                        TerminationDiv.Visible = false;
                    }

                    else if (RecStatus == "Submitted")
                    {
                        ViewExitInformation(Request.QueryString["An"].ToString());
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        startdate.Enabled = false;
                        lastdate.Enabled = false;
                        leavebalance.Enabled = false;
                        exitreason.Enabled = false;
                        Termination.Visible = true;
                        Termination.Enabled = false;
                        Acceptanceremark.Visible = true;
                        Acceptance.Enabled = false;
                        lbtnSubmit.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        Acceptancebtn.Visible = false;
                    }
                    else if (RecStatus == "Accepted")
                    {
                        ViewExitInformation(Request.QueryString["An"].ToString());
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        startdate.Enabled = false;
                        lastdate.Enabled = false;
                        leavebalance.Enabled = false;
                        exitreason.Enabled = false;
                        Acceptanceremark.Visible = true;
                        Acceptance.Enabled = false;
                        Recall.Visible = false;
                        Termination.Visible = true;
                        Termination.Enabled = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                    }
                    else if (RecStatus == "Pending Approval")
                    {
                        ViewExitInformation(Request.QueryString["An"].ToString());
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        startdate.Enabled = false;
                        lastdate.Enabled = false;
                        leavebalance.Enabled = false;
                        exitreason.Enabled = false;
                        Termination.Visible = true;
                        Termination.Enabled = false;
                        Acceptanceremark.Visible = true;
                        Acceptance.Enabled = false;
                        Recall.Visible = false;
                        lbtnSubmit.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        Acceptancebtn.Visible = false;
                    }
                    else if (RecStatus == "Approved")
                    {
                        ViewExitInformation(Request.QueryString["An"].ToString());
                        BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                        MultiView1.ActiveViewIndex = 0;
                        startdate.Enabled = false;
                        lastdate.Enabled = false;
                        leavebalance.Enabled = false;
                        exitreason.Enabled = false;
                        Termination.Visible = true;
                        Acceptanceremark.Visible = true;
                        Termination.Visible = true;
                        Termination.Enabled = false;
                        Acceptance.Enabled = false;
                        ApprovalDiv1.Visible = true;
                        Recall.Visible = false;
                        ApprovalDiv1.Disabled = true;
                        lbtnSubmit.Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        btnUpload.Visible = false;
                        Acceptancebtn.Visible = false;
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    Termination.Visible = false;
                    TerminationDiv.Visible = false;
                    Recall.Visible = false;
                    Acceptancebtn.Visible = false;
                }
            }
        }
        protected void LoadExitReason()
        {
            try
            {
                this.exitreason.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Exit Reason--", "");
                this.exitreason.Items.Add(li);
                var data = MyComponents.OdataService.GetExitReasons.AddQueryOption("$filter", "Voluntary eq  true ");
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
                    this.exitreason.Items.Add(li);

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
            string myPage = "Exit.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
        protected void DisplayAlert(string message)
        {
            String paged = Request.QueryString["Tp"];
            string RecStatus = string.Empty;
            if (paged != null)
            {
                RecStatus = Request.QueryString["status"].ToString();
            }
            else
            {
                RecStatus = "New";
            }
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');window.location.href = 'Exit.aspx?status=" + RecStatus + "'",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
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
        protected void newView()
        {
            MultiView1.ActiveViewIndex = 1;
            BindAttachmentGridviewData(Session["ExitNo"].ToString());
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
                                string ExitNo = Session["ExitNo"].ToString();
                                //Save File Name to table
                                #region commented - using webservice
                                string rtnMsg = "";
                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg = MyComponents.HrService.DocumentAttachment(4, FileType, ExitNo, base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.HrServiceZarib.DocumentAttachment(4, FileType, ExitNo, base64String, fileName);
                                }
                                if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        Message("File Uploaded successfully!");
                                        BindAttachmentGridviewData(ExitNo);
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type already exists.");
                                        BindAttachmentGridviewData(ExitNo);
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
        private void LoadLeaveBal()
        {
            try
            {
                string EmployeeNo = Session["username"].ToString();
                var leavebalanc = MyComponents.OdataService.GetEmployees.AddQueryOption("$filter", "No eq '" + EmployeeNo + "'");

                foreach (var ym in leavebalanc)
                {
                    string leave = ym.LeaveBalance.ToString();
                    if (leave == "0")
                    {
                        leavebalance.Text = "Not Available";
                    }
                    else
                    {
                        leavebalance.Text = leave;
                    }
                }

            }
            catch (Exception Ex)
            {
                Ex.Data.Clear();
            }

        }
        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            String ExitReason = exitreason.SelectedValue.ToString();
            String NoticeDate = startdate.Text.ToString();
            String lastdat = lastdate.Text.ToString();

            try
            {
                //validate exit application controls
                if (String.IsNullOrEmpty(ExitReason))
                {
                    Message("Please Select Exit Reason");
                    exitreason.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(NoticeDate))
                {
                    Message("Please enter notice date");
                    startdate.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(lastdat))
                {
                    Message("Please enter notice end date");
                    lastdate.Focus();
                    return;
                }

                if (Convert.ToDateTime(lastdat) < Convert.ToDateTime(NoticeDate))
                {
                    Message("Notice End Date  (" + lastdat + ") cannot be less than notice start date (" + NoticeDate + ")");
                    exitreason.Focus();
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
                    GrivNo = Session["ExitNo"].ToString();
                }

                String RrtMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    //RrtMsg = MyComponents.HrService.SubmitExitApplication(GrivNo, ExitReason, Convert.ToDateTime(NoticeDate), Convert.ToDateTime(lastdat));
                }
                else
                {
                    RrtMsg = MyComponents.HrServiceZarib.SubmitExitApplication(GrivNo, ExitReason, Convert.ToDateTime(NoticeDate), Convert.ToDateTime(lastdat));
                }
                if (!String.IsNullOrEmpty(RrtMsg))
                {
                    DisplayAlert("Submitted successfully");
                    Server.Transfer("Exit.aspx", true);
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void ViewExitInformation(String number)
        {
            try
            {
                var data = MyComponents.OdataService.GetExits.AddQueryOption("$filter", "No eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    startdate.Text = collectionz.Notice_Start_Date.ToString();
                    lastdate.Text = collectionz.Last_Date_of_Service.ToString();
                    exitreason.Text = collectionz.Exit_Reason.ToString();
                    Termination.Text = collectionz.Termination_Date.ToString();
                    Acceptance.Text = collectionz.Acceptance_Remarks.ToString();
                    Approval.Text = collectionz.Approval_Remarks.ToString();
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }

        protected void redirect_toExitList()
        {
            Response.Write("<script language='javascript'>alert('Submitted successfully');</script>");
            Server.Transfer("Exit.aspx", true);
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


        protected void Recall_Click(object sender, EventArgs e)
        {
            string paged = Request.QueryString["Tp"];
            string appno = string.Empty;
            if (paged != null)
            {
                appno = Request.QueryString["an"].ToString();
            }
            string rtmsg = "";
            if (Session["Company"].ToString() == "ZAAC")
            {
                rtmsg = MyComponents.HrService.RecallExit(appno);
            }
            else
            {
                rtmsg = MyComponents.HrServiceZarib.RecallExit(appno);
            }
            if (rtmsg != "")
            {
                DisplayAlert(rtmsg);
                Response.Write("<script language='javascript'>alert('Recalled successfully');</script>");
                Server.Transfer("Exit.aspx", true);

            }
            else
            {
                Message(rtmsg);
                Response.Write("<script language='javascript'>alert('Recalled successfully');</script>");
                Server.Transfer("Exit.aspx", true);
            }

        }

        protected void Acceptance_Click(object sender, EventArgs e)
        {
            String ExitReason = exitreason.SelectedValue.ToString();
            String NoticeDate = startdate.Text.ToString();
            String lastdat = lastdate.Text.ToString();

            try
            {
                //validate exit application controls
                if (String.IsNullOrEmpty(ExitReason))
                {
                    Message("Please Select Exit Reason");
                    exitreason.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(NoticeDate))
                {
                    Message("Please enter notice date");
                    startdate.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(lastdat))
                {
                    Message("Please enter notice end date");
                    lastdate.Focus();
                    return;
                }

                if (Convert.ToDateTime(lastdat) < Convert.ToDateTime(NoticeDate))
                {
                    Message("Notice End Date  (" + lastdat + ") cannot be less than notice start date (" + NoticeDate + ")");
                    exitreason.Focus();
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
                    GrivNo = Session["ExitNo"].ToString();
                }

                String RrtMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    //RrtMsg = MyComponents.HrService.SubmitExitApplication(GrivNo, ExitReason, Convert.ToDateTime(NoticeDate), Convert.ToDateTime(lastdat));
                }
                else
                {
                    RrtMsg = MyComponents.HrServiceZarib.SubmitExitApplication(GrivNo, ExitReason, Convert.ToDateTime(NoticeDate), Convert.ToDateTime(lastdat));
                }


                   
                if (!String.IsNullOrEmpty(RrtMsg))
                {
                    Server.Transfer("Exit.aspx", true);
                    DisplayAlert("Submitted successfully");
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }

        }

        protected void newback_Click(object sender, EventArgs e)
        {
            Server.Transfer("Exit.aspx", true);
        }
    }
}