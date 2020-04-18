using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverFundo : MonoBehaviour
{
    public Renderer fundoMov;
    public float velocidade = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //setema de mover o fundo
        Vector2 offset = new Vector2(velocidade * Time.deltaTime, 0);
        fundoMov.material.mainTextureOffset += offset;
    }
}
