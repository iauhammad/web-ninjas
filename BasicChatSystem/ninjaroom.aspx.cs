using BasicChatSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace BasicChatSystem
{
    public partial class ninjaroom : System.Web.UI.Page
    {
        #region Global Variables

        // -- Global variables declaration
        string sRoomId = string.Empty;  // ID of chatroom
        string sChatroomName = string.Empty;    // Name of chatroom
        DateTime? dCreatedOn = null;    // Date chatroom was created
        FacebookUser objUser = null;    // Object with user details
        string sMainAdministrator = string.Empty;   // Main(first) administrator of the room

        // -- Static variables to be used when AJAX calls occur
        private static string sChatRoomId = string.Empty;
        private static string sXMLPath = string.Empty;

        #endregion

        #region Protected Methods

        /// <summary>Function triggered each time the page loads</summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        /// <author>Created by Irfaan on March 11, 2017</author>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Find the room id user wants to join
            sRoomId = Request.QueryString["roomid"];

            // If user not signed in, redirect to home page
            if(Session["userDetails"] == null)
            {
                Response.Redirect("default.aspx");
            }
            objUser = (FacebookUser)Session["userDetails"];
            sChatRoomId = sRoomId;
            sXMLPath = Request.PhysicalApplicationPath + "App_data/messages.xml";

            // Verify if chatroom exists
            // -> If not exists - display not found message
            // -> If exists - display chatroom details and messages
            if (fChatRoomNotFound(sRoomId))
            {
                Response.Redirect("ChatroomNotFound.aspx");
            }
            else
            {
                // Details of chatroom
                chatRoomName.InnerText = sChatroomName;
                chatRoomAdmin.InnerText = sMainAdministrator;
                chatRoomCreatedOn.InnerText = dCreatedOn.Value.ToLongDateString();

                // Display messages for the particular chatroom
                pReadMessagesFromFile(objUser.id);
            }

        }

        #endregion

        #region Public Web Methods

        /// <summary>Function called using AJAX to save a new message in XML file</summary>
        /// <param name="sMessage">String representing new message sent by user</param>
        /// <returns>Returns the updated string of messages</returns>
        /// <author>Created by Irfaan on March 21, 2017</author>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SaveMessage(string sMessage)
        {
            // Variables declaration
            string sResult = string.Empty;

            // Get the current user's details from the session variable
            FacebookUser objCurrentUser = (FacebookUser)HttpContext.Current.Session["userDetails"];

            // Add Message to XML file
            pAddMessageToFile(sMessage, objCurrentUser);
            
            // Fetch updated messages
            sResult = sFetchAllMessages(objCurrentUser.id);

            return sResult;
        }

        /// <summary>Function to fetch all messages to refresh chatbox</summary>
        /// <returns>Returns all messages</returns>
        /// <author>Created by Irfaan on March 21, 2017</author>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string ReloadMessage()
        {
            // Get current user from session variable
            FacebookUser objCurrentUser = (FacebookUser)HttpContext.Current.Session["userDetails"];
            // Fetch all messages from file
            return sFetchAllMessages(objCurrentUser.id);
        }

        /// <summary>Function to get current user's details from the session variable</summary>
        /// <returns>Returns the ID, Name and Email of the current user</returns>
        /// <author>Created by Irfaan on March 21, 2017</author>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string GetSessionDetails()
        {
            // Get current user from the session variable
            FacebookUser objCurrentUser = (FacebookUser)HttpContext.Current.Session["userDetails"];

            // Construct string of user details
            string sUserSession = string.Format("User Id: {0} | User Name: {1} | User Email: {2}", 
                                                objCurrentUser.id, objCurrentUser.full_name, objCurrentUser.email);

            return sUserSession;
        }

        #endregion

        #region Private Method

        /// <summary>Function to read messages from file and display on screen</summary>
        /// <author>Created by Irfaan on March 11, 2017</author>
        private void pReadMessagesFromFile(string sCurrentUserId)
        {
            // Display messages on web page
            messagesContainer.InnerHtml = sFetchAllMessages(sCurrentUserId);
        }

        private static void pAddMessageToFile(string sMessageToSave, FacebookUser objUser)
        {
            try
            {
                // 1. Load the XML document
                //string sXMLPath = Request.PhysicalApplicationPath + "App_data/messages.xml";
                XmlDocument xdMessages = new XmlDocument();
                xdMessages.Load(sXMLPath);

                // 2. Create a new <message> element with child elements
                XmlElement xeMessage = xdMessages.CreateElement("message");
                XmlAttribute xaMessageId = xdMessages.CreateAttribute("messageId");
                XmlAttribute xaChatRoomId = xdMessages.CreateAttribute("chatRoomId");
                XmlNode xnTextMessage = xdMessages.CreateElement("textMessage");
                XmlNode xnSentAt = xdMessages.CreateElement("messageSentAt");
                XmlElement xeSender = xdMessages.CreateElement("sender");
                XmlAttribute xaUserId = xdMessages.CreateAttribute("userId");
                XmlNode xnEmail = xdMessages.CreateElement("email");
                XmlNode xnSenderName = xdMessages.CreateElement("displayName");
                XmlElement xeAvatar = xdMessages.CreateElement("avatar");
                XmlAttribute xaTitle = xdMessages.CreateAttribute("title");

                // 3. Add value to nodes & attributes created
                xaMessageId.Value = iDetermineMessageId(xdMessages).ToString();
                xaChatRoomId.Value = sChatRoomId;
                var sCDATA = xdMessages.CreateCDataSection(sMessageToSave);
                xnTextMessage.AppendChild(sCDATA);
                xnSentAt.InnerText = String.Format("{0:s}", DateTime.Now);
                xaUserId.Value = objUser.id;
                xnEmail.InnerText = objUser.email;
                xnSenderName.InnerText = objUser.full_name;
                xaTitle.Value = "User's profile picture from Facebook";
                xeAvatar.InnerText = objUser.profile_picture;

                // 4. Form XML <message> element
                xeMessage.SetAttributeNode(xaMessageId);
                xeMessage.SetAttributeNode(xaChatRoomId);
                xeMessage.AppendChild(xnTextMessage);
                xeMessage.AppendChild(xnSentAt);
                xeSender.SetAttributeNode(xaUserId);
                xeSender.AppendChild(xnEmail);
                xeSender.AppendChild(xnSenderName);
                xeAvatar.SetAttributeNode(xaTitle);
                xeSender.AppendChild(xeAvatar);
                xeMessage.AppendChild(xeSender);

                // 5. Append <message> element to root
                XmlNode xnRoot = xdMessages.SelectSingleNode("/messages");
                xnRoot.AppendChild(xeMessage);

                // 6. Save changes to the XML file
                xdMessages.Save(sXMLPath);
            }
            catch
            {
                // If an error occurs, don't break the app flow
            }
        }

        #endregion

        #region Private Functions

        /// <summary>Function to determine if a chatroom exists or not</summary>
        /// <param name="sRoomId">ID of chatroom user looking for</param>
        /// <returns>Returns TRUE/FALSE to indicate if chatroom has not been found</returns>
        /// <author>Created by Irfaan on March 11, 2017</author>
        private bool fChatRoomNotFound(string sRoomId)
        {
            // Variables declaration
            string sChatroomXML;
            XElement Chatrooms;
            XElement xeRoom;
            
            // Initialisations
            sChatroomXML = string.Concat(Request.PhysicalApplicationPath, "App_Data/chatrooms.xml");
            Chatrooms = XElement.Load(sChatroomXML);

            // Try to find the chatroom
            try
            {
                xeRoom = (from el in Chatrooms.Elements("chatRoom")
                          where (string)el.Attribute("roomId") == sRoomId
                          select el).First();
                sChatroomName = xeRoom.Element("chatRoomName").Value;
                dCreatedOn = Convert.ToDateTime(xeRoom.Element("createdAt").Value);
                sMainAdministrator = xeRoom.Element("administrators").Element("administrator").Element("displayName").Value;
            }
            catch
            {
                // Chatroom not found
                xeRoom = null;
            }
            return (xeRoom == null);
        }

        /// <summary>Function to determine the next available ID for a new message</summary>
        /// <param name="xmlDoc">XML document containing the chat messages</param>
        /// <returns>Returns the next available message ID</returns>
        /// <author>Created by Irfaan on March 11, 2017</author>
        private static int iDetermineMessageId(XmlDocument xmlDoc)
        {
            // Variable to hold max ID
            int iMaxId = 0;

            // Get all <message> element and determine max id
            XmlNodeList lstMessages = xmlDoc.GetElementsByTagName("message");
            foreach(XmlNode xeMessage in lstMessages)
            {
                int iMessageId = Convert.ToInt32(xeMessage.Attributes["messageId"].Value);
                if (iMessageId > iMaxId)
                    iMaxId = iMessageId;
            }

            // Return next available ID
            return (iMaxId+1);            
        }

        /// <summary>Function to fetch all messages of a chatroom</summary>
        /// <param name="sCurrentUserId">Current user accessing the messages</param>
        /// <returns>Returns constructed messages to the client side</returns>
        /// <author>Created by Irfaan on March 21, 2017</author>
        private static string sFetchAllMessages(string sCurrentUserId)
        {
            // Variables initialisation
            StringBuilder sChatMessages = new StringBuilder();

            // Fetch all messages once XML file has been loaded
            if (!string.IsNullOrEmpty(sXMLPath))
            {
                XDocument xmlDocMessages = XDocument.Load(sXMLPath);

                // Using LINQ to XML, find all messages
                var lstMessages = xmlDocMessages.Element("messages").Elements();
                foreach (var objMessage in lstMessages)
                {
                    if (string.Equals(objMessage.Attribute("chatRoomId").Value, sChatRoomId))
                    {
                        string sMessage = string.Format("<div class=\"bubble-container\"><div class=\"{0}\"><div class=\"profile-pic\"><img src=\"{4}\" alt=\"Avatar picture\" /></div><span class=\"bubble-container__name\">{1}</span><br /><span class=\"bubble-container__msg\">{2}</span><br /><span class=\"bubble-container__time\">{3}</span></div></div>",
                                                        (string.Equals(sCurrentUserId, objMessage.Element("sender").Attribute("userId").Value)) ? "bubble-me" : "bubble-others",
                                                        objMessage.Element("sender").Element("displayName").Value,
                                                        objMessage.Element("textMessage").Value,
                                                        Convert.ToDateTime(objMessage.Element("messageSentAt").Value).ToString("F", CultureInfo.CreateSpecificCulture("en-US")),
                                                        objMessage.Element("sender").Element("avatar").Value);
                        sChatMessages.AppendLine(sMessage);
                    }
                }
            }

            // Display messages on web page
            return sChatMessages.ToString();
        }

        #endregion

    }
}