﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "Score: 0";
    }

    public void Score(int points)
    {
        score += points;
        myText.text = "Score: " + score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
