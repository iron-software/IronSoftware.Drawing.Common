﻿using System;
using System.Globalization;
using System.Linq;

namespace IronSoftware.Drawing
{
    /// <summary>
    /// A universally compatible Color for .NET 7, .NET 6, .NET 5, and .NET Core. As well as compatiblity with Windows, NanoServer, IIS, macOS, Mobile, Xamarin, iOS, Android, Google Compute, Azure, AWS, and Linux.
    /// <para>Works nicely with popular Image Color such as <see cref="System.Drawing.Color"/>, <see cref="SkiaSharp.SKColor"/>, <see cref="SixLabors.ImageSharp.Color"/>, <see cref="Microsoft.Maui.Graphics.Color"/>.</para>
    /// <para>Implicit casting means that using this class to input and output Color from public APIs gives full compatibility to all Color-types fully supported by Microsoft.</para>
    /// </summary>
    public partial class Color
    {

        /// <summary>
        /// Gets the alpha component value of this <see cref="Color"/> structure.
        /// </summary>
        /// <return>The alpha component value of this <see cref="Color"/>.</return>
        public byte A { get; internal set; }

        /// <summary>
        /// Gets the green component value of this <see cref="Color"/> structure.
        /// </summary>
        /// <return>The green component value of this <see cref="Color"/>.</return>
        public byte G { get; internal set; }

        /// <summary>
        /// Gets the blue component value of this <see cref="Color"/> structure.
        /// </summary>
        /// <return>The blue component value of this <see cref="Color"/>.</return>
        public byte B { get; internal set; }

        /// <summary>
        /// Gets the red component value of this <see cref="Color"/> structure.
        /// </summary>
        /// <return>The red component value of this <see cref="Color"/>.</return>
        public byte R { get; internal set; }

        /// <summary>
        /// Construct a new <see cref="Color"/>.
        /// <br/><para><b>Further Documentation:</b><br/><a href="https://ironsoftware.com/open-source/csharp/drawing/examples/create-color/">Code Example</a></para>
        /// </summary>
        /// <param name="colorcode">The hexadecimal representation of the combined color components arranged in rgb, argb, rrggbb, or aarrggbb format to match web syntax.</param>
        public Color(string colorcode)
        {
            string trimmedColorcode = colorcode.TrimStart('#');

            if (trimmedColorcode.Length == 8)
            {
                this.A = ConvertToHexNumberByte(trimmedColorcode, 0, 2);
                this.R = ConvertToHexNumberByte(trimmedColorcode, 2, 2);
                this.G = ConvertToHexNumberByte(trimmedColorcode, 4, 2);
                this.B = ConvertToHexNumberByte(trimmedColorcode, 6, 2);
            }
            else if (trimmedColorcode.Length == 6)
            {
                this.A = 255;
                this.R = ConvertToHexNumberByte(trimmedColorcode, 0, 2);
                this.G = ConvertToHexNumberByte(trimmedColorcode, 2, 2);
                this.B = ConvertToHexNumberByte(trimmedColorcode, 4, 2);
            }
            else if (trimmedColorcode.Length == 3)
            {
                this.A = 255;
                this.R = ConvertToHexNumberByte(trimmedColorcode, 0, 1);
                this.G = ConvertToHexNumberByte(trimmedColorcode, 1, 1);
                this.B = ConvertToHexNumberByte(trimmedColorcode, 2, 1);
            }
            else
            {
                throw NoConverterException(colorcode, null);
            }
        }

        /// <summary>
        /// Construct a new <see cref="Color"/>.
        /// <br/><para><b>Further Documentation:</b><br/><a href="https://ironsoftware.com/open-source/csharp/drawing/examples/create-color/">Code Example</a></para>
        /// </summary>
        /// <param name="alpha">The alpha component. Valid values are 0 through 255.</param>
        /// <param name="red">The red component. Valid values are 0 through 255.</param>
        /// <param name="green">The green component. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component. Valid values are 0 through 255.</param>
        public Color(int alpha, int red, int green, int blue)
        {
            this.A = (byte)alpha;
            this.R = (byte)red;
            this.G = (byte)green;
            this.B = (byte)blue;
        }

