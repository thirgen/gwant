using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGO : MonoBehaviour {
    
    public Card card;
    public Card card2;
    public TextAsset text;
    public List<string> list = new List<string>();
    private List<GameObject> gos = new List<GameObject>();
    public List<Card> cards = new List<Card>();

    // Use this for initialization
    void Start()
    {
        //card = new UnitCard(1, "1", "1", UnitCard.Sections.Melee, 10, false, Card.Abilities.None);
        //card = UnitCard.AddComponentTo(gameObject, 1, "Name", "Art", UnitCard.Sections.Melee, 10,
            //false, Card.Abilities.None);
        card2 = SpecialCard.AddComponentTo(gameObject, 2, "Special", "Special",
            Card.Abilities.Weather, SpecialCard.WeatherTypes.Frost);

        //List<UnitCard> cards = FindObjectOfType<Battlefield>().Cards;
        Battlefield b = FindObjectOfType<Battlefield>();
        if (b != null)
        {
            //print("FROUND");
            //print(b.gameObject);
            /*
            for (int i = 0; i < 10; i++)
            {

                b.Cards.Add((UnitCard)card);
            }
            */
        }
        gos = TextParsing.ParseText(text.text, out list);
        foreach (GameObject go in gos)
        {
            cards.Add(go.GetComponent<Card>());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
