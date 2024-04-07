using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image image;
    public float timeRemaining = 60f;
    public float totalTime = 0.1f;
    private bool timerIsRunning = false;
    public UnityEvent OnTimerRanOut;

    void Start()
    {
    }
    public void StartTimer(float time)
    {
        totalTime = time;
        timeRemaining = time;
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                UpdateUI(timeRemaining);
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateUI(0f);
                OnTimerRanOut?.Invoke();
            }
        }
    }

    void UpdateUI(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        image.fillAmount = timeRemaining / totalTime;
    }
}
