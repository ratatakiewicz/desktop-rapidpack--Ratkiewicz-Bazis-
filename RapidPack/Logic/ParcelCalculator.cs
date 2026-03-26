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
            if (weight > 30)
                throw new ArgumentException("Błąd: paczka jest za ciężka.");

            if (type == PackageType.Pallet)
                return isExpress ? 115m : 100m;

            decimal basePrice = 10m;
            decimal weightPrice = (decimal)weight * 2m;
            decimal currentTotal = basePrice + weightPrice;

            if (type == PackageType.Careful)
                currentTotal += 10m;

            if ((width + height + depth) > 150)
                currentTotal *= 1.5m;

            if (isExpress)
                currentTotal += 15m;

            return currentTotal;
        }

        public string GetSummary(double width, double height, double depth, double weight, bool isExpress, PackageType type)
        {
            string typeName = type switch
            {
                PackageType.Standard => "📦 Standardowa",
                PackageType.Careful  => "🔮 Ostrożnie (szkło)",
                PackageType.Pallet   => "🪵 Paleta",
                _                    => "Nieznany"
            };

            string express = isExpress ? "✅ Tak" : "❌ Nie";

            return $"Typ: {typeName}\n" +
                   $"Waga: {weight} kg\n" +
                   $"Wymiary: {width} × {height} × {depth} cm\n" +
                   $"Ekspres: {express}";
        }
    }
}