using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloXamarinFormsWorld.LayoutExamples
{
    public class AbsoluteLayoutExample : ContentPage
    {
        public AbsoluteLayoutExample()
        {
            var red = new Label
            {
                Text = "Red",
                BackgroundColor = Color.Red,
                Font = Font.SystemFontOfSize(20),
                WidthRequest = 100
            };

            var yellow = new Label
            {
                Text = "Yellow",
                BackgroundColor = Color.Yellow,
                Font = Font.SystemFontOfSize(20),
                WidthRequest = 100
            };

            var green = new Label
            {
                Text = "Green",
                BackgroundColor = Color.Green,
                Font = Font.SystemFontOfSize(20),
                WidthRequest = 300
            };

            var absLayout = new AbsoluteLayout();
            absLayout.Children.Add(red, new Point(0, 0));
            absLayout.Children.Add(yellow, new Point(100, 0));
            absLayout.Children.Add(green, new Point(200, 0));

            Content = absLayout;
        }
    }
}
