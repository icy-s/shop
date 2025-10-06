using Microsoft.Maui.Layouts;
using SymbolGame;

namespace SymbolGame
{
    public partial class MainPage : ContentPage
    {
        private Game? game;
        private int counter = 0;
        private Random rng = new Random();

        public MainPage()
        {
            InitializeComponent();
            ThemePicker.ItemsSource = new List<Theme>
        {
            new Theme("Hele", Colors.White, Colors.Black, "Songstar"),
            new Theme("Tume", Colors.Black, Colors.White, "ToThePoint"),
            new Theme("Sinine", Colors.LightBlue, Colors.DarkBlue, "ShinyCrystal")
        };

            ThemePicker.SelectedIndexChanged += ThemePicker_SelectedIndexChanged;
            ThemePicker.SelectedIndex = 0;
        }

        private void ThemePicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ThemePicker.SelectedItem is Theme theme)
            {
                theme.Apply(this);
            }
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            if (ThemePicker.SelectedItem is not Theme theme)
            {
                DisplayAlert("Viga", "Palun vali teema", "OK");
                return;
            }

            var player = new Player("Mängija", SymbolEntry.Text ?? "X");

            if (game != null)
            {
                game.OnShowSymbol -= ShowSymbol;
                game.OnHideSymbol -= HideSymbol;
                game.OnGameFinished -= GameFinished;
                game.Stop();
            }

            game = new Game(player, theme, DurationSlider.Value);
            game.OnShowSymbol += ShowSymbol;
            game.OnHideSymbol += HideSymbol;
            game.OnGameFinished += GameFinished;

            counter = 0;
            CounterLabel.Text = "Ilmunud: 0";

            game.Start();
        }

        private void ShowSymbol(string symbol)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                counter++;
                CounterLabel.Text = $"Ilmunud: {counter}";

                double x = rng.NextDouble();
                double y = rng.NextDouble() * 0.7 + 0.3;

                SymbolLabel.Text = symbol;
                AbsoluteLayout.SetLayoutBounds(SymbolLabel, new Rect(x, y, -1, -1));
                AbsoluteLayout.SetLayoutFlags(SymbolLabel, AbsoluteLayoutFlags.PositionProportional);
                SymbolLabel.IsVisible = true;
            });
        }

        private void HideSymbol()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SymbolLabel.IsVisible = false;
            });
        }

        private void GameFinished()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Mäng läbi", $"Sümbol ilmus {counter} korda", "OK");
            });
        }
    }
}