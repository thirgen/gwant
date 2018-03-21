using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[AddComponentMenu("Gwant/Card GameObject")]
//[RequireComponent(typeof(CardEventTrigger))]
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
        SetUpSprites();
    }

    public void MoveTo(Zone NewZone, int PositionInZone = -1)
    {
        Zone.Cards.Remove(this);
        if (PositionInZone < 0 || PositionInZone > NewZone.Cards.Count)
            NewZone.Cards.Add(this);
        else
        {
            NewZone.Cards.Insert(PositionInZone, this);
        }
        //PositionInZone = NewZone.Cards.IndexOf(this);
        if (gameObject.activeSelf)
            StartCoroutine(MoveCardTo(NewZone, PositionInZone));
        else
        {
            NewZone.GetComponent<ZoneLayoutGroup>().CardAdded(PositionInZone);
            transform.SetParent(NewZone.transform);
        }

        /*          */
        //FIX THIS    
        /*          */

        if (PositionInZone > 0 && PositionInZone < NewZone.Cards.Count && (NewZone.Cards.IndexOf(this) != PositionInZone ||
            Card.Ability == Card.Abilities.Bond))
            transform.SetSiblingIndex(PositionInZone);
        if (!gameObject.activeSelf && Zone.Type != Zone.Types.Deck)
            gameObject.SetActive(true);

        

        //if (NewZone.Type != Zone.Types.Deck && NewZone.Type != Zone.Types.Discard &&
            //NewZone.Type != Zone.Types.Hand)
            //ApplyEffects(NewZone);
        //else
            //ApplyEffects(NewZone, false);
        if (Manager.manager.CardRotation)
            transform.Rotate(0, 0, Random.Range(-3, 4));
        
        //card.ApplyEffects(targetZone);
    }
    

    public void ApplyEffects(Zone zone, bool apply = true)
    {
        if (zone.Type != Zone.Types.Hand && zone.Type != Zone.Types.Deck && zone.Type != Zone.Types.Discard)
        {
            if (Card.Special)
            {
                SpecialCard c = (SpecialCard)Card;
                if (c.WeatherType != SpecialCard.WeatherTypes.None)
                {
                    if (c.WeatherType == SpecialCard.WeatherTypes.Frost)
                        c.WeatherEffect(zone); //Melee row
                    else if (c.WeatherType == SpecialCard.WeatherTypes.Fog)
                        c.WeatherEffect(zone); //Ranged row
                    else if (c.WeatherType == SpecialCard.WeatherTypes.Rain)
                        c.WeatherEffect(zone); //Siege row
                    else if (c.WeatherType == SpecialCard.WeatherTypes.Storm)
                    {
                        c.WeatherEffect(zone); //Ranged row
                        c.WeatherEffect(zone); //Siege row
                    }
                    else //if (c.WeatherType == SpecialCard.WeatherTypes.Clear)
                        c.WeatherEffect(zone); //Clear Weather row
                }
                else
                {
                    //Horn, Scorch, Mushrom, Decoy stuff
                }
            }
            else
            {
                Battlefield bf = (Battlefield)Zone;

                //None, Medic, Morale, Muster, Spy, Bond, Berserker, Horn, Scorch, Mushroom
                if (apply && Card.Ability == Card.Abilities.Medic)
                {
                    //display all graveyard cards
                }
                else if (Card.Ability == Card.Abilities.Morale)
                {
                    if (apply)
                        bf.Morale++; //+1 morale to zone
                    else
                        bf.Morale--;
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Muster)
                {
                    //select all muster cards from hand/deck

                    List<CardGO> cardsToMove = new List<CardGO>();
                    foreach (CardGO cardGO in Manager.manager.GetZone(Zone.Types.Hand).Cards)
                    {
                        if (!cardGO.Card.Special && ((UnitCard)cardGO.Card).Muster != null)
                        {
                            if (IsMusterCard(cardGO.Card, ((UnitCard)cardGO.Card).Muster))
                                cardsToMove.Add(cardGO);
                        }
                    }
                    foreach (CardGO cardGO in Manager.manager.GetZone(Zone.Types.Deck).Cards)
                    {
                        if (!cardGO.Card.Special && ((UnitCard)cardGO.Card).Muster != null)
                        {
                            if (IsMusterCard(cardGO.Card, ((UnitCard)cardGO.Card).Muster))
                                cardsToMove.Add(cardGO);
                        }
                    }

                    //play selected cards
                    while (cardsToMove.Count > 0)
                    {
                        cardsToMove[0].MoveTo(Manager.manager.GetZone(((UnitCard)cardsToMove[0].Card).GetZone));
                        cardsToMove.Remove(cardsToMove[0]);
                    }
                }
                else if (apply && Card.Ability == Card.Abilities.Spy)
                {
                    //draw card from deck
                    ((Deck)Manager.manager.GetZone(Zone.Types.Deck)).DrawCard(Manager.manager.GetZone(Zone.Types.Hand));
                }
                else if (Card.Ability == Card.Abilities.Bond)
                {
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Berserker)
                {
                    //if mushroom in zone, do berseker thing
                }
                else if (Card.Ability == Card.Abilities.Horn)
                {
                    //add card to HornZorn horn units
                    if (apply)
                        bf.ZoneHorn.UnitHorns.Add(this);
                    else
                        bf.ZoneHorn.UnitHorns.Remove(this);
                    //Recalc strength for zone
                    bf.CalcStats();
                }
                else if (apply && Card.Ability == Card.Abilities.Scorch)
                {
                    //destroy strongest card(s) in zone
                }
                else if (apply && Card.Ability == Card.Abilities.Mushroom)
                {
                    //trigger berserker stuff
                }
            }
        }
    }

    public void Discard()
    {
        MoveTo(Manager.manager.GetZone(Zone.Types.Discard));
    }

    private bool IsMusterCard(Card Card, string MusterName)
    {
        if (Card.Name.Contains(MusterName))
            return true;
        return false;
    }

    #region Sprite Fields
    [SerializeField]
    private static Sprite[] StrengthImages;
    [SerializeField]
    private static Sprite[] AbilityImages;
    [SerializeField]
    private static Sprite[] ZoneImages;

    private TextMeshProUGUI strengthText;
    public TextMeshProUGUI StrengthText { get { return strengthText; } private set { strengthText = value; } }
    #endregion

    #region Sprite methods
    private void SetUpSprites()
    {
        //set card art
        //GetComponent<Image>().sprite = null;

        //set ability image
        Sprite ability = GetAbilitySprite(card.Ability);
        if (ability == null)
            transform.GetChild(1).GetComponent<Image>().enabled = false;
        else
            transform.GetChild(1).GetComponent<Image>().sprite = GetAbilitySprite(card.Ability);


        if (StrengthImages == null)
            StrengthImages = Resources.LoadAll<Sprite>("Images/strength");

        if (!card.Special)
        {
            //set strength image type
            Image strength = transform.GetChild(2).GetComponent<Image>();
            if (((UnitCard)card).Hero)
                strength.sprite = StrengthImages[1];
            else
                strength.sprite = StrengthImages[0];

            //set strength number
            StrengthText = strength.GetComponentInChildren<TextMeshProUGUI>();
            UpdateStrengthText();

            //set zone sprite
            transform.GetChild(3).GetComponent<Image>().sprite = GetZoneSprite(((UnitCard)card).Section);

        }
    }

    public void UpdateStrengthText()
    {
        UnitCard card = (UnitCard)Card;
        StrengthText.text = card.Strength.ToString();
        if (!card.Hero)
        {
            if (card.Strength > card.GetBaseStrength())
                StrengthText.color = new Color32(6, 100, 42, 255);
            else if (card.Strength < card.GetBaseStrength())
                StrengthText.color = Color.red;
            else
                StrengthText.color = Color.black;
        }
        else
            StrengthText.color = Color.white;
    }

    private Sprite GetAbilitySprite(Card.Abilities ability)
    {
        if (AbilityImages == null)
            AbilityImages = Resources.LoadAll<Sprite>("Images/abilities/");

        if (ability == Card.Abilities.None)
            return null;
        else if ((int)ability < (int)Card.Abilities.SPECIAL_START)
            return AbilityImages[(int)ability - 1];
        else if ((int)ability < (int)Card.Abilities.UNIT_END)
            return AbilityImages[(int)ability - 2];
        else if ((int)ability < (int)Card.Abilities.Weather)
            return AbilityImages[(int)ability - 3];
        else
            return null;
    }

    private Sprite GetZoneSprite(UnitCard.Sections section)
    {
        if (ZoneImages == null)
            ZoneImages = Resources.LoadAll<Sprite>("Images/zone");

        if (section == UnitCard.Sections.Melee)
            return ZoneImages[0];
        else if (section == UnitCard.Sections.Ranged)
            return ZoneImages[1];
        else if (section == UnitCard.Sections.Siege)
            return ZoneImages[2];
        else if (section == UnitCard.Sections.Agile)
            return ZoneImages[3];
        else return null;
    }
    #endregion

    private IEnumerator MoveCardTo(Zone z, int PositionInZone)
    {
        yield return new WaitForSeconds(0.1f);

        z.GetComponent<ZoneLayoutGroup>().CardAdded(PositionInZone);
        transform.SetParent(z.transform);
    }
}
