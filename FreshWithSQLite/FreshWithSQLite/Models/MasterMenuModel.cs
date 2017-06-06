using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreshWithSQLite.Models
{
    public class MasterMenuModel
    {
        public string ListViewText { get; set; }
        public string ListViewLabel { get; set; }
        public Image ListViewIconSource { get; set; }
        
        public MasterMenuModel(string text, string label, Image source)
        {
            ListViewText = text;
            ListViewLabel = label;
            ListViewIconSource = source;

        }
    }
}
