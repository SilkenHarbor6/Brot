
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResponsePublicacionFeedViewCell : Frame
    {
        public ResponsePublicacionFeedViewCell()
        {
            InitializeComponent();
        }

        private void CachedImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            Debug.Print(sender.ToString());
            Debug.Print(e.Exception.Message);
            Debug.Print(e.Exception.InnerException.Message);
        }
    }
}