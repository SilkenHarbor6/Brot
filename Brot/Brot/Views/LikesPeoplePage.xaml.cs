using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Brot.Views
{
    public partial class LikesPeoplePage : ContentPage
    {
        public LikesPeoplePage(int id, ViewModels.likeType tipolike)
        {
            InitializeComponent();
            BindingContext = new ViewModels.LikesPeoplePageViewModel(id, tipolike);
        }
    }
}
