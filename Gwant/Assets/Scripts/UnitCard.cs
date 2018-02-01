using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : Card {

    Abilities ability;
    bool hero;
    int strength;
    int baseStrength;
    int morale;
    int baseMorale;
    string muster;
    int scorchThreshold;
    bool bond;
    bool horn;


    public UnitCard(int ID, string Name, string Art, int Strength, bool Hero, Abilities Ability, int Morale) :
        base(ID, Name, Art, false)
    {
        this.Hero = Hero;
        this.Ability = Ability;
        SetBaseStrength(Strength);
        SetBaseMorale(Morale);
    }



    public override void ApplyEffects(Zone zone)
    {
        //Stuff

        //CalcStats(zone);
    }

    public void CalcStats(Zone.Battlefield bf)
    {
        //1. apply Weather effects
        if (bf.Weather)
        {
            Strength = 1;
        }
        else
            Strength = GetBaseStrength();
        //2. apply Bond effects
        if (Bond)
        {
            Strength += Strength;
        }
        //3. apply Morale effects
        Strength += Morale;
        //4.1 apply Horn effect from Horn unit cards
        if (bf.ZoneHorn.Horn && !Horn)
        {
            Strength += Strength;
            Horn = true;
        }
        //4.2 apply Horn effect from Horn special cards
        if (bf.Horns != null && !Horn)
        {
            foreach (UnitCard c in bf.Horns)
            {
                if (c != this)
                {
                    Strength += Strength;
                    Horn = true;
                }
            }
        }
    }

    public int Strength { get { return strength; } set { strength = value; } }
    public int GetBaseStrength() { return baseStrength; }
    public int Morale { get { return morale; } set { morale = value; } }
    public int GetBaseMorale() { return baseMorale; }

    private void SetBaseStrength(int Strength)
    {
        this.Strength = Strength;
        baseStrength = Strength;
    }
    private void SetBaseMorale(int Morale)
    {
        this.Morale = Morale;
        baseMorale = Morale;
    }

    public bool Hero { get { return hero; } private set { hero = value; } }
    public bool Bond { get { return bond; } private set { bond = value; } }
    public bool Horn { get { return horn; } set { horn = value; } }
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
