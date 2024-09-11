using BC21;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;


namespace ZamaraESS.pages
{
    public partial class ReportIncident : System.Web.UI.Page
    {
        public static string StaffName = "";
        public static string StaffUserId = "";
        public static int TableID = 60480;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                LoadIncidentTypes();
                LoadEmployees();
                string paged = Request.QueryString["Tp"].ToString();
                string RecStatus = Request.QueryString["status"].ToString();
                Session["IncNo"] = Request.QueryString["An"].ToString();
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paged == "old")
                {
                    if (Request.QueryString["status"].ToString() == "New")
                    {
                        if (Request.QueryString["emp"].ToString()!= Session["username"].ToString())
                        {
                            MultiView1.ActiveViewIndex = 0;
                            ViewIncidentInformation(Request.QueryString["An"].ToString());                          
                            involvedpartdata.Columns[3].Visible = false;
                            gvAttachmentLines.Columns[3].Visible = false;
                            witnessdata.Columns[3].Visible = false;
                            filetoupload.Visible = false;
                            update.Visible = true;
                            create_next.Visible = false;
                            involvedpartycontrols.Visible = false;
                            witnesscontrols.Visible = false;
                            btnUpload.Visible = false;
                            lbnCloseparties.Visible = false;
                            witclose.Visible = false;
                            if (Session["HsCategory"].ToString() == "Disease")
                            {
                                occdtype.Enabled = false;
                            }
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex = 0;
                            EditIncidentInformation(Request.QueryString["An"].ToString());
                            Nsubumit.Visible = true;
                            update.Visible = true;
                            create_next.Visible = false;
                        }                        
                    }
                    else if (Request.QueryString["status"].ToString() == "Under Investigation")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewIncidentInformation(Request.QueryString["An"].ToString());                        
                        update.Visible = true;
                        create_next.Visible = false;
                        involvedpartdata.Columns[3].Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        witnessdata.Columns[3].Visible = false;
                        involvedpartycontrols.Visible= false;
                        witnesscontrols.Visible = false;
                        btnUpload.Visible = false;
                        lbnCloseparties.Visible = false;
                        witclose.Visible = false;
                        filetoupload.Visible = false;                       
                        if (Session["HsCategory"].ToString() == "Disease")
                        {
                            occdtype.Enabled = false;
                        }
                    }
                    else if (Request.QueryString["status"].ToString() == "Resolved")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewIncidentInformation(Request.QueryString["An"].ToString());                        
                        update.Visible = true;
                        create_next.Visible = false;
                        OSH.Visible = true;
                        OSHRemark.Enabled = false;
                        Amount.Visible = true;
                        involvedpartdata.Columns[3].Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        witnessdata.Columns[3].Visible = false;
                        involvedpartycontrols.Visible = false;
                        witnesscontrols.Visible = false;
                        btnUpload.Visible = false;
                        lbnCloseparties.Visible = false;
                        witclose.Visible = false;
                        filetoupload.Visible = false;
                        AlocatedAmount.Enabled = false;
                    }
                    else if (Request.QueryString["status"].ToString() == "Closed")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewIncidentInformation(Request.QueryString["An"].ToString());
                        OSH.Visible = true;
                        OSHRemark.Enabled = false;
                        Amount.Visible = true;
                        AlocatedAmount.Enabled = false;
                        involvedpartdata.Columns[3].Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        witnessdata.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        update.Visible = true;
                        create_next.Visible = false;
                        Amount.Visible = true;
                        involvedpartycontrols.Visible = false;
                        witnesscontrols.Visible = false;
                        btnUpload.Visible = false;
                        lbnCloseparties.Visible = false;
                        witclose.Visible = false;
                        if (Session["HsCategory"].ToString() == "Incident")
                        {
                            HrRem.Visible = true;
                            HrRemark.Enabled = false;
                        }else if (Session["HsCategory"].ToString() == "Disease")
                        {
                            occdtype.Enabled = false;
                        }
                    }
                    else if (Request.QueryString["status"].ToString() == "Rejected")
                    {
                        MultiView1.ActiveViewIndex = 0;
                        ViewIncidentInformation(Request.QueryString["An"].ToString());
                        OSH.Visible = true;
                        OSHRemark.Enabled = false;
                        involvedpartdata.Columns[3].Visible = false;
                        gvAttachmentLines.Columns[3].Visible = false;
                        filetoupload.Visible = false;
                        Amount.Visible = true;
                        AlocatedAmount.Enabled = false;
                        update.Visible = true;
                        create_next.Visible = false;
                        witnessdata.Columns[3].Visible = false;
                        involvedpartycontrols.Visible = false;
                        witnesscontrols.Visible = false;
                        btnUpload.Visible = false;
                        lbnCloseparties.Visible = false;
                        witclose.Visible = false;                        
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    Nsubumit.Visible = true;
                }
            }

        }
        protected void LoadIncidentTypes()
        {
            try
            {
                String Category = Session["HsCategory"].ToString();
                if (Category == "Accident")
                {
                    this.IncidentType.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Select Accident Type--", "");
                    this.IncidentType.Items.Add(li);

                    var data = MyComponents.OdataService.GetAccidentTypes.Execute();

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
                        this.IncidentType.Items.Add(li);

                        alreadyEncountered.Add(ym.Code);
                    }
                }
                else if (Category == "Incident")
                {
                    this.IncidentType.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Select Incident Type--", "");
                    this.IncidentType.Items.Add(li);

                    var data = MyComponents.OdataService.GetIncidentTypes.Execute();

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
                        this.IncidentType.Items.Add(li);

                        alreadyEncountered.Add(ym.Code);
                    }
                }
                else
                {
                    
                    this.IncidentType.Items.Clear();
                    ListItem li = null;
                    li = new ListItem("--Select Disease Type--", "");
                    this.IncidentType.Items.Add(li);

                    var data = MyComponents.OdataService.GetDiseaseTypes.Execute();

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
                        this.IncidentType.Items.Add(li);

                        alreadyEncountered.Add(ym.Code);
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
                this.EmployeeNo.Items.Clear();
                this.witnessNo.Items.Clear();
                ListItem li = null;
                li = new ListItem("--Select Employee--", "");
                this.EmployeeNo.Items.Add(li);
                this.witnessNo.Items.Add(li);

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
                    this.EmployeeNo.Items.Add(li);
                    this.witnessNo.Items.Add(li);

                    alreadyEncountered.Add(ym.No);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }

        protected void newView()
        {
            MultiView1.ActiveViewIndex = 1;
            involvedparties.Visible = true;
            witness.Visible = true;
            lbnAddLine.Visible = false;
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
        private void BindGridviewData(string number)
        {
            try
            {
                var involvedparty = MyComponents.OdataService.GetInvolvedParties.AddQueryOption("$filter", "Document_No eq '" + number + "'");
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string json = serializer.Serialize(involvedparty);

                involvedpartdata.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                involvedpartdata.DataBind();

                var witness = MyComponents.OdataService.GetIncidentLines.AddQueryOption("$filter", "DocumentNo eq '" + number + "'");
                var witnessserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string witnessjson = serializer.Serialize(witness);

                witnessdata.DataSource = JsonConvert.DeserializeObject<DataTable>(witnessjson);
                witnessdata.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string DocNo = txtReqNo.Text.ToString();
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
                                //Save File Name to table
                                #region commented - using webservice
                                string rtnMsg = "";
                                if (Session["Company"].ToString() == "ZAAC")
                                {
                                    rtnMsg = MyComponents.HrService.DocumentAttachment(6, FileType, DocNo, base64String, fileName);
                                }
                                else
                                {
                                    rtnMsg = MyComponents.HrServiceZarib.DocumentAttachment(6, FileType, DocNo, base64String, fileName);
                                }
                                if (!String.IsNullOrEmpty(rtnMsg))
                                {
                                    if (rtnMsg == "SUCCESS")
                                    {
                                        Message("File Uploaded successfully!");
                                        BindAttachmentGridviewData(DocNo);
                                    }
                                    else if (rtnMsg == "EXIST")
                                    {
                                        Message("FAILED: Document type alredy exists.");
                                        BindAttachmentGridviewData(Session["IncNo"].ToString());
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
        protected void RemoveAttachment(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = txtReqNo.Text.ToString();
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
                BindAttachmentGridviewData(DocNo);
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }       
        protected void ViewIncidentInformation(String number)
        {
            try
            {
                var data = MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "No eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    IncidentType.Text = collectionz.IncidentType.ToString();
                    IncidentType.SelectedValue = collectionz.IncidentType.ToString();
                    IncidentDate.Text = collectionz.IncidentDate.ToString();
                    IncidentTime.Value = collectionz.IncidentTime.ToString();
                    IncidentDesc.Text = collectionz.DetailedDescription.ToString();
                    OSHRemark.Text = collectionz.OSHRemarks.ToString();
                    AlocatedAmount.Text = collectionz.Amount.ToString();
                    IncidentType.Enabled = false;
                    IncidentLocation.Enabled = false;
                    IncidentDate.Enabled = false;
                    IncidentTime.Disabled = true;
                    IncidentDesc.Enabled = false;
                    location.Disabled = true;
                    if (Session["HsCategory"].ToString() == "Accident" || Session["HsCategory"].ToString() == "Disease")
                    {
                        if ((bool)collectionz.Withintheoffice)
                        {
                            location.Checked = true;
                        }
                        else
                        {
                            location.Checked = false;
                        }
                        occdtype.Text=collectionz.Occupationaldiseasetype.ToString();
                    }
                    else if(Session["HsCategory"].ToString() == "Incident")
                    {
                        IncidentLocation.Text = collectionz.Location.ToString();
                        HrRemark.Text=collectionz.HRRemarks.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void EditIncidentInformation(String number)
        {
            try
            {
                var data = MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "No eq '" + number + "'");
                foreach (var collectionz in data)
                {
                    IncidentDate.Text = collectionz.IncidentDate.ToString();
                    IncidentTime.Value = collectionz.IncidentTime.ToString();
                    IncidentDesc.Text = collectionz.DetailedDescription.ToString();
                    IncidentType.SelectedValue = collectionz.IncidentType.ToString();
                    occdtype.Text = collectionz.Occupationaldiseasetype.ToString();
                    if (Session["HsCategory"].ToString() == "Accident" || Session["HsCategory"].ToString() == "Disease")
                    {
                        if ((bool)collectionz.Withintheoffice)
                        {
                            location.Checked = true;
                        }
                        else
                        {
                            location.Checked = false;
                        }
                    }
                    else
                    {
                        IncidentLocation.Text=collectionz.Location.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
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

        protected void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');window.location.href = 'IncidentListing.aspx'",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }       

        protected void create_next_Click(object sender, EventArgs e)
        {

            string IncidentLoca = IncidentLocation.Text.ToString();
            string IncidentDescri = IncidentDesc.Text.ToString();
            string IncidentTyp = "";
            string occupationalDType = "";
            Boolean withofis = false;
            string IncidentDat = IncidentDate.Text.ToString();
            string IncidentTim = IncidentTime.Value.ToString();
            String Category = Session["HsCategory"].ToString();
            string EmpNo = Session["username"].ToString();
            int catpo;
            IncidentTyp = IncidentType.SelectedValue;
            try
            {

                if (location.Checked)
                {
                    withofis = true;
                }
                if (Session["HsCategory"].ToString() == "Incident")
                {
                    catpo = 1;
                    if (string.IsNullOrEmpty(IncidentLoca))
                    {
                        Message("Please enter incident Location");
                        IncidentLocation.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(IncidentTyp))
                    {
                        Message("Please select incident type");
                        IncidentType.Focus();
                        return;
                    }
                }
                else if (Session["HsCategory"].ToString() == "Accident")
                {

                    catpo = 2;
                    if (string.IsNullOrEmpty(IncidentTyp))
                    {
                        Message("Please select accident type");
                        IncidentType.Focus();
                        return;
                    }

                }
                else
                {
                    catpo = 3;
                    occupationalDType = occdtype.Text.ToString();
                    if (string.IsNullOrEmpty(occupationalDType) && string.IsNullOrEmpty(IncidentTyp))
                    {
                        Message("Please enter occupational disease type or enter other disease type");
                        IncidentType.Focus();
                        occdtype.Focus();
                        return;
                    }
                    else if (occupationalDType != "" && IncidentTyp != "")
                    {
                        Message("Please select either occupational disease type or enter other disease type");
                        IncidentType.Focus();
                        occdtype.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(IncidentDescri))
                {
                    Message("Please enter Description.");
                    IncidentDesc.Focus();
                    return;
                }

                DateTime Incidentdt, Incidenttm;

                if (string.IsNullOrEmpty(IncidentDat))
                {
                    Message("please enter date");
                    IncidentDate.Focus();
                    return;
                }
                else
                {
                    Incidentdt = DateTime.ParseExact(IncidentDat, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var todaysDate = DateTime.Today;

                    if (Incidentdt > todaysDate)
                    {
                        Message("Date cannot be a future date");
                        IncidentDate.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(IncidentTim))
                {
                    Message("Please enter incident time");
                    IncidentTime.Focus();
                    return;
                }
                else
                {
                    Incidenttm = Convert.ToDateTime(IncidentTim);
                }
                string IncidentNo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    IncidentNo = MyComponents.HrService.CreateIncident(EmpNo, IncidentTyp, IncidentLoca, IncidentDescri, Incidenttm, Incidentdt, catpo, occupationalDType, withofis);
                }
                else
                {
                    IncidentNo = MyComponents.HrServiceZarib.CreateIncident(EmpNo, IncidentTyp, IncidentLoca, IncidentDescri, Incidenttm, Incidentdt, catpo, occupationalDType, withofis);
                }
                   
                if (!String.IsNullOrEmpty(IncidentNo))
                {
                    Session["IncNo"] = IncidentNo;
                    txtReqNo.Text = Session["IncNo"].ToString();
                    uniqNo.Text = IncidentNo;
                    newView();
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void back_Click(object sender, EventArgs e)
        {
            string paged = Request.QueryString["Tp"].ToString();
            String IncNo;
            String Recstatus;
            if (paged != null && paged!="new")
            {
                IncNo = Request.QueryString["An"].ToString();
                Recstatus = Request.QueryString["status"].ToString();
            }
            else
            {
                IncNo = Session["IncNo"].ToString();
                Recstatus = "New";
            }
            if (Recstatus == "New")
            {
                EditIncidentInformation(IncNo);
                MultiView1.ActiveViewIndex = 0;
                update.Visible = true;
                create_next.Visible = false;
            }
            else
            {
                ViewIncidentInformation(IncNo);
                MultiView1.ActiveViewIndex = 0;
            }
        }
        protected void listback_Click(object sender, EventArgs e)
        {
            String Category = Session["HsCategory"].ToString();
            string RecStatus = string.Empty;
            if (Category != null)
            {
                if (Category == "Incident")
                {
                    RecStatus = "in";
                }
                else if (Category == "Accident")
                {
                    RecStatus = "ac";
                }
                else
                {
                    RecStatus = "od";
                }
            }
            Response.Redirect("IncidentListing.aspx?ty=" + RecStatus);
        }
        protected void lbnCloseparties_Click(object sender, EventArgs e)
        {
            involvedparties.Visible = false;
            lbnAddLine.Visible = true;
            lbnCloseparties.Visible = false;
            involvedpartdata.Visible = false;
        }

        protected void witclose_Click(object sender, EventArgs e)
        {
            witness.Visible = false;
            newwitness.Visible = true;
            witclose.Visible = false;
            witnessdata.Visible = false;            
        }
        protected void update_Click(object sender, EventArgs e)
        {
            string IncidentLoca = IncidentLocation.Text.ToString();
            string IncidentDescri = IncidentDesc.Text.ToString();
            string IncidentTyp = "";
            Boolean withofis = false;
            String Category = Session["HsCategory"].ToString();            
            string ocpatdiseType = "";
            IncidentTyp = IncidentType.SelectedValue;
            ocpatdiseType = occdtype.Text.ToString();
            try
            {
                if (location.Checked)
                {
                    withofis = true;
                }
                if (Session["HsCategory"].ToString() == "Accident")
                {
                    if (string.IsNullOrEmpty(IncidentTyp))
                    {
                        Message("Please select accident type");
                        IncidentType.Focus();
                        return;
                    }
                }
                else if (Session["HsCategory"].ToString() == "Disease")
                {
                    if (IncidentTyp == "" && ocpatdiseType == "")
                    {
                        Message("Please select disease type or enter other disease type");
                        occdtype.Focus();
                        return;
                    }
                    else if (IncidentTyp != "" && ocpatdiseType != "")
                    {
                        Message("Please select either disease type or enter other disease type");
                        IncidentType.Focus();
                        occdtype.Focus();
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(IncidentTyp))
                    {
                        Message("Please select incident type");
                        IncidentType.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(IncidentLoca))
                    {
                        Message("Please enter incident Location");
                        IncidentLocation.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(IncidentDescri))
                {
                    Message("Please enter Description.");
                    IncidentDesc.Focus();
                    return;
                }
                DateTime Incidentdt, Incidenttm;
                Incidentdt = DateTime.ParseExact(IncidentDate.Text.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Incidenttm = Convert.ToDateTime(IncidentTime.Value.ToString());

                if (string.IsNullOrEmpty(IncidentDate.Text.ToString()))
                {
                    Message("please enter date");
                    IncidentDate.Focus();
                    return;
                }
                else
                {
                    var todaysDate = DateTime.Today;

                    if (Incidentdt > todaysDate)
                    {
                        Message("Date cannot be a future date");
                        IncidentDate.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(IncidentTime.Value.ToString()))
                {
                    Message("Please enter time");
                    IncidentTime.Focus();
                    return;
                }
                string paged = Request.QueryString["Tp"].ToString();
                String IncNo;
                String Recstatus;
                if (paged != null && paged != "new")
                {
                    IncNo = Request.QueryString["An"].ToString();
                    Recstatus = Request.QueryString["status"].ToString();
                }
                else
                {
                    IncNo = Session["IncNo"].ToString();
                    Recstatus = "New";
                }
                if (Recstatus == "New")
                {
                    String RtMsg = "";
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RtMsg = MyComponents.HrService.UpdateIncidentInformation(IncNo, IncidentDescri, IncidentTyp, IncidentLoca, Incidenttm, Incidentdt, ocpatdiseType, withofis);
                    }
                    else
                    {
                        RtMsg = MyComponents.HrServiceZarib.UpdateIncidentInformation(IncNo, IncidentDescri, IncidentTyp, IncidentLoca, Incidenttm, Incidentdt, ocpatdiseType, withofis);
                    }
                        if (RtMsg != null)
                    {
                        if (RtMsg == "Yes")
                        {
                            txtReqNo.Text = IncNo;
                            uniqNo.Text = IncNo;
                            newwitness.Visible = false;
                            newwitness.Visible = false;
                            witness.Visible = true;
                            involvedparties.Visible = true;
                            witnessdata.Visible = true;
                            involvedpartdata.Visible = true;
                            lbnCloseparties.Visible = true;
                            witclose.Visible = true;
                            BindGridviewData(IncNo);
                            newView();
                        }
                    }
                    else
                    {
                        Message("Failed to update,,try again");
                    }
                }
                else
                {
                    BindGridviewData(IncNo);
                    txtReqNo.Text = IncNo;
                    uniqNo.Text = IncNo;
                    newView();
                }
                BindAttachmentGridviewData(IncNo);
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void involvedpartiesLines_Click(object sender, EventArgs e)
        {
            try
            {
                string EmployeeNumber = EmployeeNo.SelectedItem.Value;
                String DocNo = txtReqNo.Text.ToString();

                if (String.IsNullOrEmpty(EmployeeNumber))
                {
                    Message("Please Select Employee Name Type.");
                    EmployeeNo.Focus();
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                   rtnMsg = MyComponents.HrService.AddInvolvedparties(EmployeeNumber, DocNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.AddInvolvedparties(EmployeeNumber, DocNo);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    BindGridviewData(DocNo);
                    EmployeeNo.SelectedIndex = 0;
                    EmployeeNo.Text = null;
                    Message(rtnMsg);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void witnessLines_Click(object sender, EventArgs e)
        {
            try
            {
                string EmployeeNumber = witnessNo.SelectedItem.Value;
                String DocNo = uniqNo.Text.ToString();
                if (String.IsNullOrEmpty(EmployeeNumber))
                {
                    Message("Please Select Employee Name Type.");
                    EmployeeNo.Focus();
                    return;
                }
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                   rtnMsg = MyComponents.HrService.Addwitnesses(EmployeeNumber, DocNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.Addwitnesses(EmployeeNumber, DocNo);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    BindGridviewData(DocNo);
                    witnessNo.SelectedIndex = 0;
                    witnessNo.Text = null;
                    Message(rtnMsg);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void removewitness_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string EmpNo = arg[0];
            string DocNo = uniqNo.Text.ToString();
            try
            {
                string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.RemoveWitneses(DocNo, EmpNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.RemoveWitneses(DocNo, EmpNo);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    Message(rtnMsg);
                    BindGridviewData(DocNo);
                }

            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void removeinvolvedparty_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string EmpNo = arg[0];
            string DocNo = txtReqNo.Text.ToString();
            try
            {
                 string rtnMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    rtnMsg = MyComponents.HrService.RemoveInvolvedParties(DocNo, EmpNo);
                }
                else
                {
                    rtnMsg = MyComponents.HrServiceZarib.RemoveInvolvedParties(DocNo, EmpNo);
                }
                if (!String.IsNullOrEmpty(rtnMsg))
                {
                    Message(rtnMsg);
                    BindGridviewData(DocNo);
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }
        protected void Nsubumit_Click1(object sender, EventArgs e)
        {
            try
            {
                 string RtMg  = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMg = MyComponents.HrService.SubmitIncident(txtReqNo.Text.ToString());
                }
                else
                {
                    RtMg = MyComponents.HrServiceZarib.SubmitIncident(txtReqNo.Text.ToString());
                }
               if (!string.IsNullOrEmpty(RtMg))
                {                    
                   DisplayAlert(RtMg);                    
                }                
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
            }
        }
        protected void viewdocument_Click(object sender, EventArgs e)
        {
            string[] arg = new string[2];
            arg = (sender as LinkButton).CommandArgument.ToString().Split(';');
            string FileID = arg[0];
            string DocNo = txtReqNo.Text.ToString();
            try
            {
                string Base64Report = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    Base64Report = MyComponents.HrService.ViewAttachment(Convert.ToInt32(FileID), DocNo);
                }
                else
                {
                    Base64Report = MyComponents.HrServiceZarib.ViewAttachment(Convert.ToInt32(FileID), DocNo);
                }
                    if (Base64Report.Length > 0)
                {
                    string Content = "data:application/pdf;base64," + Base64Report;
                    myPDF.Attributes.Add("src", Content);
                    docpdf.Visible = true;
                    
                }
                else
                {
                    Message("File not found");
                    myPDF.Attributes.Add("src", null);
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message);
            }
        }

        protected void lbnAddLine_Click(object sender, EventArgs e)
        {
            lbnCloseparties.Visible = true;
            lbnAddLine.Visible = false;
            involvedpartdata.Visible = true;
            involvedparties.Visible = true;
        }

        protected void newwitness_Click(object sender, EventArgs e)
        {
            witclose.Visible = true;
            newwitness.Visible = false;
            witnessdata.Visible = true;
            witness.Visible=true;
        }
    }
}