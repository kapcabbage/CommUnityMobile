using Android.App;
using Android.Content.PM;
using Android.OS;
using FreshMvvm;
using FreshWithSQLite.Core;
using Syncfusion.SfChart.XForms.Droid;

namespace FreshWithSQLite.Droid
{
	[Activity(Label = "CommUnityMobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
            new SfChartRenderer();
            LoadApplication(new App());
		}
	}
}

