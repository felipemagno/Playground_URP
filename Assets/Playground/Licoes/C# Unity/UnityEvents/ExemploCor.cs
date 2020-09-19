using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExemploCor : MonoBehaviour
{
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void MudarCor()
    {
        renderer.material.color = Random.ColorHSV();
    }
}
