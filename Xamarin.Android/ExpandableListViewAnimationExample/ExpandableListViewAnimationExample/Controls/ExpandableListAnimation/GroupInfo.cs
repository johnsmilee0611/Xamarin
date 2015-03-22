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
    /**
     * Used for holding information regarding the group.
     */
    public class GroupInfo
    {
        bool animating = false;
        bool expanding = false;
        int firstChildPosition;

        /**
         * This variable contains the last known height value of the dummy view.
         * We save this information so that if the user collapses a group
         * before it fully expands, the collapse animation will start from the
         * CURRENT height of the dummy view and not from the full expanded
         * height.
         */
        int dummyHeight = -1;

        public bool Animating
        {
            get { return animating; }
            set { animating = value; }
        }

        public bool Expanding
        {
            get { return expanding; }
            set { expanding = value; }
        }

        public int FirstChildPosition
        {
            get { return firstChildPosition; }
            set { firstChildPosition = value; }
        }

        public int DummyHeight
        {
            get { return dummyHeight; }
            set { dummyHeight = value; }
        }
    }
}