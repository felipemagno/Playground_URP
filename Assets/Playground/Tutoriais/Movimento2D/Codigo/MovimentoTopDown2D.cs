using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoTopDown2D : MonoBehaviour, IMovimento
{
    Rigidbody2D _rigidbody2D;

    [SerializeField] float speed = 5f;
    Vector2 valorMovimento;

    public void IniciarPulo()
    {
        
    }

    public void Movimento(Vector2 direcao)
    {
         valorMovimento = direcao * speed;
    }

    public void PararPulo()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = valorMovimento;
    }
}
