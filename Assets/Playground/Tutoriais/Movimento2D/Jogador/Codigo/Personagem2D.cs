using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Personagem2D : MonoBehaviour
{
    [SerializeField]
    ETipoMovimentoPlataforma2D tipoMovimento = ETipoMovimentoPlataforma2D.Velocidade;
    ETipoMovimentoPlataforma2D tipoMovimentoAnterior = ETipoMovimentoPlataforma2D.Velocidade;

    [SerializeField] Vida _vida;

    MovimentoPlataforma2D Movimento2D;
    MovimentoPlataforma2D_Simples Movimento2D_Simples;

    IMovimento interfaceMovimento;

    [Header("Extras")]
    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] float impulseForce = 5f;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    // Inputs
    private Vector2 direcao;

    // Start is called before the first frame update
    void Awake()
    {
        Movimento2D = GetComponent<MovimentoPlataforma2D>();
        Movimento2D_Simples = GetComponent<MovimentoPlataforma2D_Simples>();
        AlterarMovimento(true);
    }

    private void OnEnable()
    {
        if (_vida)
        {
            _vida.Morreu += Renascer;
        }
    }

    private void OnDisable()
    {
        if (_vida)
        {
            _vida.Morreu -= Renascer;
        }
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

        if (Input.GetButtonDown("Jump"))
        {
            print("jump");
            interfaceMovimento.IniciarPulo();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            interfaceMovimento.PararPulo();
        }
    }

    // Modifica o script de movimento que está ativo
    private void AlterarMovimento(bool forcar = false)
    {
        if (forcar || tipoMovimentoAnterior != tipoMovimento)
        {// se mandar forçar ou for diferente pode executar
            switch (tipoMovimento)
            {
                case ETipoMovimentoPlataforma2D.Velocidade:
                    Movimento2D.enabled = false;
                    Movimento2D_Simples.enabled = true;
                    interfaceMovimento = Movimento2D_Simples;
                    break;
                case ETipoMovimentoPlataforma2D.Forca:
                    Movimento2D.enabled = true;
                    Movimento2D_Simples.enabled = false;
                    interfaceMovimento = Movimento2D;
                    break;
            }

            tipoMovimentoAnterior = tipoMovimento; // atualize valor
        }
    }

    private void Renascer()
    {
        print("Personagem Renasceu");
        Transform posicao = GameManager_Plat2D.instance?.GetRespawnPosition();
        transform.position = posicao.position;
        
        if (impulseSource)
        {
            impulseSource.GenerateImpulse(impulseForce);
        }
    }
}