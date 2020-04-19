using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlate : MonoBehaviour
{
    private Animator plateAnimator;
    public bool estaLigado;
    public AudioClip sfxOn;
    public AudioClip sfxOff;

    void Start()
    {
        plateAnimator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Arrows" || other.gameObject.tag == "Player" && !estaLigado)
        {
            estaLigado = true;
            plateAnimator.SetBool("on", true);
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Arrows" || other.gameObject.tag == "Player" && estaLigado)
        {
            estaLigado = false;
            plateAnimator.SetBool("on", false);
        }

    }
}
