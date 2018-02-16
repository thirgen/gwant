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
    static Color defaultColour = new Color32(255, 255, 255, 0);
    static Color highlightColour = new Color32(0, 255, 255, 255);
    static Color selectedColour = new Color32(0, 128, 255, 255);

    private static CardEventTrigger selectedCard;

    public static Color DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public static Color HighlightColour { get { return highlightColour; } set { highlightColour = value; } }
    public static Color SelectedColour { get { return selectedColour; } set { selectedColour = value; } }
    public Image Border { get { return border; }
        set {
            border = value;
            border.color = DefaultColour;
        }
    }


    public bool IsSelectedCard { get { if (selectedCard == this) return true; else return false; } }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!IsSelectedCard)
            Highlight(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!IsSelectedCard)
            UnHighlight(this);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        //If inputbutton = left mouse button
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if card is in hand

            if (selectedCard == null) //If no card is selected, select this one
                selectedCard = this;
            else if (IsSelectedCard) //If this card is already selected, deselect it
                selectedCard = null;
            else //If another card (in the player's hand) is already selected,
            {    //deselt that one and select this one
                UnHighlight(selectedCard);
                selectedCard = this;
            }

            if (IsSelectedCard)
                Select(this);
            else
                Highlight(this);

            //Card c = GetComponent<CardGO>().Card;
            print(GetComponent<CardGO>().InZone);
            //Highlight relevant Zone(s) for Card c
            if (Manager.manager.PlayerOnesTurn) //ALSO REMEMBER TO REVERSE FOR SPY CARDS
            {
                //highlight zones in player one's half
            }
            else
            {
                //highlight zones in player two's half
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //display big version of card.
            //set examiningCard = true;
            //lock out highlighting/selecting cards
        }
    }


    #region Card Util
    private static void Highlight(CardEventTrigger Trigger)
    {
        Trigger.Border.color = HighlightColour;
    }

    private static void UnHighlight(CardEventTrigger Trigger)
    {
        Trigger.Border.color = DefaultColour;
    }

    private static void Select(CardEventTrigger Trigger)
    {
        Trigger.border.color = SelectedColour;
    }
    #endregion
}
