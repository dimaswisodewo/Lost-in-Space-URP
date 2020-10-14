using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePanel : MonoBehaviour
{
    private void Start()
    {
        if (SceneLoader.Instance.isGuideShowed == false)
        {
            gameObject.SetActive(true);
            SceneLoader.Instance.isGuideShowed = true;
        }
        else gameObject.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
