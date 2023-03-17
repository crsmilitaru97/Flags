using UnityEngine;
using UnityEngine.UI;

public class Learn : MonoBehaviour
{
    public ScrollRect scrollViewRect;
    public RectTransform euList;
    public RectTransform africaList;
    public RectTransform asiaList;
    public RectTransform oceaniaList;
    public RectTransform northAmericaList;
    public RectTransform southAmericaList;
    public RectTransform otherList;

    public FZButton[] tabButtons;

    public GameObject learnItemPrefab;

    void Start()
    {
        foreach (var flag in FlagsManager.Flags)
        {
            var item = Instantiate(learnItemPrefab);

            if (flag.country.isEU)
                item.transform.SetParent(euList);
            else if (flag.country.isAsia)
                item.transform.SetParent(asiaList);
            else if (flag.country.isAfrica)
                item.transform.SetParent(africaList);
            else if (flag.country.isOceania)
                item.transform.SetParent(oceaniaList);
            else if (flag.country.isNorthAmerica)
                item.transform.SetParent(northAmericaList);
            else if (flag.country.isSouthAmerica)
                item.transform.SetParent(southAmericaList);
            else
                item.transform.SetParent(otherList);

            item.GetComponent<LearnItem>().Initiate(flag);
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

    public void ChangeTab(int index)
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            tabButtons[i].interactable = true;
        }
        tabButtons[index].interactable = false;

        euList.gameObject.SetActive(index == 0);
        africaList.gameObject.SetActive(index == 1);
        asiaList.gameObject.SetActive(index == 2);
        oceaniaList.gameObject.SetActive(index == 3);
        northAmericaList.gameObject.SetActive(index == 4);
        southAmericaList.gameObject.SetActive(index == 5);
        otherList.gameObject.SetActive(index == 6);

        scrollViewRect.content = index == 0 ? euList : index == 1 ? africaList : index == 2 ? asiaList : index == 3 ? oceaniaList : index == 4 ? northAmericaList : index == 5 ? southAmericaList : otherList;
    }
}
