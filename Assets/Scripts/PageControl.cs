using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class PageControl : MonoBehaviour, IInputClickHandler {

    public enum Control {UP, DOWN, NEXT, PREV};

    public Control control;
    public GameObject boardObj;
    private BoardController board;

    public void Awake() {
        board = boardObj.GetComponent<BoardController>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (control == Control.UP)
        {
            board.downPage();
        }
        else if (control == Control.DOWN)
        {
            board.upPage();
        }
        else if (control == Control.NEXT)
        {
            board.nextPage();
        }
        else if (control == Control.PREV)
        {
            board.prevPage();
        }
    }

}
