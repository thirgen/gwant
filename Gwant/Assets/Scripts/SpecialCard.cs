using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCard : Card {

    Abilities ability;
    bool weather;

    public enum WeatherTypes { Clear, Frost, Fog, Rain, Storm }
    WeatherTypes weatherType;

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


    public SpecialCard(int ID, string Name, string Art, Abilities Ability) : base(ID, Name, Art, true)
    {
        this.Ability = Ability;
    }



    public override void ApplyEffects(Zone zone)
    {
        //Horn, Sorch, Mushroom, Decoy
        
    }

    public void WeatherEffect(Zone z)
    {

    }

    #region Properties

    public bool Weather { get { return weather; } set { weather = value; } }
    public WeatherTypes WeatherType { get { return weatherType; } set { weatherType = value; } }

    #endregion


    protected override bool AbilityIsValid(Abilities ab)
    {
        if ((int)ab > SPECIAL_START && (int)ab < ABILITY_COUNT)
            return true;
        return false;
    }
}
