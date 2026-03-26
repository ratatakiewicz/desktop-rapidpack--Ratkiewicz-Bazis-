using Avalonia.Controls;
using Avalonia.Interactivity;
using RapidPack.Logic;
using System;

namespace RapidPack
{
    public partial class MainWindow : Window
    {
        // Instancja klasy logiki biznesowej odpowiedzialnej za obliczenia kosztów
        private readonly ParcelCalculator _calculator = new ParcelCalculator();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie kliknięcia przycisku "Wyceń".
        /// Pobiera dane z interfejsu użytkownika, przesyła je do silnika obliczeniowego i prezentuje wynik.
        /// </summary>
        public void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pobranie i konwersja danych wejściowych z pól tekstowych (TextBox)
                // Użycie double.TryParse zapewnia stabilność aplikacji przy błędnych danych
                double.TryParse(WeightTextBox.Text, out double weight);
                double.TryParse(WidthTextBox.Text, out double width);
                double.TryParse(HeightTextBox.Text, out double height);
                double.TryParse(DepthTextBox.Text, out double depth);

                // Odczyt opcji dodatkowych oraz wybranego typu przesyłki z ComboBox
                bool isExpress = ExpressCheckBox.IsChecked ?? false;
                PackageType type = (PackageType)ParcelTypeComboBox.SelectedIndex;

                // Przekazanie danych do metody obliczeniowej (Zadanie #6)
                decimal price = _calculator.CalculatePrice(width, height, depth, weight, isExpress, type);

                // Aktualizacja interfejsu użytkownika o wyliczoną kwotę
                ResultTextBlock.Text = $"Sugerowana cena: {price:C2}";
                ResultTextBlock.Foreground = Avalonia.Media.Brushes.SpringGreen;
            }
            catch (ArgumentException ex)
            {
                // Obsługa wyjątków walidacyjnych (np. blokada paczek powyżej 30kg)
                ResultTextBlock.Text = ex.Message;
                ResultTextBlock.Foreground = Avalonia.Media.Brushes.OrangeRed;
            }
            catch (Exception)
            {
                // Zabezpieczenie przed nieoczekiwanymi błędami systemowymi
                ResultTextBlock.Text = "Wystąpił błąd podczas obliczeń.";
                ResultTextBlock.Foreground = Avalonia.Media.Brushes.Red;
            }
        }
    }
}