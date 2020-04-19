using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    private Animator buttonAnimator;
    public bool estaLigado;
    public bool onParaOff;
    public bool offParaON;
    public float timerEmSegundos;
    public AudioClip sfxOn;
    public AudioClip sfxOff;
    public AudioClip sfxTimer;
    private AudioSource sfxSource;
    private bool habilitado = true;

    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
        buttonAnimator.SetBool("estaLigado", estaLigado);
        sfxSource = gameObject.GetComponent<AudioSource>();
    }
    public void mudarStatus()
    {

        if (estaLigado)
        {
            sfxSource.volume = 0.8f;
            sfxSource.clip = sfxOff;
            sfxSource.Play();
        }
        else
        {
            sfxSource.volume = 0.8f;
            sfxSource.clip = sfxOn;
            sfxSource.Play();
        }
        estaLigado = !estaLigado;
        buttonAnimator.SetBool("estaLigado", estaLigado);
        if ((onParaOff && estaLigado) || (offParaON && !estaLigado) && habilitado)
        {
            StartCoroutine("Mudar");
        }
    }
    IEnumerator Mudar()
    {
        sfxSource.loop = true;
        sfxSource.clip = sfxTimer;
        sfxSource.volume = 0.4f;
        sfxSource.Play();

        habilitado = false;
        yield return new WaitForSeconds(timerEmSegundos);

        sfxSource.Stop();
        sfxSource.loop = false;

        mudarStatus();
        habilitado = true;
    }
}