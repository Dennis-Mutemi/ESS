using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class Exit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                Jobs();
            }
        }
        protected string Jobs()
        {
            var htmlStr = string.Empty;           
            string EmpNo = Session["username"].ToString();
            dynamic newdata = null;

            try
            {
                newdata = MyComponents.OdataService.GetExits.AddQueryOption("$filter", "Employee_No eq '" + EmpNo + "'");
                BtnExit.Visible = true;
                htmlStr = Htmlresponse(newdata);               
            }
            catch (Exception exception)
            {
                Message("Error:" + exception.Message.ToString());
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
                                                                     <b> <a href='ExitApplication.aspx?An={1}&status={7}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                                                              </td>
                                                              </tr>",
                                                         counter,
                                                         collectionz.No,
                                                         collectionz.Exit_Reason,
                                                         collectionz.Description,
                                                         collectionz.Leave_Balance,
                                                         collectionz.Notice_Start_Date,
                                                         collectionz.Last_Date_of_Service,
                                                         collectionz.Status.ToString()



                                                     );
                }
            }
            catch (Exception ex)
            {
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
            return htmlStr;
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                String EmpNo = Session["username"].ToString();

                string ExitN = "";
                if (Session["Company"].ToString() == "ZAAC")
                {
                   // ExitN = MyComponents.HrService.CreateExit(EmpNo);
                }
                else
                {
                    ExitN = MyComponents.HrServiceZarib.CreateExit(EmpNo);
                }
                    
                if (!String.IsNullOrEmpty(ExitN))
                {
                    Session["ExitNo"] = ExitN;
                    //ExitNo.Text = ExitN;
                    Response.Redirect("ExitApplication.aspx?Tp='New'&status='New'");
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