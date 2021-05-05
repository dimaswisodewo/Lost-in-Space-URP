using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBG : MonoBehaviour
{
    public Renderer menuRenderer;
    private Material mat;
    private float offset;

    private void Awake()
    {
        mat = menuRenderer.materials[0];
    }

    private void Update()
    {
        offset -= 0.002f;
        mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
