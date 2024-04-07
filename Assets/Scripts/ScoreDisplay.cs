using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using System.Text;

[Serializable]
public class ScoreData
{
    public string practicalScore;
    public string evaluationScore;
    public string DeviceID;
    public ScoreData(string _id, string pscore, string escore)
    {
        this.DeviceID = _id;
        this.practicalScore = pscore;
        this.evaluationScore = escore;
    }
}
public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI interactionScoreText;
    public TextMeshProUGUI EvaluationScoreText;
    int interactionScore, evaluationScore;
    string url = "https://6611b66e95fdb62f24eda120.mockapi.io/ScoreData";
    string deviceID;
    private void Awake()
    {
        interactionScore = PlayerPrefs.GetInt(ScoreManager.ScoreType.Interaction.ToString());
        evaluationScore = PlayerPrefs.GetInt(ScoreManager.ScoreType.Evaluation.ToString());
        interactionScoreText.text = "Practical score : " + interactionScore;
        EvaluationScoreText.text = "Evaluation score : " + evaluationScore;
        deviceID = SystemInfo.deviceUniqueIdentifier;
        //ScoreData data = new ScoreData(deviceID, interactionScore.ToString(), evaluationScore.ToString());
        string data = "{ \"practicalScore\":\"" +interactionScore+ "\",\"evaluationScore\":\"" +evaluationScore+ "\",\"DeviceID\":\""+ deviceID+"\"}";
        //Debug.Log("string is " + data.ToString());     
        StartCoroutine(Upload(url,data));
    }
    /*
    IEnumerator Upload(string data)
    {
        using (UnityWebRequest www = UnityWebRequest.Put("https://6611b66e95fdb62f24eda120.mockapi.io/ScoreData", data))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    */
    IEnumerator Upload(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
