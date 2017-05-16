using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicChatSystem
{
    public partial class _default : System.Web.UI.Page
    {
        
        #region Protected Methods

        /// <summary>Function triggerd each time page loads</summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        /// <author>Created by Irfaan on March 11, 2017</author>
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>Function called when user clicks on the 'Join' button</summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        /// <author>Created by Irfaan on March 11, 2017</author>
        protected void btnJoin_Click(object sender, EventArgs e)
        {
            // -- Redirect to Facebook to authenticate user
            string sAuthenticateURL = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/chatrooms.aspx&response_type=code&state=1&scope=email";
            Response.Redirect(sAuthenticateURL);

        }

        #endregion

    }
}