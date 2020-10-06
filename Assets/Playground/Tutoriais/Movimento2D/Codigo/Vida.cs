using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Vida : MonoBehaviour
{
    [SerializeField] float _vidaMaxima = 100;
    [SerializeField] float _vida = 100;

    public event Action<float> Machucou;
    public event Action Morreu;

    CinemachineImpulseSource impulseSource;

    private void Start()
    {
        Machucou += TesteDano;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void LevarDano(float valor)
    {
        Machucou?.Invoke(valor);
        _vida -= valor;           
        _vida = Mathf.Clamp(_vida, 0, _vidaMaxima);
        if ( _vida <= 0.1)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        //Morreu();
        Morreu?.Invoke();

    }

    private void TesteDano(float valor)
    {
        print("Machucou muito, foi desse tamanho oh! " + valor);
        impulseSource?.GenerateImpulse(2);
    }


}
