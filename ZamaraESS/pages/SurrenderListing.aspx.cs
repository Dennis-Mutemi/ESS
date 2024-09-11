using System;
using System.Globalization;

namespace ZamaraESS.pages
{
    public partial class SurrenderListing : System.Web.UI.Page
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
            try
            {
                string username = Session["username"].ToString();

                var data = MyComponents.OdataService.GetImprestSurrender.AddQueryOption("$filter", "EmployeeNo eq '" + username + "' and PaymentType eq 'Imprest Surrender'");
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
                                                            <td>
                                                                   <b> <a href='SurrenderLines.aspx?An={1}&status={9}&Tp=old'><i class='fa fa-list text-primary'></i><span class='text-primary'> Details</span></a></b>
                                                            </td>
                                                            </tr>",
                                                         counter,
                                                         collectionz.No,
                                                         collectionz.Description,
                                                         collectionz.ImprestNo,
                                                         collectionz.PaymentMode,
                                                         string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(collectionz.TotalAmount).ToString(CultureInfo.InvariantCulture))),
                                                         string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(collectionz.ActualAmountSpent).ToString(CultureInfo.InvariantCulture))),
                                                         string.Format("{0:#,0.00}", Convert.ToDecimal(Convert.ToDouble(collectionz.ReceiptAmount).ToString(CultureInfo.InvariantCulture))),
                                                         Microsoft.OData.Edm.Date.Parse(collectionz.DateCreated.ToString()),
                                                         collectionz.Status.ToString()
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