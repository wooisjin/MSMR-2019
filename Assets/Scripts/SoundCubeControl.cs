using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class SoundCubeControl : MonoBehaviour , IInputClickHandler{

    // Cube Id
    public int position;
    // Sound Board
    public GameObject boardObj;
    private BoardController board;

    private bool selected;
    private Renderer renderer;
    private Highlight highlight;

    public void Awake() {
        renderer = GetComponent<Renderer>();
        highlight = GetComponent<Highlight>();
        board = boardObj.GetComponent<BoardController>();
        selected = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (board.currentMode == Categories.Instrument.BLANK) {
            return;
        }
        selected = !selected;
        if (selected == true)
        {
            board.modifySequence(position);
            renderer.material.color = Categories.highlight;
            highlight.setHighlightMode(false);
        }
        else
        {
            board.modifySequence(position);
            renderer.material.color = Categories.normal;
            highlight.setHighlightMode(true);
        }
    }

    public void highlightThis() {
        if (!selected) {
            selected = true;
            renderer.material.color = Categories.highlight;
            highlight.setHighlightMode(false);
        }
    }

    public void clear()
    {
        if (selected) {
            selected = false;
            renderer.material.color = Categories.normal;
            highlight.setHighlightMode(true);
        }
    }

}
