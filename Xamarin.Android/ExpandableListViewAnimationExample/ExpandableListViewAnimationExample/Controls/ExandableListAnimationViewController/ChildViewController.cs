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
using ExpandableListViewAnimationExample.DataModels;

namespace ExpandableListViewAnimationExample.Controls.ExandableListAnimationViewController
{
    public class ChildViewController : RelativeLayout
    {
        private Context _context;
        private LayoutInflater _inflater;
        private View _rootView, _colorBar;
        private TextView _txtDescription, _txtProjectName, _txtDuration;

        public ChildViewController(Context context)
            : base(context)
        {
            InitView(context);

            InitControls();
        }

        public ChildViewController(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            InitView(context);
        }

        public void LoadData(WorkTask taskData)
        {
            _txtDuration.Text = System.Math.Round(Convert.ToDouble(taskData.Duration), 2).ToString() + "h";
            _txtDescription.Text = taskData.Description;

            Projects projects = new Projects();
            _txtProjectName.Text = projects[taskData.ProjectID].ProjectName;
            _colorBar.SetBackgroundColor(Android.Graphics.Color.ParseColor(projects[taskData.ProjectID].Color));
        }

        private void InitView(Context context)
        {
            _context = context;

            _inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            _rootView = _inflater.Inflate(Resource.Layout.ChildView, this, true);
        }

        private void InitControls()
        {
            if (_rootView != null)
            {
                _txtDescription = _rootView.FindViewById<TextView>(Resource.Id.ChildControl_txtDescription);
                _txtProjectName = _rootView.FindViewById<TextView>(Resource.Id.ChildControl_txtProjects);
                _colorBar = _rootView.FindViewById<View>(Resource.Id.ChildControl_ColorProjectsBar);
                _txtDuration = _rootView.FindViewById<TextView>(Resource.Id.ChildControl_txtHoursSpend);
            }
        }
    }
}