using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    public int HighScore
    {
        get {
            if (!PlayerPrefs.HasKey("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", 0);
            }
            return PlayerPrefs.GetInt("HighScore");
        }
    }

    public void Reset()
    {
        Score = 0;
    }

    public void GainPoints(int value)
    {
        Score += value;
    }

    public void UpdateHighScore()
    {
        if (Score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }
}
