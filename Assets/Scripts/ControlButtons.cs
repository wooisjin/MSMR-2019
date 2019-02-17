using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class ControlButtons : MonoBehaviour, IInputClickHandler {

    public enum Control { PLAY, RECORD, METRONOME, CLEAR, SOLO }

    public GameObject boardObj;
    public Control control;

    private BoardController board;

    public void Awake() {
        board = boardObj.GetComponent<BoardController>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (control == Control.PLAY)
        {
            board.playPressed();
        }
        else if (control == Control.RECORD)
        {
            board.recordPressed();
        }
        else if (control == Control.SOLO) {
            board.soloPressed();
        }
        else if (control == Control.METRONOME)
        {
            board.metronomePressed();
        }
        else if (control == Control.CLEAR)
        {
            board.clearPressed();
        }
    }
}
