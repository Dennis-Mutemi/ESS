using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class LeaveSchedules : System.Web.UI.Page
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

            string username = Session["username"].ToString();
            try
            {
                var data = MyComponents.OdataService.GetLeaveSchedules.AddQueryOption("$filter", "Employee_No_ eq '" + username + "' and Converted eq false");
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
                                                            <td>{9}</td>
                                                             <td class='small'>
                                                                    <b><a href='ConvertLeavePlan.aspx?appNo={1}&status={9}'><i class='fa fa-bolt text-danger'></i><span class='text-danger'>  Convert</span></a></b>
                                                            </td>
                                                     </tr>",
                                                         counter,
                                                         collectionz.No_,
                                                         collectionz.LeavePeriod,
                                                         collectionz.Leave_Type,
                                                         Convert.ToDouble(collectionz.No__of_Days).ToString(CultureInfo.InvariantCulture),
                                                         Microsoft.OData.Edm.Date.Parse(collectionz.Created_Date.ToString()),
                                                         collectionz.Start_Date,
                                                         collectionz.End_Date,
                                                         collectionz.Return_Date,
                                                         collectionz.Status
                                                         );
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            return htmlStr;
        }
    }
}