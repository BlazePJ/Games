﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
     public Text highscore;
    void OnEnable()
    {
        highscore = GetComponent<Text>();
        highscore.text ="HighScore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
