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
using ExpandableListViewAnimationExample.Controls.ExpandableListAnimation;

namespace ExpandableListViewAnimationExample.Controls.ExandableListAnimationViewController
{
    public class ExpandableListViewAdapter : AnimatedExpandableListAdapter
    {
        private List<GroupItem> _dataSource = null;

        private Activity _activity;

        public int SelectedGroupPosition { get; set; }

        public int SelectedChildPosition { get; set; }

        public ChildViewController SelectedChildView { get; set; }

        public List<GroupItem> DataSource { get { return _dataSource; } set { _dataSource = value; } }

        public ExpandableListViewAdapter(Activity activity, List<GroupItem> dataSource)
        {
            _dataSource = dataSource;
            _activity = activity;

            SelectedGroupPosition = -1;
            SelectedChildPosition = -1;
            SelectedChildView = null;
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return _dataSource[groupPosition].Items[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            GroupItem item = (GroupItem)GetGroup(groupPosition);
            double totalTime = item.TotalTime;

            if (convertView == null)
            {
                convertView = new ExpandableListGroupView(parent.Context);
            }

            ExpandableListGroupView view = convertView as ExpandableListGroupView;

            view.LoadData(item.Title, totalTime);

            return view;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return _dataSource[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override int GroupCount
        {
            get { return _dataSource.Count; }
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override View GetRealChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            ChildItem item = (ChildItem)GetChild(groupPosition, childPosition);
            if (convertView == null)
            {
                convertView = new ChildViewController(parent.Context);
            }

            ChildViewController view = convertView as ChildViewController;
            view.LoadData(item.Task);

            return view;
        }

        public override int GetRealChildrenCount(int groupPosition)
        {
            return _dataSource[groupPosition].Items.Count;
        }
    }
}