using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject continueButton;

    private void Awake()
    {
        if (JsonData.Instance.gameData.currentLevel == "Level1") continueButton.SetActive(false);
        else continueButton.SetActive(true);
    }

    public void OnStartButtonClicked()
    {
        SceneLoader.Instance.isProlog = true;
        SceneLoader.Instance.LoadScene("DialogueScene");
    }

    public void OnContinueButtonClicked()
    {
        SceneLoader.Instance.LoadScene(JsonData.Instance.gameData.currentLevel);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
