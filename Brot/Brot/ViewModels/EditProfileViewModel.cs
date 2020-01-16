using Brot.Models.ResponseApi;
using Brot.Patterns;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Brot.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        ResponseUserProfile profiledata = new ResponseUserProfile();
        private ResponseUserProfile _Usuario;
        public ResponseUserProfile UserProfile
        {
            get { return this._Usuario; }
            set => SetProperty(ref _Usuario, value);
        }

        private bool _isvendor;
        public bool IsVendor
        {
            get { return this._isvendor; }
            set => SetProperty(ref _isvendor, value);
        }
        private string imgName;


        public EditProfileViewModel()
        {
            CargarDatos();
            
        }

        private async void CargarDatos()
        {
            profiledata = await RestAPI.userprofile(Singleton.Instance.User.id_user);

            if (profiledata != null)
            {
                imgName = profiledata.UserProfile.img;
                if (profiledata.UserProfile.img != null)
                {
                    profiledata.UserProfile.img = DLL.constantes.urlImages + profiledata.UserProfile.img;
                }
                else
                {
                    profiledata.UserProfile.img = DLL.constantes.ProfileImageError;
                }
                UserProfile = profiledata;

                IsVendor = UserProfile.UserProfile.isVendor;
            }

        }


        private Command _UpdateProfileCommand;
        public Command UpdateProfileCommand { get => _UpdateProfileCommand ?? (_UpdateProfileCommand = new Command(UpdateProfileMethod)); }

        private async void UpdateProfileMethod(object obj)
        {
            IsRefreshing = true;

            bool result = await RestAPI.Put<Models.userModel>(UserProfile.UserProfile.id_user, UserProfile.UserProfile, DLL.constantes.userst);

            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Datos Actualizados", "", "Ok");
                Singleton.Instance.User = UserProfile.UserProfile;
                Singleton.Instance.LocalJson.SaveData(Singleton.Instance.User);
                App.Current.MainPage = new NavigationPage(new Views.MainTabbed());

            }

            IsRefreshing = false;

        }
    }
}
