using System;
using System.Linq;
using System.Web.UI.WebControls;
namespace ZamaraESS.pages
{
    public partial class AppraisalListing : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["APPLineNo"] = null;

                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        protected string Jobs()
        {
            var htmlstr = string.Empty;
            var data = (dynamic)null;
            var data2 = (dynamic)null;

            string EmpNo = Session["username"].ToString();
            try
            {
               
                Decimal ExpectedScore = MyComponents.HrService.GetExpectedScore();
                Session["ExpectedScore"] = ExpectedScore;
                String Status = Request.QueryString["status"];
                if(Status!="New" && Status != "Submitted" &&  Status != "Mid Year Review" && Status != "Mid Year Closed" && Status != "End Year Review" && Status != "Completed")
                {
                    data = MyComponents.OdataService.GetAppraisalsHeader.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq '" + Status + "'").ToList();
                    data2 = MyComponents.OdataService.GetAppraisalsHeader.AddQueryOption("$filter", "SupervisorNo eq '" + EmpNo + "' and Status eq '" + Status + "'").ToList();
                    data.AddRange(data2);
                }
                else
                {
                    data = MyComponents.OdataService.GetAppraisalsHeader.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "' and Status eq '" + Status + "'");
                }               

                htmlstr = HtmlTable(data);
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }

            return htmlstr;
        }

        protected void AFAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                String UserId = Session["UserID"].ToString();
                String AppraisalNo = MyComponents.HrService.CreateAppraisal(Session["username"].ToString());
                Session["AppraisalNumber"] = AppraisalNo;                
                Response.Redirect("AppraisalApplication.aspx");
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
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
        protected String HtmlTable(dynamic data)
        {
            newappraisal.Visible = false;
            var htmlstr = string.Empty;
            int counter = 0;
            foreach (var collectionz in data)
            {
                counter++;
                htmlstr += string.Format(
                    @"<tr class='small'>
                    <td>{0}</td>
                    <td>{1}</td>
                    <td>{2}</td>
                    <td>{3}</td>
                    <td>{4}</td>
                    <td>{5}</td>
                    <td>
                        <b> <a href='AppraisalApplication.aspx?An={1}&status={5}&typ={6}&emp={2}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                    </td>
                </tr>",
            counter,
            collectionz.No,
            collectionz.EmployeeNo,
            collectionz.EmployeeName,
            collectionz.Period,
            collectionz.Status,
            collectionz.AppraisalType
                 );
            }
            return htmlstr;
        }
    }
}