using UnityEngine;

namespace JamForge
{
    public static class ColorizedStringExtensions
    {
        public static string Colorize(this string str, string color)
        {
            return $"<color={color}>{str}</color>";
        }
        
        public static string Colorize(this string str, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{str}</color>";
        }
        
        public static string Colorize(this string str, Color32 color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{str}</color>";
        }
        
        public static string DyeRed(this string str)
        {
            return str.Colorize("red");
        }
        
        public static string DyeGreen(this string str)
        {
            return str.Colorize("green");
        }
        
        public static string DyeBlue(this string str)
        {
            return str.Colorize("blue");
        }
        
        public static string DyeYellow(this string str)
        {
            return str.Colorize("yellow");
        }
        
        public static string DyeMagenta(this string str)
        {
            return str.Colorize("magenta");
        }
        
        public static string DyeCyan(this string str)
        {
            return str.Colorize("cyan");
        }
    }
}
