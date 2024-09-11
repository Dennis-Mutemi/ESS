using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
namespace ZamaraESS.pages
{
    public partial class SurrenderLines : System.Web.UI.Page
    {
        public static string StaffName = "";
        public static string StaffUserId = "";
        public static int TableID = 60054; //Payment Table
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
                Session["ReqNo"] = Request.QueryString["An"].ToString();



                if (RecStatus == "Open" && paged == "new")
                {
                    btnApproval.Visible = true;
                    gvLines.Columns[7].Visible = false;
                }
                else if (RecStatus == "Open" && paged == "old")
                {
                    btnApproval.Visible = true;
                    DdlImprests.Enabled = true;
                    TxtDescription.Enabled = true;
                    gvLines.Columns[7].Visible = false;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                }
                else if (RecStatus == "Pending Approval")
                {
                    string DocNo = Session["ReqNo"].ToString();

                    string rtn = MyComponents.FinService.CanCancelDocument(DocNo, TableID);
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

                    DdlImprests.Enabled = false;
                    TxtDescription.Enabled = false;
                    AttachmentForm.Visible = false;
                    Approvals.Visible = true;
                    Comments.Visible = true;
                    gvLines.Columns[8].Visible = false;
                    gvAttachmentLines.Columns[3].Visible = false;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                }
                else if (RecStatus == "Approved")
                {
                    DdlImprests.Enabled = false;
                    TxtDescription.Enabled = false;
                    AttachmentForm.Visible = false;
                    btnApproval.Visible = false;
                    lbtnCancel.Visible = false;
                    Approvals.Visible = true;
                    Comments.Visible = true;
                    gvLines.Columns[8].Visible = false;
                    gvAttachmentLines.Columns[3].Visible = false;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                }

                if (paged == "old")
                {
                    MultiView1.ActiveViewIndex = 1;
                    BindGridviewData(Request.QueryString["An"].ToString());
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }


                //LoadMyImprests();

