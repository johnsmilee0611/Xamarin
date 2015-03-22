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

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    /**
     * This class defines an ExpandableListView which supports animations for
     * collapsing and expanding groups.
     */
    public class AnimatedExpandableListView : ExpandableListView
    {
        /*
         * A detailed explanation for how this class works:
         *
         * Animating the ExpandableListView was no easy task. The way that this
         * class does it is by exploiting how an ExpandableListView works.
         *
         * Normally when {@link ExpandableListView#collapseGroup(int)} or
         * {@link ExpandableListView#expandGroup(int)} is called, the view toggles
         * the flag for a group and calls notifyDataSetChanged to cause the ListView
         * to refresh all of it's view. This time however, depending on whether a
         * group is expanded or collapsed, certain childViews will either be ignored
         * or added to the list.
         *
         * Knowing this, we can come up with a way to animate our views. For
         * instance for group expansion, we tell the adapter to animate the
         * children of a certain group. We then expand the group which causes the
         * ExpandableListView to refresh all views on screen. The way that
         * ExpandableListView does this is by calling getView() in the adapter.
         * However since the adapter knows that we are animating a certain group,
         * instead of returning the real views for the children of the group being
         * animated, it will return a fake dummy view. This dummy view will then
         * draw the real child views within it's dispatchDraw function. The reason
         * we do this is so that we can animate all of it's children by simply
         * animating the dummy view. After we complete the animation, we tell the
         * adapter to stop animating the group and call notifyDataSetChanged. Now
         * the ExpandableListView is forced to refresh it's views again, except this
         * time, it will get the real views for the expanded group.
         *
         * So, to list it all out, when {@link #expandGroupWithAnimation(int)} is
         * called the following happens:
         *
         * 1. The ExpandableListView tells the adapter to animate a certain group.
         * 2. The ExpandableListView calls expandGroup.
         * 3. ExpandGroup calls notifyDataSetChanged.
         * 4. As an result, getChildView is called for expanding group.
         * 5. Since the adapter is in "animating mode", it will return a dummy view.
         * 6. This dummy view draws the actual children of the expanding group.
         * 7. This dummy view's height is animated from 0 to it's expanded height.
         * 8. Once the animation completes, the adapter is notified to stop
         *    animating the group and notifyDataSetChanged is called again.
         * 9. This forces the ExpandableListView to refresh all of it's views again.
         * 10.This time when getChildView is called, it will return the actual
         *    child views.
         *
         * For animating the collapse of a group is a bit more difficult since we
         * can't call collapseGroup from the start as it would just ignore the
         * child items, giving up no chance to do any sort of animation. Instead
         * what we have to do is play the animation first and call collapseGroup
         * after the animation is done.
         *
         * So, to list it all out, when {@link #collapseGroupWithAnimation(int)} is
         * called the following happens:
         *
         * 1. The ExpandableListView tells the adapter to animate a certain group.
         * 2. The ExpandableListView calls notifyDataSetChanged.
         * 3. As an result, getChildView is called for expanding group.
         * 4. Since the adapter is in "animating mode", it will return a dummy view.
         * 5. This dummy view draws the actual children of the expanding group.
         * 6. This dummy view's height is animated from it's current height to 0.
         * 7. Once the animation completes, the adapter is notified to stop
         *    animating the group and notifyDataSetChanged is called again.
         * 8. collapseGroup is finally called.
         * 9. This forces the ExpandableListView to refresh all of it's views again.
         * 10.This time when the ListView will not get any of the child views for
         *    the collapsed group.
         */


        /**
         * The duration of the expand/collapse animations
         */

        private static readonly int ANIMATION_DURATION = 100;

        private AnimatedExpandableListAdapter _adapter;

        public AnimatedExpandableListView(Context context)
            : base(context)
        {

        }

        public AnimatedExpandableListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }

        public AnimatedExpandableListView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {

        }

        /**
         * @see ExpandableListView#setAdapter(ExpandableListAdapter)
         */
        public void SetAdapter(BaseExpandableListAdapter adapter)
        {
            base.SetAdapter(adapter);
            // Make sure that the adapter extends AnimatedExpandableListAdapter
            if (adapter is AnimatedExpandableListAdapter)
            {
                _adapter = (AnimatedExpandableListAdapter)adapter;
                _adapter.Parent = this;
            }
            else
            {
                throw new InvalidCastException(adapter.ToString() + " must implement AnimatedExpandableListAdapter");
            }
        }

        /**
         * Expands the given group with an animation.
         * @param groupPosision The position of the group to expand
         * @return  Returns true if the group was expanded. False if the group was
         *          already expanded.
         */
        public bool ExpandGroupWithAnimation(int groupPosition)
        {
            bool lastGroup = groupPosition == _adapter.GroupCount - 1;

            if (lastGroup && Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.IceCreamSandwich)
            {
                return ExpandGroup(groupPosition, true);
            }

            int groupFlatPosition = GetFlatListPosition(GetPackedPositionForGroup(groupPosition));
            if (groupFlatPosition != -1)
            {
                int childIndex = groupFlatPosition - FirstVisiblePosition;
                if (childIndex < ChildCount)
                {
                    // Get the view for the group is it is on screen...
                    View v = GetChildAt(childIndex);
                    if (v.Bottom >= Bottom)
                    {
                        // If the user is not going to be able to see the animation
                        // we just expand the group without an animation.
                        // This resolves the case where getChildView will not be
                        // called if the children of the group is not on screen

                        // We need to notify the adapter that the group was expanded
                        // without it's knowledge
                        _adapter.NotifyGroupExpanded(groupPosition);
                        return ExpandGroup(groupPosition);
                    }
                }
            }

            // Let the adapter know that we are starting the animation...
            _adapter.StartExpandAnimation(groupPosition, 0);
            // Finally call expandGroup (note that expandGroup will call
            // notifyDataSetChanged so we don't need to)
            return ExpandGroup(groupPosition);
        }

        /**
         * Collapses the given group with an animation.
         * @param groupPos The position of the group to collapse
         * @return  Returns true if the group was collapsed. False if the group was
         *          already collapsed.
         */
        public bool CollapseGroupWithAnimation(int groupPosition)
        {
            int groupFlatPos = GetFlatListPosition(GetPackedPositionForGroup(groupPosition));
            if (groupFlatPos != -1)
            {
                int childIndex = groupFlatPos - FirstVisiblePosition;
                if (childIndex >= 0 && childIndex < ChildCount)
                {
                    // Get the view for the group is it is on screen...
                    View v = GetChildAt(childIndex);
                    if (v.Bottom >= Bottom)
                    {
                        // If the user is not going to be able to see the animation
                        // we just collapse the group without an animation.
                        // This resolves the case where getChildView will not be
                        // called if the children of the group is not on screen
                        return CollapseGroup(groupPosition);
                    }
                }
                else
                {
                    // If the group is offscreen, we can just collapse it without an
                    // animation...
                    return CollapseGroup(groupPosition);
                }
            }

            // Get the position of the firstChild visible from the top of the screen
            long packedPos = GetExpandableListPosition(FirstVisiblePosition);
            int firstChildPosition = GetPackedPositionChild(packedPos);
            int firstGroupPosition = GetPackedPositionGroup(packedPos);

            // If the first visible view on the screen is a child view AND it's a
            // child of the group we are trying to collapse, then set that
            // as the first child position of the group... see
            // {@link #startCollapseAnimation(int, int)} for why this is necessary
            firstChildPosition = firstChildPosition == -1 || firstGroupPosition != groupPosition ? 0 : firstChildPosition;

            // Let the adapter know that we are going to start animating the
            // collapse animation.
            _adapter.StartCollapseAnimation(groupPosition, firstChildPosition);

            // Force the listview to refresh it's views
            _adapter.NotifyDataSetChanged();
            return IsGroupExpanded(groupPosition);

        }

        public int GetAnimationDuration()
        {
            return ANIMATION_DURATION;
        }
    }
}