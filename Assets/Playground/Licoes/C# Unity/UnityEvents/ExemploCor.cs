using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExemploCor : MonoBehaviour
{
    Renderer _renderer;
    public SpaceDelegate spaceDelegate;
         
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (spaceDelegate)
        {
            //spaceDelegate.delegateEvent += MudarCor;
            SpaceDelegate.delegateEvent += MudarCor;

            spaceDelegate.spaceDelegateAction += MudarCor;
            spaceDelegate.doideraFalante += DigaOi;

            spaceDelegate.faleMeuFio += SpaceDelegate.Falei;

            spaceDelegate.doidera += Somar20;
            spaceDelegate.doidera += Somar37;
        }
    }

    private void OnEnable()
    {
        if (spaceDelegate)
        {
            //spaceDelegate.delegateEvent += MudarCor;
            SpaceDelegate.delegateEvent += MudarCor;

            spaceDelegate.spaceDelegateAction += MudarCor;
            spaceDelegate.doideraFalante += DigaOi;

            spaceDelegate.faleMeuFio += SpaceDelegate.Falei;

            spaceDelegate.doidera += Somar20;
            spaceDelegate.doidera += Somar37;
        }
    }

    private void OnDisable()
    {
        if (spaceDelegate)
        {
            //spaceDelegate.delegateEvent -= MudarCor;
            SpaceDelegate.delegateEvent -= MudarCor;

            spaceDelegate.spaceDelegateAction -= MudarCor;
            spaceDelegate.doideraFalante -= DigaOi;

            spaceDelegate.faleMeuFio -= SpaceDelegate.Falei;

            spaceDelegate.doidera -= Somar20;
            spaceDelegate.doidera -= Somar37;
        }
    }

    public void MudarCor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    #region Exemplos com Delegate
    private void DigaOi(string convidado)
    {
        print("Olá " + convidado);
    }

    public int Somar20(int valor)
    {
        print("Somar 20");
        return valor + 20;
    }

    public int Somar37(int valor)
    {
        print("Somar 37");
        return valor + 37;
    }
    #endregion
}
