using System;
using Brot.Patterns;
using Plugin.RoundedBorderControls;
using Xamarin.Forms;

namespace Brot.Tigger
{
    public class PasswordTrigger:TriggerAction<RoundedBorderEntry>
    {
        public PasswordTrigger()
        {
        }

        protected override void Invoke(RoundedBorderEntry sender)
        {
            if (!sender.Text.Equals(Singleton.passw))
            {
                sender.BackgroundColor=Color.LightCoral;
            }
            else
            {
                sender.BackgroundColor = Color.LightGreen;
            }
        }
    }
}
