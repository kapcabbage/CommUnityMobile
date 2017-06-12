using FreshMvvm;
using FreshWithSQLite.Models;
using FreshWithSQLite.Navigation;
using FreshWithSQLite.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FreshWithSQLite.PageModels
{
    public class ChartPageModel : FreshBasePageModel
    {
        private IDialogService _dialogService;
        
        public ICommand AddCommand { get; private set; }
        private Report _series { get; set; }
        private int _max { get; set; }
        private int _interval { get; set; }


        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
                RaisePropertyChanged();
            }
        }
        public int MaxNumber
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                RaisePropertyChanged();
            }

        }

        public Report Series
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value;
                RaisePropertyChanged();
            }
        }
        public override async void Init(object initData)
        {
           
            try
            {
                ChartDataService service = new ChartDataService();
                Series = await service.GetCurrentData();
                var tmp = 0;
                foreach (DataPoint stat in Series.ChannelStats)
                {
                    var max = stat.NumberOfPosts.Max();
                    if (tmp < max)
                    {
                        tmp = max;
                    }
                }
                MaxNumber = tmp;
                Interval = tmp / 10;
                base.Init(initData);

                _dialogService = new DialogService();

                AddCommand = new Command(() =>
                {
                    //_dialogService.ShowMessage("Hello from FAB button", "ViewModel");
                    var app = Application.Current as App;
                    //we set the MasterDetailPage to the MainPage of App(this MainPage is one property of App)
                    //get the MainPage and cast it to MainPage(this is the page I defined)

                    //set the detail of this page with new page!
                    var master = app.MainPage as CustomMasterDetail;
                    master.Switch("New post");
                });
            }
            catch (Exception ex)
            {
                 await CoreMethods.PushPageModel<ModalPageModel>(true, true);
            }
        }
       
    }
}
