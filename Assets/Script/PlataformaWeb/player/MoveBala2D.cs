using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBala2D : MonoBehaviour
{
    public LayerMask LayersAlvos = -1;
    public float balaVelocidade;// Velocidade da Bala.
    public float TempoVida = 3;  // Tempo que A Bala vai ficar na Cena.
    private float currentTimeToLive = 0; //Cronometro.
    // Update is called once per frame
    void Update()
    {
        currentTimeToLive += Time.deltaTime;  //Cronometro vai Receber o TEMPO.

        if (currentTimeToLive > TempoVida)
        {//Se Cronometro for Maior que Tempo da Bala.
            Destroy(gameObject);    //Bala Vai Ser Destruida.
        }

        transform.Translate(Vector2.right * balaVelocidade); //A BALA VAI ANDAR PARA FRENTE
        //transform.Translate(0, 0, balaVelocidade * Time.deltaTime);
        RaycastHit2D hits = Physics2D.Raycast(transform.position, transform.position, 1, LayersAlvos);

        if (hits.collider != null)
        {
            Debug.Log(hits.collider.ToString());
            Destroy(gameObject);
        }

    }
}
