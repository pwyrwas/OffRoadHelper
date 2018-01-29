using System;

using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
//using FragmentManager = Android.Support.V4.App.FragmentManager;


//using OFFroadRescue.Resources.Class;



namespace OffroadHelper.Resources.Fragments
{
    public class LocationMap : Fragment
    {
        private SupportMapFragment map;
        private GoogleMap mMapView;
        private MapView _mapView;
        private GoogleMap _map;
        private Marker _nmtMarker;
        private GoogleMap mMap;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public override void OnActivityCreated(Bundle p0)
        {
            base.OnActivityCreated(p0);
            MapsInitializer.Initialize(Activity);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.LocationMap, container, false);
            
            
            _mapView = view.FindViewById<MapView>(Resource.Id.map);
            _mapView.OnCreate(savedInstanceState);
      
            

            return view;
        }
        public override void OnStart()
        {
            base.OnStart();
            InitializeMapAndHandlers();
        }
       

        private void InitializeMapAndHandlers()
        {
            //SetUpMapIfNeeded();

            if (_map != null)
            {
                
                _map.MyLocation.Accuracy = 50;
                _map.MyLocation.Longitude = 50;
                _map.MyLocation.Bearing = 50;
                //MapView.FromCamera (new RectangleF (0, 0, 320, 100), camera);
                _map.MyLocationEnabled = true;
                _map.MapType = GoogleMap.MapTypeNormal;
                _map.MyLocationChange += MapOnMyLocationChange;
                _map.MarkerDragStart += MapOnMarkerDragStart;
                _map.MarkerDragEnd += MapOnMarkerDragEnd;

                var latLng = new LatLng(50.54, 19.49);

                var markerOptions = new MarkerOptions()
                        .SetPosition(latLng)
                        .Draggable(true);
                _nmtMarker = _map.AddMarker(markerOptions);
                _map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(latLng, 13));
            }
        }


        private void MapOnMarkerDragEnd(object sender, GoogleMap.MarkerDragEndEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MapOnMarkerDragStart(object sender, GoogleMap.MarkerDragStartEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MapOnMyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetUpMapIfNeeded()
        {
            if (null == _map)
            {

                //_map = View.FindViewById<MapView>(Resource.Id.map).GetMapAsync();

            }
        }

        
        private void SettingButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(Activity, "Button setttings works !", ToastLength.Long).Show();
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _mapView.OnDestroy();
        }


        public override void OnResume()
        {
            base.OnResume();
            SetUpMapIfNeeded();
            _mapView.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            _mapView.OnLowMemory();
        }
       

    }
}