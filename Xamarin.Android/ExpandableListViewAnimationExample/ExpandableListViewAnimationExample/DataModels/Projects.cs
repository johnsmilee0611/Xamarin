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
    public class Projects
    {
        private List<Project> projects;

        public Project this[int index]
        { 
            get
            {
                return projects[index];
            }
        }

        public Projects()
        {
            projects = new List<Project>()
            {
                new Project()
                {
                    ProjectName = "Xamarin.Forms",
                    Color = "#99ff99"
                },
                new Project()
                {
                    ProjectName = "Xamarin.iOS",
                    Color = "#821418"
                },
                new Project()
                {
                    ProjectName = "Xamarin.Android",
                    Color = "#261577"
                },
                new Project()
                {
                    ProjectName = "Web Services",
                    Color = "#c1b9d9"
                },
                new Project()
                {
                    ProjectName = "Core.CSharp",
                    Color = "#2162a6"
                },
                new Project()
                {
                    ProjectName = "SharedProjects",
                    Color = "#2e57ed"
                },
                new Project()
                {
                    ProjectName = "BCL",
                    Color = "#fe011b"
                },
            };
        }
    }

    public class Project
    {
        public string ProjectName { get; set; }
        public string Color { get; set; }
    }
}