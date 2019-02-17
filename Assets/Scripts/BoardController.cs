using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {

    // Sequence arrays
    public bool[,] drumsArr;
    public bool[,] guitarArr;
    public bool[,] pianoArr;
    public bool[,] bassArr;
    public bool[,] samplerArr;


    // Public fields
    public Color instrumentColor;
    public GameObject board;

    public TextMesh instText;
    public TextMesh s1;
    public TextMesh s2;
    public TextMesh s3;
    public TextMesh s4;
    public TextMesh pageText;

    public GameObject clipsObject;
    
    public float bpm;
    public int bars;

    public Categories.Instrument currentMode;

    // Private fields
    private Renderer boardRenderer;

    private Clips clips;
    private float beatTimer;
    private AudioSource audio;
    private bool play;
    private int sequence;
    private int page;
    private int vertPage;
    private bool solo;
    private bool metronome;
    private bool record;
    private int countIn;

    private AudioClip ac;

    // Initialize mesh renderer
    public void Awake () {

        currentMode = Categories.Instrument.BLANK;

        boardRenderer = board.GetComponent<Renderer>();
        boardRenderer.material.color = instrumentColor;
        clips = clipsObject.GetComponent<Clips>();

        audio = GetComponent<AudioSource>();
        ac = Microphone.Start("", true, 1, 44100);

        solo = false;
        metronome = false;
        vertPage = 1;
        beatTimer = 0;
        bpm = 120;
        bars = 2;
        page = 1;
        play = false;
        record = false;
        countIn = 0;

        clearPressed();
    }

    // Update function runs every frame
    public void Update() {
        if (play) {
            count();    
        }
    }

    // Sets instrument depending on passed enum
    public void setInstrument(Categories.Instrument instrument) {

        if (currentMode == instrument)
            return;

        currentMode = instrument;

        page = 1;
        vertPage = 1;

        resetBoard();

        pageText.text = "1/"+bars;

        if (instrument == Categories.Instrument.DRUMS)
        {
            boardRenderer.material.color = Categories.drums;
            instText.text = "Drums";
            s1.text = "Crash";
            s2.text = "Hat";
            s3.text = "Snare";
            s4.text = "Kick";
           
        }
        else if (instrument == Categories.Instrument.GUITAR)
        {
            boardRenderer.material.color = Categories.guitar;
            instText.text = "Guitar";
            s1.text = "C";
            s2.text = "D";
            s3.text = "E";
            s4.text = "F";
        }
        else if (instrument == Categories.Instrument.BASS)
        {
            boardRenderer.material.color = Categories.bass;
            instText.text = "Bass";
            s1.text = "C";
            s2.text = "D";
            s3.text = "E";
            s4.text = "F";

        }
        else if (instrument == Categories.Instrument.PIANO)
        {
            boardRenderer.material.color = Categories.piano;
            instText.text = "Piano";
            s1.text = "C";
            s2.text = "D";
            s3.text = "E";
            s4.text = "F";
        }
        else if (instrument == Categories.Instrument.SAMPLER)
        {
            boardRenderer.material.color = Categories.sampler;
            instText.text = "Sampler";
            s1.text = "Sound 1";
            s2.text = "Sound 2";
            s3.text = "Sound 3";
            s4.text = "Sound 4";
        }
    }

    public void playPressed() {
        sequence = 0;
        play = !play;
    }

    public void metronomePressed() {
        metronome = !metronome;
    }

    public void recordPressed()
    {
        play = true;
        record = true;
        countIn = 0;
    }

    public void soloPressed()
    {
        solo = !solo;
    }

    public void clearPressed() {
        drumsArr = new bool[4 * 8, 4];
        guitarArr = new bool[4 * 8, 8];
        pianoArr = new bool[4 * 8, 8];
        samplerArr = new bool[4 * 8, 8];
        bassArr = new bool[4 * 8, 8];

        for (int i = 0; i < 32; i++) {
            for (int j = 0; j < 8; j++) {
                if (j < 4) {
                    drumsArr[i, j] = false;
                    guitarArr[i, j] = false;
                    pianoArr[i, j] = false;
                    samplerArr[i, j] = false;
                    bassArr[i, j] = false;
                }
                
            }
        }

        resetBoard();
    }

    public void modifySequence(int position) {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8) + ((vertPage - 1) * 4);

        if (currentMode == Categories.Instrument.DRUMS)
        {
            if (drumsArr[xIndex, yIndex - ((vertPage - 1) * 4)])
            {
                drumsArr[xIndex, yIndex - ((vertPage - 1) * 4)] = false;
            }
            else {
                drumsArr[xIndex, yIndex - ((vertPage - 1) * 4)] = true;
            }

        }
        else if (currentMode == Categories.Instrument.GUITAR) {
            if (guitarArr[xIndex, yIndex])
            {
                guitarArr[xIndex, yIndex] = false;
            }
            else {
                guitarArr[xIndex, yIndex] = true;
            }
        }
        else if (currentMode == Categories.Instrument.PIANO)
        {
            if (pianoArr[xIndex, yIndex])
            {
                pianoArr[xIndex, yIndex] = false;
            }
            else
            {
                pianoArr[xIndex, yIndex] = true;
            }
        }
        else if (currentMode == Categories.Instrument.BASS)
        {
            if (bassArr[xIndex, yIndex])
            {
                bassArr[xIndex, yIndex] = false;
            }
            else
            {
                bassArr[xIndex, yIndex] = true;
            }
        }
        else if (currentMode == Categories.Instrument.SAMPLER)
        {
            if (samplerArr[xIndex, yIndex])
            {
                samplerArr[xIndex, yIndex] = false;
            }
            else
            {
                samplerArr[xIndex, yIndex] = true;
            }
        }
    }

    public void nextPage() {
        if (page < bars) {
            page++;
        }
        pageText.text = page + "/" + bars;
        Debug.Log(page);
        resetBoard();
    }

    public void prevPage()
    {
        if (page > 1)
        {
            page--;
        }
        pageText.text = page + "/" + bars;
        resetBoard();
        Debug.Log(page);

    }

    public void upPage()
    {
        if (currentMode != Categories.Instrument.DRUMS) {
            if (vertPage < 2)
            {
                vertPage++;
            }
            s1.text = "G";
            s2.text = "A";
            s3.text = "B";
            s4.text = "C";
            resetBoard();
        }
    }

    public void downPage()
    {
        if (vertPage > 1)
        {
            s1.text = "C";
            s2.text = "D";
            s3.text = "E";
            s4.text = "F";
            vertPage--;
            resetBoard();
        }
    }

    private bool getDrumSequenceAt(int position) {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8);

        return drumsArr[xIndex, yIndex];
    }

    private bool getGuitarSequenceAt(int position)
    {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8) + ((vertPage-1)*4);

        return guitarArr[xIndex, yIndex];
    }

    private bool getPianoSequenceAt(int position)
    {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8) + ((vertPage - 1) * 4);

        return pianoArr[xIndex, yIndex];
    }

    private bool getBassSequenceAt(int position)
    {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8) + ((vertPage - 1) * 4);

        return bassArr[xIndex, yIndex];
    }

    private bool getSamplerSequenceAt(int position)
    {
        int xIndex = (position % 8) + ((page - 1) * 8);
        int yIndex = (position / 8) + ((vertPage - 1) * 4);

        return samplerArr[xIndex, yIndex];
    }

    // Counts Metronome 
    private void count() {
        beatTimer += Time.deltaTime;

        float interval = 30 / bpm;

        if (beatTimer >= interval)
        {
            if (countIn == 8 && record)
            {
                record = false;
                
                clips.setRecording(audio.clip);
                play = false;
            }

            if (!record)
            {
                playSequenceHit();
            }

            if ( (record || metronome) && sequence % 2 == 0) {
                audio.PlayOneShot(clips.metronome, 1);
            }

            beatTimer -= interval;
            sequence++;
            if (record)
                countIn++;

            if (sequence % (bars * 8) == 0) {
                sequence = 0;
            }
        }
    }

    private void playSequenceHit() {
        for (int j = 0; j < 8; j++) {
            //Drums
            if (j < 4 && drumsArr[sequence, j] && !(solo && currentMode != Categories.Instrument.DRUMS)) {
                if (j == 0) {
                    audio.PlayOneShot(clips.cymbal, 0.5f);
                }
                if (j == 1) {
                    audio.PlayOneShot(clips.hat, 1);
                }
                if (j == 2)
                {
                    audio.PlayOneShot(clips.snare, 1);
                }
                if (j == 3)
                {
                    audio.PlayOneShot(clips.kick, 0.5f);
                }
            }

            //Guitar
            if (guitarArr[sequence, j] && !(solo && currentMode != Categories.Instrument.GUITAR)) {
                if (j == 0) {
                    audio.PlayOneShot(clips.gc2, 0.5f);
                }
                if (j == 1)
                {
                    audio.PlayOneShot(clips.gd2, 0.5f);
                }
                if (j == 2)
                {
                    audio.PlayOneShot(clips.ge2, 0.5f);
                }
                if (j == 3)
                {
                    audio.PlayOneShot(clips.gf2, 0.5f);
                }
                if (j == 4)
                {
                    audio.PlayOneShot(clips.gg2, 0.5f);
                }
                if (j == 5)
                {
                    audio.PlayOneShot(clips.ga2, 0.5f);
                }
                if (j == 6)
                {
                    audio.PlayOneShot(clips.gb2, 0.5f);
                }
                if (j == 7)
                {
                    audio.PlayOneShot(clips.gc3, 0.5f);
                }
            }

            //Piano
            if (pianoArr[sequence, j] && !(solo && currentMode != Categories.Instrument.PIANO))
            {
                if (j == 0)
                {
                    audio.PlayOneShot(clips.pc2, 0.5f);
                }
                if (j == 1)
                {
                    audio.PlayOneShot(clips.pd2, 0.5f);
                }
                if (j == 2)
                {
                    audio.PlayOneShot(clips.pe2, 0.5f);
                }
                if (j == 3)
                {
                    audio.PlayOneShot(clips.pf2, 0.5f);
                }
                if (j == 4)
                {
                    audio.PlayOneShot(clips.pg2, 0.5f);
                }
                if (j == 5)
                {
                    audio.PlayOneShot(clips.pa2, 0.5f);
                }
                if (j == 6)
                {
                    audio.PlayOneShot(clips.pb2, 0.5f);
                }
                if (j == 7)
                {
                    audio.PlayOneShot(clips.pc3, 0.5f);
                }
            }

            //Bass
            if (bassArr[sequence, j] && !(solo && currentMode != Categories.Instrument.BASS))
            {
                if (j == 0)
                {
                    audio.PlayOneShot(clips.bc2, 0.5f);
                }
                if (j == 1)
                {
                    audio.PlayOneShot(clips.bd2, 0.5f);
                }
                if (j == 2)
                {
                    audio.PlayOneShot(clips.be2, 0.5f);
                }
                if (j == 3)
                {
                    audio.PlayOneShot(clips.bf2, 0.5f);
                }
                if (j == 4)
                {
                    audio.PlayOneShot(clips.bg2, 0.5f);
                }
                if (j == 5)
                {
                    audio.PlayOneShot(clips.ba2, 0.5f);
                }
                if (j == 6)
                {
                    audio.PlayOneShot(clips.bb2, 0.5f);
                }
                if (j == 7)
                {
                    audio.PlayOneShot(clips.bc3, 0.5f);
                }
            }

            //Sampler
            if (guitarArr[sequence, j] && !(solo && currentMode != Categories.Instrument.DRUMS))
            {
                if (j == 0 && clips.s1 != null)
                {
                    audio.PlayOneShot(clips.s1, 0.5f);
                }
                if (j == 1 && clips.s2 != null)
                {
                    audio.PlayOneShot(clips.s2, 0.5f);
                }
                if (j == 2 && clips.s3 != null)
                {
                    audio.PlayOneShot(clips.s3, 0.5f);
                }
                if (j == 3 && clips.s4 != null)
                {
                    audio.PlayOneShot(clips.s4, 0.5f);
                }
                if (j == 4 && clips.s5 != null)
                {
                    audio.PlayOneShot(clips.s5, 0.5f);
                }
                if (j == 5 && clips.s6 != null)
                {
                    audio.PlayOneShot(clips.s6, 0.5f);
                }
                if (j == 6 && clips.s7 != null) 
                {
                    audio.PlayOneShot(clips.s7, 0.5f);
                }
                if (j == 7 && clips.s8 != null)
                {
                    audio.PlayOneShot(clips.s8, 0.5f);
                }
            }
        }

    }

    private void resetBoard() {
        Transform gameCubes = transform.Find("GameCubes");

        foreach (Transform child in gameCubes)
        {
            SoundCubeControl scc = child.GetComponent<SoundCubeControl>();
            if (scc != null)
            {
                scc.clear();

                if (currentMode == Categories.Instrument.DRUMS)
                {
                    if (getDrumSequenceAt(scc.position))
                    {
                        scc.highlightThis();
                    }
                }
                else if (currentMode == Categories.Instrument.GUITAR) {
                    if (getGuitarSequenceAt(scc.position))
                    {
                        scc.highlightThis();
                    }
                }
                else if (currentMode == Categories.Instrument.BASS)
                {
                    if (getBassSequenceAt(scc.position))
                    {
                        scc.highlightThis();
                    }
                }
                else if (currentMode == Categories.Instrument.PIANO)
                {
                    if (getPianoSequenceAt(scc.position))
                    {
                        scc.highlightThis();
                    }
                }
                else if (currentMode == Categories.Instrument.SAMPLER)
                {
                    if (getSamplerSequenceAt(scc.position))
                    {
                        scc.highlightThis();
                    }
                }
            }
        }
    }
}
