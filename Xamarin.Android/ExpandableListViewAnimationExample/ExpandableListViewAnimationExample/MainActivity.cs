using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using ExpandableListViewAnimationExample.DataModels;
using ExpandableListViewAnimationExample.Controls.ExpandableListAnimation;
using System.Globalization;
using ExpandableListViewAnimationExample.Controls.ExandableListAnimationViewController;

namespace ExpandableListViewAnimationExample
{
    [Activity(Label = "ExpandableListViewAnimationExample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<GroupItem> _records;
        private ExpandableListViewAdapter _adapter;
        private AnimatedExpandableListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _listView = FindViewById<AnimatedExpandableListView>(Resource.Id.expandableListView);
            _listView.GroupClick += ListView_GroupClick;

            _adapter = new ExpandableListViewAdapter(this, new List<GroupItem>());
            _listView.SetAdapter(_adapter);

            _records = new List<GroupItem>();
            _records = GetListGroupItem(DummyData());

            _adapter.DataSource = _records;
            _adapter.NotifyDataSetChanged();

        }

        int SelectedGroup = -1;

        private void ListView_GroupClick(object sender, ExpandableListView.GroupClickEventArgs e)
        {
            if(e.GroupPosition != SelectedGroup)
            {
                _listView.ExpandGroupWithAnimation(e.GroupPosition);
                SelectedGroup = e.GroupPosition;
            }
            else
            {
                _listView.CollapseGroupWithAnimation(e.GroupPosition);
                SelectedGroup = -1;
            }
        }

        private List<GroupItem> GetListGroupItem(List<WorkTask> dataSource)
        {
            List<string> keysByDate = new List<string>();

            foreach(var workTask in dataSource)
            {
                keysByDate.Add(workTask.WorkingDate.ToString("dd/MM/yyyy"));
            }

            // Distinct keysByDate
            keysByDate = new List<string>(keysByDate.Distinct());

            // Short by Date
            List<DateTime> days = new List<DateTime>();

            foreach (string item in keysByDate)
            {
                DateTime datetime = DateTime.ParseExact(item, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                days.Add(datetime);
            }

            days.Sort();

            days.Reverse();

            // Convert Date to String
            keysByDate.Clear();
            foreach (DateTime item in days)
            {
                keysByDate.Add(item.ToString("dd/MM/yyyy"));
            }

            // Fill record to GroupItem
            List<GroupItem> records = new List<GroupItem>();

            foreach (var date in keysByDate)
            {
                GroupItem groupItem = new GroupItem();
                groupItem.Title = date;
                groupItem.Items = new List<ChildItem>();

                foreach(var workTask in dataSource)
                {
                    if (workTask.WorkingDate.ToString("dd/MM/yyyy").Equals(date))
                    {
                        ChildItem childItem = new ChildItem();
                        childItem.Task = workTask;
                        groupItem.Items.Add(childItem);
                    }
                }
                records.Add(groupItem);
            }

            return records;
        }

        private List<WorkTask> DummyData()
        {
            var dataSource = new List<WorkTask>()
            {
                new WorkTask()
                {
                    WorkTaskID = 1,
                    ProjectID = 0,
                    WorkingDate = new DateTime(2015,3,16),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 2,
                    ProjectID = 1,
                    WorkingDate = new DateTime(2015,3,16),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 3,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,16),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 4,
                    ProjectID = 5,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,20),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,17),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,19),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 5,
                    ProjectID = 6,
                    WorkingDate = new DateTime(2015,3,19),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 3,
                    WorkingDate = new DateTime(2015,3,18),
                    Duration = 7.1f,
                    Description = "Description 1"
                },
                new WorkTask()
                {
                    WorkTaskID = 6,
                    ProjectID = 2,
                    WorkingDate = new DateTime(2015,3,18),
                    Duration = 7.1f,
                    Description = "Description 1"
                }

            };

            return dataSource;
        }
    }
}

