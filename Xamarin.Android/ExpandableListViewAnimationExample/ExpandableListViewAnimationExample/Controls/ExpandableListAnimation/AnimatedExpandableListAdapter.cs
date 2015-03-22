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
using Android.Util;
using Android.Views.Animations;

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    public abstract class AnimatedExpandableListAdapter : BaseExpandableListAdapter
    {
        private SparseArray<GroupInfo> _groupInfo = new SparseArray<GroupInfo>();
        private AnimatedExpandableListView _parent;

        public static readonly int STATE_IDLE = 0;
        public static readonly int STATE_EXPANDING = 1;
        public static readonly int STATE_COLLAPSING = 2;

        public AnimatedExpandableListView Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }


        public int GetRealChildType(int groupPosition, int childPosition)
        {
            return 0;
        }

        public int GetRealChildTypeCount()
        {
            return 1;
        }

        public abstract View GetRealChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent);
        public abstract int GetRealChildrenCount(int groupPosition);

        private GroupInfo GetGroupInfo(int groupPosition)
        {

            GroupInfo f = new GroupInfo();
            GroupInfo info = _groupInfo.Get(groupPosition);
            if (info == null)
            {
                info = new GroupInfo();
                _groupInfo.Put(groupPosition, info);
            }
            return info;
        }

        public void NotifyGroupExpanded(int groupPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            info.DummyHeight = -1;
        }

        public void StartExpandAnimation(int groupPosition, int firstChildPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            info.Animating = true;
            info.FirstChildPosition = firstChildPosition;
            info.Expanding = true;
        }

        public void StartCollapseAnimation(int groupPosition, int firstChildPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            info.Animating = true;
            info.FirstChildPosition = firstChildPosition;
            info.Expanding = false;
        }

        public void StopAnimation(int groupPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            info.Animating = false;
        }

        /**
         * Override {@link #getRealChildType(int, int)} instead.
         */
        public override int GetChildType(int groupPosition, int childPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            if (info.Animating)
            {
                // If we are animating this group, then all of it's children
                // are going to be dummy views which we will say is type 0.
                return 0;
            }
            else
            {
                // If we are not animating this group, then we will add 1 to
                // the type it has so that no type id conflicts will occur
                // unless getRealChildType() returns MAX_INT
                return GetRealChildType(groupPosition, childPosition) + 1;
            }
        }

        /**
         * Override {@link #getRealChildTypeCount()} instead.
         */
        public override int ChildTypeCount
        {
            get
            {
                return GetRealChildTypeCount() + 1;
            }
        }

        protected ViewGroup.LayoutParams GenerateDefaultLayoutParams()
        {
            return new AbsListView.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent,
                0);
        }

        /**
         * Override {@link #getChildView(int, int, boolean, View, ViewGroup)} instead.
         */
        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            GroupInfo info = GetGroupInfo(groupPosition);

            if (info.Animating)
            {
                // If this group is animating, return the a DummyView...
                if (convertView is DummyView == false)
                {
                    convertView = new DummyView(parent.Context);
                    convertView.LayoutParameters = new AbsListView.LayoutParams(AbsListView.LayoutParams.MatchParent, 0);
                }

                if (childPosition < info.FirstChildPosition)
                {
                    // The reason why we do this is to support the collapse
                    // this group when the group view is not visible but the
                    // children of this group are. When notifyDataSetChanged
                    // is called, the ExpandableListView tries to keep the
                    // list position the same by saving the first visible item
                    // and jumping back to that item after the views have been
                    // refreshed. Now the problem is, if a group has 2 items
                    // and the first visible item is the 2nd child of the group
                    // and this group is collapsed, then the dummy view will be
                    // used for the group. But now the group only has 1 item
                    // which is the dummy view, thus when the ListView is trying
                    // to restore the scroll position, it will try to jump to
                    // the second item of the group. But this group no longer
                    // has a second item, so it is forced to jump to the next
                    // group. This will cause a very ugly visual glitch. So
                    // the way that we counteract this is by creating as many
                    // dummy views as we need to maintain the scroll position
                    // of the ListView after notifyDataSetChanged has been
                    // called.
                    convertView.LayoutParameters.Height = 0;
                    return convertView;
                }

                ExpandableListView listview = (ExpandableListView)parent;

                DummyView dummyView = (DummyView)convertView;

                // Clear the views that the dummy view draws
                dummyView.ClearViews();

                // Set the style of the divider
                dummyView.SetDivider(listview.Divider, parent.MeasuredWidth, listview.DividerHeight);

                // Make measure specs to measure child views
                int measureSpecW = View.MeasureSpec.MakeMeasureSpec(parent.Width, MeasureSpecMode.Exactly);
                int measureSpecH = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);

                int totalHeight = 0;
                int clipHeight = parent.Height;

                int len = GetRealChildrenCount(groupPosition);
                for (int i = info.FirstChildPosition; i < len; i++)
                {
                    View childView = GetRealChildView(groupPosition, i, (i == len - 1), null, parent);

                    AbsListView.LayoutParams p = (AbsListView.LayoutParams)childView.LayoutParameters;
                    if (p == null)
                    {
                        p = (AbsListView.LayoutParams)GenerateDefaultLayoutParams();
                        childView.LayoutParameters = p;
                    }

                    int lpHeight = p.Height;

                    int childHeightSpec;
                    if (lpHeight > 0)
                    {
                        childHeightSpec = View.MeasureSpec.MakeMeasureSpec(lpHeight, MeasureSpecMode.Exactly);
                    }
                    else
                    {
                        childHeightSpec = measureSpecH;
                    }

                    childView.Measure(measureSpecW, childHeightSpec);
                    totalHeight += childView.MeasuredHeight;

                    if (totalHeight < clipHeight)
                    {
                        // we only need to draw enough views to fool the user...
                        dummyView.AddFakeView(childView);
                    }
                    else
                    {
                        dummyView.AddFakeView(childView);

                        // if this group has too many views, we don't want to
                        // calculate the height of everything... just do a light
                        // approximation and break
                        int averageHeight = totalHeight / (i + 1);
                        totalHeight += (len - i - 1) * averageHeight;
                        break;
                    }
                }

                Java.Lang.Object o;
                int state = (o = dummyView.Tag) == null ? STATE_IDLE : (Int16)o;

                if (info.Expanding && state != STATE_EXPANDING)
                {
                    ExpandAnimation ani = new ExpandAnimation(dummyView, 0, totalHeight, info);
                    ani.Duration = _parent.GetAnimationDuration();
                    ani.SetAnimationListener(new ExpandAnimationListener(this, groupPosition, dummyView)); 
                    dummyView.StartAnimation(ani);
                    dummyView.Tag = STATE_EXPANDING;
                }
                else if (!info.Expanding && state != STATE_COLLAPSING)
                {
                    if (info.DummyHeight == -1)
                    {
                        info.DummyHeight = totalHeight;
                    }

                    ExpandAnimation ani = new ExpandAnimation(dummyView, info.DummyHeight, 0, info);
                    ani.Duration = _parent.GetAnimationDuration();
                    ani.SetAnimationListener(new CollapseAnimationListener(this, groupPosition, listview, info, dummyView)); 

                    dummyView.StartAnimation(ani);
                    dummyView.Tag = STATE_COLLAPSING;
                }

                return convertView;
            }
            else
            {
                return GetRealChildView(groupPosition, childPosition, isLastChild, convertView, parent);
            }
        }

        public override int GetChildrenCount(int groupPosition)
        {
            GroupInfo info = GetGroupInfo(groupPosition);
            if (info.Animating)
            {
                return info.FirstChildPosition + 1;
            }
            else
            {
                return GetRealChildrenCount(groupPosition);
            }
        }

    }

    class CollapseAnimationListener : Java.Lang.Object, Animation.IAnimationListener
    {
        private AnimatedExpandableListAdapter _adapter;
        private int _groupPosition;
        private DummyView _dummyView;
        private ExpandableListView _listView;
        private GroupInfo _info;

        public CollapseAnimationListener(AnimatedExpandableListAdapter adapter, int groupPosition, ExpandableListView listView, GroupInfo info, DummyView dummyView)
        {
            _adapter = adapter;
            _groupPosition = groupPosition;
            _listView = listView;
            _info = info;
            _dummyView = dummyView;
        }

        public void OnAnimationEnd(Animation animation)
        {
            _adapter.StopAnimation(_groupPosition);
            _listView.CollapseGroup(_groupPosition);
            _adapter.NotifyDataSetChanged();
            _info.DummyHeight = -1;
            _dummyView.Tag = AnimatedExpandableListAdapter.STATE_IDLE;
        }

        public void OnAnimationRepeat(Animation animation)
        {

        }

        public void OnAnimationStart(Animation animation)
        {

        }
    }

    class ExpandAnimationListener : Java.Lang.Object, Animation.IAnimationListener
    {
        private AnimatedExpandableListAdapter _adapter;
        private int _groupPosition;
        private DummyView _dummyView;

        public ExpandAnimationListener(AnimatedExpandableListAdapter adapter, int groupPosition, DummyView dummyView)
        {
            _adapter = adapter;
            _groupPosition = groupPosition;
            _dummyView = dummyView;
        }

        public void OnAnimationEnd(Animation animation)
        {
            _adapter.StopAnimation(_groupPosition);
            _adapter.NotifyDataSetChanged();
            _dummyView.Tag = AnimatedExpandableListAdapter.STATE_IDLE;
        }

        public void OnAnimationRepeat(Animation animation)
        {

        }

        public void OnAnimationStart(Animation animation)
        {

        }
    }
}