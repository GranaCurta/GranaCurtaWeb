using GranaCurtaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GranaCurtaWeb
{
    public partial class SiteSistema : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool blnIsvalidToken = false;

                if (Session["user_token"] != null)
                {
                    if (TokenManager.ValidateToken((string)Session["user_token"]) != null)
                    {
                        blnIsvalidToken = true;
                    }
                }

                if (blnIsvalidToken)
                {
                    hdnToken.Value = (string)Session["user_token"];
                }
                else
                {
                    Session["user_token"] = null;
                    hdnToken.Value = "";
                    Response.Redirect("~/Login");
                }
            }
        }

        protected void btnLogOff_Click(object sender, EventArgs e)
        {
            Session["user_token"] = null;
            Response.Redirect("~/");
        }
    }
}