using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlataforma2D_Simples : MonoBehaviour, IMovimento
{
    Transform myTransform;
    Rigidbody2D myRigidbody2D;
    VerificarChao myVerificarChao;

    private EPular estadoPulo = EPular.podePular;

    Vector2 velocidadeFinal;

    [Header("Movimento Horizontal")]
    [SerializeField]
    float velocidade = 10f;

    Vector2 direcao = Vector2.zero;
    bool estaMovendo = false;

    [Header("Pulo")]
    [SerializeField]
    float velocidadePulo = 10f;
    [SerializeField]
    float duracaoPulo = 0.5f;
    [SerializeField, Tooltip("amplifica a gravidade durante a queda")]
    float multiplicadorQueda = 2f;

    Coroutine rotinaPulo;
    float gravidadeInicial;

    public string meuPulo;

    public EPular EstadoPulo { get => estadoPulo; }

    private void Start()
    {
        myTransform = transform;
        // "transform" na verdade é "GetComponent<Transform>()"

        myRigidbody2D = GetComponent<Rigidbody2D>();
        gravidadeInicial = myRigidbody2D.gravityScale;

        //Procura pelo componente do tipo Script e de tipo "VerificarChao"
        myVerificarChao = GetComponentInChildren<VerificarChao>();
    }

    private void FixedUpdate()
    {
        velocidadeFinal = myRigidbody2D.velocity;

        Mover();
        Pular();

        meuPulo = estadoPulo.ToString();

        // Verifique os seus limites de velocidades e corrija
        myRigidbody2D.velocity = velocidadeFinal;
    }

    #region Movimento Horizontal
    public void Movimento(Vector2 direcao)
    {
        this.direcao = direcao;
        estaMovendo = direcao.sqrMagnitude > 0.1f;
    }

    private void Mover()
    {
        // Movimento Horizontal
        if (estaMovendo)
        {
            velocidadeFinal.x = velocidade * direcao.x;
        }
    }
    #endregion

    #region Pulo
    // Tente dar o comando de iniciar o pulo
    public void IniciarPulo()
    {
        if (myVerificarChao.EstaNoChao && estadoPulo == EPular.podePular)
        {
            estadoPulo = EPular.pular;
        }
    }

    // Avise para parar o pulo
    public void PararPulo()
    {
        switch (estadoPulo)
        {
            case EPular.pular:
                estadoPulo = EPular.podePular;
                break;
            case EPular.pulando:
                estadoPulo = EPular.caindo;
                myRigidbody2D.gravityScale = gravidadeInicial * multiplicadorQueda;
                StopCoroutine(rotinaPulo);
                break;
        }
        StopCoroutine(rotinaPulo);
    }

    // Temporizador para avisar a parar de pular mesmo segurando botão de pulo
    private IEnumerator TemporizadorPulo()
    {
        yield return new WaitForSeconds(duracaoPulo);
        PararPulo();
    }

    // Comportamento do Pulo
    private void Pular()
    {
        switch (estadoPulo)
        {
            // Verifique se começou a cair
            case EPular.podePular:
                if (!myVerificarChao.EstaNoChao)
                {
                    estadoPulo = EPular.caindo;
                    myRigidbody2D.gravityScale = gravidadeInicial * multiplicadorQueda;
                }
                break;

            // Inicie o pulo
            case EPular.pular:
                velocidadeFinal.y = velocidadePulo;
                rotinaPulo = StartCoroutine(TemporizadorPulo());
                estadoPulo = EPular.pulando;
                break;

            // Enquanto segurar botão de pulo, continue pulando
            case EPular.pulando:
                velocidadeFinal.y = velocidadePulo;
                break;

            // Quando estiver caindo, verifique se encontrou o chão
            case EPular.caindo:
                if (myVerificarChao.EstaNoChao)
                {
                    estadoPulo = EPular.podePular;
                    myRigidbody2D.gravityScale = gravidadeInicial;
                }
                break;
        }
    }
    #endregion

}