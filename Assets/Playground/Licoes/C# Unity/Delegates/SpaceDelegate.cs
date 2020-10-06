using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDelegate : MonoBehaviour
{
    public delegate void SpaceDelegateEvent();
    public delegate int NumeroDoido(int seila);

    public static event SpaceDelegateEvent delegateEvent;
    public event NumeroDoido doidera;

    public Action spaceDelegateAction;
    public event Action<string> doideraFalante;

    public Func<string> faleMeuFio;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delegateEvent?.Invoke();
            spaceDelegateAction?.Invoke();
            doideraFalante?.Invoke("Jeremias");

            if (faleMeuFio != null)
            {
                print(faleMeuFio());
            }

            if (doidera != null)
            {
                int valor = doidera(-10);
                print("Valor = " + valor);
            }
        }
    }

    public static string Falei()
    {
        return "nao sei";
    }
    
}
