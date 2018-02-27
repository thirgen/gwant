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

    public Zone.Types InZone { get { return transform.parent.gameObject.GetComponent<Zone>().Type; } }

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


}
