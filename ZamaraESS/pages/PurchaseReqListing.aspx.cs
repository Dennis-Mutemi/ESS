using System;
using System.Linq;
using System.Web.UI;

namespace ZamaraESS.pages
{
    public partial class PurchaseReqListing : System.Web.UI.Page
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
            if (Request.QueryString.AllKeys.Contains("Ac"))
            {
                string AppNo = Request.QueryString["An"].ToString();
                string RecStatus = Request.QueryString["status"].ToString();
                if (RecStatus == "Open")
                {
                    try
                    {
                        string message = "Do you want to delete this requisition?";
                        ClientScript.RegisterOnSubmitStatement(this.GetType(), "confirm", "return confirm('" + message + "');");
                        if (Session["Company"].ToString() == "ZAAC")
                        {
                            MyComponents.ProcService.DeletePurchaseRequisition(AppNo);
                        }else
                        {
                            MyComponents.ProcServiceZarib.DeletePurchaseRequisition(AppNo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Message("ERROR: " + ex.Message);
                    }
                }
                else
                {
                    Message("Document Status MUST be Open");
                }
            }
        }
        private void Message(string p)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + p + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
        protected string Jobs()
        {
            var htmlStr = string.Empty;
            try
            {
                string username = Session["username"].ToString();

                var data = MyComponents.OdataService.GetPurchaseRequisitions.AddQueryOption("$filter", "RequestedBy eq '" + username + "'");
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
                                                            <td>
                                                               <a href='PurchaseReqLines.aspx?An={1}&status={6}&Tp=old&Ac=up'><i class='fa fa-list'></i><span> Details</span></a> | 
                                                               <a href='PurchaseReqListing.aspx?An={1}&status={6}&Ac=del' onclick=""return confirm('Do you want to delete this requisition?')""><i class='fa fa-trash text-red'></i><span> Delete</span></a>		                                                  
                                                            </td>
                                                     </tr>",
                                                         counter,
                                                         collectionz.No,
                                                         collectionz.Description,
                                                         Microsoft.OData.Edm.Date.Parse(collectionz.DocumentDate.ToString()),
                                                         Microsoft.OData.Edm.Date.Parse(collectionz.ExpectedDeliveryDate.ToString()),
                                                         string.Format("{0:#,##0.00}", collectionz.Amount),
                                                         collectionz.Status
                                                         );
                }
            }
            catch (Exception exception)
            {
                Message("ERROR:" + exception.Message.ToString());                
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