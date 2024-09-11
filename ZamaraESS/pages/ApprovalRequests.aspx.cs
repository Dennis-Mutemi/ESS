using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class Approvals_LeaveRecalls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ActionPassed"] != null)
            {
                int ActionState = (Int32)Session["ActionPassed"];
                if (ActionState == 1)
                {
                    dvMdlContentRejConfirm.Visible = false;
                    dvMdlContentAprConfirm.Visible = true;
                }
                else if (ActionState == 2)
                {
                    dvMdlContentAprConfirm.Visible = false;
                    dvMdlContentRejConfirm.Visible = true;
                }
                Session["ActionPassed"] = null;
                ScriptManager.RegisterStartupScript(this, GetType(), "apMsg", "$(function(){ $('.msg-modal').modal('show'); })", true);
            }
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //LeaveApprovalsList();
                MultiView1.SetActiveView(View1);
            }
        }

        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "ApprovalRequests.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }

        //private void LeaveApprovalsList()
        //{
        //    try
        //    {
        //        string UserID = Session["userID"].ToString();
        //        string TableID = "60335"; //Leave Application

        //        var data = MyComponents.OdataService.GetApprovalEntriesHR.AddQueryOption("$filter", "ApproverID eq '" + UserID + "' and TableID eq '" + TableID.ToString() + "'");

        //        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        string json = serializer.Serialize(data);

        //        gvLeaveApprovals.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
        //        gvLeaveApprovals.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Clear();
        //    }
        //}

        protected void gvLeaveApprovals_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            //int rowIndex = gvr.RowIndex + 1;
            int rowIndex = gvr.RowIndex;
            GridViewRow row = gvLeaveApprovals.Rows[rowIndex];
            string LeaveNo = gvr.Cells[1].Text;
            HttpContext.Current.Session["leaveNo"] = LeaveNo;

            if (e.CommandName.Equals("ViewDetails"))
            {
                LeaveAppDetails(LeaveNo);
                MultiView1.SetActiveView(View2);
            }
        }

        #region View Details

        protected string Jobs()
        {
            var htmlStr = string.Empty;
            try
            {
                string username = Session["username"].ToString();

                var data = MyComponents.OdataService.GetLeaveApplications.AddQueryOption("$filter", "EmployeeNo eq '" + username + "'");
                int counter = 0;
                foreach (var collectionz in data)
                {
                    counter++;
                    htmlStr += string.Format(@"<tr  class='small'>
                                                            <td>{0}</td>
                                                            <td>{1}</td>
                                                            <td>{2}</td>
                                                            <td>{3}</td>
                                                            <td>{4}</td>
                                                            <td>{5}</td>
                                                            <td>{6}</td>
                                                            <td>{7}</td>
                                                            <td>{8}</td>
                                                            <td>{9}</td>
                                                             <td class='small'>
                                                                   <b> <a href='LeaveApplication.aspx?An={1}&status={9}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Open</span></a></b>
                                                            </td>
                                                     </tr>",
                                                         counter,
                                                         collectionz.No_,
                                                         collectionz.Leave_Period,
                                                         collectionz.Leave_Type,
                                                         Convert.ToDouble(collectionz.No__of_Days).ToString(CultureInfo.InvariantCulture),
                                                         collectionz.Created_Date,
                                                         collectionz.Start_Date,
                                                         collectionz.End_Date,
                                                         collectionz.Return_Date,
                                                         collectionz.Status
                                                         );
                }
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
            return htmlStr;
        }



        protected void LeaveAppDetails(string LeaveAppCode)
        {
            string query = ""; string t = ""; string applNo = ""; DateTime appDate, from, to, returnDate;
            string empName = ""; string daysApplied = ""; Decimal days = 0; Boolean isNumber = false;
            string leaveType = "";
            try
            {
                var data = MyComponents.OdataService.GetLeaveApplications.AddQueryOption("$filter", "No_ eq '" + LeaveAppCode + "'");

                t = "<table id='trainAppDetails'  style='border: silver thin ridge; width: 100%;'>";

                foreach (var collectionz in data)
                {

                    appDate = Convert.ToDateTime(collectionz.Created_Date);
                    leaveType = collectionz.Leave_Type;
                    from = Convert.ToDateTime(collectionz.Start_Date);
                    to = Convert.ToDateTime(collectionz.End_Date);
                    returnDate = Convert.ToDateTime(collectionz.Return_Date);
                    daysApplied = collectionz.No__of_Days.ToString();
                    if (MyComponents.IsNumeric(daysApplied))
                    {
                        days = Convert.ToDecimal(daysApplied);
                        days = Math.Round(days, 2);
                        daysApplied = days.ToString();
                    }

                    t += "<tr><td>Document No.:</td><td> <strong>" + collectionz.No_ + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Application Date:</td><td> <strong>" + appDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Employee No.:</td><td> <strong>" + collectionz.EmployeeNo.ToString() + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Employee Name:</td><td> <strong>" + collectionz.EmployeeName.ToString() + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Leave Type:</td><td><strong>" + leaveType + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Days Applied:</td><td><strong>" + daysApplied + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Start Date:</td><td><strong>" + from.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>End Date:</td><td><strong>" + to.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Return Date:</td><td><strong>" + returnDate.ToString("D", CultureInfo.CreateSpecificCulture("en-US")) + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Reliever No.:</td><td> <strong>" + collectionz.RelieverNo.ToString() + "</strong></td>";
                    t += "</tr>";

                    t += "<tr><td>Reliever Name:</td><td> <strong>" + collectionz.RelieverName.ToString() + "</strong></td>";
                    t += "</tr>";


                }
                t = t + "</table>";


                gvDocumentDetails.Text = t;

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }


        #endregion


        protected void btnApprovalLeave(object sender, EventArgs e)
        {
            string Comments = txtApproverComments.Text.Trim();
            #region Validations
            //if (String.IsNullOrEmpty(Comments))
            //{
            //    Message("Please Enter Approval Comments.");
            //    txtApproverComments.Focus();
            //    return;
            //}

            #endregion

            try
            {
                string UserID = Session["userID"].ToString();
                int TableID = 60335; //Leave Application
                string DocNo = HttpContext.Current.Session["leaveNo"].ToString();

                if (!string.IsNullOrEmpty(Comments))
                {
                    InsertApproverComments(DocNo);
                }

                string rtnMsg = MyComponents.HrService.ApproveDocument(TableID, DocNo, UserID);
                if (!string.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "SUCCESS")
                    {
                        SuccessMessage("Document Approved Successfully.");
                    }
                }
                else
                {
                    Message("Operation failed. Please try again later.");
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
        }
        protected void btnCancelBooking(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void btnRejectlBooking(object sender, EventArgs e)
        {

            string Comments = txtApproverComments.Text.Trim();
            #region Validations
            if (String.IsNullOrEmpty(txtApproverComments.Text.Trim()))
            {
                Message("Please Enter Rejection Comment.");
                txtApproverComments.Focus();
                return;
            }

            #endregion

            try
            {
                string UserID = Session["userID"].ToString();
                int TableID = 60335; //Leave Application
                string DocNo = HttpContext.Current.Session["leaveNo"].ToString();

                InsertApproverComments(DocNo);


                string rtnMsg = MyComponents.HrService.RejectDocument(TableID, DocNo, UserID);
                if (!string.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "SUCCESS")
                    {
                        SuccessMessage("Document Rejected Successfully.");
                    }
                }
                else
                {
                    Message("Operation failed. Please try again later.");
                }

            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
        }

        public void InsertApproverComments(string DocNo)
        {
            string userID = Session["UserID"].ToString();
            try
            {
                if (txtApproverComments.Text.Length > 250)
                {
                    Message("Kindly note that the maximum length for Comments characters is 250, and you have exceeded.");
                    return;
                }

                string UserID = Session["userID"].ToString();

                string rtnMsg = MyComponents.HrService.InsertLeaveApprovalComments(UserID, DocNo, txtApproverComments.Text);
                if (rtnMsg != "SUCCESS")
                {
                    Message("Failed to insert comments. Please try again later.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
    }
}