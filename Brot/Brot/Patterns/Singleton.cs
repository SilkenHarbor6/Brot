
namespace Brot.Patterns
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        private ObservableCollection<userModel> stores;
        #endregion

        #region MyRegion
        public static string passw = String.Empty; 
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
            stores = new ObservableCollection<userModel>();
            this._Dialogs = new DialogService();
            this._LocalJson = new UserJson();
            img = new PickPhotoAsync();
            VerifyLoggedUser();
        }

        #endregion

        #region Methods
        public void AddStore(userModel item)
        {
            stores.Add(item);
        }
        public userModel GetStore(int id)
        {
            return stores[id];
        }
        private void VerifyLoggedUser()
        {
            if (!this._LocalJson.IsUserLogged())
            {
                this.User = new userModel();
                return;
            }

            this.User = this._LocalJson.ReadData();
        }
        public async Task<string> ChangePic()
        {
            return await img.ChangePicture();
        }
        #endregion
    }
}
