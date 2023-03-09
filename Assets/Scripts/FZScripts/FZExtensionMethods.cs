using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//09.12.2020

public static class FZExtensionMethods
{
    #region List
    public static T RandomItem<T>(this List<T> list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Count)] : default;
    }

    public static int RandomItemIndex<T>(this List<T> list)
    {
        return list.Count() > 0 ? Random.Range(0, list.Count) : default;
    }

    public static T RandomItem<T>(this T[] list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Length)] : default;
    }

    public static T RandomUniqueItem<T>(this List<T> list, List<int> usedIndexes)
    {
        var notUsedList = new List<int>();

        for (int i = 0; i < list.Count(); i++)
        {
            if (!usedIndexes.Contains(i))
                notUsedList.Add(i);
        }

        int newIndex = notUsedList.RandomItemIndex();
        usedIndexes.Add(newIndex);

        return list.Count() > 0 ? list[newIndex] : default;
    }
    #endregion

    public static void ReEnable(this GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}