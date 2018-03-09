using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager manager;
    public TextAsset[] TextAssets;
    private List<GameObject> FactionGameObjects;
    public bool DebugCheats = true;
    private bool turn;
    private List<Zone> highlightedZones;
    private bool cardRotation;
    /// <summary>
    /// Returns true if it's Player One's turn.
    /// </summary>
    public bool PlayerOnesTurn { get { return turn; } private set { turn = value; } }
    public bool CardRotation { get { return cardRotation; } set { cardRotation = value; } }


    #region BoardZones
    [System.Serializable]
    public struct BoardHalf
    {
        public Zone Hand;
        public Zone Discard;
        public Deck Deck;
        public Battlefield Melee;
        public Battlefield Ranged;
        public Battlefield Siege;
    }
    #endregion
    public BoardHalf player1Half;
    public BoardHalf player2Half;
    public Zone weather;

    void Start()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
            Destroy(gameObject);
        TextAssets = Resources.LoadAll<TextAsset>("Factions/");
        FactionGameObjects = new List<GameObject>();

        GameObject UnitPrefab = Resources.Load<GameObject>("Prefabs/UnitPrefab");
        GameObject HeroPrefab = Resources.Load<GameObject>("Prefabs/HeroPrefab");
        GameObject SpecialPrefab = Resources.Load<GameObject>("Prefabs/SpecialPrefab");

        foreach (TextAsset t in TextAssets)
        {
            List<GameObject> g = TextParsing.ParseText(t.text);
            for (int i = 0; i < 2; i++)
            {
                foreach (GameObject go in g)
                {
                    FactionGameObjects.Add(go);
                    Faction f = go.GetComponent<Faction>();
                    foreach (Card c in f.Cards)
                    {
                        GameObject wrapper;
                        if (c.Special)
                        {
                            //instantiate special prefab
                            wrapper = Instantiate(SpecialPrefab, player1Half.Deck.transform);
                        }
                        else if (((UnitCard)c).Hero)
                        {
                            //instantiate hero prefab
                            wrapper = Instantiate(HeroPrefab, player1Half.Deck.transform);
                        }
                        else //regular unit
                        {
                            //instantiate unit prefab
                            wrapper = Instantiate(UnitPrefab, player1Half.Deck.transform);
                        }
                        //CardGO wrapper = new GameObject().AddComponent<CardGO>();
                        CardGO cardGO = wrapper.GetComponent<CardGO>();
                        cardGO.SetCard(c);
                        //RectTransform rect = wrapper.GetComponent<RectTransform>();
                        //rect.pivot = new Vector2(0.5f, 0.5f);
                        //rect.anchoredPosition = new Vector2(0.5f, 0.5f);
                        //rect.offsetMax = new Vector2(0.5f, 0.5f);
                        //rect.offsetMin = new Vector2(0.5f, 0.5f);
                        //rect.position = Vector2.zero;
                        player1Half.Deck.AddCard(cardGO);
                        wrapper.SetActive(false);
                    }
                    Faction.CloneTo(f, gameObject);
                }
            }
            foreach (GameObject go in g)
                Destroy(go);
        }

        int[] deck = new int[] { 1, 3, 6, 9, 10, 14, 18, 11, 11, 12, 12, 13,
            100, 101, 101, 102, 102, 102, 103, 103, 106, 107, 108, 109, 119, 121, 122, 122 };

        //PlayerOnesTurn = (Random.Range(0, 2) == 1) ? true : false;
        PlayerOnesTurn = true;


        //set up board
        InitialiseZone(player1Half, true);
        InitialiseZone(player2Half, false);
        weather.Type = Zone.Types.Weather;
        weather.VisibleTo = Zone.IsVisibleTo.Both;
        highlightedZones = new List<Zone>();
    }

    private void InitialiseZone(BoardHalf b, bool player1)
    {
        Zone.IsVisibleTo visibleTo = (player1) ? Zone.IsVisibleTo.Player1 : Zone.IsVisibleTo.Player2;
        Zone.IsVisibleTo notVisibleTo = (player1) ? Zone.IsVisibleTo.Player2 : Zone.IsVisibleTo.Player1;

        b.Hand.Type = Zone.Types.Hand;
        b.Hand.VisibleTo = visibleTo;

        b.Deck.Type = Zone.Types.Deck;
        b.Deck.VisibleTo = Zone.IsVisibleTo.None;

        b.Discard.Type = Zone.Types.Discard;
        b.Discard.VisibleTo = visibleTo;

        b.Melee.Type = Zone.Types.Melee;
        b.Melee.VisibleTo = Zone.IsVisibleTo.Both;

        b.Ranged.Type = Zone.Types.Ranged;
        b.Ranged.VisibleTo = Zone.IsVisibleTo.Both;

        b.Siege.Type = Zone.Types.Siege;
        b.Siege.VisibleTo = Zone.IsVisibleTo.Both;
    }
	
    public void HighlightNewZone(Card card, bool highlightPlayerOne)
    {
        //Unhighlight any previously highlighted zones
        UnHighlightZones();

        BoardHalf half = (highlightPlayerOne) ? player1Half : player2Half;
        if (card.Special)
        {
            switch (card.Ability)
            {
                case Card.Abilities.Decoy:
                    //highlight cards in battlefield zones
                    HighlightCardsInZone(half.Melee);
                    HighlightCardsInZone(half.Ranged);
                    HighlightCardsInZone(half.Siege);
                    break;
                case Card.Abilities.Scorch:
                    //highlight all 3 zones
                    Highlight(half.Melee);
                    Highlight(half.Ranged);
                    Highlight(half.Siege);
                    break;
                case Card.Abilities.Horn:
                    //highlight the three horn zones
                    if (half.Melee.ZoneHorn.SpecialHorn == null)
                        Highlight(half.Melee.ZoneHorn);
                    if (half.Ranged.ZoneHorn.SpecialHorn == null)
                        Highlight(half.Ranged.ZoneHorn);
                    if (half.Siege.ZoneHorn.SpecialHorn == null)
                        Highlight(half.Siege.ZoneHorn);
                    break;
            }
            if (card.Ability == Card.Abilities.Decoy)
            {
            }
            else if (card.Ability == Card.Abilities.Horn)
            {
            }
        }
        else
        {
            UnitCard unitCard = (UnitCard)card;
            if (unitCard.Section == UnitCard.Sections.Melee)
            {
                //highlight melee
                Highlight(half.Melee);
            }
            else if (unitCard.Section == UnitCard.Sections.Ranged)
            {
                //ranged
                Highlight(half.Ranged);
            }
            else if (unitCard.Section == UnitCard.Sections.Siege)
            {
                //siege
                Highlight(half.Siege);
            }
            else //if (unitCard.Section == UnitCard.Sections.Agile)
            {
                //melee & ranged
                Highlight(half.Melee);
                Highlight(half.Ranged);
            }

        }
    }

    private void HighlightCardsInZone(Zone z)
    {
        foreach(CardGO go in z.Cards)
        {
            CardEventTrigger.SpecialHighlight(go.GetComponent<CardEventTrigger>());
            //print(go.GetComponent<CardEventTrigger>().Highlighted + ", " + go.GetComponent<CardEventTrigger>().SpecialHighlighted);
        }
    }

    public Zone GetZone(Zone.Types Type, bool CurrentPlayer = true)
    {

        //11 0 - p1
        //10 1 - p2
        //01 1 - p2
        //00 0 - p1
        BoardHalf half = (PlayerOnesTurn == CurrentPlayer) ? player1Half : player2Half;
        switch(Type)
        {
            case Zone.Types.Hand:
                return half.Hand;
            case Zone.Types.Melee:
                return half.Melee;
            case Zone.Types.Ranged:
                return half.Ranged;
            case Zone.Types.Siege:
                return half.Siege;
            case Zone.Types.Deck:
                return half.Deck;
            case Zone.Types.Discard:
                return half.Discard;
            case Zone.Types.Weather:
                return weather;
            default:
                return null;
        }
    }

    public void UnHighlightZones()
    {
        foreach (Zone z in highlightedZones)
            z.UnHighlight();
        highlightedZones.Clear();
    }
    private void Highlight(Zone z)
    {
        z.Highlight();
        highlightedZones.Add(z);
    }

	void Update () {
        Cheats();
	}

    public void ChangeTurn()
    {
        PlayerOnesTurn = !PlayerOnesTurn;
    }


    void Cheats()
    {
        if (Input.GetKey(KeyCode.F1) && Time.frameCount % 5 == 0)
        {
            //draw card;
            player1Half.Deck.DrawCard(player1Half.Hand);
        }
    }
}
