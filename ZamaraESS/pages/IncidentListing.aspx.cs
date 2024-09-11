using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web.UI.WebControls;
using static Jint.Debugger.SourceCodeDescriptor;

namespace ZamaraESS.pages
{
    public partial class IncidentListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Request.QueryString.AllKeys.Contains("ty"))
                {
                    // Key 'ty' exists, you can retrieve its value safely
                    string Hscategory = Request.QueryString["ty"].ToString();
                    if (Hscategory != null)
                    {
                        if (Hscategory == "ac")
                        {
                            Session["HsCategory"] = "Accident";
                        }
                        else if (Hscategory == "in")
                        {
                           Session["HsCategory"] = "Incident";
                        }
                        else
                        {
                            Session["HsCategory"] = "Disease";
                        }
                    }
                }
              
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected string Jobs()
        {
            var htmlStr = string.Empty;
            try
            {
                string username = Session["username"].ToString();                          
                var Incident= (dynamic)null;
                var allIncident = (dynamic)null;
                var user= (dynamic)null;
                //check health and safety category
                String HsCDescription = "";
                if (Session["HsCategory"].ToString() == "Disease")
                {
                    HsCDescription = "Occupational Diseases";
                }
                else
                {
                    HsCDescription = Session["HsCategory"].ToString();
                }             
                string IncNo;
                HashSet<string> alreadyEncountered = new HashSet<string>();

                if (!String.IsNullOrEmpty(HsCDescription))
                {
                    Incident = MyComponents.OdataService.GetInvolvedParties.AddQueryOption("$filter", "Employee_No eq '" + username + "'").ToList();
                    int count=0;
                    foreach (var ym in Incident)
                    {
                        count++;
                        if (alreadyEncountered.Contains(ym.Document_No))
                        {
                            continue;
                        }
                        IncNo= ym.Document_No.ToString();
                        if (allIncident == null)
                        {
                            allIncident = MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "No eq '" + IncNo + "' and Category eq '" + HsCDescription + "'").ToList();
                        }
                        else
                        {
                            var data= MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "No eq '" + IncNo + "' and Category eq '" + HsCDescription + "'").ToList();
                            if (data != null)
                            {
                                allIncident.AddRange(data);
                            }
                        }                       
                                                                        
                        alreadyEncountered.Add(ym.Document_No);
                    }
                    
                    if (Incident.Count==0)
                    {
                        allIncident = MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "EmployeeNo eq '" + username + "' and Category eq '" + HsCDescription + "'").ToList();
                    }
                    else
                    {
                        user = MyComponents.OdataService.GetReportedIncidents.AddQueryOption("$filter", "EmployeeNo eq '" + username + "' and Category eq '" + HsCDescription + "'").ToList();
                        allIncident.AddRange(user);
                    }
                                      
                                
                }                
                int counter = 0;
                HashSet<string> alreadyEncountered2 = new HashSet<string>();
                foreach (var collectionz in allIncident) 
                {
                    string IncidentType;

                    if (Session["HsCategory"].ToString() == "Incident" || Session["HsCategory"].ToString() == "Accident")
                    {
                        IncidentType = collectionz.IncidentDescription;
                    }                   
                    else
                    {
                        if (collectionz.Occupationaldiseasetype!="")
                        {
                            IncidentType = collectionz.Occupationaldiseasetype;
                        }
                        else
                        {
                            IncidentType = collectionz.IncidentType;
                        }                       
                    }

                    if (alreadyEncountered2.Contains(collectionz.No))
                    {
                        continue;
                    }
                    counter++;                   
                    htmlStr += string.Format(
                        @"<tr  class='small'>
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                            <td>{5}</td> 
                            <td>{6}</td>                            
                            <td>
                              <b> <a href='ReportIncident.aspx?An={1}&status={6}&emp={2}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                            </td> 
                        </tr>",
                        counter,
                        collectionz.No,
                        collectionz.EmployeeNo.ToString(),
                        IncidentType,
                        collectionz.IncidentDate.ToString(),
                        collectionz.IncidentTime.ToString(),                       
                        collectionz.Status                        
                           );
                    alreadyEncountered2.Add(collectionz.No);
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message.ToString());              
            }
            return htmlStr;
        }
        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }
    }
}
