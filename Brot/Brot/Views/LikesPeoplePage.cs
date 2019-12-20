sing System;

using Xamarin.Forms;

namespace Brot.Views
{
    public class LikesPeoplePage : ContentPage
    {
        public LikesPeoplePage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

