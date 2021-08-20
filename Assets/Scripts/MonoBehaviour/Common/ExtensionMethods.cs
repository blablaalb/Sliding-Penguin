using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExtensionMethods
{
    public static bool InBetween(this float input, float min, float max, bool inlcusive = true)
    {
        if (inlcusive)
        {
            return input >= min && input <= max;
        }
        else
        {
            return input > min && input < max;
        }
    }
}
