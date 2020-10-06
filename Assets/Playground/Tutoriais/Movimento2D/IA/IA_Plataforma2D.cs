using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(IMovimento))]
public class IA_Plataforma2D : MonoBehaviour
{
    IMovimento _movimento;
    Collider2D _collider;
    Bounds _bounds => _collider.bounds;

    bool olhandoParaDireita = true;
    [Header("Arma da IA")]
    [SerializeField] Dano arma;
    [SerializeField] float offSetArma = 0.2f;

    [Header("Deteccao de Obstaculos e Buracos")]
    [SerializeField] bool debug = true;

    [Header("Detectar Obstaculos no Caminho")]
    [SerializeField] LayerMask obstaculos;
    [SerializeField] float offsetObstaculo = 0.5f;
    [SerializeField] float raioObstaculo = 0.5f;

    [Header("Detectar Chao")]
    [SerializeField] LayerMask chao;
    [SerializeField] float offsetChao = 0.5f;
    [SerializeField] float raioChao = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        _movimento = GetComponent<IMovimento>();
        if (_movimento == null)
        {
            Debug.LogError("ERRO: IA sem script de Movimentação!");
        }

        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.LogError("ERRO: IA sem Colisor!");
        }

        Vector3 offsetOrigem = new Vector3(_bounds.extents.x + offSetArma, 0,0);        
        arma.transform.position = _bounds.center + offsetOrigem;
    }

    // Update is called once per frame
    void Update()
    {
        if (_movimento == null) return;

        if (MudarDirecao())
        {
            olhandoParaDireita = !olhandoParaDireita;

            // Mudar posição da arma de dano
            Vector3 offsetOrigem = new Vector3(_bounds.extents.x + offSetArma, 0, 0);
            offsetOrigem = olhandoParaDireita ? offsetOrigem : -offsetOrigem;
            arma.transform.position = _bounds.center + offsetOrigem;
        }

        if (olhandoParaDireita)
        {
            _movimento.Movimento(Vector2.right);
        }
        else
        {
            _movimento.Movimento(Vector2.left);
        }
    }

    private bool MudarDirecao()
    {
        Vector2 origem;
        Vector2 offsetOrigem;
        Vector2 direcao;
        float raio;
        RaycastHit2D hit;

        // Verificar chão afrente
        offsetOrigem = new Vector2(_bounds.extents.x + offsetChao, 0);
        offsetOrigem = olhandoParaDireita ? offsetOrigem : -offsetOrigem;

        origem = (Vector2)_bounds.center + offsetOrigem;
        direcao = Vector2.down;
        raio = _bounds.extents.y + raioChao;

        hit = Physics2D.Raycast(origem, direcao, raio, chao);

        if (hit.collider == null)
        {
            if (debug) Debug.DrawRay(origem, direcao * raio, Color.green, 1.5f);
            return true;
        }
        else
        {
            if (debug) Debug.DrawRay(origem, direcao * raio, Color.red);
        }

        // Verificar bloqueio afrente
        offsetOrigem = new Vector2(_bounds.extents.x + offsetObstaculo, 0);
        offsetOrigem = olhandoParaDireita ? offsetOrigem : -offsetOrigem;

        origem = (Vector2)_bounds.center + offsetOrigem;
        direcao = olhandoParaDireita ? Vector2.right : Vector2.left;
        raio = raioObstaculo;

        hit = Physics2D.Raycast(origem, direcao, raio, chao);

        if (hit.collider != null)
        {
            if (debug) Debug.DrawRay(origem, direcao * raio, Color.green, 1.5f);
            return true;
        }
        else
        {
            if (debug) Debug.DrawRay(origem, direcao * raio, Color.red);
        }

        // Sem bloqueios
        return false;
    }

    private void OnDrawGizmos()
    {
        _collider = GetComponent<Collider2D>();

        Vector2 origem;
        Vector2 offsetOrigem;
        Vector2 direcao;
        float raio;

        // Verificar chão afrente
        offsetOrigem = new Vector2(_bounds.extents.x + offsetChao, 0);
        offsetOrigem = olhandoParaDireita ? offsetOrigem : -offsetOrigem;

        origem = (Vector2)_bounds.center + offsetOrigem;
        direcao = Vector2.down;
        raio = _bounds.extents.y + raioChao;

        Gizmos.DrawRay(origem, direcao * raio);

        // Verificar bloqueio afrente
        offsetOrigem = new Vector2(_bounds.extents.x + offsetObstaculo, 0);
        offsetOrigem = olhandoParaDireita ? offsetOrigem : -offsetOrigem;

        origem = (Vector2)_bounds.center + offsetOrigem;
        direcao = olhandoParaDireita ? Vector2.right : Vector2.left;
        raio = raioObstaculo;

        Gizmos.DrawRay(origem, direcao * raio);

    }
}
