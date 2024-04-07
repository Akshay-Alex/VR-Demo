using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/QuestionsData")]
public class QuestionsData : ScriptableObject
{
    public List<QuestionData> Questions;
}
