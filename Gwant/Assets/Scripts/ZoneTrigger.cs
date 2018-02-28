﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneTrigger : Button {

    static Color32 defaultColour = new Color32(255, 255, 255, 255);
    static Color32 highlightColour = new Color32(0, 128, 255, 255);

    public Color32 DefaultColour { get { return defaultColour; } set { defaultColour = value; } }
    public Color32 HighlightColour { get { return highlightColour; } set { highlightColour = value; } }

    public Image Image { get { return image; } }
    public bool Highlighted { get { return interactable; } set { interactable = value; } }

    // Use this for initialization
    new void Start () {
        if (image == null)
            image = GetComponent<Image>();
        ColorBlock colours = colors;
        colours.disabledColor = DefaultColour;
        colours.normalColor = HighlightColour;
        colours.highlightedColor = HighlightColour;
        colours.pressedColor = HighlightColour;

        colors = colours;
        Highlighted = false;
	}
	

}
