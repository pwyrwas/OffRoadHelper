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

namespace OffroadHelper
{
    public class BaseOSMMap : Fragment
    {
        protected MapController mMapController;
        protected object FragmentView;
        protected MapView mapView;
        protected IResourceProxy ResourceProxy;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            AddOverlays();

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ResourceProxy = new DefaultResourceProxyImpl(inflater.Context.ApplicationContext);
            View view = inflater.Inflate(Resource.Layout.OSMMap, container, false);

            mapView = view.FindViewById<MapView>(Resource.Id.map);
            mapView.SetTileSource(TileSourceFactory.DefaultTileSource);
            mapView.SetBuiltInZoomControls(true);
            mapView.SetMultiTouchControls(true);
            var mapController = mapView.Controller;
            mapController.SetZoom(25);

            var centreOfMap = new GeoPoint(50.54, 19.49);
            mapController.SetCenter(centreOfMap);
            
            return view;
        }
        private Context getApplicationContext()
        {
            throw new NotImplementedException();
        }

        public void OnResume()
        {
            base.OnResume();
            //Configuration.getInstance().load(this, Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(this));
        }

        protected virtual void AddOverlays()
        {

        }  

      
    }
    
}


