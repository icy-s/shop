using Microsoft.Extensions.Logging;

namespace SymbolGame
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
                    fonts.AddFont("Songstar.ttf", "Songstar");
                    fonts.AddFont("ToThePoint.ttf", "ToThePoint");
                    fonts.AddFont("ShinyCrystal.ttf", "ShinyCrystal");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
