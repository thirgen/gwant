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

    public void MoveTo(Zone NewZone)
    {
        Zone.Cards.Remove(this);
        NewZone.Cards.Add(this);

        transform.SetParent(NewZone.transform);
        if (!gameObject.activeSelf && Zone.Type != Zone.Types.Deck)
            gameObject.SetActive(true);


        Card card = Card;
        if (card.Special)
        {
            SpecialCard c = (SpecialCard)card;
            if (c.WeatherType != SpecialCard.WeatherTypes.None)
            {
                if (c.WeatherType == SpecialCard.WeatherTypes.Frost)
                    c.WeatherEffect(NewZone); //Melee row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Fog)
                    c.WeatherEffect(NewZone); //Ranged row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Rain)
                    c.WeatherEffect(NewZone); //Siege row
                else if (c.WeatherType == SpecialCard.WeatherTypes.Storm)
                {
                    c.WeatherEffect(NewZone); //Ranged row
                    c.WeatherEffect(NewZone); //Siege row
                }
                else //if (c.WeatherType == SpecialCard.WeatherTypes.Clear)
                    c.WeatherEffect(NewZone); //Clear Weather row
            }
            else
            {
                //Horn, Scorch, Mushrom, Decoy stuff
            }
        }
        else
        {
            //None, Medic, Morale, Muster, Spy, Bond, Berserker, Horn, Scorch, Mushroom

        }
        //card.ApplyEffects(targetZone);
    }

}
