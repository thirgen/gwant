using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Battlefield : Zone
{
    //public enum Combats { Melee, Ranged, Siege }
    //Combats combat;
    //List<UnitCard> horns;
    [SerializeField]
    HornZone hornZone;
    bool weather;
    public int Morale;
    

    //public Combats Combat { get { return combat; } set { combat = value; } }
    public HornZone ZoneHorn { get { return hornZone; } set { hornZone = value; } }
    public bool Weather { get { return weather; } set { weather = value; } }
    //public List<UnitCard> Horns { get { return horns; } }

    new private void Start()
    {
        base.Start();
        //Cards = new List<CardGO>();
        //horns = new List<UnitCard>();
    }

    public void CalcStats()
    {
        foreach (CardGO cardGO in Cards)
        {
            if (!cardGO.Card.Special)
                ((UnitCard)cardGO.Card).CalcStats(this, cardGO);
        }
    }

    public void PlayCard(Card c)
    {

    }

    private void Reset()
    {
        hornZone = transform.parent.GetComponentInChildren<HornZone>();
    }
}
