using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plataforma.Move
{
    public class JogadorControle : MonoBehaviour
    {
        public GameObject municao;//bala
        public GameObject canoArma;//cano
        public float FireRate = 0.1f;        //Tempo Para Arma ATIRAR.
        private float currentTimeToFire = 0; //Cronometro.
        private bool canFire = true;
        private Animator playerAnimator;
        public float distanciaDoChao = 1, forcaDoPulo = 7, tempoPorPulo = 1;
        public Vector2 direcaoDoPulo = new Vector3(0, 1);
        public LayerMask LayersNaoIgnoradas = -1;
        public bool estaNoChao;
        public bool pulou1;
        public bool pulou2;
        public bool podePular;
        public Rigidbody2D playerRB2D;//nem precisa colocar nada
        public bool EstaEspelhado = true;//referencia ao lado que ele começa olhando//
        public SpriteRenderer mySpriteRenderer;
        public float velocidade = 2.5f;//velocidade
                                       // Start is called before the first frame update
        void Start()
        {
            pulou1 = pulou2 = false;
            podePular = true;
            playerAnimator = GetComponent<Animator>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            playerRB2D = GetComponent<Rigidbody2D>();//carrega sozinho o RG2D
                                                     // player = GetComponent<Transform>();//ao rodar define quem recebe a ação, no caso quem tiver com o cod
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Esquerda.moveEsquerda)///andar pra esquerda
            {
                transform.Translate(new Vector2(-velocidade * Time.deltaTime, 0));//aplica força no x

                playerAnimator.SetBool("anda", true);
                playerAnimator.SetBool("para", false);

                if (!EstaEspelhado)
                {
                    Virar();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Direita.moveDireita)///andar pra direita
            {
                transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));//aplica força no x

                playerAnimator.SetBool("anda", true);
                playerAnimator.SetBool("para", false);

                if (EstaEspelhado)
                {
                    Virar();
                }
            }//tiro///////////////////////////////
            else if (Input.GetKey(KeyCode.X) || Input.GetKeyDown(KeyCode.X) || Atirar.tocandoAtirar)///vefirica se a tecla foi apertado e tiver liberado pulo
            {
                playerAnimator.SetBool("para", false);
                playerAnimator.SetBool("anda", false);
                playerAnimator.SetBool("atira", true);
                if (canFire)
                {
                    Tiro();
                }
                if (Input.GetKey(KeyCode.RightArrow) || Direita.moveDireita || Input.GetKey(KeyCode.LeftArrow) || Esquerda.moveEsquerda)
                {
                    playerAnimator.SetBool("anda", true);
                    playerAnimator.SetBool("atira", true);
                }

            }
            else
            {
                playerAnimator.SetBool("para", true);
                playerAnimator.SetBool("anda", false);
                playerAnimator.SetBool("atira", false);
            }
            if (canFire == false)
            { //Se Pode Atirar for Falso.
                currentTimeToFire += Time.deltaTime; //Cronometro Começa A contar.
                if (currentTimeToFire > FireRate)
                { // Se Cronometro For Maior que Tempo para Atira.
                    currentTimeToFire = 0;  //Cronometro vai Zerar.
                    canFire = true;   //Pode Atirar Fica True.
                }
            }
            /////
            Debug.DrawLine(transform.position, transform.position - Vector3.left - Vector3.up * distanciaDoChao);
            Debug.DrawLine(transform.position, transform.position - Vector3.right - Vector3.up * distanciaDoChao);
            //Debug.DrawLine(transform.position, transform.position - Vector3.up * distanciaDoChao);
            bool tempR = Physics2D.Linecast(transform.position, transform.position - Vector3.right - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);
            bool tempL = Physics2D.Linecast(transform.position, transform.position - Vector3.left - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);
            //estaNoChao = Physics2D.Linecast(transform.position, transform.position - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);
            estaNoChao = tempL || tempR;
            if ((pulou1 == true || pulou2 == true) && estaNoChao == true)
            {
                pulou1 = pulou2 = false;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Pula.tocouPular) && estaNoChao == false)
            {
                pulou1 = true;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Pula.tocouPular) && estaNoChao == true && pulou1 == false && pulou2 == false && podePular == true)
            {
                StartCoroutine("CronometroPular");
                pulou1 = true;
                pulou2 = false;
                Pula.tocouPular = false;
                playerAnimator.SetBool("pula", true);
                playerAnimator.SetBool("para", false);
                playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo, ForceMode2D.Impulse);
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Pula.tocouPular) && estaNoChao == false && pulou1 == true && pulou2 == false && podePular == true)
            {
                StartCoroutine("CronometroPular");
                pulou1 = true;
                pulou2 = true;
                Pula.tocouPular = false;
                playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo * 2, ForceMode2D.Impulse);
            }
            if (estaNoChao)
            {
                playerAnimator.SetBool("pula", false);
            }
            else
            {
                playerAnimator.SetBool("para", false);
                playerAnimator.SetBool("anda", false);
                playerAnimator.SetBool("pula", true);
            }
            
        }

        public void Tiro()
        {
            Instantiate(municao, canoArma.transform.position, canoArma.transform.rotation);
            // Instantiate(municao, new Vector3(canoEsquerdo.transform.position.x, canoEsquerdo.transform.position.y, canoEsquerdo.transform.position.z), Quaternion.identity);
            canFire = false;   // E Pode Atirar fica False.
                               //FireParticle.Emit(1); //E A Particula Aparece.
                               //GetComponent<AudioSource>().Play(); // E Solta um Audio
        }
        IEnumerator CronometroPular()
        {
            podePular = false;
            yield return new WaitForSeconds(tempoPorPulo);
            podePular = true;
        }
        void Virar()
        {
            canoArma.transform.localPosition = new Vector3((canoArma.transform.localPosition.x * -1), canoArma.transform.localPosition.y, canoArma.transform.localPosition.z);

            EstaEspelhado = !EstaEspelhado;//inverter o valor da variavel
                                           // Vector3 scala = player.localScale;//pega o valor da scala
                                           // scala.x *= -1;//mutiplica o valor da escala por -1 pra inverter  sinal = +/-
                                           // player.localScale = scala;//aplica o valor da conta acima no player;
            mySpriteRenderer.flipX = EstaEspelhado;
            if (EstaEspelhado)
            {
                canoArma.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (!EstaEspelhado)
            {
                canoArma.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
            void OnCollisionEnter2D(Collision2D col)//quando encontar
    {
        if (col.gameObject.CompareTag("inimigo"))///muita atenção no nome da tag, deve ser a mesma usado no inspetor
        {
            Debug.Log("tocou");
        }
    }
    }
}
