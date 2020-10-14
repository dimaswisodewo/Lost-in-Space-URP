using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public int sceneCount = 3;
    public bool isGuideShowed;
    public bool isProlog;
    private string nextGameSceneName;
    private int activeSceneIndex;
    private int nextGameIndex = -1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex > 0 && activeSceneIndex < sceneCount)
        {
            UIManager.Instance.SetTitleText("Stage " + activeSceneIndex);
            JsonData.Instance.SetCurrentLevel(SceneManager.GetActiveScene().name);
            JsonData.Instance.UpdateGameData();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return null;
        LoadScene(sceneName);
    }

    public void RestartGame()
    {
        LoadScene(GetActiveSceneName());
    }

    public void LoadNextStage()
    {
        LoadScene(GetNextGameSceneName(ORDER.NEXT));
    }

    public void LoadPreviousStage()
    {
        LoadScene(GetNextGameSceneName(ORDER.PREVIOUS));
    }

    private string GetActiveSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return sceneName;
    }

    public string GetNextGameSceneName(ORDER orderEnum)
    {
        switch (orderEnum)
        {
            case ORDER.PREVIOUS:
                if (activeSceneIndex == 1)
                {
                    nextGameIndex = activeSceneIndex;
                }
                else
                {
                    nextGameIndex = activeSceneIndex - 1;
                }
                break;

            case ORDER.NEXT:
                if (activeSceneIndex == sceneCount - 1)
                {
                    nextGameIndex = -2;
                }
                else
                {
                    nextGameIndex = activeSceneIndex + 1;
                }
                break;
        }

        nextGameSceneName = "Level" + nextGameIndex.ToString();

        if (nextGameIndex == -2)
        {
            isProlog = false;
            nextGameSceneName = "DialogueScene";
        }

        return nextGameSceneName;
    }
}

public enum ORDER
{
    PREVIOUS,
    NEXT
}
