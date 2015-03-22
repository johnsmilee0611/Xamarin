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
using Android.Views.Animations;

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    public class ExpandAnimation : Animation
    {
        private int _baseHeight;
        private int _delta;
        private View _view;
        private GroupInfo _groupInfo;

        public ExpandAnimation(View v, int startHeight, int endHeight, GroupInfo info)
        {
            _baseHeight = startHeight;
            _delta = endHeight - startHeight;
            _view = v;
            _groupInfo = info;

            _view.LayoutParameters.Height = startHeight;
            _view.RequestLayout();
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            base.ApplyTransformation(interpolatedTime, t);
            if (interpolatedTime < 1.0f)
            {
                int val = _baseHeight + (int)(_delta * interpolatedTime);
                _view.LayoutParameters.Height = val;
                _groupInfo.DummyHeight = val;
                _view.RequestLayout();
            }
            else
            {
                int val = _baseHeight + _delta;
                _view.LayoutParameters.Height = val;
                _groupInfo.DummyHeight = val;
                _view.RequestLayout();
            }
        }
    }
}