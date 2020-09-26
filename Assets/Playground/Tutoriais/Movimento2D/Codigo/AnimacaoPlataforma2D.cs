using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class AnimacaoPlataforma2D : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Armazena o ID do parâmetro "Andando"
    int hashAndando = Animator.StringToHash("Andando");

    // Armazenar ID dos estados
    int hashAndar = Animator.StringToHash("Andar");
    int hashPular = Animator.StringToHash("Pular");
    int hashIdle = Animator.StringToHash("Idle");

    bool estaPulando = false;
    bool estaAndando = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Flip(bool viradoDireita)
    {
        spriteRenderer.flipX = !viradoDireita;
    }

    public void Andar(bool andando)
    {
        estaAndando = andando;
        AtualizarAnimacao();

        ////animator.SetBool("Andando", andando);
        //animator.SetBool(hashAndando, andando);

        //if (andando)
        //{
        //    animator.Play("Andar");            
        //}
        //else
        //{
        //    animator.Play("Idle");
        //}
    }

    public void Pular(bool pulando)
    {
        estaPulando = pulando;
        AtualizarAnimacao();
    }

    private void AtualizarAnimacao()
    {
        if (estaPulando)
        {
            animator.Play(hashPular);
            //animator.Play("Pular");
        }
        else if (estaAndando)
        {
            animator.Play(hashAndar);
            //animator.Play("Andar");
        }
        else
        {
            animator.Play(hashIdle);
            //animator.Play("Idle");
        }
    }
}