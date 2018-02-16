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
        public Zone Deck;
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

        foreach (TextAsset t in TextAssets)
        {
            List<GameObject> g = TextParsing.ParseText(t.text);
            foreach (GameObject go in g)
            {
                FactionGameObjects.Add(go);
                Faction f = go.GetComponent<Faction>();
                foreach (Card c in f.Cards)
                {
                    CardGO wrapper = new GameObject().AddComponent<CardGO>();
                    wrapper.SetCard(c);
                }
                Faction.CloneTo(f, gameObject);
            }
        }

        PlayerOnesTurn = (Random.Range(0, 2) == 1) ? true : false;



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
        }
    }
}
