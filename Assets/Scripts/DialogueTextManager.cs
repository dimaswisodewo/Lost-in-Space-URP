using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTextManager : MonoBehaviour
{
    public static DialogueTextManager Instance;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public GameObject continueButton;
    public bool isProlog;
    private float textSpeed = 0.03f;
    private int index = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        isProlog = SceneLoader.Instance.isProlog;

        if (isProlog)
        {
            sentences = JsonData.Instance.gameData.prologDialogue;
        }
        else
        {
            sentences = JsonData.Instance.gameData.epilogDialogue;
        }
    }

    private void Start()
    {
        StartCoroutine(PlayDialogueText(sentences[index]));
    }

    private void Update()
    {
        if (index < sentences.Length && textDisplay.text == sentences[index]) continueButton.SetActive(true);
    }

    public void OnContinueButtonClicked()
    {
        textDisplay.text = "";
        continueButton.SetActive(false);
        NextSentence();
    }

    public int GetCurrentIndexDialogue()
    {
        return index;
    }

    private IEnumerator PlayDialogueText(string inputString)
    {
        foreach (char character in inputString.ToCharArray())
        {
            textDisplay.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        if (index < sentences.Length && textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    private void NextSentence()
    {
        index++;

        if (index < sentences.Length)
        {
            StartCoroutine(PlayDialogueText(sentences[index]));
        }
        else
        {
            textDisplay.text = "";

            if (isProlog) SceneLoader.Instance.LoadScene("Level1");
            else
            {
                SceneLoader.Instance.LoadScene("MenuScene");
            }
        }
    }
}
