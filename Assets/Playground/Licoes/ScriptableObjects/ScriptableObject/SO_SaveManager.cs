using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Dados/SaveManager")]
public class SO_SaveManager : ScriptableObject
{
    [SerializeField] int _vezesAtivado;

    public int Ativacoes() => _vezesAtivado;

    public int Ativar() => ++_vezesAtivado;


    public void CarregarDados()
    {
        if (PlayerPrefs.HasKey("Ativacoes"))
        {
            _vezesAtivado = PlayerPrefs.GetInt("Ativacoes");
        }
    }

    public void SalvarDados()
    {
        PlayerPrefs.SetInt("Ativacoes", _vezesAtivado);        
    }

    private void OnEnable()
    {
        CarregarDados();
    }

    private void OnDisable()
    {
        SalvarDados();
    }
}
