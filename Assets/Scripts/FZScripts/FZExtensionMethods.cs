using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//16.03.2023

public static class FZExtensionMethods
{
    #region List
    public static T RandomItem<T>(this List<T> list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Count)] : default;
    }

    public static int RandomIndex<T>(this T[] list)
    {
        return list.Count() > 0 ? Random.Range(0, list.Length) : 0;
    }

    public static T RandomUniqueItem<T>(this List<T> list, List<T> usedValues)
    {
        List<T> tempList = new List<T>();
        foreach (var item in list)
        {
            tempList.Add(item);
        }
        foreach (var value in usedValues)
        {
            tempList.Remove(value);
        }

        var newValue = tempList.RandomItem();
        usedValues.Add(newValue);

        return list.Count() > 0 ? newValue : default;
    }
    #endregion

    #region Array
    public static T RandomItem<T>(this T[] list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Length)] : default;
    }

    public static int RandomIndex<T>(this List<T> list)
    {
        return list.Count() > 0 ? Random.Range(0, list.Count) : 0;
    }
    #endregion

    public static void ReEnable(this GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}