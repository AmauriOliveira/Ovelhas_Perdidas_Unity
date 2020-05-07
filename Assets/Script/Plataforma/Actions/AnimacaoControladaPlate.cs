using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoControladaPlate : MonoBehaviour
{
    private Animator ani;
    public TurnPlate plate;
    public AudioClip sfx;
    public float volume;
    private bool estaVisivel = false;
    private AudioSource sfxSource;
    public bool inverso = false;
    private void OnBecameVisible()
    {
        estaVisivel = true;
    }
    private void OnBecameInvisible()
    {
        estaVisivel = false;
    }

    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        sfxSource = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (plate.estaLigado)
        {
            if (!inverso)
            {
                ani.SetBool("Status", true);
            }
            else
            {
                ani.SetBool("Status", false);
            }


            if (estaVisivel)
            {
                sfxSource.PlayOneShot(sfx, volume);
            }

        }
        else
        {
            if (!inverso)
            {
                ani.SetBool("Status", false);
            }
            else
            {
                ani.SetBool("Status", true);
            }
        }
    }
}