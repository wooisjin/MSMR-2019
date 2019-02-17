using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class InstrumentBlock : MonoBehaviour, IInputClickHandler{

    public Categories.Instrument instrument;
    public GameObject board;

    private BoardController soundBoard;

    public void Awake() {
        soundBoard = board.GetComponent<BoardController>();
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        soundBoard.setInstrument(instrument);
    }

}
