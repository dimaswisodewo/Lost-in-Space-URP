using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonData : MonoBehaviour
{
    public static JsonData Instance;
    public GameData gameData;
    private string fileName = "GameData.json";
    private string fullPath;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(LoadGameDataAtStart());
    }

    private IEnumerator LoadGameDataAtStart()
    {
        yield return StartCoroutine(LoadGameData(fileName));
        yield return StartCoroutine(LoadSceneCoroutine("MenuScene"));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return null;
        SceneLoader.Instance.LoadScene(sceneName);
    }

    public void UpdateGameData()
    {
        string gameDataString = JsonUtility.ToJson(gameData, true);
        System.IO.File.WriteAllText(fullPath, gameDataString);
    }

    public IEnumerator LoadGameData(string fileName)
    {
        string path = Application.streamingAssetsPath;
        fullPath = System.IO.Path.Combine(path, fileName);

        UnityWebRequest request = UnityWebRequest.Get(fullPath);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("Request Error: " + request.error);
        }

        string downloadedText = request.downloadHandler.text;
        Debug.Log(fullPath);
        Debug.Log(downloadedText);

        gameData = JsonUtility.FromJson<GameData>(downloadedText);
    }

    //private string PathCorrection(string inputPath, string inputFileName)
    //{
    //    string fullPath;

    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        fullPath = System.IO.Path.Combine(inputPath, inputFileName);
    //        //fullPath = "jar:file://" + Application.dataPath + "!/assets/" + inputFileName;
    //    }
    //    else
    //    {
    //        fullPath = System.IO.Path.Combine(inputPath, inputFileName);
    //    }

    //    return fullPath;
    //}

    public void SetCurrentLevel(string inputCurrentLevel)
    {
        gameData.currentLevel = inputCurrentLevel;
    }
}

[System.Serializable]
public class GameData
{
    public string[] prologDialogue;
    public string[] epilogDialogue;
    public string currentLevel;
}