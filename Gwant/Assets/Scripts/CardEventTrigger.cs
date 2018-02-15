using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardGO))]
public class CardEventTrigger : EventTrigger {

    Image border;
    Image image;
    Color defaultColour = new Color32(255, 255, 255, 0);
    Color highlightColour = new Color32(0, 255, 255, 255);
    Color selectedColour = new Color32(0, 128, 255, 255);

    private static CardEventTrigger selectedCard;
    bool selected = false;

    public Color DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public Color HighlightColour { get { return highlightColour; } set { highlightColour = value; } }
    public Color SelectedColour { get { return selectedColour; } set { selectedColour = value; } }

    public bool IsSelectedCard { get { if (selectedCard == this) return true; else return false; } }

    private void Start()
    {
        image = GetComponent<Image>();
        border = transform.GetChild(0).GetComponent<Image>();
        border.color = DefaultColour;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!IsSelectedCard)
            border.color = HighlightColour;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!IsSelectedCard)
            border.color = defaultColour;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (selectedCard == null)
            selectedCard = this;
        else if (IsSelectedCard)
            selectedCard = null;

        if (IsSelectedCard)
            border.color = SelectedColour;
        else
            border.color = HighlightColour;
    }
    
    

}
