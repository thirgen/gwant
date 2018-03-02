using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ZoneTrigger : EventTrigger {

    static Color32 defaultColour = new Color32(255, 255, 255, 255);
    static Color32 highlightColour = new Color32(0, 128, 255, 255);

    public Color32 DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public Color32 HighlightColour { get { return highlightColour; } set { highlightColour = value; } }
    
    Image image;
    public Image ZoneImage { get { return image; } }

    bool highlighted;
    public bool Highlighted { get { return highlighted; }
        set
        {
            if (value == true)
                image.color = HighlightColour;
            else
                image.color = DefaultColour;
            highlighted = value;
        }
    }
    //public bool Highlighted { get { return interactable; } set { interactable = value; } }

    
    void Start () {
        if (image == null)
            image = GetComponent<Image>();
        Highlighted = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        print("CLICK");
    }
    

}
