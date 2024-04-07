using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public enum ScoreType
    {
        Interaction,
        Evaluation
    }
    public ScoreType scoreType;
    public void AddScore(int value)
    {
        score += value;
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt(scoreType.ToString(), score);
        PlayerPrefs.Save();
    }
}
