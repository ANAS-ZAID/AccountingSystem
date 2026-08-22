using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.core.shared
{
    internal class AppColor
    {
        static readonly public Color primary = Color.FromArgb(24, 56, 84);
        static readonly public Color secondary = Color.Goldenrod;
        static readonly public Color third = Color.FromArgb(221, 229, 241);
        static readonly public Color defaultColor = Color.White;
        static readonly public Color btnShadowColor = Color.FromArgb(234, 234, 234);
        static readonly public List<Color> colors = new List<Color>()
        {
            primary,secondary, third,
        };
        static readonly public List<List<Color>> colorsReporte = new List<List<Color>>()
        {
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary}, 
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary},
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary},
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary}, 
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary},
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary}, 
           new List<Color>() {primary, third},
           new List<Color>() {secondary, primary},
        };
    }
}