                if (Request.QueryString["An"].ToString() != null)
                {
                    string ApprovalComment = MyComponents.FinService.GetPaymentApprovalComments(Request.QueryString["An"].ToString(), TableID);
                    if (!string.IsNullOrEmpty(ApprovalComment))
                    {
                        txtcomments.Text = ApprovalComment;
                        lblApprovalComment.Visible = true;
                    }
                }
            }
        }

        protected void LoadMyImprests()
        {
            try
            {
                this.DdlImprests.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Please Select--", "");
                this.DdlImprests.Items.Add(li);


                string username = Session["username"].ToString();
                var data = MyComponents.OdataService.GetImprestApplication.AddQueryOption("$filter", "EmployeeNo eq '" + username + "' and PaymentType eq 'Imprest' and Posted eq true and Surrendered eq false");
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.No))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.No + " - " + ym.DestinationName,
                                  ym.No
                              );
                    this.DdlImprests.Items.Add(li);

                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }

        protected string Jobs()
        {
            var htmlStr = string.Empty;

            string ReqNo = Session["ReqNo"].ToString();
            try
            {
                var data = MyComponents.OdataService.GetApprovalEntries.AddQueryOption("$filter", "DocumentNo eq '" + ReqNo + "'");
                int counter = 0;
                foreach (var collectionz in data)
                {
                    counter++;
                    htmlStr += string.Format(@"<tr class='small'>
                                                            <td>{0}</td>
                                                            <td>{1}</td>
                                                            <td>{2}</td>
                                                            <td>{3}</td>
                                                            <td>{4}</td>
                                                     </tr>",
                                                        counter,
                                                        collectionz.DocumentNo,
                                                        collectionz.ApproverID,
                                                        collectionz.Status,
                                                        Microsoft.OData.Edm.Date.Parse(collectionz.DateTimeSentforApproval.ToString())
                                                        );
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return htmlStr;
        }
        protected string Jobz()
        {
            var htmlStr = string.Empty;
            try
            {
                string docNo = Session["ReqNo"].ToString();
                string RecordID = MyComponents.FinService.GetRecordID(docNo, TableID);

                var data = MyComponents.OdataService.GetApprovalComments.AddQueryOption("$filter", "RecordIDtoApprove eq '" + RecordID + "'");
                int counter = 0;
                foreach (var collectionz in data)
                {
                    counter++;

                    htmlStr += string.Format(@"<tr class='small'>
                                                            <td>{0}</td>
                                                            <td>{1}</td>
                                                            <td>{2}</td>
                                                            <td>{3}</td>
                                                            <td>{4}</td>
                                                     </tr>",
                        counter,
                        collectionz.DocumentNo,
                        collectionz.UserID,
                        collectionz.Comment.ToString(),
                        collectionz.DateandTime
                        );
                }
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
            return htmlStr;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string imprest = DdlImprests.SelectedValue;
                string description = TxtDescription.Text.ToString();

                if (string.IsNullOrEmpty(imprest))
                {
                    Message("Please select Imprest.");
                    DdlImprests.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(description))
                {
                    Message("Please enter Description.");
                    TxtDescription.Focus();
                    return;
                }
                if (description.Length > 200)
                {
                    Message("Description cannot have more than 200 characters.");
                    TxtDescription.Focus();
                    return;
                }

                string ReqNo = MyComponents.FinService.CreateImprestSurrender(Session["username"].ToString(), imprest, description);

                if (!String.IsNullOrEmpty(ReqNo))
                {
                    Session["ReqNo"] = ReqNo;
                    newView();
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
            Response.Redirect("SurrenderListing.aspx");
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

                            if (fileExt == "pdf" || fileExt == "docx" || fileExt == "xlsx" || fileExt == "txt" || fileExt == "png" || fileExt == "jpg")
                            {

                                System.IO.Stream fs = filetoupload.PostedFile.InputStream;
                                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);


                                //Save File Name to table
                                #region commented - using webservice
                                string rtnMsg = MyComponents.FinService.DocumentAttachment(Session["ReqNo"].ToString(), base64String, fileName);

                                if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        //Message("File Uploaded successfully!");
                                        BindAttachmentGridviewData(Session["ReqNo"].ToString());
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type alredy exists.");
                                        BindAttachmentGridviewData(Session["ReqNo"].ToString());
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
                                Message("Please select file of this type *.pdf, *.docx, *.xlsx, *.txt, *.png, *.jpg");
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
        protected void CancelAttachment(object sender, EventArgs e)
        {

            string message = "Are you sure you want to remove this attachment?";
            ClientScript.RegisterOnSubmitStatement(this.GetType(), "confirm", "return confirm('" + message + "');");
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = Session["ReqNo"].ToString();
            try
            {
                String rtnMsg = MyComponents.FinService.RemoveAttachment(DocNo, Convert.ToInt32(FileID));

                Message(rtnMsg);
                BindAttachmentGridviewData(Session["ReqNo"].ToString());
                //int i = gvAttachmentLines.Rows.Count;
                //if (i > 0)
                //{
                //    btnSubmitAttachments.Visible = true;
                //}
                //else
                //{
                //    btnSubmitAttachments.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
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
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        protected void newView()
        {
            MultiView1.ActiveViewIndex = 1;
            BindGridviewData(Session["ReqNo"].ToString());
        }

        private void BindGridviewData(string number)
        {
            try
            {
                string ReqNo = Session["ReqNo"].ToString();

                var data = MyComponents.OdataService.GetSurrenderLines.AddQueryOption("$filter", "No eq '" + ReqNo + "' and PaymentType eq 'Imprest Surrender'");

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);
                gvLines.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                gvLines.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            int i = gvLines.Rows.Count;
            if (i > 0)
            {
                foreach (GridViewRow gvr in this.gvLines.Rows)
                {
                    string LineNo = gvr.Cells[1].Text;
                    TextBox ActualAmountSpent = gvr.FindControl("txtActualAmountSpent") as TextBox;

                    string rtn = MyComponents.FinService.LoadActualLineAmountSpent(Session["ReqNo"].ToString(), Convert.ToInt32(LineNo));

                    ActualAmountSpent.Text = rtn;
                }
            }
        }

        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "SurrenderListing.aspx";
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
        protected void ModifySurrenderLine()
        {
            string rtnMsg = "";
            foreach (GridViewRow gvr in this.gvLines.Rows)
            {
                string LineNo = gvr.Cells[1].Text;
                string ExpenditureType = gvr.Cells[2].Text;
                string TotalAmount = gvr.Cells[6].Text;
                TextBox ActualAmountSpent = gvr.FindControl("txtActualAmountSpent") as TextBox;

                try
                {
                    string amountSpent = ActualAmountSpent.Text;

                    if (string.IsNullOrEmpty(amountSpent))
                    {
                        Message("Enter Actual Amount Spent for Expenditure Type: " + ExpenditureType);
                        ActualAmountSpent.Focus();
                        return;
                    }
                    if (!MyComponents.IsNumeric(amountSpent))
                    {
                        Message("Actual Amount Spent Must be Numeric for Expenditure Type: " + ExpenditureType);
                        ActualAmountSpent.Focus();
                        return;
                    }
                    if (Convert.ToDecimal(TotalAmount) < Convert.ToDecimal(amountSpent))
                    {
                        Message("Actual Amount Spent cannot be more than Total Amount for Expenditure Type: " + ExpenditureType);
                        ActualAmountSpent.Focus();
                        return;
                    }

                    rtnMsg = MyComponents.FinService.ModifyImprestSurrenderLine(Session["ReqNo"].ToString(), Convert.ToInt32(LineNo), Convert.ToDecimal(amountSpent));

                }
                catch (Exception ex)
                {
                    Message("ERROR: " + ex.Message.ToString());
                }
            }

            if (!String.IsNullOrEmpty(rtnMsg))
            {
                if (rtnMsg == "SUCCESS")
                {
                    int i = gvAttachmentLines.Rows.Count;
                    if (i == 0)
                    {
                        Message("warning! Please attach receipt(s).");
                        filetoupload.Focus();
                        return;
                    }

                    string ReqNo = Session["ReqNo"].ToString().ToString();
                    string rtn = MyComponents.FinService.ImprestSurrenderApprovalRequest(ReqNo);
                    if (!String.IsNullOrEmpty(rtn))
                    {
                        if (rtnMsg == "SUCCESS")
                        {
                            SuccessMessage("Approval Request Sent Successfully.");
                        }
                        else
                        {
                            Message(rtn);
                        }
                    }
                }
                else
                {
                    Message("ERROR: Failed to submit imprest surrender for approval.");
                }
            }
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                ModifySurrenderLine();
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string ReqNo = Session["ReqNo"].ToString().ToString();
                string rtnMsg = MyComponents.FinService.CancelImprestSurrenderApprovalRequest(ReqNo);
                if (rtnMsg == "SUCCESS")
                {
                    SuccessMessage("Approval Request Cancelled Successfully.");
                }
                else
                {
                    Message(rtnMsg);
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