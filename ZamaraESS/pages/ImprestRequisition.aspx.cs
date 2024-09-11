using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class ImprestRequisition : System.Web.UI.Page
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
                    btnImportLines.Visible = true;
                    newLines.Visible = true;
                    btnApproval.Visible = true;
                }
                else if (RecStatus == "Open" && paged == "old")
                {
                    btnImportLines.Visible = true;
                    newLines.Visible = true;

                    btnApproval.Visible = true;
                    DdlDestination.Enabled = true;
                    TxtAppliedDays.Enabled = true;
                    txtStartDate.Enabled = true;
                    TxtPurpose.Enabled = true;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                }
                else if (RecStatus == "Pending Approval")
                {
                    string DocNo = Session["ReqNo"].ToString();
                    string rtn = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                       rtn = MyComponents.FinService.CanCancelDocument(DocNo, TableID);
                    }
                    else
                    {
                        rtn = MyComponents.FinServiceZarib.CanCancelDocument(DocNo, TableID);
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
                    lbnAddLine.Visible = false;
                    lbnClose.Visible = false;
                    DdldestinationType.Enabled = false;
                    DdlDestination.Enabled = false;
                    TxtAppliedDays.Enabled = false;
                    txtStartDate.Enabled = false;
                    TxtDescription.Enabled = false;
                    TxtPurpose.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlCustomers.Enabled = false;
                    txtInvoicedAmount.Enabled = false;
                    txtBalance.Enabled = false;
                    TxtProspectName.Enabled = false;
                    TxtEstimatedRevenue.Enabled = false;
                    ddlPreferredHotel.Enabled = false;
                    ddlAlternativeHotel.Enabled = false;
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
                    lbnAddLine.Visible = false;
                    lbnClose.Visible = false;
                    DdldestinationType.Enabled = false;
                    DdlDestination.Enabled = false;
                    TxtAppliedDays.Enabled = false;
                    txtStartDate.Enabled = false;
                    TxtDescription.Enabled = false;
                    TxtPurpose.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlCustomers.Enabled = false;
                    txtInvoicedAmount.Enabled = false;
                    txtBalance.Enabled = false;
                    TxtProspectName.Enabled = false;
                    TxtEstimatedRevenue.Enabled = false;
                    ddlPreferredHotel.Enabled = false;
                    ddlAlternativeHotel.Enabled = false;
                    AttachmentForm.Visible = false;
                    btnApproval.Visible = false;
                    lbtnCancel.Visible = false;
                    Approvals.Visible = true;
                    Comments.Visible = true;
                    gvLines.Columns[7].Visible = false;
                    gvAttachmentLines.Columns[3].Visible = false;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                }

                if (paged == "old")
                {
                    MultiView1.ActiveViewIndex = 1;
                    txtReqNo.Text = Session["ReqNo"].ToString();
                    BindGridviewData(Request.QueryString["An"].ToString());
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }
                LoadDrops();
                //PopulateHeaderDetails();

                if (Request.QueryString["An"].ToString() != null)
                {
                    string ApprovalComment = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        ApprovalComment = MyComponents.FinService.GetPaymentApprovalComments(Request.QueryString["An"].ToString(), TableID);
                    }
                    else
                    {
                       ApprovalComment = MyComponents.FinServiceZarib.GetPaymentApprovalComments(Request.QueryString["An"].ToString(), TableID);
                    }
                        
                    if (!string.IsNullOrEmpty(ApprovalComment))
                    {
                        txtcomments.Text = ApprovalComment;
                        lblApprovalComment.Visible = true;
                    }
                }
            }
        }
        protected void LoadDrops()
        {
            LoadHotels();
            LoadPaymentTypes();
            LoadDepartments();
            LoadCustomer();
        }

        protected void LoadDestinations(string DestinationType)
        {
            try
            {
                this.DdlDestination.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Destination--", "");
                this.DdlDestination.Items.Add(li);

                string DType = "";
                if (DestinationType == "0")
                {
                    DType = "Local";
                }
                else if (DestinationType == "1")
                {
                    DType = "International";
                }
                else
                {
                    return;
                }
                var data = MyComponents.OdataService.GetDestinations.AddQueryOption("$filter", "Type eq '" + DType + "'");

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {
                    if (alreadyEncountered.Contains(ym.Code))
                    {
                        continue;
                    }
                    li = new ListItem(
                                  ym.Name,
                                  ym.Code
                              );
                    this.DdlDestination.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadHotels()
        {
            try
            {
                ddlPreferredHotel.Items.Clear();
                ddlAlternativeHotel.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Hotel--", "");
                ddlPreferredHotel.Items.Add(li);
                ddlAlternativeHotel.Items.Add(li);

                var data = MyComponents.OdataService.GetHotels.Execute();

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
                    ddlPreferredHotel.Items.Add(li);
                    ddlAlternativeHotel.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void LoadDepartments()
        {
            try
            {
                this.ddlDepartment.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Please Select--", "0");
                this.ddlDepartment.Items.Add(li);

                var data = MyComponents.OdataService.GetDepartments.Execute();
                foreach (var collectionz in data)
                {
                    li = new ListItem(
                                   collectionz.Name,
                                   collectionz.Code
                               );

                    this.ddlDepartment.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void LoadCustomer()
        {
            try
            {
                this.ddlCustomers.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Please Select--", "0");
                this.ddlCustomers.Items.Add(li);

                var data = MyComponents.OdataService.GetCustomers.Execute();
                foreach (var collectionz in data)
                {
                    li = new ListItem(
                                   collectionz.Name,
                                   collectionz.No
                               );

                    this.ddlCustomers.Items.Add(li);
                }
            }
            catch (Exception ex)
            {Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
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
            {Message("ERROR:" + ex.Message.ToString());
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
                Message("ERROR:" + exception.Message.ToString());
                exception.Data.Clear();
            }
            return htmlStr;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string AppNo = Session["ReqNo"].ToString();
                string userID = Session["username"].ToString();
                string description = TxtDescription.Text.ToString();
                string purpose = TxtPurpose.Text.ToString();
                string destinationType = DdldestinationType.SelectedValue;
                string destination = DdlDestination.SelectedValue;
                string preferredHotel = ddlPreferredHotel.SelectedValue;
                string alternativeHotel = ddlAlternativeHotel.SelectedValue;
                string category = ddlCategory.SelectedValue;
                string customer = ddlCustomers.SelectedValue;
                string prospectName = TxtProspectName.Text.ToString();
                string estimatedRevenue = TxtEstimatedRevenue.Text.ToString();
                string appliedDays = TxtAppliedDays.Text.ToString();
                string startdate = txtStartDate.Text.ToString();
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
                if (string.IsNullOrEmpty(purpose))
                {
                    Message("Please enter Purpose.");
                    TxtPurpose.Focus();
                    return;
                }
                if (purpose.Length > 200)
                {
                    Message("Purpose cannot have more than 200 characters.");
                    TxtPurpose.Focus();
                    return;
                }

                if (ddlCategory.SelectedValue == "0")
                {
                    Message("Please Select Category.");
                    ddlCategory.Focus();
                    return;
                }
                else
               if (ddlCategory.SelectedValue == "1")
                {
                    if (customer == "0")
                    {
                        Message("Please Select Customer.");
                        ddlCustomers.Focus();
                        return;
                    }
                    estimatedRevenue = "0";
                }
                else
               if (ddlCategory.SelectedValue == "2")
                {
                    if (string.IsNullOrEmpty(prospectName))
                    {
                        Message("Please Enter Prospect Name.");
                        TxtProspectName.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(estimatedRevenue))
                    {
                        Message("Please Enter Estimated Revenue.");
                        TxtEstimatedRevenue.Focus();
                        return;
                    }
                    if (!MyComponents.IsNumeric(estimatedRevenue))
                    {
                        Message("Estimated Revenue Must be Numeric.");
                        TxtEstimatedRevenue.Focus();
                        return;
                    }
                }
                else
               if (ddlCategory.SelectedValue == "3")
                {
                    estimatedRevenue = "0";
                }
                if (string.IsNullOrEmpty(destinationType))
                {
                    Message("Please select Destination Type.");
                    DdldestinationType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(destination))
                {
                    Message("Please select Destination.");
                    DdlDestination.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(preferredHotel))
                {
                    Message("Please select preferred hotel.");
                    ddlPreferredHotel.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(alternativeHotel))
                {
                    Message("Please select alternative hotel.");
                    ddlAlternativeHotel.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(appliedDays))
                {
                    Message("Please enter the applied days.");
                    TxtAppliedDays.Focus();
                    return;
                }
                if (!MyComponents.IsNumeric(appliedDays))
                {
                    Message("Applied days accepts numeric numbers only.");
                    TxtAppliedDays.Focus();
                    return;
                }
                if (Convert.ToDecimal(appliedDays) < 1)
                {
                    Message("Applied days cannot be less than 1");
                    TxtAppliedDays.Focus();
                    return;
                }

                DateTime startingDate;

                if (string.IsNullOrEmpty(startdate))
                {
                    Message("Please select the start date.");
                    txtStartDate.Focus();
                    return;
                }
                else
                {
                    startingDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                string ReqNo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    ReqNo = MyComponents.FinService.CreateImprestHeader(AppNo, userID, description, purpose, Convert.ToInt32(destinationType), destination, Convert.ToInt32(appliedDays), Convert.ToDateTime(startingDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), preferredHotel, alternativeHotel);
                }
                else
                {
                    ReqNo = MyComponents.FinServiceZarib.CreateImprestHeader(AppNo, userID, description, purpose, Convert.ToInt32(destinationType), destination, Convert.ToInt32(appliedDays), Convert.ToDateTime(startingDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), preferredHotel, alternativeHotel);
                }
                   

                if (!String.IsNullOrEmpty(ReqNo))
                {
                    Session["ReqNo"] = ReqNo;
                    txtReqNo.Text = Session["ReqNo"].ToString();
                    newView();
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void btnLine_Click(object sender, EventArgs e)
        {
            try
            {

                string expenditureType = ddExpentitureType.SelectedValue;
                string dailyRate = txtdailyRate.Text;
                bool recoverable = false;
                if (chkRecoverable.Checked)
                {
                    recoverable = true;
                }
                else
                {
                    recoverable = false;
                }
                string dept = ddlDepartment.SelectedValue;
                if (String.IsNullOrEmpty(expenditureType))
                {
                    Message("Please Select Expenditure Type.");
                    ddExpentitureType.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(dailyRate))
                {
                    Message("Please Enter Daily Rate.");
                    txtdailyRate.Focus();
                    return;
                }
                if (!MyComponents.IsNumeric(dailyRate))
                {
                    Message("Daily Rate accepts numeric numbers only.");
                    txtdailyRate.Focus();
                    return;
                }
                if (Convert.ToDecimal(dailyRate) < 1)
                {
                    Message("Daily Rate MUST be greater than or equal to 1.");
                    txtdailyRate.Focus();
                    return;
                }
                if (dept == "0")
                {
                    Message("Please Select Department.");
                    ddlDepartment.Focus();
                    return;
                }


                #region commented - using webservice
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.FinService.InsertImprestLine(Session["ReqNo"].ToString(), expenditureType, Convert.ToDecimal(dailyRate), recoverable, dept);
                }
                else
                {
                    rtnMsg = MyComponents.FinServiceZarib.InsertImprestLine(Session["ReqNo"].ToString(), expenditureType, Convert.ToDecimal(dailyRate), recoverable, dept);
                }
                  

                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "SUCCESS")
                    {
                        BindGridviewData(Session["ReqNo"].ToString());
                        ddExpentitureType.SelectedIndex = 0;
                        txtdailyRate.Text = null;
                        chkRecoverable.Checked = false;
                    }
                    else
                    {
                        Message(rtnMsg);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Populate Balance and Invoiced Amount
            string customer = ddlCustomers.SelectedValue;
            string dept = Session["Department"].ToString();
            string staffLoginInfo = "";
            if (Session["Company"].ToString() == "ZAAC")
            {
                staffLoginInfo = MyComponents.ProcService.GetCustomerBalance(customer);
            }
            else
            {
                staffLoginInfo = MyComponents.ProcServiceZarib.GetCustomerBalance(customer);
            }

            if (!String.IsNullOrEmpty(staffLoginInfo))
            {
                string invoicedAmount = "", balance = "";
                string[] strdelimiters = new string[] { "::" };
                string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                invoicedAmount = staffLoginInfo_arr[0];
                balance = staffLoginInfo_arr[1];

                txtInvoicedAmount.Text = string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(invoicedAmount).ToString(CultureInfo.InvariantCulture)));
                txtBalance.Text = string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(balance).ToString(CultureInfo.InvariantCulture)));
            }

        }
        protected void PopulateHeaderDetails()
        {
            try
            {
                var data = MyComponents.OdataService.GetImprestHeader.AddQueryOption("$filter", "No eq '" + Session["ReqNo"].ToString() + "'");
                int counter = 0;
                foreach (var collectionz in data)
                {
                    TxtDescription.Text = collectionz.Description;
                    TxtPurpose.Text = collectionz.Purpose;
                    TxtAppliedDays.Text = collectionz.NoofDays.ToString();
                    txtStartDate.Text = Convert.ToDateTime(Microsoft.OData.Edm.Date.Parse(collectionz.StartDate.ToString())).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    string Category = "";
                    if (collectionz.Category == "Customer")
                    {
                        Category = "1";
                        CustomerName.Visible = true;
                        InvoicedAmount.Visible = true;
                        Balance.Visible = true;
                        ProspectName.Visible = false;
                        EstimatedRevenue.Visible = false;
                    }
                    else
                    if (collectionz.Category == "Prospect")
                    {
                        Category = "2";
                        ProspectName.Visible = true;
                        EstimatedRevenue.Visible = true;
                        CustomerName.Visible = false;
                        InvoicedAmount.Visible = false;
                        Balance.Visible = false;
                    }
                    else
                    if (collectionz.Category == "Internal")
                    {
                        Category = "3";
                        CustomerName.Visible = false;
                        InvoicedAmount.Visible = false;
                        Balance.Visible = false;
                        ProspectName.Visible = false;
                        EstimatedRevenue.Visible = false;
                    }
                    else

                    if (collectionz.Category == " ")
                    {
                        Category = "0";
                        CustomerName.Visible = false;
                        InvoicedAmount.Visible = false;
                        Balance.Visible = false;
                        ProspectName.Visible = false;
                        EstimatedRevenue.Visible = false;
                    }
                    string DestinationType = "";
                    if (collectionz.DestinationType == "Local")
                    {
                        DestinationType = "0";
                    }
                    else
                    if (collectionz.DestinationType == "International")
                    {
                        DestinationType = "1";
                    }
                    else

                    if (collectionz.DestinationType == " ")
                    {
                        DestinationType = "";
                    }


                    ddlCategory.SelectedValue = Category;
                    if (!string.IsNullOrEmpty(collectionz.CustomerNo))
                    {
                        ddlCustomers.SelectedValue = collectionz.CustomerNo;
                    }
                    txtInvoicedAmount.Text = collectionz.InvoicedAmount.ToString();
                    txtBalance.Text = collectionz.Balance.ToString();
                    TxtProspectName.Text = collectionz.ProspectName.ToString();
                    TxtEstimatedRevenue.Text = collectionz.EstimatedRevenue.ToString();
                    DdldestinationType.SelectedValue = DestinationType;
                    DdldestinationType_SelectedIndexChanged(this, EventArgs.Empty);
                    DdlDestination.SelectedValue = collectionz.Destination;
                    ddlPreferredHotel.SelectedValue = collectionz.PreferredHotel;
                    ddlAlternativeHotel.SelectedValue = collectionz.AlternativeHotel;
                }

                //Populate Balance and Invoiced Amount
                string customer = ddlCustomers.SelectedValue;
                string dept = Session["Department"].ToString();
                string staffLoginInfo = MyComponents.ProcService.GetCustomerBalance(customer);

                if (!String.IsNullOrEmpty(staffLoginInfo))
                {
                    string invoicedAmount = "", balance = "";
                    string[] strdelimiters = new string[] { "::" };
                    string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

                    invoicedAmount = staffLoginInfo_arr[0];
                    balance = staffLoginInfo_arr[1];

                    txtInvoicedAmount.Text = string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(invoicedAmount).ToString(CultureInfo.InvariantCulture)));
                    txtBalance.Text = string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(balance).ToString(CultureInfo.InvariantCulture)));
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            LoadDrops();
            PopulateHeaderDetails();
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
                                string rtnMsg = "";
                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg = MyComponents.FinService.DocumentAttachment(Session["ReqNo"].ToString(), base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.FinServiceZarib.DocumentAttachment(Session["ReqNo"].ToString(), base64String, fileName);
                                }                                

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
                String rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.FinService.RemoveAttachment(DocNo, Convert.ToInt32(FileID));
                }
                else
                {
                    rtnMsg = MyComponents.FinServiceZarib.RemoveAttachment(DocNo, Convert.ToInt32(FileID));
                }
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
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }

        protected void lbnAddLine_Click(object sender, EventArgs e)
        {
            txtReqNo.Text = Session["ReqNo"].ToString();
            LoadPaymentTypes();
            newLines.Visible = true;
            lbnAddLine.Visible = false;
            lbnClose.Visible = true;
        }
        protected void lbnClose_Click(object sender, EventArgs e)
        {
            newLines.Visible = false;
            lbnAddLine.Visible = true;
            lbnClose.Visible = false;
        }
        protected void newView()
        {
            MultiView1.ActiveViewIndex = 1;
            newLines.Visible = true;
            lbnAddLine.Visible = false;
            txtReqNo.Text = Session["ReqNo"].ToString();
            LoadPaymentTypes();
            BindGridviewData(Session["ReqNo"].ToString());
        }
        protected void LoadPaymentTypes()
        {
            try
            {
                this.ddExpentitureType.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Please Select--", "");
                this.ddExpentitureType.Items.Add(li);

                var data = MyComponents.OdataService.GetPaymentTypes.Execute();

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
                    this.ddExpentitureType.Items.Add(li);

                    alreadyEncountered.Add(ym.Code);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }


        private void BindGridviewData(string number)
        {
            try
            {
                string ReqNo = Session["ReqNo"].ToString();

                var data = MyComponents.OdataService.GetImprestLines.AddQueryOption("$filter", "No eq '" + ReqNo + "' and PaymentType eq 'Imprest'");

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(data);

                gvLines.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                gvLines.DataBind();
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void Cancelz(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string LineNo = arg[0];
            string ReqNo = Session["ReqNo"].ToString();
            try
            {
                if (Session["Company"].ToString() == "ZAAC")
                {
                    MyComponents.FinService.RemoveInsertImprestLine(ReqNo, Convert.ToInt32(LineNo));
                }
                else
                {
                    MyComponents.FinServiceZarib.RemoveInsertImprestLine(ReqNo, Convert.ToInt32(LineNo));
                }
                //Message("Line deleted successfully");
                BindGridviewData(Session["ReqNo"].ToString());
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "ImprestListing.aspx";
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

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                int i = gvLines.Rows.Count;
                if (i == 0)
                {
                    Message("warning! Please add lines first!");
                    return;
                }
                string ReqNo = Session["ReqNo"].ToString().ToString();
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.FinService.ImprestApprovalRequest(ReqNo);
                }
                else
                {
                    rtnMsg = MyComponents.FinServiceZarib.ImprestApprovalRequest(ReqNo);
                }
                    
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    if (rtnMsg == "SUCCESS")
                    {
                        SuccessMessage("Approval Request Sent Successfully.");
                    }
                    else
                    {
                        Message(rtnMsg);
                    }
                }
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
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.FinService.CancelImprestApprovalRequest(ReqNo);
                }
                else
                {
                    rtnMsg = MyComponents.FinServiceZarib.CancelImprestApprovalRequest(ReqNo);
                }
                    
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

        protected void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "0")
            {
                CustomerName.Visible = false;
                InvoicedAmount.Visible = false;
                Balance.Visible = false;
                ProspectName.Visible = false;
                EstimatedRevenue.Visible = false;
                TxtProspectName.Text = null;
                TxtEstimatedRevenue.Text = null;
                ddlCustomers.SelectedIndex = 0;
                txtInvoicedAmount.Text = null;
                txtBalance.Text = null;
            }
            else
            if (ddlCategory.SelectedValue == "1")
            {
                CustomerName.Visible = true;
                InvoicedAmount.Visible = true;
                Balance.Visible = true;
                ProspectName.Visible = false;
                EstimatedRevenue.Visible = false;
                TxtProspectName.Text = null;
                TxtEstimatedRevenue.Text = null;
            }
            else
            if (ddlCategory.SelectedValue == "2")
            {
                ProspectName.Visible = true;
                EstimatedRevenue.Visible = true;
                CustomerName.Visible = false;
                InvoicedAmount.Visible = false;
                Balance.Visible = false;
                ddlCustomers.SelectedIndex = 0;
                txtInvoicedAmount.Text = null;
                txtBalance.Text = null;
            }
            else
            if (ddlCategory.SelectedValue == "3")
            {
                CustomerName.Visible = false;
                InvoicedAmount.Visible = false;
                Balance.Visible = false;
                ProspectName.Visible = false;
                EstimatedRevenue.Visible = false;
                TxtProspectName.Text = null;
                TxtEstimatedRevenue.Text = null;
                ddlCustomers.SelectedIndex = 0;
                txtInvoicedAmount.Text = null;
                txtBalance.Text = null;
            }
        }
        protected void DdldestinationType_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadDestinations(DdldestinationType.SelectedValue);
        }

        protected void ddExpentitureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var data = MyComponents.OdataService.GetPaymentTypes.AddQueryOption("$filter", "Code eq '" + ddExpentitureType.SelectedValue + "'");

                foreach (var ym in data)
                {
                    txtdailyRate.Text = string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(ym.FixedAmount).ToString(CultureInfo.InvariantCulture)));
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
    }
}