using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
	public partial class GrievanceListing : System.Web.UI.Page
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
            var htmlStr = string.Empty;
            try
            {
                string EmpNo = Session["username"].ToString();

                var data = MyComponents.OdataService.GetGrievances.AddQueryOption("$filter", "EmployeeNo eq '" + EmpNo + "'");
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
                                                             <td>
                                                                   <b> <a href='ReportGrievance.aspx?An={1}&status={8}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                                                            </td>
                                                     </tr>",
                                                         counter,
                                                         collectionz.No_,
                                                         collectionz.Aggriever_No,
                                                         collectionz.Incident_Date,
                                                         collectionz.Incident_Time,
                                                         collectionz.Incident_Location,
                                                         collectionz.Grievance_Code,
                                                         collectionz.General_Description,
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

        protected void createGrievance_Click(object sender, EventArgs e)
        {
            try
            {
                String EmpNo = Session["username"].ToString();

                string GrievanceN = "";
                if (Session["Company"].ToString() == "ZAAC")
                { 
                   GrievanceN= MyComponents.HrService.CreateGrievanceCase(EmpNo);
                }
                else
                {
                    GrievanceN = MyComponents.HrServiceZarib.CreateGrievanceCase(EmpNo);
                }

                    
                if (!String.IsNullOrEmpty(GrievanceN))
                {
                    //GrievanceNo.Text = GrievanceN;
                    Session["GrivNo"] = GrievanceN;
                    Response.Redirect("ReportGrievance.aspx?Tp='New'&status='New'");
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