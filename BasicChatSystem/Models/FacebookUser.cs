using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicChatSystem.Models
{
    public class FacebookUser
    {
        /// <summary>User's Facebook Id</summary>
        public string id { get; set; }

        /// <summary>First Name</summary>
        public string first_name { get; set; }

        /// <summary>Last Name</summary>
        public string last_name { get; set; }

        /// <summary>Link to user's Facebook page</summary>
        public string link { get; set; }

        /// <summary>E-mail Address</summary>
        public string email { get; set; }

        /// <summary>Username</summary>
        public string username { get; set; }

        /// <summary>Gender</summary>
        public string gender { get; set; }

        /// <summary>Language Preference</summary>
        public string locale { get; set; }

        /// <summary>Returns user's full name</summary>
        public string full_name
        {
            get
            {
                return string.Format("{0} {1}", this.first_name, this.last_name);
            }
        }

        /// <summary>Returns user's profile picture source</summary>
        public string profile_picture
        {
            get
            {
                return string.Format("http://graph.facebook.com/{0}/picture?type=large", this.id);
            }
        }

    }
}