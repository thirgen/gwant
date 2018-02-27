using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class Zone : MonoBehaviour {

    public enum Types { NOT_SET_YET, Hand, Melee, Ranged, Siege, Weather, Deck, Discard, Horn }
    public enum IsVisibleTo { None, Player1, Player2, Both }

    #region Fields
    //GameObject zoneGO;
    Types type;
    List<CardGO> cards;
    IsVisibleTo visibleTo;
    bool collapsed;
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
    #endregion

    private void Start()
    {
        //Type = Types.Deck;
    }

    #region Methods
    public void SetDeckCards(List<Card> c)
    {
        //Cards = c;
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

    public void MoveCardTo(CardGO cardGO, Zone zone)
    {
        //Zone targetZone;
        cardGO.transform.parent.GetComponent<Zone>().Cards.Remove(cardGO);
        cardGO.transform.SetParent(zone.transform);
        if (!cardGO.gameObject.activeSelf && zone.Type != Types.Deck)
            cardGO.gameObject.SetActive(true);

        Card card = cardGO.Card;
        if (card.Special)
        {
            SpecialCard c = (SpecialCard)card;
            if (c.WeatherType != SpecialCard.WeatherTypes.None)
            {
                if (c.WeatherType == SpecialCard.WeatherTypes.Frost)
                    c.WeatherEffect(zone); //Melee row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Fog)
                    c.WeatherEffect(zone); //Ranged row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Rain)
                    c.WeatherEffect(zone); //Siege row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Storm)
                {
                    c.WeatherEffect(zone); //Ranged row
                    c.WeatherEffect(zone); //Siege row
                }
                else //if (c.WeatherType == SpecialCard.WeatherTypes.Clear)
                    c.WeatherEffect(zone); //Clear Weather row
            }
            else
            {
                //Horn, Scorch, Mushrom, Decoy stuff
            }
        }
        else
        {
            //None, Medic, Morale, Muster, Spy, Bond, Berserker, Horn, Scorch, Mushroom

        }
        //card.ApplyEffects(targetZone);
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
