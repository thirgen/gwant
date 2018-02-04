using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitCard : Card {

    public enum Sections { Agile, Melee, Ranged, Siege, COUNT }

    Sections section;
    bool hero;
    Abilities ability;

    //x = total strength
    //y = base strength
    Vector2Int strength;
    Vector2Int morale;

    string muster;
    int scorchThreshold;
    int avenger;

    bool bond;
    bool horn;


    private void CardSetUp(int ID, string Name, string Art, Sections Section, int Strength,
        bool Hero, Abilities Ability, int Avenger, string Muster, int ScorchThreshold)// : base(ID, Name, Art, false)
    {
        this.ID = ID;
        this.Name = Name;
        this.Art = Art;
        this.Special = false;
        this.Section = Section;
        this.Hero = Hero;
        this.Ability = Ability;
        strength = new Vector2Int();
        morale = new Vector2Int();
        SetBaseStrength(Strength);
        if (Ability == Abilities.Morale)
            SetBaseMorale(1);
        else
            SetBaseMorale(0);
        if (Ability == Abilities.Muster)
            this.Muster = Muster;
        if (Ability == Abilities.Avenger)
            this.Avenger = Avenger;
        if (Ability == Abilities.Scorch)
            this.ScorchThreshold = ScorchThreshold;
        //serializedUnitCard = new SerializedUnitCard(ID, Name, Art, Section, strength, hero,
        //ability, morale, muster, scorchThreshold, bond, horn);
    }



    public static UnitCard AddComponentTo(GameObject go, int ID, string Name, string Art, Sections Section, int Strength,
        bool Hero, Abilities Ability, int Avenger, string Muster, int ScorchThreshold) //
    {        UnitCard c = go.AddComponent<UnitCard>();
        c.CardSetUp(ID, Name, Art, Section, Strength, Hero, Ability, Avenger, Muster, ScorchThreshold);
        return c;
    }



    public override void ApplyEffects(Zone zone)
    {
        //Stuff

        //CalcStats(zone);
    }

    public void CalcStats(Battlefield bf)
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

    public int Strength { get { return strength.x; } set { strength.x = value; } }
    public int GetBaseStrength() { return strength.y; }
    public int Morale { get { return morale.x; } set { morale.x = value; } }
    public int GetBaseMorale() { return morale.y; }
    public int ScorchThreshold { get { return morale.y; } private set { scorchThreshold = value; } }
    public int Avenger { get { return avenger; } private set { avenger = value; } }

    private void SetBaseStrength(int Strength)
    {
        this.Strength = Strength;
        strength.y = Strength;
    }
    private void SetBaseMorale(int Morale)
    {
        //this.Morale = Morale;
        morale.y = Morale;
    }

    public bool Hero { get { return hero; } private set { hero = value; } }
    public bool Bond { get { return bond; } private set { bond = value; } }
    public bool Horn { get { return horn; } set { horn = value; } }
    public Sections Section { get { return section; } private set { section = value; } }
    public string Muster
    {
        get
        {
            if (Ability == Abilities.Muster)
                return muster;
            else
                return null;
        }
        private set { muster = value; }
    }
    public override Abilities Ability
    {
        get
        {
            if (AbilityIsValid(ability))
                return ability;
            else
                return Abilities.None;
        }
        protected set { ability = value; } //Must be protected because it's an override
    }

    protected override bool AbilityIsValid(Abilities ab)
    {
        if ((int)ab < UNIT_END)
            return true;
        return false;
    }

    
}
