using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoContolada : MonoBehaviour
{
    public float velocidade;
    public TurnButton button;

    void Update()
    {
        if (button.estaLigado)
        {
            transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
        }

    }
}
