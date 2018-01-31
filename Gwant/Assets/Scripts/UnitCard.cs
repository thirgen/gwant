using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : Card {

    Abilities ability;
    bool hero;



    public UnitCard(string Name, string Art) : base(Name, Art, false)
    {

    }


    public bool Hero { get { return hero; } private set { hero = value; } }
    public override Abilities Ability
    {
        get
        {
            if (AbilityIsValid(ability))
                return ability;
            else
                return Abilities.None;
        }
    }

    protected override bool AbilityIsValid(Abilities ab)
    {
        if ((int)ab < UNIT_END)
            return true;
        return false;
    }
}
