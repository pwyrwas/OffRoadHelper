using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Org.Osmdroid.Library;
using Osmdroid.Views;
using Osmdroid.Views.Drawing;
using Osmdroid.TileProvider.TileSource;
using Android.Content.Res;
using Android.Preferences;
using Osmdroid.Util;
using Android.Support.V4.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Support.V7.App;
using Android.Graphics.Drawables;
using Android.Graphics;
using Osmdroid;
using OffroadHelper.Resources.Class;

namespace OffroadHelper
{
    public class Home : Fragment
    {
        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            View view = inflater.Inflate(Resource.Layout.Home, container, false);

            LogInModule lg = new LogInModule();
            TextView _userNameTXTVIEW = view.FindViewById<TextView>(Resource.Id.nav_userNameHome);
            _userNameTXTVIEW.Text = lg.GetUserName();

            return view;
        }
        public void OnResume()
        {
            base.OnResume();
            
        }
    }
    
}


