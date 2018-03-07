using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardEventTrigger))]
public class CardGO : MonoBehaviour {

    private static GameObject child;
    private Card card;
    private Button button;

    public Card Card { get { return card; } private set { card = value; } }

	// Use this for initialization
	void Start () {
    }

    public Zone Zone { get { return transform.parent.gameObject.GetComponent<Zone>(); } }
    public Zone.Types InZone { get { return Zone.Type; } }

    public void SetCard(Card Card)
    {
        this.Card = Card;
        name = Card.Name;

        //Initialise Gameobject
        
        GetComponent<CardEventTrigger>().SetUpComponents();

        /*
        if (child == null)
            child = Resources.Load<GameObject>("Prefabs/CardBorder");
        GameObject border = Instantiate(child);
        border.transform.SetParent(transform);
        border.name = "Border";
        RectTransform rr = border.GetComponent<RectTransform>();

        rr.offsetMax = Vector2.zero;
        rr.offsetMin = Vector2.zero;
        */
        //GetComponent<CardEventTrigger>().Border = transform.GetChild(0).GetComponent<Image>();
        //border.SetActive(false);
    }

    public void MoveTo(Zone NewZone, int PositionInZone = -1)
    {
        Zone.Cards.Remove(this);
        if (PositionInZone < 0 || PositionInZone > NewZone.Cards.Count)
            NewZone.Cards.Add(this);
        else
            NewZone.Cards.Insert(PositionInZone, this);

        transform.SetParent(NewZone.transform);
        if (!gameObject.activeSelf && Zone.Type != Zone.Types.Deck)
            gameObject.SetActive(true);

        if (NewZone.Type != Zone.Types.Deck && NewZone.Type != Zone.Types.Discard &&
            NewZone.Type != Zone.Types.Hand)
            ApplyEffects(NewZone);
        else
            ApplyEffects(NewZone, false);

        
        //card.ApplyEffects(targetZone);
    }

    public void ApplyEffects(Zone zone, bool apply = true)
    {
        if (zone.Type != Zone.Types.Hand && zone.Type != Zone.Types.Deck && zone.Type != Zone.Types.Discard)
        {
            if (Card.Special)
            {
                SpecialCard c = (SpecialCard)Card;
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
                Battlefield bf = (Battlefield)Zone;

                //None, Medic, Morale, Muster, Spy, Bond, Berserker, Horn, Scorch, Mushroom
                if (apply && Card.Ability == Card.Abilities.Medic)
                {
                    //display all graveyard cards
                }
                else if (Card.Ability == Card.Abilities.Morale)
                {
                    if (apply)
                        bf.Morale++; //+1 morale to zone
                    else
                        bf.Morale--;
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Muster)
                {
                    //select all muster cards from hand/deck

                    List<CardGO> cardsToMove = new List<CardGO>();
                    foreach (CardGO cardGO in Manager.manager.GetZone(Zone.Types.Hand).Cards)
                    {
                        if (!cardGO.Card.Special && ((UnitCard)cardGO.Card).Muster != null)
                        {
                            if (IsMusterCard(cardGO.Card, ((UnitCard)cardGO.Card).Muster))
                                cardsToMove.Add(cardGO);
                        }
                    }
                    foreach (CardGO cardGO in Manager.manager.GetZone(Zone.Types.Deck).Cards)
                    {
                        if (!cardGO.Card.Special && ((UnitCard)cardGO.Card).Muster != null)
                        {
                            if (IsMusterCard(cardGO.Card, ((UnitCard)cardGO.Card).Muster))
                                cardsToMove.Add(cardGO);
                        }
                    }

                    //play selected cards
                    while (cardsToMove.Count > 0)
                    {
                        cardsToMove[0].MoveTo(Manager.manager.GetZone(((UnitCard)cardsToMove[0].Card).GetZone));
                        cardsToMove.Remove(cardsToMove[0]);
                    }
                }
                else if (apply && Card.Ability == Card.Abilities.Spy)
                {
                    //draw card from deck
                    ((Deck)Manager.manager.GetZone(Zone.Types.Deck)).DrawCard(Manager.manager.GetZone(Zone.Types.Hand));
                }
                else if (Card.Ability == Card.Abilities.Bond)
                {
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Berserker)
                {
                    //if mushroom in zone, do berseker thing
                }
                else if (Card.Ability == Card.Abilities.Horn)
                {
                    //add card to HornZorn horn units
                    if (apply)
                        bf.ZoneHorn.UnitHorns.Add(this);
                    else
                        bf.ZoneHorn.UnitHorns.Remove(this);
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Scorch)
                {
                    //destroy strongest card(s) in zone
                }
                else if (apply && Card.Ability == Card.Abilities.Mushroom)
                {
                    //trigger berserker stuff
                }
            }
        }
    }

    public void Discard()
    {
        MoveTo(Manager.manager.GetZone(Zone.Types.Discard));
    }

    private bool IsMusterCard(Card Card, string MusterName)
    {
        if (Card.Name.Contains(MusterName))
            return true;
        return false;
    }
}
