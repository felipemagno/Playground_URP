using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{    
    public bool ativado = false;
    [SerializeField] float dano = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vida alvo = collision.GetComponent<Vida>();
        if(alvo != null)
        {
            alvo.LevarDano(dano);
        }
    }
}
