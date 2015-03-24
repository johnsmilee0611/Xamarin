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
using Xamarin.Auth;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace XamarinAuthenticationExample
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class Login : Activity
    {
		private GooglePlusAuthentication _googlePlusAuth;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

			ImageButton btnLoginGoogle = FindViewById<ImageButton> (Resource.Id.btnLoginGoogle);
			btnLoginGoogle.Click += BtnLoginGoogle_Click;

			ImageButton btnLoginFacebook = FindViewById<ImageButton> (Resource.Id.btnLoginFacebook);
			btnLoginFacebook.Click += BtnLoginFacebook_Click;


        }

		private void BtnLoginGoogle_Click (object sender, EventArgs e)
		{
			_googlePlusAuth = new GooglePlusAuthentication (this);
			_googlePlusAuth.OnAuthenticationCompleted += GooglePlusAuth_Completed;
			_googlePlusAuth.LoginByGoogle ();

		}

		void BtnLoginFacebook_Click (object sender, EventArgs e)
		{
			// TO-DO: Implement later
		}

		private void GooglePlusAuth_Completed (bool isAuthenticated)
		{
			if(isAuthenticated)
			{
				StartActivity (typeof(MainActivity));
			}
		}
	}
}