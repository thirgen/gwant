using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : Card {

    Abilities ability;
    bool hero;



    public UnitCard(int ID, string Name, string Art, bool Hero, Abilities Ability) : base(ID, Name, Art, false)
    {
        this.Hero = Hero;
        this.Ability = Ability;
    }



    public override void ApplyEffects(Zone zone)
    {
        //Stuff

        CalcStats();
    }

    public void CalcStats()
    {
        //Definte Order of operations for effects
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
        protected set { ability = value; }
    }

    protected override bool AbilityIsValid(Abilities ab)
    {
        if ((int)ab < UNIT_END)
            return true;
        return false;
    }
}
