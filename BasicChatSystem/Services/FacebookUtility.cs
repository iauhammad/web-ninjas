using BasicChatSystem.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace BasicChatSystem.Services
{
    public class FacebookUtility
    {

        /// <summary>Function to make call to Facebook API to get user details</summary>
        /// <param name="code">Authentication code</param>
        /// <returns>Returns a facebook user object with personal details</returns>
        /// <author>Created by Irfaan on March 20, 2017</author>
        public static FacebookUser GetFacebookUserData(string code)
        {
            // Exchange the code for an access token
            Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"] + "/chatrooms.aspx&code=" + code);
            HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
            string token = str.ReadToEnd().ToString();
            dynamic objToken = JObject.Parse(token);

            // Split the access token and expiration from the single string
            string accessToken = objToken.access_token;
            HttpContext.Current.Session["accessToken"] = accessToken;   // Save access token in a session variable

            // Exchange the code for an extended access token
            Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

            StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            string eatToken = eatStr.ReadToEnd().ToString();
            dynamic objEatToken = JObject.Parse(eatToken);

            // Split the access token and expiration from the single string
            accessToken = objEatToken.access_token;

            // Request the Facebook user information
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,gender,locale,link,email&access_token=" + accessToken);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string jsondata = jsonResponse;
            FacebookUser objCurrentUser = sr.Deserialize<FacebookUser>(jsondata);

            return objCurrentUser;
        }

    }
}