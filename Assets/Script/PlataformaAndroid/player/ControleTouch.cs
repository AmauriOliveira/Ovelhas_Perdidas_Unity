using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class ControleTouch : MonoBehaviour
{
    public Touch toque;//resposavel por sentir o toque
    ///player
    public float velocidade = 2.5f;//velocidade
    public bool estaVivo = true;//vereifica se o player esta vivo
    public SpriteRenderer mySpriteRenderer;
    public bool dir;
    public GameObject direita;
    public GameObject esquerda;
    public GameObject cabeca;
    public SpriteRenderer headSpriteRenderer;
    ///mira
    public GameObject mira;
    public float distance = 10f;
    ///arco
    public GameObject Arrow;
    public Transform SpawnArrow;
    public float Force;
    public float RateOfFire = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;
    public GameObject bow;
    //rotacionar Arco
    public GameObject weapon;
    //tartaruga
    public GameObject tartaruga;
    public SpriteRenderer TartarugaSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        LookAt2D(mira.transform, weapon.transform, 17f, FacingDirection.RIGHT);//virar o arco
        LookAt2D(mira.transform, esquerda.transform, 17f, FacingDirection.RIGHT);//virar o arco
        LookAt2D(mira.transform, direita.transform, 17f, FacingDirection.RIGHT);//virar o arco
        myTime = myTime + Time.deltaTime;//atualizar o fire rate
        if (Input.touchCount > 0 && estaVivo == true)//verifica quantos toque foi feito e se ta vivo
        {
            toque = Input.GetTouch(0);//pega o primeiro toque
            if (toque.phase == TouchPhase.Began)//verefica se comecou toque
            {
                if (toque.position.y > (Screen.height / 4)) //tocou em cima na na parte jogavel
                {
                    VirarComToque();
                    Mirar();
                }
            }
            else if (toque.phase == TouchPhase.Ended && myTime > nextFire)//ao soltar atira
            {
                if (toque.position.y > (Screen.height / 4)) //tocou em cima
                {
                    Atirar();
                }
            }
        }
        /////////////////////
        if (Input.GetButton("Esquerda"))///vefirica se a tecla foi apertado e tiver liberado pulo
        {
            tartaruga.transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));//aplica força no x
            if (!dir)
            {
                Virar();
            }
        }
        if (Input.GetButton("Direita"))///vefirica se a tecla foi apertado e tiver liberado pulo
        {
            tartaruga.transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//aplica força no x
            if (dir)
            {
                Virar();
            }
        }

    }
    private void VirarComToque()
    {
        if (toque.position.x < (Screen.width / 2) && dir == false)//verefica o lado da tela que tocou e se ja esta olhando pra ele
        {/*
            dir = true;//espelha
            mySpriteRenderer.flipX = dir;//faz a face virar junto ao movimento
           */
            Virar();
        }
        else if (toque.position.x > (Screen.width / 2) && dir == true)//verefica o lado da tela que tocou e se ja esta olhando pra ele
        {
            /*
            dir = false;// volta ao normal
            mySpriteRenderer.flipX = dir;//faz a face virar junto ao movimento
            */
            Virar();
        }
    }
    private void Mirar()
    {
        Vector3 touchPosition = new Vector3(toque.position.x, toque.position.y, distance); // converte o toque em vector3
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(touchPosition); // calcula a diferença
        mira.transform.position = objPosition; // coloca o objeto na posição
    }
    private void Atirar()
    {
        nextFire = myTime + RateOfFire;
        GameObject NewArrow = Instantiate(Arrow, SpawnArrow.transform.position, bow.transform.rotation);
        NewArrow.transform.localScale = new Vector2(0.4f, 0.4f);
        NewArrow.GetComponent<Rigidbody2D>().AddForce(NewArrow.transform.right * Force);
        nextFire = nextFire - myTime;
        myTime = 0.0F;
    }
    enum FacingDirection
    {
        UP = 270,
        DOWN = 90,
        LEFT = 180,
        RIGHT = 0
    }
    void LookAt2D(Transform theTarget, Transform rotate, float theSpeed, FacingDirection facing)
    {
        Vector3 vectorToTarget = theTarget.position - rotate.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        rotate.transform.rotation = Quaternion.Slerp(rotate.transform.rotation, q, Time.deltaTime * theSpeed);
    }
    void Virar()
    {
        esquerda.transform.localPosition = new Vector3((esquerda.transform.localPosition.x * -1), esquerda.transform.localPosition.y, esquerda.transform.localPosition.z);
        direita.transform.localPosition = new Vector3((direita.transform.localPosition.x * -1), direita.transform.localPosition.y, direita.transform.localPosition.z);
        mira.transform.position = new Vector3(mira.transform.position.x*-1,mira.transform.position.y,mira.transform.position.z);

        dir = !dir;//inverter o valor da variavel
        headSpriteRenderer.flipX = dir;
        mySpriteRenderer.flipX = dir;
        TartarugaSpriteRenderer.flipX = dir;
       
        /*  if (dir)
          {
              canoArma.transform.localRotation = Quaternion.Euler(0, 180, 0);
          }
          else if (!dir)
          {
              canoArma.transform.localRotation = Quaternion.Euler(0, 0, 0);
          }
          */

    }
}