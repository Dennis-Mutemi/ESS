﻿using System;
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
    public partial class pnineform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                LoadYears();
                LoadP9();
            }
        }
        protected void LoadYears()
        {
            try
            {
                this.ddlYear.Items.Clear();
                ListItem li = null;
                var data = MyComponents.OdataService.GetPayslipPeriods.AddQueryOption("$filter", "Closed eq true");

                HashSet<string> alreadyEncountered = new HashSet<string>();
                foreach (var ym in data)
                {

                    string value = "1 " + ym.Period_Name;
                    DateTime period = DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault);

                    if (alreadyEncountered.Contains(period.Year.ToString()))
                    {
                        continue;
                    }


                    li = new ListItem(
                                      period.Year.ToString(),
                                      period.Year.ToString()
                                  );
                    this.ddlYear.Items.Add(li);

                    alreadyEncountered.Add(period.Year.ToString());
                }
            }
            catch (Exception ex)
            {

                ex.Data.Clear();
            }
        }
        protected void LoadP9()
        {
            try
            {
                string period = ddlYear.SelectedValue;

                string Base64Report = MyComponents.HrService.Generatep9Report(Session["username"].ToString(),Convert.ToInt32(period));

                if (Base64Report.Length > 0)
                {
                    string Content = "data:application/pdf;base64," + Base64Report;
                    myPDF.Attributes.Add("src", Content);
                }
                else
                {
                    Message("There is no report within the filter.");
                    myPDF.Attributes.Add("src", null);
                }
            }
            catch (Exception exception)
            {
                Message("ERROR: " + exception.Message);
            }
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadP9();
        }

        private void Message(string p)
        {
            string strScript = null;
            strScript = "<script>";
            strScript = strScript + "alert('" + p + "');";
            strScript = strScript + "</script>";
            Page.RegisterStartupScript("ClientScript", strScript.ToString());
        }
    }
}