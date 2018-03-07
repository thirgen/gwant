using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ZoneEventTrigger : MonoBehaviour, IPointerClickHandler {
    
    static Color32 defaultColour = new Color32(255, 255, 255, 255);
    static Color32 highlightColour = new Color32(0, 128, 255, 255);

    public Color32 DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public Color32 HighlightColour { get { return highlightColour; } set { highlightColour = value; } }

    public Image image;
    bool highlighted;
    public bool Highlighted { get { return highlighted; }
        set
        {
            if (value)
                image.color = HighlightColour;
            else
                image.color = DefaultColour;
            highlighted = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Highlighted)
        {
            print("CLICK: " + gameObject + " CARD: " + CardEventTrigger.SelectedCard);
            //play selected card
            CardGO cardGO = CardEventTrigger.SelectedCard.GetComponent<CardGO>();

            if (cardGO.Card.Ability == Card.Abilities.Scorch)
            {
                if (cardGO.Card.Special)
                {
                    //scorch highest str card(s)
                    Zone[] zones = new Zone[] {
                        Manager.manager.GetZone(Zone.Types.Melee),
                        Manager.manager.GetZone(Zone.Types.Ranged),
                        Manager.manager.GetZone(Zone.Types.Siege),
                        Manager.manager.GetZone(Zone.Types.Melee, false),
                        Manager.manager.GetZone(Zone.Types.Ranged, false),
                        Manager.manager.GetZone(Zone.Types.Siege, false)
                    };
                    List<CardGO> strongest = new List<CardGO>();
                    foreach (Zone zone in zones)
                    {
                        foreach (CardGO c in zone.Cards)
                        {
                            if (!c.Card.Special && !((UnitCard)c.Card).Hero)
                            {
                                if (strongest.Count == 0)
                                    strongest.Add(c);
                                else if (((UnitCard)c.Card).Strength > ((UnitCard)strongest[0].Card).Strength)
                                {
                                    strongest.Clear();
                                    strongest.Add(c);
                                }
                                else if (((UnitCard)c.Card).Strength == ((UnitCard)strongest[0].Card).Strength)
                                    strongest.Add(c);
                            }
                        }
                    }
                    //move strongest to discard
                    foreach (CardGO go in strongest)
                    {
                        go.Discard();
                        print(go.name);
                    }
                }
                else
                {
                    //scorch highest str card(s) in opponents zone
                }
            }

            CardEventTrigger.UnHighlight(CardEventTrigger.SelectedCard.GetComponent<CardEventTrigger>());
            cardGO.MoveTo(GetComponent<Zone>());
            Manager.manager.UnHighlightZones();
            Zone z = GetComponent<Zone>();
            if (z.Type == Zone.Types.Melee || z.Type == Zone.Types.Ranged ||
                z.Type == Zone.Types.Siege)
                ((Battlefield)z).CalcStats();
            else if (z.Type == Zone.Types.Horn)
            {
                ((HornZone)z).SpecialHorn = cardGO;
                //recalc stats for associated battlefield
                ((HornZone)z).Battlefield.CalcStats();
            }

            //if Cards.count > MAX_CARDS_WITHOUT_OVERLAP
            //change layout group spacing by -10 * (Cards.Count - MAX_CARDS_WITHOUT_OVERLAP)
            GetComponent<HorizontalLayoutGroup>().spacing = z.RecalcSpacing();
            CardEventTrigger.Deselect(CardEventTrigger.SelectedCard);
        }
    }

    void Start()
    {
        if (image == null)
            image = GetComponent<Image>();
        Highlighted = false;
    }

    private void Reset()
    {
        image = GetComponent<Image>();
        Highlighted = false;
    }
}
