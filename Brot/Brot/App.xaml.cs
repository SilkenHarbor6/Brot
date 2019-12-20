namespace Brot
{
    using Brot.Patterns;
    using System;
    using System.Diagnostics;
    using Views;
    using Xamarin.Forms;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            inicializar();
        }

        private void inicializar()
        {
            try
            {

                if (Singleton.Instance.LocalJson.IsUserLogged())
                {
                    MainPage = new NavigationPage(new MainTabbed());
                }
                else
                {
                    MainPage = new NavigationPage(new Login());
                }
            }
            catch (Exception ex) 
            {
                Debug.Print(ex.Message);
                MainPage = new NavigationPage(new Login()); 
            }
           ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#031540");
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
