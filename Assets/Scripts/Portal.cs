using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform player;
    public Transform portalCenter;
    public bool isPortalIn;
    public PortalCore portalCore;

    private void Start()
    {
        if (!isPortalIn)
        {
            StartCoroutine(DisableTriggerForSeconds(2f));
            MoveOutOfPortal();
        }
    }

    private void Update()
    {
        // if player collide with portal core will go to next or previous level
        if (portalCore.isPlayerCollide)
        {
            player.gameObject.SetActive(false);

            if (!isPortalIn)
            {
                UIManager.Instance.SetBackPanel(true);
                JsonData.Instance.SetCurrentLevel(SceneLoader.Instance.GetNextGameSceneName(ORDER.PREVIOUS));
            }
            else
            {
                UIManager.Instance.SetWinPanel(true);
                JsonData.Instance.SetCurrentLevel(SceneLoader.Instance.GetNextGameSceneName(ORDER.NEXT));
            }

            //JsonData.Instance.UpdateGameData();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player enter the portal area, player will be pulled to the portal core
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            MoveToPortal(rb);
            RotateToDegree(collision.transform);
            SoundManager.Instance.PlayPortal();
        }        
    }

    // move player position to portal position
    private void MoveToPortal(Rigidbody2D rb)
    {
        Tweening.Instance.MoveToPosition(rb, portalCenter.position);
    }

    // rotate player 3x
    private void RotateToDegree(Transform transform)
    {
        Tweening.Instance.RotateToDegree(transform, new Vector3(0, 0, 1080), 2f);
    }

    // called on start to give player animation get out from portal
    private void MoveOutOfPortal()
    {
        player.position = transform.position;
        Vector2 destination = new Vector2(player.position.x + 3, player.position.y);

        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();
        Tweening.Instance.MoveToPosition(rbPlayer, destination);
        RotateToDegree(player.transform);
        SoundManager.Instance.PlayPortal();
    }

    private void SetActiveTrigger(bool setActive)
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = setActive;
    }

    // disable portal collider on the first 2 seconds, because player will get out from portal on start
    private IEnumerator DisableTriggerForSeconds(float second)
    {
        SetActiveTrigger(false);
        portalCore.SetActiveTrigger(false);


        yield return new WaitForSeconds(second);

        SetActiveTrigger(true);
        portalCore.SetActiveTrigger(true);
    }
}
