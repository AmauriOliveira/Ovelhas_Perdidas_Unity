using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIEnemy : MonoBehaviour
{
    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody2D;
    private SpriteRenderer enemySpriteRenderer;
    public bool EstaEspelhado = true;
    public bool enemyEstaVivo = true;
    public float enemyVelocidade = 2.5f;
    public float tempoVoltar = 1;
    private bool contador = false;
    private InimigoBasicoVida inimigoBasicoVida;
    private MyGameController _myGameController;

    void Start()
    {
        inimigoBasicoVida = GetComponent<InimigoBasicoVida>();
        enemyAnimator = GetComponent<Animator>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("Voltar");
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
    }
    // Update is called once per frame
    void Update()
    {

        if (enemyEstaVivo)
        {
            if (inimigoBasicoVida.hp <= 0)
            {
                enemyEstaVivo = false;
            }
            else
            {
                if (!EstaEspelhado)///andar pra esquerda
                {
                    transform.Translate(new Vector2(-enemyVelocidade * Time.deltaTime, 0));

                }
                else if (EstaEspelhado)///andar pra direita
                {
                    transform.Translate(new Vector2(enemyVelocidade * Time.deltaTime, 0));
                }
            }
        }
        else
        {
            if (!contador)
            {
                contador = true;
                enemyAnimator.SetBool("die", true);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<Collider2D>());
                _myGameController.fasePontos += inimigoBasicoVida.pontos;
                Destroy(gameObject, 1);
            }
        }
    }
    IEnumerator Voltar()
    {
        yield return new WaitForSeconds(tempoVoltar);
        if (enemyEstaVivo)
        {
            EstaEspelhado = !EstaEspelhado;
            enemySpriteRenderer.flipX = EstaEspelhado;
            StartCoroutine("Voltar");
        }
    }

}