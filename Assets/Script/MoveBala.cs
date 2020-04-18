using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBala : MonoBehaviour
{
    public float velocidade = -0.2f;
    //public Rigidbody2D BalaRB2D;//nem precisa colocar nada
    // Start is called before the first frame update
    void Start()
    {
        //BalaRB2D = GetComponent<Rigidbody2D>();//faz a referencia
        //BalaRB2D.AddRelativeForce(new Vector2(5, 2), ForceMode2D.Impulse);//aplica a força
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//vai pra direita >>
        //transform.Rotate(new Vector3(0, 0, -0.2f));//aplica uma leve rotação
    }
}
