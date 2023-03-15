using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//02.11.2022

public class FZAddCreateMenuItems : Editor
{
    [MenuItem("GameObject/UI/FZ/Image")]
    private static void AddFZImage()
    {
        GameObject obj = new GameObject();
        obj.name = "Image";
        var img = obj.AddComponent<FZImage>();

        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        obj.transform.SetParent(canvas.transform);
        obj.transform.localScale = Vector3.one;
        img.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    [MenuItem("GameObject/UI/FZ/Text")]
    private static void AddFZText()
    {
        GameObject obj = new GameObject();
        obj.name = "Text";
        var txt = obj.AddComponent<FZText>();
        txt.text = "Text Field";

        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        obj.transform.SetParent(canvas.transform);
        obj.transform.localScale = Vector3.one;
        txt.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    [MenuItem("GameObject/UI/FZ/Button")]
    private static void AddFZButton()
    {
        GameObject obj = new GameObject();
        obj.name = "Button";
        var fzBtn = obj.AddComponent<FZButton>();
        var img = obj.AddComponent<Image>();
        fzBtn.buttonImage = img;

        GameObject obj2 = new GameObject();
        obj2.name = "Text";
        obj2.transform.parent = obj.transform;
        var txt = obj2.AddComponent<FZText>();
        txt.text = "Text Field";
        txt.fontSize = 24;
        fzBtn.buttonText = txt;

        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        obj.transform.SetParent(canvas.transform);
        obj.transform.localScale = Vector3.one;
        img.rectTransform.anchoredPosition = new Vector2(0, 0);

        txt.rectTransform.anchorMin = new Vector2(0, 0);
        txt.rectTransform.anchorMax = new Vector2(1, 1);
        Rect rectContainer = txt.rectTransform.rect;
        rectContainer.yMax = 0.5f;
        rectContainer.xMax = 0.5f;
        rectContainer.yMin = 0.5f;
        rectContainer.xMin = 0.5f;
    }

    [MenuItem("GameObject/UI/FZ/Progress Bar")]
    private static void AddFZProgressBar()
    {
        GameObject obj = new GameObject();
        obj.name = "Progress Bar";
        var img = obj.AddComponent<Image>();
        var pg = obj.AddComponent<FZProgressBar>();

        GameObject obj2 = new GameObject();
        obj2.name = "Fill";
        obj2.transform.parent = obj.transform;
        var img2 = obj2.AddComponent<Image>();
        img2.type = Image.Type.Filled;
        pg.fillImage = img2;

        GameObject obj3 = new GameObject();
        obj3.name = "Text";
        obj3.transform.parent = obj.transform;
        var txt = obj3.AddComponent<Text>();
        txt.text = "0/0";
        txt.fontSize = 24;
        pg.fillText = txt;
        txt.alignment = TextAnchor.MiddleCenter;

        Canvas canvas = (Canvas)FindObjectOfType(typeof(Canvas));
        obj.transform.SetParent(canvas.transform);
        obj.transform.localScale = Vector3.one;
        img.rectTransform.anchoredPosition = new Vector2(0, 0);

        img2.rectTransform.anchorMin = new Vector2(0, 0);
        img2.rectTransform.anchorMax = new Vector2(1, 1);
        Rect rectContainer = img2.rectTransform.rect;
        rectContainer.yMax = 0.5f;
        rectContainer.xMax = 0.5f;
        rectContainer.yMin = 0.5f;
        rectContainer.xMin = 0.5f;
    }
}
