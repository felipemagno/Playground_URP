using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void CarregarFase(string NomeDaFase)
    {
        SceneManager.LoadScene(NomeDaFase);
    }

    public void CarregarFaseAditiva(string NomeDaFase)
    {
        SceneManager.LoadScene(NomeDaFase,LoadSceneMode.Additive);
    }
}