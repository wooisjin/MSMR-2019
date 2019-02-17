using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Categories : MonoBehaviour {

    public enum Instrument : int { DRUMS = 0, GUITAR = 1, PIANO = 2, BASS = 3, SAMPLER = 4, BLANK = 5};

    public static Color drums = new Color(26/255.0f, 234/255.0f, 234 / 255.0f, 1.0f);
    public static Color guitar = new Color(180 / 255.0f, 171 / 255.0f, 49 / 255.0f, 1.0f);
    public static Color bass = new Color(212 / 255.0f, 55 / 255.0f, 55 / 255.0f, 1.0f);
    public static Color sampler = new Color(84 / 255.0f, 176 / 255.0f, 48 / 255.0f, 1.0f);
    public static Color piano = new Color(89 / 255.0f, 129 / 255.0f, 221 / 255.0f, 1.0f);

    public static Color normal = new Color(1, 1, 1, 1);
    public static Color highlight = new Color(9 / 255.0f, 0.0f,251 / 255.0f, 1.0f);
}
