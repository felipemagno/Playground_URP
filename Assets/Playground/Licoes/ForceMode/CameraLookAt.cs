using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    Transform myCamera;
    [SerializeField] Transform LookTarget;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (LookTarget && myCamera)
        {
            myCamera.LookAt(LookTarget);
        }
    }
}
