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

namespace ExpandableListViewAnimationExample.Controls.ExandableListAnimationViewController
{
    public class ExpandableListGroupView : RelativeLayout
    {
        private Context _context;
        private LayoutInflater _inflater;
        private View _rootView;
        private TextView _header;
        private TextView _tvTotalTime;

        public ExpandableListGroupView(Context context)
            : base(context)
        {
            InitView(context);
        }

        public ExpandableListGroupView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            InitView(context);

            InitControls();
        }

        public void LoadData(string strHeader, double totalTime)
        {
            //_header.Text = strHeader;
            //_tvTotalTime.Text = totalTime.ToString() + "h/8h";
        }

        private void InitView(Context context)
        {
            _context = context;
            _inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            _rootView = _inflater.Inflate(Resource.Layout.GroupView, this, true);
        }

        private void InitControls()
        {
            if (_rootView != null)
            {
                _header = _rootView.FindViewById<TextView>(Resource.Id.GroupControl_Header);
                _tvTotalTime = _rootView.FindViewById<TextView>(Resource.Id.txtTotalTime);
            }
        }
    }
}