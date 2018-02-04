using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public abstract class Card : MonoBehaviour
{
    public enum Abilities {
        None, Medic, Morale, Muster, Spy, Bond, Berserker, Avenger, Vanish, //Unit abilities only
        SPECIAL_START, //Special abilites start here
        Horn, Scorch, Mushroom, //Both Unit and Special abilities
        UNIT_END, //No more Unit abilites beyond this point
        Decoy, Weather, //Special abilities only
        COUNT
    }
    protected const int UNIT_END = (int)Abilities.UNIT_END;
    protected const int SPECIAL_START = (int)Abilities.SPECIAL_START;
    protected const int ABILITY_COUNT = (int)Abilities.COUNT;


    private int id;
    private string cardName;
    private string art;
    private bool special;


    #region Constructors
    /*
    public Card() : this(0, "", "", true) { }

    public Card(int ID, string Name, string Art, bool Special)
    {
        this.ID = ID;
        this.Name = Name;
        this.Art = Art;
        this.Special = special;
    }
    */
    #endregion
    
    

    #region Properties
    public int ID { get { return id; } protected set { id = value; } }
    public string Name { get { return cardName; } protected set { cardName = value; } }
    public string Art { get { return art; } protected set { art = value; } }
    public bool Special { get { return special; } protected set { special = value; } }
    #endregion

    public abstract Abilities Ability { get; protected set; }
    protected abstract bool AbilityIsValid(Abilities ab);
    public abstract void ApplyEffects(Zone zone);
    

}
