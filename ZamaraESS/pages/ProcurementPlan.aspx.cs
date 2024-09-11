using BC21;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZamaraESS.pages
{
    public partial class ProcurementPlan : System.Web.UI.Page
    {
        public string ImpNo, IReqDate, IPurpose, ICompas, IRespon, IDept = "";

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string AppNo = Request.QueryString["An"].ToString();
            string RecStatus = Request.QueryString["status"].ToString();
            string paged = Request.QueryString["Tp"].ToString();
            Response.Redirect("PurchaseReqLines.aspx?An=" + AppNo + "&status=" + RecStatus + "&Tp=old");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                string RecStatus = Request.QueryString["status"].ToString();
                string paged = Request.QueryString["Tp"].ToString();
                if (RecStatus == "Open")
                {
                    btnLine.Visible = true;
                    //BindProcPlanGridviewData(Request.QueryString["An"].ToString());
                }
            }
        }

        //private void BindProcPlanGridviewData(string number)
        //{
        //    string GlobalDim1 = "", GlobalDim2 = "";
        //    try
        //    {
        //        string rtnMsg = MyComponents.ProcService.GetPurchReqHeaderDetails(number);
        //        if (!String.IsNullOrEmpty(rtnMsg))
        //        {
        //            string[] strdelimiters = new string[] { "::" };
        //            string[] rtnMsg_arr = rtnMsg.Split(strdelimiters, StringSplitOptions.None);

        //            GlobalDim1 = rtnMsg_arr[0];
        //            GlobalDim2 = rtnMsg_arr[1];
        //        }

        //        try
        //        {
        //            var data = MyComponents.OdataService.GetProcurementPlanLines.AddQueryOption("$filter", "No eq '" + number + "' and GlobalDimension2Code eq " + GlobalDim2 + "");
        //            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //            string json = serializer.Serialize(data);

        //            gvPlanLines.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
        //            gvPlanLines.DataBind();
        //        }
        //        catch (Exception ex)
        //        {
        //            ex.Data.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Clear();
        //    }

        //}


        private void Message(string p)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + p + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
        public void SuccessMessage(string strMsg)
        {

            string AppNo = Request.QueryString["An"].ToString();
            string RecStatus = Request.QueryString["status"].ToString();
            string paged = Request.QueryString["Tp"].ToString();

            string strScript = null;
            string myPage = "PurchaseReqLines.aspx?An=" + AppNo + "&status=" + RecStatus + "&Tp=old";
            strScript = "<script>";
            strScript = strScript + "alert('" + strMsg + "');";
            strScript = strScript + "window.location='" + myPage + "'";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }

        protected void btnLine_Click(object sender, EventArgs e)
        {
            try
            {
                #region commented - using webservice

                int count = 0;
                foreach (GridViewRow gvrow in gvPlanLines.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkRow");
                    if (chk.Checked)
                    {
                        count++;
                    }
                }

                if (count < 1)
                {
                    Message("Please Select Item(s).");
                    return;
                }
                foreach (GridViewRow gvr in this.gvPlanLines.Rows)
                {
                    CheckBox PlnLine = (CheckBox)gvr.Cells[0].FindControl("chkRow");
                    int Type = 0;
                    if (PlnLine.Checked)
                    {
                        string PlanNo = gvr.Cells[1].Text;
                        string PlanLineNo = gvr.Cells[3].Text;
                        string LineType = gvr.Cells[4].Text;
                        string ItemNo = gvr.Cells[5].Text;
                        string UnitOfMeasure = gvr.Cells[7].Text;
                        string QuantityAvailable = gvr.Cells[9].Text;
                        TextBox txtboxQuoteAmount = gvr.FindControl("txtQty") as TextBox;
                        string QuantityToRequest = txtboxQuoteAmount.Text;
                        if (string.IsNullOrEmpty(QuantityToRequest))
                        {
                            Message("Quantity to request MUST have a value in Item No.: " + ItemNo);
                            gvr.Cells[7].Focus();
                            return;
                        }
                        if (Convert.ToDecimal(QuantityToRequest) > Convert.ToDecimal(QuantityAvailable))
                        {
                            Message("Quantity to Request MUST not be greater than Quantity Available.");
                            gvr.Cells[7].Focus();
                            return;
                        }
                        if (Convert.ToDecimal(QuantityToRequest) <= 0)
                        {
                            Message("Quantity to Request MUST be greater than 0.");
                            gvr.Cells[7].Focus();
                            return;
                        }

                        if (LineType == "Direct Expense")
                        {
                            Type = 0;
                        }
                        else if (LineType == "Invetory Item")
                        {
                            Type = 1;
                        }
                        else if (LineType == "Fixed Asset")
                        {
                            Type = 2;
                        }

                        //Insert Plan Line Requested Quantity
                        MyComponents.ProcService.InsertPlanLineRequestedQuantity(PlanNo, Convert.ToInt32(PlanLineNo), Convert.ToDecimal(QuantityToRequest));

                        string rtnMsg = MyComponents.ProcService.InsertPurchaseLine(PlanNo, Convert.ToInt32(PlanLineNo), Session["ReqNo"].ToString(), ItemNo);
                        if (!String.IsNullOrEmpty(rtnMsg))
                        {
                            if (rtnMsg == "SUCCESS")
                            {
                                SuccessMessage("Item(s) Selected Successfully.");
                            }
                            else
                            {
                                Message(rtnMsg);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(ex);
                Message("ERROR: " + ex.Message.ToString());
                ex.Data.Clear();
            }
        }

    }
}