using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clips : MonoBehaviour {

    public AudioClip kick;
    public AudioClip snare;
    public AudioClip hat;
    public AudioClip cymbal;

    public AudioClip metronome;

    public AudioClip gc2;
    public AudioClip gd2;
    public AudioClip ge2;
    public AudioClip gf2;
    public AudioClip gg2;
    public AudioClip ga2;
    public AudioClip gb2;
    public AudioClip gc3;

    public AudioClip pd2;
    public AudioClip pc2;
    public AudioClip pe2;
    public AudioClip pf2;
    public AudioClip pg2;
    public AudioClip pa2;
    public AudioClip pb2;
    public AudioClip pc3;

    public AudioClip bc2;
    public AudioClip bd2;
    public AudioClip be2;
    public AudioClip bf2;
    public AudioClip bg2;
    public AudioClip ba2;
    public AudioClip bb2;
    public AudioClip bc3;

    public AudioClip s1;
    public AudioClip s2;
    public AudioClip s3;
    public AudioClip s4;
    public AudioClip s5;
    public AudioClip s6;
    public AudioClip s7;
    public AudioClip s8;

    public int count;

    public void Awake() {
        count = 0;
    }

    public void setRecording(AudioClip ac) {
        if (count == 0) {
            s1 = ac;
        }
        if (count == 1)
        {
            s2 = ac;
        }
        if (count == 2)
        {
            s3 = ac;
        }
        if (count == 3)
        {
            s4 = ac;
        }
        if (count == 4)
        {
            s5 = ac;
        }
        if (count == 5)
        {
            s6 = ac;
        }
        if (count == 6)
        {
            s7 = ac;
        }
        if (count == 7)
        {
            s8 = ac;
        }
        count++;
    }

    public void clear() {
        s1 = null;
        s2 = null;
        s3 = null;
        s4 = null;
        s5 = null;
        s6 = null;
        s7 = null;
        s8 = null;
    }

}
