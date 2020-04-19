using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoControlada : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator ani;
    public TurnButton button;
    public AudioClip sfx;
    public float volume;
    private bool estaVisivel = false;
    public AudioSource sfxSource;
    private void OnBecameVisible()
    {
        estaVisivel = true;
    }
    private void OnBecameInvisible()
    {
        estaVisivel = false;
    }
    void Update()
    {
        if (button.estaLigado)
        {
            ani.SetBool("on", true);
            if (estaVisivel)
            {
                sfxSource.PlayOneShot(sfx, volume);
            }

        }
        else
        {
            ani.SetBool("off", false);
        }
    }
}
