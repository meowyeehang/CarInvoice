using LeeWengCar.Data;
using Microsoft.Extensions.Logging;
using LeeWengCar.Data; // 1. Add this to link to your Data folder

namespace LeeWengCar
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // 2. Register your Storage Service here
            // This makes the list "Permanent" and accessible everywhere
            builder.Services.AddSingleton<LeeWengCar.Data.StorageService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}