using Microsoft.Maui.Controls;

namespace SymbolGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}