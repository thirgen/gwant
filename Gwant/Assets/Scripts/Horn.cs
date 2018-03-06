using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour {

    SpecialCard hornCard;
    public List<CardGO> hornUnits = new List<CardGO>();
    //Battlefield.Types combat;
    public bool HasHorn
    {
        get
        {
            if (hornCard == null && hornUnits.Count == 0)
                return false;
            else return true;
        }
    }
    //public Battlefield.Combats Combat { get { return combat; } set { combat = value; } }



}
