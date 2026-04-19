#if ANDROID
using Android.Views;
using Microsoft.Maui.Platform;
#endif

namespace LeeWengCar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        // Use the full MAUI namespace here
        protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState? activationState)
        {
            return new Microsoft.Maui.Controls.Window(new MainPage()) { Title = "LeeWengCar" };
        }

#if ANDROID
        protected override void OnStart()
        {
            base.OnStart();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            if (activity == null || activity.Window == null) return;

            var androidWindow = activity.Window;

            // Using 'dynamic' or var with full paths helps avoid the 'missing assembly' error 
            // during the cross-platform compilation phase.
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.R)
            {
                androidWindow.SetDecorFitsSystemWindows(false);
                
                // We access the controller as an object to avoid the direct class reference error
                var controller = androidWindow.InsetsController;
                
                if (controller != null)
                {
                    // 1 = WindowInsets.Type.SystemBars()
                    controller.Hide(1); 
                    
                    // 2 = BehaviorsShowTransientBarsBySwipe
                    controller.SystemBarsBehavior = 2; 
                }
            }
            else
            {
#pragma warning disable CS0618
                androidWindow.DecorView.SystemUiVisibility = (Android.Views.StatusBarVisibility)(
                    Android.Views.SystemUiFlags.Fullscreen | 
                    Android.Views.SystemUiFlags.HideNavigation | 
                    Android.Views.SystemUiFlags.ImmersiveSticky);
#pragma warning restore CS0618
            }
        }
#endif
    }
}