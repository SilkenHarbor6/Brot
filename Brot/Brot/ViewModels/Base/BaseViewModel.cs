namespace Brot.ViewModels
{

    #region BaseViewmodel Class

    using Brot.Patterns;
    using Models;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    public class BaseViewModel : ObservableObject 
    {
        #region Attributes
        private string _Title = String.Empty;
        #endregion

        #region properties
        public string Title
        {
            get { return this._Title; }
            set => SetProperty(ref _Title, value);
        }


        bool isRefreshing;
        public bool IsRefreshing
		{
			get => isRefreshing;
			set
			{
				if (SetProperty(ref isRefreshing, value))
					IsNotBusy = !isRefreshing;
			}
		}
		bool isNotBusy = true;
		public bool IsNotBusy
		{
			get => isNotBusy;
			set
			{
				if (SetProperty(ref isNotBusy, value))
					IsRefreshing = !isNotBusy;
			}
		}
        

        #endregion
    }


    #endregion
}
