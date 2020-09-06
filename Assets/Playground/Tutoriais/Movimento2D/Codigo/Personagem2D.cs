using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem2D : MonoBehaviour
{
    [SerializeField]
    ETipoMovimento tipoMovimento = ETipoMovimento.Velocidade;

    MovimentoPlataforma2D Movimento2D;
    MovimentoPlataforma2D_Simples Movimento2D_Simples;

    IMovimento interfaceMovimento;

    // Inputs
    private Vector2 direcao;

    // Start is called before the first frame update
    void Awake()
    {
        Movimento2D = GetComponent<MovimentoPlataforma2D>();
        Movimento2D_Simples = GetComponent<MovimentoPlataforma2D_Simples>();
        AlterarMovimento();
    }

    // Update is called once per frame
    void Update()
    {
        // Altera qual script de movimento está sendo utilizado
        AlterarMovimento();

        // No Update, recebemos Inputs
        direcao.x = Input.GetAxisRaw("Horizontal");
        direcao.y = Input.GetAxisRaw("Vertical");

        interfaceMovimento.Movimento(direcao);

        if (Input.GetButtonDown("Jump"))// && myVerificarChao.EstaNoChao && estadoPulo == EPular.podePular)
        {
            interfaceMovimento.IniciarPulo();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            interfaceMovimento.PararPulo();
        }
    }

    // Modifica o script de movimento que está ativo
    private void AlterarMovimento()
    {
        switch (tipoMovimento)
        {
            case ETipoMovimento.Velocidade:
                Movimento2D.enabled = false;
                Movimento2D_Simples.enabled = true;
                interfaceMovimento = Movimento2D_Simples;
                break;
            case ETipoMovimento.Forca:
                Movimento2D.enabled = true;
                Movimento2D_Simples.enabled = false;
                interfaceMovimento = Movimento2D;
                break;
        }
    }
}