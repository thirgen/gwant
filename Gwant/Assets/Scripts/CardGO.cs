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
        if (child == null)
            child = Resources.Load<GameObject>("Prefabs/CardBorder");
	}

    public void SetCard(Card Card)
    {
        this.Card = Card;
        name = Card.Name;
        child = Instantiate(child);
        child.transform.SetParent(transform);
        child.name = "Border";
        child.SetActive(false);
    }

    
}
