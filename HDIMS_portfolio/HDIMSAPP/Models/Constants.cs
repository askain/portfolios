using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace HDIMSAPP.Models
{
    public enum GridDataType { STRING, DATE, NUMBER };

    public static class Constants
    {
        public static Style DirtyCellStyle = App.Current.Resources["DirtyCellStyle"] as Style;
        public static Style DirtyBoldCellStyle = App.Current.Resources["DirtyBoldCellStyle"] as Style;
        public static Style NormalGridViewCell = App.Current.Resources["NormalGridViewCell"] as Style;
        public static Style NormalBoldGridViewCell = App.Current.Resources["NormalBoldGridViewCell"] as Style;
        public static Style NormalGridViewHeaderCell = App.Current.Resources["NormalGridViewHeaderCell"] as Style;
        public static Style HalfHourGridViewCell = App.Current.Resources["HalfHourGridViewCell"] as Style;
        public static Style HalfHourBoldGridViewCell = App.Current.Resources["HalfHourBoldGridViewCell"] as Style;
        public static Style HalfHourGridViewHeaderCell = App.Current.Resources["HalfHourGridViewHeaderCell"] as Style;
        public static Brush DirtyColor = new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFA500")); 

        public static string[] DamData_EditableColumns = { "RWL", "OSPILWL", "ETCIQTY2", "EDQTY", "ETCEDQTY", "SPDQTY", "ETCDQTY1", "ETCDQTY2", "ETCDQTY3", "OTLTDQTY", "ITQTY1", "ITQTY2", "ITQTY3" };
        public static string[] DamData_EditableColumnOfChecks = { "RWL_CK", "OSPILWL_CK", "ETCIQTY2_CK", "EDQTY_CK", "ETCEDQTY_CK", "SPDQTY_CK", "ETCDQTY1_CK", "ETCDQTY2_CK", "ETCDQTY3_CK", "OTLTDQTY_CK", "ITQTY1_CK", "ITQTY2_CK", "ITQTY3_CK" };


        #region 색 이름으로 색찾긔
        private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>();
        static Constants() { namedColors.Add("aliceblue", GetColorFromString("#f0f8ff")); namedColors.Add("antiquewhite", GetColorFromString("#faebd7")); namedColors.Add("aqua", GetColorFromString("#00ffff")); namedColors.Add("aquamarine", GetColorFromString("#7fffd4")); namedColors.Add("azure", GetColorFromString("#f0ffff")); namedColors.Add("beige", GetColorFromString("#f5f5dc")); namedColors.Add("bisque", GetColorFromString("#ffe4c4")); namedColors.Add("black", GetColorFromString("#000000")); namedColors.Add("blanchedalmond", GetColorFromString("#ffebcd")); namedColors.Add("blue", GetColorFromString("#0000ff")); namedColors.Add("blueviolet", GetColorFromString("#8a2be2")); namedColors.Add("brown", GetColorFromString("#a52a2a")); namedColors.Add("burlywood", GetColorFromString("#deb887")); namedColors.Add("cadetblue", GetColorFromString("#5f9ea0")); namedColors.Add("chartreuse", GetColorFromString("#7fff00")); namedColors.Add("chocolate", GetColorFromString("#d2691e")); namedColors.Add("coral", GetColorFromString("#ff7f50")); namedColors.Add("cornflowerblue", GetColorFromString("#6495ed")); namedColors.Add("cornsilk", GetColorFromString("#fff8dc")); namedColors.Add("crimson", GetColorFromString("#dc143c")); namedColors.Add("cyan", GetColorFromString("#00ffff")); namedColors.Add("darkblue", GetColorFromString("#00008b")); namedColors.Add("darkcyan", GetColorFromString("#008b8b")); namedColors.Add("darkgoldenrod", GetColorFromString("#b8860b")); namedColors.Add("darkgray", GetColorFromString("#a9a9a9")); namedColors.Add("darkgreen", GetColorFromString("#006400")); namedColors.Add("darkkhaki", GetColorFromString("#bdb76b")); namedColors.Add("darkmagenta", GetColorFromString("#8b008b")); namedColors.Add("darkolivegreen", GetColorFromString("#556b2f")); namedColors.Add("darkorange", GetColorFromString("#ff8c00")); namedColors.Add("darkorchid", GetColorFromString("#9932cc")); namedColors.Add("darkred", GetColorFromString("#8b0000")); namedColors.Add("darksalmon", GetColorFromString("#e9967a")); namedColors.Add("darkseagreen", GetColorFromString("#8fbc8f")); namedColors.Add("darkslateblue", GetColorFromString("#483d8b")); namedColors.Add("darkslategray", GetColorFromString("#2f4f4f")); namedColors.Add("darkturquoise", GetColorFromString("#00ced1")); namedColors.Add("darkviolet", GetColorFromString("#9400d3")); namedColors.Add("deeppink", GetColorFromString("#ff1493")); namedColors.Add("deepskyblue", GetColorFromString("#00bfff")); namedColors.Add("dimgray", GetColorFromString("#696969")); namedColors.Add("dodgerblue", GetColorFromString("#1e90ff")); namedColors.Add("firebrick", GetColorFromString("#b22222")); namedColors.Add("floralwhite", GetColorFromString("#fffaf0")); namedColors.Add("forestgreen", GetColorFromString("#228b22")); namedColors.Add("fuchsia", GetColorFromString("#ff00ff")); namedColors.Add("gainsboro", GetColorFromString("#dcdcdc")); namedColors.Add("ghostwhite", GetColorFromString("#f8f8ff")); namedColors.Add("gold", GetColorFromString("#ffd700")); namedColors.Add("goldenrod", GetColorFromString("#daa520")); namedColors.Add("gray", GetColorFromString("#808080")); namedColors.Add("green", GetColorFromString("#008000")); namedColors.Add("greenyellow", GetColorFromString("#adff2f")); namedColors.Add("honeydew", GetColorFromString("#f0fff0")); namedColors.Add("hotpink", GetColorFromString("#ff69b4")); namedColors.Add("indianred", GetColorFromString("#cd5c5c")); namedColors.Add("indigo", GetColorFromString("#4b0082")); namedColors.Add("ivory", GetColorFromString("#fffff0")); namedColors.Add("khaki", GetColorFromString("#f0e68c")); namedColors.Add("lavender", GetColorFromString("#e6e6fa")); namedColors.Add("lavenderblush", GetColorFromString("#fff0f5")); namedColors.Add("lawngreen", GetColorFromString("#7cfc00")); namedColors.Add("lemonchiffon", GetColorFromString("#fffacd")); namedColors.Add("lightblue", GetColorFromString("#add8e6")); namedColors.Add("lightcoral", GetColorFromString("#f08080")); namedColors.Add("lightcyan", GetColorFromString("#e0ffff")); namedColors.Add("lightgoldenrodyellow", GetColorFromString("#fafad2")); namedColors.Add("lightgray", GetColorFromString("#ffd3d3d3")); namedColors.Add("lightgreen", GetColorFromString("#90ee90")); namedColors.Add("lightgrey", GetColorFromString("#d3d3d3")); namedColors.Add("lightpink", GetColorFromString("#ffb6c1")); namedColors.Add("lightsalmon", GetColorFromString("#ffa07a")); namedColors.Add("lightseagreen", GetColorFromString("#20b2aa")); namedColors.Add("lightskyblue", GetColorFromString("#87cefa")); namedColors.Add("lightslategray", GetColorFromString("#778899")); namedColors.Add("lightsteelblue", GetColorFromString("#b0c4de")); namedColors.Add("lightyellow", GetColorFromString("#ffffe0")); namedColors.Add("lime", GetColorFromString("#00ff00")); namedColors.Add("limegreen", GetColorFromString("#32cd32")); namedColors.Add("linen", GetColorFromString("#faf0e6")); namedColors.Add("magenta", GetColorFromString("#ff00ff")); namedColors.Add("maroon", GetColorFromString("#800000")); namedColors.Add("mediumaquamarine", GetColorFromString("#66cdaa")); namedColors.Add("mediumblue", GetColorFromString("#0000cd")); namedColors.Add("mediumorchid", GetColorFromString("#ba55d3")); namedColors.Add("mediumpurple", GetColorFromString("#9370db")); namedColors.Add("mediumseagreen", GetColorFromString("#3cb371")); namedColors.Add("mediumslateblue", GetColorFromString("#7b68ee")); namedColors.Add("mediumspringgreen", GetColorFromString("#00fa9a")); namedColors.Add("mediumturquoise", GetColorFromString("#48d1cc")); namedColors.Add("mediumvioletred", GetColorFromString("#c71585")); namedColors.Add("midnightblue", GetColorFromString("#191970")); namedColors.Add("mintcream", GetColorFromString("#f5fffa")); namedColors.Add("mistyrose", GetColorFromString("#ffe4e1")); namedColors.Add("moccasin", GetColorFromString("#ffe4b5")); namedColors.Add("navajowhite", GetColorFromString("#ffdead")); namedColors.Add("navy", GetColorFromString("#000080")); namedColors.Add("oldlace", GetColorFromString("#fdf5e6")); namedColors.Add("olive", GetColorFromString("#808000")); namedColors.Add("olivedrab", GetColorFromString("#6b8e23")); namedColors.Add("orange", GetColorFromString("#ffa500")); namedColors.Add("orangered", GetColorFromString("#ff4500")); namedColors.Add("orchid", GetColorFromString("#da70d6")); namedColors.Add("palegoldenrod", GetColorFromString("#eee8aa")); namedColors.Add("palegreen", GetColorFromString("#98fb98")); namedColors.Add("paleturquoise", GetColorFromString("#afeeee")); namedColors.Add("palevioletred", GetColorFromString("#db7093")); namedColors.Add("papayawhip", GetColorFromString("#ffefd5")); namedColors.Add("peachpuff", GetColorFromString("#ffdab9")); namedColors.Add("peru", GetColorFromString("#cd853f")); namedColors.Add("pink", GetColorFromString("#ffc0cb")); namedColors.Add("plum", GetColorFromString("#dda0dd")); namedColors.Add("powderblue", GetColorFromString("#b0e0e6")); namedColors.Add("purple", GetColorFromString("#800080")); namedColors.Add("red", GetColorFromString("#ff0000")); namedColors.Add("rosybrown", GetColorFromString("#bc8f8f")); namedColors.Add("royalblue", GetColorFromString("#4169e1")); namedColors.Add("saddlebrown", GetColorFromString("#8b4513")); namedColors.Add("salmon", GetColorFromString("#fa8072")); namedColors.Add("sandybrown", GetColorFromString("#f4a460")); namedColors.Add("seagreen", GetColorFromString("#2e8b57")); namedColors.Add("seashell", GetColorFromString("#fff5ee")); namedColors.Add("sienna", GetColorFromString("#a0522d")); namedColors.Add("silver", GetColorFromString("#c0c0c0")); namedColors.Add("skyblue", GetColorFromString("#87ceeb")); namedColors.Add("slateblue", GetColorFromString("#6a5acd")); namedColors.Add("slategray", GetColorFromString("#708090")); namedColors.Add("snow", GetColorFromString("#fffafa")); namedColors.Add("springgreen", GetColorFromString("#00ff7f")); namedColors.Add("steelblue", GetColorFromString("#4682b4")); namedColors.Add("tan", GetColorFromString("#d2b48c")); namedColors.Add("teal", GetColorFromString("#008080")); namedColors.Add("thistle", GetColorFromString("#d8bfd8")); namedColors.Add("tomato", GetColorFromString("#ff6347")); namedColors.Add("turquoise", GetColorFromString("#40e0d0")); namedColors.Add("violet", GetColorFromString("#ee82ee")); namedColors.Add("wheat", GetColorFromString("#f5deb3")); namedColors.Add("white", GetColorFromString("#ffffff")); namedColors.Add("whitesmoke", GetColorFromString("#f5f5f5")); namedColors.Add("yellow", GetColorFromString("#ffff00")); namedColors.Add("yellowgreen", GetColorFromString("#9acd32")); }
        public static Color ToColor(this string name)
        {
            Color ret;

            try {
                int hex = 0;
                if (int.TryParse(name.Replace("#", ""), out hex) == true)
                {
                    ret = GetColorFromString(name.Replace("#", ""));
                }
                else
                {
                        ret = namedColors[name.ToLower()];
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("색상명이 존재하지 않습니다 : " + name, ex);
            }

            return ret;
        }

        #endregion
        
        //연한색
        public static string[] ChartLightColors = { "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange",
                                                    "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange",
                                                    "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange",
                                                    "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange",
                                                    "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange",
                                                    "Tomato", "CornflowerBlue", "LightGreen", "Plum", "Yellow", "Peru", "Violet", "LightSteelBlue", "YellowGreen", "PapayaWhip", "LightSeaGreen", "LightYellow", "LightGray", "Khaki", "AquaMarine", "LightPink", "Lavender", "LightCoral", "LightGreen", "LightSalmon", "LightPink", "Olive", "PaleGoldenrod", "LightCyan", "IndianRed", "Aqua", "Beige", "BurlyWood", "CadetBlue", "Chocolate", "DarkOrange" };

        public static string[] ChartColors = {"0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                              ,
                                              "0000FF", "FF5500", "800080", "008000", "808000", "FFA500", "800000","00FF00", "FF00FF", "A52A2A", "808080", 
                                              "A5BC4E", "DAA520", "4169E1", "FF6347", "00CDCD", "00BFFF", "006400", "DEB887", "4B0082", "8B4513", "1E90FF", 
                                              "008B8B", "FFD700",  "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", 
                                              "C71585", "CD853F", "DC143C", "000080", "228B22", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", 
                                              "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "FFA07A", "BA55D3", 
                                              "CD5C5C", "EE82EE", "32CD32", "000000", "800000", "8B0000", "A0522D","8B4513","CD5C5C","BC8F8F","F08080","FA8072","E9967A",
                                              "32CD32","9ACD32","6B8E23","556B2F","228B22","006400", "2E8B57","3CB371","8FBC8F","6A5ACD","7B68EE","9370DB",
                                              "9932CC","9400D3","008080","008B8B","20B2AA", "66CDAA","5F9EA0","4682B4","DA70D6","8B008B","DB7093"
                                             };

        public static IList<KeyValue<string>> GetHourList()
        {
            string[] hours = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10"
                                   , "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"
                                   , "22", "23", "24" };

            IList<KeyValue<string>> ret = new List<KeyValue<string>>();
            foreach (string hr in hours)
            {
                KeyValue<string> item = new KeyValue<string>();
                item.Key = hr;
                item.Value = hr + "시";
                ret.Add(item);
            }
            return ret;
        }

        public static Color GetColorFromString(string col)
        {
            string col2 = col.Replace("#", "");
            string red = col2.Substring(0, 2); 
            string green = col2.Substring(2, 2);
            string blue = col2.Substring(4, 2);
            return new Color() { A=255, R = Convert.ToByte(red, 16), G = Convert.ToByte(green, 16), B = Convert.ToByte(blue, 16) };
        }

        public static IList<string> GetDamDataEditableColumns()
        {
            IList<string> ret = new List<string>();
            for (var i = 0; i < DamData_EditableColumns.Length; i++)
            {
                ret.Add(DamData_EditableColumns[i]);
            }
            
            return ret;
        }
        public static IList<string> GetDamDataEditableColumnsOfChecks()
        {
            IList<string> ret = new List<string>();
            for (var i = 0; i < DamData_EditableColumnOfChecks.Length; i++)
            {
                ret.Add(DamData_EditableColumnOfChecks[i]);
            }

            return ret;
        }
    }
}
