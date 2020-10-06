using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnabler : MonoBehaviour
{
 
    void Start()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }

}
