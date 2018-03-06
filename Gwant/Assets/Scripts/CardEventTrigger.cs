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
    TextMeshProUGUI strengthText;
    bool highlighted;
    bool specialHighlighted;

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
    public static CardEventTrigger SelectedCard { get { return selectedCard; } }


    public TextMeshProUGUI StrengthText { get { return strengthText; } private set { strengthText = value; } }
    /// <summary>
    /// The Highlight used when a card is moused over in the hand
    /// </summary>
    public bool Highlighted { get { return highlighted; } private set { highlighted = value; } }
    /// <summary>
    /// The Highlight used when a card is highlighted by special card abilities, i.e. Decoy, 
    /// </summary>
    public bool SpecialHighlighted { get { return specialHighlighted; } private set { specialHighlighted = value; } }


    public bool IsSelectedCard { get { if (selectedCard == this) return true; else return false; } }

    private void Start()
    {

    }

    [SerializeField]
    private static Sprite[] StrengthImages;// = new Sprite[2];
    //public Sprite[] StrengthImages { get { return strengthImages; } private set { strengthImages = value; } }
    [SerializeField]
    private static Sprite[] AbilityImages;
    [SerializeField]
    private static Sprite[] ZoneImages;


    public void SetUpComponents()
    {
        if (StrengthImages == null)
            StrengthImages = Resources.LoadAll<Sprite>("Images/strength");

        Card card = GetComponent<CardGO>().Card;
        
        //set card art
        //GetComponent<Image>().sprite = null;

        //set ability image
        transform.GetChild(1).GetComponent<Image>().sprite = GetAbilitySprite(card.Ability);

        if (!card.Special)
        {
            //set strength image type
            Image strength = transform.GetChild(2).GetComponent<Image>();
            if (((UnitCard)card).Hero)
                strength.sprite = StrengthImages[1];
            else
                strength.sprite = StrengthImages[0];

            //set strength number
            StrengthText = strength.GetComponentInChildren<TextMeshProUGUI>();
            StrengthText.text = ((UnitCard)card).Strength.ToString();

            //set zone sprite
            transform.GetChild(3).GetComponent<Image>().sprite = GetZoneSprite(((UnitCard)card).Section);

        }
        Border = transform.GetChild(0).GetComponent<Image>();
        Highlighted = false;
        SpecialHighlighted = false;
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

    private Sprite GetZoneSprite(UnitCard.Sections section)
    {
        if (ZoneImages == null)
            ZoneImages = Resources.LoadAll<Sprite>("Images/zone");

        if (section == UnitCard.Sections.Melee)
            return ZoneImages[0];
        else if (section == UnitCard.Sections.Ranged)
            return ZoneImages[1];
        else if (section == UnitCard.Sections.Siege)
            return ZoneImages[2];
        else if (section == UnitCard.Sections.Agile)
            return ZoneImages[3];
        else return null;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!IsSelectedCard && GetComponent<CardGO>().InZone == Zone.Types.Hand && !Highlighted && !SpecialHighlighted)
            Highlight(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!IsSelectedCard && Highlighted)
        {
            UnHighlight(this);
            print("UNHIGHTLIGHTED: " + gameObject.name);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (Highlighted && eventData.button == PointerEventData.InputButton.Left)
        {

            //If no card is selected, select this one
            if (selectedCard == null)
                Select(this);
            //If this card is already selected, deselect it then unhighlight all selected zones
            else if (IsSelectedCard) 
                Select(null);
            //If another card (in the player's hand) is already selected,
            //deselect that one and select this one
            else
            {   
                Deselect(SelectedCard);
                Select(this);
            }

            if (IsSelectedCard) //card is being selected
            {
                Card c = GetComponent<CardGO>().Card;
                //print(GetComponent<CardGO>().InZone);
                

                //highlight zones depending on who's turn it is
                //if it's a spy, reverse
                //TODO 
                //add enum for P1, P2, and Both (for scorch special cards)
                bool highlightPlayerOne;
                if (!c.Special)
                {
                    if (((UnitCard)c).Ability == Card.Abilities.Spy)
                        highlightPlayerOne = (Manager.manager.PlayerOnesTurn) ? false : true;
                    else
                        highlightPlayerOne = (Manager.manager.PlayerOnesTurn) ? true : false;
                }
                else
                    highlightPlayerOne = (Manager.manager.PlayerOnesTurn) ? true : false;

                //Highlight relevant Zone(s) for Card c
                Manager.manager.HighlightNewZone(GetComponent<CardGO>().Card, highlightPlayerOne);
                UnHighlight(this);
            }
            else if (SpecialHighlighted)
            {
                if (SelectedCard.GetComponent<CardGO>().Card.Ability == Card.Abilities.Decoy)
                {
                    //play the selected card in this cards zone
                }
            }
            else //card is being deselected
                Highlight(this);

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
        Trigger.Highlighted = true;
    }

    public static void SpecialHighlight(CardEventTrigger Trigger)
    {
        Trigger.Border.color = HighlightColour;
        Trigger.SpecialHighlighted = true;
    }

    private static void UnHighlight(CardEventTrigger Trigger)
    {
        Trigger.Border.color = DefaultColour;
        Trigger.Highlighted = false;
    }

    private static void Select(CardEventTrigger Trigger)
    {
        if (Trigger != null)
            Trigger.border.color = SelectedColour;
        Manager.manager.UnHighlightZones();
        selectedCard = Trigger;
    }

    public static void Deselect(CardEventTrigger Trigger)
    {
        Trigger.border.color = DefaultColour;
        Select(null);
    }
    #endregion
}
