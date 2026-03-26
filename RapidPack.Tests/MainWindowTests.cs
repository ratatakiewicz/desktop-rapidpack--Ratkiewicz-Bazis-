using Avalonia.Headless.XUnit;
using Xunit;
using System;
using RapidPack.Logic; // Dostęp do kalkulatora

namespace RapidPack.Tests;

public class MainWindowTests
{
    // --- TESTY INTERFEJSU ---

    [AvaloniaFact]
    public void CreateWindow_ShouldCreateANewWindow()
    {
        // Sprawdza, czy okno główne w ogóle się poprawnie tworzy
        var window = new MainWindow();
        Assert.NotNull(window);
    }

    // --- TESTY LOGIKI (Zadanie #7 - dopisane) ---

    [Fact]
    public void CalculatePrice_WeightOverLimit_ShouldThrowArgumentException()
    {
        // Sprawdza, czy Twoja walidacja z Zadania #5 działa (blokada > 30kg)
        var calculator = new ParcelCalculator();
        
        var exception = Assert.Throws<ArgumentException>(() => 
            calculator.CalculatePrice(10, 10, 10, 31, false, PackageType.Standard));
            
        Assert.Contains("nie obsługuje paczek cięższych niż 30 kg", exception.Message);
    }

    [Fact]
    public void CalculatePrice_Pallet_ShouldReturnFixedBasePrice()
    {
        // Sprawdza, czy paleta ma stałą cenę 100 zł (Zadanie #5)
        var calculator = new ParcelCalculator();
        decimal expected = 100m;

        var result = calculator.CalculatePrice(80, 120, 100, 25, false, PackageType.Pallet);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculatePrice_GabaritSurcharge_ShouldBeApplied()
    {
        // Sprawdza dopłatę 50% za gabaryt (suma wymiarów > 150cm)
        var calculator = new ParcelCalculator();
        // 10 (baza) + 5kg * 2 = 20. Z gabarytem: 20 * 1.5 = 30.
        decimal expected = 30m;

        var result = calculator.CalculatePrice(60, 60, 40, 5, false, PackageType.Standard);

        Assert.Equal(expected, result);
    }
}