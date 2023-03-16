using UnityEngine;
using UnityEngine.UI;

public class Learn : MonoBehaviour
{
    public Transform list;
    public GameObject learnItemPrefab;

    void Start()
    {
        foreach (var flag in FlagsManager.Manager.Flags)
        {
            var item = Instantiate(learnItemPrefab, list);
            item.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = flag.name;
            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = flag.sprite;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void BackToMenu()
    {
        FZCanvas.Instance.FadeLoadSceneAsync(Constants.Scenes.Menu);
    }
}
