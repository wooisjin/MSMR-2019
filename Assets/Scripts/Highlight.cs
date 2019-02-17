using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;


// Highlights object that is in Focus
public class Highlight : MonoBehaviour, IFocusable {
    
    // Private fields
    private Material mat;
    private Renderer renderer;

    // Public fields
    public Color normalColor;
    public Color highlightColor;
    public bool highlightable;

    // Initialize
    private void Awake() {
        //Get renderer then get the instance of current material
        renderer = gameObject.GetComponent<Renderer>();
        mat = renderer.material;
        mat.color = normalColor;
        highlightable = true;
    }

    // When in focus, change the material color to the highlight color
    public void OnFocusEnter() {
        mat.color = highlightColor;
    }

    // When out of focus, revert to original color
    public void OnFocusExit() {
        if (highlightable)
        {
            mat.color = normalColor;
        }
    }

    public void setHighlightMode(bool mode) {
        highlightable = mode;
    }
}
