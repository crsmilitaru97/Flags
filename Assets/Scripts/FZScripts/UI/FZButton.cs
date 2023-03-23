using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//23.03.2023

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
        if (FZAudio.Manager.clickSource != null)
        {
            if (playClickSound && interactable)
            {
                FZAudio.Manager.clickSource.Play();
            }
        }

        base.OnPointerClick(eventData);
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
