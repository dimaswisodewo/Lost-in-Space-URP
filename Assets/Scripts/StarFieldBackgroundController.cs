using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFieldBackgroundController : MonoBehaviour
{
    private Vector2 startPos;
    public GameObject cam;
    private float parallaxEffect = 0.15f;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        //Vector2 temp = transform.position;

        float xPos = startPos.x + cam.transform.position.x * -1 * parallaxEffect;
        float yPos = startPos.y + cam.transform.position.y * -1 * parallaxEffect;

        //transform.position = Vector2.Lerp(temp, new Vector2(xPos, yPos), 1f);
        transform.position = new Vector2(xPos, yPos);
    }

}
