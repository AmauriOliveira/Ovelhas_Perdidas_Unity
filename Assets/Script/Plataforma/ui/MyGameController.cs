using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour
{
    public int fasePontos;
    public int coin;
    public int tempoFase = 0;
    private int tempoInicioFase;
    public int numeroFase;
    public bool temChave;
    public bool ovelhaLivre;
    private bool isPaused = false;
    public int pontuacaoRecord = 0;
    public bool temGaiola = true;
    public bool isTutorial = false;

    [Space]
    [Header("UI")]
    public Canvas UICanvas;
    public Image BarLife;
    public Image keyShow;
    public Image BarDash;
    public Text pontosUiText;
    public Text moedaUiText;
    public Text tempoUiText;
    [Space]
    [Header("Pause")]
    public Canvas pauseCanvas;
    [Space]
    [Header("Vitoria")]
    public Canvas vitoriaCanvas;
    public Button btnProximaV;
    public Button btnReseteV;
    public Button btnMenuV;
    public Text TxtPontos;
    public Text TxtRecord;
    [Space]
    [Header("Falha")]
    public Text TxtFalha;
    public Canvas falhaCanvas;
    [Space]
    [Header("Mensaguem")]
    public Text TxtFeedback;
    public Canvas CanvasFeedback;

    [Space]
    [Header("Limites")]
    public Transform LimiteTrasnformUp;
    public Transform LimiteTrasnformDown;
    public Transform LimiteTrasnformLeft;
    public Transform LimiteTrasnformRight;

    [Space]
    [Header("Player")]
    public GameObject playerGameObject;
    public Transform playerTrasnform;
    private PlayerBasicoVida playerBasicoVida;
    public float tempoComemorar = 3f;
    [Space]
    [Header("Camera")]
    private Camera cam;
    public float velocidadeCamera;
    [Space]
    [Header("Som")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    [Space]
    [Header("Sfx Game")]
    public AudioClip SfxFalha;
    public AudioClip SfxMenu;
    public AudioClip SfxAlert;
    public AudioClip SfxClick;
    public AudioClip SxfVitoria;
    public AudioClip SfxNewRecord;
    [Space]
    [Header("Sfx Player")]
    public AudioClip SxfJump;
    public AudioClip SxfHit;
    public AudioClip SxfDie;
    public AudioClip[] SxfFlechaDisparo;
    public AudioClip[] SxfArco;
    public AudioClip SxfDash;
    [Space]
    [Header("Sfx Coletaveis")]
    public AudioClip[] SxfCoin;
    public AudioClip SxfKey;
    public AudioClip SxfHeal;
    public AudioClip SfxExplosion;
    public AudioClip SfxSize;
    public AudioClip SfxImunite;
    [Space]
    [Header("Sfx Enemy")]
    public AudioClip SxfBatDie;
    public AudioClip SxfSlimeDie;
    public AudioClip SfxQuebrou;
    [Header("Sfx Ovelha")]
    public AudioClip[] SxfOvelhaSad;
    public AudioClip[] SxfOvelhaHappy;

    private void Awake()
    {
        // PlayerPrefs.DeleteAll();///temporario

        if (!PlayerPrefs.HasKey("fase" + numeroFase) && !isTutorial)
        {
            PlayerPrefs.SetInt("fase" + numeroFase, 1);
            PlayerPrefs.SetInt("faseRecord" + numeroFase, 0);
        }
    }

    void Start()

    {
        playerTrasnform = playerGameObject.GetComponent<Transform>();
        playerBasicoVida = playerGameObject.GetComponent<PlayerBasicoVida>();
        cam = Camera.main;
        tempoInicioFase = tempoFase;//seta o valor
        StartCoroutine("CronometroTempo", tempoFase);
        tempoUiText.text = tempoFase.ToString();
        LoadFase();
        Time.timeScale = 1f;

    }

    void Update()
    {
        BarLife.fillAmount = (float)playerBasicoVida.hp / playerBasicoVida.maxHP;
        if (temChave)
        {
            keyShow.color = new Color(1f, 1f, 1f, 0.9f);
        }
        else
        {
            keyShow.color = new Color(0.2f, 0.2f, 0.2f, 0.9f);
        }
        moedaUiText.text = coin.ToString();
        pontosUiText.text = fasePontos.ToString();
    }
    void LateUpdate()
    {
        CamereControle();
    }
    #region Funçoes
    void CamereControle()
    {
        float posCamX = playerTrasnform.position.x;
        float posCamY = playerTrasnform.position.y;


        if (cam.transform.position.x < LimiteTrasnformLeft.position.x && playerTrasnform.position.x < LimiteTrasnformLeft.position.x)//limite esquerdo
        {
            posCamX = LimiteTrasnformLeft.position.x;
        }
        else if (cam.transform.position.x > LimiteTrasnformRight.position.x && playerTrasnform.position.x > LimiteTrasnformRight.position.x)//limite direito
        {
            posCamX = LimiteTrasnformRight.position.x;
        }

        if (cam.transform.position.y > LimiteTrasnformUp.position.y && playerTrasnform.position.y > LimiteTrasnformUp.position.y) //limite superior
        {
            posCamY = LimiteTrasnformUp.position.y;
        }
        else if (cam.transform.position.y < LimiteTrasnformDown.position.y && playerTrasnform.position.y < LimiteTrasnformDown.position.y) //limite inferior
        {
            posCamY = LimiteTrasnformDown.position.y;
        }
        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);
        ////////////////////////////////////// onde estou,para onde vou, velocidade
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, velocidadeCamera * Time.deltaTime);

    }
    /// <param name="venceu">Este bool endica se o player venceu ou não a fase.</param>
    public void MostrarPontos(bool venceu, string msg)
    {
        musicSource.Stop();
        if (!isTutorial)
        {
            if (UICanvas.enabled)
            {
                UICanvas.gameObject.SetActive(false);

                if (venceu)
                {
                    vitoriaCanvas.gameObject.SetActive(true);
                    StartCoroutine("PontuacaoEfeito");

                    btnProximaV.interactable = false;
                    btnReseteV.interactable = false;
                    btnMenuV.interactable = false;

                    PlaySfx(SxfVitoria, 0.8f);
                }
                else
                {
                    TxtFalha.text = msg;
                    falhaCanvas.gameObject.SetActive(true);
                    Time.timeScale = 0f;

                    PlaySfx(SfxFalha, 0.4f);
                }
            }
        }
        else
        {
            PlaySfx(SxfVitoria, 1);
            Time.timeScale = 1f;
            StartCoroutine("FeedBack", "Tour guiado, Completo.");
            StartCoroutine("TutorialCompleto");
        }
    }
    public void SalveFase()
    {
        btnProximaV.interactable = true;
        btnReseteV.interactable = true;
        btnMenuV.interactable = true;
        Time.timeScale = 0f;
        if (fasePontos > pontuacaoRecord)
        {
            PlayerPrefs.SetInt("faseRecord" + numeroFase, fasePontos);
            TxtRecord.text = fasePontos.ToString();
            PlaySfx(SfxNewRecord, 1);
        }
    }
    public void LoadFase()
    {
        if (!isTutorial)
        {
            pontuacaoRecord = PlayerPrefs.GetInt("faseRecord" + numeroFase);
        }

    }
    public void GamePause(bool status)
    {
        PlaySfx(SfxMenu, 1);
        isPaused = status;
        if (status)
        {
            musicSource.Pause();
            Time.timeScale = 0f;
        }
        else
        {
            musicSource.UnPause();
            Time.timeScale = 1f;
        }
        pauseCanvas.gameObject.SetActive(status);
        UICanvas.gameObject.SetActive(!status);
    }
    public bool EmPausa()
    {
        return isPaused;
    }

    #endregion
    //////////////////
    #region Corotines
    public IEnumerator FeedBack(string msg)
    {
        CanvasFeedback.gameObject.SetActive(true);
        TxtFeedback.text = msg;
        yield return new WaitForSeconds(2);
        CanvasFeedback.gameObject.SetActive(false);
        TxtFeedback.text = "";
    }
    public IEnumerator OvelhaLivre()
    {
        ovelhaLivre = true;
        yield return new WaitForSeconds(tempoComemorar);
        MostrarPontos(true, null);
    }
    private IEnumerator TutorialCompleto()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Principal");
    }
    public IEnumerator PlayerMorto()
    {
        yield return new WaitForSeconds(1);
        MostrarPontos(false, "Tente Novamente");
    }
    public IEnumerator PontuacaoEfeito()
    {
        int acceleration = 10;

        TxtRecord.text = pontuacaoRecord.ToString();
        TxtPontos.text = fasePontos.ToString();
        tempoFase /= acceleration;

        yield return new WaitForSeconds(0.2f);//deley

        for (int i = tempoFase; i > 0; i--)
        {
            yield return new WaitForSeconds(0.02f);
            fasePontos += (5 * acceleration);
            tempoFase--;
            TxtPontos.text = fasePontos.ToString();
        }
        SalveFase();
    }
    public IEnumerator EfeitoDashBar()
    {
        BarDash.fillAmount = 0;
        for (int i = 1; i <= 30; i++)
        {
            yield return new WaitForSeconds(0.1f);
            BarDash.fillAmount = i * 0.033f;
        }
    }
    public IEnumerator CronometroTempo(int tempo)
    {
        for (int i = tempo; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            tempoFase--;
            tempoUiText.text = tempoFase.ToString();
            if (ovelhaLivre)
            {
                StopCoroutine("CronometroTempo");
            }
        }
        MostrarPontos(false, "Tempo esgotado.");
    }
    #endregion

    #region Audio
    public void PlaySfx(AudioClip sfx, float volume)
    {
        sfxSource.PlayOneShot(sfx, volume);
    }
    public void PlaySfxArray(AudioClip[] sfx, float volume)
    {
        sfxSource.PlayOneShot(sfx[Random.Range(0, sfx.Length)], volume);
    }

    #endregion
    #region Buttons
    public void btnPause()
    {
        PlaySfx(SfxClick, 1);
        GamePause(true);
    }
    public void btnResume()
    {
        PlaySfx(SfxClick, 1);
        GamePause(false);
    }
    public void btnReset()
    {
        PlaySfx(SfxClick, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Fase" + numeroFase);
    }
    public void btnResetTutorial()
    {
        PlaySfx(SfxClick, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }
    public void btnMenuPrincipal()
    {
        PlaySfx(SfxClick, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Principal");
    }
    public void btnProximaFase()
    {
        PlaySfx(SfxClick, 1);
        int temp = numeroFase + 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Fase" + temp);
    }
    #endregion
}