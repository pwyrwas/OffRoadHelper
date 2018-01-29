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
using Android.Locations;

namespace OffroadHelper.Resources.Class
{
    class user
    {
        private string u_name;
        private string u_login;
        private int u_age;
        private string u_city;
        private Location u_currentLocation;
        private Location u_lastLocation;
        private DateTime u_lastActivity;

        public void SetUserName(string name) => u_name = name;
        public void SetLogin(string login) => u_login = login;
        public void SetUserAge(int age) => u_age = age;
        public void SetUserCity(string city) => u_city = city;
        public void SetUserCurrentLocation(Location location) => u_currentLocation = location;
        public void SetUserLastLocation(Location location) => u_lastLocation = location;
        public void SetUserLastActivity(DateTime dataAndTime) => u_lastActivity = dataAndTime;

        public string UserName => u_name;
        public string GetLogin() => u_login;
        public int GetUserAge() => u_age;
        public string GetUserCity() => u_city;
        public DateTime GetLastActivity() => u_lastActivity;
        public Location GetUserLocation() => u_currentLocation;
        private Location LastUserLocation => u_lastLocation;
    }
}