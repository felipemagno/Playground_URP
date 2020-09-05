using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarChao : MonoBehaviour
{
    private bool estaNoChao = false;
    Transform myTransform;

    [SerializeField]
    float raio = 0.25f;

    [SerializeField]
    LayerMask layerDoChao;

    public bool EstaNoChao { get => estaNoChao; }

    private void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = (Physics2D.OverlapCircle((Vector2)myTransform.position, raio, layerDoChao) != null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
