using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCard : Card {

    Abilities ability;
    //bool weather;

    public enum WeatherTypes { None, Clear, Frost, Fog, Rain, Storm, COUNT }
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

    
    private void CardSetUp(int ID, string Name, string Art, Abilities Ability, WeatherTypes WeatherType)
    {
        this.ID = ID;
        this.Name = Name;
        this.Art = Art;
        this.Ability = Ability;
    }
    

    public static SpecialCard AddComponentTo(GameObject go, int ID, string Name, string Art, Abilities Ability,
        WeatherTypes WeatherType = WeatherTypes.None) //
    {
        SpecialCard c = go.AddComponent<SpecialCard>();
        if (Ability == Abilities.Weather && WeatherType == WeatherTypes.None)
            throw new GwantExceptions.InvalidWeatherException(ID);
        c.CardSetUp(ID, Name, Art, Ability, WeatherType);
        return c;
    }

    public override void ApplyEffects(Zone zone)
    {
        //Horn, Sorch, Mushroom, Decoy
        
    }

    public void WeatherEffect(Zone z)
    {

    }

    #region Properties

    //public bool Weather { get { return weather; } set { weather = value; } }
    public WeatherTypes WeatherType { get { return weatherType; } set { weatherType = value; } }

    #endregion


    protected override bool AbilityIsValid(Abilities ab)
    {
        if ((int)ab > SPECIAL_START && (int)ab < ABILITY_COUNT)
            return true;
        return false;
    }
}
