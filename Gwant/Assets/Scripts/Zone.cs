using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
[RequireComponent(typeof(ZoneEventTrigger))]
public class Zone : MonoBehaviour {

    public enum Types { NOT_SET_YET, Hand, Melee, Ranged, Siege, Weather, Deck, Discard, Horn }
    public enum IsVisibleTo { None, Player1, Player2, Both }

    #region Fields
    //GameObject zoneGO;
    Types type;
    List<CardGO> cards;
    IsVisibleTo visibleTo;
    bool collapsed;
    bool highlighted = false;
    ZoneEventTrigger trigger;
    public const int MAX_CARDS_HAND = 10;
    public const int MAX_CARDS_BATTLEFIELD = 9;
    [SerializeField]
    private const int CARD_OVERLAP_AMOUNT = -4;
    #endregion

    #region Properties
    //public GameObject ZoneGO { get { return zoneGO; } set { zoneGO = value; } }

    public Types Type 
    {
        get { return type; }
        set
        {
            type = value;
            if (type == Types.Deck)
            {
            }
            else if (type == Types.Discard)
                IsCollapsed = true;
            else
                IsCollapsed = false;
        }
    }
    public List<CardGO> Cards { get { return cards; } protected set { cards = value; } }
    public IsVisibleTo VisibleTo { get { return visibleTo; } set { visibleTo = value; } }
    public bool IsCollapsed { get { return collapsed; } protected set { collapsed = value; } }
    //public bool Highlighted { get { return highlighted; } protected set { highlighted = value; } }
    public bool Highlighted { get { return trigger.Highlighted; } protected set { trigger.Highlighted = value; } }
    #endregion

    protected void Start()
    {
        //Type = Types.Deck;
        trigger = GetComponent<ZoneEventTrigger>();
        Cards = new List<CardGO>();
    }

    #region Methods
    public float RecalcSpacing()
    {
        //-4, -7, -9.5, -11.75
        /*
         
        14x = -9
        20x = -17
        59x = -30
        //added overlap gets smaller the more cards there are
        //ranges from -4 to 0
        */
        if (Type == Types.Hand && Cards.Count > MAX_CARDS_HAND)
        {
            float width = 40 * MAX_CARDS_HAND;//GetComponent<RectTransform>().rect.width;
            float cardAlloc = width / (Cards.Count);
            return cardAlloc - 35.66666f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
        }
        else if ((Type == Types.Melee || Type == Types.Ranged ||
            Type == Types.Siege) && Cards.Count > MAX_CARDS_BATTLEFIELD)
        {
            float width = 40 * MAX_CARDS_BATTLEFIELD;//GetComponent<RectTransform>().rect.width;
            float cardAlloc = width / (Cards.Count);
            return cardAlloc - 40f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
        }
        return 0;
    }

    public void SetDeckCards(List<Card> c)
    {
        //Cards = c;
    }

    public void Highlight()
    {
        //GetComponent<Button>().interactable = true;
        Highlighted = true;
    }

    public void UnHighlight()
    {
        //GetComponent<Button>().interactable = false;
        Highlighted = false;
    }

    public void HighlightZone(Card c)
    {
        if (c.Special)
        {
            SpecialCard card = (SpecialCard)c;
            if (card.WeatherType != SpecialCard.WeatherTypes.None)
            {
                //Highlight weather zone;
            }
            else if (card.Ability == Card.Abilities.Horn)
            {
                //Highlight horn zones;
            }
            else //if ability = Scorch, Mushroom, or Decoy
            {
                //Highlight all battlefield zones;
            }
        }
        else
        {
            UnitCard card = (UnitCard)c;
            if (card.Section == UnitCard.Sections.Agile)
            {
                //Highlights melee;
                //Highlight ranged;
            }
            else if (card.Section == UnitCard.Sections.Melee)
            {
                //Highlight melee;
            }
            else if (card.Section == UnitCard.Sections.Ranged)
            {
                //Highlight ranged;
            }
            else if (card.Section == UnitCard.Sections.Siege)
            {
                //Highlight Siege;
            }
        }
    }

    #endregion

    #region Children
    public class Hand : Zone
    {

    }
    public class WeatherZone : Zone
    {
        List<SpecialCard> cards;
        Card frost;
        Card fog;
        Card rain;
        Card storm;

        public ReadOnlyCollection<SpecialCard> Cards { get { return cards.AsReadOnly(); } }

        public Card Forst { get { return frost; } }
        public Card Fog { get { return fog; } }
        public Card Rain { get { return rain; } }
        public Card Storm { get { return storm; } }

        public void AddWeather(SpecialCard c)
        {
            if (c.WeatherType == SpecialCard.WeatherTypes.Frost)
            {
                frost = c;
            }
            else if (c.WeatherType == SpecialCard.WeatherTypes.Fog)
            {
                fog = c;
            }
            else if (c.WeatherType == SpecialCard.WeatherTypes.Rain)
            {
                rain = c;
            }
            else if (c.WeatherType == SpecialCard.WeatherTypes.Storm)
            {
                storm = c;
            }
            else if (c.WeatherType == SpecialCard.WeatherTypes.Clear)
            {
                ClearWeather();
            }
            else
            {
                //do nothing

            }
        }

        private void ClearWeather()
        {
            frost = null;
            fog = null;
            rain = null;
            storm = null;
        }

    }
    #endregion
}
