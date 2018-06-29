using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GranaCurtaWeb.Models;

namespace GranaCurtaWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Session["user_token"] = hdnToken.Value;
            }

            if (Session["user_token"] != null)
            {
                if (TokenManager.ValidateToken((string)Session["user_token"]) != null)
                {
                    Response.Redirect("~/Sistema");
                }
                else
                {
                    Session["user_token"] = null;
                }
            }
        }
    }
}