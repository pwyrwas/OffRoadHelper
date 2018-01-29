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
using OffroadHelper.Resources.Class;

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
        private BaseOSMMap mOSMMap;
        private Home mHome;
        private SampleWithMinimapItemizedOverlayWithFocus mapWithLabel;
   
        private Stack<SupportFragment> mStackFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HomePage);

            SupportToolbar toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
           

            mLocationMap = new LocationMap();
            mOSMMap = new BaseOSMMap();
            mHome = new Home();

            mapWithLabel = new SampleWithMinimapItemizedOverlayWithFocus();
            mStackFragment = new Stack<SupportFragment>();
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, mLocationMap, "mainPage");
            trans.Add(Resource.Id.fragmentContainer, mOSMMap, "OSMMAP");
            trans.Add(Resource.Id.fragmentContainer, mapWithLabel, "MapWithLabel");
            trans.Add(Resource.Id.fragmentContainer, mHome, "Home");
            trans.Hide(mLocationMap);
            trans.Hide(mOSMMap);
            trans.Hide(mapWithLabel);
            trans.Commit();
            mCurrentFragment = mHome;//mMainPage;

            
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
            LogInModule lg = new LogInModule();
            View header = navigationView.GetHeaderView(0);
            TextView _userNameTXTVIEW = header.FindViewById<TextView>(Resource.Id.nav_userName);
            
            /*View view=navigationView.inflateHeaderView(R.layout.nav_header_main);*/
            _userNameTXTVIEW.Text = lg.GetUserName();

        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer((int)GravityFlags.Left);
                
                 ///   Intent intent = new Intent(this, typeof(OSMMap));
                 //   this.StartActivity(intent);
                //    this.Finish();
                    return true;
           
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.BringToFront();
            LogInModule lg = new LogInModule();
         
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                
                switch(e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home:
                        ShowFragment(mHome);
                        mDrawerLayout.CloseDrawers();
                        break;
                    case Resource.Id.nav_map:
                        ShowFragment(mapWithLabel);
                        mDrawerLayout.CloseDrawers();
                        break;
                    case Resource.Id.nav_logOut:
                        lg.setFalseRemeberMe(); //set RememberMe to false - autoLogin to NoAutoLogin
                        Intent intent = new Intent(this, typeof(MainActivity)); // i tutaj trzeba dodać że jak się wylogowuje to wpisuje w plik LogInData.xml false wtedy będzie sie mogło wrzucić okno do logowania
                        this.StartActivity(intent);
                        this.Finish();
                        break;
                    case Resource.Id.nav_set:
                        ShowFragment(mLocationMap);
                        mDrawerLayout.CloseDrawers();
                        break;
                    default:
                        mDrawerLayout.CloseDrawers();
                        break;
                }
                e.MenuItem.SetChecked(true);

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

