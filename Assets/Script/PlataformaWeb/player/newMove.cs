using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newMove : MonoBehaviour
{
    public Vector2 direcaoDoPulo = new Vector3(0, 1);
    public Rigidbody2D playerRB2D;//nem precisa colocar nada
    public bool EstaEspelhado = true;//referencia ao lado que ele começa olhando//
                                     // public Transform player;//referencia a quem vai ser moviementado//
    public float velocidade = 2.5f;//velocidade
    public float distanciaDoChao = 1, forcaDoPulo = 7, tempoPorPulo = 1;
    public LayerMask LayersNaoIgnoradas = -1;
    private bool estaNoChao;
    private bool pulou1;
    private bool pulou2;
    private bool podePular;
    public SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerRB2D = GetComponent<Rigidbody2D>();//carrega sozinho o RG2D|


    }

    public void Pulo()
    {
        estaNoChao = Physics2D.Linecast(transform.position, transform.position - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);
        if ((pulou1 == true || pulou2 == true) && estaNoChao == true)
        {
            pulou1 = pulou2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == false)
        {
            pulou1 = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == true && pulou1 == false && pulou2 == false && podePular == true)
        {
            StartCoroutine("CronometroPular");
            pulou1 = true;
            pulou2 = false;
            playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == false && pulou1 == true && pulou2 == false && podePular == true)
        {
            StartCoroutine("CronometroPular");
            pulou1 = true;
            pulou2 = true;
            playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo * 2, ForceMode2D.Impulse);
        }
    }
    public void Esquerda()
    {
        transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));//aplica força no x
    }
    public void Direita()
    {
        transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//aplica força no x
    }


    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.Linecast(transform.position, transform.position - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);
        if ((pulou1 == true || pulou2 == true) && estaNoChao == true)
        {
            pulou1 = pulou2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == false)
        {
            pulou1 = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == true && pulou1 == false && pulou2 == false && podePular == true)
        {
            StartCoroutine("CronometroPular");
            pulou1 = true;
            pulou2 = false;
            playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == false && pulou1 == true && pulou2 == false && podePular == true)
        {
            StartCoroutine("CronometroPular");
            pulou1 = true;
            pulou2 = true;
            playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo * 2, ForceMode2D.Impulse);
        }
    }
    IEnumerator CronometroPular()
    {
        podePular = false;
        yield return new WaitForSeconds(tempoPorPulo);
        podePular = true;
    }
}
