using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager manager;
    public TextAsset[] TextAssets;
    private List<GameObject> FactionGameObjects;
    public bool DebugCheats = true;
    private bool turn;

    /// <summary>
    /// Returns true if it's Player One's turn.
    /// </summary>
    public bool PlayerOnesTurn { get { return turn; } private set { turn = value; } }


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
            foreach (GameObject go in g)
                Destroy(go);
        }

        int[] deck = new int[] { 1, 3, 6, 9, 10, 14, 18, 11, 11, 12, 12, 13,
            100, 101, 101, 102, 102, 102, 103, 103, 106, 107, 108, 109, 119, 121, 122, 122 };

        PlayerOnesTurn = (Random.Range(0, 2) == 1) ? true : false;


        //set up board
        InitialiseZone(player1Half, true);
        InitialiseZone(player2Half, false);
        weather.Type = Zone.Types.Weather;
        weather.VisibleTo = Zone.IsVisibleTo.Both;
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
	
	void Update () {
        Cheats();
	}

    public void ChangeTurn()
    {
        PlayerOnesTurn = !PlayerOnesTurn;
    }


    void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //draw card;
            CardGO cardGO = player1Half.Deck.Cards[0];
            player1Half.Deck.MoveCardTo(cardGO ,player1Half.Hand);
        }
    }
}
