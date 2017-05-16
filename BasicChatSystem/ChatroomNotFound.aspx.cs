using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicChatSystem
{
    public partial class ChatroomNotFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("chatrooms.aspx");
        }
    }
}