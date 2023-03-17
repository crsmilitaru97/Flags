using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class LearnItem : MonoBehaviour
{
    public Text nameText;
    public Text abbrevText;
    public Image flagImage;

    void Start()
    {

    }

    public void Search()
    {
        Application.OpenURL("https://en.wikipedia.org/wiki/" + nameText.text);
    }

    public void Initiate(Flag flag)
    {
        transform.localScale = Vector3.one;
        nameText.text = flag.name;
        abbrevText.text = flag.abbrev;
        flagImage.sprite = flag.sprite;
    }
}
