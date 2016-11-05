using UnityEngine;
using System.Collections;

public class ColorConverter {
    public static Color convert(Colour colour) {
        Color color = new Color(colour.r, colour.g, colour.b, colour.a);
        return color;
    }
}
