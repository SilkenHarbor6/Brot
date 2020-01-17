﻿using Brot.Models;
using Brot.Models.ResponseApi;
using Brot.Patterns;
using Brot.Services;
using DLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public string pic
        {
            get
            {
                return imgName;
            }
            set
            {
                imgName = value;OnPropertyChanged("pic");
            }
        }

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
            pic = profiledata.UserProfile.img;
        }


        private Command _UpdateProfileCommand;
        public Command UpdateProfileCommand { get => _UpdateProfileCommand ?? (_UpdateProfileCommand = new Command(UpdateProfileMethod)); }
        public ICommand changePicture
        {
            get
            {
                return new Xamarin.Forms.Command(cp);
            }
        }
        private async void cp()
        {
            string name = await Singleton.Instance.ChangePic();
            Task.Delay(1000);
            Singleton.Instance.User.img = name;
            pic = constantes.urlImages + name;
            Singleton.Instance.LocalJson.SaveData(Singleton.Instance.User);
            var resp = await RestClient.Put<userModel>("users/photo", Singleton.Instance.User.id_user, Singleton.Instance.User);
            if (!resp)
            {
                await Singleton.Instance.Dialogs.Message("Error", "No se ha podido actualizar la imagen");
                return;
            }

        }
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