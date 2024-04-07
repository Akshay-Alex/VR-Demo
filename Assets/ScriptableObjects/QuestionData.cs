using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

[Serializable]
public class QuestionData
{
    [SerializeField]
    public string Question;
    [SerializeField]
    public List<string> Options;
    public int CorrectAnswerIndex;
    public AudioClip QuestionAudio;
}
