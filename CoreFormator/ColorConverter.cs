using System.Drawing;

namespace CoreFormator
{
    public sealed class ColorConverter
    {
        public static Color GetBackColorByGroup(int i)
        {
            switch (i)
            {
                case 0:
                    return Color.Aqua;
                case 1:
                    return Color.GreenYellow;
                case 2:
                    return Color.Orange;
                case 3:
                    return Color.Fuchsia;
                case 4:
                    return Color.Gray;
                case 5:
                    return Color.Purple;
                case 6:
                    return Color.Yellow;
                case 7:
                    return Color.Orange;
                case 8:
                    return Color.Lime;
                case 9:
                    return Color.Green;
                case 10:
                    return Color.Teal;
                case 11:
                    return Color.DarkGray;
                    break;
            }
            return Color.DeepSkyBlue;
        }
        public static Color GetColorByGroup(int i = 0)
        {
            switch (i)
            {
                case 4:
                case 5:
                case 9:
                case 10:
                case 11:
                    return Color.White;
            }
            return Color.Black;
        } 
    }
}