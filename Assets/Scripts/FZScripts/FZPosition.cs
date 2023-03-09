using UnityEngine;

public static class FZPosition
{
    public static Vector2 AddToX(this Vector2 pos, int x)
    {
        return new Vector2(pos.x + x, pos.y);
    }

    public static Vector2 AddToY(this Vector2 pos, int y)
    {
        return new Vector2(pos.x, pos.y + y);
    }

    public static Vector2 AddToXAndY(this Vector2 pos, int x, int y)
    {
        return new Vector2(pos.x + x, pos.y + y);
    }
}
