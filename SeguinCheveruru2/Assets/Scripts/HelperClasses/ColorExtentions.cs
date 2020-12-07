using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ColorExtentions
{
    /// <summary>
    /// Modify a color's alpha, newAlpha must be between 0 and 1
    /// </summary>
    /// <param name="color"></param>
    /// <param name="newAlpha"></param>
    public static Color ModifyAlpha(this Color color, float newAlpha)
    {
        newAlpha = Mathf.Clamp(newAlpha, 0, 1);
        return new Color(color.r, color.g, color.b, newAlpha);
    }
}
