using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGO : MonoBehaviour {

    private Card card;

    public Card Card { get { return card; } private set { card = value; } }

	// Use this for initialization
	void Start () {
		
	}

    public void SetCard(Card Card)
    {
        this.Card = Card;
        name = Card.Name;
    }

    private void AddEventTrigger()
    {
        //Add and set up event trigger paramets here;
    }
}
