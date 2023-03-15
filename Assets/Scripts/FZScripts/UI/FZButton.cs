using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//08.12.2022

public class FZButton : Button, IPointerClickHandler
{
    public bool playClickSound = true;
    public Text buttonText;
    public Image buttonImage;
    public Color selectedColor = Color.white;
    public bool isSelected;

    Color defaultColor;

    //public bool IsSelected
    //{
    //    get => isSelected; set
    //    {
    //        if (value)
    //        {
    //            defaultColor = targetGraphic.color;
    //            targetGraphic.color = selectedColor;
    //        }
    //        else
    //        {
    //            targetGraphic.color = defaultColor;
    //            isSelected = value;
    //        }
    //    }
    //}

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (playClickSound && interactable)
        {
            FZAudio.Manager.clickSource?.Play();
        }
    }

    public void SelectButton()
    {
        isSelected = true;
        defaultColor = targetGraphic.color;
        targetGraphic.color = selectedColor;
    }

    public void UnselectButton()
    {
        isSelected = false;
        targetGraphic.color = defaultColor;
    }

    bool activation = false;
    public void ToggleActivateGroup(GameObject group)
    {
        activation = !activation;
        group.SetActive(activation);
    }
}
