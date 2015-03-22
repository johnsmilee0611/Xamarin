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
using ExpandableListViewAnimationExample.DataModels;

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    public class ChildItem : Java.Lang.Object
    {
        public WorkTask Task { get; set; }
    }
}