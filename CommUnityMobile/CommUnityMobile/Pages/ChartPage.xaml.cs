using FreshMvvm;
using FreshWithSQLite.Models;
using FreshWithSQLite.PageModels;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FreshWithSQLite.Pages
{
    public partial class ChartPage : FreshBaseContentPage
    {
        public ObservableCollection<int> s { get; set; } 

        public ChartPage()
        {
            InitializeComponent();
            s = new ObservableCollection<int>();
            s.Add(1);
            s.Add(200);
            series.ItemsSource = s;
        }

        protected override void OnAppearing()
        {

            //series = ;
            if (Chart.Series.Count <=1)
            {
                var report = (ChartPageModel)this.BindingContext;
                //report.PropertyChanged += SignInViewModel_PropertyChanged;
                //var series = await
                int i = 0;
                foreach (DataPoint data in report.Series.ChannelStats)
                {
                    i = 0;
                    ObservableCollection<ChartDataPoint> points = new ObservableCollection<ChartDataPoint>();
                    foreach (int number in data.NumberOfPosts)
                    {
                        points.Add(new ChartDataPoint(i + 1, number));
                        i++;
                    }
                    Chart.Series.Add(new LineSeries
                    {
                        ItemsSource = points,
                        Label = data.ChannelName,
                        EnableDataPointSelection = true
                    });

                }
            }
            var basePageModel = this.BindingContext as FreshMvvm.FreshBasePageModel;
            if (basePageModel != null)
            {
                if (basePageModel.IsModalAndHasPreviousNavigationStack())
                {
                    if (ToolbarItems.Count < 2)
                    {
                        var closeModal = new ToolbarItem("Close Modal", "", () =>
                        {
                            basePageModel.CoreMethods.PopModalNavigationService();
                        });

                        ToolbarItems.Add(closeModal);
                    }
                }
            }

            base.OnAppearing();

        }
        void SignInViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var vm = sender as ChartPageModel;

            if (vm.Series != null && Chart.Series.Count < vm.Series.ChannelStats.Count)
            {
                foreach (DataPoint data in vm.Series.ChannelStats)
                {
                    Chart.Series.Add(new LineSeries
                    {
                        ItemsSource = data.NumberOfPosts,
                        Label = data.ChannelName
                    });
                }
            }


        }
    }
}
