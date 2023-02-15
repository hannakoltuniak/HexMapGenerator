using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class HexTypeHelper
    {
        //Colors from Adobe Colors, i know these are not exactly those specified in task but I found them more pleasing to eye 
        public static Color GetColorFromHexType(HexType type)
        {
            if (type == HexType.Blue)
            {
                Color newCol;
                ColorUtility.TryParseHtmlString("#4064F3", out newCol);
                return newCol;
            }
            else if (type == HexType.Green)
            {
                Color newCol;
                ColorUtility.TryParseHtmlString("#6CC50E", out newCol);
                return newCol;
            }
            else if (type == HexType.Grey)
            {
                Color newCol;
                ColorUtility.TryParseHtmlString("#CFCBC7", out newCol);
                return newCol;

            }
            else if (type == HexType.Yellow)
            {
                Color newCol;
                ColorUtility.TryParseHtmlString("#FBBB0A", out newCol);
                return newCol;
            }
            else return Color.white;
        }

    }
}
