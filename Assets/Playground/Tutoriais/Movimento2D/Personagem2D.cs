using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem2D : MonoBehaviour
{
    Transform myTransform;
    Rigidbody2D myRigidbody2D;
    VerificarChao myVerificarChao;

    Vector2 movimento = Vector2.zero;
    bool pule = false;
    bool pararPulo = false;

    [SerializeField]
    float velocidade = 10f;
    [SerializeField]
    float velocidadePulo = 10f;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        // "transform" na verdade é "GetComponent<Transform>()"

        myRigidbody2D = GetComponent<Rigidbody2D>();

        myVerificarChao = GetComponentInChildren<VerificarChao>();
    }

    // Update is called once per frame
    void Update()
    {
        // No Update, recebemos Inputs
        movimento.x = Input.GetAxis("Horizontal");
        //movimento.y = Input.GetAxis("Vertical");
        //movimento.Normalize();

        //if (movimento.sqrMagnitude != 0f)
        //{
        //    myTransform.Translate(movimento * Time.deltaTime * velocidade, Space.World);
        //}
                
        if (Input.GetButtonDown("Jump") && myVerificarChao.EstaNoChao)
        {
            pule = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            pule = false;
        }

    }

    private void FixedUpdate()
    {
        // No Fixed Update, tratamos a física (e as respostas dos inputs para isso)

        // Movimento Horizontal
        if (movimento.sqrMagnitude != 0f)
        {
            movimento = movimento * velocidade;
            movimento.y = myRigidbody2D.velocity.y;
            myRigidbody2D.velocity = movimento;
        }
        
        // Pular
        if (pule) // == true)
        {
            movimento.y = velocidadePulo;
            myRigidbody2D.velocity = movimento;
            pule = false;
        }        
    }
}
