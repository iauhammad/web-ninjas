using BasicChatSystem.Models;
using BasicChatSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicChatSystem
{
    public partial class chatrooms : System.Web.UI.Page
    {
        // Global variable holding logged in user details
        FacebookUser objUserDetails = null;

        #region Protected Methods

        /// <summary>Function triggered each time the page loads</summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        /// <author>Created by Irfaan on March 11, 2017</author>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Facebook code from the querystring
                if (Request.QueryString["code"] != "")
                {
                    if(Session["userDetails"] == null)
                    {
                        objUserDetails = FacebookUtility.GetFacebookUserData(Request.QueryString["code"]);
                        Session["userDetails"] = objUserDetails;
                    } else
                    {
                        objUserDetails = (FacebookUser)Session["userDetails"];
                    }
                }
                else
                {
                    Response.Redirect("default.aspx");
                }

                // Display username
                lblUsername.Text = objUserDetails.full_name;
            }
                        
        }

        /// <summary>Function to destroy user's current session on Logout</summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        /// <author>Created by Irfaan on March 11, 2017</author>
        protected void LogoutLink_Click(object sender, EventArgs e)
        {
            // Logging out actually redirects to Facebook to manually disconnect
            string sAppURL = "http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/default.aspx";
            sAppURL = HttpUtility.UrlEncode(sAppURL);
            string logoutURL = "https://www.facebook.com/logout.php?next=" + sAppURL + "&access_token=" + Session["accessToken"].ToString();

            // Clear session variables
            Session.RemoveAll();
            Session.Abandon();

            Response.Redirect(logoutURL);        
        }

        #endregion

    }
}