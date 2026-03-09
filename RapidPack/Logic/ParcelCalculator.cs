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
            // logika w #6
            return 0;
        }
    }
}