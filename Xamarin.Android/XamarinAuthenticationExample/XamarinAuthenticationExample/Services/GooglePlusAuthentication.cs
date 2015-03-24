using System;

using Android.Content;
using Xamarin.Auth;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace XamarinAuthenticationExample
{
	public class GooglePlusAuthentication
	{
		private const string CLIENT_ID = "<your's client id>";
		private const string SCOPE = "https://www.googleapis.com/auth/userinfo.email";
		private const string AUTHORIZE_URL = "https://accounts.google.com/o/oauth2/auth";
		private const string REDIRECT_URL = "https://www.example.com/oauth2callback";
		private const string ACCESS_TOKEN_URL = "https://www.googleapis.com/oauth2/v1/userinfo?access_token";
		private Context _context;

		public GoogleUser UserInfo
		{
			get;
			set;
		}

		public Action<bool> OnAuthenticationCompleted
		{
			get;
			set;
		}

		public GooglePlusAuthentication(Context context)
		{
			_context = context;
		}

		public void LoginByGoogle()
		{
			var auth = new OAuth2Authenticator (
				clientId: CLIENT_ID,
				scope: SCOPE,
				authorizeUrl: new Uri (AUTHORIZE_URL),
				redirectUrl: new Uri(REDIRECT_URL)
			);

			auth.Completed += Auth_Completed;

			var intent = auth.GetUI (_context);
			_context.StartActivity (intent);
		}

		private void Auth_Completed (object sender, AuthenticatorCompletedEventArgs e)
		{
			if(e.IsAuthenticated)
			{
				string access_token;
				e.Account.Properties.TryGetValue ("access_token", out access_token);

				GetUserInfo (access_token);
			}

			if(OnAuthenticationCompleted != null)
			{
				OnAuthenticationCompleted (e.IsAuthenticated);
			}
		}

		private async void GetUserInfo (string access_token)
		{
			string userInfo = await GetDataFromGoogle (access_token); 

			if(userInfo.Equals("Exception"))
			{
				//Deserialize to GoogleUserInfo object
				UserInfo = JsonConvert.DeserializeObject<GoogleUser> (userInfo);
			}
		}

		private async Task<string> GetDataFromGoogle (string access_token)
		{
			string strURL = string.Format("{0}={1}",ACCESS_TOKEN_URL,access_token);
			WebClient client = new WebClient ();
			string strResult = string.Empty;

			try
			{
				strResult = await client.DownloadStringTaskAsync(new Uri(strURL));
				Console.WriteLine("Downloaded");
			}
			catch 
			{
				strResult = "Exception";
			}
			finally 
			{
				client.Dispose ();
				client = null;
			}

			return strResult;
		}
	}
}

