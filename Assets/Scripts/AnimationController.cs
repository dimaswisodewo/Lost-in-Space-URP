using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    public GameObject rocket;
    public GameObject astronaut;
    public GameObject earth;
    public Animator rocketAnimator;
    public Animator astronautAnimator;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        if (SceneLoader.Instance.isProlog)
        {
            PlayGoHomeAnimation(false);
            rocket.SetActive(true);
            astronaut.SetActive(true);
            earth.SetActive(false);
        }
        else
        {
            rocket.SetActive(false);
            astronaut.SetActive(true);
            earth.SetActive(true);
            PlayCrashedAnimation(false);
            PlayGoHomeAnimation(true);
        }
    }

    private void Update()
    {
        if (DialogueTextManager.Instance.GetCurrentIndexDialogue() == 5) PlayCrashedAnimation(true);
    }

    private void PlayCrashedAnimation(bool setActive)
    {
        rocketAnimator.SetBool("isCrashed", setActive);
        astronautAnimator.SetBool("isCrashed", setActive);
    }

    private void PlayGoHomeAnimation(bool setActive)
    {
        astronautAnimator.SetBool("isGoHome", setActive);
    }
}
