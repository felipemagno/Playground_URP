using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimacaoPlataforma2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovimentoPlataforma2D : MonoBehaviour, IMovimento
{
    Transform _transform;
    Rigidbody2D _rigidbody2D;
    VerificarChao _verificarChao;
    AnimacaoPlataforma2D animacaoPlataforma;

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
    bool viradoDireita = true;

    [Header("Pulo")]
    [SerializeField]
    float velocidadePulo = 10f;
    [SerializeField]
    float forcaPulo = 10f;
    [SerializeField]
    float duracaoPulo = 0.5f;
    [SerializeField, Tooltip("amplifica a gravidade durante a queda")]
    float multiplicadorQueda = 2f;

    // Variáveis para o "Coyote Jump"
    bool PuloCoyote = false;
    [SerializeField] float DuracaoCoyote = 0.4f;
    Coroutine coroutineCoyote;

    Coroutine rotinaPulo;
    float gravidadeInicial;

    [SerializeField] private EPular meuPulo;

    public EPular EstadoPulo { get => estadoPulo; }

    private void Start()
    {
        _transform = transform;
        // "transform" na verdade é "GetComponent<Transform>()"

        _rigidbody2D = GetComponent<Rigidbody2D>();
        gravidadeInicial = _rigidbody2D.gravityScale;

        //Procura pelo componente do tipo Script e de tipo "VerificarChao"
        _verificarChao = GetComponentInChildren<VerificarChao>();

        animacaoPlataforma = GetComponent<AnimacaoPlataforma2D>();
    }

    private void FixedUpdate()
    {
        Mover();
        Pular();

        meuPulo = estadoPulo;

        // Verifique os seus limites de velocidades e corrija
        _rigidbody2D.velocity = VelocidadeCorrigida();
    }

    #region Movimento Horizontal
    public void Movimento(Vector2 direcao)
    {
        this.direcao = direcao;
        estaMovendo = direcao.sqrMagnitude > 0.1f;

        // verificar direção da animação
        if (estaMovendo)
        {
            viradoDireita = (direcao.x >= 0);
            animacaoPlataforma.Flip(viradoDireita);
            animacaoPlataforma.Andar(true);
        }
        else
        {
            animacaoPlataforma.Andar(false);
        }
    }

    private void Mover()
    {
        // Movimento Horizontal
        if (estaMovendo)
        {
            float forca = direcao.x * aceleracao;
            if (!_verificarChao.EstaNoChao)
            {
                forca *= controleNoAr;
            }

            _rigidbody2D.AddForce(Vector2.right * forca, ForceMode2D.Impulse);
        }
    }

    #endregion

    #region Pulo
    // Tente dar o comando de iniciar o pulo
    public void IniciarPulo()
    {
        if (PossoPular())
        {
            estadoPulo = EPular.pular;
        }
    }

    private bool PossoPular()
    {
        bool resultado = _verificarChao.EstaNoChao && estadoPulo == EPular.podePular;

        // Lógica Coyote Jump
        if (!resultado && PuloCoyote)
        {
            if (coroutineCoyote != null) StopCoroutine(coroutineCoyote);
            PuloCoyote = false;
            print("COYOTE!");
            return true;
        }

        return resultado;
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
                _rigidbody2D.gravityScale = gravidadeInicial * multiplicadorQueda;
                break;
        }
        StopCoroutine(rotinaPulo);
    }

    private IEnumerator InicioCoyote()
    {
        PuloCoyote = true;
        yield return new WaitForSeconds(DuracaoCoyote);
        PuloCoyote = false;
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
                if (!_verificarChao.EstaNoChao)
                {
                    estadoPulo = EPular.caindo;

                    // Temporario, precisaria de uma animação dele caindo
                    animacaoPlataforma.Pular(true);

                    // Lógica Coyote Jump
                    if (coroutineCoyote != null) StopCoroutine(coroutineCoyote);
                    coroutineCoyote = StartCoroutine(InicioCoyote());

                    _rigidbody2D.gravityScale = gravidadeInicial * multiplicadorQueda;
                }
                break;
            // Inicie o pulo
            case EPular.pular:
                _rigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
                rotinaPulo = StartCoroutine(TemporizadorPulo());
                estadoPulo = EPular.pulando;
                animacaoPlataforma.Pular(true);
                break;
            // Enquanto segurar botão de pulo, continue pulando
            case EPular.pulando:
                _rigidbody2D.AddForce(Vector2.up * forcaPulo, ForceMode2D.Force);
                break;
            // Quando estiver caindo, verifique se encontrou o chão
            case EPular.caindo:
                if (_verificarChao.EstaNoChao)
                {
                    animacaoPlataforma.Pular(false);
                    estadoPulo = EPular.podePular;
                    _rigidbody2D.gravityScale = gravidadeInicial;
                }
                break;
        }
    }
    #endregion

    #region Auxiliares
    private Vector2 VelocidadeCorrigida()
    {
        Vector2 velocidadeCorrigida = _rigidbody2D.velocity;

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