using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.App;
using System.Collections.Generic;
using Java.Lang;
using OffroadHelper.Resources.Fragments;
namespace OffroadHelper
{
    [Activity(Label = "OffroadHelper", MainLauncher = false, Theme = "@style/Theme.DesignDemo")]
    public class HomePage : AppCompatActivity
    {
        private DrawerLayout mDrawerLayout;
        private SupportToolbar mToolbar;
       
        
        private ListView mLeftDrawer;
        //Create option on left drawer
        private ArrayAdapter mLeftAdapter;
        private List<string> mLeftDataSet;
        private SupportFragment mCurrentFragment;
        private LocationMap mLocationMap;
   
        private Stack<SupportFragment> mStackFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HomePage);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);

            mLocationMap = new LocationMap();
            mStackFragment = new Stack<SupportFragment>();
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, mLocationMap, "mainPage");
            trans.Hide(mLocationMap);
            trans.Commit();
            mCurrentFragment = mLocationMap;//mMainPage;

            SetSupportActionBar(toolBar);

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            ab.SetDisplayHomeAsUpEnabled(true);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigationView != null)
            {
                SetUpDrawerContent(navigationView);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                    ShowFragment(mLocationMap);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);
                mDrawerLayout.CloseDrawers();
            };
        }
        private void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible) return;

            SupportActionBar.SetTitle(Resource.String.settingsDrawer);
            var trans = SupportFragmentManager.BeginTransaction();
            trans.SetCustomAnimations(Resource.Animation.slide_right, Resource.Animation.slide_right, Resource.Animation.slide_right, Resource.Animation.slide_right);
            trans.Hide(mCurrentFragment);
            trans.Show(fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            mStackFragment.Push(mCurrentFragment);
            mCurrentFragment = fragment;
        }
    }
}

