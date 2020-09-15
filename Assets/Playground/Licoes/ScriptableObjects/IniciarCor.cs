using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarCor : MonoBehaviour
{
    [SerializeField] SO_Config MinhasCores;
    Renderer _renderizador;


    // Start is called before the first frame update
    void Start()
    {
        _renderizador = GetComponent<Renderer>();
        _renderizador.material.color = MinhasCores.GetRandomColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _renderizador.material.color = MinhasCores.GetRandomColor();
        }
    }
}
