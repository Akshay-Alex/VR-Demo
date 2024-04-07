using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MCQManager : MonoBehaviour
{
    public QuestionsData questionsData;
    public TextMeshProUGUI questionText;
    public Transform answersGridParentObject;
    public GameObject OptionPrefab;
    public AudioSource audioSource;
    public QuestionData currentQuestion;
    public int score;
    public int minimumScore;
    public int questionIndex;
    public UnityEvent OnCorrectAnswerSubmitted;
    public UnityEvent OnWrongAnswerSubmitted;
    public UnityEvent OnAnswerSubmitted;
    public UnityEvent OnMCQComplete;
    public UnityEvent OnMCQFailed;
    public UnityEvent OnMCQPassed;
    public void OptionSelected(int index)
    {
        if(currentQuestion.CorrectAnswerIndex == index)
        {
            CorrectOptionSelected();
        }
        else
        {
            WrongOptionSelected();
        }
        OnAnswerSubmitted?.Invoke();
        LoadNextQuestion();
    }
    public void CorrectOptionSelected()
    {
        score++;
        OnCorrectAnswerSubmitted?.Invoke();
    }
    public void WrongOptionSelected()
    {
        OnWrongAnswerSubmitted?.Invoke();
    }
    void InitializeMCQ()
    {
        score = 0;
        questionIndex = 0;
    }
    public void LoadNextQuestion()
    {
        if(questionIndex < questionsData.Questions.Count)
        {
            currentQuestion = questionsData.Questions[questionIndex];
            questionIndex++;
            DestroyAllOptions();
            GenerateOptions();
            SetQuestionText();
            PlayQuestionAudio();
        }
        else
        {
            OnMCQComplete?.Invoke();
            if(score<minimumScore)
            {
                OnMCQFailed?.Invoke();
            }
            else
            {
                OnMCQPassed?.Invoke();
            }
        }
    }
    void SetQuestionText()
    {
        questionText.text = currentQuestion.Question;
    }
    public void GenerateOptions()
    {
        int currentOptionIndex = 0;
        foreach (string option in currentQuestion.Options)
        {
            var newOption = GameObject.Instantiate(OptionPrefab, answersGridParentObject);
            newOption.GetComponent<Option>().InitializeOption(currentOptionIndex, option, this);
            currentOptionIndex++;
        }
    }
    void DestroyAllOptions()
    {
        foreach (Transform child in answersGridParentObject)
        {
            Destroy(child.gameObject);
        }
    }
    void PlayQuestionAudio()
    {
        if(currentQuestion.QuestionAudio != null)
        {
            audioSource.clip = currentQuestion.QuestionAudio;
            audioSource.Play();
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeMCQ();
        LoadNextQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
