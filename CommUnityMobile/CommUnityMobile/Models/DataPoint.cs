using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshWithSQLite.Models
{
    public class DataPoint
    {
        public string ChannelName { get; set; }
        public ObservableCollection<int> NumberOfPosts { get; set; }
    }
}
