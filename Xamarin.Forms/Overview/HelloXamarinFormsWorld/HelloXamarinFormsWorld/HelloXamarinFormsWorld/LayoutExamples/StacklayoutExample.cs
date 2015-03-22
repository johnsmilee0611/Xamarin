using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace HelloXamarinFormsWorld.LayoutExamples
{
    public class StacklayoutExample : ContentPage
    {
        public StacklayoutExample()
        {
            Padding = new Thickness(20);
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
           
            Content = new StackLayout
            {
                Spacing = 10,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
                Children = { green, red, yellow }
            };
        }
    }
}
