using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config_", menuName = "Dados/Config Cores", order = 0)]
public class SO_Config : ScriptableObject
{
    [SerializeField] List<Color> colors;

    public Color GetRandomColor()
    {
        int indice = Random.Range(0, colors.Count);
        return colors[indice];
    }
}

