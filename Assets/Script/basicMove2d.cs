using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMove2d : MonoBehaviour
{
    public Rigidbody2D playerRB2D;//nem precisa colocar nada
    public bool EstaEspelhado = true;//referencia ao lado que ele começa olhando//
    public float velocidade = 2.5f;//velocidade
    public float salto = 6.5f;//salto
    public bool SaltoLiberado = false;//limitador de salto
    public SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerRB2D = GetComponent<Rigidbody2D>();//carrega sozinho o RG2D
                                                 // player = GetComponent<Transform>();//ao rodar define quem recebe a ação, no caso quem tiver com o cod
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && EstaEspelhado)///vefirica se a tecla foi apertado e não esta espelhado
        {
            Virar();//chama o metodo de virar
        }

        else if (Input.GetKey(KeyCode.RightArrow) && !EstaEspelhado)///vefirica se a tecla foi apertado e esta espelhado
        {
            Virar();//chama o metodo de virar
        }

        if (Input.GetKey(KeyCode.LeftArrow))///andar pra esquerda
        {
            transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));//aplica força no x
        }

        if (Input.GetKey(KeyCode.RightArrow))///andar pra direita
        {
            transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//aplica força no x
        }

        if (Input.GetKeyDown(KeyCode.Space) && SaltoLiberado)///vefirica se a tecla foi apertado e tiver liberado pulo
        {
            playerRB2D.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);///////////////////
        }
    }
    void Virar()
    {
        EstaEspelhado = !EstaEspelhado;//inverter o valor da variavel
                                       // Vector3 scala = player.localScale;//pega o valor da scala
                                       // scala.x *= -1;//mutiplica o valor da escala por -1 pra inverter  sinal = +/-
                                       // player.localScale = scala;//aplica o valor da conta acima no player;
        mySpriteRenderer.flipX = EstaEspelhado;
    }
    void OnCollisionEnter2D(Collision2D col)//quando encontar
    {
        if (col.gameObject.CompareTag("chao"))///muita atenção no nome da tag, deve ser a mesma usado no inspetor
        {
            SaltoLiberado = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)//quando afastar
    {
        if (col.gameObject.CompareTag("chao"))///muita atenção no nome da tag, deve ser a mesma usado no inspetor
        {
            SaltoLiberado = false;
        }
    }
}