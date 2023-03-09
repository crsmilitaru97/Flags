using UnityEngine;

//11.11.2022

public class FZGameObject : MonoBehaviour
{
    public static GameObject CreateSprite(Sprite sprite, int order)
    {
        GameObject gameObject = new GameObject();
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = order;
        return gameObject;
    }
}
