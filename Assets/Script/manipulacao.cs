using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manipulacao : MonoBehaviour
{
    //public int x,y,z;
    public float velocidade = -0.2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        transform.Rotate(new Vector3(0, 0, velocidade));
        /*         //mover
                transform.Translate(new Vector3(0, -0.1f, 0));//desce
                transform.Translate(new Vector3(0, 0.1f, 0));//sobe
                transform.Translate(new Vector3(-0.1f, 0, 0));//vai pra esquerda <<
                transform.Translate(new Vector3(0.1f, 0, 0));//vai pra direita >>


                //rolacionar
                transform.Rotate(new Vector3(0, 0, 0.1f));   //anti horario
                transform.Rotate(new Vector3(0, 0, -0.2f)); //horario

                //escala
                transform.localScale += new Vector3(0.01f, 0.01f, 0);//aumentar
                transform.localScale -= new Vector3(0.01f, 0.01f, 0);//diminuir 
        */
    }
}
