using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

using Figgle;

namespace AdventOfCode_2020.Common
{
    public static class FiggleFontExtensions
    {
        private static readonly PropertyInfo[] figgleFontProperties;

        static FiggleFontExtensions()
        {
            figgleFontProperties = GetPublicStaticFiggleFontProperties();
        }

        public static FiggleFont RandomFiggleFont()
        {
            var index = RandomNumberGenerator.GetInt32(0, figgleFontProperties.Length);
            return (FiggleFont)figgleFontProperties[index].GetValue(null, null);
        }

        public static PropertyInfo[] GetPublicStaticFiggleFontProperties()
        {
            return typeof(FiggleFonts)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.PropertyType == typeof(FiggleFont))
                .ToArray();
        }
    }
}
