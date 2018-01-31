using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    public enum Types { Hand, Melee, Ranged, Siege, Weather, Deck, Discard }
    public enum IsVisibleTo { None, Player1, Player2, Both }
    public enum Restrictions { None, Melee, Ranged, Siege, Weather }

    GameObject zoneGO;
    Types type;
    List<Card> cards;
    Restrictions restriction;
    IsVisibleTo visibleTo;
    bool collapsed;

    public GameObject ZoneGO { get { return zoneGO; } set { zoneGO = value; } }
    public Types Type 
    {
        get { return type; }
        set
        {
            type = value;
            if (type == Types.Deck)
            {
                Restriction = Restrictions.None;
                VisibleTo = IsVisibleTo.None;
                IsCollapsed = true;
            }
        }
    }
    public List<Card> Cards { get { return cards; } private set { cards = value; } }
    public Restrictions Restriction { get { return restriction; } private set { restriction = value; } }
    public IsVisibleTo VisibleTo { get { return visibleTo; } private set { visibleTo = value; } }
    public bool IsCollapsed { get { return collapsed; } private set { collapsed = value; } }


    public void SetDeckCards(List<Card> c)
    {
        Cards = c;
    }
}
