using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class CaseListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                
            }
        }
        protected string Jobs()
        {
            var returntype = string.Empty;
            string RecStatus = Request.QueryString["status"].ToString();
            string EmpNo = Session["username"].ToString();
            var newdata = (dynamic)null;
            var seconddata = (dynamic)null;
            try
            {
                if (RecStatus != null)
                {
                    if (RecStatus == "New")
                    {
                        newdata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq 'New'").ToList();
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo eq '" + EmpNo + "' and Status eq 'New'").ToList();
                        newdata.AddRange(seconddata);
                        returntype = Htmlresponse(newdata);
                    }
                    else if (RecStatus == "Submitted")
                    {
                        newdata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq 'Submitted'").ToList();
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo  eq '" + EmpNo + "' and Status eq 'Submitted'").ToList();
                        newdata.AddRange(seconddata);
                        returntype = Htmlresponse(newdata);                      
                    }
                    else if (RecStatus == "Show Cause")
                    {
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo  eq '" + EmpNo + "' and Status eq 'Show Cause'").ToList();
                       
                        returntype = Htmlresponse(seconddata);
                    }
                    else if (RecStatus == "Show Cause Response")
                    {
                        newdata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq 'Show Cause Response'").ToList();
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo eq '" + EmpNo + "' and Status eq 'Show Cause Response'").ToList();
                        newdata.AddRange(seconddata);
                        returntype = Htmlresponse(newdata);
                        
                    }
                    else if (RecStatus == "Ongoing")
                    {
                       
                        newdata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq 'Ongoing'").ToList();
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo eq '" + EmpNo + "' and Status eq 'Ongoing'").ToList();
                        newdata.AddRange(seconddata);
                        returntype = Htmlresponse(newdata);
                    }
                    else if (RecStatus == "Closed")
                    {
                      
                        seconddata = MyComponents.OdataService.GetDisciplinaryCase.AddQueryOption("$filter", "OffenderNo eq '" + EmpNo + "' and Status eq 'Closed'").ToList();
                        returntype = Htmlresponse(seconddata);
                    }
                }
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }
            return returntype;
        }
        protected String Htmlresponse(dynamic data)
        {
            int counter = 0;
            var htmlStr = string.Empty;
            try
            {
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
                                                                 <td>
                                                                       <b> <a href='DisciplinaryCase.aspx?An={1}&status={7}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                                                                </td>
                                                                </tr>",
                                                         counter,
                                                         collectionz.No,
                                                         collectionz.OffenderName,
                                                         collectionz.OffenceType,
                                                         collectionz.OffenceCategory,
                                                         collectionz.CaseDescription,
                                                         collectionz.CaseDate,
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

        public void Message(string strMsg)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", strScript.ToString());
        }

        protected void Case_Click(object sender, EventArgs e)
        {
            try
            {
                String EmpNo = Session["username"].ToString();

                string DisNo = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                    //DisNo = MyComponents.HrService.CreateDisciplinaryCase(EmpNo);

                }
                else
                {
                    DisNo = MyComponents.HrServiceZarib.CreateDisciplinaryCase(EmpNo);
                }


                   
                if (!String.IsNullOrEmpty(DisNo))
                {
                    Session["DisNo"] = DisNo;
                    //DisNo.Text = ExitN;
                    Response.Redirect("DisciplinaryCase.aspx?Tp='New'&status='New'");
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