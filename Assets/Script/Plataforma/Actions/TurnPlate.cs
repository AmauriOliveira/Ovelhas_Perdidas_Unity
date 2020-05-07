using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlate : MonoBehaviour
{
    private Animator plateAnimator;
    public bool estaLigado;
    public AudioClip sfxOn;
    public AudioClip sfxOff;
    private AudioSource sfxSource;

    void Start()
    {
        sfxSource = gameObject.GetComponent<AudioSource>();
        plateAnimator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Arrows" || other.gameObject.tag == "Player" && !estaLigado)
        {
            estaLigado = true;
            plateAnimator.SetBool("on", true);
            sfxSource.PlayOneShot(sfxOn,1);
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Arrows" || other.gameObject.tag == "Player" && estaLigado)
        {
            estaLigado = false;
            plateAnimator.SetBool("on", false);
             sfxSource.PlayOneShot(sfxOff,1);
        }

    }
}