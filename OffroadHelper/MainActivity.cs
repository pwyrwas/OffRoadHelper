using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using Android.Content;
using Android.Graphics;
using OffroadHelper.Resources.Class;
using System.Linq;


namespace OffroadHelper
{
    [Activity(Label = "OffroadHelper", MainLauncher = true, Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private Button mBtnSignIn;
        dialog_SignUp signUpDialog;
        dialog_sign_in signInDialog;

        private ProgressBar mProgressBar;
        protected override void OnCreate(Bundle bundle)
        {
            Intent intent = new Intent(this, typeof(HomePage));
            base.OnCreate(bundle);
            LogInModule lg = new LogInModule();
            if (lg.checkAutoLogin()) // jeżeli tutaj przeczytam w pliku LogInData XML że jest true dla remenber me to się loguje i wbijam prosto do mainView/ jeżeli nie to okno startowe.
            {

                this.StartActivity(intent);
                this.Finish();
            }
            else
            {
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
                mBtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
                mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

                mBtnSignUp.Click += (object sender, EventArgs args) =>
                {
                    //Pull up dialog
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    signUpDialog = new dialog_SignUp();

                    signUpDialog.Show(transaction, "dialog fragment");
                    signUpDialog.mOnSignUpComplete += singUpDialog_mOnSingUpComplete;
                };
                mBtnSignIn.Click += (object sender, EventArgs args) =>
                {
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    signInDialog = new dialog_sign_in();

                    signInDialog.Show(transaction, "dialog fragment");
                    signInDialog.mOnSignInComplete += singInDialog_mOnSingInComplete;
                };
            }
        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            //color to sign wrong wrote filed
            Color colorWrong = Color.ParseColor("#FFCDD2");             //red
            Color colorGood = Color.ParseColor("#ffffff");              //white
            Color colorAllGood = Color.ParseColor("#64FFDA");           //Green

            try
            {
                EditText mtxtFirstName = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtFirstName);
                EditText mTxtEmail = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtEmail);
                Button mBtnSignUp = signUpDialog.View.FindViewById<Button>(Resource.Id.btnDialogEmail);
                EditText mTxtPassword = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtPassword);
                EditText mTxtPassword2 = signUpDialog.View.FindViewById<EditText>(Resource.Id.txtPassword2);
                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                if (result == "All fields must be filled in")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.AllfieldsmustBefilledIn), ToastLength.Long).Show();
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                    mTxtPassword.SetBackgroundColor(colorWrong);
                    mTxtEmail.SetBackgroundColor(colorWrong);
                    mTxtPassword2.SetBackgroundColor(colorWrong);
                }
                else
                {
                    mtxtFirstName.SetBackgroundColor(colorGood);
                    mTxtPassword.SetBackgroundColor(colorGood);
                    mTxtEmail.SetBackgroundColor(colorGood);
                    mTxtPassword2.SetBackgroundColor(colorGood);
                }

                if (result == "Email Adress are incorrect!")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.EmailAdressAreIncorrect), ToastLength.Long).Show();
                    mTxtEmail.SetBackgroundColor(colorWrong);
                }
                else if (result != "All fields must be filled in")
                    mTxtEmail.SetBackgroundColor(colorGood);

                if (result == "This login is arleady in use" && result != "All fields must be filled in")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.ThisLoginIsArleadyInUse), ToastLength.Long).Show();
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                }

                else if (result != "All fields must be filled in")
                {
                    mtxtFirstName.SetBackgroundColor(colorGood);
                }

                if (result == "Your password should have minmum 5 letters" && result != "All fields must be filled in")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.YourPasswordShouldHaveMinmum5Letters), ToastLength.Long).Show();
                    mTxtPassword.SetBackgroundColor(colorWrong);
                }
                else if (result != "All fields must be filled in")
                {
                    mTxtPassword.SetBackgroundColor(colorGood);
                }

                if (result == "Login should have minimum 4 sign" && result != "All fields must be filled in")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.LoginShouldhaveMinimum4Sign), ToastLength.Long).Show();
                    mtxtFirstName.SetBackgroundColor(colorWrong);
                }
                else if (result != "All fields must be filled in")
                {
                    mtxtFirstName.SetBackgroundColor(colorGood);
                }

                if (result == "Passwords are not the same" && result != "All fields must be filled in")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.PasswordsAreNotTheSame), ToastLength.Long).Show();
                    mTxtPassword2.SetBackgroundColor(colorWrong);
                }
                else if (result != "All fields must be filled in")
                {
                    mTxtPassword2.SetBackgroundColor(colorGood);
                }
                //at least
                if (result == "Account was created!")
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.AccountWasCreated), ToastLength.Long).Show();
                    mTxtPassword2.SetBackgroundColor(colorAllGood);
                    mtxtFirstName.SetBackgroundColor(colorAllGood);
                    mTxtPassword.SetBackgroundColor(colorAllGood);
                    mTxtEmail.SetBackgroundColor(colorAllGood);
                    mTxtPassword2.SetBackgroundColor(colorAllGood);
                    //don't knwo why colors not set at #64FFDA after all good field
                    Thread.Sleep(1000);
                    signUpDialog.Dismiss();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        void singUpDialog_mOnSingUpComplete(object sender, OnSignUpEvenArgs e)
        {
            createAccount("rejestruj", e.FirstName, e.Password, e.Password2, e.Email);
            mProgressBar.Visibility = Android.Views.ViewStates.Visible;
        }
        void singInDialog_mOnSingInComplete(object sender, OnSignInEvenArgs e)
        {

            EditText mtxtusername = signInDialog.View.FindViewById<EditText>(Resource.Id.txtLogin);
            EditText mtxtpassword = signInDialog.View.FindViewById<EditText>(Resource.Id.txtPassowrd);
            RadioButton rbRememberMe = signInDialog.View.FindViewById<RadioButton>(Resource.Id.rb_rememberMe);

            LogInModule lg = new LogInModule();
            lg.AddUserParams(mtxtusername.Text.ToString(), mtxtpassword.Text.ToString(), rbRememberMe.Checked);
            //login

            string txtpasswd = mtxtpassword.Text;
            string txtname = mtxtusername.Text;

            // set color if someting wrong
            if (txtname.Count() < 1 && txtpasswd.Count() < 1)
            {
                mtxtusername.SetBackgroundColor(lg.colorWrong);
                mtxtpassword.SetBackgroundColor(lg.colorWrong);
                Toast.MakeText(ApplicationContext, GetString(Resource.String.AllfieldsmustBefilledIn), ToastLength.Long).Show();
            }
            else if (txtname.Count() < 1)
            {
                mtxtusername.SetBackgroundColor(lg.colorWrong);
                mtxtpassword.SetBackgroundColor(lg.colorGood);
                Toast.MakeText(ApplicationContext, GetString(Resource.String.NeedLogin), ToastLength.Long).Show();
            }
            else if (txtpasswd.Count() < 1)
            {
                mtxtpassword.SetBackgroundColor(lg.colorWrong);
                mtxtusername.SetBackgroundColor(lg.colorGood);
                Toast.MakeText(ApplicationContext, GetString(Resource.String.NeedPassword), ToastLength.Long).Show();
            }
            else
            {
                mtxtusername.SetBackgroundColor(lg.colorGood);
                mtxtpassword.SetBackgroundColor(lg.colorGood);
                logiInRequest(txtname, txtpasswd);

            }
        }
        private bool logiInRequest(string sLogIn, string sPassword)
        {
            ServerConnectionManager SCM = new ServerConnectionManager();

            WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/login.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("loguj", "loguj");
            parameters.Add("login", sLogIn);
            parameters.Add("haslo", sPassword);

            client.UploadValuesCompleted += client_UploadValuesCompleted2;
            client.UploadValuesAsync(uri, "POST", parameters);
            return true;
        }
        void createAccount(string registrate, string s_login, string s_password1, string s_password2, string s_email)
        {

            WebClient client = new WebClient();
            Uri uri = new Uri("http://www.offroadresque.eu/registration.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("rejestruj", registrate);
            parameters.Add("login", s_login);
            parameters.Add("haslo1", s_password1);
            parameters.Add("haslo2", s_password2);
            parameters.Add("email", s_email);

            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, "POST", parameters);
        }
        private void client_UploadValuesCompleted2(object sender, UploadValuesCompletedEventArgs e)
        {
            //lDialog.View.FindFocus();
            //color to sign wrong wrote filed
            Color colorWrong = Color.ParseColor("#FFCDD2");             //red
            Color colorGood = Color.ParseColor("#ffffff");              //white
            Color colorAllGood = Color.ParseColor("#64FFDA");           //Green
            try
            {
                //login
                EditText mtxtusername = signInDialog.View.FindViewById<EditText>(Resource.Id.txtLogin);
                EditText mtxtpassword = signInDialog.View.FindViewById<EditText>(Resource.Id.txtPassowrd);
                RadioButton rbRememberMe = signInDialog.View.FindViewById<RadioButton>(Resource.Id.rb_rememberMe);

                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                if (result.Contains("Wrong data") && mtxtpassword.Length() < 1)
                {
                    //nie działa czemu ?:(
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.AllfieldsmustBefilledIn), ToastLength.Long).Show();
                    mtxtusername.SetBackgroundColor(colorWrong);
                    mtxtpassword.SetBackgroundColor(colorWrong);


                }
                else if (result.Contains("Wrong data") && !(mtxtpassword.Length() > 1))
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.AllfieldsmustBefilledIn), ToastLength.Long).Show();
                    mtxtusername.SetBackgroundColor(colorWrong);
                    mtxtpassword.SetBackgroundColor(colorWrong);
                }
                else
                {
                    mtxtusername.SetBackgroundColor(colorGood);
                    mtxtpassword.SetBackgroundColor(colorGood);
                }
                if (result.Contains("Success"))
                {
                    Toast.MakeText(ApplicationContext, GetString(Resource.String.logInSuccess), ToastLength.Long).Show();
                    mtxtusername.SetBackgroundColor(colorAllGood);
                    mtxtpassword.SetBackgroundColor(colorAllGood);

                    mProgressBar.Visibility = Android.Views.ViewStates.Visible;
                    LogInModule lg = new LogInModule();
                    lg.AddUserParams(mtxtusername.ToString(), mtxtpassword.ToString(), rbRememberMe.Checked);
                    lg.saveLoginData();
                    //don't knwo why colors not set at #64FFDA after all good field
                    Thread.Sleep(1000);
                    signInDialog.Dismiss();
                    Thread thread = new Thread(logInSuccessProcess);
                    thread.Start();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }
        private void logInSuccessProcess() //request to database in the future
        {
            //now it is for nothing ;)
            Intent intent = new Intent(this, typeof(HomePage));
            this.StartActivity(intent);
            this.Finish();
        }

    }
}

