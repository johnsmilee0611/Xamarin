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
using Android.Graphics.Drawables;
using Android.Graphics;

namespace ExpandableListViewAnimationExample.Controls.ExpandableListAnimation
{
    public class DummyView : View
    {
        private List<View> _views = new List<View>();
        private Drawable _divider;
        private int _dividerWidth;
        private int _dividerHeight;

        public DummyView(Context context)
            : base(context)
        {

        }

        public void SetDivider(Drawable divider, int dividerWidth, int dividerHeigth)
        {
            if (divider != null)
            {
                _divider = divider;
                _dividerWidth = dividerWidth;
                _dividerHeight = dividerHeigth;

                divider.SetBounds(0, 0, dividerWidth, dividerHeigth);
            }
        }

        /**
         * Add a view for the DummyView to draw.
         * @param childView View to draw
         */
        public void AddFakeView(View childView)
        {
            childView.Layout(0, 0, Width, childView.MeasuredHeight);
            _views.Add(childView);
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);

            int len = _views.Count;

            for (int i = 0; i < len; i++)
            {
                View v = _views[i];
                v.Layout(left, top, left + v.MeasuredWidth, top + v.MeasuredHeight);
            }
        }

        public void ClearViews()
        {
            _views.Clear();
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            canvas.Save();
            if (_divider != null)
            {
                _divider.SetBounds(0, 0, _dividerWidth, _dividerHeight);
            }

            int len = _views.Count;
            for (int i = 0; i < len; i++)
            {
                View v = _views[i];
                canvas.Save();
                canvas.ClipRect(0, 0, Width, v.MeasuredHeight);
                v.Draw(canvas);
                canvas.Restore();

                if (_divider != null)
                {
                    _divider.Draw(canvas);
                    canvas.Translate(0, _dividerHeight);
                }

                canvas.Translate(0, v.MeasuredHeight);
            }

            canvas.Restore();
        }

    }
}