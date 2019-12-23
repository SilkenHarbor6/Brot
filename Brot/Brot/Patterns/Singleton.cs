
namespace Brot.Patterns
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class Singleton
    {
        #region Attributes

        private static Singleton _Instance;
        private UserJson _LocalJson;
        private userModel _User;
        private DialogService _Dialogs;
        private PickPhotoAsync img;

        #endregion

        #region MyRegion
        public static string passw;
        public static bool fromProfile;
        public static Singleton Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Singleton();

                return _Instance;
            }
        }

        public userModel User
        {
            set
            {
                if (this._User == value)
                    return;

                this._User = value;
            }
            get { return this._User; }
        }

        public DialogService Dialogs
        {
            get { return this._Dialogs; }
        }

        public UserJson LocalJson
        {
            get { return this._LocalJson; }
        }

        #endregion

        #region Constructor

        public Singleton()
        {
            this._Dialogs = new DialogService();
            this._LocalJson = new UserJson();
            img = new PickPhotoAsync();
            VerifyLoggedUser();
        }

        #endregion

        #region Methods

        private void VerifyLoggedUser()
        {
            if (!this._LocalJson.IsUserLogged())
            {
                this.User = new userModel();
                return;
            }

            this.User = this._LocalJson.ReadData();
        }
        public void ChangePic()
        {
            img.ChangePicture();
        }
        #endregion
    }
}
