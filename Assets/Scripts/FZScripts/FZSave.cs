using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//30.10.22

public static class FZSave
{
    public class Constants
    {
        public class Options
        {
            public const string Music = "Music";
            public const string Sound = "Sound";
        }
        public const string Terrain = "Terrain";
        public const string Coins = "Coins";
        public const string Level = "Level";
        public const string IsFirstPlay = "IsFirstPlay";
        public const string Items = "Items";
        public const string HasSave = "HasSave";
    }

    public static void Delete(string name)
    {
        PlayerPrefs.DeleteKey(name);
    }


    public class Bool
    {
        public static bool Get(string title, bool defaultValue)
        {
            int val = 0;
            if (defaultValue)
                val = 1;

            var value = PlayerPrefs.GetInt(title, val);

            if (value == 1)
                return true;
            else
                return false;
        }

        public static void Set(string title, bool value)
        {
            if (value)
                PlayerPrefs.SetInt(title, 1);
            else
                PlayerPrefs.SetInt(title, 0);
        }
    }

    public class Int
    {
        public static int Get(string title, int defaultValue)
        {
            return PlayerPrefs.GetInt(title, defaultValue);
        }

        public static void Set(string title, int value)
        {
            PlayerPrefs.SetInt(title, value);
        }

        public static void SetArray(string title, int[] array)
        {
            PlayerPrefs.SetInt(title + "Size", array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                PlayerPrefs.SetInt(title + i.ToString(), array[i]);
            }
        }

        public static void SetList(string title, List<int> list)
        {
            PlayerPrefs.SetInt(title + "Size", list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                PlayerPrefs.SetInt(title + i.ToString(), list[i]);
            }
        }

        public static int[] GetArray(string title)
        {
            var size = PlayerPrefs.GetInt(title + "Size", 0);
            List<int> save = new List<int>();
            for (int i = 0; i < size; i++)
            {
                save.Add(PlayerPrefs.GetInt(title + i.ToString(), 0));
            }
            return save.ToArray();
        }

        public static List<int> GetList(string title)
        {
            var size = PlayerPrefs.GetInt(title + "Size", 0);
            List<int> save = new List<int>();
            for (int i = 0; i < size; i++)
            {
                save.Add(PlayerPrefs.GetInt(title + i.ToString(), 0));
            }
            return save;
        }

        public static int GetInList(string title, int index)
        {
            return PlayerPrefs.GetInt(title + index, 0);
        }

        public static int GetToText(string name, Text text, int defaultValue = 0)
        {
            var value = PlayerPrefs.GetInt(name, defaultValue);
            text.text = value.ToString();

            return value;
        }
    }

    public class String
    {
        public static string Get(string title, string defaultValue)
        {
            return PlayerPrefs.GetString(title, defaultValue);
        }

        public static void Set(string title, string value)
        {
            PlayerPrefs.SetString(title, value);
        }

        public static void SetList(string title, List<string> list)
        {
            PlayerPrefs.SetInt(title + "Size", list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                PlayerPrefs.SetString(title + i.ToString(), list[i]);
            }
        }

        public static List<string> GetList(string title)
        {
            var size = PlayerPrefs.GetInt(title + "Size", 0);
            List<string> save = new List<string>();
            for (int i = 0; i < size; i++)
            {
                save.Add(PlayerPrefs.GetString(title + i.ToString(), string.Empty));
            }
            return save;
        }

        public static string GetToText(string name, Text text, string defaultValue = "")
        {
            var value = PlayerPrefs.GetString(name, defaultValue);
            text.text = value;

            return value;
        }
    }

    public class Float
    {
        public static float Get(string title, float defaultValue)
        {
            return PlayerPrefs.GetFloat(title, defaultValue);
        }

        public static void Set(string title, float value)
        {
            PlayerPrefs.SetFloat(title, value);
        }
    }

    public class Vectors
    {
        public static void SetVector2Array(string title, Vector2[] value)
        {
            PlayerPrefsX.SetVector2Array(title, value);
        }
        public static Vector2[] GetVector2Array(string title)
        {
            return PlayerPrefsX.GetVector2Array(title);
        }

        public static void SetVector3Array(string title, Vector3[] value)
        {
            PlayerPrefsX.SetVector3Array(title, value);
        }
        public static Vector3[] GetVector3Array(string title)
        {
            return PlayerPrefsX.GetVector3Array(title);
        }
    }

    public class TimeDate
    {
        public static void Set(string saveName, System.DateTime dateTime)
        {
            PlayerPrefs.SetString(saveName, dateTime.ToString());
        }

        public static System.DateTime Get(string saveName)
        {
            string date = PlayerPrefs.GetString(saveName, System.DateTime.Now.Date.ToString());
            return System.DateTime.Parse(date);
        }
    }

    public class Dictionary
    {
        public static void SetVector2StringPair(string saveName, Dictionary<Vector2, string> valuePairs)
        {
            PlayerPrefsX.SetVector2Array(saveName + "_Keys", valuePairs.Keys.ToArray());
            PlayerPrefsX.SetStringArray(saveName + "_Values", valuePairs.Values.ToArray());
        }
        public static Dictionary<Vector2, string> GetVector2StringPair(string saveName)
        {
            var result = new Dictionary<Vector2, string>();
            var pos = PlayerPrefsX.GetVector2Array(saveName + "_Keys");
            var value = PlayerPrefsX.GetStringArray(saveName + "_Values");

            for (int i = 0; i < pos.Length; i++)
            {
                result.Add(pos[i], value[i]);
            }

            return result;
        }

        public static void SetVector2IntPair(string saveName, Dictionary<Vector2, int> valuePairs)
        {
            PlayerPrefsX.SetVector2Array(saveName + "_Keys", valuePairs.Keys.ToArray());
            PlayerPrefsX.SetIntArray(saveName + "_Values", valuePairs.Values.ToArray());
        }
        public static Dictionary<Vector2, int> GetVector2IntPair(string saveName)
        {
            var result = new Dictionary<Vector2, int>();
            var pos = PlayerPrefsX.GetVector2Array(saveName + "_Keys");
            var value = PlayerPrefsX.GetIntArray(saveName + "_Values");

            for (int i = 0; i < pos.Length; i++)
            {
                result.Add(pos[i], value[i]);
            }

            return result;
        }

        public static void SetStringIntPair(string saveName, Dictionary<string, int> valuePairs)
        {
            PlayerPrefsX.SetStringArray(saveName + "_Keys", valuePairs.Keys.ToArray());
            PlayerPrefsX.SetIntArray(saveName + "_Values", valuePairs.Values.ToArray());
        }
        public static Dictionary<string, int> GetStringIntPair(string saveName)
        {
            var result = new Dictionary<string, int>();
            var name = PlayerPrefsX.GetStringArray(saveName + "_Keys");
            var value = PlayerPrefsX.GetIntArray(saveName + "_Values");

            for (int i = 0; i < name.Length; i++)
            {
                result.Add(name[i], value[i]);
            }

            return result;
        }
    }
}
