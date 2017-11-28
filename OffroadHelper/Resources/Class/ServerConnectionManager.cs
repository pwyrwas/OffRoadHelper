using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Threading;
using System.Net;
using System.Collections.Specialized;

namespace OffroadHelper.Resources.Class
{
    class ServerConnectionManager
    {
        public const String serverPath = "http://www.offroadresque.eu/";
        public const String registrationPHP = "http://www.offroadresque.eu/registration.php";
        public const String loginPHP = "http://www.offroadresque.eu/login.php";

        WebClient client;

        public bool LogIn()
        {
            Uri uri = new Uri(serverPath+loginPHP);
            NameValueCollection parameters = new NameValueCollection();
            return true;
        }
    }
}