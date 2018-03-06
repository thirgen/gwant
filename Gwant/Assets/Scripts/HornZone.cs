using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornZone : Zone {

    public CardGO SpecialHorn;
    public List<CardGO> UnitHorns = new List<CardGO>();
    private Battlefield battlefield;
    
    public Battlefield Battlefield { get { return battlefield; } }

    //Battlefield.Types combat;
    public bool HasHorn
    {
        get
        {
            if (SpecialHorn == null && UnitHorns.Count == 0)
                return false;
            else return true;
        }
    }
    //public Battlefield.Combats Combat { get { return combat; } set { combat = value; } }

    new private void Start()
    {
        Type = Types.Horn;
        base.Start();
        
    }

}
