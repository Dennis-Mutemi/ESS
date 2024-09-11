using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveScheduling : System.Web.UI.Page
    {
        string[] StaffDetails = new string[3];
        string[] RelieverDetails = new string[3];
        public static string StaffName = "";
        public static string StaffUserId = "";
        string LeaveNum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");

                }
                Fill_DropDownLeaveTypes();
                LoadLeaveLabel();
                LoadLeaveBal();
            }
        }
        public void Fill_DropDownLeaveTypes()
        {
            try
            {
                string gender = Session["Gender"].ToString();

                this.DdLeaveType.Items.Clear();
                ListItem li = null;


                var data = MyComponents.OdataService.GetLeaveTypes.AddQueryOption("$filter", "Gender eq '" + gender + "' or Gender eq '0'");

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


        private void LoadLeaveLabel()
        {
            string LeaveType = DdLeaveType.SelectedValue.ToString();
            //lblLeaveType.Text = LeaveType;
        }
        private void LoadLeaveBal()
        {
            try
            {
                string EmployeeNo = Session["username"].ToString();
                string LeaveType = DdLeaveType.SelectedValue.ToString();
                int Year = Convert.ToInt32(DateTime.Now.Year);
                //leaveMsg.Visible = false;

                string availabledays = "";
                //availabledays = MyComponents.HrService.AvailableLeaveDays(EmployeeNo, LeaveType, Year);
                if (Session["Company"].ToString() == "ZAAC")
                {
                    availabledays = MyComponents.HrService.AvailableLeaveDays(EmployeeNo, LeaveType);

                }
                else
                {
                    availabledays=MyComponents.HrServiceZarib.AvailableLeaveDays(EmployeeNo, LeaveType);
                }
                    
                if (!string.IsNullOrEmpty(availabledays))
                {
                    double leavedays = Convert.ToDouble(availabledays);
                    lblLeaveBal.Text = leavedays.ToString();
                    if (leavedays <= 0)
                    {
                        lblLeaveBal.Text = "Not Available";
                        lbtnApply.Visible = false;
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

        public void Message1(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
        public void Message(string strMsg)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myDetails", "$('#eventModal').modal();", true);
            dvMdlContentFail.Visible = true;
            dvMdlContentPass.Visible = false;
        }

        public void ExceptionMsg(string Msg)
        {
            lbtnApply.Visible = false;
            Message(Msg);
        }
        protected void lbtnApply_Click(object sender, EventArgs e)
        {
            try
            {

                //Validate Leave days
                var appliedDays = TxtAppliedDays.Text.ToString();
                if (lblLeaveBal.Text != "Sorry, You have exhausted your leave days. You may select a different leave Type")
                {
                    if (Convert.ToDecimal(appliedDays) > Convert.ToDecimal(lblLeaveBal.Text))
                    {
                        Message1("Sorry, Applied days cannot be more than the available leave days");
                        TxtAppliedDays.Focus();
                        return;
                    }
                }
                else
                {
                    Message1("Sorry, Your Leave balance is exhausted, Consult your system administrator");
                }



                #region Validation

                DateTime startingDate;




                if (string.IsNullOrEmpty(appliedDays))
                {
                    Message1("Please enter the applied days.");
                    TxtAppliedDays.Focus();
                    return;
                }
                if (!MyComponents.IsNumeric(appliedDays))
                {
                    Message1("Applied days accepts numeric numbers only.");
                    TxtAppliedDays.Focus();
                    return;
                }
                if (Convert.ToDecimal(appliedDays) < 1)
                {
                    Message1("Applied days cannot be less than 1");
                    TxtAppliedDays.Focus();
                    return;
                }
                if (Convert.ToDecimal(appliedDays) > Convert.ToDecimal(lblLeaveBal.Text))
                {
                    Message1("Days applied cannot be more than the leave balance.");
                    TxtAppliedDays.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtStartDate.Text))
                {
                    Message1("Please select the start date.");
                    //dtPicker.Focus();
                    return;
                }
                else
                {
                    startingDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                var purpose = TxtPurpose.Text.Replace("'", "").Trim();
                if (purpose.Length > 200)
                {
                    Message("Description cannot have more than 200 characters.");
                    //Message("");
                    TxtPurpose.Focus();
                    return;
                }

                #endregion

                #region SendforApproval

                try
                {
                    string rtnMsg = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        rtnMsg = MyComponents.HrService.HRLeaveScheduling(Session["username"].ToString(), DdLeaveType.SelectedValue, TxtPurpose.Text, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate));
                    }else
                    {
                        rtnMsg=MyComponents.HrServiceZarib.HRLeaveScheduling(Session["username"].ToString(), DdLeaveType.SelectedValue, TxtPurpose.Text, Convert.ToDecimal(appliedDays), Convert.ToDateTime(startingDate));
                    }
                    if (!string.IsNullOrEmpty(rtnMsg))
                    {
                        if (rtnMsg == "SUCCESS")
                        {
                            SuccessMessage("Leave schedule has been created successfully!");
                        }
                        else
                        {
                            Message("Failed to sent leave schedule for approval.");
                        }
                    }
                    else
                    {
                        Message("Failed to submit leave schedule application.");
                    }

                }
                catch (Exception ex)
                {
                    Message1("ERROR: " + ex.Message.ToString());
                    ex.Data.Clear();
                }
                #endregion  
            }
            catch (Exception ex)
            {
                Message1("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
            
        }
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveSchedules.aspx");
        }
        protected void DdLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsContinuous();

            LoadLeaveBal();

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
            string myPage = "LeaveSchedules.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
            Response.Redirect("LeaveSchedules.aspx");
        }
    }
}