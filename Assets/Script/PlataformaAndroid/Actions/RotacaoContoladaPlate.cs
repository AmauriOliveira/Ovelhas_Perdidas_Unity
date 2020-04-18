using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoContoladaPlate : MonoBehaviour
{
    public float velocidade;
    public TurnPlate plate;

    void Update()
    {
        if (plate.estaLigado)
        {
            transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
        }
    }
}