        /// <summary>
        /// Construct a new <see cref="Color"/>.
        /// <br/><para><b>Further Documentation:</b><br/><a href="https://ironsoftware.com/open-source/csharp/drawing/examples/create-color/">Code Example</a></para>
        /// </summary>
        /// <param name="red">The red component. Valid values are 0 through 255.</param>
        /// <param name="green">The green component. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component. Valid values are 0 through 255.</param>
        public Color(int red, int green, int blue)
        {
            this.A = 255;
            this.R = (byte)red;
            this.G = (byte)green;
            this.B = (byte)blue;
        }

        /// <summary>
        /// Represents a color that is null.
        /// </summary>
        public static readonly Color Empty;
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F0F8FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color AliceBlue = new("#F0F8FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FAEBD7.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color AntiqueWhite = new("#FAEBD7");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Aqua = new("#00FFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #7FFFD4.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Aquamarine = new("#7FFFD4");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F0FFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Azure = new("#F0FFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F5F5DC.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Beige = new("#F5F5DC");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE4C4.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Bisque = new("#FFE4C4");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #000000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Black = new("#000000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFEBCD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color BlanchedAlmond = new("#FFEBCD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #0000FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Blue = new("#0000FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #8A2BE2.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color BlueViolet = new("#8A2BE2");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #A52A2A.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Brown = new("#A52A2A");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DEB887.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color BurlyWood = new("#DEB887");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #5F9EA0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color CadetBlue = new("#5F9EA0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #7FFF00.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Chartreuse = new("#7FFF00");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #D2691E.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Chocolate = new("#D2691E");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF7F50.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Coral = new("#FF7F50");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #6495ED.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color CornflowerBlue = new("#6495ED");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF8DC.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Cornsilk = new("#FFF8DC");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DC143C.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Crimson = new("#DC143C");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Cyan = new("#00FFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00008B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkBlue = new("#00008B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #008B8B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkCyan = new("#008B8B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #B8860B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkGoldenrod = new("#B8860B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #A9A9A9.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkGray = new("#A9A9A9");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #006400.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkGreen = new("#006400");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #BDB76B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkKhaki = new("#BDB76B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #8B008B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkMagenta = new("#8B008B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #556B2F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkOliveGreen = new("#556B2F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF8C00.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkOrange = new("#FF8C00");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #9932CC.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkOrchid = new("#9932CC");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #8B0000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkRed = new("#8B0000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #E9967A.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkSalmon = new("#E9967A");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #8FBC8B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkSeaGreen = new("#8FBC8B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #483D8B.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkSlateBlue = new("#483D8B");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #2F4F4F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkSlateGray = new("#2F4F4F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00CED1.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkTurquoise = new("#00CED1");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #9400D3.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DarkViolet = new("#9400D3");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF1493.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DeepPink = new("#FF1493");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00BFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DeepSkyBlue = new("#00BFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #696969.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DimGray = new("#696969");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #1E90FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color DodgerBlue = new("#1E90FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #B22222.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Firebrick = new("#B22222");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFAF0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color FloralWhite = new("#FFFAF0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #228B22.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color ForestGreen = new("#228B22");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Fuchsia = new("#FF00FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DCDCDC.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Gainsboro = new("#DCDCDC");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F8F8FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color GhostWhite = new("#F8F8FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFD700.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Gold = new("#FFD700");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DAA520.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Goldenrod = new("#DAA520");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #808080.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Gray = new("#808080");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #008000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Green = new("#008000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #ADFF2F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color GreenYellow = new("#ADFF2F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F0FFF0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Honeydew = new("#F0FFF0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF69B4.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color HotPink = new("#FF69B4");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #CD5C5C.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color IndianRed = new("#CD5C5C");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #4B0082.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Indigo = new("#4B0082");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFF0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Ivory = new("#FFFFF0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F0E68C.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Khaki = new("#F0E68C");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #E6E6FA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Lavender = new("#E6E6FA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF0F5.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LavenderBlush = new("#FFF0F5");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #7CFC00.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LawnGreen = new("#7CFC00");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFACD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LemonChiffon = new("#FFFACD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #ADD8E6.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightBlue = new("#ADD8E6");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F08080.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightCoral = new("#F08080");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #E0FFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightCyan = new("#E0FFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FAFAD2.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightGoldenrodYellow = new("#FAFAD2");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #D3D3D3.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightGray = new("#D3D3D3");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #90EE90.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightGreen = new("#90EE90");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFB6C1.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightPink = new("#FFB6C1");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFA07A.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightSalmon = new("#FFA07A");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #20B2AA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightSeaGreen = new("#20B2AA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #87CEFA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightSkyBlue = new("#87CEFA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #778899.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightSlateGray = new("#778899");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #B0C4DE.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightSteelBlue = new("#B0C4DE");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFE0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LightYellow = new("#FFFFE0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FF00.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Lime = new("#00FF00");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #32CD32.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color LimeGreen = new("#32CD32");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FAF0E6.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Linen = new("#FAF0E6");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF00FF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Magenta = new("#FF00FF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #800000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Maroon = new("#800000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #66CDAA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumAquamarine = new("#66CDAA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #0000CD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumBlue = new("#0000CD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #BA55D3.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumOrchid = new("#BA55D3");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #9370DB.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumPurple = new("#9370DB");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #3CB371.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumSeaGreen = new("#3CB371");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #7B68EE.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumSlateBlue = new("#7B68EE");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FA9A.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumSpringGreen = new("#00FA9A");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #48D1CC.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumTurquoise = new("#48D1CC");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #C71585.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MediumVioletRed = new("#C71585");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #191970.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MidnightBlue = new("#191970");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F5FFFA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MintCream = new("#F5FFFA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE4E1.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color MistyRose = new("#FFE4E1");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFE4B5.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Moccasin = new("#FFE4B5");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDEAD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color NavajoWhite = new("#FFDEAD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #000080.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Navy = new("#000080");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FDF5E6.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color OldLace = new("#FDF5E6");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #808000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Olive = new("#808000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #6B8E23.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color OliveDrab = new("#6B8E23");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFA500.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Orange = new("#FFA500");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF4500.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color OrangeRed = new("#FF4500");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DA70D6.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Orchid = new("#DA70D6");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #EEE8AA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PaleGoldenrod = new("#EEE8AA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #98FB98.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PaleGreen = new("#98FB98");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #AFEEEE.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PaleTurquoise = new("#AFEEEE");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DB7093.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PaleVioletRed = new("#DB7093");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFEFD5.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PapayaWhip = new("#FFEFD5");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFDAB9.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PeachPuff = new("#FFDAB9");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #CD853F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Peru = new("#CD853F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFC0CB.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Pink = new("#FFC0CB");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #DDA0DD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Plum = new("#DDA0DD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #B0E0E6.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color PowderBlue = new("#B0E0E6");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #800080.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Purple = new("#800080");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #663399.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color RebeccaPurple = new("#663399");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF0000.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Red = new("#FF0000");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #BC8F8F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color RosyBrown = new("#BC8F8F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #4169E1.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color RoyalBlue = new("#4169E1");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #8B4513.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SaddleBrown = new("#8B4513");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FA8072.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Salmon = new("#FA8072");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F4A460.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SandyBrown = new("#F4A460");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #2E8B57.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SeaGreen = new("#2E8B57");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFF5EE.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SeaShell = new("#FFF5EE");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #A0522D.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Sienna = new("#A0522D");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #C0C0C0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Silver = new("#C0C0C0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #87CEEB.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SkyBlue = new("#87CEEB");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #6A5ACD.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SlateBlue = new("#6A5ACD");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #708090.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SlateGray = new("#708090");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFAFA.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Snow = new("#FFFAFA");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FF7F.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SpringGreen = new("#00FF7F");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #4682B4.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color SteelBlue = new("#4682B4");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #D2B48C.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Tan = new("#D2B48C");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #008080.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Teal = new("#008080");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #D2B48C.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Thistle = new("#D8BFD8");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FF6347.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Tomato = new("#FF6347");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #00FFFFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Transparent = new("#00FFFFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #40E0D0.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Turquoise = new("#40E0D0");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #EE82EE.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Violet = new("#EE82EE");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F5DEB3.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Wheat = new("#F5DEB3");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFFFF.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color White = new("#FFFFFF");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #F5F5F5.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color WhiteSmoke = new("#F5F5F5");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #FFFF00.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color Yellow = new("#FFFF00");
        /// <summary>
        /// Gets a system-defined color that has an ARGB value of #9ACD32.
        /// </summary>
        /// <return>A <see cref="Color"/> representing a system-defined color.</return>
        public static Color YellowGreen = new("#9ACD32");

        /// <summary>
        /// Creates a <see cref="Color"/> structure from the specified 8-bit color values
        /// (red, green, and blue). The alpha value is implicitly 255 (fully opaque). Although
        /// this method allows a 32-bit value to be passed for each color component, the
        /// value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="red">The red component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="green">The green component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <returns><see cref="Color"/></returns>
        /// <seealso cref="FromArgb(int)"/>
        /// <seealso cref="FromArgb(int, Color)"/>
        /// <seealso cref="FromArgb(int, int, int, int)"/>
        public static Color FromArgb(int red, int green, int blue)
        {
            return new Color(red, green, blue);
        }

        /// <summary>
        /// Creates a <see cref="Color"/> structure from the specified 8-bit color values
        /// (alpha, red, green, and blue). Although this method allows a 32-bit value to be passed for each color component,
        /// the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="alpha">The alpha value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="red">The red component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="green">The green component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <returns><see cref="Color"/></returns>
        /// <seealso cref="FromArgb(int)"/>
        /// <seealso cref="FromArgb(int, Color)"/>
        /// <seealso cref="FromArgb(int, int, int)"/>
        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            return new Color(alpha, red, green, blue);
        }

