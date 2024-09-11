using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class PurchaseReqLines : System.Web.UI.Page
    {
        public static int TableID = 60212; //Payment Table
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
                string Action = Request.QueryString["Ac"].ToString();
                Session["ReqNo"] = Request.QueryString["An"].ToString();
                if (Action == "new")
                {
                    CreateHeader();
                }
                LoadDrops();
                PopulateHeaderDetails();

                if (RecStatus == "Open" && paged == "new")
                {
                    btnImportLines.Visible = true;
                }
                if (RecStatus == "Open" && paged == "old")
                {
                    btnImportLines.Visible = true;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                    int i = gvLines.Rows.Count;
                    if (i > 0)
                    {
                        btnApproval.Visible = true;
                    }
                    else
                    {
                        btnApproval.Visible = false;
                    }
                }
                if (RecStatus == "Pending Approval")
                {
                    gvLines.Columns[11].Visible = false;
                    btnCancel.Visible = true;
                    gvAttachmentLines.Columns[4].Visible = false;
                    AttachmentForm.Visible = false;


                    ddlbudgetcode.Enabled = false;
                    deliverydate.Enabled = false;
                    description.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlCustomers.Enabled = false;
                    txtInvoicedAmount.Enabled = false;
                    txtBalance.Enabled = false;
                    TxtProspectName.Enabled = false;
                    TxtEstimatedRevenue.Enabled = false;
                    Approvals.Visible = true;
                    Comments.Visible = true;
                }
                if (RecStatus == "Approved")
                {
                    gvLines.Columns[11].Visible = false;
                    gvAttachmentLines.Columns[4].Visible = false;
                    AttachmentForm.Visible = false;

                    ddlbudgetcode.Enabled = false;
                    deliverydate.Enabled = false;
                    description.Enabled = false;
                    ddlCategory.Enabled = false;
                    ddlCustomers.Enabled = false;
                    txtInvoicedAmount.Enabled = false;
                    txtBalance.Enabled = false;
                    TxtProspectName.Enabled = false;
                    TxtEstimatedRevenue.Enabled = false;
                    Approvals.Visible = true;
                    Comments.Visible = true;
                }
                if (RecStatus == "Rejected")
                {
                    btnImportLines.Visible = true;
                    BindGridviewData(Session["ReqNo"].ToString());
                    BindAttachmentGridviewData(Session["ReqNo"].ToString());
                    int i = gvLines.Rows.Count;
                    if (i > 0)
                    {
                        btnApproval.Visible = true;
                    }
                    else
                    {
                        btnApproval.Visible = false;
                    }
                    Approvals.Visible = true;
                    Comments.Visible = true;
                }

                if (paged == "old")
                {
                    MultiView1.ActiveViewIndex = 1;
                    Session["ReqNo"] = Request.QueryString["An"].ToString();
                    txtReqNo.Text = Session["ReqNo"].ToString();
                    BindGridviewData(Request.QueryString["An"].ToString());
                    BindAttachmentGridviewData(Request.QueryString["An"].ToString());
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    LoadDrops();
                }
                if (Request.QueryString["An"].ToString() != null)
                {
                    int TableId = 52167610;
                    string ApprovalComment = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        ApprovalComment = MyComponents.ProcService.GetPurchaseApprovalComments(Request.QueryString["An"].ToString(), TableId);
                    }
                    else
                    {
                        ApprovalComment = MyComponents.ProcServiceZarib.GetPurchaseApprovalComments(Request.QueryString["An"].ToString(), TableId);
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
            LoadBudgetCodes();
            LoadCustomers();
            LoadDepartments();
        }
        protected void LoadBudgetCodes()
        {
            try
            {
                this.ddlbudgetcode.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Please Select--", "0");
                this.ddlbudgetcode.Items.Add(li);

                var data = MyComponents.OdataService.GetBudgetCodes.Execute();
                foreach (var collectionz in data)
                {
                    li = new ListItem(
                                   collectionz.CurrentBudget,
                                   collectionz.CurrentBudget
                               );

                    this.ddlbudgetcode.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void LoadCustomers()
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
                                  collectionz.No + " - " + collectionz.Name,
                                   collectionz.No
                               );

                    this.ddlCustomers.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void LoadItems(string Linetype)
        {
            try
            {
                if (Linetype == "0")//Direct Expense
                {
                    this.ddlItems.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Please Select--", "0");
                    this.ddlItems.Items.Add(li);

                    var data = MyComponents.OdataService.GetGLAccounts.Execute();
                    foreach (var collectionz in data)
                    {
                        li = new ListItem(
                                       collectionz.Name,
                                       collectionz.No
                                   );

                        this.ddlItems.Items.Add(li);
                    }
                }
                else if (Linetype == "1")//Inventory Item
                {
                    this.ddlItems.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Please Select--", "0");
                    this.ddlItems.Items.Add(li);

                    var data = MyComponents.OdataService.GetItems.Execute();
                    foreach (var collectionz in data)
                    {
                        li = new ListItem(
                                       collectionz.Description,
                                       collectionz.No_
                                   );

                        this.ddlItems.Items.Add(li);
                    }
                }
                else if (Linetype == "2")//Fixed Asset
                {
                    this.ddlItems.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Please Select--", "0");
                    this.ddlItems.Items.Add(li);

                    var data = MyComponents.OdataService.GetFixedAsset.Execute();
                    foreach (var collectionz in data)
                    {
                        li = new ListItem(
                                       collectionz.Description,
                                       collectionz.No
                                   );

                        this.ddlItems.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
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
        protected void PopulateHeaderDetails()
        {
            try
            {
                var data = MyComponents.OdataService.GetPurchaseHeader.AddQueryOption("$filter", "No eq '" + Session["ReqNo"].ToString() + "'");
                int counter = 0;
                foreach (var collectionz in data)
                {
                    ddlbudgetcode.SelectedValue = collectionz.BudgetCode;
                    deliverydate.Text = Convert.ToDateTime(Microsoft.OData.Edm.Date.Parse(collectionz.ExpectedDeliveryDate.ToString())).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

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



                    ddlCategory.SelectedValue = Category;
                    if(!string.IsNullOrEmpty(collectionz.CustomerNo))
                    {
                        ddlCustomers.SelectedValue = collectionz.CustomerNo;
                    }
                    txtInvoicedAmount.Text = collectionz.InvoicedAmount.ToString();
                    txtBalance.Text = collectionz.Balance.ToString();
                    TxtProspectName.Text = collectionz.ProspectName.ToString();
                    TxtEstimatedRevenue.Text = collectionz.EstimatedRevenue.ToString();
                    description.Text = collectionz.Description;
                }

                //Populate Balance and Invoiced Amount
                string customer = ddlCustomers.SelectedValue;
                string dept = Session["Department"].ToString();
                string staffLoginInfo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    staffLoginInfo = MyComponents.ProcService.GetCustomerBalance(customer);
                }else
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
            catch (Exception ex)
            {
                Message("ERROR:" + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        private void BindGridviewData(string number)
        {
            try
            {
                var data = MyComponents.OdataService.GetPurchaseLine.AddQueryOption("$filter", "DocumentNo eq '" + number + "'");
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

        protected void CreateHeader()
        {
            try
            {
                string AppNo = "";
                if (Session["ReqNo"] != null)
                {
                    AppNo = Session["ReqNo"].ToString();
                }

                string userID = Session["username"].ToString();
                string BudgetCode = ddlbudgetcode.SelectedValue.ToString();
                string DeliveryDate = DateTime.Now.Date.ToString();
                string category = "0";
                string customer = ddlCustomers.SelectedValue;
                string prospectName = TxtProspectName.Text.ToString();
                string estimatedRevenue = "0";
                string Description = description.Text.ToString();

                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.ProcService.PurchaseHeaderCreate(AppNo, userID, BudgetCode, Convert.ToDateTime(DeliveryDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), Description);
                }
                else
                {
                    rtnMsg = MyComponents.ProcServiceZarib.PurchaseHeaderCreate(AppNo, userID, BudgetCode, Convert.ToDateTime(DeliveryDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), Description);
                }            
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    Session["ReqNo"] = rtnMsg;
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:"+ ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string AppNo = Session["ReqNo"].ToString();
                string userID = Session["username"].ToString();
                string BudgetCode = ddlbudgetcode.SelectedValue.ToString();
                string DeliveryDate = deliverydate.Text.ToString();
                string category = ddlCategory.SelectedValue;
                string customer = ddlCustomers.SelectedValue;
                string prospectName = TxtProspectName.Text.ToString();
                string estimatedRevenue = TxtEstimatedRevenue.Text.ToString();
                string Description = description.Text.ToString();

                if (BudgetCode == "0")
                {
                    Message("Please Select Budget Code.");
                    ddlbudgetcode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DeliveryDate))
                {
                    Message("Please Select Expected Delivery Date.");
                    deliverydate.Focus();
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

                if (string.IsNullOrEmpty(Description))
                {
                    Message("Please Enter Description.");
                    description.Focus();
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.ProcService.PurchaseHeaderCreate(AppNo, userID, BudgetCode, Convert.ToDateTime(DeliveryDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), Description);
                }
                else
                {
                    rtnMsg = MyComponents.ProcServiceZarib.PurchaseHeaderCreate(AppNo, userID, BudgetCode, Convert.ToDateTime(DeliveryDate), Convert.ToInt32(category), customer, prospectName, Convert.ToDecimal(estimatedRevenue), Description);
                }
                if (!String.IsNullOrEmpty(Session["ReqNo"].ToString()))
                {
                    Session["BudgetCode"] = BudgetCode;
                    txtReqNo.Text = Session["ReqNo"].ToString();
                    newView();
                }
                else
                {
                    Message("ERROR: Failed to create purchase requisition.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Message("ERROR:"+ ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                int i = gvLines.Rows.Count;
                if (i == 0)
                {
                    Message("Warning! Please add lines first!");
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.ProcService.PrnApprovalRequest(Session["ReqNo"].ToString());
                }
                else
                {
                    rtnMsg = MyComponents.ProcServiceZarib.PrnApprovalRequest(Session["ReqNo"].ToString());
                }
                SuccessMessage(rtnMsg);
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.ProcService.CancelPrnApprovalRequest(Session["ReqNo"].ToString());
                }
                else
                {
                    rtnMsg = MyComponents.ProcServiceZarib.CancelPrnApprovalRequest(Session["ReqNo"].ToString());
                }
                SuccessMessage(rtnMsg);
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }


        protected void newView()
        {
            MultiView1.ActiveViewIndex = 1;
            BindGridviewData(Session["ReqNo"].ToString());
            BindAttachmentGridviewData(Session["ReqNo"].ToString());
        }

        private void Message(string p)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + p + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
        public void SuccessMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "PurchaseReqListing.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            LoadDrops();
            PopulateHeaderDetails();
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
                    MyComponents.ProcService.RemovePurchaseLine(ReqNo, Convert.ToInt32(LineNo));
                }
                else
                {
                    MyComponents.ProcServiceZarib.RemovePurchaseLine(ReqNo, Convert.ToInt32(LineNo));
                }
                Message("Line deleted successfully.");
                BindGridviewData(Session["ReqNo"].ToString());

                int j = gvLines.Rows.Count;
                if (j > 0)
                {
                    btnApproval.Visible = true;
                }
                else
                {
                    btnApproval.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }

        protected void ddlLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems(ddlLineType.SelectedValue);
        }
        protected void PopulateUoM(string ItemNo)
        {
            txtUoM.Text = null;
            var data = MyComponents.OdataService.GetItems.AddQueryOption("$filter", "No_ eq '" + ItemNo + "'");
            foreach (var collectionz in data)
            {
                txtUoM.Text = collectionz.Base_Unit_of_Measure.ToString();
            }
        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUoM(ddlItems.SelectedValue);
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


        //protected void ProcPlan_Click(object sender, EventArgs e)
        //{
        //    string RecStatus = Request.QueryString["status"].ToString();
        //    string paged = Request.QueryString["Tp"].ToString();

        //    string AppNo = "";
        //    if (paged == "new")
        //    {
        //        AppNo = Session["ReqNo"].ToString();
        //    }
        //    else
        //    {
        //        AppNo = Request.QueryString["An"].ToString();
        //    }
        //    string staffLoginInfo = MyComponents.ProcService.GetPurchReqHeaderDetails(AppNo);

        //    if (!String.IsNullOrEmpty(staffLoginInfo))
        //    {
        //        string BudgetCode = "";
        //        string[] strdelimiters = new string[] { "::" };
        //        string[] staffLoginInfo_arr = staffLoginInfo.Split(strdelimiters, StringSplitOptions.None);

        //        BudgetCode = staffLoginInfo_arr[2];
        //        Session["BudgetCode"] = BudgetCode;
        //    }

        //    Response.Redirect("ProcurementPlan.aspx?An=" + AppNo + "&status=" + RecStatus + "&Tp=" + paged);
        //}

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            string RecStatus = Request.QueryString["status"].ToString();
            string paged = Request.QueryString["Tp"].ToString();

            string AppNo = "";
            if (paged == "new")
            {
                AppNo = Session["ReqNo"].ToString();
            }
            else
            {
                AppNo = Request.QueryString["An"].ToString();
            }

            string LineType = ddlLineType.SelectedValue;
            string ItemNo = ddlItems.SelectedValue;
            string Quantity = txtQuantity.Text;
            string cost = txtcost.Text;
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

            if (ItemNo == "0")
            {
                Message("Please Select Item.");
                ddlItems.Focus();
                return;
            }
            if (string.IsNullOrEmpty(Quantity))
            {
                Message("Please Enter Quantity.");
                txtQuantity.Focus();
                return;
            }
            if (!MyComponents.IsNumeric(Quantity))
            {
                Message("Quantity MUST be Numeric.");
                txtQuantity.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cost))
            {
                Message("Please Enter Approx. Unit Cost.");
                txtcost.Focus();
                return;
            }
            if (!MyComponents.IsNumeric(cost))
            {
                Message("Approx. Unit Cost MUST be Numeric.");
                txtcost.Focus();
                return;
            }
            if (dept == "0")
            {
                Message("Please Select Department.");
                ddlDepartment.Focus();
                return;
            }
            string staffLoginInfo = "";
            if (Session["Company"].ToString() == "ZAAC")
            {
                staffLoginInfo = MyComponents.ProcService.InsertPurchaseReqLine(AppNo, Convert.ToInt32(LineType), ItemNo, Convert.ToDecimal(Quantity), Convert.ToDecimal(cost), recoverable, dept);
            }
            else
            {
                staffLoginInfo = MyComponents.ProcServiceZarib.InsertPurchaseReqLine(AppNo, Convert.ToInt32(LineType), ItemNo, Convert.ToDecimal(Quantity), Convert.ToDecimal(cost), recoverable, dept);
            }
                

            if (staffLoginInfo == "SUCCESS")
            {
                BindGridviewData(AppNo);

                ddlLineType.SelectedIndex = 0;
                ddlItems.SelectedIndex = 0;
                txtUoM.Text = null;
                txtQuantity.Text = null;
                txtcost.Text = null;
                chkRecoverable.Checked = false;
            }
            else
            {
                Message(staffLoginInfo);
            }
            int i = gvLines.Rows.Count;
            if (i > 0)
            {
                btnApproval.Visible = true;
            }
            else
            {
                btnApproval.Visible = false;
            }
        }
        protected void btnLine_Click(object sender, EventArgs e)
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
                                    rtnMsg = MyComponents.ProcService.DocumentAttachment(Session["ReqNo"].ToString(), base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.ProcServiceZarib.DocumentAttachment(Session["ReqNo"].ToString(), base64String, fileName);
                                }
                                    if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        Message("File Uploaded successfully!");
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

            string message = "Are you sure you want to remove this file?";
            ClientScript.RegisterOnSubmitStatement(this.GetType(), "confirm", "return confirm('" + message + "');");
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = Session["ReqNo"].ToString();
            try
            {
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.ProcService.RemoveAttachment(DocNo, Convert.ToInt32(FileID));
                }
                else
                {
                    rtnMsg = MyComponents.ProcServiceZarib.RemoveAttachment(DocNo, Convert.ToInt32(FileID));
                }
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
            //Bind Attachment Lines
            try
            {
                var data = MyComponents.OdataService.GetAttachmentLines.AddQueryOption("$filter", "No eq '" + number + "'");
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
        protected string Jobs()
        {
            var htmlStr = string.Empty;
            try
            {
                string docNo = Session["ReqNo"].ToString();

                var data = MyComponents.OdataService.GetApprovalEntries.AddQueryOption("$filter", "DocumentNo eq '" + docNo + "'");
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
                        collectionz.Status.ToString(),
                        collectionz.DateTimeSentforApproval
                        );
                }
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
            return htmlStr;
        }
        protected string Jobz()
        {
            var htmlStr = string.Empty;
            try
            {
                string docNo = Session["ReqNo"].ToString();
                string RecordID = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RecordID = MyComponents.ProcService.GetRecordID(docNo, TableID);
                }
                else
                {
                    RecordID = MyComponents.ProcServiceZarib.GetRecordID(docNo, TableID);
                }
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

        public void RedirectMessage(string strMsg)
        {
            string strScript = null;
            string myPage = "Dashboard.aspx";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
    }
}