using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshWithSQLite.Models
{
    public class Report
    {
        public object ReportDate;
        public ObservableCollection<DataPoint> ChannelStats { get; set; }
    }
}
