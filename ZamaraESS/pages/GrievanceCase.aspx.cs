using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class GrievanceCase : System.Web.UI.Page
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

                var data = MyComponents.OdataService.GetGrievances.AddQueryOption("$filter", "Aggriever_No eq '" + EmpNo + "'");
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

    }
}