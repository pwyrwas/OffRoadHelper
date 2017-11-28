using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace OffroadHelper
{
    public class OnSignInEvenArgs : EventArgs
    {
        private string mLogin;
        private string mPassword;
        private bool mRememberMe;


        public string Login
        {
            get { return mLogin; }
            set { mLogin = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public bool RememberMe
        {
            get { return mRememberMe; }
            set { mRememberMe = value; }
        }
        


        public OnSignInEvenArgs(string login, string password, bool  rememberMe) : base()
        {
            Login = login;
            Password = password;
            RememberMe = rememberMe;
            
        }
    }
    class dialog_sign_in : DialogFragment
   
    {
        private EditText mtxtLogin;
        private EditText mTxtPassword;
        private RadioButton rb_rememberMe;
        private Button mBtnSignIn;

        public event EventHandler<OnSignInEvenArgs> mOnSignInComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_in, container, false);
            //var view = inflater.Inflate(Resource.Layout.dialog_sign_in, container, false);

            mtxtLogin = view.FindViewById<EditText>(Resource.Id.txtLogin);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassowrd);
            mBtnSignIn = view.FindViewById<Button>(Resource.Id.btnSignIn);
            rb_rememberMe = view.FindViewById<RadioButton>(Resource.Id.rb_rememberMe);

            mBtnSignIn.Click += mBtnSignIn_Click;

            return view;
        }
        void mBtnSignIn_Click(object sender, EventArgs e)
        {
            //User has clicked the sign up button 
            mOnSignInComplete.Invoke(this, new OnSignInEvenArgs(mtxtLogin.Text, mTxtPassword.Text, rb_rememberMe.Checked));
            //this.Dismiss();
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the tite bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // set the animation
        }
    }
}