        /// <summary>
        /// Creates a <see cref="Color"/> structure from the specified <see cref="Color"/> structure, but with the new specified alpha value. 
        /// <para>Although this method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.</para>
        /// </summary>
        /// <param name="alpha">The alpha value for the new <see cref="Color"/>. Valid values are 0 through 255.</param>
        /// <param name="baseColor">The <see cref="Color"/> from which to create the new <see cref="Color"/>.</param>
        /// <returns><see cref="Color"/></returns>
        /// <seealso cref="FromArgb(int)"/>
        /// <seealso cref="FromArgb(int, int, int)"/>
        /// <seealso cref="FromArgb(int, int, int, int)"/>
        public static Color FromArgb(int alpha, Color baseColor)
        {
            return new Color(alpha, baseColor.R, baseColor.G, baseColor.B);
        }

        /// <summary>
        /// Creates a <see cref="Color"/> structure from a 32-bit ARGB value.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <returns><see cref="Color"/></returns>
        /// <seealso cref="FromArgb(int, Color)"/>
        /// <seealso cref="FromArgb(int, int, int)"/>
        /// <seealso cref="FromArgb(int, int, int, int)"/>
        public static Color FromArgb(int argb)
        {
            return new Color(argb.ToString("X"));
        }

        /// <summary>
        /// Creates a <see cref="Color"/> structure from the specified name of a predefined color.
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="Color"/></returns>
        public static Color FromName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (KnownColors.ArgbByName.TryGetValue(name.ToLower(), out uint argb))
                    return Color.FromArgb((int)argb);
            }
            throw new InvalidOperationException($"{name} is unable to convert to {typeof(Color)} because it requires a suitable name.");
        }

        /// <summary>
        /// Returns the color as a string in the format: #AARRGGBB.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"#{this.A:X}{this.R:X}{this.G:X}{this.B:X}";
        }

        /// <summary>
        /// Luminance is a value from 0 (black) to 100 (white) where 50 is the perceptual "middle grey". 
        /// Luminance = 50 is the equivalent of Y = 18.4, or in other words a 18% grey card, representing the middle of a photographic exposure.
        /// </summary>
        /// <returns>Preceived Lightness</returns>
        public double GetLuminance()
        {
            return Math.Round(Percentage(255, CalculateLuminance()), MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Gets the 32-bit ARGB value of this <see cref="Color"/> structure.
        /// <br/><para><b>Further Documentation:</b><br/><a href="https://ironsoftware.com/open-source/csharp/drawing/examples/convert-color-to-32-bit-argb-value/">Code Example</a></para>
        /// </summary>
        /// <returns>The 32-bit ARGB value of this <see cref="Color"/>.</returns>
        public int ToArgb()
        {
            return (this.A << 24) | (this.R << 16) | (this.G << 8) | this.B;
        }

        /// <summary>
        /// Implicitly casts <see cref="System.Drawing.Color"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="System.Drawing.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="System.Drawing.Color"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(System.Drawing.Color Color)
        {
            return new Color(Color.A, Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="System.Drawing.Color"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="System.Drawing.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="System.Drawing.Color"/> </param>
        static public implicit operator System.Drawing.Color(Color Color)
        {
            return System.Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts <see cref="SkiaSharp.SKColor"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SkiaSharp.SKColor"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SkiaSharp.SKColor"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SkiaSharp.SKColor Color)
        {
            return new Color(Color.Alpha, Color.Red, Color.Green, Color.Blue);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SkiaSharp.SKColor"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SkiaSharp.SKColor"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SkiaSharp.SKColor"/> </param>
        static public implicit operator SkiaSharp.SKColor(Color Color)
        {
            return new SkiaSharp.SKColor(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.Color"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.Color"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.Color Color)
        {
            string hex = Color.ToHex();
            return new Color(ConvertToHexNumberByte(hex, 6, 2), ConvertToHexNumberByte(hex, 0, 2), ConvertToHexNumberByte(hex, 2, 2), ConvertToHexNumberByte(hex, 4, 2));
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.Color"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.Color"/> </param>
        static public implicit operator SixLabors.ImageSharp.Color(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgba(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Rgba32 Color)
        {
            string hex = Color.ToHex(); // Rgba
            return new Color(ConvertToHexNumberByte(hex, 6, 2), ConvertToHexNumberByte(hex, 0, 2), ConvertToHexNumberByte(hex, 2, 2), ConvertToHexNumberByte(hex, 4, 2));
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Rgba32"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Rgba32(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgba(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Bgra32 Color)
        {
            return new Color(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Bgra32"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Bgra32(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgba(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Rgb24 Color)
        {
            return new Color(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Rgb24"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Rgb24(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgb(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Bgr24 Color)
        {
            return new Color(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Bgr24"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Bgr24(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgb(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Rgb48 Color)
        {
            return new Color(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Rgb48"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Rgb48(Color Color)
        {
            return new SixLabors.ImageSharp.PixelFormats.Rgb48(Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Rgba64 Color)
        {
            return new Color(Color.A, Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Rgba64"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Rgba64(Color Color)
        {
            return SixLabors.ImageSharp.Color.FromRgba(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Abgr32 Color)
        {
            return new Color(Color.A, Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Abgr32"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Abgr32(Color Color)
        {
            return new SixLabors.ImageSharp.PixelFormats.Abgr32(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(SixLabors.ImageSharp.PixelFormats.Argb32 Color)
        {
            return new Color(Color.A, Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Implicitly casts to <see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="SixLabors.ImageSharp.PixelFormats.Argb32"/> </param>
        static public implicit operator SixLabors.ImageSharp.PixelFormats.Argb32(Color Color)
        {
            return new SixLabors.ImageSharp.PixelFormats.Argb32(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Implicitly casts <see cref="Microsoft.Maui.Graphics.Color"/> objects to <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="Microsoft.Maui.Graphics.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Microsoft.Maui.Graphics.Color"/> will automatically be cast to <see cref="Color"/> </param>
        public static implicit operator Color(Microsoft.Maui.Graphics.Color Color)
        {
            Color.ToRgba(out byte r, out byte g, out byte b, out byte a);
            return new Color(a, r, g, b);
        }

        /// <summary>
        /// Implicitly casts to <see cref="Microsoft.Maui.Graphics.Color"/> objects from <see cref="Color"/>.  
        /// <para>When your .NET Class methods use <see cref="Color"/> as parameters or return types, you now automatically support <see cref="Microsoft.Maui.Graphics.Color"/> as well.</para>
        /// </summary>
        /// <param name="Color"><see cref="Color"/> is explicitly cast to a <see cref="Microsoft.Maui.Graphics.Color"/> </param>
        static public implicit operator Microsoft.Maui.Graphics.Color(Color Color)
        {
            return Microsoft.Maui.Graphics.Color.FromRgba(Color.R, Color.G, Color.B, Color.A);
        }

        /// <summary>
        /// Tests whether two specified <see cref="Color"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="Color"/> that is to the right of the equality operator.</param>
        /// <returns>true if the two <see cref="Color"/> structures are equal; otherwise, false.</returns>
        public static bool operator ==(Color left, Color right)
        {
            if (left is null && right is null)
            {
                return true;
            }
            else if ((left is not null && right is null) ||
                    (left is null && right is not null))
            {
                return false;
            }

            return left.R == right.R &&
                left.G == right.G &&
                left.B == right.B &&
                left.A == right.A;
        }

        /// <summary>
        /// Tests whether two specified <see cref="Color"/> structures are different.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="Color"/> that is to the right of the inequality operator.</param>
        /// <returns>true if the two <see cref="Color"/> structures are different; otherwise, false.</returns>
        public static bool operator !=(Color left, Color right)
        {
            if (left is null && right is null)
            {
                return false;
            }
            else if ((left is not null && right is null) ||
                    (left is null && right is not null))
            {
                return true;
            }

            return left.R != right.R ||
                left.G != right.G ||
                left.B != right.B ||
                left.A != right.A;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to other; otherwise, false.</returns>
        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (other is not Color)
            {
                return false;
            }

            return this.R == ((Color)other).R &&
                   this.G == ((Color)other).G &&
                   this.B == ((Color)other).B &&
                   this.A== ((Color)other).A;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return 0;
        }

        #region Private Method

        private static InvalidOperationException NoConverterException(string color, Exception innerException)
        {
            return new InvalidOperationException($"{color} is unable to convert to {typeof(Color)} because it requires a suitable length of string.", innerException);
        }

        private double Percentage(int total, double value)
        {
            return (value * 100) / (double)total;
        }

        private static byte ConvertToHexNumberByte(string colorcode, int start, int length)
        {
            if (length == 2)
            {
                return (byte)int.Parse(colorcode.Substring(start, length), NumberStyles.HexNumber);
            }
            else if (length == 1)
            {
                return (byte)int.Parse(string.Join("", Enumerable.Repeat(colorcode.Substring(start, length), 2)), NumberStyles.HexNumber);
            }
            else
            {
                throw new InvalidOperationException($"{colorcode} is unable to convert to {typeof(byte)} because it requires 1 or 2 for length.");
            }
        }

        private double CalculateLuminance()
        {
            return 0.299 * R + 0.587 * G + 0.114 * B;
        }

        #endregion
    }
}
