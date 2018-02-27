using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CardGO))]
public class CardEventTrigger : EventTrigger {

    Image border;
    Image image;

    Image strength;
    TextMeshProUGUI strengthText;
    Image zone;
    Image ability;

    static Color defaultColour = new Color32(255, 255, 255, 0);
    static Color highlightColour = new Color32(0, 255, 255, 255);
    static Color selectedColour = new Color32(0, 128, 255, 255);

    private static CardEventTrigger selectedCard;

    public static Color DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public static Color HighlightColour { get { return highlightColour; } set { highlightColour = value; } }
    public static Color SelectedColour { get { return selectedColour; } set { selectedColour = value; } }
    public Image Border { get { return border; }
        private set {
            border = value;
            border.color = DefaultColour;
        }
    }

    public Image Strength { get { return strength; } private set { strength = value; } }
    public Image Zone { get { return zone; } private set { zone = value; } }
    public Image Ability { get { return ability; } private set { ability = value; } }


    public bool IsSelectedCard { get { if (selectedCard == this) return true; else return false; } }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private static Sprite[] strengthImages;// = new Sprite[2];
    public Sprite[] StrengthImages { get { return strengthImages; } private set { strengthImages = value; } }
    private static Sprite[] AbilityImages;
    public void SetUpComponents()
    {
        if (strengthImages == null)
            StrengthImages = Resources.LoadAll<Sprite>("Images/strength");

        Card card = GetComponent<CardGO>().Card;
            Ability = transform.GetChild(1).GetComponent<Image>();
        if (!card.Special)
        {
            Strength = transform.GetChild(2).GetComponent<Image>();
            Zone = transform.GetChild(3).GetComponent<Image>();

            if (((UnitCard)card).Hero)
                Strength.sprite = strengthImages[1];
            else
                Strength.sprite = strengthImages[0];
        }
        Ability.sprite = GetAbilitySprite(card.Ability);
        Border = transform.GetChild(0).GetComponent<Image>();
    }

    private Sprite GetAbilitySprite(Card.Abilities ability)
    {
        if (AbilityImages == null)
            AbilityImages = Resources.LoadAll<Sprite>("Images/abilities/");

        if (ability == Card.Abilities.None)
            return null;
        else if ((int)ability < (int)Card.Abilities.SPECIAL_START)
            return AbilityImages[(int)ability - 1];
        else if ((int)ability < (int)Card.Abilities.UNIT_END)
            return AbilityImages[(int)ability - 2];
        else if ((int)ability < (int)Card.Abilities.Weather)
            return AbilityImages[(int)ability - 3];
        else
            return null;
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
