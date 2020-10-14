using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public AudioListener audioListener;
    private bool isPaused;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (Player.Instance.health <= 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!isPaused);
        }
    }

    private void GameOver()
    {
        Player.Instance.isAlive = false;
        Player.Instance.ChangePlayerSpriteColor(COLOR.RED);
        UIManager.Instance.SetGameOverPanel(true);
    }

    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        AudioListener.pause = pause;
        UIManager.Instance.SetPausePanel(pause);
        Debug.Log("Pause " + pause);
        isPaused = !isPaused;
    }

    public void onBackToMainMenuButtonClicked()
    {
        PauseGame(false);
        SceneLoader.Instance.LoadScene("MenuScene");
    }
}
