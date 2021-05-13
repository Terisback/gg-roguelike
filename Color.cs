using System.Drawing;

namespace DungeonMaster
{
    public enum Color
    {
        None,
        Red,
        Green,
        Blue,
    }

    public static class ColorExt
    {
        public static KnownColor KnownColor(this Color color)
        {
            switch (color)
            {
                case Color.Red:
                    return System.Drawing.KnownColor.Red;
                case Color.Green:
                    return System.Drawing.KnownColor.Green;
                case Color.Blue:
                    return System.Drawing.KnownColor.Blue;
                default:
                    return System.Drawing.KnownColor.GrayText;
            }
        }
    }
}