using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//08.12.2020

public static class FZExtensionMethods
{
    #region List
    public static T RandomItem<T>(this List<T> list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Count)] : default;
    }

    public static T RandomItem<T>(this T[] list)
    {
        return list.Count() > 0 ? list[Random.Range(0, list.Length)] : default;
    }

    public static T RandomUniqueItem<T>(this List<T> list, List<int> usedIndexes)
    {
        //foreach (int index in usedIndexes)
        //{
        //    list.RemoveAt(index);
        //}

        int newIndex = Random.Range(0, list.Count());
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