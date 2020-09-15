using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerInformacao : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] SO_SaveManager _saveManager;

    // Start is called before the first frame update
    void Start()
    {
        
        if (_saveManager != null && _text != null)
        {
            _saveManager.CarregarDados();
            _text.text = "Ativações : " + _saveManager.Ativacoes();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _text.text = "Ativações : " + _saveManager.Ativar();
        }
    }

    private void OnDestroy()
    {
        //if(        _saveManager != null)
        //{
        //    _saveManager.SalvarDados();
        //}
        _saveManager?.SalvarDados();
    }
}
