using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Osmdroid.Util;
using Osmdroid.Api;
using Osmdroid.Views.Overlay;
using Org.Osmdroid.Views.Overlay.Gestures;
using Android.App;
using static Android.Resource;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;
using Osmdroid.Views.Overlay.MyLocation;

namespace OffroadHelper.Resources.Fragments
{
    public class SampleWithMinimapItemizedOverlayWithFocus : BaseOSMMap
    {
        public static string Title = "Itemized overlay w/focus";

        private const int MenuZoominId = Android.Views.Menu.First;
        private const int MenuZoomoutId = MenuZoominId + 1;
        private const int MenuLastId = MenuZoomoutId + 1; // Always set to last unused id
        private MyLocationNewOverlay _myLocationOverlay;



        protected override void AddOverlays()
        {
            base.AddOverlays();
            /* Itemized Overlay */
            /* Create a static ItemizedOverlay showing some Markers on various cities. */

            var context = Activity;
            _myLocationOverlay = new MyLocationNewOverlay(context, new GpsMyLocationProvider(context), mapView);

            var items = new List<OverlayItem>
            {
                new OverlayItem("Moj Dom!", "Morsko ul. Zamkowa 25", new GeoPoint(50.54457, 19.50684)),
            };
            //19.50684
            /* OnTapListener for the Markers, shows a simple Toast. */
            var overlay = new ItemizedOverlayWithFocus(items, new OnItemGestureListener(Application.Context), ResourceProxy);
            //int resID = Resources.GetIdentifier(Resources, "drawcheese_1",);
            Drawable newMarker = new Drawable();
           // newMarker = Resources.GetDrawable(Resource.Drawable.cheese_1);
            overlay.SetFocusItemsOnTap(true);
            overlay.SetFocusedItem(0);
            mapView.Overlays.Add(_myLocationOverlay);
            _myLocationOverlay.EnableMyLocation();
            _myLocationOverlay.EnableFollowLocation();




            mapView.Overlays.Add(overlay);

            var rotationGestureOverlay = new RotationGestureOverlay(Activity, mapView) {Enabled = false};
            mapView.Overlays.Add(rotationGestureOverlay);

            /* MiniMap */
            var miniMapOverlay = new MinimapOverlay(Activity, mapView.TileRequestCompleteHandler);
            mapView.Overlays.Add(miniMapOverlay);

            // Zoom and center on the focused item.
            mapView.Controller.SetZoom(50);
           // var geoPoint = ((OverlayItem) overlay.FocusedItem).Point;
           // mapView.Controller.AnimateTo(geoPoint);
            //mapView.Controller.AnimateTo(_myLocationOverlay.MyLocation);
            
          //  HasOptionsMenu = true;
        }
        


        

        private class OnItemGestureListener : Object, ItemizedIconOverlay.IOnItemGestureListener
        {
            private readonly Context _context;

            public OnItemGestureListener(Context context)
            {
                _context = context;
            }

            public bool OnItemSingleTapUp(int index, Object item)
            {
                var overlayItem = (OverlayItem) item;
                Toast.MakeText(
                    _context,
                    "Item '" + overlayItem.Title + "' (index=" + index + ") got single tapped up", 
                    ToastLength.Long).Show();
                return true;
            }

            public bool OnItemLongPress(int index, Object item)
            {
                var overlayItem = (OverlayItem) item;
                Toast.MakeText(
                    _context,
                    "Item '" + overlayItem.Title + "' (index=" + index + ") got long pressed", 
                    ToastLength.Long).Show();
                return false;
            }
        }
    }
}