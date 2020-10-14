using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCore : MonoBehaviour
{
    public bool isPlayerCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.Instance.isAlive)
        {
            isPlayerCollide = true;
        }
    }

    public void SetActiveTrigger(bool setActive)
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = setActive;
    }
}
