using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoControladaButton: MonoBehaviour
{
    // Start is called before the first frame update
    private Animator ani;
    public TurnButton button;
    public AudioClip sfx;
    public float volume;
    private bool estaVisivel = false;
    private AudioSource sfxSource;
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
        if (button.estaLigado)
        {
            ani.SetBool("Status", true);
            if (estaVisivel)
            {
                sfxSource.PlayOneShot(sfx, volume);
            }

        }
        else
        {
            ani.SetBool("Status", false);
        }
    }
}
