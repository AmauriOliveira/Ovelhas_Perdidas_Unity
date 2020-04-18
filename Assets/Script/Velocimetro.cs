using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocimetro : MonoBehaviour
{
    public Rigidbody2D veiculoRB2D;
    public int rotacaoInicial;
    public int rotacaoFinal;
    public GameObject ponteiro;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (veiculoRB2D.velocity.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, ((veiculoRB2D.velocity.x) * -5));
        }
        else if (veiculoRB2D.velocity.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, ((veiculoRB2D.velocity.x) * 5));
        }
    }
}
