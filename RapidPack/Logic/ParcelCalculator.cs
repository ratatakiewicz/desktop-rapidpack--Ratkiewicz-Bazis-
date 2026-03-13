using System;

namespace RapidPack.Logic
{
    public enum PackageType
    {
        Standard,
        Careful,
        Pallet
    }

    public class ParcelCalculator
    {
        public decimal CalculatePrice(double width, double height, double depth, double weight, bool isExpress, PackageType type)
        {
            // 1. Walidacja wagi - jeśli powyżej 30 kg, przerywamy i rzucamy błąd
            if (weight > 30)
            {
                throw new ArgumentException("Błąd: Firma RapidPack nie obsługuje paczek cięższych niż 30 kg.");
            }

            // 2. Jeśli to Paleta - cena jest stała i wynosi 100 zł
            if (type == PackageType.Pallet)
            {
                return isExpress ? 115m : 100m; 
            }

            // 3. Liczenie ceny dla Standard i Ostrożnie
            decimal basePrice = 10m;
            decimal weightPrice = (decimal)weight * 2m;
            decimal currentTotal = basePrice + weightPrice;

            // Dopłata za typ "Ostrożnie"
            if (type == PackageType.Careful)
            {
                currentTotal += 10m;
            }

            // 4. Sprawdzenie gabarytu (suma wymiarów > 150 cm)
            if ((width + height + depth) > 150)
            {
                currentTotal *= 1.5m; // Dodajemy 50% do dotychczasowej ceny
            }

            // 5. Dopłata za ekspres (na samym końcu)
            if (isExpress)
            {
                currentTotal += 15m;
            }

            return currentTotal;
        }
    }
}