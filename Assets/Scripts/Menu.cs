using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject moreTile;
    public GameObject optionsTile;
    public GameObject statsTile;

    public static bool isMoreUP = false;
    public static bool isOptionsShown = false;

    void Start()
    {

    }

    public void StartGame()
    {
        FZCanvas.Instance.FadeLoadSceneAsync(Constants.Scenes.Game);
    }

    public void More()
    {
        isMoreUP = !isMoreUP;
        moreTile.GetComponent<Animator>().SetBool("isMoreUP", isMoreUP);
    }

    public void Options()
    {
        isOptionsShown = !isOptionsShown;
        moreTile.GetComponent<Animator>().SetBool("isOptionsShown", isOptionsShown);
    }

    public void ShowMenuWithAnim(GameObject tile)
    {
        tile.GetComponent<Animator>().SetBool("shown", true);
    }

    public void HideMenuWithAnim(GameObject tile)
    {
        tile.gameObject.GetComponent<Animator>().SetBool("shown", false);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
