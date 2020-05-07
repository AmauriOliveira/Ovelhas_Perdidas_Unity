using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceChainSFX : MonoBehaviour
{
    public AudioSource sfxSource;
    private bool estaVisivel = false;
    private bool espera = false;

    void Start()
    {

        sfxSource = gameObject.GetComponent<AudioSource>();
        sfxSource.Stop();
    }

    private void OnBecameVisible()
    {
        estaVisivel = true;
    }
    private void OnBecameInvisible()
    {
        estaVisivel = false;
    }
    IEnumerator Espere()
    {
        yield return new WaitForSeconds(2);
        espera = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10 && !espera)
        {
            if (estaVisivel)
            {
                sfxSource.Play();
                StartCoroutine("Espere");
            }
            else if (!estaVisivel)
            {
                sfxSource.Stop();
            }
        }
    }
}