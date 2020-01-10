using Brot.Models.ResponseApi;
using Brot.Patterns;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;

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


        public EditProfileViewModel()
        {
            CargarDatos();
            
        }

        private async void CargarDatos()
        {
            profiledata = await RestAPI.userprofile(Singleton.Instance.User.id_user);

            if (profiledata != null)
            {
                if (profiledata.UserProfile.img != null)
                {
                    profiledata.UserProfile.img = DLL.constantes.urlImages + profiledata.UserProfile.img;
                }
                else
                {
                    profiledata.UserProfile.img = DLL.constantes.ProfileImageError;
                }
                UserProfile = profiledata;

                IsVendor = UserProfile.UserProfile.isVendor ? true : false;
            }

        }
    }
}
