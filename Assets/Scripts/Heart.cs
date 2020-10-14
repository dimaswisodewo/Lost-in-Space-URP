using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Add player health when collide with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Player.Instance.health < 3 && Player.Instance.health > 0) Player.Instance.AddHealth();
            gameObject.SetActive(false);
            SoundManager.Instance.PlayPowerUp();
        }
    }
}
