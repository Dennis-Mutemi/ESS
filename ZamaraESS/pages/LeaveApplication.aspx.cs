using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveApplication : System.Web.UI.Page
    {
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
                Session["AppNo"] = Request.QueryString["An"].ToString();

                Fill_DropDownLeaveTypes();
                LoadLeaveLabel();
                LoadLeaveBal(DdLeaveType.SelectedValue.ToString());
                LoadRelievers();


                if (RecStatus == "Open" && paged == "new")
                {
                    lbtnApply.Visible = true;

                    MustAttach();
                }
                else if (RecStatus == "Open" && paged == "old")
                {
                    lbtnApply.Visible = true;
                    DdLeaveType.Enabled = true;
                    TxtAppliedDays.Enabled = true;
                    txtStartDate.Enabled = true;
                    TxtPurpose.Enabled = true;
                    BindLeaveDetails(Session["AppNo"].ToString());

                    MustAttach();
                }
                else if (RecStatus == "Pending Approval")
                {
                    string DocNo = Session["AppNo"].ToString();
                    int TableID = 52167705; //Leave Application

                    string rtn = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        rtn = MyComponents.HrService.CanCancelDocument(DocNo, TableID);
                    }
                    else
                    {
                        rtn = MyComponents.HrServiceZarib.CanCancelDocument(DocNo, TableID);
                    }
                        if (!string.IsNullOrEmpty(rtn))
                    {
                        if (rtn == "Yes")
                        {
                            lbtnCancel.Visible = true;
                        }
                        else
                        {
                            lbtnCancel.Visible = false;
                        }
                    }


                    lbtnApply.Visible = false;
                    DdLeaveType.Enabled = false;
                    ddlReliever.Enabled = false;
                    DdLeaveType.Visible = false;
                    ddlReliever.Visible = false;
                    txtLeaveType.Visible = true;
                    txtReleiverName.Visible = true; 

                    TxtAppliedDays.Enabled = false;
                    txtStartDate.Enabled = false;
                    TxtPurpose.Enabled = false;
                    DocAttachment.Visible = false;
                    //Approvals.Visible = true;
                    BindLeaveDetails(Session["AppNo"].ToString());
                }
                else if (RecStatus == "Approved")
                {
                    lbtnApply.Visible = false;
                    DdLeaveType.Enabled = false;
                    ddlReliever.Enabled = false;
                    DdLeaveType.Visible = false;
                    ddlReliever.Visible = false;
                    txtLeaveType.Visible = true;
                    txtReleiverName.Visible = true;

                    TxtAppliedDays.Enabled = false;
                    txtStartDate.Enabled = false;
                    TxtPurpose.Enabled = false;
                    DocAttachment.Visible = false;
                    //Approvals.Visible = true;
                    BindLeaveDetails(Session["AppNo"].ToString());
                }

                //bool HasPendingApplications_ = HasPendingApplications();
                //if (HasPendingApplications_)
                //{
                //    contents.Visible = false;
                //    SuccessMessage("You have a Pending Leave Application. Please Cancel/Wait for it to be Approved and Try Again.");
                //}
               
            }
        }

        public void Fill_DropDownLeaveTypes()
        {
            try
            {
                string gender = Session["gnder"].ToString();

                this.DdLeaveType.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Leave Type--", "");
                this.DdLeaveType.Items.Add(li);
                var data = MyComponents.OdataService.GetLeaveTypes.AddQueryOption("$filter", "Gender eq '" + gender + "' or Gender eq '0'");
                int counter = 0;
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
                    this.DdLeaveType.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }


        protected void LoadRelievers()
        {
            try
            {
                string username = Session["username"].ToString();
                string department = Session["Department"].ToString();

                this.ddlReliever.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Reliever--", "");
                this.ddlReliever.Items.Add(li);

                var data = MyComponents.OdataService.GetRelievers.AddQueryOption("$filter", "No ne '" + username + "' and EmployeeStatus eq 'Active'");

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.No))
                    {
                        continue;
                    }
                    li = new ListItem(
                                   ym.FirstName,                                  
                                  ym.No
                              );
                    this.ddlReliever.Items.Add(li);

                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }

        private void LoadLeaveLabel()
        {
            string LeaveType = DdLeaveType.SelectedValue.ToString();
            //lblLeaveType.Text = LeaveType;
        }
        private void LoadLeaveBal(string LeaveType)
        {
            try
            {
                string EmployeeNo = Session["username"].ToString();
                //string LeaveType = DdLeaveType.SelectedValue.ToString();
                int Year = Convert.ToInt32(DateTime.Now.Year);
                //leaveMsg.Visible = false;
                //BtnApply.Visible = true;

                string availabledays = "";
                //availabledays = MyComponents.HrService.AvailableLeaveDays(EmployeeNo, LeaveType, Year);
                if (Session["Company"].ToString() == "ZAAC")
                {
                    availabledays = MyComponents.HrService.AvailableLeaveDays(EmployeeNo, LeaveType);
                }
                else
                {
                    availabledays = MyComponents.HrServiceZarib.AvailableLeaveDays(EmployeeNo, LeaveType);
                }
                if (!string.IsNullOrEmpty(availabledays))
                {
                    double leavedays = Convert.ToDouble(availabledays);
                    lblLeaveBal.Text = leavedays.ToString();
                    if (leavedays <= 0)
                    {
                        lblLeaveBal.Text = "Not Available";
                        lbtnApply.Visible = true;
                        lbtnBack.Visible = true;
                        //lblSelectedLeaveType.Text = DdLeaveType.SelectedItem.Text;
                    }
                }
                else
                {
                    lblLeaveBal.Text = "Not Available";
                    lbtnApply.Visible = false;
                    lbtnBack.Visible = true;
                    //lblSelectedLeaveType.Text = DdLeaveType.SelectedItem.Text;
                }

            }
            catch (Exception Ex)
            {
                Ex.Data.Clear();
            }

        }
        private void BindLeaveDetails(string number)
        {
            try
            {
                var data = MyComponents.OdataService.GetLeaveApplications.AddQueryOption("$filter", "No_ eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    //Bind Leave Details Here
                    DdLeaveType.SelectedValue= collectionz.Leave_Type;
                    ddlReliever.SelectedValue = collectionz.RelieverNo;                   
                    TxtAppliedDays.Text = Convert.ToString(Convert.ToDouble(collectionz.No__of_Days).ToString(CultureInfo.InvariantCulture));
                    txtStartDate.Text = collectionz.Start_Date.ToString();
                    txtEndDate.Text = collectionz.End_Date.ToString();
                    TxtPurpose.Text = collectionz.Description.ToString();
                    
                    LoadLeaveBal(collectionz.Leave_Type);
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

        protected bool HasPendingApplications()
        {
            bool b = false;

            try
            {
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.HasPendingLeaveApplication(Session["username"].ToString());
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.HasPendingLeaveApplication(Session["username"].ToString());
                }
                    if (rtnMsg == "Yes")
                {
                    b = true;
                }
            }
            catch (Exception Ex)
            {
                Ex.Data.Clear();
            }

            return b;
        }
        public bool MustAttach()
        {
            bool Attachment = false;
            try
            {
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.MustAttach(DdLeaveType.SelectedValue);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.MustAttach(DdLeaveType.SelectedValue);
                }
                    if (rtnMsg == "Yes")
                {
                    DocAttachment.Visible = true;
                    Attachment = true;
                }
                else
                {
                    DocAttachment.Visible = false;
                    Attachment = false;
                }
            }
            catch (Exception Ex)
            {
                Message("ERROR: " + Ex.Message.ToString());
                //HttpContext.Current.Response.Write(Ex);
                //Ex.Data.Clear();
            }
            return Attachment;
        }
        protected void lbtnApply_Click(object sender, EventArgs e)
        {
            string DocNo = "";
            String paged = Request.QueryString["Tp"];
            if (paged != null && paged != "new")
            {
                DocNo = Request.QueryString["An"].ToString();               
            }
            else
            {
                DocNo = Session["AppNo"].ToString();
            }
            try
            {           

                bool HasPendingApplications_ = HasPendingApplications();

                if (HasPendingApplications_)
                {
                    Message("You have a Pending Leave Application. Please Cancel/Wait for it to be Approved and Try Again.");
                    return;
                }

                string Reliever = "";

                Reliever = ddlReliever.SelectedValue;
                //Validate Leave days
                var appliedDays = TxtAppliedDays.Text.ToString();             
               
                DateTime startingDate;
                startingDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
               
                var purpose = TxtPurpose.Text.Replace("'", "").Trim();
               
                if (purpose.Length > 200)
                {
                    Message("Description cannot have more than 200 characters.");
                    TxtPurpose.Focus();
                    return;
                }
                if (MustAttach())
                {                   
                    if (String.IsNullOrEmpty(filetoupload.PostedFile.FileName))
                    {
                        Message("You MUST attach supporting document to proceed.");
                        filetoupload.Focus();
                        return;
                    }                    
                }

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

                            if (fileExt == "pdf" || fileExt == "docx" || fileExt == "xlsx" || fileExt == "txt" || fileExt == "png" || fileExt == "jpg")
                            {
                                System.IO.Stream fs = filetoupload.PostedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                string FileType = "Supporting Document";

                                //Save File Name to table                                
                                string rtnMsg = "";
                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg = MyComponents.HrService.DocumentAttachment(1, FileType, DocNo, base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.HrServiceZarib.DocumentAttachment(1, FileType, DocNo, base64String, fileName);
                                }

                                    if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        string Rtmsg = "";
                                        if (Session["Company"].ToString() == "ZAAC")
                                        {
                                            Rtmsg = MyComponents.HrService.HRLeaveApplication(DocNo, Session["username"].ToString(), DdLeaveType.SelectedValue, purpose, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate), Reliever);
                                        }
                                        else
                                        {
                                            Rtmsg = MyComponents.HrServiceZarib.HRLeaveApplication(DocNo, Session["username"].ToString(), DdLeaveType.SelectedValue, purpose, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate), Reliever);
                                        }
                                            if (!string.IsNullOrEmpty(Rtmsg))
                                        {
                                            SuccessMessage("Rtmsg");
                                        }
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type alredy exists.");
                                    }
                                }
                                else
                                {
                                    Message("ERROR: Failed to upload document!");
                                }
                            }
                            else
                            {
                                Message("Please select file of this type *.pdf, *.docx, *.xlsx, *.txt, *.png, *.jpg");
                                filetoupload.Focus();
                                return;
                            }
                        }
                        else
                        {
                            Message("Please select a file to upload.");
                            filetoupload.Focus();
                            return;
                        }
                    }
                    else
                    {
                        string Rtmsg = "";
                        if (Session["Company"].ToString() == "ZAAC")
                        {
                            Rtmsg = MyComponents.HrService.HRLeaveApplication(DocNo, Session["username"].ToString(), DdLeaveType.SelectedValue, purpose, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate), Reliever);
                        }
                        else
                        {
                            Rtmsg = MyComponents.HrServiceZarib.HRLeaveApplication(DocNo, Session["username"].ToString(), DdLeaveType.SelectedValue, purpose, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate), Reliever);
                        }
                        if (!string.IsNullOrEmpty(Rtmsg))
                        {
                            SuccessMessage(Rtmsg);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Message("ERROR: " + Ex.Message.ToString());
                    Ex.Data.Clear();
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }            
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveListing.aspx");
        }
        protected void DdLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsContinuous();

            LoadLeaveBal(DdLeaveType.SelectedValue.ToString());

            MustAttach();
        }
        protected void IsContinuous()
        {
            string LeaveType = DdLeaveType.SelectedValue.ToString();
            try
            {
                #region commented - using webservice
                string staffLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.HrService.IsContinuous(LeaveType);
                }
                else
                {
                    staffLoginInfo = MyComponents.HrServiceZarib.IsContinuous(LeaveType);
                }

                //returnMsg::changedPassword::staffNo::staffUserID::staffName
                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string returnMsg = "", days = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                    returnMsg = staffLoginInfo_arr[0];
                    days = staffLoginInfo_arr[1];
                    if (returnMsg == "Yes")
                    {
                        TxtAppliedDays.Enabled = false;
                        TxtAppliedDays.Text = days;
                        lblLeaveBal.Text = days;
                    }
                    else
                    {
                        TxtAppliedDays.Enabled = true;
                        TxtAppliedDays.Text = days;
                        lblLeaveBal.Text = days;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "LeaveListing.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            string DocNo = Session["AppNo"].ToString();
            string rtnMsg = "";
            if (Session["Company"].ToString() == "ZAAC")
            {
                rtnMsg = MyComponents.HrService.HRCancelLeaveApplication(DocNo);
            }
            else
            {
                rtnMsg = MyComponents.HrServiceZarib.HRCancelLeaveApplication(DocNo);
            }
                
            SuccessMessage(rtnMsg);
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {

            try
            {
                string[] strdelimiters = new string[] { "::" };               
                DateTime LvSrtDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);                             
                string AppNo = Session["AppNo"].ToString();
                string rtms = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    //rtms = MyComponents.HrService.ValidateStartDate(AppNo, DdLeaveType.SelectedValue, Session["username"].ToString(), Convert.ToDateTime(DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)), Convert.ToInt16(TxtAppliedDays.Text), ddlReliever.SelectedValue);
                }
                else
                {
                    rtms=MyComponents.HrServiceZarib.ValidateStartDate(AppNo, DdLeaveType.SelectedValue, Session["username"].ToString(), Convert.ToDateTime(DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)), Convert.ToInt16(TxtAppliedDays.Text), ddlReliever.SelectedValue);
                }
                if(rtms == null || rtms.Length != 0)
                {
                    string[] LeaveInfo = rtms.Split(strdelimiters, StringSplitOptions.None);
                    txtEndDate.Text = LeaveInfo[0];
                    Session["AppNo"] = LeaveInfo[1];
                    displaymsg.Visible = false;
                }           
                
            }
            catch (Exception ex)
            {

                displaymsg.Visible = true;
                msg.InnerText = ex.Message;
            }
        }
    }
}