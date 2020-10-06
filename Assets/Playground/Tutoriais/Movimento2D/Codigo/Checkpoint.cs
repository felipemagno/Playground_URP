using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameManager_Plat2D.instance.ReceberCheckpoint(this);
        GameManager_Plat2D.instance?.ReceberCheckpoint(this);        
    }

    public void ActivateCheckpoint() => _renderer.material.color = Color.green;

    public void DeactivateCheckpoint() => _renderer.material.color = Color.white;
}
