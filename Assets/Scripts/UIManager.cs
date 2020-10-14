using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public List<GameObject> heartList = new List<GameObject>();
    public Text bulletText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject backPanel;
    public GameObject pausePanel;
    public Text titleText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        SetGameOverPanel(false);
        SetWinPanel(false);
    }

    public void OnRestartButtonClicked()
    {
        SceneLoader.Instance.RestartGame();
    }

    public void OnPreviousStageButtonClicked()
    {
        SceneLoader.Instance.LoadPreviousStage();
    }

    public void OnNextStageButtonClicked()
    {
        SceneLoader.Instance.LoadNextStage();
    }

    public void SetActiveHeart(int index, bool setActive)
    {
        heartList[index].SetActive(setActive);
    }

    public void SetBulletText(string inputString)
    {
        SetText(bulletText, inputString);
    }

    public void SetGameOverPanel(bool setActive)
    {
        SetActiveObject(gameOverPanel, setActive);
    }

    public void SetWinPanel(bool setActive)
    {
        SetActiveObject(winPanel, setActive);
    }

    public void SetBackPanel(bool setActive)
    {
        SetActiveObject(backPanel, setActive);
    }

    public void SetTitleText(string inputString)
    {
        SetText(titleText, inputString);
    }

    private void SetText(Text target, string inputString)
    {
        target.text = inputString;
    }

    private void SetActiveObject(GameObject obj, bool setActive)
    {
        obj.SetActive(setActive);
    }

    public void SetPausePanel(bool setActive)
    {
        pausePanel.SetActive(setActive);
    }
}
