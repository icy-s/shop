using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolGame
{
    public class Theme
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public string FontFamily { get; set; }
        public Theme(string name, Color background, Color text, string font)
        {
            Name = name;
            BackgroundColor = background;
            TextColor = text;
            FontFamily = font;
        }

        public override string ToString() => Name;
        public void Apply(ContentPage page)
        {
            // Taust
            page.BackgroundColor = BackgroundColor;

            // Rakendame globaalsed stiilid
            Application.Current.Resources["GlobalLabelStyle"] = new Style(typeof(Label))
            {
                Setters =
            {
                new Setter() { Property = Label.FontFamilyProperty, Value = FontFamily },
                new Setter() { Property = Label.TextColorProperty, Value = TextColor },
            }
            };

            Application.Current.Resources["GlobalEntryStyle"] = new Style(typeof(Entry))
            {
                Setters =
            {
                new Setter() { Property = Entry.FontFamilyProperty, Value = FontFamily },
                new Setter() { Property = Entry.TextColorProperty, Value = TextColor },
            }
            };

            Application.Current.Resources["GlobalButtonStyle"] = new Style(typeof(Button))
            {
                Setters =
            {
                new Setter() { Property = Button.FontFamilyProperty, Value = FontFamily },
                new Setter() { Property = Button.TextColorProperty, Value = TextColor },
            }
            };
        }
    }
}
