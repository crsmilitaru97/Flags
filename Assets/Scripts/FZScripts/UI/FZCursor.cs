using System;
using UnityEngine;
using UnityEngine.UI;

//18.11.2022

public class FZCursor : MonoBehaviour
{
    public CursorType cursorType;
    Image cursor;

    [Serializable]
    public class CursorType
    {
        public Sprite normal;
        public Sprite select;
        public Sprite click;
    }
    public static FZCursor Instance;

    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        cursor = GetComponent<Image>();
        ChangeCursor(cursorType.normal);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ChangeCursor(cursorType.click);

        cursor.transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 20, Input.mousePosition.z);
    }

    public void ChangeCursor(Sprite cursorType)
    {
        cursor.sprite = cursorType;
    }
}
