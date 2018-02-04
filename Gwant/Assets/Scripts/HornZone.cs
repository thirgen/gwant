using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornZone : MonoBehaviour {

    SpecialCard card;
    bool horn;
    Battlefield.Combats combat;
    public bool Horn { get { return horn; } set { horn = value; } }
    public Battlefield.Combats Combat { get { return combat; } set { combat = value; } }

}
