using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGO : MonoBehaviour {
    
    public Card card;
    public Card card2;
    public TextAsset[] TextAssets;
    private List<GameObject> FactionGameObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        TextAssets = Resources.LoadAll<TextAsset>("Factions/");
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
        foreach (TextAsset t in TextAssets)
        {
            List<GameObject> g = TextParsing.ParseText(t.text);
            foreach (GameObject go in g)
            {
                FactionGameObjects.Add(go);
            }
        }
        
        foreach (GameObject go in FactionGameObjects)
        {
            DontDestroyOnLoad(go);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
