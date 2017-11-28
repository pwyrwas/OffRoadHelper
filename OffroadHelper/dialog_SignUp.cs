using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace OffroadHelper
{
    public class OnSignUpEvenArgs : EventArgs
    {
        private string mFirstName;
        private string mEmail;
        private string mPassword;
        private string mPassword2;
        

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value;  }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public string Password2
        {
            get { return mPassword2; }
            set { mPassword2 = value; }
        }


        public OnSignUpEvenArgs(string firstName, string email, string password, string password2) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
            Password2 = password2;
        }
    }

    class dialog_SignUp : DialogFragment
    {
        private EditText mtxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private EditText mTxtPassword2;
        private Button mBtnSignUp;
       

        public event EventHandler<OnSignUpEvenArgs> mOnSignUpComplete;
        public event EventHandler<OnSignUpEvenArgs> seding;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mtxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mTxtPassword2 = view.FindViewById<EditText>(Resource.Id.txtPassword2);

            mBtnSignUp.Click += mBtnSignUp_Click;
            
            return view;
        }
        void sends(object sender, EventArgs e)
        {
            mOnSignUpComplete.Invoke(this, new OnSignUpEvenArgs(mtxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text, mTxtPassword2.Text));
        }
        void mBtnSignUp_Click(object sender, EventArgs e)
        {
            //User has clicked the sign up button 
            mOnSignUpComplete.Invoke(this, new OnSignUpEvenArgs(mtxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text, mTxtPassword2.Text));
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