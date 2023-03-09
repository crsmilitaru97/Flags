using UnityEngine;

//30.11.22

public class FZRandom : MonoBehaviour
{
    public static bool Should(float chanceFrom100)
    {
        return chanceFrom100 > Random.Range(0, 100);
    }

    public static int RandomBetween(params int[] choices)
    {
        return choices[Random.Range(0, choices.Length)];
    }
}
