using System;
using UnityEngine;

//18.10.2022

public static class FZMath
{
    public static bool IsBetween(this int number, int first, int last)
    {
        return Math.Abs(number) >= Math.Abs(first) && Math.Abs(number) <= Math.Abs(last);
    }

    public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
