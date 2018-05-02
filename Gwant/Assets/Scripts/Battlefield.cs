using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

[AddComponentMenu("Gwant/Battlefield Zone")]
public class Battlefield : Zone
{
    //public enum Combats { Melee, Ranged, Siege }
    //Combats combat;
    //List<UnitCard> horns;
    [SerializeField]
    HornZone hornZone;
    bool weather;
    public int Morale;
    private int strength;
    private TextMeshProUGUI strengthText;
    

    //public Combats Combat { get { return combat; } set { combat = value; } }
    public HornZone ZoneHorn { get { return hornZone; } set { hornZone = value; } }
    public bool Weather { get { return weather; } set { weather = value; } }
    public int Strength { get { return strength; } private set { strength = value; } }
    //public List<UnitCard> Horns { get { return horns; } }

    new private void Start()
    {
        base.Start();
        //Cards = new List<CardGO>();
        //horns = new List<UnitCard>();
        strengthText = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
        //print("");
    }

    private void CalcStats()
    {
        Strength = 0;
        foreach (CardGO cardGO in Cards)
        {
            if (!cardGO.Card.Special)
            {
                ((UnitCard)cardGO.Card).CalcStats(this, cardGO);
                Strength += ((UnitCard)cardGO.Card).Strength;
            }
        }
        strengthText.text = Strength.ToString();
    }

    public void PlayCard(Card c)
    {

    }

    private void Reset()
    {
        hornZone = transform.parent.GetComponentInChildren<HornZone>();
    }

    bool alreadyCalled = false;
    public IEnumerator RecalcStatsAtEndOfFrame()
    {
        if (!alreadyCalled)
        {
            alreadyCalled = true;
            yield return new WaitForEndOfFrame();
            CalcStats();
            alreadyCalled = false;
        }
    }
}
