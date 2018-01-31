using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public enum Abilities {
        None = -1, Agile, Medic, Morale, Muster, Spy, Bond, Berserker, //Unit abilities only
        SPECIAL_START, //Special abilites start here
        Horn, Scorch, //Both Unit and Special abilities
        UNIT_END, //No more Unit abilites beyond this point
        Mushroom, Decoy, Weather, //Special abilities only
        ABILITY_COUNT
    }
    protected const int UNIT_END = (int)Abilities.UNIT_END;
    protected const int SPECIAL_START = (int)Abilities.SPECIAL_START;
    protected const int ABILITY_COUNT = (int)Abilities.ABILITY_COUNT;


    private int id;
    private string cardName;
    private string art;
    private bool special;

    #region Constructors
    public Card() : this(0, "", "", true) { }

    public Card(int ID, string Name, string Art, bool Special)
    {
        this.ID = ID;
        this.Name = Name;
        this.Art = Art;
        this.Special = special;
    }
    #endregion

    #region Properties
    public int ID { get { return id; } private set { id = value; } }
    public string Name { get { return cardName; } private set { cardName = value; } }
    public string Art { get { return art; } private set { art = value; } }
    public bool Special { get { return special; } private set { special = value; } }
    #endregion

    public abstract Abilities Ability { get; }
    protected abstract bool AbilityIsValid(Abilities ab);

}
