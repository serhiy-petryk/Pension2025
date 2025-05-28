using System.Globalization;
using System.IO;

namespace Pension2025
{
    public static class Settings
    {
        public const string DataFolder = @"E:\Temp\Pension2025";
        public static string ListFileName = Path.Combine(DataFolder, "List.txt");
        public static string ListFileName_Api = Path.Combine(DataFolder, "List.Api.txt");
        public static string ExtendedListFileName = Path.Combine(DataFolder, "ExtendedList.txt");
        public static CultureInfo UaCulture = new CultureInfo("uk");
    }
}