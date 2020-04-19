using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotacao : MonoBehaviour
{
    public float velocidade;
    public AudioSource sfxSource;
    public bool estaVisivel = false;
    public bool emiteSom = false;


    void Start()
    {
        if (emiteSom)
        {
            sfxSource = gameObject.GetComponent<AudioSource>();
            sfxSource.Stop();
        }

    }
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
        if (emiteSom)
        {
            if (estaVisivel)
            {

                sfxSource.Play();
            }
            else
            {
                sfxSource.Stop();
            }
        }

        transform.Rotate(new Vector3(0, 0, velocidade * Time.deltaTime));
    }
}
