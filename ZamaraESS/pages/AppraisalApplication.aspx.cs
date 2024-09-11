using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
namespace ZamaraESS.pages
{
    public partial class AppraisalApplication : System.Web.UI.Page
    {
        public static int TableID = 60444;
        public static String paged = null;
        public static String AppraisalNo = null;
        public static dynamic secCat = (dynamic)null;
        public dynamic SectionCode = (dynamic)null;
        public static dynamic SectnCode = (dynamic)null;

        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                var test = "status";
                string RecStatus = "New";

                if (Request.Url.AbsoluteUri.Contains(test))
                {
                    RecStatus= Request.QueryString["status"].ToString();
                }               
              
                if (RecStatus=="End Year Submitted" || RecStatus =="End Year Closed" || RecStatus == "Moderation" || RecStatus == "Completed")
                {
                    loadrecommendations();
                    GetSuperviserComments();
                }
                if(RecStatus == "Mid Year submitted" || RecStatus == "Mid Year Closed" ||  RecStatus == "End Year Review"){
                    loadrecommendations();
                    GetSuperviserComments();
                }              
               
                try
                {
                    if (RecStatus == "New")
                    {
                        secCat = MyComponents.OdataService.GetAppraisalCategory.Execute().ToList();
                        SectnCode = MyComponents.OdataService.GetAppraisalCategory.Execute().ToList();
                    }                                   
                   
                    
                }
                catch (Exception exception)
                {
                    Message("ERROR: " + exception.Message.ToString());
                    exception.Data.Clear();
                }
            }           
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
              string.Format("alert('{0}');window.location.href = 'AppraisalListing.aspx?status=" + RecStatus + "'",
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
        //load recommendations

        protected void loadrecommendations()
        {
          
            this.recommend.Items.Clear();
            ListItem li = null;
            li = new ListItem("--Select Recommendation--", "");
            this.recommend.Items.Add(li);

            var recom = MyComponents.OdataService.GetRecommendations.Execute();

            HashSet<string> alreadyEncountered = new HashSet<string>();
            foreach (var ym in recom)
            {
                if (alreadyEncountered.Contains(ym.Code))
                {
                    continue;
                }
                li = new ListItem(
                              ym.Description,
                              ym.Code
                          );
                this.recommend.Items.Add(li);

                alreadyEncountered.Add(ym.Code);
            }
        }
        //load appraisal categories
        [System.Web.Services.WebMethod]
        public static string SectionCategories(String Section)
        {
            StringBuilder sectionAppraisalCatBuilder = new StringBuilder();
            foreach (var ym in secCat)
            {
                if (ym.Section == Section)
                {
                    sectionAppraisalCatBuilder.Append(
                        $@"<div class='card-header' id='{ym.Code}a' appraisaldescipt='{ym.Code}'>
                            <h5 class='mb-0'>
                                <input type='checkbox' value='{ym.Code}' class='selectedSecApraisalCat'/>
                                <button class='btn btn-link' type='button' data-toggle='collapse' data-target='#collapseOne' aria-expanded='true' aria-controls='collapseOne'>
                                   {ym.Description}
                                </button>
                            </h5>
                        </div>");
                }
            }
            string sectionAppraisalCat = sectionAppraisalCatBuilder.ToString();
            return sectionAppraisalCat;
        }

        //Load Appraisal sections
        protected Dictionary<string, Dictionary<string, string>> categoryjobs()
        {
            dynamic data = (dynamic)null;
            string path = System.AppContext.BaseDirectory;
            String ENo = Session["username"].ToString();
            Dictionary<string, string> AppraisalCategories = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> appraisalCat = new Dictionary<string, Dictionary<string, string>>();

            try
            {
                using (StreamReader r = new StreamReader(path + "pages\\AppraisalCategories.json"))
                {
                    string json = r.ReadToEnd();
                    JObject jsonObject = JObject.Parse(json);

                    //Find the UserAppraisalCategory array
                    JArray userAppraisalCategory = (JArray)jsonObject["UserAppraisalCategory"];
                    JArray userAppraisalSec = (JArray)jsonObject["UserAppraisalSection"];


                    data = MyComponents.OdataService.GetAppraisalCategory.Execute().ToList();
                    String paged = Request.QueryString["Tp"];
                    if (paged != null && (Request.QueryString["status"] == "Mid Year submitted" || Request.QueryString["status"] == "Moderation" || Request.QueryString["status"] == "End Year Submitted" || Request.QueryString["status"] == "End Year Closed") && Request.QueryString["emp"] != ENo)
                    {
                        String EmpNo = Request.QueryString["emp"].ToString();
                        var targetcatObject = userAppraisalCategory.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                        var targetsecObject = userAppraisalSec.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                        JObject secObject = (JObject)targetsecObject;
                        JObject catObject = (JObject)targetcatObject;
                        if (targetsecObject != null)
                        {
                            foreach (var propert in secObject.Properties())
                            {
                                string key = propert.Name;
                                if (key != "EmpNo")
                                {
                                    foreach (var ym in data)
                                    {
                                        if (key == ym.Code)
                                        {
                                            string sect = ym.Section;
                                            if (targetcatObject != null)
                                            {
                                                AppraisalCategories = new Dictionary<string, string>();
                                                foreach (var dat in data)
                                                {
                                                    if (dat.Section == sect)
                                                    {
                                                        foreach (var property in catObject.Properties())
                                                        {
                                                            if (property.Name != "EmpNo")
                                                            {
                                                                if (dat.Code == property.Name)
                                                                {
                                                                    AppraisalCategories.Add(dat.Code, dat.Description);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                appraisalCat.Add(sect, new Dictionary<string, string>(AppraisalCategories));
                                            }
                                            else
                                            {
                                                appraisalCat.Add(sect, new Dictionary<string, string>(AppraisalCategories));
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Find the object with the specified EmpNo
                        var targetcatObject = userAppraisalCategory.FirstOrDefault(e => (string)e["EmpNo"] == ENo);
                        var targetsecObject = userAppraisalSec.FirstOrDefault(e => (string)e["EmpNo"] == ENo);
                        JObject secObject = (JObject)targetsecObject;
                        JObject catObject = (JObject)targetcatObject;

                        if (targetsecObject != null)
                        {
                            foreach (var propert in secObject.Properties())
                            {
                                string key = propert.Name;
                                if (key != "EmpNo")
                                {
                                    foreach (var ym in data)
                                    {
                                        if (key == ym.Code)
                                        {
                                            string sect = ym.Section;
                                            if (targetcatObject != null)
                                            {
                                                AppraisalCategories = new Dictionary<string, string>();
                                                foreach (var dat in data)
                                                {
                                                    if (dat.Section == sect)
                                                    {
                                                        foreach (var property in catObject.Properties())
                                                        {
                                                            if (property.Name != "EmpNo")
                                                            {
                                                                if (dat.Code == property.Name)
                                                                {
                                                                    AppraisalCategories.Add(dat.Code, dat.Description);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                appraisalCat.Add(sect, new Dictionary<string, string>(AppraisalCategories));
                                            }
                                            else
                                            {
                                                appraisalCat.Add(sect, new Dictionary<string, string>(AppraisalCategories));
                                            }
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
            return appraisalCat;
        }


        //load appraisal section
        protected dynamic LoadAppraisalSections()
        {
            Dictionary<string, string> SelectAppraisalSections = new Dictionary<string, string>();
            try
            {
                dynamic SectionCod = MyComponents.OdataService.GetAppraisalCategory.Execute().ToList();
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in SectionCod)
                {
                    if (!alreadyEncountered.Contains(ym.Section))
                    {
                        SelectAppraisalSections.Add(ym.Code, ym.Section);
                    }

                    alreadyEncountered.Add(ym.Section);
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
            return SelectAppraisalSections;
        }


        [System.Web.Services.WebMethod]
        public static string LoadAppraisalLines(String AppCategory, String AppraisalNo)
        {
            AppraisalApplication emp = new AppraisalApplication();
            dynamic data = (dynamic)null;
            try
            {
                data = MyComponents.OdataService.GetAppraisalLines.AddQueryOption("$filter", "AppraisalCategory eq '" + AppCategory + "' and DocumentNo eq '" + AppraisalNo + "'");
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }

        [System.Web.Services.WebMethod]
        public static string LoadWeight(String AppCategory, String Code)
        {
            AppraisalApplication emp = new AppraisalApplication();
            var data = (dynamic)null;
            try
            {
                data = MyComponents.OdataService.GetAppraisalKPI.AddQueryOption("$filter", "Category eq '" + AppCategory + "' and Code eq '" + Code + "'");
            }
            catch (Exception exception)
            {
                emp.Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }

        [System.Web.Services.WebMethod]
        public static String AutoSave(String LineNo, String AppraisalNo, String KPICode, String Objective, String Measure, String SourceofEvidence, String AppraisalCategory, String EmpImprove, String EmpSuccess, String EmployeeSCode, String EmployeeScore, string EmployeeRDec, string Wegh, string AppraiseeCom, string supscorecode, string supscore, string supscoredescrip, string agrdscorecode, string agrdscore, string agrdscoredescrip, string appraisercomm,string moderationcomm)
        {
            AppraisalApplication emp = new AppraisalApplication();
            int RtnMsg = 0;
            String RtMsg = "";
            Decimal Weight = Convert.ToDecimal(Wegh);
            Decimal EmployeScore = Convert.ToDecimal(EmployeeScore);
            Decimal SupScore = Convert.ToDecimal(supscore);
            Decimal AgrdScore = Convert.ToDecimal(agrdscore);

            try
            {
                int LinNo = Convert.ToInt32(LineNo);
                if (LinNo == 0)
                {
                    if (AppraisalNo != "")
                    {

                        if (Objective != "")
                        {
                            if (Measure != "")
                            {
                                if (SourceofEvidence != "")
                                {

                                    RtnMsg = MyComponents.HrService.CreateAppraisalLine(AppraisalNo, AppraisalCategory, KPICode, Objective, Measure, SourceofEvidence);
                                    return JsonConvert.SerializeObject(RtnMsg);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (AppraisalNo != "")
                    {
                        if (Objective != "")
                        {
                            if (Measure != "")
                            {
                                if (SourceofEvidence != "")
                                {
                                    RtMsg = MyComponents.HrService.UpdateAppraisalLine(AppraisalNo, LinNo, AppraisalCategory,KPICode, Objective, SourceofEvidence, Weight, Measure, EmpImprove, EmpSuccess, EmployeeSCode, EmployeScore, EmployeeRDec, AppraiseeCom, supscorecode, SupScore, supscoredescrip, agrdscorecode, AgrdScore, agrdscoredescrip, appraisercomm,moderationcomm);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            return JsonConvert.SerializeObject(RtMsg);
        }

        [System.Web.Services.WebMethod]
        public static string LoadKPICodes(String AppCategory)
        {
            AppraisalApplication emp = new AppraisalApplication();
            var data = (dynamic)null;
            try
            {
                data = MyComponents.OdataService.GetAppraisalKPI.AddQueryOption("$filter", "Category eq '" + AppCategory + "'");
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }

        [System.Web.Services.WebMethod]
        public static string WriteToJsonFile(Dictionary<string, string> SelectedAppraisalCat)
        {
            AppraisalApplication emp = new AppraisalApplication();
            try
            {
                string path = System.AppContext.BaseDirectory;
                string jsonFilePath = Path.Combine(path, "pages\\AppraisalCategories.json");

                // Read existing JSON data using StreamReader
                string json;
                using (StreamReader streamReader = new StreamReader(jsonFilePath))
                {
                    json = streamReader.ReadToEnd();
                }

                JObject jObject = JObject.Parse(json);

                // Check if "UserAppraisalCategory" array exists
                if (jObject["UserAppraisalCategory"] != null)
                {
                    // Loop through existing items
                    JArray userAppraisalCategory = (JArray)jObject["UserAppraisalCategory"];
                    bool empNoExists = false;
                    int currentIndex = 0;
                    // Loop through existing items                   
                    foreach (JObject category in userAppraisalCategory)
                    {
                        // Check if the category for the current user already exists
                        if (category["EmpNo"].ToString() == SelectedAppraisalCat["EmpNo"])
                        {
                            // update properties
                            empNoExists = true;
                            foreach (var key in SelectedAppraisalCat.Keys)
                            {
                                if (key == "EmpNo")
                                {
                                    category[key] = SelectedAppraisalCat[key];
                                }
                                else
                                {
                                    category[key] = "";
                                }
                            }
                            break;
                        }
                        currentIndex++;
                    }

                    // If EmpNo doesn't exist and it's the last iteration, create a new category
                    if (!empNoExists && currentIndex == userAppraisalCategory.Count)
                    {
                        JObject newCategory = new JObject();
                        newCategory["EmpNo"] = SelectedAppraisalCat["EmpNo"];
                        foreach (var key in SelectedAppraisalCat.Keys)
                        {
                            if (key != "EmpNo")
                            {
                                newCategory[key] = "";
                            }
                        }
                        userAppraisalCategory.Add(newCategory);
                    }

                    // Write the updated JSON back to the file using StreamWriter
                    using (StreamWriter streamWriter = new StreamWriter(jsonFilePath))
                    {
                        streamWriter.Write(jObject.ToString());
                    }

                    return JsonConvert.SerializeObject("Yes");
                }
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            return JsonConvert.SerializeObject("No");
        }

        //remove category from the json file
        [System.Web.Services.WebMethod]
        public static string RemoveAppraisalCategory(String AppNo, String EmpNo, String CategoryKey)
        {
            AppraisalApplication emp = new AppraisalApplication();
            Dictionary<string, string> RtMsg = new Dictionary<string, string>();
            var data = (dynamic)null;
            string path = System.AppContext.BaseDirectory;
            string filepath = Path.Combine(path, "pages", "AppraisalCategories.json");

            try
            {
                // Read the JSON file and parse it
                string json;
                using (StreamReader r = new StreamReader(filepath))
                {
                    json = r.ReadToEnd();
                }

                JObject jsonObject = JObject.Parse(json);

                // Find the UserAppraisalCategory array
                JArray userAppraisalCategory = (JArray)jsonObject["UserAppraisalCategory"];

                // Find the object with the specified EmpNo
                var targetObject = userAppraisalCategory.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                if (targetObject != null)
                {
                    // Check whether the category has existing appraisal line
                    data = MyComponents.OdataService.GetAppraisalLines.AddQueryOption("$filter", "AppraisalCategory eq '" + CategoryKey + "' and DocumentNo eq '" + AppNo + "'").ToList().Count().ToString();
                    if (data == "0")
                    {
                        // Remove the specified key-value pair from the object
                        targetObject[CategoryKey].Parent.Remove();

                        // Write the modified JSON back to the file
                        using (StreamWriter writer = new StreamWriter(filepath))
                        {
                            writer.Write(JsonConvert.SerializeObject(jsonObject));
                        }

                        RtMsg["Msg"] = "Yes";
                    }
                    else
                    {
                        RtMsg["Msg"] = "No";
                    }
                }
            }
            catch (Exception exception)
            {
                emp.Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }

            return JsonConvert.SerializeObject(RtMsg);
        }

        protected void Moveback_Click1(object sender, EventArgs e)
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
            };
            Response.Redirect("AppraisalListing.aspx?status=" + RecStatus);
        }

        protected void submitAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                string supervisorSuccAreaValue = SupervisorSuccArea.Value.ToString();
                string supervisorImprAreaValue = SupervisorImprArea.Value.ToString();
                string supervisorRemarksValue = supervisoreremarks.Value.ToString();
                string Reccom = recommend.SelectedValue.ToString();

                String paged = Request.QueryString["Tp"];
                string RecStatus = string.Empty;
                string AppNo = string.Empty;
                string RtMsg = string.Empty;
                
                if (paged != null)
                {
                    AppNo = Request.QueryString["An"].ToString();
                    RecStatus = Request.QueryString["status"].ToString();
                }
                else
                {
                    AppNo = Session["AppraisalNumber"].ToString();
                }               

                //check appraisal status
                if (RecStatus == "Mid Year submitted")
                {
                    if (supervisorSuccAreaValue == "" && supervisorImprAreaValue == "")
                    {
                        Message("Both success and improvement area cannot be blank");                        
                        return;
                    }                   
                } 
                else if(RecStatus == "Moderation")
                {
                    if (Reccom == "")
                    {
                        Message("Recommendation cannot be blank");
                        recommend.Focus();  
                        return;
                    }
                }
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMsg = MyComponents.HrService.UpdateAppraisalHeader(AppNo, supervisorRemarksValue, supervisorSuccAreaValue, supervisorImprAreaValue, Reccom);
                }
                else
                {
                    RtMsg = MyComponents.HrServiceZarib.UpdateAppraisalHeader(AppNo, supervisorRemarksValue, supervisorSuccAreaValue, supervisorImprAreaValue, Reccom);
                }
                   
                if (RtMsg == "No")
                {
                    Message("Failed to accept,try again");
                    return;
                }


                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMsg = MyComponents.HrService.SubmitAppraisal(AppNo);
                }
                else
                {
                    RtMsg = MyComponents.HrServiceZarib.SubmitAppraisal(AppNo);
                }
                    
                if (RtMsg == "Yes")
                {
                    DisplayAlert("Submitted successifully");
                }
                else
                {
                    Message("Failed to submit");
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }

        }

        protected void Recall_Click(object sender, EventArgs e)
        {
            //recall
            try
            {
                String paged = Request.QueryString["Tp"];
                string AppNo = string.Empty;
                if (paged != null)
                {
                    AppNo = Request.QueryString["An"].ToString();
                }
                String RtMsg = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMsg = MyComponents.HrService.InitiateGoalRevision(AppNo);
                }
                else
                {
                    RtMsg = MyComponents.HrServiceZarib.InitiateGoalRevision(AppNo);
                }
                   
                if (RtMsg == "Yes")
                {
                    DisplayAlert("Appraisal recalled successifully");
                }
                else
                {
                    Message("Recall failed");
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
        }
        [System.Web.Services.WebMethod]
        public static string LoadAppraisalRating()
        {
            AppraisalApplication emp = new AppraisalApplication();
            var data = (dynamic)null;
            try
            {
                data = MyComponents.OdataService.GetAppraisalRating.Execute();
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }
        [System.Web.Services.WebMethod]
        public static string LoadEmployeeRatingD(String Code)
        {
            AppraisalApplication emp = new AppraisalApplication();
            var data = (dynamic)null;
            try
            {
                data = MyComponents.OdataService.GetAppraisalRating.AddQueryOption("$filter", "Code eq '" + Code + "'");
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }
        [System.Web.Services.WebMethod]
        public static string DeleteAppraisalLine(Dictionary<string, string> SelectedAppraisalLine)
        {
            AppraisalApplication emp = new AppraisalApplication();
            var data = (dynamic)null;
            try
            {
                ICollection<string> keys = SelectedAppraisalLine.Keys;
                foreach (string key in keys)
                {
                    //{ "SelectedAppraisalCat":{ "30000":"30000","APNo":"APPR208","AppCategory":"PEOPLE"} }
                    if (key != "APNo" && key != "AppCategory")
                    {
                        data = MyComponents.HrService.DeleteAppraisalLine(SelectedAppraisalLine["APNo"], Convert.ToInt32(SelectedAppraisalLine[key]));
                    }
                }
                if (data != "")
                {
                    data = MyComponents.OdataService.GetAppraisalLines.AddQueryOption("$filter", "AppraisalCategory eq '" + SelectedAppraisalLine["AppCategory"] + "' and DocumentNo eq '" + SelectedAppraisalLine["APNo"] + "'");
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }

            }
            catch (Exception exception)
            {
                emp.Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
            string jsonString = JsonConvert.SerializeObject(data);
            return jsonString;
        }

        protected void GetSuperviserComments()
        {
            paged = Request.QueryString["Tp"];
            if (paged != null)
            {
                var data = (dynamic)null;
                String AppNo = Request.QueryString["An"].ToString();
                try
                {
                    data = MyComponents.OdataService.GetAppraisalsHeader.AddQueryOption("$filter", "No eq '" + AppNo + "'");

                    foreach (var ym in data)
                    {
                        supervisoreremarks.Value = ym.Remarks;
                        SupervisorSuccArea.Value = ym.SupervisorSuccessArea;
                        SupervisorImprArea.Value = ym.SupervisorImprovementArea;
                        recommend.SelectedValue = ym.Recommendation;
                    }
                }
                catch (Exception exception)
                {
                    Message("ERROR: " + exception.Message.ToString());
                    exception.Data.Clear();
                }
            }

        }
        protected void Decline_Click(object sender, EventArgs e)
        {

            try
            {
                String paged = Request.QueryString["Tp"];
                string AppNo = string.Empty;
                string RtMsg = string.Empty;

                string SupImproveArea = SupervisorImprArea.Value;
                String SupSuccArea = SupervisorSuccArea.Value;
                string SupRemark = supervisoreremarks.Value;
                string Reccom = recommend.SelectedValue.ToString();

                if (SupRemark == "")
                {
                    Message("Remark cannnot be blank");
                    supervisoreremarks.Focus();
                    return;
                }

                if (paged != null)
                {
                    AppNo = Request.QueryString["An"].ToString();
                }
                else
                {
                    AppNo = Session["AppraisalNumber"].ToString();
                }
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMsg = MyComponents.HrService.UpdateAppraisalHeader(AppNo, SupRemark, SupSuccArea, SupImproveArea, Reccom);
                }
                else
                {
                    RtMsg = MyComponents.HrServiceZarib.UpdateAppraisalHeader(AppNo, SupRemark, SupSuccArea, SupImproveArea, Reccom);
                }
                   
                if (RtMsg == "Yes")
                {

                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RtMsg = MyComponents.HrService.DeclineAppraisal(AppNo);
                    }
                    else
                    {
                        RtMsg = MyComponents.HrServiceZarib.DeclineAppraisal(AppNo);
                    }
                    if (RtMsg == "Yes")
                    {
                        DisplayAlert("Declined successifully");
                    }
                    else
                    {
                        Message("Failed to decline");
                    }
                }
                else
                {
                    Message("Failed to update remarks,try again");
                }

            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
        }

        protected void Complete_Click(object sender, EventArgs e)
        {
            try
            {
                String paged = Request.QueryString["Tp"];
                string RecStatus = Request.QueryString["status"].ToString();
                string AppNo = string.Empty;
                string RtMsg = string.Empty;
                string SupImproveArea = SupervisorImprArea.Value;
                String SupSuccArea = SupervisorSuccArea.Value;
                string SupRemark = supervisoreremarks.Value;
                string Reccom = recommend.SelectedValue.ToString();             
                
                if (paged != null)
                {
                    AppNo = Request.QueryString["An"].ToString();
                }
                else
                {
                    AppNo = Session["AppraisalNumber"].ToString();
                }
                if (Session["Company"].ToString() == "ZAAC")
                {
                    RtMsg = MyComponents.HrService.UpdateAppraisalHeader(AppNo, SupRemark, SupSuccArea, SupImproveArea, Reccom);
                }
                else
                {
                    RtMsg = MyComponents.HrServiceZarib.UpdateAppraisalHeader(AppNo, SupRemark, SupSuccArea, SupImproveArea, Reccom);
                }
                   
                if (RtMsg != "Yes")
                {
                    Message("Failed to update recommendation,,try again");
                    return;
                }               

                //check appraisal status                
                if (RecStatus == "End Year Closed")
                {
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RtMsg = MyComponents.HrService.CompleteAppraisal(AppNo);
                    }
                    else
                    {
                        RtMsg = MyComponents.HrServiceZarib.CompleteAppraisal(AppNo);
                    }                        
                }
                else
                {                 
                 if (Reccom == "")
                    {
                        Message("Recommendation cannot be blank");
                        recommend.Focus();
                        return;
                    }
                    if (Session["Company"].ToString() == "ZAAC")
                    {
                        RtMsg = MyComponents.HrService.SubmitAppraisal(AppNo);
                    }
                    else
                    {
                        RtMsg = MyComponents.HrServiceZarib.SubmitAppraisal(AppNo);
                    }
                                        
                }
                if (RtMsg == "Yes")
                {
                    DisplayAlert("Appraisal completed successfully");
                }
                else
                {
                    Message(" Failed to complete appraisal,,try again");
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
        }

        [System.Web.Services.WebMethod]
        public static string AddAppraisalCategorySections(Dictionary<string, string> SelectedAppraisalCat)
        {
            AppraisalApplication emp = new AppraisalApplication();
            try
            {
                string path = System.AppContext.BaseDirectory;
                string jsonFilePath = Path.Combine(path, "pages\\AppraisalCategories.json");

                // Read existing JSON data using StreamReader
                string json;
                using (StreamReader streamReader = new StreamReader(jsonFilePath))
                {
                    json = streamReader.ReadToEnd();
                }

                JObject jObject = JObject.Parse(json);

                // Check if "UserAppraisalCategory" array exists
                if (jObject["UserAppraisalSection"] != null)
                {
                    // Loop through existing items
                    JArray userAppraisalCategory = (JArray)jObject["UserAppraisalSection"];
                    bool empNoExists = false;
                    int currentIndex = 0;
                    // Loop through existing items                   
                    foreach (JObject category in userAppraisalCategory)
                    {
                        // Check if the category for the current user already exists
                        if (category["EmpNo"].ToString() == SelectedAppraisalCat["EmpNo"])
                        {
                            // Add or update properties
                            empNoExists = true;
                            foreach (var key in SelectedAppraisalCat.Keys)
                            {
                                if (key == "EmpNo")
                                {
                                    category[key] = SelectedAppraisalCat[key];
                                }
                                else
                                {
                                    category[key] = "";
                                }
                            }
                            break;
                        }
                        currentIndex++;
                    }

                    // If EmpNo doesn't exist and it's the last iteration, create a new category
                    if (!empNoExists && currentIndex == userAppraisalCategory.Count)
                    {
                        JObject newCategory = new JObject();
                        newCategory["EmpNo"] = SelectedAppraisalCat["EmpNo"];
                        foreach (var key in SelectedAppraisalCat.Keys)
                        {
                            if (key != "EmpNo")
                            {
                                newCategory[key] = "";
                            }
                        }
                        userAppraisalCategory.Add(newCategory);
                    }

                    // Write the updated JSON back to the file using StreamWriter
                    using (StreamWriter streamWriter = new StreamWriter(jsonFilePath))
                    {
                        streamWriter.Write(jObject.ToString());
                    }

                    return JsonConvert.SerializeObject("Yes");
                }
            }
            catch (Exception ex)
            {
                emp.Message("ERROR: " + ex.Message.ToString());
            }
            return JsonConvert.SerializeObject("No");
        }
        //remove category from the json file
        [System.Web.Services.WebMethod]
        public static string RemoveAppSection(String SecCode, String EmpNo)
        {
            AppraisalApplication emp = new AppraisalApplication();
            Dictionary<string, string> RtMsg = new Dictionary<string, string>();
            string sectCode = string.Empty;
            string path = System.AppContext.BaseDirectory;
            string filepath = Path.Combine(path, "pages", "AppraisalCategories.json");

            try
            {
                // Read the JSON file and parse it
                string json;
                using (StreamReader r = new StreamReader(filepath))
                {
                    json = r.ReadToEnd();
                }

                JObject jsonObject = JObject.Parse(json);

                // Find the UserAppraisalCategory array
                JArray userAppraisalCategory = (JArray)jsonObject["UserAppraisalCategory"];
                JArray userAppraisalSec = (JArray)jsonObject["UserAppraisalSection"];

                // Find the object with the specified EmpNo
                var targetcatObject = userAppraisalCategory.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                var targetsecObject = userAppraisalSec.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                JObject secObject = (JObject)targetsecObject;
                JObject catObject = (JObject)targetcatObject;                
                //get the section code
                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in SectnCode)
                {
                    if (!alreadyEncountered.Contains(ym.Section))
                    {
                        if (ym.Section == SecCode)
                        {
                            sectCode = ym.Code;
                            break;
                        }
                    }
                    alreadyEncountered.Add(ym.Section);
                }
                // Find the object with the specified EmpNo
                var targetObject = userAppraisalSec.FirstOrDefault(e => (string)e["EmpNo"] == EmpNo);
                int k = 0;
                if (targetsecObject != null)
                {
                    if (targetcatObject != null)
                    {
                        foreach (var property in catObject.Properties())
                        {
                            if (property.Name != "EmpNo")
                            {
                                if(property.Name == sectCode)
                                {
                                    k++;
                                    break;
                                }
                                else {
                                    foreach (var ym in SectnCode)
                                    {
                                        String section = ym.Section;
                                        if (ym.Code == property.Name)
                                        {
                                            if (section == SecCode)
                                            {
                                                k++;
                                                break;
                                            }
                                        }
                                    }
                                }                                                               
                            }
                        }
                    }
                    if (k == 0)
                    {
                        // Remove the specified key-value pair from the object
                        targetObject[sectCode].Parent.Remove();

                        // Write the modified JSON back to the file
                        using (StreamWriter writer = new StreamWriter(filepath))
                        {
                            writer.Write(JsonConvert.SerializeObject(jsonObject));
                        }

                        RtMsg["Msg"] = "Yes";
                    }
                    else
                    {
                        RtMsg["Msg"] = "No";
                    }
                }
            }
            catch (Exception exception)
            {
                emp.Message("ERROR: " + exception.Message.ToString());
                exception.Data.Clear();
            }
            return JsonConvert.SerializeObject(RtMsg);
        }     

    }
}