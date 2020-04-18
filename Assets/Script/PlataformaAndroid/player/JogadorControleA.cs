using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class JogadorControleA : MonoBehaviour
{
    #region Vars

    [Space]
    [Header("Tiro")]
    private float varaicaoTiro;
    public GameObject municao;//bala
    public GameObject canoArma;//cano
    public float taxaTiroSegundo = 0.1f;
    public float tiroForca;
    private bool podeAtirar = true;

    [Space]
    [Header("Pulo")]
    public float distanciaDoChao = 1;
    public float forcaDoPulo = 7;
    public float tempoPorPulo = 1;
    private Vector2 direcaoDoPulo = new Vector3(0, 1);
    public LayerMask LayersNaoIgnoradas = -1;
    private bool estaNoChao;
    private bool pulou1;
    private bool pulou2;
    private bool podePular;

    [Space]
    [Header("Dash")]
    public bool dash = false;
    public float dashSpeed = 20;
    public ParticleSystem particulasDashSolo;
    public ParticleSystemRenderer particulaRenderSolo;
    public ParticleSystem particulasDashAr;
    public ParticleSystemRenderer particulaRenderAr;

    [Space]
    [Header("Player")]
    private bool EstaVivo = true;
    public bool EstaEspelhado = true;//referencia ao lado que ele começa olhando//
    private Rigidbody2D playerRB2D;//nem precisa colocar nada
    public float velocidade = 2.5f;//velocidade
    private bool tamanhoEstaNormal = true;
    public float powerUpScale = 0.02f;

    [Space]
    [Header("DragonBone")]
    public UnityArmatureComponent armatureComponent;
    private bool aniIdle = false;
    private bool aniWalk = false;
    private bool aniShot = false;
    private bool aniJump = false;
    private bool aniDie = false;
    public bool aniDash = false;

    [Space]
    [Header("Outros")]
    private MyGameController _myGameController;
    private PlayerBasicoVida playerBasicoVida;
    /////
    #endregion
    /////////
    #region Funções Unity
    void Start()
    {
        pulou1 = pulou2 = false;
        podePular = true;
        playerBasicoVida = GetComponent<PlayerBasicoVida>();
        playerRB2D = GetComponent<Rigidbody2D>();
        _myGameController = FindObjectOfType(typeof(MyGameController)) as MyGameController;
        _myGameController.playerTrasnform = this.transform;
        particulasDashSolo.Stop(true);
        particulasDashAr.Stop(true);
    }
    void Update()
    {
        if (!_myGameController.EmPausa())
        {
            ////////////////Horizontal
            float x = Input.GetAxis("Horizontal");
            if (x != 0 && !aniShot && EstaVivo && !Input.GetButton("Fire1"))
            {
                transform.Translate(new Vector2(x * velocidade * Time.deltaTime, 0));//aplica força no x

                AnimationWalk();

                if (x > 0 && EstaEspelhado)
                {
                    Virar();
                }
                else if (x < 0 && !EstaEspelhado)
                {
                    Virar();
                }
            }
            ///////////////TIRO
            else if (Input.GetButton("Fire1") && estaNoChao && !aniShot && podeAtirar && EstaVivo && !aniDash)///vefirica se a tecla foi apertado e tiver liberado pulo
            {
                podeAtirar = false;
                StartCoroutine("Atirar");
            }
            else if (estaNoChao && EstaVivo)
            {
                AnimationIdle();
            }
            else if (estaNoChao && !EstaVivo)
            {
                AnimationDie();
            }
            ///////////////////////VERTICAL
            float y = Input.GetAxis("Vertical");
            varaicaoTiro = y / 8;
            //////////////////DASH
            if (Input.GetButtonDown("Dash") && !aniShot && EstaVivo && !dash)
            {
                float xRaw = Input.GetAxisRaw("Horizontal");
                float yRaw = Input.GetAxisRaw("Vertical");

                if (xRaw != 0 || yRaw > 0)
                {
                    Dash(xRaw, yRaw);
                }
            }
            /////////////PULO
            bool tempR = Physics2D.Linecast(transform.position, transform.position - new Vector3(0.5f, 0, 0) - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);//R
            bool tempL = Physics2D.Linecast(transform.position, transform.position - new Vector3(-0.5f, 0, 0) - Vector3.up * distanciaDoChao, LayersNaoIgnoradas);//L
            estaNoChao = tempL || tempR;
            if ((pulou1 || pulou2) && estaNoChao)
            {
                pulou1 = pulou2 = false;

            }
            else if (pulou1 && pulou2 && estaNoChao)
            {
                playerRB2D.velocity = Vector2.zero;
            }
            if (Input.GetButtonDown("Jump") && !estaNoChao)
            {
                pulou1 = true;
            }
            if (Input.GetButtonDown("Jump") && estaNoChao && !pulou1 && !pulou2 && podePular && EstaVivo)
            {
                StartCoroutine("CronometroPular");
                pulou1 = true;
                pulou2 = false;
                _myGameController.PlaySfx(_myGameController.SxfJump, 0.5f);
                AnimationJump();
                playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo * 1.5f, ForceMode2D.Impulse);
            }
            if (Input.GetButtonDown("Jump") && !estaNoChao && pulou1 && !pulou2 && podePular && EstaVivo)
            {

                StartCoroutine("CronometroPular");
                pulou1 = true;
                pulou2 = true;
                playerRB2D.AddForce(direcaoDoPulo * forcaDoPulo * 2, ForceMode2D.Impulse);
                _myGameController.PlaySfx(_myGameController.SxfJump, 0.5f);
                AnimationJump();
            }
            if (estaNoChao)
            {
                aniJump = false;
            }
            else
            {
                AnimationJump();
            }
            if (playerBasicoVida.hp <= 0)
            {
                EstaVivo = false;
                AnimationDie();
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<Collider2D>());
                _myGameController.MostrarPontos(false, "Tente Novamente.");
                _myGameController.StartCoroutine("FeedBack", "Morreu...");
                _myGameController.PlaySfx(_myGameController.SxfDie, 0.5f);              
            }
        }
    }
    #endregion
    //////////////////////////
    #region Ações
    public void Tiro()
    {
        StartCoroutine("CronometroAtirar");
        GameObject NewArrow = Instantiate(municao, canoArma.transform.position, canoArma.transform.rotation);
        if (EstaEspelhado)
        {
            NewArrow.GetComponent<Rigidbody2D>().AddForce(NewArrow.transform.right + new Vector3(-1, 0.4f + varaicaoTiro, 0) * tiroForca);
        }
        else
        {
            NewArrow.GetComponent<Rigidbody2D>().AddForce(NewArrow.transform.right + new Vector3(1, 0.4f + varaicaoTiro, 0) * tiroForca);
        }
        _myGameController.PlaySfxArray(_myGameController.SxfFlechaDisparo, 0.3f);
         _myGameController.PlaySfxArray(_myGameController.SxfArco, 0.2f);
    }
    void Virar()
    {
        canoArma.transform.localPosition = new Vector3((canoArma.transform.localPosition.x * -1), canoArma.transform.localPosition.y, canoArma.transform.localPosition.z);

        EstaEspelhado = !EstaEspelhado;
        armatureComponent.armature.flipX = EstaEspelhado;
        if (EstaEspelhado)
        {
            canoArma.transform.localRotation = Quaternion.Euler(0, 180, 0);
            particulaRenderSolo.flip = new Vector3(1, 0, 0);
            particulaRenderAr.flip = new Vector3(1, 0, 0);
        }
        else if (!EstaEspelhado)
        {
            canoArma.transform.localRotation = Quaternion.Euler(0, 0, 0);
            particulaRenderSolo.flip = new Vector3(0, 0, 0);
            particulaRenderAr.flip = new Vector3(0, 0, 0);
        }
    }

    void Dash(float x, float y)
    {
        dash = true;
        _myGameController.StartCoroutine("EfeitoDashBar");
        playerRB2D.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        playerRB2D.velocity += dir.normalized * dashSpeed;

        AnimationDash(y);
        _myGameController.PlaySfx(_myGameController.SxfDash, 0.5f);
        StartCoroutine("CronometroDash");
    }
    #endregion
    //////////////////////////
    #region Coroutine
    IEnumerator Atirar()
    {
        AnimationShot();
        yield return new WaitForSeconds(0.45f);//atrazo pra sicronizar animação com dragom bone
        Tiro();
    }
    IEnumerator AumentarPlayer()
    {
        yield return new WaitForSeconds(0.2f);
        if (!tamanhoEstaNormal)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                transform.localScale = new Vector3(transform.localScale.x + powerUpScale, transform.localScale.y + powerUpScale, 1);

            }

            // transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, 1);
            tamanhoEstaNormal = true;
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                transform.localScale = new Vector3(transform.localScale.x - powerUpScale, transform.localScale.y - powerUpScale, 1);

            }
            tamanhoEstaNormal = false;
        }
    }
    IEnumerator CronometroAtirar()
    {
        aniShot = false;
        yield return new WaitForSeconds(taxaTiroSegundo);
        podeAtirar = true;
    }
    IEnumerator CronometroPular()
    {
        podePular = false;
        yield return new WaitForSeconds(tempoPorPulo);
        podePular = true;
    }
    IEnumerator CronometroDash()
    {
        yield return new WaitForSeconds(3);
        dash = false;
    }
    IEnumerator ParticulaDash(int y)
    {
        ParticleSystem particle = (y != 0 ? particulasDashAr : particulasDashSolo);
        playerBasicoVida.StartCoroutine("CronometroImunidadePotion", 1);
        particle.Play(true);
        yield return new WaitForSeconds(1);
        particle.Stop(true);
        aniDash = false;
    }
    #endregion
    //////////////////
    #region Animações
    private void AnimationJump()
    {
        if (!aniJump && EstaVivo && !aniDash)
        {
            armatureComponent.animation.Play("jump", 1);
            armatureComponent.animation.timeScale = 0.8f;
            aniWalk = false;
            aniIdle = false;
            aniJump = true;
            aniShot = false;
        }
    }
    private void AnimationIdle()
    {
        if (!aniIdle && !aniShot && EstaVivo && !aniDash)
        {

            armatureComponent.animation.Play("idle");
            armatureComponent.animation.timeScale = 0.5f;
            aniWalk = false;
            aniIdle = true;
            aniJump = false;
            aniShot = false;
        }
    }
    private void AnimationWalk()
    {
        if (!aniWalk && !aniJump && EstaVivo && !aniDash)
        {
            armatureComponent.animation.Play("walk");
            armatureComponent.animation.timeScale = 1f;
            aniWalk = true;
            aniIdle = false;
            aniJump = false;
            aniShot = false;
        }
    }
    private void AnimationShot()
    {
        if (!aniShot && !aniJump && EstaVivo && !aniDash)
        {
            armatureComponent.animation.Play("shot", 1);
            armatureComponent.animation.timeScale = 1.5f;
            aniWalk = false;
            aniIdle = false;
            aniJump = false;
            aniShot = true;
        }
    }
    private void AnimationDie()
    {
        if (!EstaVivo && !aniDie)
        {
            armatureComponent.animation.Play("dieF", 1);
            armatureComponent.animation.timeScale = 2;
            aniWalk = true;
            aniIdle = true;
            aniJump = true;
            aniShot = true;
            aniDie = true;
        }
    }
    private void AnimationDash(float y)
    {

        if (EstaVivo && !aniDash)
        {

            StartCoroutine("ParticulaDash", y);
            if (y != 0)
            {
                armatureComponent.animation.Play("dashAr");
            }
            else
            {
                armatureComponent.animation.Play("dashSolo");
            }
            armatureComponent.animation.timeScale = 2;
            aniWalk = false;
            aniIdle = false;
            aniJump = false;
            aniShot = false;
            aniDash = true;
        }
    }
    #endregion
    public bool EstaNoChao()
    {
        return estaNoChao;
    }
}