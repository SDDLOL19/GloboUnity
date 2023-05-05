using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    public Text scoreText;
    int score = 0;

    private void Start()
    {
        scoreManager = this;
    }
    public void RaiseScore(int s)
    {
        score += s;
        //Debug.Log(score);//para escribirlo en la consola
        scoreText.text = score + "";

    }
}
