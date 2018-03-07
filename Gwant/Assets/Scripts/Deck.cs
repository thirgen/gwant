using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : Zone
{
    //new string name;
    //Faction faction;
    //List<CardGO> cards;

    //public List<CardGO> Cards { get { return cards; } private set { cards = value; } }

    private void Start()
    {
        Type = Types.Deck;
        IsCollapsed = true;
    }

    public void AddCard(CardGO cardGO)
    {
        if (Cards == null)
            Cards = new List<CardGO>();
        Cards.Add(cardGO);
        //cardGO.transform.SetParent(transform);
    }

    public void DrawCard(Zone hand)
    {
        Cards[0].MoveTo(hand);
        hand.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>().spacing = hand.RecalcSpacing();
    }
}
