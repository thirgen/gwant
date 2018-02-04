using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

    new string name;
    List<Card> cards;
    bool exclusive;

    public string Name { get { return name; } private set { name = value; } }
    public List<Card> Cards { get { return cards; } private set { cards = value; } }
    public bool Exclusive { get { return exclusive; } private set { exclusive = value; } }

    public static GameObject CreateFaction(string Name, List<Card> Cards, bool Exclusive)
    {
        GameObject go = new GameObject(Name);
        Faction f = go.AddComponent<Faction>();
        f.Name = Name;
        f.Cards = Cards;
        f.Exclusive = Exclusive;
        return go;
    }
}
