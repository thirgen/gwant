using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    public enum Types { Hand, Melee, Ranged, Siege, Weather, Deck, Discard }
    public enum IsVisibleTo { None, Player1, Player2, Both }

    #region Fields
    //GameObject zoneGO;
    Types type;
    List<Card> cards;
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
                //VisibleTo = IsVisibleTo.None;
                //IsCollapsed = true;


                if (!gameObject.GetComponent<Deck>())
                {
                    Deck d = gameObject.AddComponent<Deck>();
                    Destroy(gameObject.GetComponent<Zone>());
                    d.VisibleTo = IsVisibleTo.None;
                    d.IsCollapsed = true;
                    d.Cards = new List<Card>();
                }
            }
        }
    }
    public List<Card> Cards { get { return cards; } private set { cards = value; } }
    public IsVisibleTo VisibleTo { get { return visibleTo; } private set { visibleTo = value; } }
    public bool IsCollapsed { get { return collapsed; } private set { collapsed = value; } }
    #endregion

    private void Start()
    {
        Type = Types.Deck;
    }

    #region Methods
    public void SetDeckCards(List<Card> c)
    {
        Cards = c;
    }

    public void MoveCardTo(Card card, Zone zone)
    {
        //Zone targetZone;
        if (card.Special)
        {
            SpecialCard c = (SpecialCard)card;
            if (c.Weather)
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
            //None, Agile, Medic, Morale, Muster, Spy, Bond, Berserker, Horn, Scorch

        }
        //card.ApplyEffects(targetZone);
    }

    #endregion

    #region Children
    public class Hand : Zone
    {

    }
    public class Deck : Zone
    {

    }
    public class Discard : Zone
    {

    }
    public class WeatherZone : Zone
    {

    }
    public class HornZone : Zone
    {
        bool horn;
        public bool Horn { get { return horn; } set { horn = value; } }
    }
    public class Battlefield : Zone
    {
        public enum Combats { Melee, Ranged, Siege }
        Combats combat;
        List<UnitCard> horns;
        HornZone hornZone;
        bool weather;

        public Combats Combat { get { return combat; } set { combat = value; } }
        public HornZone ZoneHorn { get { return hornZone; } set { hornZone = value; } } 
        public bool Weather { get { return weather; } set { weather = value; } }
        public List<UnitCard> Horns { get { return horns; } }


        public void CalcStats()
        {
            foreach (UnitCard card in Cards)
                card.CalcStats(this);
        }
    }
    #endregion
}
