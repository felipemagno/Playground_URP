using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlataforma2D : MonoBehaviour, IMovimento
{
    Transform myTransform;
    Rigidbody2D myRigidbody2D;
    VerificarChao myVerificarChao;

    private EPular estadoPulo = EPular.podePular;

    [Header("Movimento Horizontal")]
    [SerializeField]
    float velocidade = 10f;
    [SerializeField]
    float aceleracao = 3f;
    [SerializeField, Range(0, 1), Tooltip("O quanto o jogador consegue alterar a direcao no ar")]
    float controleNoAr = 1f;

    Vector2 direcao = Vector2.zero;
    bool estaMovendo = false;

    [Header("Pulo")]
    [SerializeField]
    float velocidadePulo = 10f;
    [SerializeField]
    float forcaPulo = 10f;
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
        Mover();
        Pular();

        meuPulo = estadoPulo.ToString();

        // Verifique os seus limites de velocidades e corrija
        myRigidbody2D.velocity = VelocidadeCorrigida();
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
            float forca = direcao.x * aceleracao;
            if (!myVerificarChao.EstaNoChao)
            {
                forca *= controleNoAr;
            }

            myRigidbody2D.AddForce(Vector2.right * forca, ForceMode2D.Impulse);
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
                myRigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
                rotinaPulo = StartCoroutine(TemporizadorPulo());
                estadoPulo = EPular.pulando;
                break;
            // Enquanto segurar botão de pulo, continue pulando
            case EPular.pulando:
                myRigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Force);
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

    #region Auxiliares
    private Vector2 VelocidadeCorrigida()
    {
        Vector2 velocidadeCorrigida = myRigidbody2D.velocity;

        // verificar velocidade maxima horizontal
        if (Mathf.Abs(velocidadeCorrigida.x) > velocidade)
        {
            velocidadeCorrigida.x = Mathf.Clamp(velocidadeCorrigida.x, -velocidade, velocidade);
        }

        // verificar velocidade maxima de subida no pulo
        if (velocidadeCorrigida.y > velocidadePulo)
        {
            velocidadeCorrigida.y = velocidadePulo;
        }

        return velocidadeCorrigida;
    }
    #endregion
}