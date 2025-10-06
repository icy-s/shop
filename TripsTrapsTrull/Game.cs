using SymbolGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolGame
{
    public class Game
    {
        public Player CurrentPlayer { get; set;}
        public Theme CurrentTheme { get; set;}
        public double DurationMs { get; set; }

        public event Action<string>? OnShowSymbol;
        public event Action? OnHideSymbol;
        public event Action? OnGameFinished;

        private bool isRunning;
        private Random rng = new Random();

        public Game(Player player, Theme theme, double durationMs)
        {
            CurrentPlayer = player;
            CurrentTheme = theme;
            DurationMs = durationMs;
        }

        public async void Start()
        {
            isRunning = true;
            var start = DateTime.Now;

            while (isRunning && (DateTime.Now - start).TotalMilliseconds < DurationMs)
            {
                OnShowSymbol?.Invoke(CurrentPlayer.Symbol);

                // Kuvatud sümbool jääb ekraanile 500 ms
                await Task.Delay(500);
                OnHideSymbol?.Invoke();

                // Juhuslik paus 0.5s kuni 2s
                int pause = rng.Next(500, 2000);
                await Task.Delay(pause);
            }

            isRunning = false;
            OnGameFinished?.Invoke();
        }

        public void Stop() => isRunning = false;
    }
}
