using FreshMvvm;
using FreshWithSQLite.Core;
using FreshWithSQLite.Navigation;
using FreshWithSQLite.PageModels;
using FreshWithSQLite.Pages;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FreshWithSQLite
{
	public partial class App : Application
	{
        public bool IsLoggedIn { get; set; }

        Page welcomePage { get; set; }
        public FreshNavigationContainer loginStack { get; set; }
        public class NavigationStacks
        {
            public static string LoginNavigationStack = "LoginNavigationStack";
            public static string MainAppStack = "MainAppStack";
        }

        public App()
		{
			InitializeComponent();
            try
            {

                welcomePage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                loginStack = new FreshNavigationContainer(welcomePage, NavigationStacks.LoginNavigationStack);
                //var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                ////var vm = (LoginPageModel)page.BindingContext;
                ////vm.PropertyChanged += SignInViewModel_PropertyChanged;
                //var navContainer = new FreshNavigationContainer(page);

                //LoadCustomNav();
                LoadMasterDetail();
                MainPage = loginStack;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
		}
     
        public void LoadBasicNav()
        {
            //var page = FreshPageModelResolver.ResolvePageModel<MainMenuPageModel>();
            //var basicNavContainer = new FreshNavigationContainer(page);
            //MainPage = basicNavContainer;
        }

        public void LoadMasterDetail()
        {
            var masterDetailNav = new CustomMasterDetail(NavigationStacks.MainAppStack);
            masterDetailNav.Init("Menu", "");
           // masterDetailNav.AddPage<LoginPageModel>("Contacts", null);
            masterDetailNav.AddPage<CreateMessagePageModel>("New post", "write your post",null,null);
            masterDetailNav.AddPage<ChartPageModel>("Statistics", "write your post", null, null);
            //masterDetailNav.AddPage<LoginPageModel>("Logout", "write your post", null, null);
        }


        public void LoadTabbedNav()
        {
            //var tabbedNavigation = new FreshTabbedNavigationContainer();
            //tabbedNavigation.AddTab<ContactListPageModel>("Contacts", "contacts.png", null);
            //tabbedNavigation.AddTab<QuoteListPageModel>("Quotes", "document.png", null);
            //MainPage = tabbedNavigation;
        }

        public void LoadFOTabbedNav()
        {
            //var tabbedNavigation = new FreshTabbedFONavigationContainer("CRM");
            //tabbedNavigation.AddTab<ContactListPageModel>("Contacts", "contacts.png", null);
            //tabbedNavigation.AddTab<QuoteListPageModel>("Quotes", "document.png", null);
            //MainPage = tabbedNavigation;
        }

        public void LoadCustomNav()
        {
            MainPage = new CustomNavigation();
        }

        public void LoadMultipleNavigation()
        {
            //var masterDetailsMultiple = new MasterDetailPage(); //generic master detail page

            ////we setup the first navigation container with ContactList
            //var contactListPage = FreshPageModelResolver.ResolvePageModel<ContactListPageModel>();
            //contactListPage.Title = "Contact List";
            ////we setup the first navigation container with name MasterPageArea
            //var masterPageArea = new FreshNavigationContainer(contactListPage, "MasterPageArea");
            //masterPageArea.Title = "Menu";

            //masterDetailsMultiple.Master = masterPageArea; //set the first navigation container to the Master

            ////we setup the second navigation container with the QuoteList 
            //var quoteListPage = FreshPageModelResolver.ResolvePageModel<QuoteListPageModel>();
            //quoteListPage.Title = "Quote List";
            ////we setup the second navigation container with name DetailPageArea
            //var detailPageArea = new FreshNavigationContainer(quoteListPage, "DetailPageArea");

            //masterDetailsMultiple.Detail = detailPageArea; //set the second navigation container to the Detail

            //MainPage = masterDetailsMultiple;
        }


        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
