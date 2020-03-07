using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensionMethods
{
    public static string ToPercentageString (this float number)
    {
        return (number * 100).ToString("0.##\\%");
    }
}
