using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GameManager_Plat2D.instance.ReceberCheckpoint(this);
        GameManager_Plat2D.instance?.ReceberCheckpoint(this);
    }
}
