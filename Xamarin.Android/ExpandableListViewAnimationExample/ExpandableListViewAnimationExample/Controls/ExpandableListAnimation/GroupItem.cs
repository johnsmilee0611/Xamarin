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

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    public class GroupItem : Java.Lang.Object
    {
        private string _Date;
        private List<ChildItem> _items = new List<ChildItem>();

        public string Title
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public List<ChildItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public double TotalTime
        {
            get
            {
                double totalTime = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    totalTime += Items[i].Task.Duration;
                }
                return Math.Round(totalTime, 2);
            }
        }
    }
}