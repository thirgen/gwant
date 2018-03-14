using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Gwant/Zone Layout Group")]
[RequireComponent(typeof(Zone))]
public class ZoneLayoutGroup : HorizontalLayoutGroup {

    Zone zone;

    protected override void OnTransformChildrenChanged()
    {
        base.OnTransformChildrenChanged();

        if (zone.Type == Zone.Types.Hand && zone.Cards.Count > Zone.MAX_CARDS_HAND)
        {
            float width = 40 * Zone.MAX_CARDS_HAND;//GetComponent<RectTransform>().rect.width;
            float cardAlloc = width / (zone.Cards.Count);
            spacing = cardAlloc - 35.66666f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
        }
        else if ((zone.Type == Zone.Types.Melee || zone.Type == Zone.Types.Ranged ||
            zone.Type == Zone.Types.Siege) && zone.Cards.Count > Zone.MAX_CARDS_BATTLEFIELD)
        {
            float width = 40 * Zone.MAX_CARDS_BATTLEFIELD;//GetComponent<RectTransform>().rect.width;
            float cardAlloc = width / (zone.Cards.Count);
            spacing = cardAlloc - 40f;// + ((Cards.Count - MAX_CARDS_HAND) * 0.25f);
        }
        else
            spacing = 0;
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
