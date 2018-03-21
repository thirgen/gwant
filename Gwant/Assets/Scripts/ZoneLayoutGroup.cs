using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Gwant/Zone Layout Group")]
[RequireComponent(typeof(Zone))]
public class ZoneLayoutGroup : HorizontalLayoutGroup {
    [SerializeField]
    Zone zone;
    [SerializeField]
    int newCardPosition;
    [SerializeField]
    int childCount;

    protected override void Start()
    {
        //if (zone.Type == Zone.Types.Deck || zone.Type == Zone.Types.Discard)
        //spacing = -40;
        newCardPosition = -1;
        childCount = transform.childCount;
    }

    protected override void OnTransformChildrenChanged()
    {
        base.OnTransformChildrenChanged();
        
        //print("New Card Position: " + newCardPosition);

        //Check if a card was added to the Zone
        bool hasNewChild = false;
        if (transform.childCount > childCount)
            hasNewChild = true;
        childCount = transform.childCount;

        //CardGO cardGO = (newCardPosition == -1) ? zone.Cards[zone.Cards.Count - 1] : zone.Cards[newCardPosition];
        CardGO cardGO = null;
        if (hasNewChild) //If a card was added, get that card
        {
            if (newCardPosition == -1)
            {
                if (zone.Cards.Count >= 1)
                    cardGO = zone.Cards[zone.Cards.Count - 1];
                else
                    cardGO = zone.Cards[0];
            }
            else
                cardGO = zone.Cards[newCardPosition];
            print("New Card: " + cardGO + " in Zone: " + zone);
        }
        if (zone.Type == Zone.Types.Hand)
        {
            if (zone.Cards.Count > Zone.MAX_CARDS_HAND)
            {
                float width = 40 * Zone.MAX_CARDS_HAND;//GetComponent<RectTransform>().rect.width;
                float cardAlloc = width / (zone.Cards.Count);
                spacing = cardAlloc - 35.66666f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
            }
            //
            if (hasNewChild)
                cardGO.ApplyEffects(zone, false);
        }
        else if (zone.Type == Zone.Types.Melee || zone.Type == Zone.Types.Ranged ||
            zone.Type == Zone.Types.Siege)
        {
            if (zone.Cards.Count > Zone.MAX_CARDS_BATTLEFIELD)
            {
                float width = 40 * Zone.MAX_CARDS_BATTLEFIELD;//GetComponent<RectTransform>().rect.width;
                float cardAlloc = width / (zone.Cards.Count);
                spacing = cardAlloc - 40f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
            }
            if (hasNewChild)
                ApplyAllCardEffects();
            //if (ability != Card.Abilities.Avenger)
            //Trigger card ability
        }
        else if (zone.Type == Zone.Types.Discard)
        {
            //trigger Avenger ability
            CardGO go = zone.Cards[zone.Cards.Count - 1];
            if (go.Card.Ability == Card.Abilities.Avenger)
            {
                print("Avenger on " + go.Card.Name);
                
            }
        }
        
        newCardPosition = -1;
    }

    public void CardAdded(int index) { newCardPosition = index; }

    private void ApplyAllCardEffects(bool ApplyEffects = true)
    {
        Zone z =  (zone.Type == Zone.Types.Horn)? ((HornZone)zone).Battlefield : zone;
        for (int i = 0; i < z.Cards.Count; i++)
            z.Cards[i].ApplyEffects(z, ApplyEffects);
    }

    protected override void Reset()
    {
        childAlignment = TextAnchor.MiddleCenter;
        childControlHeight = false;
        childControlWidth = false;
        childForceExpandWidth = false;
        childForceExpandHeight = true;
        zone = GetComponent<Zone>();
    }
}
