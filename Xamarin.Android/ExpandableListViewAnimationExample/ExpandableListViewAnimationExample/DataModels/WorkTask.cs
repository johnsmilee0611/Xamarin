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

namespace ExpandableListViewAnimationExample.DataModels
{
    public class WorkTask
    {
        public int WorkTaskID { get; set; }

        public int ProjectID { get; set; }

        public DateTime WorkingDate { get; set; }

        public string Description { get; set; }

        public float Duration { get; set; }
    }
}