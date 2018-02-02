using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        
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
