using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text score;
    void OnEnable()
    {
        score = GetComponent<Text>();
        score.text = GameManager.Instance.Score.ToString();
        
    }

    
}
