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
    }


    public SpecialCard(int ID, string Name, string Art) : base(ID, Name, Art, true)
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
