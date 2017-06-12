using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FreshMvvm;
using FreshWithSQLite.Pages;
using FreshWithSQLite.Models;
using System.ComponentModel;
using FreshWithSQLite.PageModels;
using static FreshWithSQLite.App;

namespace FreshWithSQLite.Navigation
{
    public class CustomMasterDetail :  Xamarin.Forms.MasterDetailPage, IFreshNavigationService
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        List<Page> _pagesInner = new List<Page>();
        Dictionary<string, Page> _pages = new Dictionary<string, Page>();
        ContentPage _menuPage;

        MasterMenuModel logout { get; set; }
        List<MasterMenuModel> _pageItems = new List<MasterMenuModel>();
        ObservableCollection<string> _pageNames = new ObservableCollection<string>();
        public Dictionary<string, Page> Pages { get { return _pages; } }
        protected ObservableCollection<string> PageNames { get { return _pageNames; } }

        public CustomMasterDetail() : this(Constants.DefaultNavigationServiceName)
        {
        }

        public CustomMasterDetail(string navigationServiceName)
        {
            NavigationServiceName = navigationServiceName;
            logout = new MasterMenuModel("Log out", "", null);
            _pageItems.Add(logout);
            RegisterNavigation();
        }

        public void Init(string menuTitle, string menuIcon = null)
        {
            CreateMenuPage(menuTitle, menuIcon);
            RegisterNavigation();
        }

        protected virtual void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public virtual void AddPage<T>(string title, string subtitle, Image source,object data = null) where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            _pagesInner.Add(page);
            var navigationContainer = CreateContainerPage(page);
            _pages.Add(title, navigationContainer);
            _pageNames.Add(title);
            var item = new MasterMenuModel(title, subtitle, source);
            _pageItems.Add(item);
            if (_pages.Count == 1)
                Detail = navigationContainer;
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
            return new NavigationPage(page);
        }

        public void Switch(string name)
        {
            Detail = _pages[name];
        }

        protected virtual void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            _menuPage = new ContentPage();
            var _listView = new ImageCellPage();
            _menuPage.Title = menuPageTitle;
            var listView = new ListView();
            _menuPage.BackgroundColor = Color.FromHex("#222");

            _listView.ItemsSource = _pageItems;

            _listView.ItemSelected += (sender, args) => {
                if (args.SelectedItem != null)
                {
                    if (((MasterMenuModel)args.SelectedItem) == logout)
                    {

                        App s = App.Current as App;
                        App.Current.MainPage = s.loginStack;

                    }
                    else if (_pages.ContainsKey(((MasterMenuModel)args.SelectedItem).ListViewText))
                    {
                        Detail = _pages[((MasterMenuModel)args.SelectedItem).ListViewText];
                    }
                }
                _listView.SelectedItem = null;
                IsPresented = false;
            };

            _menuPage.Content = _listView;
            _menuPage.Content.BackgroundColor = Color.FromHex("#222");
            
            var navPage = new NavigationPage(_menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;

            Master = navPage;
        }

        public Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PushModalAsync(CreateContainerPageSafe(page));
            return (Detail as NavigationPage).PushAsync(page, animate); //TODO: make this better
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PopModalAsync(animate);
            return (Detail as NavigationPage).PopAsync(animate); //TODO: make this better            
        }

        public Task PopToRoot(bool animate = true)
        {
            return (Detail as NavigationPage).PopToRootAsync(animate);
        }

        public string NavigationServiceName { get; private set; }

        public void NotifyChildrenPageWasPopped()
        {
            if (Master is NavigationPage)
                ((NavigationPage)Master).NotifyAllChildrenPopped();
            foreach (var page in this.Pages.Values)
            {
                if (page is NavigationPage)
                    ((NavigationPage)page).NotifyAllChildrenPopped();
            }
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            var tabIndex = _pagesInner.FindIndex(o => o.GetModel().GetType().FullName == typeof(T).FullName);

            Detail = _pages.Values.ElementAt(tabIndex); ;

            return Task.FromResult((Detail as NavigationPage).CurrentPage.GetModel());
        }
    }

}
