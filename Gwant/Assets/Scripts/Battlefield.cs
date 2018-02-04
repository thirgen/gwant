using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Battlefield : Zone
{
    public enum Combats { Melee, Ranged, Siege }
    Combats combat;
    List<UnitCard> horns;
    List<UnitCard> cards;
    HornZone hornZone;
    bool weather;

    public Combats Combat { get { return combat; } set { combat = value; } }
    public HornZone ZoneHorn { get { return hornZone; } set { hornZone = value; } }
    public bool Weather { get { return weather; } set { weather = value; } }
    public List<UnitCard> Horns { get { return horns; } }
    public List<UnitCard> Cards { get { return cards; } }

    private void Start()
    {
        cards = new List<UnitCard>();
    }

    public void CalcStats()
    {
        foreach (UnitCard card in Cards)
            card.CalcStats(this);
    }

    public void PlayCard(Card c)
    {

    }
}
