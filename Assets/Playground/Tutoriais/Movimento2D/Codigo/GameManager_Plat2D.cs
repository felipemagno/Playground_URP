using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Plat2D : MonoBehaviour
{
    public static GameManager_Plat2D instance;
    private Checkpoint checkpointAtual;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static bool ReceberCheckpointStatic(Checkpoint checkpoint)
    {
        if (instance)
        {
            return instance.ReceberCheckpoint(checkpoint);
        }
        else
        {
            return false;
        }
    }

    public static Transform GetRespawnPoint()
    {
        return instance.GetRespawnPosition();
    }

    public bool ReceberCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint != checkpointAtual)
        {
            checkpointAtual = checkpoint;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform GetRespawnPosition()
    {
        if (checkpointAtual != null)
        {
            return checkpointAtual.transform;
        }
        else
            return null;
    }
}
